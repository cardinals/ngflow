﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;
using System.Data;
using System.Collections;
using BP.CT;

namespace BP.CT.AttrFlow
{
    /// <summary>
    ///  Generation title 
    /// </summary>
    public  class GenerTitle : TestBase
    {
        #region  Variable 
        /// <summary>
        ///  Process ID 
        /// </summary>
        public string fk_flow = "";
        /// <summary>
        ///  User ID 
        /// </summary>
        public string userNo = "";
        /// <summary>
        ///  All processes 
        /// </summary>
        public Flow fl = null;
        /// <summary>
        ///  The main thread ID
        /// </summary>
        public Int64 workID = 0;
        /// <summary>
        ///  After sending the return object 
        /// </summary>
        public SendReturnObjs objs = null;
        /// <summary>
        ///  Staff List 
        /// </summary>
        public GenerWorkerList gwl = null;
        /// <summary>
        ///  Process Registry 
        /// </summary>
        public GenerWorkFlow gwf = null;
        #endregion  Variable 

        /// <summary>
        ///  Generation title 
        /// </summary>
        public GenerTitle()
        {
            this.Title = " Title generation rules ";
            this.DescIt = " Process :023 The simplest 3 Node ( Track mode ).";
            this.EditState = EditState.Passed; 
        }
        /// <summary>
        ///  Process Description :
        /// 1, In the process  023 The simplest 3 Node ( Track mode ),  To test .
        /// 2, Only test transmission function , After checking with the data sent is complete .
        /// 3,  This test has three node initiates points , Intermediate point , End point , Corresponding to the three test methods .
        /// </summary>
        public override void Do()
        {
            #region  Definition of variables .
            fk_flow = "023";
            userNo = "zhanghaicheng";
            fl = new Flow(fk_flow);
            #endregion  Definition of variables .

            //让 userNo  Log in .
            BP.WF.Dev2Interface.Port_Login(userNo);
            // Create a blank ,  In case the title is empty .
            workID = BP.WF.Dev2Interface.Node_CreateBlankWork(fk_flow, null, null, WebUser.No, "TitleTest", 0, null, 0, null);

            #region  Check whether the title in line with expectations .
            string title = DBAccess.RunSQLReturnString("SELECT Title FROM " + fl.PTable + " WHERE OID=" + workID);
            if (title != "TitleTest")
                throw new Exception("@ No data is specified (TitleTest) Generation title ,  Reports in the process , It is :"+title);
            #endregion 

            // Performing transmission .
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workID);

            #region  Check whether the title in line with expectations .
            title = DBAccess.RunSQLReturnString("SELECT  Title FROM " + fl.PTable + " where OID=" + workID);
            if (title != "TitleTest")
                throw new Exception("@ After sending : The title is not generated by the specified data ,  Reports in the process ."+title);
            #endregion 

            // Delete test data .
            BP.WF.Dev2Interface.Flow_DoDeleteFlowByReal(fl.No, workID, false);


            // Create a blank , 让ccflow Automatically generate the title according to the rules .
            workID = BP.WF.Dev2Interface.Node_CreateBlankWork(fk_flow, null, null, WebUser.No, null, 0, null, 0, null);

            #region  Check whether the title in line with expectations .
            //title = DBAccess.RunSQLReturnString("SELECT Title FROM WF_GenerWorkFlow where WorkID=" + workID);
            //if (string.IsNullOrEmpty(title))
            //    throw new Exception("@ The title does not generate  在 WF_GenerWorkFlow Not found .");

            title = DBAccess.RunSQLReturnString("SELECT Title FROM " + fl.PTable + " where OID=" + workID);
            if (string.IsNullOrEmpty(title))
                throw new Exception("@ The title does not generate , 在 PTable Not found .");
            #endregion

            // Performing transmission .
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workID);

            #region  Check whether the title in line with expectations .
            title = DBAccess.RunSQLReturnString("SELECT Title FROM WF_GenerWorkFlow where WorkID=" + workID);
            if (string.IsNullOrEmpty(title))
                throw new Exception("@ The title does not generate  在 WF_GenerWorkFlow Not found .");

            title = DBAccess.RunSQLReturnString("SELECT Title FROM " + fl.PTable + " WHERE OID=" + workID);
            if (string.IsNullOrEmpty(title))
                throw new Exception("@ The title does not generate , 在 PTable Not found .");
            #endregion 

            // Delete test data .
            BP.WF.Dev2Interface.Flow_DoDeleteFlowByReal(fl.No, workID, false);
        }
    }
}
