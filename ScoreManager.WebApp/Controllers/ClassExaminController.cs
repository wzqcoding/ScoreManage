using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ScoreManager.ServiceInterface;
using ScoreManager.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScoreManager.WebApp.Controllers
{

    [Authorize(Roles = "学科老师,班主任")]
    [Authorize(Roles = "Teacher")]

    //TODO  需要班主任 学科老师 角色权限
    public class ClassExaminController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IClassService _classService;
        private readonly IExamService _examService;
        private readonly IExamDetailService _examDetailService;
        private readonly ISuggestionService _suggestionService;
        public ClassExaminController(ITeacherService teacherService, IClassService classService, IExamService examService, IExamDetailService examDetailService, ISuggestionService suggestionService)
        {
            _teacherService = teacherService;
            _classService = classService;
            _examService = examService;
            _examDetailService = examDetailService;
            _suggestionService = suggestionService;
        }
        public IActionResult ClassExaminList()
        {
            //查询当前老师用户的班级
            int teacherId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "TeacherId").Value);
            List<long> classIds= _classService.QueryAllClassIdByTeacherId(teacherId);
            List<EDU_EXAM> list = _examService.QueryFullExamInfoByClassIds(classIds)?.OrderBy(x=>x.ID)?.ToList();
            return View(list);
        }
        /// <summary>
        /// 查询班级成绩详情
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public IActionResult ClassExaminDetail(int examId)
        {
           List<EDU_EXAMDETIAL> examinDetailList= _examDetailService.QueryFullInfoByExaminId(examId);
           return View(examinDetailList);
        }
        /// <summary>
        /// 老师提交建议
        /// </summary>
        /// <param name="ExamDetailId"></param>
        /// <param name="StudentId"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GiveSuggestion(int ExamDetailId,int StudentId,string Content)
        {
            int teacherId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "TeacherId").Value);
            EDU_SUGGESTIONS suggestion = new EDU_SUGGESTIONS()
            {
                EXAMDETAILID = ExamDetailId,
                STUDENTID = StudentId,
                CONTENT = Content,
                TEACHERID = teacherId
            };
            bool isSuccess = _suggestionService.Add(suggestion) > 0;
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error("提交失败"));

        }
    }
}
