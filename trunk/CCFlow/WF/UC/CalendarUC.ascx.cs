﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

namespace CCFlow.WF.UC
{
    public partial class CalendarUC : BP.Web.UC.UCBase3
    {
        /// <summary>
        ///  Date 
        /// </summary>
        public string FK_Date
        {
            get
            {
                string s = this.Request.QueryString["FK_Date"];
                if (s == null)
                    s = this.FK_NY + "-01";
                return s;
            }
        }
        /// <summary>
        ///  Month 
        /// </summary>
        public string FK_NY
        {
            get
            {
                string s = this.Request.QueryString["FK_NY"];
                if (s == null)
                    return DataType.CurrentYearMonth;
                return s;
            }
        }
        /// <summary>
        ///  Staff 
        /// </summary>
        public string FK_Emp
        {
            get
            {
                string s = this.Request.QueryString["FK_Emp"];
                if (s == null)
                    return WebUser.No;
                return s;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime myday = DataType.ParseSysDate2DateTime(this.FK_Date);
            this.BindMonth(myday);
            this.BindLog(myday);
        }
        public void BindLog(DateTime dt)
        {
#warning  Undeveloped calendar .
            this.Pub1.AddMsgGreen(" Prompt "," Function changes , Not perfect .");
            return;

            this.Pub1.AddTable("width=100%");
            this.Pub1.AddCaptionLeft(" Work Calendar :" + dt.ToString("yyyy-MM-dd"));
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("IDX");
            this.Pub1.AddTDTitle(" Time ");
            this.Pub1.AddTDTitle(" Activity ");
            this.Pub1.AddTDTitle(" Process ");
            this.Pub1.AddTDTitle(" From node ");
            this.Pub1.AddTDTitle(" To node ");
            this.Pub1.AddTDTitle(" Staff ");
            this.Pub1.AddTDTitle(" Information ");
            this.Pub1.AddTDTitle(" Form ");
            this.Pub1.AddTREnd();

            string sql = "SELECT a.*,b.Name FROM WF_Track a, WF_Flow b WHERE a.FK_Flow=b.No  AND  a.RDT LIKE '" + dt.ToString("yyyy-MM-dd") + "%' AND a.EmpFrom='" + WebUser.No + "'";
            sql += " UNION ";
            sql += "SELECT a.*,b.Name FROM WF_TrackTemp a, WF_Flow b WHERE a.FK_Flow=b.No  AND  a.RDT LIKE '" + dt.ToString("yyyy-MM-dd") + "%' AND a.EmpFrom='" + WebUser.No + "'";

            DataTable dtLog = DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            foreach (DataRow dr in dtLog.Rows)
            {
                this.Pub1.AddTR();
                this.Pub1.AddTDIdx(idx);
                this.Pub1.AddTD(dr["RDT"].ToString().Substring(10, 6));
                this.Pub1.AddTD(BP.WF.Track.GetActionTypeT((ActionType)int.Parse(dr["ActionType"].ToString())));
                this.Pub1.AddTD(dr["Name"].ToString());
                this.Pub1.AddTD(dr["NDFromT"].ToString());
                this.Pub1.AddTD(dr["NDToT"].ToString());
                this.Pub1.AddTD(dr["EmpToT"].ToString());
                this.Pub1.AddTD(DataType.ParseText2Html(dr["Msg"].ToString()));
                this.Pub1.AddTD("<a href=\"javascript:WinOpen('WFRpt.aspx?DoType=View&MyPK=" + dr["MyPK"].ToString() + "','" + dr["MyPK"].ToString() + "');\"> Form </a>");
                this.Pub1.AddTREnd();
                idx++;
            }
            this.Pub1.AddTableEnd();
        }
        public void BindMonth(DateTime dt)
        {
#warning  Undeveloped calendar .
            this.Pub1.AddMsgGreen(" Prompt ", " Function changes , Not perfect .");
            return;

            string html = "";
            string year = dt.Year.ToString();
            string month = dt.Month.ToString();
            string day = dt.Day.ToString();
            string ny = dt.ToString("yyyy-MM");
            string today = DataType.CurrentData;
            string selectedDay = dt.ToString("yyyy-MM-dd");
            string sql = "SELECT * FROM WF_Track_NYR WHERE FK_NY='" + dt.ToString("yyyy-MM") + "' AND FK_Emp='" + WebUser.No + "'";
            //sql += " UNION ";
            //sql += "SELECT * FROM WF_Track_NYR WHERE FK_NY='" + dt.ToString("yyyy-MM") + "' AND FK_Emp='" + WebUser.No + "'";

            DataTable dtLog = DBAccess.RunSQLReturnTable(sql);

            //  The first day of a month 
            DateTime firstDay = DataType.ParseSysDate2DateTime(year + "-" + month.PadLeft(2, '0') + "-01");
            switch (firstDay.DayOfWeek)
            {
                case DayOfWeek.Sunday: // 0
                    break;
                case DayOfWeek.Monday: // 1
                    firstDay = firstDay.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday: // 2
                    firstDay = firstDay.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday: // 3
                    firstDay = firstDay.AddDays(-3);
                    break;
                case DayOfWeek.Thursday: // 4
                    firstDay = firstDay.AddDays(-4);
                    break;
                case DayOfWeek.Friday: // 5
                    firstDay = firstDay.AddDays(-5);
                    break;
                case DayOfWeek.Saturday: // 6
                    firstDay = firstDay.AddDays(-6);
                    break;
            } //  After the operation , Can not be the day of the month .

            html += "<Table border=1 width=100% height=100% class='CTable' alt=' Double-click on the content of the day date appears ' style='border-collapse: collapse' bordercolor='#111111' valign=top  >";
            html += "<TR>";
            html += " <TD align=center colspan=7 class=Sum ><a href='" + this.PageID + ".aspx?FK_NY=" + dt.AddMonths(-1).ToString("yyyy-MM") + "&CalendarType=1'  > <img src='/WF/Img/Arr/Arrowhead_Previous_S.gif' border=0 /> </a>" + year + "-" + month + "<a href='" + this.PageID + ".aspx?CalendarType=1&FK_NY=" + dt.AddMonths(+1).ToString("yyyy-MM") + "'  ><img src='/WF/Img/Arr/Arrowhead_Next_S.gif' border=0 /></a></TD>";
            html += "</TR>";

            html += "<TR >";
            html += " <TD class='Week' >Sun</TD>";
            html += " <TD class='Week' >Mon</TD>";
            html += " <TD class='Week' >Tues</TD>";
            html += " <TD class='Week' >Wed</TD>";
            html += " <TD class='Week' >Thurs</TD>";
            html += " <TD class='Week' >Fri</TD>";
            html += " <TD class='Week' >Sat</TD>";
            html += "</TR>";

            int i = 0;
            int week = 0;
            int Mday = 0;
            string cellDate = "";
            int workNum = 0;
            while (i != 35)
            {
                i++;
                cellDate = firstDay.ToString(DataType.SysDataFormat);
                Mday = firstDay.Day;
                workNum = 0;
                foreach (DataRow dr in dtLog.Rows)
                {
                    if (dr["RDT"].ToString() == cellDate)
                    {
                        workNum = int.Parse(dr["Num"].ToString());
                        break;
                    }
                }
                switch (firstDay.DayOfWeek)
                {
                    case DayOfWeek.Sunday: // 0
                        html += "<TR>";
                        week++;
                        if (workNum > 0)
                            html += " <TD class='HolidayHave'  ><a href='" + this.PageID + ".aspx?FK_Date=" + cellDate + "' ><b>" + Mday + "</b></a></TD>";
                        else
                            html += " <TD class='Holiday'>" + Mday + "</TD>";
                        break;
                    case DayOfWeek.Saturday: // 1
                        if (workNum > 0)
                            html += " <TD class='HolidayHave'  ><a href='" + this.PageID + ".aspx?FK_Date=" + cellDate + "' ><b>" + Mday + "</b></a></TD>";
                        else
                            html += " <TD  class='Holiday' >" + Mday + "</TD>";
                        html += "</TR>";
                        break;
                    default:
                        if (workNum > 0)
                            html += " <TD class='DayHave'  ><a href='" + this.PageID + ".aspx?FK_Date=" + cellDate + "' ><b>" + Mday + "</b></a></TD>";
                        else
                            html += " <TD  class='Day' >" + Mday + "</TD>";
                        break;
                }
                firstDay = firstDay.AddDays(1);
            }
            html += "</Table>";
            this.Left.Add(html);
            return;

            if (dtLog.Rows.Count > 0)
            {
                this.Left.AddUL();
                foreach (DataRow dr in dtLog.Rows)
                {
                    this.Left.AddLi(this.PageID + ".aspx?FK_Data=" + dr["RDT"], dr["RDT"] + "" + dr["Num"]);
                }
                this.Left.AddULEnd();

            }
        }
    }
}