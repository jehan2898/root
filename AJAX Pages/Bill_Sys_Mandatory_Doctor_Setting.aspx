<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Mandatory_Doctor_Setting.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Mandatory_Doctor_Setting"
    Title="Doctor Setting" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Data" Assembly="DevExpress.Data.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<%@ Register Assembly="DevExpress.XtraCharts.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.PivotGrid.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid" TagPrefix="temp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
     function val_CheckControls()
      {
         var readingdoctor = document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value;
         if(readingdoctor=="NA")
        {
             alert("Please Select Case Type");
             return false;
        }
         
      }
      function Clear()
      {
       document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value="NA";
       var casetype=document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType");
       casetype.disabled = false;
       document.getElementById('<%=chknpi.ClientID %>').checked = false;  
       document.getElementById('<%=chkdoclicensenumber.ClientID %>').checked = false;  
       document.getElementById('<%=chkwcbauthnumber.ClientID %>').checked = false;  
       document.getElementById('<%=Chkwcbratingcode.ClientID %>').checked = false;  
      }
      function callfordelete()
       {
          document.getElementById('ctl00_ContentPlaceHolder1_hdndelete').value='true'; 
          var f= document.getElementById("<%=grddoctorsetting.ClientID%>");
         
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkall') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                }
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        
                if(confirm("Are you sure want to Delete?")==true)
				{
				  
				   return true;
				}
				else
				{
					return false;
				}
       }  
    </script>

    <table style="width: 100%; background-color: White">
        <tr>
            <td>
                <asp:UpdatePanel ID="upMain" runat="server">
                    <ContentTemplate>
                        <table style="width: 46%; background-color: White">
                            <tr>
                                <td valign="top">
                                    <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid;
                                        border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                                        <tr>
                                            <td style="background-color: #b4dd82; height: 15%; font-weight: bold; font-size: small">
                                                &nbsp;Doctor Setting
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upmsg" runat="server">
                                                    <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                            </contenttemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" width="42%">
                                                    <tr>
                                                        <td style="text-align: left; color: #626262; font-family: Arial; font-size: 14px;">
                                                            Case Type
                                                        </td>
                                                        <td>
                                                            <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                            </extddl:ExtendedDropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table border="0" width="100%" style="height: 50px;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chknpi" runat="server" />
                                                                        <%-- <dx:ASPxCheckBox ID="chknpi" runat="server">
                                                                        </dx:ASPxCheckBox>--%>
                                                                    </td>
                                                                    <td>
                                                                        <b>NPI</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkdoclicensenumber" runat="server" />
                                                                        <%--<dx:ASPxCheckBox ID="chkdoclicensenumber" runat="server">
                                                                        </dx:ASPxCheckBox>--%>
                                                                    </td>
                                                                    <td>
                                                                        <b>Doctor License Number</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkwcbauthnumber" runat="server" />
                                                                        <%-- <dx:ASPxCheckBox ID="chkwcbauthnumber" runat="server">
                                                                        </dx:ASPxCheckBox>--%>
                                                                    </td>
                                                                    <td>
                                                                        <b>WCB Authorization Number </b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="Chkwcbratingcode" runat="server" />
                                                                        <%-- <dx:ASPxCheckBox ID="Chkwcbratingcode" runat="server">
                                                                        </dx:ASPxCheckBox>--%>
                                                                    </td>
                                                                    <td>
                                                                        <b>WCB Rating Code</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 100%; border: 0px solid Gray;" cellpadding="0" cellspacing="0"
                                                    border="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" Style="width: 15%" />
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                                Style="width: 15%" />
                                                            <input type="button" runat="server" id="Button1" onclick="Clear();" style="width: 15%"
                                                                value="Clear" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="28" align="left" class="txt2" style="width: 100%">
                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upMain"
                                                                DisplayAfter="10" DynamicLayout="true">
                                                                <ProgressTemplate>
                                                                    <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                        runat="Server">
                                                                        <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                            Height="25px" Width="24px"></asp:Image>
                                                                        Loading...</div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; background-color: White">
                            <tr>
                                <td>
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="right" style="width: 98%;">
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td id="grdid" runat="server">
                                    <dx:ASPxGridView ID="grddoctorsetting" ClientInstanceName="grddoctorsetting" runat="server"
                                        SettingsBehavior-AllowSort="true" AutoGenerateColumns="False" KeyFieldName="SZ_COMPANY_ID"
                                        OnRowCommand="grddoctorsetting_RowCommand" Width="100%" SettingsPager-PageSize="15"
                                        SettingsPager-Mode="ShowPager" SettingsCustomizationWindow-Height="200">
                                        <Columns>
                                            <dx:GridViewDataButtonEditColumn Caption="" VisibleIndex="0" CellStyle-HorizontalAlign="Left"
                                                Width="60px">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkPatientInfo123" runat="server" Text="Select" CommandName="Select"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataButtonEditColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="I_SETTING_ID" Caption="I_SETTING_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_ID" Caption="SZ_CASE_TYPE_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BT_NPI" Caption="NPI" HeaderStyle-HorizontalAlign="Center"
                                                Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BT_DOC_LICENSE_NUMBER" Caption="Doctor License Number"
                                                HeaderStyle-HorizontalAlign="Center" Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BT_WCB_AUTHORIZATION_NUMBER" Caption="WCB Authorization Number "
                                                HeaderStyle-HorizontalAlign="Center" Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BT_WCB_RATING_CODE" Caption="WCB Rating Code "
                                                HeaderStyle-HorizontalAlign="Center" Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="chk" VisibleIndex="9" Width="30px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" Visible="false" />
                                                </HeaderTemplate>
                                                <DataItemTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" />
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                        <SettingsPager Position="Bottom" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                        <asp:HiddenField ID="hdndelete" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
