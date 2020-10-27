<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AssignProcedure.aspx.cs" Inherits="Bill_Sys_AssignProcedure"
    Title="Untitled Page" %>
  <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
  <%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript" src="validation.js"></script>
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
                                                         <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 25px;">
                                                    Doctor</td>
                                                <td class="ContentLabel" style="width: 35%; height: 25px;">
                                                    <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 25px;">
                                                </td>
                                                <td style="width: 35%; height: 25px; font-family:Arial;font-size:12px" align="center" >
                                                    Specialty</td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;" valign="top">
                                                    Procedure Code</td>
                                                <td class="ContentLabel" style="width: 35%; height: 18px;" valign="top">
                                                    <asp:ListBox ID="lstProcCode" runat="server" SelectionMode="Multiple" Width="97%"></asp:ListBox>
                                                </td>
                                                
                                                <td class="ContentLabel" style="width: 35%; height: 18px;">
                                                    </td>
                                                <td style="width: 100%; height: 18px;border:2px">
                                                <div id ="divtest" runat="server" style="overflow:scroll;height:200px; border-right: #6699ff 1px solid; border-top: #6699ff 1px solid; border-left: #6699ff 1px solid; border-bottom: #6699ff 1px solid;width:100%">
                                                      <asp:CheckBoxList ID="chklist" runat="server"></asp:CheckBoxList>
                                                 </div>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
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
                                                    <asp:TextBox ID="txtProcCode" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>                                                    
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>                                                    
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSave_Click" />
                                                       <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons" Visible="false"/>
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons" OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right; height: 44px;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAssignProcCode" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left" Visible="false" >
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="I_ASSOCIATE_ID" HeaderText="I_ASSOCIATE_ID" Visible="false" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false" ></asp:BoundColumn>
                                                
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="SZ_PROCEDURE_CODE_ID" Visible="false" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure code" ></asp:BoundColumn>                                                
                                                
                                                <asp:BoundColumn DataField="BT_COMPLETE" HeaderText="BT_COMPLETE" Visible="false" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false" ></asp:BoundColumn>
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
