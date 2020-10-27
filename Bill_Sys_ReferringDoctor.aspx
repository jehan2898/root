<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ReferringDoctor.aspx.cs" Inherits="Bill_Sys__Referring_Doctor" %>

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
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop" style="height: 10px">
                        </td>
                        <td class="CenterTop" style="height: 10px">
                        </td>
                        <td class="RightTop" style="height: 10px">
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
                                                    Doctor Name
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtDoctorName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Speciality
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctorType" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="--- Select ---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Doctor License Number
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Provider Name --%>
                                                    Office Name
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlProvider" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                        Visible="false" />
                                                    <cc1:ExtendedDropDownList ID="extddlOffice" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%" valign="top">
                                                    <%-- Tax Type (Check Only one)--%>
                                                    Assign No</td>
                                                <td class="ContentLabel" style="width: 35%" valign="top">
                                                    <asp:TextBox ID="txtAssignNumber" runat="server" MaxLength="50" CssClass="textboxCSS" ></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--KOEL--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:TextBox ID="txtKOEL" runat="server" MaxLength="50" Width="250px" Visible="False"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--WCB Authorization --%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtWCBAuth" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--WCB Rating Code--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtWCBRatingCode" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office Address--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeAdd" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office City--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office State--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeState" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office Zip--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office Phone--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Address--%>
                                                    <%--Office Fax--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingAdd" runat="server" MaxLength="50" Width="250px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtOfficeFax" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing City--%>
                                                    <%-- Office Email--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtOfficeEmail" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%-- Billing State--%>
                                                    <%-- Assign Number--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingState" runat="server" MaxLength="50" Width="250px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Zip--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingZip" runat="server" MaxLength="50" Width="250px" Visible="False"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Phone--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingPhone" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%-- NPI--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtNPI" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%-- Federal Tax--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="50" Width="250px" Visible="false"></asp:TextBox></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:CheckBoxList ID="chklstTaxType" runat="server" RepeatDirection="Horizontal"
                                                        Visible="false">
                                                        <asp:ListItem Value="0">SSN</asp:ListItem>
                                                        <asp:ListItem Value="1">EIN</asp:ListItem>
                                                    </asp:CheckBoxList></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    &nbsp;<asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
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
                                    <td class="TDPart" style="width: 100%; text-align: right; height: 44px;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdDoctorNameList" runat="server" OnDeleteCommand="grdDoctorNameList_DeleteCommand"
                                            OnPageIndexChanged="grdDoctorNameList_PageIndexChanged" OnSelectedIndexChanged="grdDoctorNameList_SelectedIndexChanged"
                                            OnItemCommand="grdDoctorNameList_ItemCommand" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_TYPE" HeaderText="Doctor Type" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_TYPE_ID" HeaderText="Doctor ID " Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_LICENSE_NUMBER" HeaderText="Doctor License Number">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="Provider ID" Visible="false">
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_ASSIGN_NUMBER" HeaderText="Assign #" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Office Name"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_ManageVisitTreatmentTest.aspx?Flag=Visit&DoctorID=<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>"
                                                            target="_self">Manage Visit</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_ManageVisitTreatmentTest.aspx?Flag=Treatment&DoctorID=<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>"
                                                            target="_self">Manage Treatment</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_ManageVisitTreatmentTest.aspx?Flag=Test&DoctorID=<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>"
                                                            target="_self">Manage Test</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:ButtonColumn CommandName="AddDiagnosis" Text="Add Diagnosis code" Visible="false">
                                                </asp:ButtonColumn>
                                                <asp:ButtonColumn CommandName="AddProcedure" Text="Add Procedure Code" Visible="false">
                                                </asp:ButtonColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_WCB_AUTHORIZATION" HeaderText="WCB Auth" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_WCB_RATING_CODE" HeaderText="WCB Code" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ADDRESS" HeaderText="Office Address" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_CITY" HeaderText="Office City" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_STATE" HeaderText="Office State" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ZIP" HeaderText="Office Zip" Visible="false"></asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="SZ_OFFICE_PHONE" HeaderText="Office Phone" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_ADDRESS" HeaderText="Billing Address" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_CITY" HeaderText="Billing City" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_STATE" HeaderText="Billing State" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_ZIP" HeaderText="Billing Zip" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_PHONE" HeaderText="Billing Phone" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_NPI" HeaderText="NPI" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_FEDERAL_TAX_ID" HeaderText="Federal Tax" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BIT_TAX_ID_TYPE" HeaderText="Tax Type" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_KOEL" HeaderText="KOEL" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="KOEL" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ID" HeaderText="Office ID" Visible="false"></asp:BoundColumn>
                                                
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

