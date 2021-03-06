﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.Web.Controls;
namespace CCFlow.WF.CCForm
{
    public partial class WF_DtlFrm : BP.Web.WebPage
    {
        public int addRowNum
        {
            get
            {
                try
                {
                    int i = int.Parse(this.Request.QueryString["addRowNum"]);
                    if (this.Request.QueryString["IsCut"] == null)
                        return i;
                    else
                        return i;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public string RefPKVal
        {
            get
            {
                string str = this.Request.QueryString["RefPKVal"];
                if (str == null)
                    return "1";
                return str;
            }
        }
        public bool IsReadonly
        {
            get
            {
                if (this.Request.QueryString["IsReadonly"] == "1")
                    return true;
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            #region  Loading documents .
            this.Page.RegisterClientScriptBlock("sgu",
    "<link href='./Style/Frm/Tab.css' rel='stylesheet' type='text/css' />");

            this.Page.RegisterClientScriptBlock("s2g4uh",
     "<script language='JavaScript' src='./Style/Frm/jquery.min.js' ></script>");

            this.Page.RegisterClientScriptBlock("sdfuy24j",
    "<script language='JavaScript' src='./Style/Frm/jquery.idTabs.min.js' ></script>");
            #endregion  Loading documents .

            #region  Check out the table .
            MapDtl mdtl = new MapDtl(this.EnsName);
            GEDtls dtls = new GEDtls(this.EnsName);
            QueryObject qo = null;
            try
            {
                qo = new QueryObject(dtls);
                switch (mdtl.DtlOpenType)
                {
                    case DtlOpenType.ForEmp:
                        qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                        break;
                    case DtlOpenType.ForWorkID:
                        qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                        break;
                    case DtlOpenType.ForFID:
                        qo.AddWhere(GEDtlAttr.FID, this.RefPKVal);
                        break;
                }
                qo.addOrderBy("oid");
                qo.DoQuery();
            }
            catch (Exception ex)
            {
                dtls.GetNewEntity.CheckPhysicsTable();
                throw ex;

                //#region  Solve Access  The problem does not refresh .
                //string rowUrl = this.Request.RawUrl;
                //if (rowUrl.IndexOf("rowUrl") > 1)
                //{
                //    throw ex;
                //}
                //else
                //{
                //    //this.Response.Redirect(rowUrl + "&rowUrl=1&IsWap=" + this.IsWap, true);
                //    return;
                //}
                //#endregion
            }
            #endregion  Check out the table .

            #region  Initialize a blank line 
            if (this.IsReadonly == false)
            {
                mdtl.RowsOfList = mdtl.RowsOfList + this.addRowNum;
                int num = dtls.Count;
                if (mdtl.IsInsert)
                {
                    int dtlCount = dtls.Count;
                    for (int i = 0; i < mdtl.RowsOfList - dtlCount; i++)
                    {
                        BP.Sys.GEDtl dt = new GEDtl(this.EnsName);
                        dt.ResetDefaultVal();
                        //dt.OID = i;
                        dt.OID = 0;
                        dtls.AddEntity(dt);
                    }

                    if (num == mdtl.RowsOfList)
                    {
                        BP.Sys.GEDtl dt1 = new GEDtl(this.EnsName);
                        dt1.ResetDefaultVal();
                        //dt1.OID = mdtl.RowsOfList + 1;
                        dt1.OID = 0;
                        dtls.AddEntity(dt1);
                    }
                }
            }
            #endregion  Initialize a blank line 

            MapData md = new MapData(mdtl.No);
            this.UCEn1.Clear();

            this.UCEn1.Add("\t\n<div id='maintabs' class=\"easyui-tabs\" fit=\"true\" border=\"false\" style='width:" + md.FrmW + "px;height:" + md.FrmH + "px;' data-options=\"tools:'#tab-tools'\">");  //begain.

            #region  Output tab .
            int idx = 0;
            int dtlsNum = dtls.Count;
            
            foreach (GEDtl dtl in dtls)
            {
                idx++;
                this.UCEn1.Add("\t\n<div id=" + idx + " title='Item " + idx + "' style='overflow: auto;'>");
                string src = "";
                src = "FrmDtl.aspx?FK_MapData=" + this.EnsName + "&WorkID=" + this.RefPKVal + "&OID=" + dtl.OID + "&IsReadonly=" + this.IsReadonly;
                //this.UCEn1.Add("\t\n<iframe id='IF" + idx + "' Onblur=\"SaveDtlData('" + idx + "');\" frameborder='0' style='width:" + md.FrmW + "px;height:" + md.FrmH + "px;' src=\"" + src + "\"></iframe>");
                //2015-01-24 15:30 ating 把onblur去掉
                this.UCEn1.Add("\t\n<iframe id='IF" + idx + "' frameborder='0' style='width:" + md.FrmW + "px;height:" + md.FrmH + "px;' src=\"" + src + "\"></iframe>");

                this.UCEn1.Add("\t\n</div>");
            }
            this.UCEn1.Add("\t\n </div>");
            if (this.IsReadonly == false && mdtl.IsInsert)
            {
                int addNum = addRowNum + 1;
                int cutNum = addRowNum - 1;

                this.UCEn1.Add("\t\n<div id=\"tab-tools\">");
                string jscreatetab =string.Format("createTab('{0}',{1},{2},'{3}','{4}');"
                    ,"Item "
                    ,md.FrmW
                    ,md.FrmH
                    ,EnsName
                    ,RefPKVal
                    ,IsReadonly
                    );
                string jsdeletetab =string.Format("deleteTab('{0}','{1}');"
                    , EnsName
                    ,RefPKVal
                    );
                this.UCEn1.Add(
                        "\t\n<a href=\"javascript:"+jsdeletetab+"\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-reload'\"> Remove </a>");

                if (cutNum >= 0)
                {
                    //this.UCEn1.Add(
                    //    "\t\n<a href='DtlCard.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal
                    //    + "&addRowNum=" + cutNum
                    //    + "' class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-reload'\"> Remove </a>");
                    //this.UCEn1.Add("\t\n<a href='DtlCard.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal + "&addRowNum=" + addNum + "' class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-reload'\"> Insert </a>");
                    
                  //  this.UCEn1.Add(
                  //      "\t\n<a href='javascript:" + jscreatetab+ "' class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-reload'\"> Insert </a>");

                }
               // else
                //{
                    this.UCEn1.Add("\t\n<a href=\"javascript:" + jscreatetab + "\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-reload'\"> Insert </a>");
   
                //}
                    //this.UCEn1.Add("\t\n<a href='DtlCard.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal + "&addRowNum=" + addNum + "' > Insert </a>");
                this.UCEn1.Add("\t\n</div>");
            }
            #endregion  Output tab .


            if (this.IsReadonly == false)
            {

            }

            #region  Deal with iFrom SaveDtlData.
            //string js = "";
            //js = "\t\n<script type='text/javascript' >";
            //js += "\t\n function SaveDtl(dtl) { ";
            //js += "\t\n document.getElementById('F' + dtl ).contentWindow.SaveDtlData();";
            //js += "\t\n } ";
            //js += "\t\n</script>";
            //this.UCEn1.Add(js);
            #endregion  Deal with iFrom SaveDtlData.
        }
    }
}