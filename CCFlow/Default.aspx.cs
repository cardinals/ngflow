﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.WF;
using BP.Web;

namespace CCFlow
{
    public partial class Default : WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MapData md = new MapData();
            //Node nd = new Node(8801);
            //Work wk = nd.HisWork;
            //wk.ResetDefaultVal();
            //wk.OID = BP.DA.DBAccess.GenerOID();
            //wk.DirectInsert();
            //wk.Retrieve();
            //string val = wk.GetValStrByKey("ZhengShu");
            ////string val1 = wk.GetValStrByKey("JinELeiXing");
            ////string val2 = wk.GetValStrByKey("FuDianLeiXing");
            //this.Response.Write(val);
            //wk.Update();

            //wk.SetValByKey("ZhengShu", 0);
            //wk.Update();
            //return;

            //FlowSort fs = new FlowSort();
            //fs.No = "1";
            //fs.ParentNo = "0";
            //fs.Name = " Root directory ";
            //fs.DirectInsert();

            //BP.Port.Dept d2 = new BP.Port.Dept();
            //d2.CheckPhysicsTable();

            //BP.GPM.Dept dept = new BP.GPM.Dept();
            //dept.CheckPhysicsTable();
            //return;

            //Flow fl = new Flow();
            //fl.DoNewFlow("01", "323232", DataStoreModel.ByCCFlow, "tttt4", "ttttt6");
            //return;

            if (this.Request.RawUrl.ToLower().Contains("wap"))
            {
                this.Response.Redirect("./WF/WAP/", true);
                return;
            }

            BP.DA.DataType.ParseExpToDecimal("232*232+(23+323)");

            if (this.Request.QueryString["IsCheckUpdate"] == "1")
                this.Response.Redirect("./WF/Admin/XAP/Designer.aspx?IsCheckUpdate=1", true);
            else
                this.Response.Redirect("./WF/Admin/XAP/Designer.aspx", true);
            return;
        }
    }
}