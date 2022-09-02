using Models;
using ScoreManager.Model.ViewParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IExamService : IBaseSerice<EDU_EXAM>
    {
        /// <summary>
        /// 查询总数 分页用到
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int CountByKeyWords(ExamListParameter parameter);
        /// <summary>
        /// 查询考试的所有信息 包括教务老师，班级，科目，试卷
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<EDU_EXAM> QueryFullExamInfo(ExamListParameter parameter, ref int totalCount);
        /// <summary>
        /// 添加考试
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool AddExam(AddExamParameter parameter, out string msg);
        /// <summary>
        /// 查询老师带的班级考试
        /// </summary>
        /// <param name="classIds"></param>
        /// <returns></returns>
        List<EDU_EXAM> QueryFullExamInfoByClassIds(List<long> classIds);

        /// <summary>
        /// 发布考试
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool ReleaseExam(EDU_EXAM exam, out string msg);
        /// <summary>
        /// 删除考试
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteExam(EDU_EXAM exam, out string msg);
        /// <summary>
        /// 阅卷老师查询需要阅卷的考试信息 携带班级信息
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        List<EDU_EXAM> QueryExamWithClassInfoByTId(long teacherId);
    }
}
