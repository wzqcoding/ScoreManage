using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class UpdateStudentParameter
    {
        /// <summary>
        /// 学生id
        /// </summary>
        public int Id { get; set; }
       
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pass { get; set; }
       
        /// <summary>
        /// 班级id
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudyCode { get; set; }
    }
}
