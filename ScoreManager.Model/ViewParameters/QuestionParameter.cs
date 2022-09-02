using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class QuestionParameter
    {
        /// <summary>
        /// 题目序号
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 试题内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 试题答案
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public float Score { get; set; }
    }
}
