<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ReferringFacility.aspx.cs" Inherits="Bill_Sys_ReferringFacility" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script type="text/javascript">
        //function ConfirmDelete()
        //{
        //     var msg = "Do you want to proceed?";
        //     var result = confirm(msg);
        //     if(result==true)
        //     {
        //        return true;
        //     }
        //     else
        //     {
        //        return false;
        //     }
        //}

        function OnSave() {
            
            
            var ReferringFacility = document.getElementById('_ctl0_ContentPlaceHolder1_extddlReferringFacility');          
            if (ReferringFacility.value == "NA") {
                alert('Please select the Facility to proceed');
                return false;
            }
            else {
                return true;
            }
        }

        function OnDelete() {
   
            var f = document.getElementById('_ctl0_ContentPlaceHolder1_grdReferenceList');
            var bfFlag = false;

            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            bfFlag = true;
                            break;
                        }
                    }
                }
            }

            if (bfFlag == false) {
                alert('select at least one Facility');
                return false;
            }
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
                                                        &nbsp;</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Referring Facility </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <extddl:ExtendedDropDownList id="extddlReferringFacility" runat="server" Width="90%" Selected_Text="--- Select ---" Procedure_Name="SP_MST_REFERRING_FACILITY" 
                                                        Flag_Key_Value="REFERRING_FACILITY_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                    
                                                    </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
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
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdReferenceList" runat="server" OnSelectedIndexChanged="grdReferenceList_SelectedIndexChanged"
                                            OnPageIndexChanged="grdReferenceList_PageIndexChanged" OnItemCommand="grdReferenceList_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_REF_FAC_COM_ID" HeaderText="Referred Company ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_REFERRING_FACILITY_ID" HeaderText="Referring Facility ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_REFERRING_FACILITY" HeaderText="Referring Facility" ></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
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
