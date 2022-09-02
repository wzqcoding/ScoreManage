﻿using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class EDU_SUBJECT
    {
           public EDU_SUBJECT(){


           }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_SUBJECT")]
        public long ID {get;set;}

           /// <summary>
           /// Desc:学科名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NAME {get;set;}
        /// <summary>
        /// Desc:权限描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime ADDTIME { get; set; }
        /// <summary>
        /// 是否启用 0：不启用 1：启用 默认启用
        /// </summary>
        public string ISENABLE { get; set; } = "1";

    }
}
