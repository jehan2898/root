<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ConfigureColorCode.aspx.cs" Inherits="Bill_Sys_ConfigureColorCode" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script language="javascript" src="color_conv.js"></script>
 
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
    </script>
 
    <script>
    
     
      
    
    function vColor()
    {
        var i = document.getElementById('txtCategoryName').value;
        if(i == "" || i.trim().length() == 0)
        {
            alert('Category name cannot be left blank');
            return false;
        }    
        i = document.getElementById('txtChosenColor').value;
        if(i == "")
        {
            alert('Select a color code for the category');
            return false;
        }
        
        return true;
    }

    function OnChangeColor(color,param,controlid,i_id){
	    if(color){		  
                document.getElementById("_ctl0_ContentPlaceHolder1_txtSelectedColorCode").value = color;
	    	    return true;
    			
	    }
    }

    function OnBtnColorClick()
    {
        fnShowChooseColorDlg('CCCCCC',2,'','_ctl0_ContentPlaceHolder1_txtSelectedColorCode','_ctl0_ContentPlaceHolder1_txtSelectedColorCode');
    }
    </script>

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
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Event Status </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <cc1:ExtendedDropDownList ID="extddlEventStatus" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_TXN_EVENT_STATUS" Flag_Key_Value="GET_EVENT_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Color Code&nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtSelectedColorCode" runat="server"></asp:TextBox>
                                                    <a href="#" onclick="javascript:OnBtnColorClick();"> Select color</a>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align:right; height: 44px;">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdColorCode" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                                            OnSelectedIndexChanged="grdColorCode_SelectedIndexChanged">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_TXN_ID" HeaderText="Txn ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Event Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="Color Code" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COLOR" HeaderText="Color"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="I_EVENT_STATUS_ID" HeaderText="Event Status ID" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn>
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
    </table>
</asp:Content>

