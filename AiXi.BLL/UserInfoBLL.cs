using AiXi.DAL;
using AiXi.IBLL;
using AiXi.IDAL;
using AiXi.Model;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.BLL
{
    public class UserInfoBLL : IUserInfoIBLL
    {
        public OperResult<TBUsers> EditWithoutAvatar(TBUsers user)
        {
            IUserInfoIDAL  userInfoIDAL = new UserInfoDAL();
            try
            {
                TBUsers profileEntity = userInfoIDAL.EditWithoutAvatar(user);
                return OperResult<TBUsers>.Succeed(profileEntity);
            }
            catch (Exception ex)
            {
                return OperResult<TBUsers>.Failed(ex.Message);
            }
        }
    }
}
