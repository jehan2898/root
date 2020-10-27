<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_DiagnosisCode.aspx.cs" Inherits="Bill_Sys_DiagnosisCode" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    

    <script type="text/javascript">
        function ConfirmDelete()
        {
             var msg = "Do you want to proceed?";
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
        
        function confirm_check()
		 {
            var f= document.getElementById("<%= grdDiagonosisCode.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            return true;
			        }
			    }			
		    }
		    alert('Please Select Record.');
		    return false;
          }
		   
		function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdDiagonosisCode.ClientID %>");	
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

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 76%;
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
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Diagnosis Type
                                                </td>
                                                <td style="width: 35%; height: 18px;" class="ContentLabel">
                                                    <cc1:ExtendedDropDownList ID="extddlDiagnosisCodeType" runat="server" CssClass="textboxCSS"
                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                        Flag_Key_Value="DIAGNOSIS_TYPE_LIST" Width="90%"></cc1:ExtendedDropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Diagnosis Code</td>
                                                <td style="width: 35%; height: 18px;" class="ContentLabel">
                                                    <asp:TextBox ID="txtDiagonosisCode" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Description
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textboxCSS"
                                                        MaxLength="50"  Width="80%"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Add to Preferred List
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:CheckBox ID="chkAddPrefList" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                           <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                   Type
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                 <asp:DropDownList ID="ddlType" runat="server" Width="80%">
                                                    <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="ICD9" Value="ICD9"></asp:ListItem>
                                                    <asp:ListItem Text="ICD10" Value="ICD10"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                   
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right;">
                                        <asp:Button ID="btnUpdatePreferedList" runat="server" CssClass="Buttons" Text="Update Preferred List"
                                            OnClick="btnUpdatePreferedList_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdDiagonosisCode" runat="server" OnSelectedIndexChanged="grdDiagonosisCode_SelectedIndexChanged"
                                            OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="15" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                                    Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BT_ADD_TO_PREFERRED_LIST" HeaderText="PREFERRED BIT" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE_ID" HeaderText="TYPE" ></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                            ToolTip="Select All" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
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
        <tr>
            <td>
                <asp:TextBox ID="txtAddToPreferredList" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
