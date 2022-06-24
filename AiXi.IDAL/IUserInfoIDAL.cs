using AiXi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.IDAL
{
    public interface IUserInfoIDAL
    {
        /// <summary>
        /// 更新用户除头像外的User信息
        /// </summary>
        /// <param name="users">TBUsers用户对象</param>
        /// <returns></returns>
        TBUsers EditWithoutAvatar(TBUsers users);
    }
}
