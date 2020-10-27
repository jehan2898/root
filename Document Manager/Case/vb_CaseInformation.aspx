<%@ Page Language="VB" AutoEventWireup="true" Inherits="Case_vb_CaseInformation"
    CodeFile="vb_CaseInformation.aspx.vb"  %>

<%--<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajc" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Case Information Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css"
        rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/CssAndJs/LinkSelector.css")%>' type="text/css"
        rel="stylesheet" />

    <script type="text/javascript" src="../CssAndJs/milonic_src.js"></script>

    <script type="text/javascript" src="../CssAndJs/mmenudom.js"></script>

    <script type="text/javascript" src="../CssAndJs/menu_data.js"></script>

    <script type="text/javascript" src="../CssAndJs/script.js"></script>

    <link rel="stylesheet" type="text/css" href="EasyMenu/Styles/obout_treeview2.css" />
    <link href="../CssAndJs/css.css" type="text/css" rel="stylesheet" />
    <link href="css/forms.css" type="text/css" rel="stylesheet" />
    <link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="EasyMenu/Styles/style.css" />

    <script src="../../js/scan/jquery.min.js" type="text/javascript"></script> 
    <script src="../../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../../js/scan/function.js" type="text/javascript"></script>
    <script src="../../js/scan/Common.js" type="text/javascript"></script>
    <link href="../../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="EasyMenu/Styles/js_TreeViewFunctions.js"></script>

    <script language="JavaScript" src="../CssAndJs/menu.js"></script>
    <script language="JavaScript" src="../CssAndJs/menu.js"></script>
	
	   <script language="javascript" type="text/javascript">
        document.onmousedown=disableclick;
        status="Right Click Disabled";
        function disableclick(e)
        {
          if(event.button==2)
           {
            
              e.preventDefault = true;
			   
			  return false;
             	
           }
        }

          
</script>
    <script language="JavaScript" src="../CssAndJs/JavaScriptClass.js"></script>
       <%-- <%            
            Dim ds As New DataSet
            ds = GetNodeDetails()
            Dim i As Integer
            i = ds.Tables(0).Rows.Count
            Dim szOutput As String = "<script> var arr = new Array('" + i.ToString + "');"
            Dim iCnt As Integer
            For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                szOutput = szOutput + "arr[" + iCnt.ToString() + "] = new Array(2);arr[" + iCnt.ToString() + "][1] = '" + ds.Tables(0).Rows(iCnt).ItemArray.GetValue(0).ToString() + "'; arr[" + iCnt.ToString() + "][2] = '" + ds.Tables(0).Rows(iCnt).ItemArray.GetValue(1).ToString() + "';"
            Next
            szOutput = szOutput + "</script>"
            Response.Write(szOutput)
        %>--%>
          
    <script>        
    
        function closeTypePage()
        {
            document.getElementById('divid4').style.visibility='hidden';
            document.getElementById('divid4').style.top='-10000px';
            document.getElementById('divid4').style.left='-10000px';
        }
    
        function RedirectSetOrder(Case_Id,cmpid,cmpName)
        {
            var WindowWidth = 800;
            var WindowHeight = 300;
            var url;
            var leftpos=screen.width/2-WindowWidth/2;
            var toppos=screen.height/2-WindowHeight/2;
            //var strApplication= '<% Dim str as String=Request.ApplicationPath %>';
            //var strApplicationName = '<%=str%>' ;
            url = './SetOrder.aspx?Case_Id='+Case_Id+'&Companyid='+cmpid+'&CompanyName='+cmpName ;
            //alert(url);
            window.open(url,'SetOrder','toolbar=0,location=1,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width='+WindowWidth+',height='+WindowHeight+',left='+leftpos+',top='+toppos).focus();
        }

        function chkgotxt()
        {
            if(document.getElementById("txtCaseSearch").value=="")
            {
                alert("Please Enter the Case No!!!");
                return false;
            }
        }
        
        function AlphaLowerUpperNumeric()
		{								
			if((event.keyCode >= 65 && event.keyCode <= 90)||(event.keyCode >= 97 && event.keyCode <= 122 ) || (event.keyCode >= 48 && event.keyCode <= 57)||(event.keyCode == 95)||(event.keyCode == 45))
			{
				event.keyCode = event.keyCode ;
				return true;
			} 
			else
			{
				event.keyCode = 0;
				return false;
			}
		}        
    </script>

    <style type="text/css">
			body {font-family: Verdana; font-size: XX-Small; margin: 0px; padding: 20px}
			.title {font-size: X-Large; padding: 20px; border-bottom: 2px solid gray;}
	</style>
    <link href="EasyMenu/Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #ffffff;"  oncontextmenu="return false" >
   <form id="Form1" method="post" runat="server"  oncontextmenu="return false">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="20%" style="height: 28px">
                                <b>Patient Name : </b>
                                <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                                
                            </td>
                            <td align="right" style="height: 28px">
                                Billing Company :
                                <% =CType(Session("BILLING_COMPANY_OBJECT"),Bill_Sys_BillingCompanyObject).SZ_COMPANY_NAME  %>
                            </td>
                            <td width="10px" style="height: 28px">
                            </td>
                            <td style="height: 28px">
                                As :
                                <%=CType(Session("USER_OBJECT"),Bill_Sys_UserObject).SZ_USER_NAME %>
                            </td>
                            <td style="height: 28px" id="tdJumpto" runat="server" visible="true">
                                <span>Jump To File No</span>
                                <input id = "txtCaseSearch" runat = "server" type ="text" value ="" style ="width:40px; height:18px"/>
                                
                                <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" CssClass="Buttons" OnClick="btnGo_Click" OnClientClick = "javascript:return chkgotxt()" />
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="height: 25px; background: #eee8db" class="black11arial">
                                <b>&nbsp;Document Manager</b>&nbsp;&nbsp;
                                 <asp:Label ID="lblMsg" runat ="server" Text = "Invalid Case ID" Font-Bold="true" Visible = "false"></asp:Label>
                                 <asp:label ID = "lblNodeID" runat ="server" Text = "" Font-Bold = "true" Visible = "false"></asp:label>
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                    </table>
                    
                        <table width="100%">
                            <tr>
                                <td style="height: 25px; background: #eee8db" class="black11arial">
                                    <b>
                                        <%=getCaseInformation()%>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 27px">
                                    <asp:Button Width="300px" ID="btnLoadWithNodes" CssClass="button" Text=" ... Show all nodes ... "
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                        <input value="<%=Session("Case_ID")%>" type="hidden" id="hCase" name="hCase" class="box"
                            style="width: 210px; height: 0px" />
                        <input value="<%=Session("User_Name")%>" type="hidden" id="hUserName" name="hUserName"
                            class="box" style="width: 210px; height: 0px" />
                        <input value="<%=Session("LastAction")%>" type="hidden" id="hidLA" name="hidLA" class="box"
                            style="width: 210px; height: 0px" />
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="96%">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="20%" valign="top">
                                                            <div style="height: auto; overflow: auto;">
                                                                <table width="100%" cellpadding="2" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td>
                                                                        <input name="button" type="button" class="box" id="btnSelectMultipleNodes" onclick="selectMultipleNodes()"
                                                                            value="Email Files" runat="server" style="visibility: hidden" />
                                                                    </td>
                                                                    <td>
                                                                            <input name="button" type="button" class="box" id="btnMergeNode" onclick="mergeNode()"
                                                                            value="PDF Merge" runat="server" style="visibility: visible" />
                                                                    </td>
                                                                </tr>
                                                                    <tr>
                                                                        <td id="tdCaseID" runat="server">
                                                                            Case ID:-
                                                                            <asp:DropDownList ID="ddlCaseID" runat="server" CssClass="combox" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlCaseID_SelectedIndexChanged">
                                                                            </asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="TreeView" EnableViewState="true" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" style="height: auto; width: 20%;" id="TblMenu" runat="server">
                                                                                <tr>
                                                                                    <td valign="top" style="height: 30px; width: 40px;">
                                                                                        &nbsp;<oem:EasyMenu  ID="EasyMenu1" runat="server" StyleFolder="EasyMenu/Styles" Align="Advanced"
                                                                                            UseIcons="true" IconsFolder="EasyMenu/Icons" Width="150px">
                                                                                            <Components>
                                                                                                <oem:MenuItem ID="miAddNode" runat="server" InnerHtml="&#160;Add New Node" Icon="folder.jpg"
                                                                                                    OnClientClick="showhideNode()">
                                                                                                </oem:MenuItem>
                                                                                                <%--<oem:MenuItem ID="miAddFile" runat="server" InnerHtml="&#160;Upload File" Icon="new.gif"
                                                                                                    OnClientClick="showhideFile()">
                                                                                                </oem:MenuItem>--%>
                                                                                                <%--<oem:MenuItem ID="miScanDocument"  runat="server" InnerHtml="&#160;Scan File" Icon="scan.jpg"
                                                                                                    OnClientClick="GetWebScanUrl()">
                                                                                                </oem:MenuItem>--%>
                                                                                                <oem:MenuItem ID="MenuItem1" runat="server" InnerHtml="&#160;Scan / Upload" Icon="scan.jpg"  
                                                                                            OnClientClick="GetWebScanUpload()">
                                                                                        </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miMerge" runat="server" InnerHtml="&#160;Re-Merge" Icon="pg_def_copy_ico.gif" 
                                                                                                    OnClientClick="Remerge()" >
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miCopytoCase" runat="server" InnerHtml="&#160;Copy to Case" Icon="pg_def_copy_ico.gif"
                                                                                                    OnClientClick="CopyToCaseSelectedNode()">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miDeleteNode" runat="server" InnerHtml="&#160;Delete Node" Icon="new.gif"
                                                                                                    OnClientClick="removeSelectedNode()">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miCopy" runat="server" InnerHtml="&#160;Copy" Icon="pg_def_copy_ico.gif"
                                                                                                    OnClientClick="CopySelectedNode()">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miPaste" runat="server" InnerHtml="&#160;Paste" Icon="paste.jpg"
                                                                                                    OnClientClick="PasteSelectedNode()" Disabled="true">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miEdit" runat="server" InnerHtml="&#160;Edit" OnClientClick="EditSelectedNode()"
                                                                                                    Icon="paste.jpg">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miEmailFile" InnerHtml="&#160;Email File" Icon="E_Mail.png" runat="server"
                                                                                                    OnClientClick="SendMail()">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miFax" InnerHtml="&#160;Fax File" Icon="faxicon.gif" runat="server"
                                                                                                    OnClientClick="SendFax()">
                                                                                                </oem:MenuItem>
                                                                                               
                                                                                                <oem:MenuItem ID="menuItem7" OnClientClick="try {targetEl.parentNode.firstChild.firstChild.onclick();} catch (e) {}"
                                                                                                    InnerHtml="&#160;Expand/Collapse Node" runat="server">
                                                                                                </oem:MenuItem>
                                                                                                <oem:MenuItem ID="miSearch" InnerHtml="&#160;Search" runat="server" Icon="ico_search12.GIF"
                                                                                                    OnClientClick="SearchOCRData()" Disabled="true">
                                                                                                </oem:MenuItem>
                                                                                               <%-- <oem:MenuItem ID="miRenameNode" InnerHtml="&#160;Rename Node" runat="server"
                                                                                                    OnClientClick="showhideRename()" Disabled="false">
                                                                                                </oem:MenuItem>--%>
                                                                                                <oem:MenuItem ID="miPrint" InnerHtml="&#160;Print" Icon="print.gif" runat="server"
                                                                                                    OnClientClick="SendPrint()" Disabled="true">
                                                                                                </oem:MenuItem>
                                                                                            </Components>
                                                                                            <CSSClassesCollection>
                                                                                                <oem:CSSClasses Component="easyMenuItem" ComponentContentCell="easyMenuItemContentCell"
                                                                                                    ComponentContentCellOver="easyMenuItemContentCellOver" ComponentIconCell="easyMenuItemIconCell"
                                                                                                    ComponentIconCellOver="easyMenuItemIconCellOver" ComponentOver="easyMenuItemOver"
                                                                                                    ComponentSubMenuCell="easyMenuItemSubMenuCell" ComponentSubMenuCellOver="easyMenuItemSubMenuCellOver"
                                                                                                    ObjectType="OboutInc.EasyMenu_Pro.MenuItem" />
                                                                                            </CSSClassesCollection>
                                                                                        </oem:EasyMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <asp:GridView ID="GridView1" runat="server">
                                                            </asp:GridView>
                                                        </td>
                                                        <td style="width: 1%" valign="top">
                                                            <img src="EasyMenu/Icons/line.GIF" height="610" /></td>
                                                        <td style="width: 1%" valign="top">
                                                        </td>
                                                        <td style="width: 79%" valign="top">
                                                            <div id="divAddNode" style="display: none">
                                                                <table id="tblAddNode" border="0" style="width: 22%; height: auto" runat="server">
                                                                    <tr>
                                                                        <td align="left" class="ver11blue" style="width: 251px" valign="top">
                                                                            Add Node:-&nbsp;
                                                                            <input name="text" type="text" class="box" runat="server" id="sNewNodeText" style="width: 210px;
                                                                                height: 18px" value="" />
                                                                            <input type="hidden" id="Text1" name="Text12" runat="server" class="box" value=""
                                                                                style="width: 210px; height: 18px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 251px; height: 22px;" valign="top">
                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                                            <input name="button" type="button" class="box" id="btnAddNode" onclick="addNode()"
                                                                                value="Add Node" runat="server" />
                                                                            &nbsp;&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divAddFile" style="display: none">
                                                                <table id="tblAddFile" border="0" style="width: 22%; height: auto" runat="server">
                                                                    <tr>
                                                                        <td align="left" class="ver11blue" style="width: 251px" valign="top">
                                                                            Upload File:- &nbsp;
                                                                            <input name="file" type="file" class="box" id="fileUpload" style="width: 299px" enableviewstate="true"
                                                                                runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 251px; height: 22px;" valign="top">
                                                                            <asp:CheckBox ID="chkIsFileEncrypted" runat="server" Visible="False" />
                                                                            <input name="btnAddFile" type="button" class="box" id="btnAddFile" onclick="upLoadFile()"
                                                                                value="Upload File" runat="server" /></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divmyiframe" style="display: none">
                                                                <iframe name="imageframe" id="myiframe" runat="server" frameborder="0" style="height: 1000px;
                                                                    width: 890px;" />
                                                            </div>
                                                            <div id="divCopyToCase" style="display: none">
                                                                <table id="tblCopyToCase" border="0" style="width: 22%; height: auto" runat="server">
                                                                    <tr>
                                                                        <td align="left" class="ver11blue" style="width: 251px" valign="top">
                                                                            Copy To Case:-&nbsp;
                                                                            <input name="CopyToCase" type="text" class="box" runat="server" id="txtCopyToCase"
                                                                                style="width: 210px; height: 18px" value="" />
                                                                            <asp:Label ID="lblMessage" runat="server" BackColor="White" ForeColor="Red" Visible="False"
                                                                                Width="208px"></asp:Label>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 251px; height: 22px;" valign="top">
                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                                            <input name="CopyToCase" type="button" class="box" id="btnCopyToCase" value="Copy To Case"
                                                                                runat="server" style="width: 94px" />
                                                                            &nbsp;&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <%--<div id ="divRenameNode" style="display: none">
                                                                <table id="tblRenameNode" border="0" style="width: 22%; height: auto" runat="server">
                                                                    <tr>
                                                                        <td align="left" class="ver11blue" style="width: 251px" valign="top">
                                                                            Rename Node:-&nbsp;
                                                                            <input name ="RenameNode" type = "text" class="box" runat="server" id="txtRenameNode" style="width: 210px; height: 18px" value="" onkeypress = "javascript:return AlphaLowerUpperNumeric(this.event)" />
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                    <tr>                                           
                                                                    <td align="right" style="width: 251px; height: 22px;" valign="top" >
                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                                        
                                                                                <asp:Button ID= "btnRenameNode" runat = "server" Text ="Rename Node" width = "94px" OnClick = "btnRenameNode_Click" OnClientClick = "javascript:return renameNode();" />
                                                                                &nbsp;&nbsp;
                                                                               
                                                                    </td>               
                                                                    </tr>
                                                                </table>
                                                            </div>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="2%">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%  Dim strurl As String = ConfigurationSettings.AppSettings("DesktopDefaultUrl")
                                                    Dim stripaddress As String = ConfigurationSettings.AppSettings("DocumentManagerServer")
                                                    Dim szPath = stripaddress + strurl
                                                    txtdocumentStr.Value = szPath
                                                %>
                                                <asp:HiddenField ID="txtdocumentStr" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hfUrl" runat="server" />
                                                <asp:HiddenField ID="hfCompanyId" runat="server" />
                                                <asp:HiddenField ID="hfAllId" runat="server" />
                                                <asp:HiddenField ID="hfCaseNo" runat="server" />
                                                <asp:HiddenField ID="hfPatientName" runat="server" />
                                                <asp:HiddenField ID="hfcompanyname" runat="server" />
                                                 <asp:HiddenField ID="hdnMenu" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="div2" style="visibility: hidden">
                                         <asp:HiddenField ID="hdnids" runat="server"  />
                                        <asp:HiddenField ID="hdnRemerge" runat="server" />
                                        <asp:Button ID="btnREMergePDF" runat="server" CssClass="box" Text="Button" Visible="true" />
                                    <asp:Button ID="btnMergePDF" runat="server" CssClass="box" Text="Button" Visible="true" />
                                        <input type="hidden" id="nodeid" name="nodeid" runat="server" class="box" value="" />
                                        <asp:Button ID="Button3" runat="server" CssClass="box" Text="Button" Visible="true" />
                                        <input type="hidden" id="Hidden1" name="Text1" runat="server" class="box" value="" />
                                        <input type="hidden" id="hidnSelected" name="hidnSelected" runat="server" class="box"
                                            value="" />
                                        <input type="hidden" id="hidnFileName" name="hidnFileName" runat="server" class="box"
                                            value="" />
                                        <input type="hidden" id="selectedID" name="selectedID" runat="server" class="box"
                                            value="" />
                                        <asp:HiddenField ID="HiddenField1" EnableViewState="true" runat="server" />
                                        <asp:HiddenField ID="hdldelete" runat="server" EnableViewState="true" />
                                        <input type="hidden" id="lblSession" name="lblSession" runat="server" value="" />
                                        <asp:Button ID="printButton" runat="server" Text="Button" />&nbsp;
                                        <asp:Button ID="btnPasteNode" runat="server" Text="Paste" />
                                        <input type="hidden" id="SourceID" name="SourceID" runat="server" class="box" value="" />
                                        <asp:Button ID="btnEditNode" runat="server" Text="Edit"></asp:Button>
                                        <asp:Button ID="btnDeleteNode" runat="server" Text="Delete"></asp:Button>
                                        
                                    </div>
                                    <%--<busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />--%>
                                </td>
                            </tr>
                        </table>
                   
                </td>
            </tr>
            <asp:TextBox ID="hidPath" Text="" Visible="false" runat="server"></asp:TextBox></table>
            
            <%--<div id="divSetorder" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                border-left: silver 1px solid; border-bottom: silver 1px solid;">
                <div style="position: relative; text-align: right; background-color: #8babe4;">
                    <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
                </div>
                <iframe id="frameSetOrder" src="" frameborder="0" height="470px" width="850px"
                    visible="false"></iframe>
            </div>--%>
             <div id="divid4" style="position:inherit; width: 550px; height: 350px; background-color: #DBE6FA;
                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                border-left: silver 1px solid; border-bottom: silver 1px solid;">
                <div style="position: relative; text-align: right; background-color: #8babe4;">
                    <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
                </div>
                <iframe id="frameeditexpanse1" runat="server" src="" frameborder="0" height="450px" width="950px"></iframe>
            </div>
             <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
                 <style>
                     .not-active {
   pointer-events: none;
   cursor: default;
   color: #ff0000;
   font-weight:bold;
}

                     /* unvisited link */
.not-active:link {
    color: red;
}

.active {

}
                 </style>
</div>                  
     </form>
</body>
</html>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
CssClass="btn btn-primary"
<script language="JavaScript" src="EasyMenu/Styles/js_TreeViewEvents.js"></script>


