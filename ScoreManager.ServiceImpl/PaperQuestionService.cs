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
    public class PaperQuestionService : BaseService<EDU_EXAMQUESTIONS>, IPaperQuestionService
    {
        public PaperQuestionService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 根据试卷id获取试卷题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EDU_EXAMQUESTIONS> GetQuestionsByPaperId(int id)
        {
            List<EDU_EXAMQUESTIONS> retData = _sqlSugarClient.Queryable<EDU_EXAMQUESTIONS>().Includes(x => x.PAPER,c=>c.EXAM).Where(x=>x.PAPERID==id)?.ToList();
            return retData;
        }
    }
}
