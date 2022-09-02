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
    public class ExaminPaperService : BaseService<EDU_EXAMINE_PAPER>, IExaminPaperService
    {
        public ExaminPaperService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 查询试卷总数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int CountByKeyWords(TeacherListParameter parameter)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMINE_PAPER>()
               .Where(x =>  x.DESCRIPTION.Contains(parameter.KeyWords)||x.NAME.Contains(parameter.KeyWords) ).Count();
        }
        /// <summary>
        /// 查询试卷信息 携带考试信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<EDU_EXAMINE_PAPER> QueryExaminPaperWithExaminInfo(TeacherListParameter parameter, ref int totalCount)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMINE_PAPER>()
              .Includes(x => x.EXAM)
              .Where(x => x.DESCRIPTION.Contains(parameter.KeyWords) || x.NAME.Contains(parameter.KeyWords))
              .ToPageList(parameter.PageIndex, parameter.PageSize, ref totalCount);
        }

        /// <summary>
        /// 增加试卷题目
        /// </summary>
        /// <param name="questions"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public bool AddQuestions(List<QuestionParameter> questions,int paperId)
        {
            List< EDU_EXAMQUESTIONS > data=new List< EDU_EXAMQUESTIONS >();
            foreach (var item in questions)
            {
                EDU_EXAMQUESTIONS question = new EDU_EXAMQUESTIONS()
                {
                    PAPERID=paperId,
                    ORDERINDEX=item.Order,
                    DESCRIPTION=item.Content,
                    RESULT=item.Result,
                    SCORE=item.Score
                };
                data.Add(question);
            }
            try
            {
                TransactionOperation(client =>
                {
                    //先删除
                    client.Deleteable<EDU_EXAMQUESTIONS>().Where(c => c.PAPERID == paperId).ExecuteCommand();
                    //再增加
                    client.Insertable(data).ExecuteCommand();
                });
                
            }
            catch (Exception ex)
            {
                //TODO  记录日志  
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据考试id 查询试卷信息 携带考题
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public EDU_EXAMINE_PAPER QueryExaminPaperWithQuestionInfo(long examId)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMINE_PAPER>().Includes(x => x.Questions).Single(x => x.EXAMINEID == examId);
        }

        /// <summary>
        /// 根据试卷id 查询试卷 携带考题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EDU_EXAMINE_PAPER QueryWithQuestionsById(int id)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMINE_PAPER>().Includes(x => x.Questions).Single(x => x.ID == id);
        }
    }
}
