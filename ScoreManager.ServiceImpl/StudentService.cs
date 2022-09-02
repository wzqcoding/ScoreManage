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
    public class StudentService : BaseService<EDU_STUDENT>, IStudentService
    {
        public StudentService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 根据关键词查询数量
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public int CountByKeyWords(TeacherListParameter parameter)
        {
            return _sqlSugarClient.Queryable<EDU_STUDENT>()
                .Where(x => x.NAME.Contains(parameter.KeyWords) && x.ISDELETE == "1").Count();
        }


        /// <summary>
        /// 查询所有学生 携带班级信息
        /// </summary>
        /// <returns></returns>
        public List<EDU_STUDENT> QueryStudentWithClassInfo(TeacherListParameter parameter, ref int totalCount)
        {
            return _sqlSugarClient.Queryable<EDU_STUDENT>()
               .Includes(x => x.Class)
               .Includes(x => x.User)
               .Where(x => x.NAME.Contains(parameter.KeyWords) && x.ISDELETE == "1")
               .ToPageList(parameter.PageIndex, parameter.PageSize, ref totalCount);

        }


        /// <summary>
        /// 学号是否重复
        /// </summary>
        /// <param name="studyCode"></param>
        /// <returns></returns>
        public bool IsExist(string studyCode)
        {
            return _sqlSugarClient.Queryable<EDU_STUDENT>().Count(c => c.STUDYCODE == studyCode) > 0;
        }

        /// <summary>
        /// 获取学生信息 携带班级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EDU_STUDENT GetFullInfoById(int id)
        {
            return _sqlSugarClient.Queryable<EDU_STUDENT>().Includes(x=>x.Class).Includes(x => x.User).Single(x => x.ID == id);
        }


        /// <summary>
        /// 更新学生的信息 包括用户密码
        /// </summary>
        /// <param name="student"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateFullInfo(EDU_STUDENT student, out string msg)
        {
            msg = "";

            try
            {
                return _sqlSugarClient.UpdateNav(student).Include(x => x.User).ExecuteCommand();
                
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// 批量更新学生的班级
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ids"></param>
        /// <param name="classId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool BatchUpdateClassId(string type, List<long> ids, int classId, out string msg)
        {
            msg = "";
            List<EDU_STUDENT> students = _sqlSugarClient.Queryable<EDU_STUDENT>().Where(c => ids.Contains(c.ID))?.ToList();
            foreach (var item in students)
            {
                if (type == "0")//新增
                {
                    item.CLASSID = classId;

                }
                else//去掉
                {
                    item.CLASSID = 0;
                }
            }
            try
            {
                return _sqlSugarClient.Updateable<EDU_STUDENT>(students).ExecuteCommand() > 0;
            }
            catch (Exception ex)
            {

                msg = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// 删除学生的一切信息 包括用户信息 班级信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteFullInfo(int id, out string msg)
        {
            msg = "";
            try
            {

                TransactionOperation(c => {
                    var student = c.Queryable<EDU_STUDENT>().Single(c => c.ID == id);
                    student.ISDELETE = "0";
                    student.CLASSID = 0;
                    var user = c.Queryable<EDU_USER>().Single(c => c.ID == student.USERID);
                    user.ISENABLE = "0";
                    //更新学生表状态
                    c.Updateable<EDU_TEACHER>(student).ExecuteCommand();
                    //更新用户表状态
                    c.Updateable<EDU_USER>(user).ExecuteCommand();

                });
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
