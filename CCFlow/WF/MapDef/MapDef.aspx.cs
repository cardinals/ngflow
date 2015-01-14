﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;
using BP;
namespace CCFlow.WF.MapDef
{
    public partial class WF_MapDef_MapDef : BP.Web.WebPage
    {
        public new string MyPK
        {
            get
            {
                string key = this.Request.QueryString["MyPK"];
                if (key == null)
                    key = this.Request.QueryString["PK"];
                if (key == null)
                    key = this.Request.QueryString["FK_MapData"];
                if (key == null)
                    key = "ND1601";
                return key;
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.MyPK;
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        /// <summary>
        /// IsEditMapData
        /// </summary>
        public bool IsEditMapData
        {
            get
            {
                string s = this.Request.QueryString["IsEditMapData"];
                if (s == null || s == "1")
                    return true;
                return false;
            }
        }
        public void BindLeft()
        {
            BP.WF.XML.MapMenus xmls = new BP.WF.XML.MapMenus();
            xmls.RetrieveAll();

            #region bindleft
            this.Left.Add("<a href='http://ccflow.org' target=_blank ><img src='../../DataUser/ICON/" + SystemConfig.CompanyID + "/LogBiger.png' border=0/></a>");
            this.Left.AddHR();
            this.Left.AddUL();
            foreach (BP.WF.XML.MapMenu item in xmls)
            {
                this.Left.AddLi("<a href=\"" + item.JS.Replace("@MyPK", "'" + this.FK_MapData + "'").Replace("@FK_Flow", "'" + this.FK_Flow + "'") + "\" ><img src='" + item.Img + "' width='16px' /><b>" + item.Name + "</b></a><br><font color=green>" + item.Note + "</font>");
            }
            this.Left.AddULEnd();
            #endregion bindleft
        }
        public MapData md = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string fk_node = this.Request.QueryString["FK_Node"];
            md = new MapData(this.FK_MapData);
            MapAttrs mattrs = new MapAttrs(md.No);
            int count = mattrs.Count;
            this.BindLeft();

            #region  Calculated from the width of the columns .
            int labCol = 50;
            int ctrlCol = 300;
            int width = (labCol + ctrlCol)*md.TableCol/2;
            #endregion  Calculated from the width of the columns .

            #region  Generate header .
            this.Pub1.Add("\t\n<Table style='width:" + width + "px;' align=left>");
            this.Pub1.AddTR();
            this.Pub1.AddTD("colspan=" + md.TableCol, "<div style='float:left' ><img src='../../DataUser/ICON/Smaller.png' border=0/></div><h3><div style='float:center' >" + md.Name + "</div></h3>");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            bool isLabel = true;
            for (int i = 0; i < md.TableCol; i++)
            {
                if (isLabel)
                    this.Pub1.AddTD("width='" + labCol + "px' align=center", i);
                else
                    this.Pub1.AddTD("width='" + ctrlCol + "px' align=center", i.ToString());
                isLabel = !isLabel;
            }
            this.Pub1.AddTREnd();
            #endregion  Generate header .

            /*
             *  According to  GroupField  Recurring menu .
             */
            foreach (GroupField gf in gfs)
            {
                rowIdx = 0;
                string gfAttr = ""; 
                currGF = gf; 

                #region  Output grouping bar .
                this.Pub1.AddTR(gfAttr);
                if (gfs.Count == 1)
                    this.Pub1.AddTD("colspan=" + md.TableCol + " class=GroupField valign='top' align:left style='height: 24px;align:left' ", "<div style='text-align:left; float:left'>&nbsp;<a href=\"javascript:GroupField('" + this.FK_MapData + "','" + gf.OID + "')\" >" + gf.Lab + "</a></div><div style='text-align:right; float:right'></div>");
                else
                    this.Pub1.AddTD("colspan=" + md.TableCol + " class=GroupField valign='top'  style='height: 24px;align:left' ", "<div style='text-align:left; float:left'><img src='./Style/Min.gif' alert='Min' id='Img" + gf.Idx + "'  border=0 />&nbsp;<a href=\"javascript:GroupField('" + this.FK_MapData + "','" + gf.OID + "')\" >" + gf.Lab + "</a></div><div style='text-align:right; float:right'> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" ><img src='../Img/Btn/Up.gif' class='Arrow' border=0/></a> <a href=\"javascript:GFDoDown('" + gf.OID + "')\" ><img src='../Img/Btn/Down.gif' class='Arrow' border=0/></a></div>");
                this.Pub1.AddTREnd();
                #endregion  Output grouping bar .

                this.idx = 0; //  Sequence number field is set 0.
                int colSpan = md.TableCol;  //  Definition colspan Width .
                for (int i = 0; i < mattrs.Count; i++)
                {
                    MapAttr attr = mattrs[i] as MapAttr;

                    #region  Filter field does not need to be displayed .
                    if (attr.GroupID == 0)
                    {
                        attr.GroupID = gf.OID;
                        attr.Update();
                    }

                    if (attr.GroupID != gf.OID)
                    {
                        if (gf.Idx == 0 && attr.GroupID == 0)
                        {
                        }
                        else
                            continue;
                    }
                    if (attr.HisAttr.IsRefAttr || attr.UIVisible == false)
                        continue;

                    if (colSpan == 0)
                        this.InsertObjects(true);
                    #endregion  Filter field does not need to be displayed .

                    #region  Add blank columns .
                    if (colSpan <= 0)
                    {
                        /* If the column has been used .*/
                        this.Pub1.AddTREnd();
                        colSpan = md.TableCol; //  Add columns .
                    }
                    #endregion  Add blank columns .

                    #region  Handling large blocks of text in the two states .
                    if (attr.IsBigDoc && (attr.ColSpan == md.TableCol || attr.ColSpan==0 ))
                    {
                        if (colSpan == md.TableCol)
                        {
                            /* Description Just fill column ( In the state has changed the line )*/
                            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "'  " + gfAttr);
                        }
                        else
                        {
                            // Add it to wrap the space .
                            this.Pub1.AddTD("colspan=" + colSpan, "");
                            this.Pub1.AddTREnd();
                            colSpan = md.TableCol;
                        }

                        /* Is a chunk of text , And occupied the entire span of the remaining cell lines . */
                        this.Pub1.Add("<TD colspan=" + md.TableCol + " width='100%' height='" + attr.UIHeight.ToString() + "px' >");
                        this.Pub1.Add("<span style='float:left' height='" + attr.UIHeight.ToString() + "px' >" + this.GenerLab(attr, 0, count) + "</span>");
                        this.Pub1.Add("<span style='float:right' height='" + attr.UIHeight.ToString() + "px'  >");

                        Label lab = new Label();
                        lab.ID = "Lab" + attr.KeyOfEn;
                        lab.Text = " Defaults ";
                        this.Pub1.Add(lab);
                        this.Pub1.Add("</span><br>");

                        TB mytbLine = new TB();
                        mytbLine.ID = "TB_" + attr.KeyOfEn;
                        mytbLine.TextMode = TextBoxMode.MultiLine;
                        mytbLine.Attributes["style"] = "width:100%;height:100%;padding: 0px;margin: 0px;";
                        mytbLine.Enabled = attr.UIIsEnable;
                        this.Pub1.Add(mytbLine);
                        mytbLine.Attributes["width"] = "100%";
                        lab = this.Pub1.GetLabelByID("Lab" + attr.KeyOfEn);
                        string ctlID = mytbLine.ClientID;
                        lab.Text = "<a href=\"javascript:TBHelp('" + ctlID + "','" + this.Request.ApplicationPath + "','" + md.No + "','" + attr.KeyOfEn + "')\"> Defaults </a>";
                        this.Pub1.AddTDEnd();
                        this.Pub1.AddTREnd();
                        continue;
                    }

                    if (attr.IsBigDoc)
                    {
                        /* If it is large text ,  And it does not show the entire column .*/
                        if (colSpan == md.TableCol)
                        {
                            /* Already filled up */
                            this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "' " + gfAttr);
                            colSpan = colSpan - attr.ColSpan; //  Minus the already occupied col.
                        }

                        this.Pub1.Add("<TD  colspan=" + attr.ColSpan + " width='50%' height='" + attr.UIHeight.ToString() + "px' >");
                        this.Pub1.Add("<span height='" + attr.UIHeight.ToString() + "px' style='float:left'>" + this.GenerLab(attr, 0, count) + "</span>");
                        this.Pub1.Add("<span height='" + attr.UIHeight.ToString() + "px' style='float:right'>");
                        Label lab = new Label();
                        lab.ID = "Lab" + attr.KeyOfEn;
                        lab.Text = " Defaults ";
                        this.Pub1.Add(lab);
                        this.Pub1.Add("</span>");

                        TB mytbLine = new TB();
                        mytbLine.TextMode = TextBoxMode.MultiLine;
                        mytbLine.Attributes["class"] = "TBDoc"; // "width:100%;padding: 0px;margin: 0px;";
                        mytbLine.ID = "TB_" + attr.KeyOfEn;
                        mytbLine.Enabled = attr.UIIsEnable;
                        if (mytbLine.Enabled == false)
                            mytbLine.Attributes["class"] = "TBReadonly";
                        mytbLine.Attributes["style"] = "width:100%;height:100%;padding: 0px;margin: 0px;";
                        this.Pub1.Add(mytbLine);

                        lab = this.Pub1.GetLabelByID("Lab" + attr.KeyOfEn);
                        string ctlID = mytbLine.ClientID;
                        lab.Text = "<a href=\"javascript:TBHelp('" + ctlID + "','" + this.Request.ApplicationPath + "','" + md.No + "','" + attr.KeyOfEn + "')\"> Defaults </a>";
                        this.Pub1.AddTDEnd();
                        this.InsertObjects(false);
                        continue;
                    }
                    #endregion  Handling large blocks of text in the two states .

                    /* 
                     *  The following is a label a way to show the controls .
                     */

                    #region   First determine whether the remaining cells of the current needs of the current control .
                    if (attr.ColSpan + 1 > md.TableCol)
                        attr.ColSpan = md.TableCol - 1; // If you set the 

                    if (colSpan < attr.ColSpan + 1 || colSpan == 1 || colSpan == 0)
                    {
                        /* If the remaining columns can not meet the current cell , To supplement its , Wrap it .*/
                        this.Pub1.AddTD("colspan=" + colSpan, "");
                        this.Pub1.AddTREnd();

                        colSpan = md.TableCol;
                        this.Pub1.AddTR();
                    }
                    #endregion   First determine whether the remaining cells of the current needs of the current control .

                    #region  Increase control and description .
                    //  Increase the description .
                    colSpan = colSpan - 1 - attr.ColSpan; //  First subtract the current placeholder .

                    TB tb = new TB();
                    tb.Attributes["width"] = "100%";
                    tb.ID = "TB_" + attr.KeyOfEn;

                    switch (attr.LGType)
                    {
                        case FieldTypeS.Normal:
                            tb.Enabled = attr.UIIsEnable;
                            switch (attr.MyDataType)
                            {
                                case BP.DA.DataType.AppString:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    tb.ShowType = TBType.TB;
                                    tb.Text = attr.DefVal;

                                    if (attr.IsSigan)
                                        this.Pub1.AddTD("colspan=" + attr.ColSpan, "<img src='/DataUser/Siganture/" + WebUser.No + ".jpg' border=0 onerror=\"this.src='../../DataUser/Siganture/UnName.jpg'\"/>");
                                    else
                                        this.Pub1.AddTD("colspan=" + attr.ColSpan, tb);

                                    break;
                                case BP.DA.DataType.AppDate:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    TB tbD = new TB();
                                    if (attr.UIIsEnable)
                                    {
                                        tbD.Attributes["onfocus"] = "WdatePicker();";
                                        tbD.Attributes["class"] = "TBcalendar";
                                    }
                                    else
                                    {
                                        tbD.Enabled = false;
                                        tbD.ReadOnly = true;
                                        tbD.Attributes["class"] = "TBcalendar";
                                    }
                                    this.Pub1.AddTD("colspan=" + attr.ColSpan, tbD);
                                    break;
                                case BP.DA.DataType.AppDateTime:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    TB tbDT = new TB();
                                    tbDT.Text = attr.DefVal;
                                    if (attr.UIIsEnable)
                                    {
                                        tbDT.Attributes["onfocus"] = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'});";
                                        tbDT.Attributes["class"] = "TBcalendar";
                                    }
                                    else
                                    {
                                        tbDT.Enabled = false;
                                        tbDT.ReadOnly = true;
                                        tbDT.Attributes["class"] = "TBcalendar";
                                    }
                                    this.Pub1.AddTD("colspan=" + attr.ColSpan, tbDT);
                                    break;
                                case BP.DA.DataType.AppBoolean:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    CheckBox cb = new CheckBox();
                                    cb.Text = attr.Name;
                                    cb.Checked = attr.DefValOfBool;
                                    cb.Enabled = attr.UIIsEnable;
                                    cb.ID = "CB_" + attr.KeyOfEn;
                                    this.Pub1.AddTD("  colspan=" + attr.ColSpan, cb);
                                    break;
                                case BP.DA.DataType.AppDouble:
                                case BP.DA.DataType.AppFloat:
                                case BP.DA.DataType.AppInt:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    tb.ShowType = TBType.Num;
                                    tb.Text = attr.DefVal;
                                    if (attr.IsNull)
                                        tb.Text = "";
                                    this.Pub1.AddTD("  colspan=" + attr.ColSpan, tb);
                                    break;
                                case BP.DA.DataType.AppMoney:
                                case BP.DA.DataType.AppRate:
                                    this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                                    tb.ShowType = TBType.Moneny;
                                    tb.Text = attr.DefVal;
                                    if (attr.IsNull)
                                        tb.Text = "";

                                    this.Pub1.AddTD(" colspan=" + attr.ColSpan, tb);
                                    break;
                                default:
                                    break;
                            }

                            tb.Attributes["width"] = "100%";
                            switch (attr.MyDataType)
                            {
                                case BP.DA.DataType.AppString:
                                case BP.DA.DataType.AppDateTime:
                                case BP.DA.DataType.AppDate:
                                    if (tb.Enabled)
                                        tb.Attributes["class"] = "TB";
                                    else
                                        tb.Attributes["class"] = "TBReadonly";
                                    break;
                                default:
                                    if (tb.Enabled)
                                        tb.Attributes["class"] = "TBNum";
                                    else
                                        tb.Attributes["class"] = "TBNumReadonly";
                                    break;
                            }
                            break;
                        case FieldTypeS.Enum:
                            if (attr.UIContralType == UIContralType.DDL)
                            {
                                this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));

                                DDL ddle = new DDL();
                                ddle.ID = "DDL_" + attr.KeyOfEn;
                                ddle.BindSysEnum(attr.UIBindKey);
                                ddle.SetSelectItem(attr.DefVal);
                                ddle.Enabled = attr.UIIsEnable;
                                this.Pub1.AddTD("colspan=" + attr.ColSpan, ddle);
                            }
                            else
                            {
                                this.Pub1.Add("<TD class=TD colspan='" + attr.ColSpan + "'>");
                                this.Pub1.Add( this.GenerLab(attr, i, count));

                                SysEnums ses = new SysEnums(attr.UIBindKey);
                                foreach (SysEnum item in ses)
                                {
                                    RadioButton rb = new RadioButton();
                                    rb.ID = "RB_" + attr.KeyOfEn + "_" + item.IntKey;
                                    rb.Text = item.Lab;
                                    if (item.IntKey.ToString() == attr.DefVal)
                                        rb.Checked = true;
                                    else
                                        rb.Checked = false;
                                    rb.GroupName = item.EnumKey + attr.KeyOfEn;
                                    this.Pub1.Add(rb);
                                }
                                this.Pub1.AddTDEnd();
                            }
                            break;
                        case FieldTypeS.FK:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, i, count));
                            DDL ddl1 = new DDL();
                            ddl1.ID = "DDL_" + attr.KeyOfEn;
                            try
                            {
                                EntitiesNoName ens = attr.HisEntitiesNoName;
                                ens.RetrieveAll();
                                ddl1.BindEntities(ens);
                                ddl1.SetSelectItem(attr.DefVal);
                            }
                            catch
                            {
                            }
                            ddl1.Enabled = attr.UIIsEnable;
                            this.Pub1.AddTD("colspan=" + attr.ColSpan, ddl1);
                            break;
                        default:
                            break;
                    }
                    #endregion  Increase control .

                } // end Cycling group .

                if (colSpan == 0)
                {
                    colSpan = md.TableCol;
                    this.Pub1.AddTREnd();
                    this.InsertObjects(false);
                }

                //  After the packet processing it ,  First determine whether the remaining cells of the current needs of the current control .
                if (colSpan!=md.TableCol )
                {
                    /* If the remaining columns can not meet the current cell , To supplement its , Wrap it .*/
                    this.Pub1.AddTD("colspan=" + colSpan, "");
                    this.Pub1.AddTREnd();
                    this.InsertObjects(false);
                    colSpan = md.TableCol;
                }
            } // end Cycling group .

            #region  Audit component output .
            FrmWorkCheck fwc = new FrmWorkCheck(md.No);
            if (fwc.HisFrmWorkCheckSta != FrmWorkCheckSta.Disable)
            {
                this.Pub1.AddTR();
                this.Pub1.AddTD("colspan=" + md.TableCol + " class=GroupField valign='top' align:left style='height: 24px;align:left' ", "<div style='text-align:left; float:left'>&nbsp; Audit information </div><div style='text-align:right; float:right'></div>");
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.Add("<TD valign=top colspan=" + md.TableCol + " ID='TDFWC' height='150px' > ");
                string src = "../WorkOpt/WorkCheck.aspx?s=2";
                string paras = this.RequestParas;
                if (paras.Contains("OID=") == false)
                    paras += "&OID=123";
                src += "&r=q" + paras;
                this.Pub1.Add("<iframe ID='FWC' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "'  width='100%' height='150px' scrolling=auto  /></iframe>");
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
            #endregion 

            this.Pub1.AddTableEnd();


            #region  Exception handling .
            foreach (MapDtl dtl in dtls)
            {
                if (dtl.IsUse == false)
                {
                    dtl.RowIdx = 0;
                    dtl.GroupID = 0;
                    dtl.Update();
                    //    this.Response.Redirect(this.Request.RawUrl, true);
                }
            }
            #endregion  Exception handling .

            #region  Information processing extensions .
            MapExts mes = new MapExts(this.FK_MapData);
            if (mes.Count != 0)
            {
                string appPath = this.Request.ApplicationPath;

                this.Page.RegisterClientScriptBlock("s",
              "<script language='JavaScript' src='../Scripts/jquery-1.4.1.min.js' ></script>");

                this.Page.RegisterClientScriptBlock("b",
             "<script language='JavaScript' src='../CCForm/MapExt.js' defer='defer' type='text/javascript' ></script>");

                this.Page.RegisterClientScriptBlock("dC",
         "<script language='JavaScript' src='" + appPath + "DataUser/JSLibData/" + this.FK_MapData + ".js' ></script>");

                this.Pub1.Add("<div id='divinfo' style='width: 155px; position: absolute; color: Lime; display: none;cursor: pointer;align:left'></div>");
            }
            string jsStr = "";
            foreach (MapExt me in mes)
            {
                switch (me.ExtType)
                {
                    case MapExtXmlList.DDLFullCtrl: //  Automatic filling .
                        DDL ddlOper = this.Pub1.GetDDLByID("DDL_" + me.AttrOfOper);
                        if (ddlOper == null)
                            continue;
                        ddlOper.Attributes["onchange"] = "DDLFullCtrl(this.value,\'" + ddlOper.ClientID + "\', \'" + me.MyPK + "\')";
                        break;
                    case MapExtXmlList.ActiveDDL:
                        DDL ddlPerant = this.Pub1.GetDDLByID("DDL_" + me.AttrOfOper);
                        DDL ddlChild = this.Pub1.GetDDLByID("DDL_" + me.AttrsOfActive);
                        if (ddlChild == null || ddlPerant == null)
                        {
                            me.Delete();
                            continue;
                        }

                        ddlPerant.Attributes["onchange"] = "DDLAnsc(this.value,\'" + ddlChild.ClientID + "\', \'" + me.MyPK + "\')";
                        // ddlChild.Attributes["onchange"] = "ddlCity_onchange(this.value,'" + me.MyPK + "')";
                        break;
                    case MapExtXmlList.TBFullCtrl: //  Automatic filling .
                        TB tbAuto = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                        if (tbAuto == null)
                        {
                            me.Delete();
                            continue;
                        }
                        tbAuto.Attributes["onkeyup"] = "DoAnscToFillDiv(this,this.value,\'" + tbAuto.ClientID + "\', \'" + me.MyPK + "\');";
                        tbAuto.Attributes["AUTOCOMPLETE"] = "OFF";
                        break;
                    case MapExtXmlList.InputCheck: /*js  Inspection  */
                        TB tbJS = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                        if (tbJS != null)
                            tbJS.Attributes[me.Tag2] += me.Tag1 + "(this);";
                        else
                            me.Delete();
                        break;
                    case MapExtXmlList.PopVal: // Pop-up window .
                        TB  tbPop = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                        //tb.Attributes["ondblclick"] = "ReturnVal(this,'" + me.Doc + "','sd');";
                        break;
                    case MapExtXmlList.AutoFull: // Automatic filling .
                        string  js = "\t\n <script type='text/javascript' >";
                        TB tb = this.Pub1.GetTBByID("TB_" + me.AttrOfOper);
                        if (tb == null)
                            continue;

                        string left = "\n  document.forms[0]." + tb.ClientID + ".value = ";
                        string right = me.Doc;
                        foreach (MapAttr mattr in mattrs)
                        {
                            if (mattr.IsNum == false)
                                continue;

                            if (me.Doc.Contains("@" + mattr.KeyOfEn)
                                || me.Doc.Contains("@" + mattr.Name))
                            {
                            }
                            else
                            {
                                continue;
                            }

                            string tbID = "TB_" + mattr.KeyOfEn;
                            TB mytb = this.Pub1.GetTBByID(tbID);
                            if (mytb == null)
                                continue;

                            this.Pub1.GetTBByID(tbID).Attributes["onkeyup"] = "javascript:Auto" + me.AttrOfOper + "();";
                            right = right.Replace("@" + mattr.Name, " parseFloat( document.forms[0]." + mytb.ClientID + ".value.replace( ',' ,  '' ) ) ");
                            right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + mytb.ClientID + ".value.replace( ',' ,  '' ) ) ");
                        }

                        js += "\t\n function Auto" + me.AttrOfOper + "() { ";
                        js += left + right + ";";
                        js += " \t\n  document.forms[0]." + tb.ClientID + ".value= VirtyMoney(document.forms[0]." + tb.ClientID + ".value ) ;";
                        js += "\t\n } ";
                        js += "\t\n</script>";
                        this.Pub1.Add(js);
                        break;
                    default:
                        break;
                }
            }
            #endregion  Information processing extensions .

            #region  Enter the minimum processing , Maximum verification .
            foreach (MapAttr attr in mattrs)
            {
                if (attr.MyDataType != DataType.AppString || attr.MinLen == 0)
                    continue;

                if (attr.UIIsEnable == false || attr.UIVisible == false)
                    continue;

                this.Pub1.GetTextBoxByID("TB_" + attr.KeyOfEn).Attributes["onblur"] = "checkLength(this,'" + attr.MinLen + "','" + attr.MaxLen + "')";
            }
            #endregion  Enter the minimum processing , Maximum verification .

            #region  Deal with iFrom  Adaptive problem .
            string myjs = "\t\n<script type='text/javascript' >";
            foreach (MapDtl dtl in dtls)
            {
                myjs += "\t\n window.setInterval(\"ReinitIframe('F" + dtl.No + "','TD" + dtl.No + "')\", 200);";
            }

            foreach (MapM2M M2M in dot2dots)
            {
                if (M2M.ShowWay == FrmShowWay.FrmAutoSize)
                    myjs += "\t\n window.setInterval(\"ReinitIframe('F" + M2M.NoOfObj + "','TD" + M2M.NoOfObj + "')\", 200);";
            }
            foreach (FrmAttachment ath in aths)
            {
                if (ath.IsAutoSize)
                    myjs += "\t\n window.setInterval(\"ReinitIframe('F" + ath.MyPK + "','TD" + ath.MyPK + "')\", 200);";
            }
            foreach (MapFrame fr in frams)
            {
                myjs += "\t\n window.setInterval(\"ReinitIframe('F" + fr.MyPK + "','TD" + fr.MyPK + "')\", 200);";
            }

            myjs += "\t\n</script>";
            this.Pub1.Add(myjs);
            #endregion  Deal with iFrom  Adaptive problem .

            #region  Processing hidden field .
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM Sys_MapAttr WHERE FK_MapData='" + this.FK_MapData + "' AND GroupID NOT IN (SELECT OID FROM Sys_GroupField WHERE EnName='" + this.FK_MapData + "')");
            if (dt.Rows.Count != 0)
            {
                int gfid = gfs[0].GetValIntByKey("OID");
                foreach (DataRow dr in dt.Rows)
                    DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID=" + gfid + " WHERE MyPK='" + dr["MyPK"] + "'");

                this.Response.Redirect(this.Request.RawUrl);
            }
            #endregion  Processing hidden field .
        }

        public void InsertObjects(bool isJudgeRowIdx)
        {
            #region  Increase from the table 
            foreach (MapDtl dtl in dtls)
            {
                if (dtl.IsUse)
                    continue;

                if (isJudgeRowIdx)
                {
                    if (dtl.RowIdx != rowIdx)
                        continue;
                }

                if (dtl.GroupID == 0 && rowIdx == 0)
                {
                    dtl.GroupID = currGF.OID;
                    dtl.RowIdx = 0;
                    dtl.Update();
                }
                else if (dtl.GroupID == currGF.OID)
                {

                }
                else
                {
                    continue;
                }

                dtl.IsUse = true;
                int myidx = rowIdx + 10;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                this.Pub1.Add("<TD colspan="+md.TableCol+" class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditDtl('" + this.FK_MapData + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../Img/Btn/New.gif' border=0/> Insert Column </a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddFGroup('" + dtl.No + "');\"><img src='../Img/Btn/New.gif' border=0/> Insert Column Group </a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../Img/Btn/Copy.gif' border=0/> Copy Column </a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.HidAttr('" + dtl.No + "');\"><img src='../Img/Btn/Copy.gif' border=0/> Hide Columns </a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.DtlMTR('" + dtl.No + "');\"><img src='../Img/Btn/Copy.gif' border=0/> Multi-header </a> <a href='Action.aspx?FK_MapData=" + dtl.No + "' > From table event </a> <a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../Img/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../Img/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.AddTREnd();

                myidx++;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + dtl.No + "' height='50px' width='"+this.md.TableWidth+"' > ");
                string src = "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + dtl.No;
                this.Pub1.Add("<iframe ID='F" + dtl.No + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "'  width='" + this.md.TableWidth + "' height='10px' scrolling=no  /></iframe>");
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
            #endregion  Increase from the table 

            #region  Add attachments 
            foreach (FrmAttachment dtl in this.aths)
            {
                if (dtl.IsUse)
                    continue;

                if (isJudgeRowIdx)
                {
                    if (dtl.RowIdx != rowIdx)
                        continue;
                }

                if (dtl.GroupID == 0 && rowIdx == 0)
                {
                    dtl.GroupID = currGF.OID;
                    dtl.RowIdx = 0;
                    dtl.Update();
                }
                else if (dtl.GroupID == currGF.OID)
                {
                }
                else
                {
                    continue;
                }

                dtl.IsUse = true;
                int myidx = rowIdx + 10;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                this.Pub1.Add("<TD colspan=" + md.TableCol + " class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditAth('" + this.FK_MapData + "','" + dtl.NoOfObj + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:AthDoUp('" + dtl.MyPK + "')\" ><img src='../Img/Btn/Up.gif' border=0/></a> <a href=\"javascript:AthDoDown('" + dtl.MyPK + "')\" ><img src='../Img/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.AddTREnd();

                myidx++;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + dtl.MyPK + "' height='50px' width='1000px'>");

                string src = "../CCForm/AttachmentUpload.aspx?PKVal=0&Ath=" + dtl.NoOfObj + "&FK_MapData=" + this.FK_MapData + "&FK_FrmAttachment=" + dtl.MyPK;
                if (dtl.IsAutoSize)
                    this.Pub1.Add("<iframe ID='F" + dtl.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px' scrolling=no  /></iframe>");
                else
                    this.Pub1.Add("<iframe ID='F" + dtl.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + dtl.W + "' height='" + dtl.H + "' scrolling=auto  /></iframe>");

                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
            #endregion  Add attachments 

            #region  Increase M2M
            foreach (MapM2M m2m in dot2dots)
            {
                if (m2m.IsUse)
                    continue;

                if (isJudgeRowIdx)
                {
                    if (m2m.RowIdx != rowIdx)
                        continue;
                }

                if (m2m.GroupID == 0 && rowIdx == 0)
                {
                    m2m.GroupID = currGF.OID;
                    m2m.RowIdx = 0;
                    m2m.Update();
                }
                else if (m2m.GroupID == currGF.OID)
                {
                }
                else
                {
                    continue;
                }

                m2m.IsUse = true;
                int myidx = rowIdx + 10;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditM2M('" + this.FK_MapData + "','" + m2m.NoOfObj + "')\" >" + m2m.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:M2MDoUp('" + m2m.MyPK + "')\" ><img src='../Img/Btn/Up.gif' border=0/></a> <a href=\"javascript:M2MDoDown('" + m2m.MyPK + "')\" ><img src='../Img/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.AddTREnd();

                myidx++;
                string src = "";
                if (m2m.HisM2MType == M2MType.M2M)
                    src = "../CCForm/M2M.aspx?FK_MapData=" + this.FK_MapData + "&NoOfObj=" + m2m.NoOfObj + "&OID=0";
                else
                    src = "../CCForm/M2MM.aspx?FK_MapData=" + this.FK_MapData + "&NoOfObj=" + m2m.NoOfObj + "&OID=0";

                switch (m2m.ShowWay)
                {
                    case FrmShowWay.FrmAutoSize:
                        //this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                        //this.Pub1.Add("<TD colspan=4 ID='TD" + m2m.NoOfObj + "' width='100%'>");
                        //this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%'   scrolling=no  /></iframe>");
                        //this.Pub1.AddTDEnd();
                        //this.Pub1.AddTREnd();

                        myidx++;
                        this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                        this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + m2m.NoOfObj + "' height='50px' width='1000px'>");
                        this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px' scrolling=no  /></iframe>");
                        this.Pub1.AddTDEnd();
                        this.Pub1.AddTREnd();
                        break;
                    case FrmShowWay.FrmSpecSize:
                        this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                        this.Pub1.Add("<TD colspan=" + md.TableCol + "ID='TD" + m2m.NoOfObj + "' height='" + m2m.H + "' width='" + m2m.W + "'  >");
                        this.Pub1.Add("<iframe ID='F" + m2m.NoOfObj + "' src='" + src + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' width='" + m2m.W + "' height='" + m2m.H + "' scrolling=auto /></iframe>");
                        this.Pub1.AddTDEnd();
                        this.Pub1.AddTREnd();
                        break;
                    case FrmShowWay.Hidden:
                        break;
                    case FrmShowWay.WinOpen:
                        this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "'");
                        this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + m2m.NoOfObj + "' height='20px' width='100%' >");
                        this.Pub1.Add("<a href=\"javascript:WinOpen('" + src + "&IsOpen=1','" + m2m.W + "','" + m2m.H + "');\"  />" + m2m.Name + "</a>");
                        this.Pub1.AddTDEnd();
                        this.Pub1.AddTREnd();
                        break;
                    default:
                        break;
                }

            }
            #endregion  Increase M2M

            #region  Increase the frame 
            foreach (MapFrame fram in frams)
            {
                if (fram.IsUse)
                    continue;

                if (isJudgeRowIdx)
                {
                    if (fram.RowIdx != rowIdx)
                        continue;
                }

                if (fram.GroupID == 0 && rowIdx == 0)
                {
                    fram.GroupID = currGF.OID;
                    fram.RowIdx = 0;
                    fram.Update();
                }
                else if (fram.GroupID == currGF.OID)
                {

                }
                else
                {
                    continue;
                }

                fram.IsUse = true;
                int myidx = rowIdx + 20;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                // this.Pub1.Add("<TD colspan=4 class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditDtl('" + this.FK_MapData + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../Img/Btn/New.gif' border=0/> Insert Column </a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../Img/Btn/Copy.gif' border=0/> Copy Column </a><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../Img/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../Img/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.Add("<TD colspan=" + md.TableCol + " class=TRSum  ><div style='text-align:left; float:left'><a href=\"javascript:EditFrame('" + this.FK_MapData + "','" + fram.MyPK + "')\" >" + fram.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:FrameDoUp('" + fram.MyPK + "')\" ><img src='../Img/Btn/Up.gif' border=0/></a> <a href=\"javascript:FrameDoDown('" + fram.MyPK + "')\" ><img src='../Img/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.AddTREnd();

                myidx++;
                this.Pub1.AddTR(" ID='" + currGF.Idx + "_" + myidx + "' ");
                if (fram.IsAutoSize)
                    this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + fram.MyPK + "' height='50px' width='1000px'>");
                else
                    this.Pub1.Add("<TD colspan=" + md.TableCol + " ID='TD" + fram.MyPK + "' height='" + fram.H + "' width='" + fram.W + "' >");


                string src = fram.URL; // "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + fram.No;
                if (src.Contains("?"))
                    src += "&FK_Node=" + this.RefNo + "&WorkID=" + this.RefOID;
                else
                    src += "?FK_Node=" + this.RefNo + "&WorkID=" + this.RefOID;

                if (fram.IsAutoSize)
                    this.Pub1.Add("<iframe ID='F" + fram.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='100%' scrolling=no  /></iframe>");
                else
                    this.Pub1.Add("<iframe ID='F" + fram.MyPK + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + fram.W + "' height='" + fram.H + "' scrolling=no  /></iframe>");

                //  this.Pub1.Add("<iframe ID='F" + fram.No + "' frameborder=0 style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='" + fram.W + "px' height='" + fram.H + "px' scrolling=no /></iframe>");

                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
            #endregion  Increase from the table 
        }

        #region varable.
        private FrmAttachments _aths;
        public FrmAttachments aths
        {
            get
            {
                if (_aths == null)
                    _aths = new FrmAttachments(this.FK_MapData);
                return _aths;
            }
        }

        public GroupField currGF = new GroupField();
        private MapDtls _dtls;
        public MapDtls dtls
        {
            get
            {
                if (_dtls == null)
                    _dtls = new MapDtls(this.FK_MapData);
                return _dtls;
            }
        }
        private MapFrames _frams;
        public MapFrames frams
        {
            get
            {
                if (_frams == null)
                    _frams = new MapFrames(this.FK_MapData);
                return _frams;
            }
        }
        private MapM2Ms _dot2dots;
        public MapM2Ms dot2dots
        {
            get
            {
                if (_dot2dots == null)
                    _dot2dots = new MapM2Ms(this.FK_MapData);
                return _dot2dots;
            }
        }
        private GroupFields _gfs;
        public GroupFields gfs
        {
            get
            {
                if (_gfs == null)
                    _gfs = new GroupFields(this.FK_MapData);

                return _gfs;
            }
        }
        public int rowIdx = 0;
        public bool isLeftNext = true;
        #endregion varable.

        public string GenerLab_arr(MapAttr attr, int idx, int i, int count)
        {
            string divAttr = " onmouseover=FieldOnMouseOver('" + attr.MyPK + "') onmouseout=FieldOnMouseOut('" + attr.MyPK + "') ";
            string lab = attr.Name;
            if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
                lab = " Editor ";

            bool isLeft = true;
            if (i == 1)
                isLeft = false;

            if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
            {
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.FK:
                        lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.Enum:
                        lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                lab = attr.Name;
            }

            if (idx == 0)
            {
                /* First .*/
                return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' class='Arrow' alt=' Move to the right order ' border=0/></a></div>";
            }

            if (idx == count - 1)
            {
                /* The number of first .*/
                return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move left the order ' class='Arrow' border=0/></a>" + lab + "</div>";
            }
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move down the order ' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' alt=' Move to the right order ' class='Arrow' border=0/></a></div>";
        }
        /// <summary>
        ///  Field or Sequence number of the control .
        /// </summary>
        public int idx = 0;
        /// <summary>
        ///  Property 
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="idx"></param>
        /// <param name="i"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GenerLab(MapAttr attr, int i, int count)
        {
            idx++;

            string divAttr = " onDragEnd=onDragEndF('" + attr.MyPK + "','" + attr.GroupID + "');  onDrag=onDragF('" + attr.MyPK + "','" + attr.GroupID + "'); ";
            divAttr += " onDragOver=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "');  onDragEnter=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "'); ";
            divAttr += " onDragLeave=FieldOnMouseOut();";

            //divAttr += " onDragLeave=FieldOnMouseOut('" + attr.MyPK + "','" + attr.GroupID + "');";

            string lab = attr.Name;
            if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
                lab = " Editor ";

            bool isLeft = true;
            if (i == 1)
                isLeft = false;

            if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
            {
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.FK:
                        lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.Enum:
                        lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                lab = attr.Name;
            }


            if (idx == 0)
            {
                /* First .*/
                return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' class='Arrow' alt=' Move to the right order ' border=0/></a></div>";
            }

            if (idx == count - 1)
            {
                /* The number of first .*/
                return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move left the order ' class='Arrow' border=0/></a>" + lab + "</div>";
            }
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move down the order ' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' alt=' Move to the right order ' class='Arrow' border=0/></a></div>";

            //if (idx == 0)
            //{
            //    /* First .*/
            //    return "<div " + divAttr + " >" + lab + "</div>";
            //}

            //if (idx == count - 1)
            //{
            //    /* The number of first .*/
            //    return "<div " + divAttr + " >" + lab + "</div>";
            //}
            //return "<div " + divAttr + " >" + lab + "</div>";
        }
        public string GenerLab_bak(MapAttr attr, int idx, int i, int count)
        {
            string divAttr = " onDragEnd=onDragEndF('" + attr.MyPK + "','" + attr.GroupID + "')  onDrag=onDragF('" + attr.MyPK + "','" + attr.GroupID + "')  onMouseUp=alert('sss'); onmouseover=FieldOnMouseOver('" + attr.MyPK + "','" + attr.GroupID + "') onmouseout=FieldOnMouseOut('" + attr.MyPK + "','" + attr.GroupID + "') ";
            string lab = attr.Name;
            if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
                lab = " Editor ";

            bool isLeft = true;
            if (i == 1)
                isLeft = false;

            if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
            {
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        lab = "<a  href=\"javascript:Edit('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.FK:
                        lab = "<a  href=\"javascript:EditTable('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    case FieldTypeS.Enum:
                        lab = "<a  href=\"javascript:EditEnum('" + this.FK_MapData + "','" + attr.MyPK + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                lab = attr.Name;
            }

            if (idx == 0)
            {
                /* First .*/
                return "<div " + divAttr + " >" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' class='Arrow' alt=' Move to the right order ' border=0/></a></div>";
            }

            if (idx == count - 1)
            {
                /* The number of first .*/
                return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move left the order ' class='Arrow' border=0/></a>" + lab + "</div>";
            }
            return "<div " + divAttr + " ><a href=\"javascript:Up('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Left.gif' alt=' Move down the order ' class='Arrow' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.FK_MapData + "','" + attr.MyPK + "','1');\" ><img src='../Img/Btn/Right.gif' alt=' Move to the right order ' class='Arrow' border=0/></a></div>";
        }
    }

}