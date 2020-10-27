<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Casetype_BillConfig.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Casetype_BillConfig"
    Title="Case Type Bill Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
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



<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
       function val_CheckControls()
        {
            var casetype = document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value;	
            var Billtype = document.getElementById("ctl00_ContentPlaceHolder1_extddlBillType").value;	
            if(casetype=="NA")
            {
                 alert("Please Select Case Type");
                 return false;
            }
             if(Billtype=="NA")
            {
                 alert("Please Select Bill Type");
                 return false;
            }
        }
        
        function Clear()
      {
            document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value="NA";
            document.getElementById("ctl00_ContentPlaceHolder1_extddlBillType").value="NA";
            var casetype=document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType");
            var save=document.getElementById("ctl00_ContentPlaceHolder1_btnsave");
            var update=document.getElementById("ctl00_ContentPlaceHolder1_btnUpdate");
            save.disabled = false;
            update.disabled = true;
            casetype.disabled = false;
      }
      
        function callfordelete()
       {
          document.getElementById('ctl00_ContentPlaceHolder1_hdndelete').value='true';
          var f= document.getElementById("<%=grdcasetypewithbill.ClientID%>");
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

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <table style="width: 100%; background-color: White; border: 1px;">
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <asp:UpdatePanel ID="upMain" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 0px solid;">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: White;">
                                    <table border="0" width="42%">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                    height: 42%; border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                            <b class="txt3">Search Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table border="0" width="100%">
                                                                <tr>
                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                        Case Type
                                                                    </td>
                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                        Bill Type
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="82%" Selected_Text="---Select---"
                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                        </extddl:ExtendedDropDownList>
                                                                    </td>
                                                                    <td align="center">
                                                                        <extddl:ExtendedDropDownList ID="extddlBillType" runat="server" Width="82%" Selected_Text="---Select---"
                                                                            Procedure_Name="SP_GET_BILL_TYPE_BILL_CONFIG" Flag_Key_Value="BILLTYPE_LIST"
                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="width: 100%; border: 0px solid Gray; padding-top: 20px; padding-bottom: 10px;"
                                                                cellpadding="0" cellspacing="0">
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
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" width="100%">
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
                                    <dx:ASPxGridView ID="grdcasetypewithbill" ClientInstanceName="grdcasetypewithbill"
                                        runat="server" AutoGenerateColumns="False" KeyFieldName="SZ_CASE_TYPE_ID" Width="100%"
                                        OnRowCommand="grdcasetypewithbill_RowCommand" SettingsPager-PageSize="2" SettingsCustomizationWindow-Height="150"
                                        SettingsPager-Mode="ShowPager">
                                        <Columns>
                                            <dx:GridViewDataButtonEditColumn Caption="" VisibleIndex="0" CellStyle-HorizontalAlign="Left"
                                                Width="60px">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkPatientInfo123" runat="server" Text="Select" CommandName="Select"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataButtonEditColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_ID" Caption="SZ_CASE_TYPE_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_BILLTYPE_ABBRIVATION_ID" Caption="SZ_BILLTYPE_ABBRIVATION_ID"
                                                HeaderStyle-HorizontalAlign="Center" Visible="False">
                                                <HeaderStyle HorizontalAlign="left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                Width="240px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_BILLTYPE_ABBRIVATION" Caption="Bill Type" HeaderStyle-HorizontalAlign="Center"
                                                Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="chk"  Width="30px">
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
                                        <SettingsPager Position="Bottom" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtUserid" runat="server" Visible="False" Width="10px"></asp:TextBox>
     <asp:HiddenField ID="hdndelete" runat="server" />
</asp:Content>
