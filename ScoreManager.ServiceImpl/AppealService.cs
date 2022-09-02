using Models;
using ScoreManager.ServiceInterface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceImpl
{
    public class AppealService : BaseService<EDU_APPEAL>, IAppealService
    {
        public AppealService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 为某一题 申诉
        /// </summary>
        /// <param name="scoreDetailId"></param>
        /// <param name="appealContent"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public bool AddAppealForAnswer(long scoreDetailId, string appealContent,long teacherId)
        {
            EDU_APPEAL appeal = new EDU_APPEAL()
            {
                SCOREDETAILID = scoreDetailId,
                REASON = appealContent,
                HANDLETEACHERID = teacherId
            };
            return Add(appeal) > 0;
        }

        /// <summary>
        /// 处理学生申诉
        /// </summary>
        /// <param name="appealId"></param>
        /// <param name="result"></param>
        /// <param name="newScore"></param>
        /// <param name="scoreDetailId"></param>
        /// <returns></returns>
        public bool HandleAppeal(int appealId, string result, float newScore, int scoreDetailId)
        {
            try
            {
                TransactionOperation(client =>
                {
                    var appeal = QueryById(appealId);
                    appeal.RESULT = result;
                    client.Updateable(appeal).ExecuteCommand();
                    EDU_SCOREDETAIL scoreDetail = client.Queryable<EDU_SCOREDETAIL>().Single(x => x.ID == scoreDetailId);
                    float oldScore = scoreDetail.SCORE;
                    scoreDetail.SCORE = newScore;
                    client.Updateable(scoreDetail).ExecuteCommand();

                    EDU_EXAMDETIAL examinDetail= client.Queryable<EDU_EXAMDETIAL>().Single(x => x.ID == scoreDetail.EXAMDETAILID);
                    examinDetail.SCORE = examinDetail.SCORE - oldScore + newScore;
                    client.Updateable(examinDetail).ExecuteCommand();

                });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
