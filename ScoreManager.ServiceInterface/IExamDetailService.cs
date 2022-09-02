using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IExamDetailService : IBaseSerice<EDU_EXAMDETIAL>
    {
        /// <summary>
        /// 获取当前学生的考试信息 包括考试信息，学科信息，和考试详情
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        List<EDU_EXAMDETIAL> QueryExamFullInfoByStudentId(long studentId);
        /// <summary>
        /// 根据考试id和学生id 查询学生的某场考试详情
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="stuId"></param>
        /// <returns></returns>
        EDU_EXAMDETIAL QueryByExamIdAndStuId(long examId, long stuId);
        /// <summary>
        /// 查询某个班级下的某场考试
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        List<EDU_EXAMDETIAL> QueryByExamIdAndClassId(long examId, long classId);
        /// <summary>
        /// 查询班级成绩 携带建议和学生信息
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        List<EDU_EXAMDETIAL> QueryFullInfoByExaminId(int examId);
    }
}
