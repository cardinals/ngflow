﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CCFlowWord2007;
using CCFlowWord2007.DocFlow;


namespace BP.DA
{
    /// <summary>
    ///  Database Connectivity 
    /// 2011.11.09 Liuxc
    /// </summary>
    public static class DBAccess
    {
        static DBAccess()
        {
            client = DBAccess.GetDesignerServiceInstance();
            //            client = new DocFlowSoapClient();
        }

        private static DocFlowSoapClient client;
        /// <summary>
        ///  Get WebService Object  
        /// </summary>
        /// <returns></returns>
        public static DocFlowSoapClient GetDesignerServiceInstance()
        {
            var basicBinding = new BasicHttpBinding()
            {
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                Name = "WSDesignerSoap"
            };
            basicBinding.Security.Mode = BasicHttpSecurityMode.None;
            string url = BP.WF.Glo.WFServ+"/WF/DocFlow/DocFlow.asmx";
            url = url.Replace("//", "/");
            url = url.Replace(":/", "://");

            var endPoint = new EndpointAddress(url);
            var ctor =
                typeof(DocFlowSoapClient).GetConstructor(new Type[] { typeof(Binding), typeof(EndpointAddress) });
            return (DocFlowSoapClient)ctor.Invoke(new object[] { basicBinding, endPoint });
        }
        #region Methods
        /// <summary>
        ///  Get web configuration items 
        /// </summary>
        /// <param name="key"> Configuration Item Name </param>
        /// <returns></returns>
        public static string GetWebConfigByKey(string key)
        {
            return client.GetSettingByKey(key);
        }

        /// <summary>
        ///  Run SQL Statement , Return int Digital 
        /// </summary>
        /// <param name="sql">SQL Statement </param>
        /// <returns></returns>
        public static int RunSQL(string sql)
        {
            return client.RunSQL(sql);
        }
        /// <summary>
        ///  Run SQL Statement , Return DataTable
        /// </summary>
        /// <param name="sql">SQL Statement </param>
        /// <returns></returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            return client.RunSQLReturnTable(sql);
        }
        /// <summary>
        ///  Run SQL Statement , Return string String 
        /// </summary>
        /// <param name="sql">SQL Statement </param>
        /// <returns></returns>
        public static string RunSQLReturnString(string sql)
        {
            return client.RunSQLReturnString(sql);
        }
        /// <summary>
        ///  Get a ID
        /// </summary>
        /// <returns></returns>
        public static int GenerOID()
        {
            return client.GenerOID();
        }
        #endregion
    }
}
