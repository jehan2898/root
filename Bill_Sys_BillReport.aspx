<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillReport.aspx.cs" Inherits="Bill_Sys_BillReport" Title="Welcome" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
     function ascii_value(c){
             c = c . charAt (0);
             var i;
             for (i = 0; i < 256; ++ i)
             {
                  var h = i . toString (16);
                  if (h . length == 1)
                    h = "0" + h;
                   h = "%" + h;
                  h = unescape (h);
                  if (h == c)
                    break;
             }
             return i;
        }
    function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
            
            
         }
    </script>
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b>Bill Report By Doctor </b>
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    From Date&nbsp; &nbsp;</td>
                                                <td style="width: 22%">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxcontrol:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Bill Status&nbsp;</td>
                                                <td style="width: 22%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />
                                                    &nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Specialty
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="170px" autopost_back="True" onextenddropdown_selectedindexchanged="BindProcedureCodeList" ></cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="ContentLabel" style="width: 15%; height: 18px;" >
                                                    <asp:CheckBox ID="chkVerificationsent" runat="Server" Text="VerificationSent" />
                                                    <asp:CheckBox ID="chkVerificationreceived" runat="Server" Text="VerificationReceived" />
                                                    <asp:CheckBox ID="chkdenail" runat="Server" Text="Denial" />
                                                    <asp:CheckBox ID="chkPaidfull" runat="Server" Text="PaidInFull" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Doctor Name
                                                </td>
                                                <td style="width: 22%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                    &nbsp;
                                                </td>
                                                <td td class="ContentLabel" style="width: 15%">
                                                    Procedure Codes
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    <asp:ListBox ID = "lstProcedureCode" runat ="server" Width="198px"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                               <td style="width: 22%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="65%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                               <%-- <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="text-align: right;">
                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                        </div>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" OnItemCommand="grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName = "View" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%>' Visible="false"></asp:LinkButton>
                                                         <a target="_self" href='Bill_Sys_ViewBillRecordDetails.aspx?flag=View&Status=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_Id")%>&speciality=<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Specialty"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PROVIDER" HeaderText="Provider">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILL COUNT" HeaderText="Count Of Bills"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SUM" HeaderText="Sum Of Amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_BILL_STATUS_ID" HeaderText="StatusID" Visible="false"></asp:BoundColumn>
                                                  <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="DoctorID" Visible = "false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                            Total Amount : <asp:Label ID="lblTotal" runat="server" />
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    </div>
</asp:Content>
