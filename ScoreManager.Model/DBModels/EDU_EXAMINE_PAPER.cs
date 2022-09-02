using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models 
{ 
    public class EDU_EXAMINE_PAPER
    {
        public EDU_EXAMINE_PAPER()
        {

        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>  
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_EXAMINE_PAPER")]
        public long ID { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        [SugarColumn(DefaultValue ="默认试卷")]
        public string NAME { get; set; }
        /// <summary>
        /// 考试id
        /// </summary>
        public long EXAMINEID { get; set; }

        /// <summary>
        /// Desc:权限描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 是否启用 0：不启用 1：启用 默认启用
        /// </summary>
        public string ISENABLE { get; set; } = "0";

        [Navigate(NavigateType.OneToOne, nameof(EXAMINEID))]
        public EDU_EXAM EXAM { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(EDU_EXAMQUESTIONS.PAPERID))]
        public List<EDU_EXAMQUESTIONS> Questions { get; set; }
    }
}
