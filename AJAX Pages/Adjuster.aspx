<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adjuster.aspx.cs" Inherits="AJAX_Pages_Adjuster" %>


<%@ Register Src="~/UserControl/PatientInformation.ascx" TagName="UserPatientInfoControl"  TagPrefix="UserPatientInfo" %>
<%--<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>--%>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Src="~/UserControl/Bill_Sys_AssociateCases.ascx" TagName="Bill_Sys_AssociateCases"
    TagPrefix="ASC" %>
<%@ Register Src="~/UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript">
    function validation()
    {
      //  alert('');
        if(document.getElementById("<%= txtAdjusterPopupName.ClientID %>").value == '')
        {
            alert('Please enter adjuster name!');
            document.getElementById("<%= txtAdjusterPopupName.ClientID %>").focus();
         return false;
        }else{
            return true;
        }
    }
        function GetAdjusterValue(object, arg)
       {
           document.getElementById("<%=hdadjusterCode.ClientID %>").value = arg.get_value();
       }
       //Nirmalkumar
       function POMConformation()
        {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute';
            document.getElementById('div1').style.left = '360px';
            document.getElementById('div1').style.top = '250px';
            document.getElementById('div1').style.visibility = 'visible';
            return false;
        }
       //End
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout=50000>
    </asp:ScriptManager>
    <asp:Panel ID="pnlAddress" runat="server" BackColor="white" BorderColor="steelblue"
            Width="100%">
    <table>
    <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
            
                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
          
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
    </tr>
        
        <tr>
            <td class="ContentLabel">
               Adjuster Name:
             </td>
             <td >
             <%--<ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                 DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtAdjusterPopupName" 
                 ServiceMethod="GetAdjuster" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetAdjusterValue">
             </ajaxToolkit:AutoCompleteExtender>--%>
              <%--<asp:TextBox ID="txtAdjusterPopupName" runat="Server" autocomplete="off"  OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>--%>
              <asp:TextBox ID="txtAdjusterPopupName" runat="Server" MaxLength="50" ></asp:TextBox>
             </td>
             <td class="ContentLabel" >
                 Address:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupAddress" runat="server" MaxLength="50"></asp:TextBox>
            </td>
             
        </tr>
        <tr>
             <td class="ContentLabel" >

                 City:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupCity" runat="server"  MaxLength="50"></asp:TextBox></td>
             <td class="ContentLabel" >
                 State:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupState" runat="server"  MaxLength="50"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="ContentLabel" >
                 Zip:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupZip" runat="server"  MaxLength="50"></asp:TextBox></td>
             <td class="ContentLabel" >
                 Phone Number:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupPhone" runat="server"  MaxLength="50"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="ContentLabel" >
                 Extension:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupExtension" runat="server"  MaxLength="50"></asp:TextBox></td>
             <td class="ContentLabel" >
                 FAX:</td>
             <td >
                 <asp:TextBox ID="txtAdjusterPopupFax" runat="server"  MaxLength="50"></asp:TextBox></td>
         </tr>
         <tr>
         
             <td class="ContentLabel" >
                 Email:</td>
             <td colspan="3" >
                 <asp:TextBox ID="txtAdjusterPopupEmail" runat="server" Width="100%"  MaxLength="50"></asp:TextBox></td>
             
             
         </tr>
         <tr>
              <td class="ContentLabel" colspan="4" align="center">
                  &nbsp;
                  <asp:Button ID="btnSaveAdjuster" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                      OnClick="btnSaveAdjuster_Click"  OnClientClick ="return validation()"/>
                      <asp:Button ID="btnupdate" runat="server" Text="Update" Width="80px" CssClass="Buttons" OnClick="btnupdate_click"/>
                      <asp:Button ID="btnClearAdjuster" runat="server" Text="Clear" Width="80px" CssClass="Buttons" OnClick="btnClearAdjuster_click"/>
                  <br />
              </td>
          </tr>
          <tr>
              <td class="ContentLabel" colspan="4" align="center">
                 
                  <asp:CheckBox ID="ChkAddToCase" runat="server" 
                      Text="Assign adjuster to current case" />
                 
              </td>
          </tr>
    </table>
    <%--Nirmalkumar--%>
  <%--  <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%;
        height: 150px; background-color: White; visibility: hidden; border-right: #b4dd82 2px solid;
        border-top: #b4dd82 2px solid; border-left: #b4dd82 2px solid; border-bottom: #b4dd82 2px solid;
        text-align: center;">
        <div style="position: relative; width: 100%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #b4dd82; left: 0px; top: 0px;">
            MSG
            
        </div>--%>
        <%--<div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4; left: 0px; top: 0px;">
            <a onclick="CancelErrorMassage();" style="cursor: pointer;" title="Close">X</a>
        </div>--%>
        <%--<br />
        <br />        
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2"  runat="server" style="font-size:10pt;font-family:"></span></div>
     <br />
        <br />
        <div style="text-align: center;">--%>
       <%-- <asp:Button ID="Button1"   style="width: 15%;" runt="server" Text="Ok" OnClick="btnOK_Click" />
          <asp:Button ID="btnYes" runat="server"  OnClick="btnYes_Click"
                                            Text="Yes" Width="80px" />
         <asp:Button ID="btnNo" runat="server"  OnClick="btnNo_Click"
                                            Text="No" Width="80px" /> 
   
   --%>
      <asp:HiddenField ID="hdadjusterCode" runat="server" />
       <asp:HiddenField ID="hdacode" runat="server" />
       <asp:HiddenField  id="hdfadjname" runat="server" />
        <asp:TextBox ID="txtCompanyID" runat="server" Style="visibility:hidden;"></asp:TextBox>
         <asp:TextBox ID="Link" runat="server" Style="visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="txtCaseID" runat="server" Style="visibility:hidden;"></asp:TextBox>
        <%--END--%>
        </asp:Panel>
      
    </form>
</body>
</html>
