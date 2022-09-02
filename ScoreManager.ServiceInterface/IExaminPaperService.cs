using Models;
using ScoreManager.Model.ViewParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IExaminPaperService : IBaseSerice<EDU_EXAMINE_PAPER>
    {
        /// <summary>
        /// 查询试卷总数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int CountByKeyWords(TeacherListParameter parameter);
        /// <summary>
        /// 查询试卷信息 携带考试信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<EDU_EXAMINE_PAPER> QueryExaminPaperWithExaminInfo(TeacherListParameter parameter, ref int totalCount);

        /// <summary>
        /// 增加试卷题目
        /// </summary>
        /// <param name="questions"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        bool AddQuestions(List<QuestionParameter> questions,int paperId);
        /// <summary>
        /// 根据考试id 查询试卷信息 携带考题
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        EDU_EXAMINE_PAPER QueryExaminPaperWithQuestionInfo(long examId);
        /// <summary>
        /// 根据试卷id 查询试卷 携带考题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EDU_EXAMINE_PAPER QueryWithQuestionsById(int id);
    }
}
