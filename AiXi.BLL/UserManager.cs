using AiXi.DAL;
using AiXi.IBLL;
using AiXi.IDAL;
using AiXiu.Model;
using AiXiu.Common;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXi.BLL
{
    public class UserManager : IUserManager
    {
        static readonly object lockreg = new object(); //定义一个静态资源，锁定注册方法，防止高并发


        public OperResult Register(TBLogins tBLogins)
        {
            #region 加锁注册
            lock (lockreg)
            {
                OperResult operResult = new OperResult();
                //判断账号、手机号、密码都是否为空
                if (string.IsNullOrWhiteSpace(tBLogins.UserName))
                {
                    return OperResult.Failed("用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(tBLogins.MobileNumber))
                {
                    return OperResult.Failed("手机号不能为空");
                }
                if (string.IsNullOrWhiteSpace(tBLogins.Password))
                {
                    return OperResult.Failed("密码不能为空");
                }
                IUserService userService = new UserService();
                if (userService.IsSameMobile(tBLogins.MobileNumber))
                {
                    return OperResult.Failed("手机号不能重复");
                }
                if (userService.IsSameUserName(tBLogins.UserName))
                {
                    return OperResult.Failed("用户名不能重复");
                }
                SHAEncryption sHAEncryption = new SHAEncryption();
                tBLogins.Password = sHAEncryption.SHA1Encrypt(tBLogins.Password);
                if (userService.AddUser(tBLogins))
                {
                    return OperResult.Succeed();
                }
                else
                {
                    return OperResult.Failed("注册失败");
                }

            }

            #endregion

        }



        public OperResult<TBUsers> LoginByUserName(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                return OperResult<TBUsers>.Failed("用户名或密码不能为空");
            }
            IUserService userService = new UserService();
            TBLogins tBLogins = userService.FindByUserName(username);
            if (tBLogins==null) {
                return OperResult<TBUsers>.Failed("用户不存在");
            }
            //验证用户密码
            SHAEncryption sHAEncryption=new SHAEncryption();
            if (!sHAEncryption.SHA1Encrypt(password).Equals(tBLogins.Password)) {
                return OperResult<TBUsers>.Failed("密码错误");
            }
            TBUsers tBUsers = userService.FindByUserId(tBLogins.Id);
            //返回结果
            if (tBUsers!=null) { return OperResult<TBUsers>.Succeed(tBUsers); }
            return OperResult<TBUsers>.Failed("用户信息不存在");
        }

        public OperResult<TBUsers> LoginByMobile(string mobile, string password)
        {
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                return OperResult<TBUsers>.Failed("手机号或密码不能为空");
            }
            IUserService userService = new UserService();
            TBLogins tBLogins = userService.FindByMobile(mobile);
            if (tBLogins == null)
            {
                return OperResult<TBUsers>.Failed("用户不存在");
            }
            //验证用户密码
            SHAEncryption sHAEncryption = new SHAEncryption();
            if (!sHAEncryption.SHA1Encrypt(password).Equals(tBLogins.Password))
            {
                return OperResult<TBUsers>.Failed("密码错误");
            }
            TBUsers tBUsers = userService.FindByUserId(tBLogins.Id);
            //返回结果
            if (tBUsers != null) { return OperResult<TBUsers>.Succeed(tBUsers); }
            return OperResult<TBUsers>.Failed("用户信息不存在");
        }

        public OperResult<TBUsers> EditWithoutAvatar(TBUsers user)
        {
            IUserService userInfoIDAL = new UserService();
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
