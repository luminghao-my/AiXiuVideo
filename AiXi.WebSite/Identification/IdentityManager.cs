using AiXiu.Model;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace AiXi.WebSite
{
    /// <summary>
    /// 身份标识业务管理类
    /// </summary>
    public class IdentityManager
    {
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUser(TBUsers user)
        {
            if (user == null)
            {
                return;
            }
            // 序列化用户资料
            string profileString = JsonConvert.SerializeObject(user);
            //string profileString = JsonConvert.SerializeObject(user, new JsonSerializerSettings()
            // 生成身份票据
          DateTime timeNow = DateTime.Now;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString(),
                timeNow,
                timeNow.AddMonths(1),
                false,
                profileString,
                FormsAuthentication.FormsCookiePath);
            string ticketString = FormsAuthentication.Encrypt(ticket);
            // 保存身份票据到Cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            cookie.Expires = timeNow.AddMonths(1);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <returns></returns>
        public static TBUsers ReadUser()
        {
            // 读取身份
            string ticketString = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName]?.Value;
            if (string.IsNullOrWhiteSpace(ticketString))
            {
                return default;
            }
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketString);
            string userData = ticket.UserData;
            // 反序列化用户资料
            TBUsers user = JsonConvert.DeserializeObject<TBUsers>(userData);
            return user;
        }
    }
}