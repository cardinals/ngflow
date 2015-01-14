﻿using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// GroupField
    /// </summary>
    public class GroupFieldAttr : EntityOIDAttr
    {
        /// <summary>
        ///  Main table 
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// Lab
        /// </summary>
        public const string Lab = "Lab";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";
    }
    /// <summary>
    /// GroupField
    /// </summary>
    public class GroupField : EntityOID
    {
        #region  Property 
        public bool IsUse = false;
        public string EnName
        {
            get
            {
                return this.GetValStrByKey(GroupFieldAttr.EnName);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.EnName, value);
            }
        }
        public string Lab
        {
            get
            {
                return this.GetValStrByKey(GroupFieldAttr.Lab);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.Lab, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(GroupFieldAttr.Idx);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.Idx, value);
            }
        }
        #endregion

        #region  Constructor 
        /// <summary>
        /// GroupField
        /// </summary>
        public GroupField()
        {
        }
        public GroupField(int oid)
            : base(oid)
        {
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_GroupField");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "GroupField";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();
                map.AddTBString(GroupFieldAttr.Lab, null, "Lab", true, false, 0, 500, 20);
                map.AddTBString(GroupFieldAttr.EnName, null, " Main table ", true, false, 0, 200, 20);
                map.AddTBInt(GroupFieldAttr.Idx, 99, "Idx", true, false);
                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public void DoDown()
        {
            this.DoOrderDown(GroupFieldAttr.EnName, this.EnName, GroupFieldAttr.Idx);
            return;
        }
        public void DoUp()
        {
            this.DoOrderUp(GroupFieldAttr.EnName, this.EnName, GroupFieldAttr.Idx);
            return;
        }
        protected override bool beforeInsert()
        {
            if (this.IsExit(GroupFieldAttr.EnName, this.EnName, GroupFieldAttr.Lab, this.Lab) == true)
                throw new Exception("@ Already ("+this.EnName+") In the presence of ("+this.Lab+") The grouping of the .");

            try
            {
                string sql = "SELECT MAX(IDX) FROM " + this.EnMap.PhysicsTable + " WHERE EnName='" + this.EnName + "'";
                this.Idx = DBAccess.RunSQLReturnValInt(sql, 0) + 1;
            }
            catch
            {
                this.Idx = 1;
            }
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// GroupFields
    /// </summary>
    public class GroupFields : EntitiesOID
    {
        #region  Structure 
        /// <summary>
        /// GroupFields
        /// </summary>
        public GroupFields()
        {
        }
        /// <summary>
        /// GroupFields
        /// </summary>
        /// <param name="EnName">s</param>
        public GroupFields(string EnName)
        {
            int i = this.Retrieve(GroupFieldAttr.EnName, EnName, GroupFieldAttr.Idx);
            if (i == 0)
            {
                GroupField gf = new GroupField();
                gf.EnName = EnName;
                MapData md = new MapData();
                md.No = EnName;
                if (md.RetrieveFromDBSources() == 0)
                    gf.Lab = " Basic Information ";
                else
                    gf.Lab = md.Name;
                gf.Idx = 0;
                gf.Insert();
                this.AddEntity(gf);
            }
        }
        /// <summary>
        ///  Get it  Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GroupField();
            }
        }
        #endregion

       
    }
}
