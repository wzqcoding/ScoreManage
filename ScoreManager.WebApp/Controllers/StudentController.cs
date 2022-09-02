using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ScoreManager.Model.ViewParameters;
using ScoreManager.ServiceInterface;
using ScoreManager.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreManager.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IClassService _classService;
        public StudentController(IStudentService studentService, IUserService userService, IClassService classService)
        {
            _studentService = studentService;
            _userService = userService;
            _classService = classService;
        }

        public IActionResult StudentList()
        {
            return View();
        }
        /// <summary>
        /// 查询分页信息
        /// </summary>
        /// <param name="KeyWords">关键词</param>
        /// <returns></returns>
        public IActionResult StudentListPageInfo(TeacherListParameter parameter)
        {
            int totalCount = _studentService.CountByKeyWords(parameter);
            return Json(new { TotalCount = totalCount, PageSize = parameter.PageSize });
        }

        /// <summary>
        /// 查询列表详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult StudentListDetail(TeacherListParameter parameter)
        {
            //查询教师列表数据
            int totalCount = 0;
            var list = _studentService.QueryStudentWithClassInfo(parameter, ref totalCount)?.OrderBy(x=>x.ID)?.ToList();
            ViewData["TotalCount"] = totalCount;
            ViewData["PageSize"] = parameter.PageSize;
            ViewData["PageIndex"] = parameter.PageIndex;
            return PartialView("StudentListDetail", list);
        }

        /// <summary>
        /// 增加学生
        /// </summary>
        /// <returns></returns>
        public IActionResult AddStudent()
        {
            // 查询可用班级
            List<EDU_CLASS> enableClass= _classService.Query().Where(c => c.ISENABLE == "1")?.ToList();
            ViewBag.EnableClass = enableClass;
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentParameter parameters)
        {
            ApiResult apiResult = new ApiResult() { Code = 0 };
            try
            {
                //先查
                bool isExist = _userService.IsExist(parameters.UserName);
                if (isExist)
                {
                    apiResult.Code = -1;
                    apiResult.Message = "用户名已被使用";
                    return Json(apiResult);
                }
                if (!string.IsNullOrEmpty( parameters.StudyCode))
                {
                    isExist=  _studentService.IsExist(parameters.StudyCode);
                    if (isExist)
                    {
                        apiResult.Code = -1;
                        apiResult.Message = "学号已被使用";
                        return Json(apiResult);
                    }
                }
                _userService.TransactionOperation(c =>
                {

                    //1. 先增加用户
                    EDU_USER user = new EDU_USER()
                    {
                        USERNAME = parameters.UserName,
                        PASSWORD = parameters.Pass,
                        TYPE = parameters.UserType
                    };
                    int userId = c.Insertable<EDU_USER>(user).ExecuteReturnIdentity();

                    //2. 增加学生

                    EDU_STUDENT student = new EDU_STUDENT();
                    student.USERID = userId;
                    student.NAME = parameters.RealName;
                    student.CLASSID = parameters.ClassId;
                    student.STUDYCODE = parameters.StudyCode;
                    c.Insertable(student).ExecuteCommand();
                });
            }
            catch (Exception ex)
            {
                apiResult.Message = "创建学生失败";
                apiResult.Code = -1;
            }
            return Json(apiResult);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditStudent(int id)
        {
            EDU_STUDENT student= _studentService.GetFullInfoById(id);
            List<EDU_CLASS> enableClass = _classService.Query().Where(c => c.ISENABLE == "1")?.ToList();
            ViewBag.EnableClass = enableClass;
            return View(student);
        }
        [HttpPost]
        public IActionResult EditStudent(UpdateStudentParameter parameter)
        {
            ApiResult result = new ApiResult();
            EDU_STUDENT student = _studentService.GetFullInfoById(parameter.Id);
            student.NAME = parameter.RealName;
            student.CLASSID = parameter.ClassId;
            student.User.PASSWORD = parameter.Pass;
            student.STUDYCODE = parameter.StudyCode;
            string msg;
            bool isSuccess = _studentService.UpdateFullInfo(student, out msg);
            if (isSuccess)
            {
                result.Code = 0;
            }
            else
            {
                result.Code = -1;
                result.Message = msg;
            }

            return Json(result);
        }
        /// <summary>
        /// 软删除，更新学生的班级id 状态 以及用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteFullInfo(int id)
        {
            string msg;
            ApiResult result = new ApiResult();
            bool isSuccess = _studentService.DeleteFullInfo(id, out msg);
            if (isSuccess)
            {
                result.Code = 0;
            }
            else
            {
                result.Code = -1;
                result.Message = msg;
            }

            return Json(result);
        }

    }
}
