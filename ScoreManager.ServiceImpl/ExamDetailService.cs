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
    public class ExamDetailService : BaseService<EDU_EXAMDETIAL>, IExamDetailService
    {
        public ExamDetailService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 获取当前学生的考试信息 包括考试信息，学科信息，和考试详情 ,老师建议
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<EDU_EXAMDETIAL> QueryExamFullInfoByStudentId(long studentId)
        {

            return _sqlSugarClient.Queryable<EDU_EXAMDETIAL>()
                .Includes(x => x.MyExam, z => z.Exam_Subject)
                .Includes(x=>x.Suggestion,y=>y.Teacher)
                .Where(x => x.STUDENTID == studentId)?.ToList();
        }

        /// <summary>
        /// 根据考试id和学生id 查询学生的某场考试详情
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="stuId"></param>
        /// <returns></returns>
        public EDU_EXAMDETIAL QueryByExamIdAndStuId(long examId, long stuId)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMDETIAL>().Single(x => x.STUDENTID == stuId && x.EXAMID == examId);
        }

        /// <summary>
        /// 查询某个班级下的某场考试
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<EDU_EXAMDETIAL> QueryByExamIdAndClassId(long examId, long classId)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMDETIAL>()
                .Includes(x => x.Student)
                .Where(x => x.EXAMID == examId && x.Student.CLASSID == classId)?.ToList();
        }


        /// <summary>
        /// 查询班级成绩 携带建议和学生信息
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public List<EDU_EXAMDETIAL> QueryFullInfoByExaminId(int examId)
        {
            return _sqlSugarClient.Queryable<EDU_EXAMDETIAL>()
                .Includes(x => x.Student)
                .Includes(x => x.Suggestion)
                .Where(x => x.EXAMID == examId)?
                .OrderByDescending(x=>x.SCORE)
                .ToList();
        }
    }
}
