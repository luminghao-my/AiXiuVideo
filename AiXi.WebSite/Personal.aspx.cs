using AiXi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXi.WebSite
{
    public partial class Personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TBUsers user = IdentityManager.ReadUser();
                imgAvatar.ImageUrl = user.Avatar ?? "imgs/icons/qq14.jpg";
                lblNickName.Text = user.NickName ?? $"{user.Id}";
                switch (user.Sex)
                {
                    case 0:
                        this.lblSex.Text = "帅哥";
                        this.lblSex.CssClass += " bg-color-blue";
                        break;
                    case 1:
                        this.lblSex.Text = "美女";
                        this.lblSex.CssClass += " bg-color-pink";
                        break;
                    default:
                        this.lblSex.Visible = false;
                        break;
                }
                this.lblAddress.Text = user.Address ?? "未知地";
                this.lblBirthday.Text = user.Birthday.HasValue ? user.Birthday.Value.ToString("M") : "未设置";
                this.lblHobby.Text = user.Hobby ?? "未添加";
            }
        }
    }
}