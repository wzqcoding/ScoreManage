using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EDU_APPEAL
    {
        public EDU_APPEAL()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_APPEAL")]
        public long ID { get; set; }

        /// <summary>
        /// 得分详情id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long SCOREDETAILID { get; set; }

        /// <summary>
        /// 申诉内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string REASON { get; set; }

        /// <summary>
        /// Desc:申诉结果
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string  RESULT { get; set; }
        /// <summary>
        /// 处理老师id
        /// </summary>
        public long HANDLETEACHERID { get; set; }


        /// <summary>
        /// 考试详情
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SCOREDETAILID))]
        public EDU_SCOREDETAIL ScoreDetail { get; set; }
        /// <summary>
        /// 处理老师
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(HANDLETEACHERID))]
        public EDU_TEACHER HandelTeacher { get; set; }
    }
}
