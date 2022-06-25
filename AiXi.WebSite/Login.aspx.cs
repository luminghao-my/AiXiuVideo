using AiXi.BLL;
using AiXi.IBLL;
using AiXiu.Common;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXi.WebSite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) {
                //根据用户名和密码登录
                IUserManager userManager = new UserManager();
                OperResult<TBUsers> operResult = userManager.LoginByUserName(txtUserName.Text,txtPassword.Text);
                if (operResult.StatusCode == StatusCode.Succeed)
                {
                    //保存身份票据
                    IdentityManager.SaveUser(operResult.Result);
                    PageExtensions.AlertAndRedirect(this,"login",operResult.Message, "Personal.aspx");
                }
                else {
                    PageExtensions.Alert(this, "login", operResult.Message);
                }
            }
        }
    }
}