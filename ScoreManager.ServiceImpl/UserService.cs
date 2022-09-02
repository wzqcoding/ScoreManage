﻿using Models;
using ScoreManager.ServiceInterface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceImpl
{
    public class UserService : BaseService<EDU_USER>, IUserService
    {
        public UserService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {

        }

        public EDU_USER GetUserByNameAndPass(string userName, string passWord)
        {
            return _sqlSugarClient.Queryable<EDU_USER>()
                .Includes(x=>x.Teacher)
                .Includes(x=>x.Student)
                .Single(c => c.USERNAME == userName && c.PASSWORD == passWord&&c.ISENABLE=="1");
        }

        public bool IsExist(string userName)
        {
            return this.QueryByWhere(c => c.USERNAME == userName).Any();
        }
    }
}
