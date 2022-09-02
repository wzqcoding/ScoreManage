using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class AddStudentParameter
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        
        /// <summary>
        /// 用户类型 
        /// </summary>
        public short UserType { get => 2; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pass { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudyCode { get; set; }
        /// <summary>
        /// 班级id
        /// </summary>
        public int ClassId { get; set; }
    }
}
