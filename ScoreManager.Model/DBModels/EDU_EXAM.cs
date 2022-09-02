using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class EDU_EXAM
    {
           public EDU_EXAM(){


           }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SqlSugar.SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_EXAM")]
        public long ID {get;set;}

           /// <summary>
           /// Desc:科目Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long SUBJECTID {get;set;}

           /// <summary>
           /// Desc:考试名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NAME {get;set;}
        /// <summary>
        /// 教务老师id
        /// </summary>
        public long EDU_TEACHERID { get; set; }
        /// <summary>
        /// 阅卷老师id
        /// </summary>
        public long MARKPAPERTEACHERID { get; set; }
        /// <summary>
        /// 是否已发布 0：未发布 1：已发布 默认0
        /// </summary>
        public string ISPUB { get; set; } = "0";
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public DateTime STARTTIME { get; set; }
        /// <summary>
        /// 考试结束时间
        /// </summary>
        public DateTime ENDTIME { get; set; }
        /// <summary>
        /// 班级id
        /// </summary>
        public long CLASSID { get; set; }
        /// <summary>
        /// 试卷id
        /// </summary>
        [SqlSugar.SugarColumn( IsIgnore =true)]
        public long PaperId { get; set; }

        /// <summary>
        /// 班级信息
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(CLASSID))]//多对一 
        public EDU_CLASS Exam_Class { get; set; }


        /// <summary>
        /// 学科信息
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(SUBJECTID))]
        public EDU_SUBJECT Exam_Subject { get; set; }


        /// <summary>
        /// 教务老师
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(EDU_TEACHERID))]
        public EDU_TEACHER Exam_EducationTeacher { get; set; }

        /// <summary>
        /// 阅卷老师
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(MARKPAPERTEACHERID))]
        public EDU_TEACHER Exam_MarkPaperTeacher { get; set; }
        /// <summary>
        /// 试卷
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PaperId))]
        public EDU_EXAMINE_PAPER Exam_Paper { get; set; }
    }
}
