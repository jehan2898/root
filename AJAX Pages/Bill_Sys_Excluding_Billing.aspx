<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Excluding_Billing.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Excluding_Billing"
    Title="Exclude Billing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
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



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">
     function SelectAllForprovider(ival)
       {
            var f= document.getElementById("<%= grdexcludingbill.ClientID %>");	
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
       
       function callfordelete()
       {
          document.getElementById('ctl00_ContentPlaceHolder1_hdndelete').value='true'; 
          var f= document.getElementById("<%=grdexcludingbill.ClientID%>");
         
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
       
       var keyValue;
       function popup_Shown(s, e)
       {
            callbackPanel.PerformCallback(keyValue);
       }
        
        
         function val_CheckControls()
      {
        var readingdoctor = document.getElementById("ctl00_ContentPlaceHolder1_ddlReadingddlDoctor_DDD_L_VI").value;
        var insurance = document.getElementById("ctl00_ContentPlaceHolder1_ddlInsurance_DDD_L_VI").value;	
        var casetype = document.getElementById("ctl00_ContentPlaceHolder1_ddlCaseType_DDD_L_VI").value;		
        var lst= document.getElementById('ctl00_ContentPlaceHolder1_lstReadingdoctor').value;
        if(readingdoctor=="")
        {
             alert("Please Select Reading Doctor");
             return false;
        }
        if(insurance=="")
        {
            alert("Please Select Insurance Company");
            return false;
        }
        if(casetype=="")
        {
             alert("Please Select Case Type");
             return false;
        }
        if(lst=="")
        {
             alert("Please Select Reading Doctor List");
             return false;
        }
     }
     
        function Clear()
        {
             ddlReadingddlDoctor.SetValue("--Select--");
             ddlInsurance.SetValue("--Select--");
             ddlCaseType.SetValue("--Select--");
             document.getElementById('ctl00_ContentPlaceHolder1_lstReadingdoctor').options.length=null; 
        }
        
        function removeItem()
        {
            var liste = document.getElementById('ctl00_ContentPlaceHolder1_lstReadingdoctor');
            var i;
            for(i=liste.options.length-1;i>=0;i--)
            {
            if(liste.options[i].selected)
            liste.remove(i);
            }
        }
        
        function Remove(listbox)
        {
            var arrText = new Array();
            var arrValue = new Array();
            var count = 0;
            for(i = 0; i < listbox.options.length; i++)
            {
                if (listbox.options[i].selected && listbox.options[i].value != "")
                {
              //      alert(listbox.options[i].text);
                }
                else
                {
                    arrText[count] = listbox.options[i].text;
                    arrValue[count] = listbox.options[i].value;
                    count++;
                }
            }
            listbox.length = 0;
            for(index = 0; index < arrText.length; index++)
            {
                var no = new Option();
                no.value = arrValue[index];
                no.text = arrText[index];  
                listbox[index] = no;     
            }
        }
        
        
        
    </script>

    <table style="width: 100%; background-color: White; border: 1px;">
        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback">
            <ClientSideEvents CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
        </dx:ASPxCallback>
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <div id="Div1" runat="server">
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
                                <table border="0" width="100%">
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                height: 100%; border: 1px solid #B5DF82;">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                        <b class="txt3">Search Parameters</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch">
                                                                    Reading Doctor
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch">
                                                                    Insurance Company
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Case Type
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxComboBox runat="server" ID="ddlReadingddlDoctor" DropDownStyle="DropDownList"
                                                                        ClientInstanceName="ddlReadingddlDoctor" IncrementalFilteringMode="StartsWith"
                                                                        EnableSynchronization="False" CssClass="txtbox-diplay" TextField="DESCRIPTION"
                                                                        ValueField="CODE" OnSelectedIndexChanged="ASPxComboBoxddlReadingddlDoctor_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxComboBox runat="server" ID="ddlInsurance" DropDownStyle="DropDownList" ClientInstanceName="ddlInsurance"
                                                                        IncrementalFilteringMode="StartsWith" EnableSynchronization="False" CssClass="txtbox-diplay"
                                                                        TextField="DESCRIPTION" ValueField="CODE">
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxComboBox runat="server" ID="ddlCaseType" DropDownStyle="DropDownList" ClientInstanceName="ddlCaseType"
                                                                        IncrementalFilteringMode="StartsWith" EnableSynchronization="False" CssClass="txtbox-diplay"
                                                                        TextField="DESCRIPTION" ValueField="CODE">
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width: 100%; border: 0px solid Gray; padding-top: 20px; padding-bottom: 10px;"
                                                            cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="font-size: 11px; font-family: Arial; vertical-align: top;" align="right">
                                                                    <dx:ASPxButton ClientInstanceName="" ID="btnsave" runat="server" Text="Save" Width="24%"
                                                                        OnClick="btnSave_Click" Native="true">
                                                                        <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                                                    </dx:ASPxButton>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="top" align="center" style="width: 15%">
                                                                    <dx:ASPxButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                                        Width="80%" Native="true">
                                                                        <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td valign="top" align="left">
                                                                    <input type="button" runat="server" id="btnclear" onclick="Clear();" style="width: 24%"
                                                                        value="Clear" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="width: 50%;">
                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                height: 100%; border: 1px solid #B5DF82;">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                        <b class="txt3">Reading Doctor List</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch">
                                                                    Reading Doctor
                                                                </td>
                                                                <td style="width: 80%;">
                                                                    <%-- <dx:ASPxListBox ID="lstReadingdoctor" runat="server" SelectionMode="Multiple" Height="96px"
                                                                        Width="100%">
                                                                    </dx:ASPxListBox>--%>
                                                                    <asp:ListBox Width="100%" ID="lstReadingdoctor" runat="server" SelectionMode="Multiple"
                                                                        Height="72px" CausesValidation="true"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="center" colspan="2">
                                                                    <input type="button" runat="server" id="btnremove" onclick="Remove(ctl00_ContentPlaceHolder1_lstReadingdoctor);"
                                                                        value="Remove" />
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
                                            <dx:ASPxGridView ID="grdexcludingbill" ClientInstanceName="grdexcludingbill" runat="server"
                                                SettingsBehavior-AllowSort="true" AutoGenerateColumns="False" KeyFieldName="ID"
                                                Width="100%" OnRowCommand="grdexcludingbill_RowCommand" SettingsPager-PageSize="15"
                                                SettingsCustomizationWindow-Height="150" SettingsPager-Mode="ShowPager">
                                                <Columns>
                                                    <dx:GridViewDataButtonEditColumn Caption="" VisibleIndex="0" CellStyle-HorizontalAlign="Left"
                                                        Width="60px">
                                                        <DataItemTemplate>
                                                            <asp:LinkButton ID="lnkPatientInfo123" runat="server" Text="Select" CommandName="Select"></asp:LinkButton>
                                                        </DataItemTemplate>
                                                        <HeaderStyle BackColor="#B5DF82" Font-Bold="True" />
                                                    </dx:GridViewDataButtonEditColumn>
                                                    <dx:GridViewDataColumn FieldName="ID" Caption="ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_READING_DOCTOR_ID" Caption="SZ_READING_DOCTOR_ID"
                                                        HeaderStyle-HorizontalAlign="Center" Visible="False">
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_READING_DOCTOR" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                        Width="240px">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_INSURANCE_ID" Caption="SZ_INSURANCE_ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance Name" HeaderStyle-HorizontalAlign="Center"
                                                        Width="240px">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_ID" Caption="SZ_CASE_TYPE_ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                        Width="100px">
                                                        <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="chk" VisibleIndex="9" Width="30px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllForprovider(this.checked);"
                                                                ToolTip="Select All" Visible="false" />
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" />
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" BackColor="#B5DF82" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <Settings  ShowFilterRow="true" ShowGroupPanel="true" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsBehavior AllowSelectByRowClick="true" />
                                                <SettingsPager Position="Bottom" />
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Modal="True">
                    </dx:ASPxLoadingPanel>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:HiddenField ID="hdndelete" runat="server" />
</asp:Content>
