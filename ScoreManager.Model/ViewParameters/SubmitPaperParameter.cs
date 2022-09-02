using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class SubmitPaperParameter
    {
        /// <summary>
        /// 回答
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 试题id
        /// </summary>
        public long QuestionId { get; set; }
    }
}
