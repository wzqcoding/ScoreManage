using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ScoreManager.Model.ViewParameters;
using ScoreManager.ServiceInterface;
using ScoreManager.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScoreManager.WebApp.Controllers
{
    [Authorize(Roles ="学科老师")]
    [Authorize(Roles = "教务老师")]
    public class ExaminPaperController : Controller
    {
        private IExaminPaperService _examinPaperService;
        private IPaperQuestionService _paperQuestionService;
        public ExaminPaperController(IExaminPaperService examinPaperService, IPaperQuestionService paperQuestionService)
        {
            _examinPaperService = examinPaperService;
            _paperQuestionService = paperQuestionService;
        }
        public IActionResult ExaminPaperList()
        {
            return View();
        }

        /// <summary>
        /// 查询分页信息
        /// </summary>
        /// <param name="KeyWords">关键词</param>
        /// <returns></returns>
        public IActionResult ExaminPaperListPageInfo(TeacherListParameter parameter)
        {
            int totalCount = _examinPaperService.CountByKeyWords(parameter);
            return Json(new { TotalCount = totalCount, PageSize = parameter.PageSize });
        }

        public IActionResult ExaminPaperListDetail(TeacherListParameter parameter)
        {
            //查询试卷列表数据
            int totalCount = 0;
            var list = _examinPaperService.QueryExaminPaperWithExaminInfo(parameter, ref totalCount)?.OrderBy(x=>x.ID)?.ToList();
            ViewData["TotalCount"] = totalCount;
            ViewData["PageSize"] = parameter.PageSize;
            ViewData["PageIndex"] = parameter.PageIndex;
            return PartialView("ExaminPaperListDetail", list);
        }
        /// <summary>
        /// 增加试卷
        /// </summary>
        /// <returns></returns>
        public IActionResult AddPaper()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPaper(AddPaperParameter paper)
        {
            ApiResult apiResult = new ApiResult() { Code = 0 };
            EDU_EXAMINE_PAPER eduPaper = new EDU_EXAMINE_PAPER();
            eduPaper.NAME = paper.PaperName;
            eduPaper.DESCRIPTION = paper.ParperDes;
            try
            {
                _examinPaperService.Add(eduPaper);

            }
            catch (System.Exception)
            {
                apiResult.Message = "创建试卷失败";
                apiResult.Code = -1;
            }
            return Json(apiResult);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditPaper(int id)
        {
            var examPaper= _examinPaperService.QueryById(id);
            return View(examPaper);
        }
        [HttpPost]
        
        public IActionResult EditPaper(int id, string parperDes)
        {
            ApiResult result = new ApiResult();
            var examPaper = _examinPaperService.QueryById(id);
            examPaper.DESCRIPTION = parperDes;
            bool isSuccess=  _examinPaperService.Update(examPaper);
            if (isSuccess)
            {
                result.Code = 0;
            }
            else
            {
                result.Code = -1;
                result.Message = "修改失败";
            }

            return Json(result);
        }



       
        /// <summary>
        /// 启动 或禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EnableOrDisAbledPaper(int id, string status)
        {
            ApiResult result = new ApiResult();
            //启用前检查试卷是否有题目
            EDU_EXAMINE_PAPER examPaper = _examinPaperService.QueryWithQuestionsById(id);
            if (status=="1"&&( examPaper.Questions == null||examPaper.Questions.Count==0))
            {
                return Json( ApiResult.Error("请编写好试题后再启用"));
            }
            examPaper.ISENABLE = status;
            bool isSuccess = _examinPaperService.Update(examPaper);
            if (isSuccess)
            {
                result.Code = 0;
            }
            else
            {
                result.Code = -1;
                result.Message = "修改失败";
            }

            return Json(result);
        }

        /// <summary>
        /// 编写考题
        /// </summary>
        /// <param name="id">试卷id</param>
        /// <returns></returns>
        public IActionResult EditPaperQuestions(int id)
        {
            ViewBag.PaperId = id;
            //查询试卷考题
            List<EDU_EXAMQUESTIONS> questions=_paperQuestionService.GetQuestionsByPaperId(id);
            //如果试卷已经被发布，能看不能改
            bool IsInUse = false;
            if (questions!=null&&questions.Any())
            {
                IsInUse = questions.First().PAPER?.EXAM?.ISPUB == "1";
            }
            ViewBag.IsInUse = IsInUse;
            return View(questions);
        }
        /// <summary>
        /// 给试卷增加题目
        /// </summary>
        /// <param name="questions"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddQuestions(List<QuestionParameter> questions,int Id)
        {
            ApiResult result = new ApiResult();
            bool isSuccess = _examinPaperService.AddQuestions(questions, Id);
            if (isSuccess)
            {
                result.Code = 0;
            }
            else
            {
                result.Code = -1;
                result.Message = "修改失败";
            }

            return Json(result);
        }
    }
}
