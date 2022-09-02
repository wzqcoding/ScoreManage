using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EDU_EXAMQUESTIONS
    {
        public EDU_EXAMQUESTIONS()
        {

        }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>  
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_EXAMQUESTIONS")]
        public long ID { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        public long PAPERID { get; set; }
        /// <summary>
        /// 试题序号
        /// </summary>
        public int ORDERINDEX { get; set; }

        /// <summary>
        /// Desc:试题内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public float SCORE { get; set; }
        /// <summary>
        /// 试题答案
        /// </summary>
        public string RESULT { get; set; }

        [Navigate(NavigateType.ManyToOne, nameof(PAPERID))]
        public EDU_EXAMINE_PAPER PAPER { get; set; }
    }
}
