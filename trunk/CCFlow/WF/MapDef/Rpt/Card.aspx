﻿<%@ Page Language="C#" MasterPageFile="../WinOpen.master" AutoEventWireup="true" Inherits="WF_MapDef_WFRpt" Title=" Untitled Page " Codebehind="Card.aspx.cs" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
    <script language="JavaScript" src="../MapDef.js"></script>
    <script language="javascript" >
        function AddDtl(kv) {
            var b = window.showModalDialog('WFRptDtl.aspx?MyPK=' + kv, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
            window.location.href = window.location.href;
        }
        function DoReset(fk_flow, kv) {
            if (window.confirm('Are you sure ? ') == false)
                return;
            window.location.href = 'Card.aspx?FK_MapData=' + kv + '&FK_Flow=' + fk_flow + '&DoType=Reset';
        }
    
	function HelpGroup()
	{
	   var msg=' Field Grouping : Is to put together a similar field , Allowing users to operate more friendly .\t\n Such as : We designed a basic taxpayer information collection node .';
	   msg+=' Basic information when registering taxpayers , We can put basic information , Travel Information , Real Estate Information , Investor information packet .\t\n \t\n Packet format is :@ From the field name 1= Group Name 1@ From the field name 2= Group Name 2 ,\t\n Such as : Node information set ,@NodeID= Basic Information @LitData= Assessment Information .';
       alert( msg);
	}
	function DoGroupF( enName)
	{
	    var b = window.showModalDialog('../GroupTitle.aspx?EnName=' + enName, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
	}
	function DoGroupF( AddDtl)
	{
	    var b = window.showModalDialog('../GroupTitle.aspx?EnName=' + enName, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
	}
	function Insert(mypk,IDX)
    {
        var url = '../Do.aspx?DoType=AddF&MyPK=' + mypk + '&IDX=' + IDX;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function AddF(mypk)
    {
        var url = '../Do.aspx?DoType=AddF&MyPK=' + mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function AddTable(mypk)
    {
        var url = '../EditCells.aspx?MyPK=' + mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function CopyFieldFromNode(mypk)
    {
        var url = '../CopyFieldFromNode.aspx?DoType=AddF&FK_Node=' + mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GroupFieldNew(mypk)
    {
        var url = '../GroupField.aspx?RefNo=' + mypk + "&RefOID=0&DoType=FunList";
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GroupField(mypk, OID )
    {
        var url = '../GroupField.aspx?RefNo=' + mypk + "&RefOID=" + OID;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GroupFieldDel(mypk,refoid)
    {
        var url = '../GroupField.aspx?RefNo=' + mypk + '&DoType=DelIt&RefOID=' + refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
     
    function Edit(mypk,refno, ftype)
    {
        var url = '../EditF.aspx?DoType=Edit&MyPK=' + mypk + '&RefNo=' + refno + '&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function EditEnum(mypk,refno)
    {
        var url = '../EditEnum.aspx?DoType=Edit&MyPK=' + mypk + '&RefNo=' + refno;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
     function EditTable(mypk,refno)
    {
        var url = '../EditTable.aspx?DoType=Edit&MyPK=' + mypk + '&RefNo=' + refno;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    
	function Up(mypk,refoid,idx)
    {
        var url = '../Do.aspx?DoType=Up&MyPK=' + mypk + '&RefNo=' + refoid + '&ToIdx=' + idx;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        //window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
        window.location.href = window.location.href ;
    }
    function Down(mypk,refoid,idx)
    {
        var url = '../Do.aspx?DoType=Down&MyPK=' + mypk + '&RefNo=' + refoid + '&ToIdx=' + idx;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GFDoUp(refoid)
    {
        var url = '../Do.aspx?DoType=GFDoUp&RefOID=' + refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href ;
    }
    function GFDoDown(refoid)
    {
        var url = '../Do.aspx?DoType=GFDoDown&RefOID=' + refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function DtlDoUp(MyPK)
    {
        var url = '../Do.aspx?DoType=DtlDoUp&MyPK=' + MyPK;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href ;
    }
    function DtlDoDown(MyPK)
    {
        var url='../Do.aspx?DoType=DtlDoDown&MyPK='+MyPK;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    
    function Del(mypk,refoid)
    {
        if (window.confirm(' Are you sure you want to delete it ?') ==false)
            return ;

        var url = '../Do.aspx?DoType=Del&MyPK=' + mypk + '&RefOID=' + refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function Esc()
    {
        if (event.keyCode == 27)     
        window.close();
       return true;
    }

    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './../Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 40; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            if (sta == 'block') {
                document.getElementById(rowIdx + '_' + i).style.display = '';
            } else {
                document.getElementById(rowIdx + '_' + i).style.display = sta;
            }
        }
    }

    var isInser = "";

    function CopyFieldFromNode(mypk) {
        var url = '../CopyFieldFromNode.aspx?FK_Node=' + mypk;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = window.location.href;
    }

  function EditDtl(mypk, dtlKey) {
      var url = '../MapDtl.aspx?DoType=Edit&FK_MapData=' + mypk + '&FK_MapDtl=' + dtlKey;
      var b = window.showModalDialog(url, 'ass', 'dialogHeight: 600px; dialogWidth: 700px;center: yes; help:no;resizable:yes');
      //var b = window.showModalDialog(url, 'ass', 'dialogHeight: 800px; dialogWidth: 700px;center: yes; help: no;resizable:yes');
      window.location.href = window.location.href;
  }

  function MapDtl(mypk) {
      var url = '../MapDtl.aspx?DoType=DtlList&FK_MapData=' + mypk;
      var b = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no');
      window.location.href = window.location.href;
  }
</script>

	<base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" class="Table"  width='95%' height="500px" >
            <tr>
                <td valign="top"  align=center>
                <uc1:Pub ID="Pub1" runat="server" />
                </td>
            </tr>
        </table>
</asp:Content>