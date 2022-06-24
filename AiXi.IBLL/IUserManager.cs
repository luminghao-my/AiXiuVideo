using AiXi.Model;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.IBLL
{
    public interface IUserManager
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="tBLogins">用户信息</param>
        /// <returns>返回封装类OperResult</returns>
        OperResult Register(TBLogins tBLogins);

        /// <summary>
        /// 通过用户名和密码登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回封装类用户集合 </returns>
        OperResult<TBUsers> LoginByUserName(string username,string password);

        /// <summary>
        /// 通过手机号和密码登录
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <returns>返回封装类用户集合</returns>
        OperResult<TBUsers> LoginByMobile(string mobile, string password);
    }
}
