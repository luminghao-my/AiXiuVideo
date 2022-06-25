using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.IDAL
{
    public interface IUserService
    {


        /// <summary>
        /// 更新用户除头像外的User信息
        /// </summary>
        /// <param name="users">TBUsers用户对象</param>
        /// <returns></returns>
        TBUsers EditWithoutAvatar(TBUsers users);


        /// <summary>
        /// 是否有重复的用户名
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>true存在;false不存在</returns>
        bool IsSameUserName(string Name);
        /// <summary>
        /// 是否有重复的手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns>true:已经存在;false:不存在</returns>
        bool IsSameMobile(string mobile);
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="tBLogins">TBLogins表</param>
        /// <returns>true:成功;false:失败</returns>
        bool AddUser(TBLogins tBLogins);



        /// <summary>
        /// 通过用户名查找用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>true:找到;false:没找到</returns>
        TBLogins FindByUserName(string username);

        /// <summary>
        /// 通过用户编号查找用户信心
        /// </summary>
        /// <param name="Id">用户编号</param>
        /// <returns>返回用户信息表</returns>
        TBUsers FindByUserId(int Id);

        /// <summary>
        /// 通过手机号查找用户
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>返回一个TBLogin表</returns>
        TBLogins FindByMobile(string mobile);

    }
}
