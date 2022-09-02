using Models;
using Newtonsoft.Json;
using ScoreManager.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Common
{
    public class CommonHelper
    {
        private ITeacherService _teacherService;
        public CommonHelper(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        public static ActionConfig ActionConfig { get; private set; }
        /// <summary>
        /// 根据用户类型 获取用户菜单权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SetActionConfigByUser(EDU_USER user)
        {
            ActionConfig config = new ActionConfig()
            {
                DicManageMenu = new List<ActionItem>(),
                PersonManageMenu = new List<ActionItem>(),
                ExamManageMenu = new List<ActionItem>()
            };
            if (user.TYPE == 0)//管理员权限代码里边写固定
            {
                config.DicManageMenu = new List<ActionItem>()
               {
                    new ActionItem(){Url="/Teacher/ActionList",Title="教师权限列表",Label="教师权限"},
                   new ActionItem(){Url="/Teacher/RoleList",Title="教师角色列表",Label="教师角色"},

                   new ActionItem(){Url="/Subject/SubjectList",Title="学科管理",Label="学科管理"},
                   new ActionItem(){Url="/Class/ClassList",Title="班级管理",Label="班级管理"},
               };
                config.PersonManageMenu = new List<ActionItem>()
                {
                    new ActionItem(){ Url="/Teacher/TeacherList",Title="教师管理",Label="教师管理"},
                    new ActionItem(){ Url="/Student/StudentList",Title="学生管理",Label="学生管理"},
                };
            }
            if (user.TYPE == 1)//教师权限走权限配置
            {
                //获取教师权限
                List<EDU_ACTION> actions = _teacherService.GetTeacherActions(user.Teacher.ID);
                foreach (var item in actions)
                {
                    if (string.IsNullOrEmpty(item.CONFIG))
                    {
                        continue;
                    }
                    ActionConfig temconfig = JsonConvert.DeserializeObject<ActionConfig>(item.CONFIG);
                    if (temconfig.PersonManageMenu != null)
                    {
                        config.PersonManageMenu.AddRange(temconfig.PersonManageMenu);
                    }
                    if (temconfig.DicManageMenu != null)
                    {
                        config.DicManageMenu.AddRange(temconfig.DicManageMenu);
                    }
                    if (temconfig.ExamManageMenu != null)
                    {
                        config.ExamManageMenu.AddRange(temconfig.ExamManageMenu);
                    }
                }
                //去重
                config.PersonManageMenu = config.PersonManageMenu?.Distinct(new ActionConfigCompare())?.ToList();
                config.DicManageMenu = config.DicManageMenu?.Distinct(new ActionConfigCompare())?.ToList();
                config.ExamManageMenu = config.ExamManageMenu?.Distinct(new ActionConfigCompare())?.ToList();
            }
            if (user.TYPE == 2)//学生权限同样走代码
            {
                config.ExamManageMenu = new List<ActionItem>()
                {
                    new ActionItem(){ Url="/Exam/MyExamList",Title="我的考试",Label="我的考试"},

                };
            }
            ActionConfig = config;
        }
    }
    internal class ActionConfigCompare : IEqualityComparer<ActionItem>
    {
        public bool Equals(ActionItem x, ActionItem y)
        {
            return x.Title == y.Title && x.Url == y.Url && x.Label == y.Label;
        }

        public int GetHashCode([DisallowNull] ActionItem obj)
        {
            return obj.Url.GetHashCode();
        }
    }
}
