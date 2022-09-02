using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ScoreManager.ServiceInterface;
using ScoreManager.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScoreManager.WebApp.Controllers
{
   
    public class AppealController : Controller
    {
        private readonly IScoreDetailService _scoreDetailService;
        private readonly IAppealService _appealService;
        private readonly ITeacherService _teacherService;
        public AppealController(IScoreDetailService scoreDetailService, IAppealService appealService, ITeacherService teacherService)
        {
            _scoreDetailService = scoreDetailService;
            _appealService = appealService;
            _teacherService = teacherService;
        }
        /// <summary>
        /// 填写申诉
        /// </summary>
        /// <param name="ScoreDetailId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        public IActionResult WriteAppeal(long ScoreDetailId)
        {
            //查询学科老师
            var teachers=  _teacherService.GetTeacherListWithRole().Where(c => c.Roles.Any(x => x.NAME == "学科老师"));
            ViewBag.Teachers = teachers;
            ViewBag.ScoreDetailId = ScoreDetailId;
            return View();
        }

        /// <summary>
        /// 提交我的申诉  TODO 需要权限
        /// </summary>
        /// <param name="AppealContent"></param>
        /// <param name="ScoreDetailId"></param>
        /// <param name="TeacherId">学科老师id</param>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        [HttpPost]
        public IActionResult AppealForMyAnswer(long ScoreDetailId, string AppealContent,long TeacherId)
        {
            bool isSuccess = _appealService.AddAppealForAnswer(ScoreDetailId, AppealContent,TeacherId);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error("提交申诉失败"));
        }
        /// <summary>
        /// 查询自己某场考试的申诉  TODO 需要权限
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        public IActionResult MyAppeal(long examDetailId)
        {
            List<EDU_SCOREDETAIL> scoreDetails = _scoreDetailService.QueryMyAppeal(examDetailId);
            //过滤自己的申诉

            scoreDetails = scoreDetails.Where(x => x.Appeal != null)?.ToList();
            return View(scoreDetails);
        }

        /// <summary>
        /// 当前老师要处理的学生申诉
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "学科老师")]
        public IActionResult AppealList()
        {
            //当前登录老师 TODO 必须是学科老师
            long teacherId = long.Parse(HttpContext.User.Claims.First(c => c.Type == "TeacherId").Value);
            //获取当前老师未处理的申诉
            List<EDU_SCOREDETAIL> scoreDetails = _scoreDetailService.QueryAppealNeedToHandle(teacherId)?.OrderBy(x=>x.ID)?.ToList();
            return View(scoreDetails);
        }
        /// <summary>
        /// 处理申诉
        /// </summary>
        /// <param name="AppealId"></param>
        /// <param name="Result"></param>
        /// <param name="NewScore"></param>
        /// <param name="ScoreDetailId"></param>
        /// <returns></returns>
        [Authorize(Roles = "学科老师")]
        [HttpPost]
        public IActionResult HandleAppeal(int AppealId,string Result,float NewScore,int ScoreDetailId)
        {
            bool isSuccess = _appealService.HandleAppeal(AppealId, Result, NewScore, ScoreDetailId);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error("处理失败"));
        }
    }
}
