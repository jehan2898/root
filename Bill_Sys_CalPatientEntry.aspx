<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_CalPatientEntry.aspx.cs" Inherits="Bill_Sys_CalPatientEntry" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script language="javascript" type="text/javascript">
       
       function openTypePage(obj)
       {
            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute'; 
            document.getElementById('divid').style.left= '300px'; 
            document.getElementById('divid').style.top= '100px'; 
            document.getElementById('divid').style.zindex= '1'; 
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src="Bill_Sys_AddAppointment.aspx?_date=" + obj  ;
            
       }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%;vertical-align:top;" >
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%;vertical-align: top;">
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
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;vertical-align:top;">

                                <tr>
                                    <td align="center" class="TDPart">
                                        <table width="100%">
                                        
                                        <tr>
                                           <td width="15%" style="height: 19px" >                                                   
                                               <asp:Label ID="lblHeaderPatientName" runat="server" ></asp:Label>
                                                </td>
                                                 <td width="15%" style="height: 19px" >                                                   
                                               
                                                </td>
                                                 <td width="25%" style="height: 19px" >                                                   
                                               
                                                </td>
                                                 <td width="35%" style="height: 19px; text-align:right;" colspan="2" >                                                                                                 
                                                  <asp:Label ID="lblCurrentDate" runat="server" width="100%"></asp:Label>                                               
                                                </td>
                                        </tr>
                                            <tr>
                                               <%-- <td width="10%" style="height: 64px">
                                                    Date
                                                </td>
                                                <td width="30%" style="height: 64px">
                                                   <asp:TextBox ID="txtDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                    <asp:ImageButton ID="imgDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" PopupButtonID="imgDate" />
                                                    
                                                </td>--%>
                                                <td width="15%" style="height: 64px">
                                                    
                                                <asp:Label runat="server" ID="lblTestFacility">Test Facility :</asp:Label> 
                                                </td>
                                                 <td width="25%" style="height: 64px">
                                                    <extddl:ExtendedDropDownList id="extddlReferringFacility" runat="server" Width="150px" Selected_Text="--- Select ---" Procedure_Name="SP_TXN_REFERRING_FACILITY" Flag_Key_Value="REFERRING_FACILITY_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList></td>
                                                <td style="width: 15%; height: 64px;" align="right">
                                                    Interval
                                                </td>
                                                <td style="width: 25%; height: 64px;">
                                                    <asp:DropDownList ID="ddlInterval" runat="server" Width="60px">
                                                        <asp:ListItem Text="0.15" Value="0.15"></asp:ListItem>
                                                        <asp:ListItem Text="0.30" Value="0.30"></asp:ListItem>
                                                        <asp:ListItem Text="0.45" Value="0.45"></asp:ListItem>
                                                        <asp:ListItem Text="0.60" Value="0.60"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="20%" style="height: 64px">
                                                   <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnShowGrid" runat="server" Text="Show" OnClick="btnShowGrid_Click" CssClass="Buttons"/>
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    
                                    
                                        
                                                    
                                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="vertical-align: top;" width="14%">
                                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <contenttemplate>
						                                        <asp:Panel ID="Panel1" runat="server">
						                                        </asp:Panel>
					                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td> 
                                                <td style="vertical-align: top;" width="86%">
                                                
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <contenttemplate>
		                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
		                                                        <asp:Panel ID="Panel2" runat="server">
						
						                                        </asp:Panel>
						
	                                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                    
                                                    
                                                <asp:DataGrid ID="grdScheduleReport" runat="server"  Width="100%" CssClass="GridTable" AutoGenerateColumns="true"  
                                        >
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle CssClass="GridRow" />
                                       <%-- <Columns>
                                            <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left"
                                                Visible="false"></asp:ButtonColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Appointment Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="ID" Visible="false" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Patient Name"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Doctor Name"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Start Time"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="End Time"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Type"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="" HeaderText="Status"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="" Text="Add" ></asp:ButtonColumn>
                                            
                                        </Columns>--%>
                                        <HeaderStyle CssClass="GridHeader"/>
                                    </asp:DataGrid>
                                                </td> 
                                                </tr> 
                                                </table> 
                                    
                                        
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
 
    <div id="divid" style="position: absolute; width: 700px; height: 500px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a  onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="500px" width="700px"></iframe>
    </div>
 
   
</asp:Content>

