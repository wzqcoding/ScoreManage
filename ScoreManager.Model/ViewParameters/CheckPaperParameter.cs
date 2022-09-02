using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class CheckPaperParameter
    {
        /// <summary>
        /// 题目得分
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// 学生答题id
        /// </summary>
        public long ScoreDetailId { get; set; }
    }
}
