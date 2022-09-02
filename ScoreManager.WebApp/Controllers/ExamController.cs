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
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;
        private readonly IExaminPaperService _examinPaperService;
        private readonly IExamDetailService _examDetailService;
        private readonly IScoreDetailService _scoreDetailService;
        public ExamController(IExamService examService,
            IClassService classService,
            ITeacherService teacherService,
            ISubjectService subjectService,
            IExaminPaperService examinPaperService,
            IExamDetailService examDetailService,
            IScoreDetailService scoreDetailService
            )
        {
            _examService = examService;
            _classService = classService;
            _teacherService = teacherService;
            _subjectService = subjectService;
            _examinPaperService = examinPaperService;
            _examDetailService = examDetailService;
            _scoreDetailService = scoreDetailService;
        }
        [Authorize(Roles = "教务老师")]
        public IActionResult ExamList()
        {
            return View();
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IActionResult ExamListPageInfo(ExamListParameter parameter)
        {
            int totalCount = _examService.CountByKeyWords(parameter);
            return Json(new { TotalCount = totalCount, PageSize = parameter.PageSize });
        }
        /// <summary>
        /// 查询列表详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IActionResult ExamListDetail(ExamListParameter parameter)
        {
            //查询考试列表数据
            int totalCount = 0;
            List<EDU_EXAM> list = _examService.QueryFullExamInfo(parameter, ref totalCount)?.OrderBy(x=>x.ID)?.ToList();
            ViewData["TotalCount"] = totalCount;
            ViewData["PageSize"] = parameter.PageSize;
            ViewData["PageIndex"] = parameter.PageIndex;
            return PartialView("ExamListDetail", list);
        }

        /// <summary>
        /// 增加考试
        /// </summary>
        /// <returns></returns>
        public IActionResult AddExam()
        {
            //查询班级
            ViewBag.EnableClass = _classService.Query().Where(x => x.ISENABLE == "1");
            //查询教务老师
            ViewBag.EnableTeacher = _teacherService.GetTeacherListWithRole().Where(x => x.Roles.Any(c => c.NAME == "教务老师"));
            //查询阅卷老师
            ViewBag.MarkPaperTeacher = _teacherService.GetTeacherListWithRole().Where(x => x.Roles.Any(c => c.NAME == "阅卷老师"));
            //查询学科
            ViewBag.EnableSubject = _subjectService.Query().Where(x => x.ISENABLE == "1");
            //查询试卷
            ViewBag.EnablePaper = _examinPaperService.Query().Where(x => x.ISENABLE == "1"&&x.EXAMINEID==0);
            return View();
        }

        /// <summary>
        /// 增加考试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddExam(AddExamParameter parameter)
        {
            string msg;
            bool isSuccess = _examService.AddExam(parameter, out msg);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error(msg));
        }
        /// <summary>
        /// 发布考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ReleaseExam(int id)
        {
            //检查是否已经发布
            var exam= _examService.QueryById(id);
            if (exam.ISPUB=="1")
            {
                return Json(ApiResult.Error("该考试已经发布过，不用再次发布"));
            }
            string msg;
            bool isSuccess = _examService.ReleaseExam(exam, out msg);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error(msg));
        }
        /// <summary>
        /// 删除考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteExam(int id)
        {
            //检查是否已经发布
            var exam = _examService.QueryById(id);
            if (exam.ISPUB == "1")
            {
                return Json(ApiResult.Error("该考试已经发布过，不能删除"));
            }
            string msg;
            bool isSuccess = _examService.DeleteExam(exam, out msg);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error(msg));
        }
        /// <summary>
        /// 我的考试  TODO 权限 学生可进
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Student")]

        public IActionResult MyExamList()
        {
            //获取当前学生id
            long studentId = long.Parse(HttpContext.User.Claims.First(c => c.Type == "StudentId").Value);
            //获取学生的考试信息
            List<EDU_EXAMDETIAL> examList = _examDetailService.QueryExamFullInfoByStudentId(studentId)?.OrderBy(x=>x.ID)?.ToList();
            return View(examList);
        }
        /// <summary>
        /// 进入考试
        /// </summary>
        /// <returns></returns>
        public IActionResult EnterExam(long examId)
        {
            //获取试卷 试题
            ViewBag.StudentName= HttpContext.User.Claims.First(c => c.Type == "StudentName").Value;
            EDU_EXAMINE_PAPER paper = _examinPaperService.QueryExaminPaperWithQuestionInfo(examId);
            ViewBag.ExamId= examId;

            return View(paper);
        }
        /// <summary>
        /// 交卷
        /// </summary>
        /// <param name="Questions"></param>
        /// <param name="ExamId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitPaper(List<SubmitPaperParameter> Questions,long ExamId)
        {
            //先查出来试卷详情id
           long stuId=long.Parse( HttpContext.User.Claims.First(c => c.Type == "StudentId").Value);
            EDU_EXAMDETIAL detail= _examDetailService.QueryByExamIdAndStuId(ExamId, stuId);
            bool isSuccess= _scoreDetailService.SubmitPaper(detail.ID, Questions);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error("交卷失败"));
            
        }
        /// <summary>
        /// 考试阅卷  //TODO  鉴权 必须是阅卷老师角色可以进入
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "阅卷老师")]
        public IActionResult MarkPaperList()
        {
            //获取当前阅卷老师id
            long teacherId = long.Parse(HttpContext.User.Claims.First(c => c.Type == "TeacherId").Value);
            //查询当前老师需要阅卷的考试信息
            List<EDU_EXAM> examList = _examService.QueryExamWithClassInfoByTId(teacherId);//Query().Where(x => x.MARKPAPERTEACHERID == teacherId)?.ToList();

            return View(examList);
        }

       /// <summary>
       /// 进入某个班级的阅卷 //TODo 需要权限
       /// </summary>
       /// <param name="examId"></param>
       /// <param name="classId"></param>
       /// <returns></returns>
        public IActionResult MarkPaper(long examId,long classId)
        {
            List<EDU_EXAMDETIAL> detailList= _examDetailService.QueryByExamIdAndClassId(examId, classId)?.OrderBy(x=>x.ID)?.ToList();
            return View(detailList);
        }
        /// <summary>
        /// 进入某个学生的考卷前先验证状态
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckExamStatus(int examDetailId)
        {
            var detail= _examDetailService.QueryById(examDetailId);
            if (detail.Status!=1)
            {
                return Json(ApiResult.Error("当前试卷状态不能阅卷"));
            }
            return Json(ApiResult.OK());
        }
        /// <summary>
        /// 阅卷老师查看某个学生的试卷 打分 TODO 需要权限
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public IActionResult CheckPaper(long examDetailId,string studentName)
        {
            ViewBag.StudentName=studentName;
            ViewBag.ExamDetailId = examDetailId;
            List<EDU_SCOREDETAIL> scoreDetails= _scoreDetailService.QueryWithQuestions(examDetailId)?.OrderBy(x=>x.ID)?.ToList();
            return View(scoreDetails);
        }
        /// <summary>
        /// 阅卷老师提交试卷打分 打分 TODO 需要权限
        /// </summary>
        ///<param name="CheckData"></param>
        ///<param name="ExamDetailId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckPaper(List< CheckPaperParameter> CheckData,long ExamDetailId)
        {
            bool isSuccess= _scoreDetailService.CheckPaper(CheckData, ExamDetailId);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error("提交失败"));
        }
        /// <summary>
        /// 学生查看自己成绩  TODO 需要权限
        /// </summary>
        /// <param name="examDetailId"></param>
        /// <returns></returns>
        [Authorize(Roles ="Student")]
        public IActionResult MyExamScore(int examDetailId)
        {
            var examDetail= _examDetailService.QueryById(examDetailId);
            ViewBag.TotalScore = examDetail.SCORE;
            List < EDU_SCOREDETAIL > scoreDetails = _scoreDetailService.QueryWithQuestionsAndAppeal(examDetailId);
            return View(scoreDetails);
        }
    }
}
