using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.Model;
using AiXi.Model;
using AiXi.IBLL;
using AiXi.BLL;
using AiXiu.Common;

namespace AiXi.WebSite
{
    public partial class PersonalEdit : System.Web.UI.Page
    {
        //json反序列化城市
        District district = null;
        ListItem ddlTips = new ListItem("----请选择----","");

        public PersonalEdit() {
            //加载省市信息
            using (FileStream fileStream = new FileStream(Server.MapPath("~/App_Data/district.json"), FileMode.Open,FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {
                    string distictString = reader.ReadToEnd();
                    district = JsonConvert.DeserializeObject<District>(distictString);
                }
            }
               
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*BindProvinces();*/
                TBUsers tBUsers = IdentityManager.ReadUser();
                txtNickName.Text = tBUsers.NickName;
                if (!string.IsNullOrEmpty(tBUsers.Birthday+""))
                {
                    txtBirthday.Text = tBUsers.Birthday.Value.ToString("yyyy-MM-dd");
                }
                ddlSex.SelectedValue = tBUsers.Sex.ToString();
                //处理城市
                if (!string.IsNullOrEmpty(tBUsers.Address))
                {
                    string[] address = tBUsers.Address.Split(' ');
                    /*ddlProvince.SelectedValue = address[0];*/
                    BindProvinces(address[0]);
                    BindCitys(address[0], address[1]);
                }

                //处理爱好
                if (!string.IsNullOrEmpty(tBUsers.Hobby))
                {
                    string[] hobbys = tBUsers.Hobby.Split(' ');
                    /*for (int i = 0; i < hobbys.Length; i++)
                    {
                        foreach (ListItem item in cblHobby.Items)
                        {
                            if (item.Value.Contains(hobbys[i]))
                            {
                                item.Selected = true;
                                continue;
                            }
                        }
                    }*/
                    for (int i = 0; i < cblHobby.Items.Count; i++)
                    {
                        if (hobbys.Contains(cblHobby.Items[i].Value)) {
                            cblHobby.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 省份选项索引变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string provinceName = this.ddlProvince.SelectedValue;
            BindCitys(provinceName);
        }


        

        /// <summary>
        /// 绑定省份下拉列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        private void BindProvinces(string provinceName = null) {
            //绑定省份下拉列表
            this.ddlProvince.DataSource = district.Provinces;
            this.ddlProvince.DataValueField = "ProvinceName";
            this.ddlProvince.DataTextField = "ProvinceName";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0,ddlTips);
            //设置省份默认选项
            if (string.IsNullOrWhiteSpace(provinceName))
            {
                this.ddlProvince.SelectedIndex = 0;
            }
            else {
                this.ddlProvince.SelectedValue = provinceName;
            }
        }


        /// <summary>
        /// 绑定城市下拉列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <param name="cityName">城市名称</param>
        private void BindCitys(string provinceName, string cityName = null) {
            //未选择省份名称时，清空城市列表
            if (string.IsNullOrWhiteSpace(provinceName))
            {
                this.ddlCity.Items.Clear();
                this.ddlCity.Items.Add(ddlTips);
                return;
            }
            //未查找到省份名称时，清空城市列表
            Province province = district.Provinces.SingleOrDefault(m=>m.ProvinceName==provinceName);
            if (province==null) {
                this.ddlCity.Items.Clear();
                this.ddlCity.Items.Add(ddlTips);
                return;
            }

            //绑定省份下的城市列表
            this.ddlCity.DataSource = province.Citys;
            this.ddlCity.DataTextField = "CityName";
            this.ddlCity.DataValueField = "CityName";
            this.ddlCity.DataBind();
            this.ddlCity.Items.Insert(0,ddlTips);
            //设置城市默认选中项
            if (string.IsNullOrWhiteSpace(provinceName))
            {
                this.ddlCity.SelectedIndex = 0;
            }
            else {
            this.ddlCity.SelectedValue = cityName;
            }

        }

        /// <summary>
        /// 实现更新功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProfile_Click(object sender, EventArgs e)
        {
            TBUsers profile = IdentityManager.ReadUser();
            profile.NickName = this.txtNickName.Text;
            profile.Sex = int.Parse(this.ddlSex.SelectedValue);
            if (!string.IsNullOrEmpty(this.txtBirthday.Text)) {
                profile.Birthday = DateTime.Parse(this.txtBirthday.Text);
            }
            profile.Address = $"{this.ddlProvince.SelectedValue} {this.ddlCity.SelectedValue}";
            List<string> hobbyList=new List<string>();
            foreach (ListItem item in this.cblHobby.Items)
            {
                if (item.Selected) {
                    hobbyList.Add(item.Value);
                }
            }
            profile.Hobby = string.Join(" ",hobbyList);
            //更新资料
            IUserInfoIBLL userInfoBLL = new UserInfoBLL();
            OperResult<TBUsers> operResult = userInfoBLL.EditWithoutAvatar(profile);
            if (operResult.StatusCode == StatusCode.Succeed)
            {
                TBUsers userEntity = operResult.Result;
                //读取缓存用户资料
                TBUsers userCookie = IdentityManager.ReadUser();
                if (userCookie == null) { PageExtensions.Alert(this, "cookienull", "缓存读取出错"); }
                //更新用户资料
                userCookie.NickName = userEntity.NickName;
                userCookie.Sex = userEntity.Sex;
                userCookie.Birthday = userEntity.Birthday;
                userCookie.Address = userEntity.Address;
                userCookie.Hobby = userEntity.Hobby;
                //重新缓存用户资料
                IdentityManager.SaveUser(userCookie);
                Response.Redirect("Personal.aspx");
            }
            else {
                PageExtensions.Alert(this,"editerror",operResult.Message);
            }


        }
    }
}