using AiXi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace AiXi.WebSite.Identification
{
    /// <summary>
    /// Cookie身份验证类
    /// </summary>
    public class CookieIdentity
    {
        /// <summary>
        /// Cookie保存身份标识
        /// </summary>
        /// <param name="user">用户信息表</param>
        public static void SaveUser(TBUsers user)
        {
            if (user == null) { return; }
            //序列化用户资料
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var u = new
            {
                Id = user.Id,
                NickName = user.NickName,
                Sex = user.Sex,
                Birthday = user.Birthday,
                Hobby = user.Hobby,
                CreationTime = user.CreationTime
            };
            string profileString = jss.Serialize(u);
            DateTime timenow = DateTime.Now;
            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString(),
                timenow,
                timenow.AddMonths(1),
                false,
                profileString,
                FormsAuthentication.FormsCookiePath);
            string ticketString = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            //保存身份票据到cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString)
            {
                Expires = timenow.AddMonths(1)
            };
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 从Cookie中获取身份标识
        /// </summary>
        /// <returns></returns>
        public static TBUsers ReadUser()
        {
            //读取加密票据
            string ticketString = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName]?.Value;
            if (string.IsNullOrWhiteSpace(ticketString))
            {
                return default;
            }
            //解密票据
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketString);
            string UserData = ticket.UserData;
            //反序列化用户资料
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<TBUsers>(UserData);
        }
    }
}