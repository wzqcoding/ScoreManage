using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IAppealService : IBaseSerice<EDU_APPEAL>
    {
        /// <summary>
        /// 为某一题 申诉
        /// </summary>
        /// <param name="scoreDetailId"></param>
        /// <param name="appealContent"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        bool AddAppealForAnswer(long scoreDetailId, string appealContent,long teacherId);
        /// <summary>
        /// 处理学生申诉
        /// </summary>
        /// <param name="appealId"></param>
        /// <param name="result"></param>
        /// <param name="newScore"></param>
        /// <param name="scoreDetailId"></param>
        /// <returns></returns>
        bool HandleAppeal(int appealId, string result, float newScore, int scoreDetailId);
    }
}
