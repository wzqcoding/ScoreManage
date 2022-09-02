using Models;
using ScoreManager.Model.ViewParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceInterface
{
    public interface IStudentService : IBaseSerice<EDU_STUDENT>
    {
        /// <summary>
        /// 查询所有学生 携带班级信息
        /// </summary>
        /// <returns></returns>
        List<EDU_STUDENT> QueryStudentWithClassInfo(TeacherListParameter parameter,ref int totalCount);
        /// <summary>
        /// 根据关键词查询数量
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int CountByKeyWords(TeacherListParameter parameter);
        /// <summary>
        /// 学号是否重复
        /// </summary>
        /// <param name="studyCode"></param>
        /// <returns></returns>
        bool IsExist(string studyCode);
        /// <summary>
        /// 获取学生信息 携带班级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EDU_STUDENT GetFullInfoById(int id);
        /// <summary>
        /// 更新学生的信息 包括用户密码
        /// </summary>
        /// <param name="student"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool UpdateFullInfo(EDU_STUDENT student, out string msg);
        /// <summary>
        /// 批量更新学生的班级
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ids"></param>
        /// <param name="classId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool BatchUpdateClassId(string type, List<long> ids, int classId, out string msg);
        /// <summary>
        /// 删除学生的所有信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteFullInfo(int id, out string msg);
    }
}
