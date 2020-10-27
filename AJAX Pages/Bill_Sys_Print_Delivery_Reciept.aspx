<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Print_Delivery_Reciept.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Print_Delivery_Reciept"
    Title="Print Delivery Receipt" %>

<%@ Register Src="~/UserControl/PatientInformation.ascx" TagName="UserPatientInfoControl"
    TagPrefix="UserPatientInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdreciept.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		            if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		        {		
	                if(f.getElementsByTagName("input").item(i).disabled==false)
	                {
                        f.getElementsByTagName("input").item(i).checked=ival;
                    }
			    }	
		    }
       }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" style="width: 100%; height: 100%" bgcolor="white">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table id="First" style="width: 100%; height: 100%" bgcolor="white">
                            <tr>
                                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                                    padding-top: 3px; height: 100%">
                                    <table id="MainBodyTable" style="width: 100%; border: 0;">
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
                                            <td style="width: 100%">
                                                <UserPatientInfo:UserPatientInfoControl runat="server" ID="UserPatientInfoControl" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div id="DivStatus2" runat="Server">
                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                    Height="25px" Width="24px"></asp:Image>
                                                Loading...</div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span style="font-size: small"><b>Select Speciality </b></span>
                                                <%--  <asp:Label ID="lblspeciality" Text="Select Speciality" runat="server"></asp:Label>--%>
                                                <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="200Px" Selected_Text="---Select---"
                                                    Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                    AutoPost_back="true" Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged">
                                                </extddl:ExtendedDropDownList>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td align="right">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <span style="font-size: small"><b>Date&nbsp; </b></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                CssClass="text-box" MaxLength="10" Width="100px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                                PopupButtonID="imgbtnFromDate" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 45%">
                                                            <dx:ASPxGridView ID="grdreciept" ClientInstanceName="grdreciept" Width="100%" SettingsCustomizationWindow-Height="400"
                                                                Settings-VerticalScrollableHeight="400" runat="server" AutoGenerateColumns="false" SettingsPager-PageSize="1000"
                                                                 KeyFieldName="SZ_PROCEDURE_ID">
                                                                <Columns>
                                                                    <%--    
                    <dx:GridViewDataColumn Caption="Procedure ID" Visible="false" FieldName="SZ_PROCEDURE_ID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Procedure Code" FieldName="SZ_PROCEDURE_CODE"></dx:GridViewDataColumn>
                               <dx:GridViewDataColumn Caption="Description" FieldName="SZ_CODE_DESCRIPTION"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Amount" FieldName="FLT_AMOUNT"></dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Caption="Procedure Group ID" FieldName="SZ_PROCEDURE_GROUP_ID" Visible="false"></dx:GridViewDataColumn>
                    </Columns>--%>
                                                                    <dx:GridViewDataColumn Width="90px" Caption="Select">
                                                                        <DataItemTemplate>
                                                                            <asp:CheckBox ID="chkall" runat="server" />
                                                                        </DataItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_ID" Caption="Procedure ID" HeaderStyle-HorizontalAlign="Center"
                                                                        Visible="false">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_CODE" Caption="Procedure Code" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_CODE_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" Font-Bold="True" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="FLT_AMOUNT" Caption="Amount" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                        HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Procedure Group" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                               
                                                                <SettingsBehavior AllowSelectByRowClick="true" />
                                                                <Settings ShowVerticalScrollBar="true" ShowHorizontalScrollBar="true" />
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 10%" align="center">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnAdd" Width="80px" Text="Add >>" runat="server" OnClick="btnAdd_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnRemove" Width="80px" Text="Remove <<" runat="server" OnClick="btnRemove_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 45%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:100%">
                                                            <dx:ASPxGridView ID="grdprintselected" ClientInstanceName="grdprintselected" Width="100%"
                                                                SettingsCustomizationWindow-Height="400" Settings-VerticalScrollableHeight="400"
                                                                runat="server" AutoGenerateColumns="false" KeyFieldName="SZ_PROCEDURE_ID" SettingsPager-PageSize="1000">
                                                                <Columns>
                                                                    <%--    
                    <dx:GridViewDataColumn Caption="Procedure ID" Visible="false" FieldName="SZ_PROCEDURE_ID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Procedure Code" FieldName="SZ_PROCEDURE_CODE"></dx:GridViewDataColumn>
                               <dx:GridViewDataColumn Caption="Description" FieldName="SZ_CODE_DESCRIPTION"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Amount" FieldName="FLT_AMOUNT"></dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Caption="Procedure Group ID" FieldName="SZ_PROCEDURE_GROUP_ID" Visible="false"></dx:GridViewDataColumn>
                    </Columns>--%>
                                                                    <dx:GridViewDataColumn Width="90px" Caption="Select">
                                                                        <DataItemTemplate>
                                                                            <asp:CheckBox ID="chkall" runat="server" />
                                                                        </DataItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_ID" Caption="Procedure ID" HeaderStyle-HorizontalAlign="Center"
                                                                        Visible="false">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_CODE" Caption="Procedure Code" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_CODE_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" Font-Bold="True" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="FLT_AMOUNT" Caption="Amount" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                        HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Procedure Group" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                    </dx:GridViewDataColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                <SettingsPager Visible="false">
                                                                </SettingsPager>
                                                                <SettingsBehavior AllowSelectByRowClick="true" />
                                                                <Settings ShowVerticalScrollBar="true" ShowHorizontalScrollBar="true" />
                                                                <%-- <Styles>
                        <FocusedRow BackColor="#99ccff">
                        </FocusedRow>
                        <AlternatingRow Enabled="True">
                        </AlternatingRow>
                        <Header BackColor="#99ccff">
                        </Header>
                    </Styles>--%>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClick="btnPrint_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--   <asp:Button ID="btnRemove" Text="Remove" Width="60px" runat="server" OnClick="btnRemove_Click" />--%>
                                    <%--<asp:Button ID="btnPrint" Text="Print" runat="server" Width="60px" OnClick="btnPrint_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
