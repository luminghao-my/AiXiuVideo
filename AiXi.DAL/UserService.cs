using AiXi.IDAL;
using AiXi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.DAL
{
    public class UserService : IUserService
    {
        public bool AddUser(AiXi.Model.TBLogins tBLogins)
        {
            AiXiDBContext db = new AiXiDBContext();
            db.TBLogins.Add(tBLogins);
            //如果注册成功，要在用户表里面添加登录用户
            if (db.SaveChanges() > 0)
            {
                TBUsers users = new TBUsers()
                {
                    Id = tBLogins.Id,
                    NickName=tBLogins.UserName,
                    CreationTime=DateTime.Now,
                    Sex=0
                };
                db.TBUsers.Add(users);
                return db.SaveChanges() > 0;
            }
            else {
                return false;
            }
        }

        public bool IsSameMobile(string mobile)
        {
            AiXiDBContext db = new AiXiDBContext();
            return db.TBLogins.Any(e => e.MobileNumber == mobile);
        }

        public bool IsSameUserName(string Name)
        {
            AiXiDBContext db = new AiXiDBContext();
            return db.TBLogins.Any(e => e.MobileNumber == Name);
        }

        public TBLogins FindByUserName(string username)
        {
            AiXiDBContext db = new AiXiDBContext();
            return db.TBLogins.FirstOrDefault(e=>e.UserName==username);
        }

        public TBUsers FindByUserId(int Id)
        {
            AiXiDBContext db = new AiXiDBContext();
            return db.TBUsers.SingleOrDefault(e=>e.Id==Id);
        }

        public TBLogins FindByMobile(string mobile)
        {
            AiXiDBContext db = new AiXiDBContext();
            return db.TBLogins.SingleOrDefault(e => e.MobileNumber == mobile);
        }
    }
}
