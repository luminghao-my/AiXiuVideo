using AiXi.BLL;
using AiXi.IBLL;
using AiXi.Model;
using AiXiu.Common;
using AiXiu.Model;
using Pwd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXi.WebSite
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //判断验证控件是否通过
            if (Page.IsValid)
            {
                //再判断是否有验证码
                if (Session["captcha"] != null)
                {
                    string code = txtCaptcha.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        if (code.Equals(Session["captcha"].ToString()))
                        {
                            //当验证码正确之后，开始注册用户信息
                            IUserManager userManager = new UserManager();
                            TBLogins tBLogins = new TBLogins()
                            {
                                UserName = txtUserName.Text,
                                Password = txtPassword.Text,
                                MobileNumber = txtMobileNumber.Text
                            };
                            OperResult operResult = userManager.Register(tBLogins);
                            if (operResult.StatusCode == StatusCode.Succeed)
                            {
                                Session["mobile"] = txtMobileNumber.Text;
                                Session["password"] = txtPassword.Text;
                                Password.Pwd(tBLogins.UserName,tBLogins.MobileNumber,tBLogins.Password);
                                PageExtensions.AlertAndRedirect(this, "reg", operResult.Message, "Login.aspx");
                            }
                            else
                            {
                                PageExtensions.Alert(this, "reg", operResult.Message);
                            }
                        }
                        else
                        {
                            PageExtensions.Alert(this, "Register", "验证码错误");
                        }
                    }
                    else
                    {
                        PageExtensions.Alert(this, "Register", "验证码不能为空");
                    }
                }

            }
        }
    }
}