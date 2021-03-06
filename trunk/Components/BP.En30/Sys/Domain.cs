using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// ��
	/// </summary>
    public class DomainAttr:EntityNoNameAttr
    {
        /// <summary>
        /// DBLink
        /// </summary>
        public const string DBLink = "DBLink";
    }
	/// <summary>
	/// ��
	/// </summary>
	public class Domain: EntityNoName
	{
		#region  Basic properties 
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(DomainAttr.DBLink);
            }
            set
            {
                this.SetValByKey(DomainAttr.DBLink, value);
            }
        }
		#endregion

		#region  Constructor 
		/// <summary>
		/// ��
		/// </summary>
		public Domain()
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
                Map map = new Map("Sys_Domain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "Domain";
                map.EnType = EnType.Sys;
                map.AddTBStringPK(DomainAttr.No, null, " Serial number ", false, false, 0, 30, 20);
                map.AddTBString(DomainAttr.Name, null, "Name", false, false, 0, 30, 20);
                map.AddTBString(DomainAttr.DBLink, null, "DBLink", false, false, 0, 130, 20);
                 this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        #region  Rewrite 
        public override Entities GetNewEntities
        {
            get { return new Domains(); }
        }
        #endregion  Rewrite 
    }
	/// <summary>
	/// ��s
	/// </summary>
    public class Domains : EntitiesNoName
    {
        #region  Structure 
        public Domains()
        {
        }
        #endregion

        #region  Rewrite 
        /// <summary>
        ///  Get it  Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Domain();
            }
        }
        #endregion

    }
}
