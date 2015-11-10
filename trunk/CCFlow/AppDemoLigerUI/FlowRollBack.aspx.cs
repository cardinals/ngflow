﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;

namespace CCFlow.AppDemoLigerUI
{
    public partial class FlowRollBack : System.Web.UI.Page
    {
        #region  Property 
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public Int64 WorkID
        {
            get
            {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
        }
        #endregion  Property 
        protected void Page_Load(object sender, EventArgs e)
        {
            Nodes nds = new Nodes(this.FK_Flow);
            foreach (BP.WF.Node nd in nds)
            {
                this.DDL_SkipToNode.Items.Add(new ListItem(" Step :" + nd.Step + " Name :" + nd.Name,
                    nd.NodeID.ToString()));
            }

        }
        protected void Btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                string note = this.TB_Note.Text.Trim();

                int nodeID = int.Parse(this.DDL_SkipToNode.SelectedItem.Value);

                // Performing transmission .
                //string info =  BP.WF.Dev2Interface.Node_SendWork(this.FK_Flow, this.WorkID, null, null, nd.NodeID, emp.No).ToMsgOfText();
                string info = BP.WF.Dev2Interface.Flow_DoRebackWorkFlow(this.FK_Flow, this.WorkID, nodeID, note);

                //  Prompt .
                BP.Sys.PubClass.Alert(" Data has been successfully rolled back ," + info);
                BP.Sys.PubClass.WinClose("ss");
            }
            catch (Exception ex)
            {
                BP.Sys.PubClass.Alert(ex.Message);
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BP.Sys.PubClass.WinClose();
        }

    }
}