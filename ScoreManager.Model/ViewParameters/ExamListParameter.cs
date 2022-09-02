using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.Model.ViewParameters
{
    public class ExamListParameter:BasePageParameter
    {
        public string KeyWords { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
