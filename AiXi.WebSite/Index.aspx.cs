using AiXiu.Model;
using AiXi.WebSite.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXi.WebSite
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                TBUsers tBUsers = CookieIdentity.ReadUser();
                if (tBUsers!=null) {
                    TBUsers users = IdentityManager.ReadUser();
                    string s = users.Id +"</br>"+ users.NickName + "</br>" + users.Avatar + "</br>" + users.Sex;
                    Response.Write(s);
                }
            }
        }
    }
}