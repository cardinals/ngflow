﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="CCFlow.AppDemoLigerUI.Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery/lib/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../WF/Scripts/easyUI/easyloader.js" type="text/javascript"></script>
    <script src="../WF/Scripts/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../WF/Scripts/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../WF/Scripts/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/WF/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/WF/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="js/AppData.js" type="text/javascript"></script>
    <script src="js/startPage.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div id="pageloading">
    </div>
    <div data-options="region:'center'" border="false" style="margin: 0; padding: 0;
        overflow: hidden;">
        <div id="maingrid" fit="true" fitcolumns="true" style="margin: 0; padding: 0;" class="easyui-datagrid">
        </div>
        <div id="easyui">
        </div>
        <div id="showHistory">
            <div id="historyGrid" style="margin: 0; padding: 0;">
            </div>
        </div>
        <div id="divTitle" style="margin: 0; padding: 0;">
            <div id="panel" style="margin-top: 30px; padding: 0;">
                 Title Name :<input type="text" id="TB_Title" name="TB_Title" style="height: 25px; width: 320px;" />
            </div>
        </div>
        <div id="flowPicDiv">
            <img alt=" Flow chart " id="FlowPic" src="" onerror="this.src='/DataUser/ICON/CCFlow/LogBig.png'" />
        </div>
    </div>
    </form>
</body>
</html>
