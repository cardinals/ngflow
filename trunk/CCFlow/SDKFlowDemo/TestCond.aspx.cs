﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCFlow.SDKFlowDemo
{
    public partial class TestCond : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region  Obtain the necessary variables .
            string flowNo = this.Request.QueryString["FK_Flow"];
            string nodeID = this.Request.QueryString["FK_Node"];
            string workid = this.Request.QueryString["WorkID"];
            string fid = this.Request.QueryString["FID"];
            string userNo = this.Request.QueryString["UserNo"];
            string sid = this.Request.QueryString["SID"];
            #endregion  Obtain the necessary variables .

            string CurrNode = this.Request.QueryString["CurrNode"].Trim();
            string toNodeID = this.Request.QueryString["ToNodeID"].Trim();
            if (CurrNode == toNodeID)
                this.Response.Write("1");
            else
                this.Response.Write("0");
        }
    }
}