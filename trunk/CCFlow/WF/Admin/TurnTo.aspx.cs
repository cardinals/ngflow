﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF.Template;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

namespace CCFlow.WF.Admin
{
    public partial class WF_Admin_TurnTo : BP.Web.WebPage
    {
        #region  Property 
        public DDL DDL_Attr
        {
            get
            {
                return this.Pub1.GetDDLByID("DDL_Attr");
            }
        }
        public string FK_Attr
        {
            get
            {
                return this.Request.QueryString["FK_Attr"];
            }
        }
        public DDL DDL_Node
        {
            get
            {
                return this.Pub1.GetDDLByID("DDL_Node");
            }
        }
        public string FK_Node
        {
            get
            {
                return this.Request.QueryString["FK_Node"];
            }
        }
        public int FK_NodeInt
        {
            get
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        #endregion  Property 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.FK_Node == null)
            {
                this.BindFlow();
            }
            else
            {
                this.BindNode();
            }
        }
        /// <summary>
        ///  Binding node 
        /// </summary>
        public void BindNode()
        {
            if (this.DoType == "Del")
            {
                TurnTo condDel = new TurnTo();
                condDel.MyPK = this.MyPK;
                condDel.Delete();
                this.Response.Redirect("TurnTo.aspx?FK_Node=" + this.FK_Node, true);
                return;
            }

            BP.WF.Node nd = new BP.WF.Node(this.FK_NodeInt);
            TurnTos conds = new TurnTos();
            conds.Retrieve(TurnToAttr.FK_Node, this.FK_Node);

            TurnTo cond = new TurnTo();
            if (this.MyPK != null)
            {
                cond.MyPK = this.MyPK;
                cond.RetrieveFromDBSources();
                if (this.FK_Attr != null)
                    cond.FK_Attr = this.FK_Attr;
            }
            if (this.FK_Attr != null)
                cond.FK_Attr = this.FK_Attr;

            this.Title = " After completion of the steering condition node ";
            this.Pub1.AddTable("align=center");
            this.Pub1.AddCaptionLeft(" After completion of the steering condition node " + nd.Name);
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle(" Project ");
            this.Pub1.AddTDTitle(" Collection ");
            this.Pub1.AddTDTitle(" Description ");
            this.Pub1.AddTREnd();

            //  Property / Field 
            MapAttrs attrs = new MapAttrs("ND" + this.FK_Node);
            MapAttrs attrNs = new MapAttrs();
            foreach (MapAttr attr in attrs)
            {
                if (attr.IsBigDoc)
                    continue;

                switch (attr.KeyOfEn)
                {
                    case "Title":
                    case "FK_Emp":
                    case "MyNum":
                    case "FK_NY":
                    case WorkAttr.Emps:
                    case WorkAttr.OID:
                    case StartWorkAttr.Rec:
                    case StartWorkAttr.FID:
                        continue;
                    default:
                        break;
                }
                attrNs.AddEntity(attr);
            }

            DDL ddl = new DDL();
            ddl.ID = "DDL_Attr";
            ddl.BindEntities(attrNs, MapAttrAttr.MyPK, MapAttrAttr.Name);
            ddl.AutoPostBack = true;
            ddl.SelectedIndexChanged += new EventHandler(ddl_Node_SelectedIndexChanged);
            ddl.SetSelectItem(cond.FK_Attr);
            if (attrNs.Count == 0)
            {
                BP.WF.Node tempND = new BP.WF.Node(cond.FK_Node);
                nd.CreateMap();
                this.Pub1.AddTR();
                this.Pub1.AddTD("");
                this.Pub1.AddTD("colspan=2", " Node not found the right conditions ");
                this.Pub1.AddTREnd();
                return;
            }
            this.Pub1.AddTR();
            this.Pub1.AddTD(  " Property / Field ");
            this.Pub1.AddTD(ddl);
            this.Pub1.AddTD(" Please select the node form field .");
            this.Pub1.AddTREnd();

            MapAttr attrS = new MapAttr(this.DDL_Attr.SelectedItemStringVal);
            this.Pub1.AddTR();
            this.Pub1.AddTD(" Operator ");
            ddl = new DDL();
            ddl.ID = "DDL_Oper";
            switch (attrS.LGType)
            {
                case BP.En.FieldTypeS.Enum:
                case BP.En.FieldTypeS.FK:
                    ddl.Items.Add(new ListItem("=", "="));
                    break;
                case BP.En.FieldTypeS.Normal:
                    switch (attrS.MyDataType)
                    {
                        case BP.DA.DataType.AppString:
                        case BP.DA.DataType.AppDate:
                        case BP.DA.DataType.AppDateTime:
                            ddl.Items.Add(new ListItem("=", "="));
                            ddl.Items.Add(new ListItem("LIKE", "LIKE"));
                            break;
                        case BP.DA.DataType.AppBoolean:
                            ddl.Items.Add(new ListItem("=", "="));
                            break;
                        default:
                            ddl.Items.Add(new ListItem("=", "="));
                            ddl.Items.Add(new ListItem(">", ">"));
                            ddl.Items.Add(new ListItem(">=", ">="));
                            ddl.Items.Add(new ListItem("<", "<"));
                            ddl.Items.Add(new ListItem("<=", "<="));
                            break;
                    }
                    break;
                default:
                    break;
            }
            ddl.SetSelectItem(cond.FK_Operator.ToString());
            this.Pub1.AddTD(ddl);
            this.Pub1.AddTD(" Operation Symbol ");
            this.Pub1.AddTREnd();
            switch (attrS.LGType)
            {
                case BP.En.FieldTypeS.Enum:
                    this.Pub1.AddTR();
                    this.Pub1.AddTD("Value");
                    ddl = new DDL();
                    ddl.ID = "DDL_Val";
                    ddl.BindSysEnum(attrS.KeyOfEn);
                    if (cond != null)
                    {
                        try
                        {
                            ddl.SetSelectItem(cond.OperatorValueInt);
                        }
                        catch
                        {
                        }
                    }
                    this.Pub1.AddTD(ddl);
                    this.Pub1.AddTD("");
                    this.Pub1.AddTREnd();
                    break;
                case BP.En.FieldTypeS.FK:
                    this.Pub1.AddTR();
                    this.Pub1.AddTD("Value");
                    ddl = new DDL();
                    ddl.ID = "DDL_Val";
                    ddl.BindEntities(attrS.HisEntitiesNoName);
                    if (cond != null)
                    {
                        try
                        {
                            ddl.SetSelectItem(cond.OperatorValueStr);
                        }
                        catch
                        {
                        }
                    }
                    this.Pub1.AddTD(ddl);
                    this.Pub1.AddTD("");
                    this.Pub1.AddTREnd();
                    break;
                default:
                    if (attrS.MyDataType == BP.DA.DataType.AppBoolean)
                    {
                        this.Pub1.AddTR();
                        this.Pub1.AddTD("Value");
                        ddl = new DDL();
                        ddl.ID = "DDL_Val";
                        ddl.BindAppYesOrNo(0);
                        if (cond != null)
                        {
                            try
                            {
                                ddl.SetSelectItem(cond.OperatorValueInt);
                            }
                            catch
                            {
                            }
                        }
                        this.Pub1.AddTD(ddl);
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                    }
                    else
                    {
                        this.Pub1.AddTR();
                        this.Pub1.AddTD("Value");
                        TB tb = new TB();
                        tb.ID = "TB_Val";
                        if (cond != null)
                            tb.Text = cond.OperatorValueStr;
                        this.Pub1.AddTD(tb);
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                    }
                    break;
            }

            this.Pub1.AddTR();
            this.Pub1.AddTD(" Steering Url");
            TextBox mytb = new TextBox();
            mytb.ID = "TB_TurnToUrl";
            mytb.Text = cond.TurnToURL;
            mytb.Columns = 90;
            this.Pub1.AddTD("colspan=3", mytb);
            this.Pub1.AddTREnd();

            this.Pub1.AddTRSum();
            this.Pub1.Add("<TD class=TD colspan=3 align=center>");
            Button btn = new Button();
            btn.ID = "Btn_Save";
            btn.CssClass = "Btn";
            btn.Text = "Save";
            btn.Click += new EventHandler(btn_Save_Node_Click);
            this.Pub1.Add(btn);

            if (cond.IsExits == true)
            {
                Btn btnN = new Btn();
                btnN.ShowType = BP.Web.Controls.BtnType.Confirm;
                btnN.ID = "Btn_Del";
                btnN.Text = "Delete";
                btnN.Click += new EventHandler(btn_Del_Node_Click);
                this.Pub1.Add(btnN);
            }

            this.Pub1.AddBR();
            this.Pub1.AddBR(" Prompt :After url In addition to the system parameters (FromFlow,FromNode,SID,WebUser.No), You can also add a variable convention .");
            this.Pub1.AddBR("&nbsp;&nbsp;&nbsp; Such as : ../EIP/aaa.aspx?Jiner=@jiner,@jiner For form fields ");
            this.Pub1.AddBR("&nbsp;&nbsp;&nbsp; Steering system after treatment url is: <br>../EIP/aaa.aspx?Jiner=123&UserNo=abc&SID=xxxx&FromFlow=010&FromNode=108.");

            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
            this.Pub1.AddTableEndWithHR();

            if (conds.Count > 0)
            {
                this.Pub1.AddTable();
                this.Pub1.AddCaption(" Direction of the list of conditions ");

                this.Pub1.AddTR();
                this.Pub1.AddTDTitle("IDX");
                this.Pub1.AddTDTitle(" Attribute key ");
                this.Pub1.AddTDTitle(" Name ");
                this.Pub1.AddTDTitle(" Operation Symbol ");
                this.Pub1.AddTDTitle("Value");
                this.Pub1.AddTDTitle(" Value Description ");
                this.Pub1.AddTDTitle("Url");
                this.Pub1.AddTDTitle(" Editor ");
                this.Pub1.AddTDTitle(" Delete ");
                this.Pub1.AddTREnd();

                int idx = 0;
                foreach (TurnTo tt in conds)
                {
                    idx++;
                    this.Pub1.AddTR();
                    this.Pub1.AddTDIdx(idx);
                    this.Pub1.AddTD(tt.AttrKey);
                    this.Pub1.AddTD(tt.AttrT);

                    this.Pub1.AddTD(tt.FK_Operator);
                    this.Pub1.AddTD(tt.OperatorValueStr);
                    this.Pub1.AddTD(tt.OperatorValueT);
                    this.Pub1.AddTDBigDoc(tt.TurnToURL);

                    this.Pub1.AddTDA("TurnTo.aspx?MyPK=" + tt.MyPK + "&FK_Node=" + tt.FK_Node, "<img src='../Img/Btn/Edit.gif' />");
                    this.Pub1.AddTDA("TurnTo.aspx?MyPK=" + tt.MyPK + "&FK_Node=" + tt.FK_Node + "&DoType=Del", "<img src='../Img/Btn/Delete.gif' />");

                    this.Pub1.AddTREnd();
                }
                this.Pub1.AddTableEnd();
            }
        }
        void ddl_Node_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Response.Redirect("TurnTo.aspx?FK_Node=" + this.FK_Node + "&FK_Attr=" + this.DDL_Attr.SelectedItemStringVal, true);
        }
        public DDL DDL_Oper
        {
            get
            {
                return this.Pub1.GetDDLByID("DDL_Oper");
            }
        }
        public string GetOperVal
        {
            get
            {
                if (this.Pub1.IsExit("TB_Val"))
                    return this.Pub1.GetTBByID("TB_Val").Text;
                return this.Pub1.GetDDLByID("DDL_Val").SelectedItemStringVal;
            }
        }
        public string GetOperValText
        {
            get
            {
                if (this.Pub1.IsExit("TB_Val"))
                    return this.Pub1.GetTBByID("TB_Val").Text;
                return this.Pub1.GetDDLByID("DDL_Val").SelectedItem.Text;
            }
        }
        void btn_Del_Node_Click(object sender, EventArgs e)
        {
            TurnTo cond = new TurnTo();
            cond.MyPK = this.FK_Node;
            cond.Delete();
            this.Response.Redirect("TurnTo.aspx?FK_Node=" + this.FK_Node, true);
        }
        void btn_Save_Node_Click(object sender, EventArgs e)
        {
            TurnTo cond = new TurnTo();
            BP.WF.Node nd = new BP.WF.Node(this.FK_NodeInt);
            cond.FK_Flow = nd.FK_Flow;
            cond.FK_Node = this.FK_NodeInt;
            cond.FK_Attr = this.DDL_Attr.SelectedItemStringVal;
            cond.FK_Operator = this.DDL_Oper.SelectedItemStringVal;
            cond.OperatorValue = this.GetOperVal;
            cond.OperatorValueT = this.GetOperValText;
            cond.TurnToURL = this.Pub1.GetTextBoxByID("TB_TurnToURL").Text;
            cond.MyPK = this.FK_Node + "_" + this.FK_Attr + "_" + cond.FK_OperatorExt + "_" + cond.OperatorValue;
            cond.Save();
            this.Response.Redirect("TurnTo.aspx?FK_Node=" + this.FK_Node + "&FK_Attr=" + cond.FK_Attr + "&MyPK=" + cond.MyPK, true);
            return;
        }
        /// <summary>
        ///  Binding Process 
        /// </summary>
        public void BindFlow()
        {

        }
    }
}