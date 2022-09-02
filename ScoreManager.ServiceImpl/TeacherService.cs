﻿using Models;
using ScoreManager.Model.Enum;
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
    public class TeacherService : BaseService<EDU_TEACHER>, ITeacherService
    {
        public TeacherService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 获取教师信息 携带用户信息 和 角色信息
        /// </summary>
        /// <param name="parameter">查询条件</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public List<EDU_TEACHER> GetTeacherListWithUserAndRole(TeacherListParameter parameter,ref int totalCount)
        {

           return _sqlSugarClient.Queryable<EDU_TEACHER>()
                .Includes(x => x.User)
                .Includes(x => x.Roles)
                .Includes(x => x.Subject)
                .Where(x=>(x.User.USERNAME.Contains(parameter.KeyWords)||x.NAME.Contains(parameter.KeyWords) )&& x.ISDELETE == "1")
                .ToPageList(parameter.PageIndex,parameter.PageSize,ref totalCount);

        }

        /// <summary>
        /// 根据关键词查询数量
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public int CountByKeyWords(TeacherListParameter parameter)
        {
            return _sqlSugarClient.Queryable<EDU_TEACHER>()
                .Includes(x => x.User)
                .Includes(x => x.Roles)
                .Where(x => (x.User.USERNAME.Contains(parameter.KeyWords) || x.NAME.Contains(parameter.KeyWords))&&x.ISDELETE=="1").Count();
        }

        /// <summary>
        /// 查询老师 带上角色信息
        /// </summary>
        /// <returns></returns>
        public  List<EDU_TEACHER> GetTeacherListWithRole()
        {
            return _sqlSugarClient.Queryable<EDU_TEACHER>()
               .Includes(x => x.Roles)
               .Where(x =>  x.ISDELETE == "1")?
               .ToList();
        }
        /// <summary>
        /// 根据id获取单个老师的全部信息  包括学科 角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EDU_TEACHER GetFullInfoById(int id)
        {
            try
            {
                return _sqlSugarClient.Queryable<EDU_TEACHER>()
                .Includes(x => x.User)
                .Includes(x => x.Roles)
                .Includes(x => x.Subject)
                .Single(x => x.ID == id);
            }
            catch (Exception ex)
            {

                return new EDU_TEACHER();
            }
            
              
        }


        /// <summary>
        /// 更新老师的用户密码，老师相关信息，角色相关信息
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateFullInfo(EDU_TEACHER teacher, out string msg)
        {
            msg = "";
            
            try
            {
                TransactionOperation(c =>
                {
                    c.UpdateNav(teacher).Include(x => x.User).ExecuteCommand();
                    c.Deleteable<EDU_TEACHER_ROLE>(c => c.TEACHERID == teacher.ID).ExecuteCommand();
                    if (teacher.Roles != null && teacher.Roles.Any())
                    {
                        List<EDU_TEACHER_ROLE> teacherRoles = new List<EDU_TEACHER_ROLE>();
                        foreach (var item in teacher.Roles)
                        {
                            teacherRoles.Add(new EDU_TEACHER_ROLE() { TEACHERID = teacher.ID, ROLEID = item.ID });
                        }
                        c.Insertable<EDU_TEACHER_ROLE>(teacherRoles).ExecuteCommand();
                    }

                });
                return true;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            
        }

        /// <summary>
        /// 删除老师的一切信息 包括用户信息 角色信息 
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
                    var teacher= c.Queryable<EDU_TEACHER>().Single(c => c.ID == id);
                    teacher.ISDELETE = "0";
                    var user= c.Queryable<EDU_USER>().Single(c => c.ID == teacher.USERID);
                    user.ISENABLE = "0";
                    //删除角色中间表
                    c.Deleteable<EDU_TEACHER_ROLE>(c => c.TEACHERID == id).ExecuteCommand();
                    //更新教师表状态
                    c.Updateable<EDU_TEACHER>(teacher).ExecuteCommand();
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
        /// <summary>
        /// 批量更新老师的学科信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ids"></param>
        /// <param name="subjectId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool BatchUpdateSubjectId(string type, List<long> ids, int subjectId, out string msg)
        {
            msg = "";
            List<EDU_TEACHER> teachers= _sqlSugarClient.Queryable<EDU_TEACHER>().Where(c => ids.Contains(c.ID))?.ToList();
            foreach (var item in teachers)
            {
                if (type == "0")//新增
                {
                    item.SUBJECTID = subjectId;

                }
                else//去掉
                {
                    item.SUBJECTID = 0;
                }
            }
            try
            {
                return _sqlSugarClient.Updateable<EDU_TEACHER>(teachers).ExecuteCommand() > 0;
            }
            catch (Exception ex)
            {

                msg = ex.Message;
                return false;
            }
           
        }



        /// <summary>
        /// 根据老师id 获取教师的权限集合
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public List<EDU_ACTION> GetTeacherActions(long iD)
        {
          return  _sqlSugarClient.Queryable<EDU_TEACHER, EDU_TEACHER_ROLE, EDU_ROLE, EDU_ROLE_ACTION, EDU_ACTION>((t, tr, r, ra, a) => new JoinQueryInfos(
                JoinType.Inner, t.ID == tr.TEACHERID,
                JoinType.Inner, tr.ROLEID == r.ID,
                JoinType.Inner, r.ID == ra.ROLEID,
                JoinType.Inner, ra.ACTIONID == a.ID
                )).Where((t, tr, r, ra, a)=>t.ID==iD).Select((t, tr, r, ra, a) => a)?.ToList();
        }
    }
}
