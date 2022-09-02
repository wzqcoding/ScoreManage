using System;
using System.Collections.Generic;

namespace ScoreManager.Common
{
    /// <summary>
    /// 权限配置菜单
    /// </summary>
    public class ActionConfig
    {
        /// <summary>
        /// 字典管理
        /// </summary>
        public List<ActionItem> DicManageMenu { get; set; }
        /// <summary>
        /// 人员管理
        /// </summary>
        public List<ActionItem> PersonManageMenu { get; set; }
        /// <summary>
        /// 考试管理
        /// </summary>
        public List<ActionItem> ExamManageMenu { get; set; }
    }
    public class ActionItem
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 打开窗口显示的title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 菜单显示名称
        /// </summary>
        public string Label { get; set; }
    }
}
