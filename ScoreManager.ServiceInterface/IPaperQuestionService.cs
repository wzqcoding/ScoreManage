using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IPaperQuestionService : IBaseSerice<EDU_EXAMQUESTIONS>
    {
        /// <summary>
        /// 根据试卷id获取试卷题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<EDU_EXAMQUESTIONS> GetQuestionsByPaperId(int id);
    }
}
