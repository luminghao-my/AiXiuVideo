using AiXi.IDAL;
using AiXi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.DAL
{
    public class UserInfoDAL : IUserInfoIDAL
    {
        public TBUsers EditWithoutAvatar(TBUsers users)
        {
            AiXiDBContext db = new AiXiDBContext();
            TBUsers tBUsers = db.TBUsers.SingleOrDefault(m=>m.Id==users.Id);
            if (tBUsers==null) { 
                return null;
            }
            tBUsers.Address = users.Address;
            tBUsers.NickName = users.NickName;
            tBUsers.Sex = users.Sex;
            tBUsers.Birthday = users.Birthday;
            tBUsers.Hobby = users.Hobby;
            //更新到数据库
            db.Entry(tBUsers).State= System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges()>0) {
                return tBUsers;
            }
            //返回结果
            return null;
        }
    }
}
