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
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        public ClassController(IClassService classService, ITeacherService teacherService, IStudentService studentService)
        {
            _classService = classService;
            _teacherService = teacherService;
            _studentService = studentService;
        }
        public IActionResult ClassList()
        {
            List<EDU_CLASS> data=  _classService.GetAllClassInfo()?.OrderBy(x=>x.ID)?.ToList();
            return View(data);
        }
        /// <summary>
        /// 增加班级
        /// </summary>
        /// <returns></returns>
        public IActionResult AddClass()
        {
            //查询老师带上角色
            List<EDU_TEACHER> teachers= _teacherService.GetTeacherListWithRole();
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpPost]
        public IActionResult AddClass(AddClassParameter parameter)
        {
            string msg;
            bool success = _classService.AddClass(parameter,out msg);
            if (success)
            {
                return Json(ApiResult.OK());
            }
            else
            {
                return Json(ApiResult.Error(msg));
            }
            
        }

        /// <summary>
        /// 启动 或禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EnableOrDisAbledClass(int id, string status)
        {
            ApiResult result = new ApiResult();
            EDU_CLASS cl = _classService.QueryById(id);
            cl.ISENABLE = status;
            _classService.Update(cl);
            return Json(result);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">班级id</param>
        /// <returns></returns>
        public IActionResult EditClass(int id)
        {
            List<EDU_TEACHER> teachers = _teacherService.GetTeacherListWithRole();
            ViewBag.Teachers = teachers;
            EDU_CLASS data= _classService.GetAllClassInfoByClassId(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditClass(UpdateClassParameter parameter)
        {
            string msg;
            bool success= _classService.UpdateTeacher(parameter, out msg);
            if (success)
            {
                return Json(ApiResult.OK());
            }
            else
            {
                return Json(ApiResult.Error(msg));
            }
        }
        /// <summary>
        /// 给班级分配学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult SetStudents(int id)
        {
            //查询当前班级下所有的学生 以及未分配班级的学生
            List<EDU_STUDENT> allEnableStudents = _studentService.Query().Where(c => c.ISDELETE == "1" && (c.CLASSID == 0 || c.CLASSID == id||!c.CLASSID.HasValue))?.ToList();
            //查询所有当前班级的学生
            List<EDU_STUDENT> currentSubjectStudents = allEnableStudents.Where(c => c.CLASSID == id)?.ToList();

            ViewBag.AllEnableStudents = allEnableStudents;
            ViewBag.CurrentSubjectStudents = currentSubjectStudents;
            ViewBag.ClassId = id;
            return View();
        }

        /// <summary>
        /// 批量更新学生的班级
        /// </summary>
        /// <param name="type">更新类型 0 新增  1 去掉</param>
        /// <param name="ids">要变更的学生id</param>
        /// <param name="classId">班级id</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetStudents(string type, List<long> ids, int classId)
        {
            string msg;
            bool isSuccess = _studentService.BatchUpdateClassId(type, ids, classId, out msg);
            if (isSuccess)
            {
                return Json(ApiResult.OK());
            }
            return Json(ApiResult.Error(msg));
        }
    }
}
