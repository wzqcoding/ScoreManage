using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class EDU_EXAMDETIAL
    {
           public EDU_EXAMDETIAL(){


           }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_EXAMDETIAL")]
        public long ID {get;set;}

           /// <summary>
           /// Desc:学生id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long STUDENTID {get;set;}

           /// <summary>
           /// Desc:考试Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long EXAMID {get;set;}

           /// <summary>
           /// Desc:总分数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public float SCORE {get;set;}

           
        /// <summary>
        /// 状态 0：未开始  1:考完  2：阅卷完毕 默认0
        /// </summary>
        public short  Status { get; set; }
        /// <summary>
        /// 我的考试
        /// </summary>
        [Navigate(NavigateType.ManyToOne,nameof(EXAMID))]
        public EDU_EXAM MyExam { get; set; }

        /// <summary>
        /// 学生
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(STUDENTID))]
        public EDU_STUDENT Student { get; set; }


        /// <summary>
        /// 建议
        /// </summary>
        [Navigate(NavigateType.OneToOne,nameof(ID), nameof(EDU_SUGGESTIONS.EXAMDETAILID))]
        public EDU_SUGGESTIONS Suggestion { get; set; }



    }
}
