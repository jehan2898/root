<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_UserMaster.aspx.cs" Inherits="Bill_Sys_UserMaster" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript" src="js/jquery-latest.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $(document).on('keypress', '.txt-string', function (e) {

                var sChars = ',"\'';
                if (sChars != '' || sChars != null) {
                    var sCharArr = sChars.split("");
                    for (i = 0; i < sCharArr.length; i++) {
                        var c = sCharArr[i].charCodeAt(0);
                        if (e.which == c) {
                            $(this).css({ "background-color": "#ff8080" });
                            $(this).focus();
                            $(this).css({ "background-color": "white" });
                            alert('" , \' is not allowed');
                            return false;
                        }
                        else {
                            $(this).css({ "background-color": "white" });
                            $(this).closest('span').css("display", "none");
                            ;
                        }
                    }
                }
                return true;
            });


            $('.txt-string').focusout(function (e) {
                debugger;
                var len = $(this).val().length;
                if (len < 6 || len > 25) {
                    $(this).css({ "background-color": "#ff8080" });
                    var xyz = $(this).closest('span');
                    $(this).closest('td').find('.showerror').css("display", "inline");
                    $(this).focus();
                }
                else {
                    $(this).css({ "background-color": "white" });
                    $(this).closest('td').find('.showerror').css("display", "none");

                }
            });


            //

        });


        $(document).ready(function () {

            $(document).on('keypress', '.txt-email', function (e) {

                var sChars = '~!#$%^&*()_+|\-=":\';?></,\[]{} ';
                if (sChars != '' || sChars != null) {
                    var sCharArr = sChars.split("");
                    for (i = 0; i < sCharArr.length; i++) {
                        var c = sCharArr[i].charCodeAt(0);
                        if (e.which == c) {
                            $(this).css({ "background-color": "#ff8080" });
                            $(this).focus();
                            $(this).css({ "background-color": "white" });
                            return false;
                        }
                        else {
                            $(this).css({ "background-color": "white" });
                            $(this).closest('span').css("display", "none");
                            ;
                        }
                    }
                }
                return true;
            });


            $('.txt-email').focusout(function (e) {

                //var len = $(this).val().length;
                //if (len < 6 || len > 12)
                //{
                //    $(this).css({ "background-color": "#ff8080" });
                //    var xyz = $(this).closest('span');
                //    $(this).closest('td').find('.showerror').css("display", "inline");
                //    $(this).focus();
                //}
                //else
                //{
                //    $(this).css({ "background-color": "white" });
                //    $(this).closest('td').find('.showerror').css("display", "none");

                //}
            });


            //

        });



        function ConfirmDelete() {
            var msg = "This will delete the user permanently. Do you want to continue..?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function SelectAll(ival) {
            var f = document.getElementById("<%= grdReffProvider.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {

                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }

        }
        function SelectAllProvider(ival) {
            alert(ival);
            var f = document.getElementById('_ctl0_ContentPlaceHolder1_grvProvider');
            alert(f);
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

    </script>
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
        function SelectAll(ival) {
            var f = document.getElementById("<%= grdReffProvider.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {

                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }

        }
        function SelectAllProvider(ival) {
            alert(ival);
            var f = document.getElementById('_ctl0_ContentPlaceHolder1_grvProvider');
            alert(f);
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

    </script>
    <script type="text/javascript">
        function checkValid() {

            var struser = document.getElementById('_ctl0_ContentPlaceHolder1_txtUserName');
            var strpwd = document.getElementById('_ctl0_ContentPlaceHolder1_txtPassword');
            var strconfpwd = document.getElementById('_ctl0_ContentPlaceHolder1_txtConfirmPassword');
            var stremail = document.getElementById('_ctl0_ContentPlaceHolder1_txtUserEmail');

            var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (struser.value == "") {
                alert('Please enter the username.');
                return false
            }
            else if (struser.value.length < 6 || struser.value.length > 25) //len < 6 || len >12
            {
                alert('Username length should be in between 6 to 25 characters.');
                return false
            }
            else if (strpwd.value == "") {
                alert('Please enter the Password.');
                return false
            }


            else if (strconfpwd.value == "") {
                alert('Please enter the confirm Password.');
                return false
            }

            else if (strpwd.value != strconfpwd.value) {
                alert('Password and Confirm Password do not match');
                return false
            }
            else if (strpwd.value.length < 6 || strpwd.value.length > 25) {
                alert('Password length should be in between 6 to 25 characters.');
                return false
            }

            else if (strconfpwd.value.length < 6 || strconfpwd.value.length > 25) {
                alert('confirm password length should be in between 6 to 25 characters.');
                return false
            }

            else if (stremail.value == "") {
                alert('Please enter the mail id');
                return false
            }
            if (!re.test(stremail.value)) {
                alert('You must enter a valid email');
                stremail.focus();
                return false
            }


        }

        function checkValidupdate() {

            var struser = document.getElementById('_ctl0_ContentPlaceHolder1_txtUserName');
            var strpwd = document.getElementById('_ctl0_ContentPlaceHolder1_txtPassword');
            var strconfpwd = document.getElementById('_ctl0_ContentPlaceHolder1_txtConfirmPassword');
            var stremail = document.getElementById('_ctl0_ContentPlaceHolder1_txtUserEmail');

            var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (strpwd.value == "") {
                alert('Please enter the Password.');
                return false
            }
            else if (strconfpwd.value == "") {
                alert('Please enter the confirm Password.');
                return false
            }
            else if (strpwd.value != strconfpwd.value) {
                alert('Password and Confirm Password do not match');
                return false
            }
            else if (strpwd.value.length < 6 || strpwd.value.length > 25) {
                alert('Password length should be in between 6 to 25 characters.');
                return false
            }
            else if (strconfpwd.value.length < 6 || strconfpwd.value.length > 25) {
                alert('confirm password length should be in between 6 to 25 characters.');
                return false
            }
            else if (stremail.value == "") {
                alert('Please enter the mail id');
                return false
            }
            if (!re.test(stremail.value)) {
                alert('You must enter a valid email');
                stremail.focus();
                return false
            }

        }

    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop"></td>
                        <td class="CenterTop"></td>
                        <td class="RightTop"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%"></td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="2" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password do not match."
                                                            ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Font-Bold="true"></asp:CompareValidator>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserName" Font-Size="Medium" ErrorMessage="length must be between 6 to 25 characters"
                                                        ValidationExpression="^[a-zA-Z0-9\s]{6,25}$" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                    <asp:Label ID="Label1" runat="server" Text="User Name" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="textboxCSS txt-string" MaxLength="12"></asp:TextBox>
                                                    <span class='showerror' style='display: none;'>&nbsp;<img width="14" height="14" src="Images/info.png" title='Username length should be between 6 to 25 characters' /></span>
                                                </td>
                                                <td style="width: 10%" align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="User Role" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <extddl:ExtendedDropDownList ID="extddlUserRole" runat="server" Width="95%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_USER_ROLE_LIST" Procedure_Name="SP_MST_USER_ROLES" Selected_Text="---SELECT---"
                                                        OnextendDropDown_SelectedIndexChanged="extddlUserRole_extendDropDown_SelectedIndexChanged" AutoPost_back="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; height: 22px;">
                                                    <asp:Label ID="Label3" runat="server" Text="Password" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 22px;">
                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textboxCSS txt-string"
                                                        MaxLength="40"></asp:TextBox>
                                                    <span class='showerror' style='display: none;'>&nbsp;<img width="14" height="14" src="Images/info.png" title='Password length should be between 6 to 12 characters' /></span>
                                                </td>
                                                <td align="left" style="width: 10%; height: 22px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Confirm Password" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 22px;">
                                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textboxCSS txt-string"
                                                        MaxLength="40">
                                                    </asp:TextBox>
                                                    <span class='showerror' style='display: none;'>&nbsp;<img width="14" height="14" src="Images/info.png" title='Password length should be between 6 to 12 characters' /></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                    <asp:Label ID="Label5" runat="server" Text="Email ID" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtUserEmail" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <%--Label6--%>
                                                <td style="width: 10%" align="left" runat="server">
                                                    <asp:Label ID="lblReferringOffice" runat="server" Text="Referring Office" Font-Size="13px" Font-Names="Arial"></asp:Label>
                                                    <asp:Label ID="lblRefferingProvider" runat="server" Text="Reffering Provider" Visible="false" Font-Size="13px"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <extddl:ExtendedDropDownList ID="extddlProviderList" runat="server" Width="90%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="PROVIDER_LIST" Selected_Text="---SELECT---" Text="NA" Visible="False" />
                                                    <extddl:ExtendedDropDownList ID="extddlOfficeList" runat="server" Width="95%" Connection_Key="Connection_String"
                                                        Selected_Text="---SELECT---" OnextendDropDown_SelectedIndexChanged="extddlProviderList_OnextendDropDown_SelectedIndexChanged" />
                                                    <asp:TextBox ID="txtReffProvSearch" runat="server" CssClass="textboxCSS" Visible="False"></asp:TextBox>
                                                    <asp:Button ID="btnSearchRP" runat="server" Text="Search" Width="80px" OnClick="btnSearchRP_Click" CssClass="Buttons" Visible="False" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="rdoIsActive" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="Active" Selected="True" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 10%"></td>
                                                <td style="width: 20%"></td>
                                                <td align="left" style="width: 10%">
                                                    <asp:Label ID="lblProvider" runat="server" Text="Provider" Visible="false" Font-Size="13px"></asp:Label>

                                                </td>
                                                <td style="width: 20%">
                                                    <extddl:ExtendedDropDownList ID="extddlProvider" runat="server" Width="95%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="OFFICE_LIST_PROVIDER" Procedure_Name="SP_MST_OFFICE" Selected_Text="---SELECT---"
                                                        Visible="false" OnextendDropDown_SelectedIndexChanged="extddlProvider_extendDropDown_SelectedIndexChanged"
                                                        AutoPost_back="true" />
                                                    <%--<extddl:ExtendedDropDownList ID="extdllReffProvider" runat="server" Width="95%" Connection_Key="Connection_String"
                                                    Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE_REFF" Selected_Text="---SELECT---"
                                                    Visible="false" OnextendDropDown_SelectedIndexChanged="extdllReffProvider_extendDropDown_SelectedIndexChanged"
                                                    AutoPost_back="true" />--%>

                                                    <dx:ASPxGridView Width="100%" runat="server" AutoGenerateColumns="false" ID="grvProvider">
                                                        <Columns>
                                                            <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAllProvider" runat="server" onclick="javascript:SelectAllProvider(this.checked);"
                                                                        ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                <DataItemTemplate>
                                                                    <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="Provider" VisibleIndex="1">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CODE" Caption="Provider Id" VisibleIndex="2" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                        <Settings
                                                            ShowVerticalScrollBar="true"
                                                            VerticalScrollBarStyle="Standard"
                                                            VerticalScrollableHeight="100" />
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                                                    </dx:ASPxGridView>



                                                    <asp:DataGrid ID="grdReffProvider" runat="server" OnPageIndexChanged="grdReffProvider_PageIndexChanged"
                                                        Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" Visible="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelete" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Reffering Provider"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="CODE" HeaderText="REFF_PROVIDER_ID" Visible="false"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td style="width: 14%" valign="top" align="left">
                                                    <asp:Label ID="lblDoctorlst" runat="server" Text="List Of Doctors" Visible="false"
                                                        Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 60%">
                                                    <asp:ListBox ID="LstBxDoctorList" runat="server" Width="100%" Height="150px" Visible="false"
                                                        SelectionMode="Single"></asp:ListBox>
                                                </td>
                                                <td align="right" style="width: 13%"></td>
                                                <td align="right" style="width: 13%"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 14%" valign="top" align="right">
                                                    <asp:CheckBox ID="chkAllowandshow" runat="server" Visible="false" />&nbsp;
                                                </td>
                                                <td align="left" style="width: 60%">
                                                    <asp:Label ID="lblvalidateshow" runat="server" Text="Validate And Show Previous Visits Results"
                                                        Font-Size="13px" Font-Names="Arial" Visible="false"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 13%"></td>
                                                <td align="right" style="width: 13%"></td>
                                            </tr>
                                            <tr>
                                                <%--<td class="ContentLabel" style="width: 15%" valign="top">
                                                    <asp:Label ID="lblDoctorlst" runat="server" Text="List Of Doctors" Visible ="true"></asp:Label>
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:ListBox ID="LstBxDoctorList" runat="server" Width="180px" Visible = "true"></asp:ListBox>
                                                </td>--%>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtDoctorID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCurUserID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtIS_PROVIDER" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtReffOffID" runat="server" Visible="False" Width="10px"></asp:TextBox>


                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px"
                                                        CssClass="Buttons"  OnClick="btnSearch_Click" />


                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                                        CssClass="Buttons" CausesValidation="true" ValidationGroup="a" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                        CssClass="Buttons" CausesValidation="true" ValidationGroup="a" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                        Width="80px" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdUser" runat="server" OnPageIndexChanged="grdUser_PageIndexChanged" 
                                            OnSelectedIndexChanged="grdUser_SelectedIndexChanged" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="25" PagerStyle-Mode="NumericPages" OnItemCommand="grdUser_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_USER_ID" HeaderText="User ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_ROLE_ID" HeaderText="User Role ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_ROLE" HeaderText="Role"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="Provider ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_EMAIL" HeaderText="Email"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="sz_created_user_name" HeaderText="Created By"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_CREATED" HeaderText="Created On"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PASSWORD" HeaderText="Password" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ID" HeaderText="OFFICE ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_REFFERING_PROVIDER_ID" HeaderText="REFF_PROVIDER_ID" Visible="false"></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        Delete
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        Send To No Diagnosys Page
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDiagnosys" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="BT_DIAGNOSYS_PAGE" HeaderText="Email" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="bt_is_disabled" HeaderText="Disabled" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActive" runat="server"  CommandName="Active" Visible="false"
                                                            Font-Bold="true" Font-Size="12px">Activate</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDeactive" runat="server" CommandName="DeActive" Visible="false"
                                                            Font-Bold="true" Font-Size="12px">Deactivate</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                    </tr>
                    <tr>
                        <td class="LeftBottom"></td>
                        <td class="CenterBottom"></td>
                        <td class="RightBottom" style="width: 10px"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
