using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class EDU_STUDENT
    {
           public EDU_STUDENT(){


           }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_STUDENT")]
        public long ID {get;set;}

           /// <summary>
           /// Desc:班级id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? CLASSID {get;set;}

           /// <summary>
           /// Desc:姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NAME {get;set;}
        /// <summary>
        /// 用户id
        /// </summary>
        public long? USERID { get; set; }
        /// <summary>
        /// 是否已删除 0：已删除 1：未删除 默认未删除
        /// </summary>
        public string ISDELETE { get; set; } = "1";
        /// <summary>
        /// 学号
        /// </summary>
        public string STUDYCODE { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(CLASSID))]//一对一 
        public EDU_CLASS Class { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(USERID))]//一对一 
        public EDU_USER User { get; set; }

    }
}
