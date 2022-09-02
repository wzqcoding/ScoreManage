using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EDU_SCOREDETAIL
    {

        public EDU_SCOREDETAIL()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_SCOREDETAIL")]
        public long ID { get; set; }

        /// <summary>
        /// 考试详情id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long EXAMDETAILID { get; set; }

        /// <summary>
        /// 题目id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long QUESTIONID { get; set; }

        /// <summary>
        /// Desc:分数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public float SCORE { get; set; }
        /// <summary>
        /// 回答
        /// </summary>
        public string ANSWER { get; set; }
        /// <summary>
        /// 考试详情
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(EXAMDETAILID))]
        public EDU_EXAMDETIAL ExamDetail { get; set; }
        /// <summary>
        /// 题目
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(QUESTIONID))]
        public EDU_EXAMQUESTIONS Question { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ID),nameof(EDU_APPEAL.SCOREDETAILID))]
        public EDU_APPEAL Appeal { get; set; }
    }
}
