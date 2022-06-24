
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using System.Linq;
using System.Web;
using AiXiu.Common;

namespace AiXi.WebSite.Ashx
{
    /// <summary>
    /// CaptchaHandler 的摘要说明
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //获取一个随机编码
            CaptchaGenerator captchaGenerator = new CaptchaGenerator(4);
            //CaptchaGenerator对象返回的有一个，数字字符串、图片类型
            Bitmap bitmap = captchaGenerator.Image;
            context.Session.Add("captcha", captchaGenerator.Text);
            //设置响应的类型为image的jpeg格式
            context.Response.ContentType = "image/jpeg";
            //Bitmap类型图片保存到响应输出流中，文件类型为ImageFormat.Jpeg
            bitmap.Save(context.Response.OutputStream,ImageFormat.Jpeg);
            //响应到客户端后，停止该页面执行
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}