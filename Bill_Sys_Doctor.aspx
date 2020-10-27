<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Doctor.aspx.cs" Inherits="Bill_Sys_Doctor" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }

        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }

        function CheckSpeciality() {
            var chkDoctor = document.getElementById('_ctl0_ContentPlaceHolder1_chkSuperVisingDoctor');
            var speciality = document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality').value;
            alert(speciality);
            if (speciality == 'NA') {
                document.getElementById('_ctl0_ContentPlaceHolder1_chkSuperVisingDoctor').checked = false;
                alert('Select the Specialty');
                return false;

            }
            else {
                alert('Specailty value selected');
                return true;
            }
            //            if(chkDoctor.checked)
            //            {
            //                alert('Specaility value selected');
            //                return true;
            //            }
            //            else
            //            {
            //                alert('Select the Speciality');
            //                return false;
            //            }
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
                                        <table border="0" cellpadding="0" cellspacing="0" class="ContentTable" style="width: 100%">
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
                                                    Title
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    <cc1:ExtendedDropDownList ID="extddlDoctorType" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="--- Select ---" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Doctor License Number
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <%--<td class="ContentLabel" style="width: 15%">
                                                    Provider Name
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlProvider" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---" />
                                                </td>--%>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Provider Name&nbsp;</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlOffice" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    WCB Authorization
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtWCBAuth" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    WCB Rating Code
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtWCBRatingCode" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="vertical-align: top; width: 15%">
                                                    Specialty</td>
                                                <td class="ContentLabel" style="font-size: 12px; vertical-align: top; width: 35%;">
                                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                        Selected_Text="---Select---" Width="90%" />
                                                    <asp:TextBox ID="txtFollowUp" runat="server" CssClass="textboxCSS" onkeypress="return CheckForInteger(this,'')"
                                                        MaxLength="3" Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="vertical-align: top; width: 15%">
                                                    Tax ID</td>
                                                <td class="ContentLabel" style="vertical-align: top; width: 15%">
                                                    <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="50" CssClass="textboxCSS">
                                                    </asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    NPI
                                                </td>
                                                 <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtNPI" runat="server" MaxLength="50" CssClass="textboxCSS" Visible="True"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Federal Tax--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtKOEL" runat="server" MaxLength="50" CssClass="textboxCSS" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>

                                           <%-- <tr>
                                                 <td class="ContentLabel" style="width: 15%">
                                                    NPI
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtNpi" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                            </tr>--%>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    <%--Office Address--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px;">
                                                    <asp:TextBox ID="txtOfficeAdd" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    <%--Office City--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px;">
                                                    <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    <%-- Office State--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px;">
                                                    <asp:TextBox ID="txtOfficeState" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    <%-- Office Zip--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px;">
                                                    <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Office Phone--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Address--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingAdd" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%-- Billing City--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%-- Billing State--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingState" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Zip--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingZip" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <%--Billing Phone--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingPhone" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; vertical-align: top;">
                                                    Tax Type (Check Only one)
                                                </td>
                                                <td style="font-size: 12px; font-family: arial; width: 35%; vertical-align: top;">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 16%; vertical-align: top;">
                                                            </td>
                                                            <td width="80%" style="vertical-align: top;">
                                                                <asp:CheckBoxList ID="chklstTaxType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="0">SSN</asp:ListItem>
                                                                    <asp:ListItem Value="1">EIN</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                                </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; vertical-align: top;">
                                                </td>
                                                <td style="font-size: 12px; vertical-align: top; width: 35%; font-family: arial;
                                                    text-align: left">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 16%;vertical-align: top;">
                                                            </td>
                                                            <td width="80%" style="vertical-align: top;">
                                                                <asp:RadioButtonList ID="rdlstWorkType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Employee" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Owner" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               <%-- <td class="ContentLabel" style="width: 15%; vertical-align: top;">
                                                    &nbsp;</td>
                                                <td class="ContentLabel" style="width: 15%; vertical-align: top;">
                                                    &nbsp;</td>--%>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="vertical-align: top; width: 15%">
                                                </td>
                                                <td style="font-size: 12px; vertical-align: top; width: 35%; font-family: arial;
                                                    text-align: left">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 16%; height: 39px;" valign = "top">
                                                            </td>
                                                            <td width="80%" style="height: 39px" align="left" valign="top">
                                                                <asp:CheckBox ID="chkIsReferral" runat="server" Text="Is Referral" />
                                                                &nbsp; &nbsp;
                                                                <asp:CheckBox ID="chkIsUnBilled" runat="server" Text="UnBilled" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--<table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 16%">
                                                            </td>
                                                            <td width="80%">
                                                                <asp:RadioButtonList ID="rdlstWorkType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Employee" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Owner" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                </td>
                                                <td class="ContentLabel" style="vertical-align: top; width: 15%">
                                                    SuperVising Doctor
                                                </td>
                                                <td style="font-size: 12px; vertical-align: top; width: 35%; font-family: arial;
                                                    text-align: left">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width= "16%" height="39px">
                                                            </td>
                                                            <td width="80%" style="height: 39px" align="left" valign="top">
                                                                <asp:CheckBox ID="chkSuperVisingDoctor" runat="server" OnCheckedChanged="chkSuperVisingDoctor_CheckedChanged" AutoPostBack= "true"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--<table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 16%; height: 39px;">
                                                            </td>
                                                            <td width="80%" style="height: 39px" align="left" valign="top">
                                                                <asp:CheckBox ID="chkIsReferral" runat="server" Text="Is Referral" />
                                                                &nbsp; &nbsp;
                                                                <asp:CheckBox ID="chkIsUnBilled" runat="server" Text="UnBilled" />
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                </td>
                                            </tr>
                                            <tr width="100%">
                                                <td width="15%" class="ContentLabel" style="vertical-align: top; height: 42px" >
                                                    <asp:Label ID= "lblProcedurecode" Text="Procedure Code" CssClass="ContentLabel" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; height: 42px" width = "35%" class="ContentLabel">
                                                
                                                    <asp:ListBox ID="lstProcedureCode" runat="server" Height="100px" Width="88%" Visible="false" SelectionMode = "Multiple"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" colspan="4">
                                                    <asp:Button ID="btnDeleteSpeciality" runat="server" Text="Delete" Width="80px" CssClass="Buttons"
                                                        CausesValidation="false" OnClick="btnDeleteSpeciality_Click" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="width: 15%;">
                                                    <asp:DataGrid ID="grdDoctSpeciality" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        OnItemCommand="grdDoctSpeciality_ItemCommand" ShowFooter="True" Width="100%"
                                                        OnItemDataBound="grdDoctSpeciality_ItemDataBound" Visible="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="SZ_SPECIALITY_DOCTOR_ID" HeaderText="SPECIALITY ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SPECIALITY ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="SPECIALITY" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Specialty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROCEDURE_GROUP") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:ExtendedDropDownList ID="FooterextddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                        Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                                </FooterTemplate>
                                                                <ItemStyle Width="75%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="">
                                                                <ItemTemplate>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="FooterlnkAddSpeciality" runat="server" CommandName="Speciality"
                                                                        Font-Bold="true" Font-Size="12px">Add Specialty</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <ItemStyle Width="15%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" />
                                                    </asp:DataGrid></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtWorkType" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <cc1:ExtendedDropDownList ID="extddlProvider" Width="2px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                        Visible="false" />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtDoctorID" runat="server" Visible="False" Width="10px"></asp:TextBox>

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
                                    <td class="TDPart" style="width: 100%; text-align: right;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="overflow: scroll; height: 300px; width: 100%;">
                                            <asp:DataGrid ID="grdDoctorNameList" runat="server" OnDeleteCommand="grdDoctorNameList_DeleteCommand"
                                                OnPageIndexChanged="grdDoctorNameList_PageIndexChanged" OnSelectedIndexChanged="grdDoctorNameList_SelectedIndexChanged"
                                                OnItemCommand="grdDoctorNameList_ItemCommand" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_TYPE" HeaderText="Doctor Type" Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_TYPE_ID" HeaderText="Doctor ID " Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_LICENSE_NUMBER" HeaderText="Doctor License Number">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="Provider ID" Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PROVIDER_NAME" HeaderText="Provider" Visible="false">
                                                    </asp:BoundColumn>
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
                                                    <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Provider Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TITLE" HeaderText="Title" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="I_IS_EMPLOYEE" HeaderText="Work Type" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="referral Type" Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="I_FOLLOWUP_REPORT" HeaderText="Follow Up Report" Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="Specialty"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BT_IS_UNBILLED" HeaderText="BT_IS_UNBILLED" Visible="false">
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="BT_SUPERVISING_DOCTOR" HeaderText="BT_SUPERVISING_DOCTOR" Visible="false">
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                        </div>
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

