using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class AddExamParameter
    {
        /// <summary>
        /// 考试名称
        /// </summary>
        public string Examname { get; set; }
        /// <summary>
        /// 班级id
        /// </summary>
        public long ClassId { get; set; }
        /// <summary>
        /// 学科id
        /// </summary>
        public long SubjectId { get; set; }
        /// <summary>
        /// 教务老师id
        /// </summary>
        public long TeacherId { get; set; }
        /// <summary>
        /// 阅卷老师id
        /// </summary>
        public long MarkPaperTeacherId { get; set; }
        /// <summary>
        /// 试卷id
        /// </summary>
        public long PaperId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public string IsPub { get; set; } = "0";

    }
}
