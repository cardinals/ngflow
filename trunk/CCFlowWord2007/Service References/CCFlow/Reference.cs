﻿//------------------------------------------------------------------------------
// <auto-generated>
//      This code is generated by the tool .
//      Runtime version :4.0.30319.1
//
//      Changes to this file may cause incorrect behavior , And if 
//      Regenerate code , These changes will be lost .
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCFlowWord2007.CCFlow {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CCFlow.CCFlowAPISoap")]
    public interface CCFlowAPISoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Port_SigOut", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Port_SigOut(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Port_Menu", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Port_Menu(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Port_ChangePassword", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Port_ChangePassword(string userNo, string oldPass, string newPass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Port_SMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Port_SMS(string userNo, string lastTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_GenerWillReturnNodes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_GenerWillReturnNodes(int nodeID, long workid, long fid, string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DataTable_DB_GenerWillReturnNodes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable DataTable_DB_GenerWillReturnNodes(int nodeID, long workid, long fid, string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_TaskPool", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_TaskPool(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_TaskPoolOfMyApply", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_TaskPoolOfMyApply(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_GenerCanStartFlowsOfDataTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_GenerCanStartFlowsOfDataTable(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DataTable_DB_GenerCanStartFlowsOfDataTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable DataTable_DB_GenerCanStartFlowsOfDataTable(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DataTable_DB_GenerEmpWorksOfDataTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable DataTable_DB_GenerEmpWorksOfDataTable(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_GenerEmpWorksOfDataTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_GenerEmpWorksOfDataTable(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_CCList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_CCList(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_GenerHungUpList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_GenerHungUpList(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DB_GenerRuning", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DB_GenerRuning(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_CreateBlankWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        long Node_CreateBlankWork(string flowNo, string starter, string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_CC_DoDel", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Node_CC_DoDel(string mypk);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_CC_To", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_CC_To(string flowNo, long workID, string toEmpNo, string toEmpName, string msgTitle, string msgDoc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_CC", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_CC(string fk_flow, int fk_node, long workID, string toEmpNo, string toEmpName, string msgTitle, string msgDoc, string pFlowNo, long pWorkID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_SetDraft", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Node_SetDraft(string fk_flow, long workID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_UnHungUpWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Node_UnHungUpWork(string fk_flow, long workid, string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Flow_DoUnSend", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Flow_DoUnSend(string fk_flow, long workid, string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_HungUpWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_HungUpWork(string fk_flow, long workid, int wayInt, string reldata, string hungNote);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_Shift", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_Shift(long workID, string toEmp, string msg, string userNo, string sid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_ReturnWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_ReturnWork(string fk_flow, long workID, long fid, int currentNodeID, int returnToNodeID, string msg, bool isBackToThisNode, string userNo, string sid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AlertString", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AlertString(string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Port_Login", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Port_Login(string userNo, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoIt", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DoIt(string flag, string userNo, string sid, string val0, string val1, string val2, string val3, string val4, string val5);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GenerWorkNode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GenerWorkNode(string fk_flow, int fk_node, long workID, long fid, string userNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_SaveWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_SaveWork(string fk_flow, int fk_node, long workID, string userNo, string dsXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Node_SendWork", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Node_SendWork(string fk_flow, int fk_node, long workID, string dsXml, string currUserNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNoName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetNoName(string SQL);
        
        // CODEGEN:  Parameters [intoBuffer] Other programs require information , Parameter model can not capture this information . Specific characteristics of [System.Xml.Serialization.XmlElementAttribute].
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Upload", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        CCFlowWord2007.CCFlow.UploadResponse Upload(CCFlowWord2007.CCFlow.UploadRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ParseExp", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ParseExp(string strExp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CfgKey", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string CfgKey(string kev);
        
        // CODEGEN:  Parameters [FileByte] Other programs require information , Parameter model can not capture this information . Specific characteristics of [System.Xml.Serialization.XmlElementAttribute].
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        CCFlowWord2007.CCFlow.UploadFileResponse UploadFile(CCFlowWord2007.CCFlow.UploadFileRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int RunSQL(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLs", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int RunSQLs(string sqls);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RunSQLReturnTable(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnTable2DataTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable RunSQLReturnTable2DataTable(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnString", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RunSQLReturnString(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnValInt", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int RunSQLReturnValInt(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnValFloat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        float RunSQLReturnValFloat(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnTableS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RunSQLReturnTableS(string sqls);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ParseStringToPinyin", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ParseStringToPinyin(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveEnum", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SaveEnum(string enumKey, string enumLab, string cfg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveEnumField", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SaveEnumField(string fk_mapData, string fieldKey, string fieldName, string enumKey, double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveFKField", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SaveFKField(string fk_mapData, string fieldKey, string fieldName, string enName, double x, double y);
        
        // CODEGEN:  Parameters [img] Other programs require information , Parameter model can not capture this information . Specific characteristics of [System.Xml.Serialization.XmlElementAttribute].
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveImageAsFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        CCFlowWord2007.CCFlow.SaveImageAsFileResponse SaveImageAsFile(CCFlowWord2007.CCFlow.SaveImageAsFileRequest request);
        
        // CODEGEN:  Parameters [fileByte] Other programs require information , Parameter model can not capture this information . Specific characteristics of [System.Xml.Serialization.XmlElementAttribute].
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LoadFrmTemplete", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        CCFlowWord2007.CCFlow.LoadFrmTempleteResponse LoadFrmTemplete(CCFlowWord2007.CCFlow.LoadFrmTempleteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LoadFrmTempleteFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LoadFrmTempleteFile(string file, string fk_mapData, bool isClear, bool isSetReadonly);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetXmlData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetXmlData(string xmlFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DoType([System.ServiceModel.MessageParameterAttribute(Name="dotype")] string dotype1, string v1, string v2, string v3, string v4, string v5);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveEn", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SaveEn(string vals);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FtpMethod", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string FtpMethod(string doType, string v1, string v2, string v3);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RequestSFTableV1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RequestSFTableV1(string uiBindKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GenerFrm", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GenerFrm(string fk_mapdata, int workID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CopyFrm", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string CopyFrm(string fromMapData, string fk_mapdata, bool isClear, bool isSetReadonly);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveFrm", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SaveFrm(string fk_mapdata, string xml, string sqls, string mapAttrKeyName);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Upload", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public long offSet;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] intoBuffer;
        
        public UploadRequest() {
        }
        
        public UploadRequest(string fileName, long offSet, byte[] intoBuffer) {
            this.fileName = fileName;
            this.offSet = offSet;
            this.intoBuffer = intoBuffer;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool UploadResult;
        
        public UploadResponse() {
        }
        
        public UploadResponse(bool UploadResult) {
            this.UploadResult = UploadResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] FileByte;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string fileName;
        
        public UploadFileRequest() {
        }
        
        public UploadFileRequest(byte[] FileByte, string fileName) {
            this.FileByte = FileByte;
            this.fileName = fileName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string UploadFileResult;
        
        public UploadFileResponse() {
        }
        
        public UploadFileResponse(string UploadFileResult) {
            this.UploadFileResult = UploadFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SaveImageAsFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SaveImageAsFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] img;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string pkval;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string fk_Frm_Ele;
        
        public SaveImageAsFileRequest() {
        }
        
        public SaveImageAsFileRequest(byte[] img, string pkval, string fk_Frm_Ele) {
            this.img = img;
            this.pkval = pkval;
            this.fk_Frm_Ele = fk_Frm_Ele;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SaveImageAsFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SaveImageAsFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string SaveImageAsFileResult;
        
        public SaveImageAsFileResponse() {
        }
        
        public SaveImageAsFileResponse(string SaveImageAsFileResult) {
            this.SaveImageAsFileResult = SaveImageAsFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="LoadFrmTemplete", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class LoadFrmTempleteRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] fileByte;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string fk_mapData;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public bool isClear;
        
        public LoadFrmTempleteRequest() {
        }
        
        public LoadFrmTempleteRequest(byte[] fileByte, string fk_mapData, bool isClear) {
            this.fileByte = fileByte;
            this.fk_mapData = fk_mapData;
            this.isClear = isClear;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="LoadFrmTempleteResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class LoadFrmTempleteResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string LoadFrmTempleteResult;
        
        public LoadFrmTempleteResponse() {
        }
        
        public LoadFrmTempleteResponse(string LoadFrmTempleteResult) {
            this.LoadFrmTempleteResult = LoadFrmTempleteResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CCFlowAPISoapChannel : CCFlowWord2007.CCFlow.CCFlowAPISoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CCFlowAPISoapClient : System.ServiceModel.ClientBase<CCFlowWord2007.CCFlow.CCFlowAPISoap>, CCFlowWord2007.CCFlow.CCFlowAPISoap {
        
        public CCFlowAPISoapClient() {
        }
        
        public CCFlowAPISoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CCFlowAPISoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CCFlowAPISoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CCFlowAPISoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Port_SigOut(string userNo) {
            base.Channel.Port_SigOut(userNo);
        }
        
        public string Port_Menu(string userNo) {
            return base.Channel.Port_Menu(userNo);
        }
        
        public string Port_ChangePassword(string userNo, string oldPass, string newPass) {
            return base.Channel.Port_ChangePassword(userNo, oldPass, newPass);
        }
        
        public string Port_SMS(string userNo, string lastTime) {
            return base.Channel.Port_SMS(userNo, lastTime);
        }
        
        public string DB_GenerWillReturnNodes(int nodeID, long workid, long fid, string userNo) {
            return base.Channel.DB_GenerWillReturnNodes(nodeID, workid, fid, userNo);
        }
        
        public System.Data.DataTable DataTable_DB_GenerWillReturnNodes(int nodeID, long workid, long fid, string userNo) {
            return base.Channel.DataTable_DB_GenerWillReturnNodes(nodeID, workid, fid, userNo);
        }
        
        public string DB_TaskPool(string userNo) {
            return base.Channel.DB_TaskPool(userNo);
        }
        
        public string DB_TaskPoolOfMyApply(string userNo) {
            return base.Channel.DB_TaskPoolOfMyApply(userNo);
        }
        
        public string DB_GenerCanStartFlowsOfDataTable(string userNo) {
            return base.Channel.DB_GenerCanStartFlowsOfDataTable(userNo);
        }
        
        public System.Data.DataTable DataTable_DB_GenerCanStartFlowsOfDataTable(string userNo) {
            return base.Channel.DataTable_DB_GenerCanStartFlowsOfDataTable(userNo);
        }
        
        public System.Data.DataTable DataTable_DB_GenerEmpWorksOfDataTable(string userNo) {
            return base.Channel.DataTable_DB_GenerEmpWorksOfDataTable(userNo);
        }
        
        public string DB_GenerEmpWorksOfDataTable(string userNo) {
            return base.Channel.DB_GenerEmpWorksOfDataTable(userNo);
        }
        
        public string DB_CCList(string userNo) {
            return base.Channel.DB_CCList(userNo);
        }
        
        public string DB_GenerHungUpList(string userNo) {
            return base.Channel.DB_GenerHungUpList(userNo);
        }
        
        public string DB_GenerRuning(string userNo) {
            return base.Channel.DB_GenerRuning(userNo);
        }
        
        public long Node_CreateBlankWork(string flowNo, string starter, string title) {
            return base.Channel.Node_CreateBlankWork(flowNo, starter, title);
        }
        
        public void Node_CC_DoDel(string mypk) {
            base.Channel.Node_CC_DoDel(mypk);
        }
        
        public string Node_CC_To(string flowNo, long workID, string toEmpNo, string toEmpName, string msgTitle, string msgDoc) {
            return base.Channel.Node_CC_To(flowNo, workID, toEmpNo, toEmpName, msgTitle, msgDoc);
        }
        
        public string Node_CC(string fk_flow, int fk_node, long workID, string toEmpNo, string toEmpName, string msgTitle, string msgDoc, string pFlowNo, long pWorkID) {
            return base.Channel.Node_CC(fk_flow, fk_node, workID, toEmpNo, toEmpName, msgTitle, msgDoc, pFlowNo, pWorkID);
        }
        
        public void Node_SetDraft(string fk_flow, long workID) {
            base.Channel.Node_SetDraft(fk_flow, workID);
        }
        
        public void Node_UnHungUpWork(string fk_flow, long workid, string msg) {
            base.Channel.Node_UnHungUpWork(fk_flow, workid, msg);
        }
        
        public void Flow_DoUnSend(string fk_flow, long workid, string userNo) {
            base.Channel.Flow_DoUnSend(fk_flow, workid, userNo);
        }
        
        public string Node_HungUpWork(string fk_flow, long workid, int wayInt, string reldata, string hungNote) {
            return base.Channel.Node_HungUpWork(fk_flow, workid, wayInt, reldata, hungNote);
        }
        
        public string Node_Shift(long workID, string toEmp, string msg, string userNo, string sid) {
            return base.Channel.Node_Shift(workID, toEmp, msg, userNo, sid);
        }
        
        public string Node_ReturnWork(string fk_flow, long workID, long fid, int currentNodeID, int returnToNodeID, string msg, bool isBackToThisNode, string userNo, string sid) {
            return base.Channel.Node_ReturnWork(fk_flow, workID, fid, currentNodeID, returnToNodeID, msg, isBackToThisNode, userNo, sid);
        }
        
        public string AlertString(string userNo) {
            return base.Channel.AlertString(userNo);
        }
        
        public string Port_Login(string userNo, string pass) {
            return base.Channel.Port_Login(userNo, pass);
        }
        
        public string DoIt(string flag, string userNo, string sid, string val0, string val1, string val2, string val3, string val4, string val5) {
            return base.Channel.DoIt(flag, userNo, sid, val0, val1, val2, val3, val4, val5);
        }
        
        public string GenerWorkNode(string fk_flow, int fk_node, long workID, long fid, string userNo) {
            return base.Channel.GenerWorkNode(fk_flow, fk_node, workID, fid, userNo);
        }
        
        public string Node_SaveWork(string fk_flow, int fk_node, long workID, string userNo, string dsXml) {
            return base.Channel.Node_SaveWork(fk_flow, fk_node, workID, userNo, dsXml);
        }
        
        public string Node_SendWork(string fk_flow, int fk_node, long workID, string dsXml, string currUserNo) {
            return base.Channel.Node_SendWork(fk_flow, fk_node, workID, dsXml, currUserNo);
        }
        
        public string GetNoName(string SQL) {
            return base.Channel.GetNoName(SQL);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CCFlowWord2007.CCFlow.UploadResponse CCFlowWord2007.CCFlow.CCFlowAPISoap.Upload(CCFlowWord2007.CCFlow.UploadRequest request) {
            return base.Channel.Upload(request);
        }
        
        public bool Upload(string fileName, long offSet, byte[] intoBuffer) {
            CCFlowWord2007.CCFlow.UploadRequest inValue = new CCFlowWord2007.CCFlow.UploadRequest();
            inValue.fileName = fileName;
            inValue.offSet = offSet;
            inValue.intoBuffer = intoBuffer;
            CCFlowWord2007.CCFlow.UploadResponse retVal = ((CCFlowWord2007.CCFlow.CCFlowAPISoap)(this)).Upload(inValue);
            return retVal.UploadResult;
        }
        
        public string ParseExp(string strExp) {
            return base.Channel.ParseExp(strExp);
        }
        
        public string CfgKey(string kev) {
            return base.Channel.CfgKey(kev);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CCFlowWord2007.CCFlow.UploadFileResponse CCFlowWord2007.CCFlow.CCFlowAPISoap.UploadFile(CCFlowWord2007.CCFlow.UploadFileRequest request) {
            return base.Channel.UploadFile(request);
        }
        
        public string UploadFile(byte[] FileByte, string fileName) {
            CCFlowWord2007.CCFlow.UploadFileRequest inValue = new CCFlowWord2007.CCFlow.UploadFileRequest();
            inValue.FileByte = FileByte;
            inValue.fileName = fileName;
            CCFlowWord2007.CCFlow.UploadFileResponse retVal = ((CCFlowWord2007.CCFlow.CCFlowAPISoap)(this)).UploadFile(inValue);
            return retVal.UploadFileResult;
        }
        
        public int RunSQL(string sql) {
            return base.Channel.RunSQL(sql);
        }
        
        public int RunSQLs(string sqls) {
            return base.Channel.RunSQLs(sqls);
        }
        
        public string RunSQLReturnTable(string sql) {
            return base.Channel.RunSQLReturnTable(sql);
        }
        
        public System.Data.DataTable RunSQLReturnTable2DataTable(string sql) {
            return base.Channel.RunSQLReturnTable2DataTable(sql);
        }
        
        public string RunSQLReturnString(string sql) {
            return base.Channel.RunSQLReturnString(sql);
        }
        
        public int RunSQLReturnValInt(string sql) {
            return base.Channel.RunSQLReturnValInt(sql);
        }
        
        public float RunSQLReturnValFloat(string sql) {
            return base.Channel.RunSQLReturnValFloat(sql);
        }
        
        public string RunSQLReturnTableS(string sqls) {
            return base.Channel.RunSQLReturnTableS(sqls);
        }
        
        public string ParseStringToPinyin(string name) {
            return base.Channel.ParseStringToPinyin(name);
        }
        
        public string SaveEnum(string enumKey, string enumLab, string cfg) {
            return base.Channel.SaveEnum(enumKey, enumLab, cfg);
        }
        
        public string SaveEnumField(string fk_mapData, string fieldKey, string fieldName, string enumKey, double x, double y) {
            return base.Channel.SaveEnumField(fk_mapData, fieldKey, fieldName, enumKey, x, y);
        }
        
        public string SaveFKField(string fk_mapData, string fieldKey, string fieldName, string enName, double x, double y) {
            return base.Channel.SaveFKField(fk_mapData, fieldKey, fieldName, enName, x, y);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CCFlowWord2007.CCFlow.SaveImageAsFileResponse CCFlowWord2007.CCFlow.CCFlowAPISoap.SaveImageAsFile(CCFlowWord2007.CCFlow.SaveImageAsFileRequest request) {
            return base.Channel.SaveImageAsFile(request);
        }
        
        public string SaveImageAsFile(byte[] img, string pkval, string fk_Frm_Ele) {
            CCFlowWord2007.CCFlow.SaveImageAsFileRequest inValue = new CCFlowWord2007.CCFlow.SaveImageAsFileRequest();
            inValue.img = img;
            inValue.pkval = pkval;
            inValue.fk_Frm_Ele = fk_Frm_Ele;
            CCFlowWord2007.CCFlow.SaveImageAsFileResponse retVal = ((CCFlowWord2007.CCFlow.CCFlowAPISoap)(this)).SaveImageAsFile(inValue);
            return retVal.SaveImageAsFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CCFlowWord2007.CCFlow.LoadFrmTempleteResponse CCFlowWord2007.CCFlow.CCFlowAPISoap.LoadFrmTemplete(CCFlowWord2007.CCFlow.LoadFrmTempleteRequest request) {
            return base.Channel.LoadFrmTemplete(request);
        }
        
        public string LoadFrmTemplete(byte[] fileByte, string fk_mapData, bool isClear) {
            CCFlowWord2007.CCFlow.LoadFrmTempleteRequest inValue = new CCFlowWord2007.CCFlow.LoadFrmTempleteRequest();
            inValue.fileByte = fileByte;
            inValue.fk_mapData = fk_mapData;
            inValue.isClear = isClear;
            CCFlowWord2007.CCFlow.LoadFrmTempleteResponse retVal = ((CCFlowWord2007.CCFlow.CCFlowAPISoap)(this)).LoadFrmTemplete(inValue);
            return retVal.LoadFrmTempleteResult;
        }
        
        public string LoadFrmTempleteFile(string file, string fk_mapData, bool isClear, bool isSetReadonly) {
            return base.Channel.LoadFrmTempleteFile(file, fk_mapData, isClear, isSetReadonly);
        }
        
        public string GetXmlData(string xmlFileName) {
            return base.Channel.GetXmlData(xmlFileName);
        }
        
        public string DoType(string dotype1, string v1, string v2, string v3, string v4, string v5) {
            return base.Channel.DoType(dotype1, v1, v2, v3, v4, v5);
        }
        
        public string SaveEn(string vals) {
            return base.Channel.SaveEn(vals);
        }
        
        public string FtpMethod(string doType, string v1, string v2, string v3) {
            return base.Channel.FtpMethod(doType, v1, v2, v3);
        }
        
        public string RequestSFTableV1(string uiBindKey) {
            return base.Channel.RequestSFTableV1(uiBindKey);
        }
        
        public string GenerFrm(string fk_mapdata, int workID) {
            return base.Channel.GenerFrm(fk_mapdata, workID);
        }
        
        public string CopyFrm(string fromMapData, string fk_mapdata, bool isClear, bool isSetReadonly) {
            return base.Channel.CopyFrm(fromMapData, fk_mapdata, isClear, isSetReadonly);
        }
        
        public string SaveFrm(string fk_mapdata, string xml, string sqls, string mapAttrKeyName) {
            return base.Channel.SaveFrm(fk_mapdata, xml, sqls, mapAttrKeyName);
        }
    }
}
