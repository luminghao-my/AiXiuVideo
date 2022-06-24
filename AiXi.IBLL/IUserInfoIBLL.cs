using AiXi.Model;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.IBLL
{
    public interface IUserInfoIBLL
    {
        /// <summary>
        /// 更新除头像外的其他个人资料
        /// </summary>
        /// <param name="user">用户更改后TBUsers的信息</param>
        /// <returns></returns>
        OperResult<TBUsers> EditWithoutAvatar(TBUsers user);
    }
}
