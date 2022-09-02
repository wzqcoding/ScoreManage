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
    public class ExamService : BaseService<EDU_EXAM>, IExamService
    {
        public ExamService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 查询总数 分页用到
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int CountByKeyWords(ExamListParameter parameter)
        {
           var query=  _sqlSugarClient.Queryable<EDU_EXAM>()
               .Where(x => x.NAME.Contains(parameter.KeyWords));
            if (parameter.Start.HasValue)
            {
                query = query.Where(x => x.STARTTIME >= parameter.Start.Value);
            }
            if (parameter.End.HasValue)
            {
                query = query.Where(x => x.ENDTIME <= parameter.End.Value);
            }
            return query.Count();
        }


        /// <summary>
        /// 查询考试的所有信息 包括教务老师，班级，科目
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<EDU_EXAM> QueryFullExamInfo(ExamListParameter parameter, ref int totalCount)
        {
            var query = _sqlSugarClient.Queryable<EDU_EXAM>()
                 .Includes(x => x.Exam_Class)
                 .Includes(x => x.Exam_EducationTeacher)
                 .Includes(x => x.Exam_MarkPaperTeacher)
                 .Includes(x => x.Exam_Subject)
                 .Includes(x => x.Exam_Paper);

            if (parameter.Start.HasValue)
            {
                query = query.Where(x => x.STARTTIME >= parameter.Start.Value);
            }
            if (parameter.End.HasValue)
            {
                query = query.Where(x => x.ENDTIME <= parameter.End.Value);
            }
            return query.ToPageList(parameter.PageIndex, parameter.PageSize, ref totalCount);
        }
        /// <summary>
        /// 查询老师带的班级考试
        /// </summary>
        /// <param name="classIds"></param>
        /// <returns></returns>
        public List<EDU_EXAM> QueryFullExamInfoByClassIds(List<long> classIds)
        {
            var query = _sqlSugarClient.Queryable<EDU_EXAM>()
                .Includes(x => x.Exam_Class)
                .Includes(x => x.Exam_EducationTeacher)
                .Includes(x => x.Exam_MarkPaperTeacher)
                .Includes(x => x.Exam_Subject)
                .Includes(x => x.Exam_Paper);
            return query.Where(x => classIds.Contains(x.CLASSID))?.ToList();
        }
        /// <summary>
        /// 添加考试
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddExam(AddExamParameter parameter, out string msg)
        {
            msg = "";
            try
            {
                TransactionOperation(client =>
                {
                    //增加考试信息
                    EDU_EXAM exam = new EDU_EXAM();
                    exam.NAME = parameter.Examname;
                    exam.CLASSID = parameter.ClassId;
                    exam.SUBJECTID = parameter.SubjectId;
                    exam.EDU_TEACHERID = parameter.TeacherId;
                    exam.STARTTIME = parameter.StartTime;
                    exam.ENDTIME = parameter.EndTime;
                    exam.ISPUB = parameter.IsPub;
                    exam.MARKPAPERTEACHERID = parameter.MarkPaperTeacherId;

                    int examId = client.Insertable(exam).ExecuteReturnIdentity();
                    //更新试卷信息
                    EDU_EXAMINE_PAPER paper = client.Queryable<EDU_EXAMINE_PAPER>().Single(c => c.ID == parameter.PaperId);
                    paper.EXAMINEID = examId;
                    client.Updateable(paper).ExecuteCommand();

                    //如果发布 添加考试详情
                    if (parameter.IsPub=="1")
                    {
                        List<EDU_EXAMDETIAL> detailList = new List<EDU_EXAMDETIAL>();
                        var students = client.Queryable<EDU_STUDENT>().Where(x => x.CLASSID == parameter.ClassId)?.ToList();
                        foreach (var student in students)
                        {
                            EDU_EXAMDETIAL tempDetail = new EDU_EXAMDETIAL()
                            {
                                STUDENTID = student.ID,
                                EXAMID = examId,
                                Status = 0
                            };
                            detailList.Add(tempDetail);
                        }
                        client.Insertable<EDU_EXAMDETIAL>(detailList).ExecuteCommand();
                    }
                });
                return true;
            }
            catch (Exception)
            {
                msg = "创建考试失败";
                return false;
            }
           
        }

        /// <summary>
        /// 发布考试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ReleaseExam(EDU_EXAM exam, out string msg)
        {
            msg = "";
            try
            {
                TransactionOperation(client =>
                {
                    List<EDU_EXAMDETIAL> detailList = new List<EDU_EXAMDETIAL>();
                    var students = client.Queryable<EDU_STUDENT>().Where(x => x.CLASSID == exam.CLASSID)?.ToList();
                    foreach (var student in students)
                    {
                        EDU_EXAMDETIAL tempDetail = new EDU_EXAMDETIAL()
                        {
                            STUDENTID = student.ID,
                            EXAMID = exam.ID,
                            Status = 0
                        };
                        detailList.Add(tempDetail);
                    }
                    client.Insertable<EDU_EXAMDETIAL>(detailList).ExecuteCommand();
                    exam.ISPUB = "1";
                    client.Updateable(exam).ExecuteCommand();

                });
                return true;
            }
            catch (Exception)
            {
                msg = "发布考试失败";
                return false;
            }
            
        }
        /// <summary>
        /// 删除考试
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteExam(EDU_EXAM exam, out string msg)
        {
            msg = "";
            try
            {
                TransactionOperation(client =>
                {
                    //先修改试卷信息
                    EDU_EXAMINE_PAPER paper = client.Queryable<EDU_EXAMINE_PAPER>().Single(c => c.EXAMINEID == exam.ID);
                    paper.EXAMINEID = 0;
                    client.Updateable(paper).ExecuteCommand();
                    //物理删除考试
                    client.Deleteable<EDU_EXAM>(exam.ID).ExecuteCommand();
                });
                return true;
            }
            catch (Exception)
            {
                msg = "删除考试失败";
                return false;
            }
            
        }


        /// <summary>
        /// 阅卷老师查询需要阅卷的考试信息 携带班级信息
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public List<EDU_EXAM> QueryExamWithClassInfoByTId(long teacherId)
        {
            return _sqlSugarClient.Queryable<EDU_EXAM>().Includes(x => x.Exam_Class).Where(x => x.MARKPAPERTEACHERID == teacherId)?.ToList();
        }
    }
}
