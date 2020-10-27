<%@ Page Language="vb" AutoEventWireup="false" Inherits="RTFEditter" CodeFile="RTFEditter.aspx.vb"
    EnableEventValidation="false" AspCompat="True" %>
<%@ Register TagPrefix="cc1" Namespace="SubSystems.WebTer" Assembly="WebTer" %>
<%@ Register TagPrefix="ccm" Namespace="SubSystems.WebTerMenu" Assembly="WebTerMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <style type="text/css">
          .ClsMenu { font='9pt Arial'; text-decoration:'none'; border:'none'; margin='1px'};
          .ClsMenuOver { font='9pt Arial'; text-decoration:'none'; margin='none'; 
                         border-top:'1px solid white'; 
                         border-left:'1px solid white'; 
                         border-right:'1px solid gray'; 
                         border-bottom:'1px solid gray'; };
          .ClsMenuSelect { font='9pt Arial'; text-decoration:'none'; margin='none';
                         border-top:'1px solid gray'; 
                         border-left:'1px solid gray'; 
                         border-right:'1px solid white'; 
                         border-bottom:'1px solid white'; };
          
          .ClsShow { visibility:visible }
          .ClsHide { visibility:hidden }

       </style>
</head>
<body onload="LoadPage()" onresize="ResizeObjects()" onbeforeunload="UnloadPage()"
    ms_positioning="GridLayout" style="background-color: Gray">
    <asp:ScriptManager ID="MasterScriptManager1" runat="server"></asp:ScriptManager>
    <script language="javascript" event="ControlCreated()" for="WebTer1">
          ID_SUPSCR_ON          =652;
          ID_SUBSCR_ON          = 653;
       
          ter=Form1.WebTer1.object;  // short cut variable
    
          //window.status = "Page is loaded!";
          
          // Comment out example of adding or removing toolbar icons
          //ter.TerAddToolbarIcon(0,0,ID_SUPSCR_ON,"http://localhost/DmoTer/images/icon1.bmp","superscript"); 
          //ter.TerAddToolbarIcon(0,0,ID_SUBSCR_ON,"http://localhost/DmoTer/images/icon2.bmp","subscript");
          //ter.TerHideToolbarIcon(23,true);  // TLB_HELP = 23

          //ter.TerRecreateToolbar(true);
        
          // Example of disabling a speedkey
          //ter.TerEnableSpeedKey(ID_CHAR_STYLE,0);  // disable the original Alt+1 keycommand
    </script>
    <script type="text/javascript">
     function OpenFile()
     {
   
            var bname = navigator.appName; 
	        if(bname == "Netscape")
	        {
               alert('Template manager requires Internet Explorer or another browser which supports ActiveX controls.')
               return false;
            }            
            var listbox=document.getElementById("<%=drpReferral.ClientID%>");
            //var count = 0;   
            var curText = listbox.options[listbox.selectedIndex].text;        
            var curValue = listbox.options[listbox.selectedIndex].value;
                   //alert(curText + ' ' + curValue);                 
            window.open('../tm/ReferralRTFEditter.aspx? path = ' + curValue + '&Name =' + curText, 'Referral_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=1,status=no,scrollbars=0'); 
            

//            if(count == 0)
//            {
//                alert('select one template from list.');
//            }
            return true;
     }    
    </script>

    <script type="text/javascript" language="JavaScript">
<!--
function success()
{
    alert("Your template was successfully modified and saved to the server");
    //window.opener.location.href = window.opener.location.href;
    window.close();
}
function failed()
{
    alert("Your template cannot be saved!");
   // window.opener.location.href = window.opener.location.href;
    window.close();
}
function OpenSign()
{
    //window.open('../Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=350,height=400'); 
    //window.location.href ='../Signature.aspx';
      //  window.opener.location.href = window.opener.location.href;
   // window.close();
   document.getElementById("data").value=WebTer1.Data;
    return true;
}
//-->
    </script>

    <script language="javascript">
        function getdata()
        {
            document.getElementById("data").value=WebTer1.Data;
        }
    </script>

    <!-- Declare TE constants -->

    <script type="text/javascript" language="javascript" src="ter_var.js"></script>

    <!-- TE script -->

    <script type="text/javascript" language="javascript" src="ter.js"></script>

    <script language="javascript" event="ControlCreated()" for="WebTer1">
           InitTE();         // initialize the control
    </script>

    <!-- *****************************************************************************************
               Check browser 
        ********************************************************************************************* -->
    <table id="BrowserCheck" style="border-width: 1px; border-style: solid; height: 20px;
        width: 755px; z-index: 1; left: 0px; position: absolute; top: 0px; visibility: hidden;
        background-color: Gray">
        <tr>
            <td align="center">
                This browser not supported.
                <br />
                This application requires <b>Internet Explorer</b> or another browser which support
                ActiveX controls.
            </td>
            <tr>
    </table>
    <!-- *****************************************************************************************
               Build top menu
        ********************************************************************************************* -->
    <table id="TopMenu" style="border-width: 0px; border-style: none; height: 20px; width: 755px;
        z-index: 113; left: 0px; position: absolute; top: 0px">
        <tr style="height:10px">
            <td>
                <form id="form1" runat="server">
                    <table style="font-size:smaller;font-family:Verdana;height:10px">
                        <tr>
                            <td>
                                Document Name:
                                <asp:TextBox ID="Txtfile" runat="server"></asp:TextBox>
                            
                                Save as:
                                <asp:DropDownList ID="drpSaveAsType" runat="server">
                                    <asp:ListItem Text="PDF" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="RTF" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;
                                Referral:
                                <asp:DropDownList ID="drpReferral" runat="server">
                                </asp:DropDownList>&nbsp;
                                <a id="A1" href="#" runat="server" onclick="OpenFile()" style="font-size:8pt">Generate Referral</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="data" runat="server" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="getdata()" Font-Size="8pt" />
                            
                                <%--<asp:UpdatePanel ID="update" runat="server">
                                    <ContentTemplate>--%>
                                        <asp:Button ID="btnSign" runat="server" OnClientClick="return OpenSign();" Text="Doctor's Sign"
                                            OnClick="btnSign_Click" Font-Size="8pt" />
                                        <asp:Button ID="btnpatientSign" runat="server" OnClientClick="return OpenSign();"
                                            Text="Patient's Sign" OnClick="btnPatientSign_Click" Font-Size="8pt" />
                                        <asp:Button ID="btnDiagnosysCode" runat="server" OnClientClick="return OpenSign();"
                                            Text="Diagnosis Code" OnClick="btnDiagnosys_Click" Font-Size="8pt" />
                                        
                                        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                
                            </td>
                        </tr>
                    </table>
                </form>
            </td>
        </tr>
           <tr>
            <td style="float:left;">
            
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_FILE)"   onmouseout="ExitMenu(this)">File</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_EDIT)"   onmouseout="ExitMenu(this)">Edit</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_VIEW)"   onmouseout="ExitMenu(this)">View</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_INSERT)" onmouseout="ExitMenu(this)">Insert</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_FONT)"   onmouseout="ExitMenu(this)">Font</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_PARAGRAPH)" onmouseout="ExitMenu(this)">Paragraph</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_TABLE)"  onmouseout="ExitMenu(this)">Table</span>&nbsp;&nbsp;&nbsp;
                <span class="ClsMenu" onmouseover="OverMenu(this)"; onclick="ShowMenu(this,MENU_OTHER)"  onmouseout="ExitMenu(this)">Other</span>&nbsp;&nbsp;&nbsp;
            </td> 
        </tr>
        <tr>
            <td>
                <cc1:WebTer ID="WebTer1" Codebase="toc17.cab#version=17,0,0,0" BorderWidth="0px"
                    BorderStyle="none" Height="80px" Width="755px" WordWrap="true" PageMode="true"
                    PrintViewMode="true" FittedView="false" ShowStatusBar="true" ShowRuler="true"
                    ShowVertRuler="true" ShowToolBar="true" BorderMargin="false" RTFOutput="true"
                    ReadOnlyMode="false" VertScrollBar="true" HorzScrollBar="true" TerKey="" runat="server" />
            </td>
        </tr>
       
    </table>
</body>
</html>
