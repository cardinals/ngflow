﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

namespace CCFlow.AppDemoLigerUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["DoType"] == "Logout")
                BP.Web.WebUser.Exit();

            if (this.Request.Browser.Cookies == false)
            {
                this.Response.Write(" Browser does not support cookies.");
                return;
            }

            string strNo = "";
            string strPs = "";
            string isRemember = "";
            if (IsPostBack == false)
            {
                if (this.Request.Browser.Cookies == true) // Get cookie
                {
                    if (Request.Cookies["CCS"] != null)
                    {
                        strNo = Convert.ToString(Request.Cookies["CCS"].Values["No"]);
                        if (strNo != "")
                        {
                            if (strNo == "Guest")
                                return;

                            strPs = Convert.ToString(Request.Cookies["CCS"].Values["Pass"]);
                            isRemember = Request.Cookies["CCS"].Values["IsRememberMe"].ToString();
                            // Get cookie The user name and password , And judges whether consistent 
                            Emp em = new Emp(strNo);
                            if (em.CheckPass(strPs))
                            {
                                WebUser.SignInOfGenerLang(em, WebUser.SysLang, isRemember == "0" ? false : true);
                                if (isRemember == "1")
                                {

                                    if (this.Request.RawUrl.ToLower().Contains("wap"))
                                        WebUser.IsWap = true;
                                    else
                                        WebUser.IsWap = false;

                                    WebUser.Token = this.Session.SessionID;

                                    Response.Redirect("Default.aspx", false);
                                    return;
                                }
                                else
                                {
                                    this.txtUserName.Text = strNo;
                                }
                            }
                            else
                            {
                                this.txtUserName.Text = strNo;
                            }
                        }

                    }
                }
            }

            this.Page.RegisterStartupScript("event_handler", "<script>document.body.onkeypress = keyPressed;</script>");
            this.Page.RegisterClientScriptBlock("default_button", "<script> function keyPressed() { if(window.event.keyCode == 13) { document.getElementById(\""
                + this.lbtnSubmit.ClientID + "\").click(); } } </script>");

            //if (WebUser.No != null)
            //    txtUserName.Text = WebUser.No;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text.Trim();
            string pass = txtPassword.Text.Trim();
            try
            {
                if (WebUser.No != null)
                    WebUser.Exit();

                BP.Port.Emp em = new BP.Port.Emp(user);
                if (em.CheckPass(pass))
                {
                    bool bl = this.IsRemember.Checked;

                    WebUser.SignInOfGenerLang(em, WebUser.SysLang, bl);

                    if (this.Request.RawUrl.ToLower().Contains("wap"))
                        WebUser.IsWap = true;
                    else
                        WebUser.IsWap = false;

                    WebUser.Token = this.Session.SessionID;

                    string s = "";
                    s = BP.Web.WebUser.No;
                    if (string.IsNullOrEmpty(s))
                        s = BP.Web.WebUser.NoOfRel;
                    if (string.IsNullOrEmpty(s))
                        throw new Exception("@ Number is not written :" + s);

                    this.Response.Redirect("Default.aspx?ss=" + s+"&DDD="+em.No, false);
                    return;
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "kesy", "<script language=JavaScript>alert(' Username Password error , Note that the password is case sensitive , Check to see if pressed CapsLock..');</script>");
                }
            }
            catch (System.Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "kesy", "<script language=JavaScript>alert('@ Username Password error !@ Check if pressed CapsLock.@ More information :" + ex.Message + "');</script>");
            }
        }
    }
}