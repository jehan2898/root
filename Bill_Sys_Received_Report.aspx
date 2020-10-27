<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Received_Report.aspx.cs" Inherits="Bill_Sys_Received_Report"
    Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        
           function SelectAll(ival)
       {
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdReceivedeport");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
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
         
          function ConfirmDelete()
        {
             var msg = "Do you want to Revert?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
        
        
          function ChekOne()
       {
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdReceivedeport");	
            var k=0
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked==true)
			        {    k=1;
			            return ConfirmDelete();
			        
			        }
			    }			
		    }
		    if(k==0)
		    {
		        alert('Select Record');
		        return false;
		    }
       }
    </script>

    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
        <table width="100%">
            <tr>
                <td align="center" width="100%" style="height: 25px">
                    <asp:Label ID="lblMsg" runat="server" CssClass="message-text"></asp:Label>
                </td>
            </tr>
        </table>
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
                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                            </asp:ScriptManager>
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                        <b>Report Received </b>
                                                        <%-- <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Date&nbsp; &nbsp;</td>
                                                    <td style="width: 35%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        To Date&nbsp;
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                        <asp:Label ID="lblOffice" runat="server">Office</asp:Label>&nbsp;
                                                    </td>
                                                    <td style="width: 35%; height: 18px;">
                                                        &nbsp;<asp:DropDownList ID="ddlOffice" runat="server" Width="60%">
                                                        </asp:DropDownList></td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                        Patient Name &nbsp;
                                                    </td>
                                                    <td style="width: 35%; height: 18px;">
                                                        &nbsp;<asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                    <td style="width: 35%">
                                                        &nbsp;</td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                    <td style="width: 35%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td align="left">
                                                 <asp:Button ID="btnRevert" runat="server" CssClass="Buttons"
                                                            Text="Revert" OnClick="btnRevert_Click" />
                                                </td>
                                                    <td class="ContentLabel" colspan="3" style="height: 23px">
                                                        
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtSort" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                            OnClick="btnSearch_Click"  />
                                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" OnClick="btnExportToExcel_Click"
                                                            Text="Export To Excel" />
                                                           
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <div style="overflow: scroll; height: 600px">
                                                <asp:DataGrid ID="grdReceivedeport" runat="server" Width="100%" CssClass="GridTable"
                                                    AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages"
                                                    OnItemCommand="grdReceivedeport_ItemCommand" OnSelectedIndexChanged="grdReceivedeport_SelectedIndexChanged">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        
                                                    
                                                      <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkUpdateStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                   <%-- <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No"></asp:BoundColumn>--%>
                                                  <asp:TemplateColumn HeaderText="Case No" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="CaseNo" CommandArgument="CASE_NO"
                                                                Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CASE_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                     <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="ChartNo" CommandArgument="CHART_NO"
                                                                Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CHART_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                               
                                               
                                                    <asp:TemplateColumn HeaderText="Office Name" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlOfficeName" runat="server" CommandName="OfficeName" CommandArgument="OFFICE_NAME"
                                                                Font-Bold="true" Font-Size="12px">Office Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.OFFICE_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                     <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PatientName" CommandArgument="PATIENT_NAME"
                                                                Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.PATIENT_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Date Of Referral" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlDateOfReferral" runat="server" CommandName="DateOfReferral" CommandArgument="DATE_OF_REFERRAL_PROC"
                                                                Font-Bold="true" Font-Size="12px">Date Of Referral</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.DATE_OF_REFERRAL_PROC", "{0:MM/dd/yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                 
                                                       
                                                    
                                                    
                                                     <%--   <asp:BoundColumn DataField="OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                      <asp:BoundColumn DataField="DATE_OF_REFERRAL_PROC" HeaderText="Date Of Referral" DataFormatString="{0:MM/dd/yyyy}" Visible="true"></asp:BoundColumn>--%>
                                                        <asp:BoundColumn DataField="PROCEDURE CODES" HeaderText="Procedure code"></asp:BoundColumn>
                                                          <asp:TemplateColumn HeaderText="Completed Date" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlCompletedDate" runat="server" CommandName="CompletedDate" CommandArgument="DATE_OF_REFERRAL_PROC"
                                                                Font-Bold="true" Font-Size="12px">Completed Date</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.DATE_OF_REFERRAL_PROC", "{0:MM/dd/yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                        <%--<asp:BoundColumn DataField="COMPLETED DATE" HeaderText="Completed Date" DataFormatString="{0:MM/dd/yyyy" Visible="true">
                                                        </asp:BoundColumn>--%>
                                                        <asp:BoundColumn DataField="REPORT LINK" HeaderText="Report Link" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Report">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="btnlnkView">
																		                <img src="Images/123.gif" style="border:none;" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>&nbsp;
                                            </div>
                                            
                                        </td>
                                    </tr>
                                      <tr visible="false">
                                        <td style="width: 100%" class="TDPart" visible="false">
                                            <div style="overflow: scroll; height: 600px">
                                                <asp:DataGrid ID="grdExel" runat="server" Width="100%" CssClass="GridTable"
                                                    AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages" Visible="false">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        
                                                    
                                                    
                                                 
                                                       
                                                        
                                                        <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                      <asp:BoundColumn DataField="DATE_OF_REFERRAL_PROC" HeaderText="Date Of Referral" DataFormatString="{0:MM/dd/yyyy}" Visible="true"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PROCEDURE CODES" HeaderText="Procedure code"></asp:BoundColumn>
                                                       
                                                    <asp:BoundColumn DataField="DATE_OF_REFERRAL_PROC" HeaderText="Completed Date" DataFormatString="{0:MM/dd/yyyy}" Visible="true"></asp:BoundColumn>
                                                        <%--<asp:BoundColumn DataField="DATE_OF_REFERRAL_PROC" HeaderText="Completed Date" DataFormatString="{0:MM/dd/yyyy" Visible="true">
                                                        </asp:BoundColumn>--%>
                                                        <asp:BoundColumn DataField="REPORT LINK" HeaderText="Report Link" Visible="false"></asp:BoundColumn>
                                                       
                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>&nbsp;
                                            </div>
                                            
                                        </td>
                                    </tr>
                                </table>
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
