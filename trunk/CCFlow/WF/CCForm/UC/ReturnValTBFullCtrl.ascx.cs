﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.En;
using BP.Sys;
using BP.Sys.Xml;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
using Newtonsoft.Json.Linq;

public partial class ReturnValTBFullCtrl : BP.Web.UC.UCBase3
{
    string FK_MapExt
    {
        get
        {
            return this.Request.QueryString["FK_MapExt"];
        }
    }
    string Val
    {
        get
        {
            string s = this.Request.QueryString["CtrlVal"];
            if ( s==null || s == "")
                s = null;
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MapExt ext = new MapExt(this.FK_MapExt);
        string sql = ext.TagOfSQL_autoFullTB;
        if (this.Val != null)
            sql = sql.Replace("@Key", this.Val);

        sql = sql.Replace("$", "");
        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        BindIt(dt);
    }
    public void BindIt(DataTable dt)
    {
        this.AddTable("width=80%");
        this.AddTR();
        this.AddTDBegin("class=Title colspan=" + dt.Columns.Count);
        this.Add(" Keyword ");
        TextBox tb = new TextBox();
        tb.ID = "TB_Key";
        tb.Text = this.Val;
        this.Add(tb);

        Button btn = new Button();
        btn.CssClass = "Btn";
        btn.ID = "Btn_Search";
        btn.Text = " Find ";
        btn.Click += new EventHandler(btn_Search_Click);
        this.Add(btn);

        Button btn0 = new Button();
        btn0.ID = "s";
        btn0.CssClass = "Btn";
        btn0.Text = " Determine ";
        btn0.Click += new EventHandler(btn_Click);
        this.Add(btn0);

        this.AddTDEnd();
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle(" Choose ");
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName == "No" || dc.ColumnName == "Name")
                continue;
            this.AddTDTitle(dc.ColumnName);
        }
        this.AddTREnd();

        foreach (DataRow dr in dt.Rows)
        {
            this.AddTR();
            RadioButton rb = new RadioButton();
            rb.Text = dr["No"].ToString() + "," + dr["Name"].ToString();
            rb.ID = "RB_" + dr["No"];
            rb.GroupName = "sd";
            this.AddTD(rb);

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "No" || dc.ColumnName == "Name")
                    continue;

                this.AddTD(dr[dc.ColumnName].ToString());
            }
            this.AddTREnd();
        }
        this.AddTableEndWithHR();
        
    }
    void btn_Search_Click(object sender, EventArgs e)
    {
        string key = this.GetTextBoxByID("TB_Key").Text;
        this.Response.Redirect("FrmReturnValTBFullCtrl.aspx?FK_MapExt=" + this.FK_MapExt + "&CtrlVal=" + key, true);
    }
    void btn_Click(object sender, EventArgs e)
    {
        MapExt ext = new MapExt(this.FK_MapExt);
        string sql = ext.TagOfSQL_autoFullTB;
        if (this.Val != null)
            sql = sql.Replace("@Key", this.Val);

        sql = sql.Replace("$", "");

        string val = "";
        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            RadioButton rb = this.GetRadioButtonByID("RB_" + dr["No"]);
            if (rb.Checked)
            {
                //val = dr["No"].ToString();
                JObject objreturn = new JObject();
                foreach (DataColumn col in dt.Columns) {
                    objreturn[col.ColumnName] = dr[col.ColumnName].ToString();
                }
                val = objreturn.ToString();
                string clientscript = "<script language='javascript'> window.returnValue = " + val + "; window.close(); </script>";
                this.Page.Response.Write(clientscript);
                return;

            }
        }
        this.WinClose(val);
    }
}