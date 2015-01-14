﻿<%@ Page Title="" Language="C#" MasterPageFile="SiteEUI.Master" AutoEventWireup="true"
    CodeBehind="SearchEUIReadOnly.aspx.cs" Inherits="CCFlow.WF.Rpt.SearchEUIReadOnly" %>

<%@ Register Src="UC/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/FrmReportFieldReadOnly.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="pageloading">
    </div>
    <div region="center" border="true" style="margin: 0; padding: 0; overflow: hidden;">
        <div id="tb" style="padding: 3px;">
            <div id="Div1" runat="server" style="height: 30px;">
                <div style="float: left;">
                    <uc2:ToolBar ID="ToolBar1" runat="server" Text="df" />
                    <a id="querybtn" href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'"
                        runat="server" onserverclick="ToolBar1_ButtonClick"> Inquiry </a>
                </div>
                <div class="datagrid-btn-separator">
                </div>
                <a id="A1" href="#" style="float: left;" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-config'" 
                    onclick="SetEntity()"> Set up </a>
            </div>
        </div>
        <table id="ensGrid" fit="true" fitcolumns="true" toolbar="#tb" class="easyui-datagrid">
        </table>
    </div>
    <input type="hidden" id="EnNo" value="<%=FK_MapData %>" />
</asp:Content>
