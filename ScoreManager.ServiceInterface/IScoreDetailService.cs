using Models;
using ScoreManager.Model.ViewParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IScoreDetailService : IBaseSerice<EDU_SCOREDETAIL>
    {
        /// <summary>
        /// 学生交卷
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="questions"></param>
        /// <returns></returns>
        bool SubmitPaper(long iD, List<SubmitPaperParameter> questions);
        /// <summary>
        /// 查询学生的答卷 携带考题信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        List<EDU_SCOREDETAIL> QueryWithQuestions(long examDetailId);
        /// <summary>
        /// 老师阅卷
        /// </summary>
        /// <param name="checkData"></param>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        bool CheckPaper(List<CheckPaperParameter> checkData, long examDetailId);
        /// <summary>
        /// 查询自己的答卷 携带考题信息和自己的申诉信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        List<EDU_SCOREDETAIL> QueryWithQuestionsAndAppeal(long examDetailId);
        /// <summary>
        /// 查询当前学生自己的申诉信息
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        List<EDU_SCOREDETAIL> QueryMyAppeal(long examDetailId);
        /// <summary>
        /// 查询当前学科老师需要处理的申诉
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        List<EDU_SCOREDETAIL> QueryAppealNeedToHandle(long teacherId);
    }
}
