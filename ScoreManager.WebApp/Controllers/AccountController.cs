using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ScoreManager.Common;
using ScoreManager.Model.Enum;
using ScoreManager.Model.ViewParameters;
using ScoreManager.ServiceInterface;
using ScoreManager.WebApp.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ScoreManager.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly CommonHelper _commonHelper;
        private readonly ITeacherService _teacherService;

        public AccountController( ILogger<AccountController> logger, IUserService userService, CommonHelper commonHelper, ITeacherService teacherService)
        {
            _logger = logger;
            _userService = userService;
            _commonHelper = commonHelper;
            _teacherService = teacherService;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string userName, string passWord, string returl)
        {
            var user= _userService.GetUserByNameAndPass(userName, passWord);
            if (user == null) return Json(ApiResult.Error("用户名或密码错误"));
            //获取当前用户的类型 同时获取教师id 学生id
            List<Claim> claims = new List<Claim>()
            {
                new Claim(type:ClaimTypes.Name,value:user.USERNAME),
                new Claim("PassWord",user.PASSWORD),
                new Claim("UserType",user.TYPE.ToString())
            };
            if (user.TYPE==0)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            if (user.TYPE==1)
            {
                claims.Add(new Claim("TeacherId", user.Teacher.ID.ToString()));
                claims.Add(new Claim("TeacherName", user.Teacher.NAME));
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                //查询老师的所有角色
                var teacher=  _teacherService.GetTeacherListWithRole()?.First(c => c.ID == user.Teacher.ID);
                foreach (var item in teacher.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.NAME));
                }

            }
            if (user.TYPE == 2)
            {
                claims.Add(new Claim("StudentId", user.Student.ID.ToString()));
                claims.Add(new Claim("StudentName", user.Student.NAME));
                claims.Add(new Claim(ClaimTypes.Role, "Student"));

            }
            //设置权限
            _commonHelper.SetActionConfigByUser(user);
            
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "customer"));
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            }).Wait();
            return Json(ApiResult.OK());
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Json(ApiResult.OK());
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Regist()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Regist(RegisterParameter parameters)
        {
            ApiResult apiResult = new ApiResult() { Code=0};
            try
            {
                //先查
               bool isExist=  _userService.IsExist(parameters.UserName);
                if (isExist)
                {
                    apiResult.Code = -1;
                    apiResult.Message = "用户名已被使用";
                    return Json(apiResult);
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

                    //2. 根据用户类型 增加学生或者老师
                    if (parameters.UserType == (short)UserType.Teacher)
                    {
                        EDU_TEACHER teacher = new EDU_TEACHER();
                        teacher.USERID = userId;
                        teacher.NAME = parameters.RealName;
                        teacher.PHONE_NUMBER = parameters.Phone;
                        teacher.EMAIL_ADDRESS = parameters.Email;
                        c.Insertable(teacher).ExecuteCommand();
                    }
                    if (parameters.UserType == (short)UserType.Student)
                    {
                        EDU_STUDENT student = new EDU_STUDENT();
                        student.USERID = userId;
                        student.NAME = parameters.RealName;
                        student.STUDYCODE = parameters.StudyCode;
                        c.Insertable(student).ExecuteCommand();
                    }
                });
            }
            catch (Exception ex)
            {
                apiResult.Message = "创建用户失败";
                apiResult.Code = -1;
            }
           
            

            return Json(apiResult);
        }
    }
}
