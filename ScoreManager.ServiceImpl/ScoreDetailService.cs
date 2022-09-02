using Models;
using ScoreManager.Model.ViewParameters;
using ScoreManager.ServiceInterface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceImpl
{
    public class ScoreDetailService : BaseService<EDU_SCOREDETAIL>, IScoreDetailService
    {
        public ScoreDetailService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 学生交卷
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="questions"></param>
        /// <returns></returns>
        public bool SubmitPaper(long iD, List<SubmitPaperParameter> questions)
        {
            List<EDU_SCOREDETAIL> scores = new List<EDU_SCOREDETAIL>();
            foreach (var item in questions)
            {
                EDU_SCOREDETAIL tempScore = new EDU_SCOREDETAIL()
                {
                    EXAMDETAILID = iD,
                    ANSWER = item.Answer,
                    QUESTIONID = item.QuestionId
                };
                scores.Add(tempScore);
            }
            try
            {
                TransactionOperation(client =>
                {
                    //插入得分详情表
                    client.Insertable(scores).ExecuteCommand();
                    //更改考试详情状态为 考完
                    var examDetail= client.Queryable<EDU_EXAMDETIAL>().Single(x=>x.ID==iD);
                    examDetail.Status = 1;
                    client.Updateable(examDetail).ExecuteCommand();
                });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        /// 查询学生的答卷 携带考题信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        public List<EDU_SCOREDETAIL> QueryWithQuestions(long examDetailId)
        {
            return _sqlSugarClient.Queryable<EDU_SCOREDETAIL>().Includes(x => x.Question).Where(x=>x.EXAMDETAILID==examDetailId)?.ToList();
        }

        /// <summary>
        /// 老师阅卷
        /// </summary>
        /// <param name="checkData"></param>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        public bool CheckPaper(List<CheckPaperParameter> checkData, long examDetailId)
        {
            var ids= checkData.Select(x => x.ScoreDetailId);
            List<EDU_SCOREDETAIL> scoreDetailList = _sqlSugarClient.Queryable<EDU_SCOREDETAIL>().Where(x => ids.Contains(x.ID))?.ToList();
            float totalScore=0;
            foreach (var item in scoreDetailList)
            {
                var cdata= checkData.Single(x => x.ScoreDetailId == item.ID);
                item.SCORE = cdata.Score;
                totalScore+=cdata.Score;
            }
            try
            {
                TransactionOperation(client =>
                {
                    client.Updateable(scoreDetailList).ExecuteCommand();
                    var examDetail = client.Queryable<EDU_EXAMDETIAL>().Single(x => x.ID == examDetailId);
                    examDetail.Status = 2;
                    examDetail.SCORE = totalScore;
                    client.Updateable(examDetail).ExecuteCommand();
                });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }


        /// <summary>
        /// 查询自己的答卷 携带考题信息和自己的申诉信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        public List<EDU_SCOREDETAIL> QueryWithQuestionsAndAppeal(long examDetailId)
        {
            return _sqlSugarClient.Queryable<EDU_SCOREDETAIL>().Includes(x => x.Question).Includes(x=>x.Appeal).Where(x => x.EXAMDETAILID == examDetailId)?.ToList();
        }

        /// <summary>
        /// 查询自己的申诉信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        public List<EDU_SCOREDETAIL> QueryMyAppeal(long examDetailId)
        {
            return _sqlSugarClient.Queryable<EDU_SCOREDETAIL>()
                .Includes(x => x.Question)
                .Includes(x => x.Appeal,c=>c.HandelTeacher).Where(x => x.EXAMDETAILID == examDetailId)?.ToList();
        }

        /// <summary>
        /// 查询当前学科老师需要处理的申诉
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public List<EDU_SCOREDETAIL> QueryAppealNeedToHandle(long teacherId)
        {
            return _sqlSugarClient.Queryable<EDU_SCOREDETAIL>()
                .Includes(x => x.Question)
                .Includes(x => x.Appeal, c => c.HandelTeacher).Where(x=>x.Appeal.HANDLETEACHERID==teacherId)?.ToList();
        }
    }
}
