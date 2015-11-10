﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.DA;

public partial class AppDemo_Default123 : System.Web.UI.Page
{
    #region   Operating variables 
    /// <summary>
    ///  The current process ID 
    /// </summary>
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (string.IsNullOrEmpty(s))
                throw new Exception("@ Process ID is empty ...");
            return s;
        }
    }
    public string FromNode
    {
        get
        {
            return this.Request.QueryString["FromNode"];
        }
    }
    /// <summary>
    ///  Current work ID
    /// </summary>
    public Int64 WorkID
    {
        get
        {
            if (ViewState["WorkID"] == null)
            {
                if (this.Request.QueryString["WorkID"] == null)
                    return 0;
                else
                    return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            else
                return Int64.Parse(ViewState["WorkID"].ToString());
        }
        set
        {
            ViewState["WorkID"] = value;
        }
    }
    private int _FK_Node = 0;
    /// <summary>
    ///  Current  NodeID , At the beginning of time ,nodeID, Is to a , Start node processes ID.
    /// </summary>
    public int FK_Node
    {
        get
        {
            string fk_nodeReq = this.Request.QueryString["FK_Node"];
            if (string.IsNullOrEmpty(fk_nodeReq))
                fk_nodeReq = this.Request.QueryString["NodeID"];

            if (string.IsNullOrEmpty(fk_nodeReq) == false)
                return int.Parse(fk_nodeReq);

            if (_FK_Node == 0)
            {
                if (this.Request.QueryString["WorkID"] != null)
                {
                    string sql = "SELECT FK_Node from  WF_GenerWorkFlow where WorkID=" + this.WorkID;
                    _FK_Node = DBAccess.RunSQLReturnValInt(sql);
                }
                else
                {
                    _FK_Node = int.Parse(this.FK_Flow + "01");
                }
            }
            return _FK_Node;
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int PWorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["PWorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    #endregion

    public string mainSrc = "../../Start.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BP.Web.WebUser.No == null)
        {
            this.Response.Redirect("Login.aspx", true);
            return;
        }

        if (!IsPostBack)
        {
            // Are there traditional values 
            if (this.Request.QueryString.Count > 0)
            {
                string paras = "";
                foreach (string str in this.Request.QueryString)
                {
                    string val = this.Request.QueryString[str];
                    if (val.IndexOf('@') != -1)
                        throw new Exception(" You can not have arguments : [ " + str + " ," + val + " ]  To value  ,URL  Will not be executed .");

                    switch (str)
                    {
                        case DoWhatList.DoNode:
                        case DoWhatList.Emps:
                        case DoWhatList.EmpWorks:
                        case DoWhatList.FlowSearch:
                        case DoWhatList.Login:
                        case DoWhatList.MyFlow:
                        case DoWhatList.MyWork:
                        case DoWhatList.Start:
                        case DoWhatList.Start5:
                        case DoWhatList.StartSmall:
                        case DoWhatList.FlowFX:
                        case DoWhatList.DealWork:
                        case DoWhatList.DealWorkInSmall:
                        case "FK_Flow":
                        case "WorkID":
                        case "FK_Node":
                        case "SID":
                            break;
                        default:
                            paras += "&" + str + "=" + val;
                            break;
                    }
                }
                mainSrc = "../../MyFlow.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + FK_Node;
            }
        }
    }
}