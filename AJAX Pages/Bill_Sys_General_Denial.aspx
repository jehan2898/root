<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_General_Denial.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_General_Denial" EnableEventValidation="false"%>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function OpenGeneralDenialScan(denialId, caseId, caseNo) {
            var nodeId = document.getElementById('hdnNodeId').value;
            scanGeneralDenial(caseId, caseNo, denialId, nodeId, '1');
        }

        function closeWindow() {
            window.parent.location.reload(true);
        }
    </script>

    <script language="javascript" type="text/javascript">

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
        function AddDenial() {
            var e = document.getElementById("carTabPage_extddenial");
            var user = e.options[e.selectedIndex].text;
            if (user == "---Select---") {
                alert("Please select Denial Reason!");
                return false;
            }

            //alert(user);
            var vlength = document.getElementById("carTabPage_lbSelectedDenial").length;


            //alert(vlength);
            var status = "0";

            var i;
            if (vlength != 0) {
                for (i = 0; i < vlength; i++) {
                    if (document.getElementById("carTabPage_extddenial").value == document.getElementById("carTabPage_lbSelectedDenial").options[i].value) {
                        alert("Denial reason already exists");
                        status = "1";
                        return false;
                    }
                }
            }
            if (status != "1") {
                // alert(e.options[e.selectedIndex].text);
                document.getElementById("carTabPage_hfdenialReason").value = document.getElementById("carTabPage_hfdenialReason").value + e.options[e.selectedIndex].value + ",";
                var optn = document.createElement("OPTION");
                optn.text = e.options[e.selectedIndex].text;
                optn.value = e.options[e.selectedIndex].value;
                document.getElementById("carTabPage_lbSelectedDenial").options.add(optn);
                //alert(document.getElementById("carTabPage_hfdenialReason").value);
                return false;
            }
        }

        function RemoveDenial() {

            if (document.getElementById("carTabPage_lbSelectedDenial").length <= 0) {
                alert("No Denial reason available to remove.")
                document.getElementById("carTabPage_lbSelectedDenial").focus();
                return false;
            }

            else if (document.getElementById("carTabPage_lbSelectedDenial").selectedIndex < 0) {
                alert("Please Select Denail reason to Remove.");
                document.getElementById("carTabPage_lbSelectedDenial").focus();
                return false;
            }
            else {

                var e = document.getElementById("carTabPage_lbSelectedDenial");
                var user = e.options[e.selectedIndex].value;
                //alert(user);
                document.getElementById("carTabPage_hfremovedenialreason").value = document.getElementById("carTabPage_hfremovedenialreason").value + e.options[e.selectedIndex].value + ",";
                document.getElementById("carTabPage_lbSelectedDenial").options[document.getElementById("carTabPage_lbSelectedDenial").selectedIndex] = null;
                user = user + ',';
                //alert(user);
                var newVal = document.getElementById("carTabPage_hfdenialReason").value;
                newVal = newVal.replace(user, '');
                document.getElementById("carTabPage_hfdenialReason").value = newVal;
                var list = document.getElementById("carTabPage_lbSelectedDenial");
                var items = list.getElementsByTagName("option");
                //alert(document.getElementById("carTabPage_hfdenialReason").value);
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function Validate() {

            var Date = document.getElementById('carTabPage_txtSaveDate');

            if (Date.value == "") {
                alert('Please Enter Verification Date');
                return false;
            }

            var Denial = document.getElementById('carTabPage_lbSelectedDenial');

            if (Denial.options.length == 0) {
                alert('Please Add Denial Reason');
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function ShowAddDocumentDenialPopup(caseID, DenialID) {
            var url = "Bill_Sys_Upload_File.aspx?CaseID=" + caseID + "&DenialId=" + DenialID + "";
            UploadPopup.SetContentUrl(url);
            UploadPopup.Show();
            return false;
        }

        function CloseAddDocumentDenial() {
            UploadPopup.Hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table style="width: 100%; text-align: center">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="vertical-align: top">
                    <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                        Width="100%" Height="250" OnActiveTabChanged="carTabPage_ActiveTabChanged" AutoPostBack="true">
                        <TabPages>
                            <dx:TabPage Text="Add" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:Panel Style="background-color: white; width: 400px; height: 300px;" ID="pnlSaveDescription"
                                            runat="server">
                                            <div align="left" style="vertical-align: top;">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                                                    class="TDPart">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSave" runat="server" Text="Date" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSaveDate" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="lbl">
                                                            <asp:Label ID="lblSaveDatevalue" runat="server" Text="Verification Date" CssClass="lbl"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"
                                                                Visible="true">
                                                            </asp:TextBox><asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red" CssClass="lbl"></asp:Label>
                                                            <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtSaveDate"
                                                                PopupButtonID="imgSavebtnToDate" />
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Description" runat="server" Text="Description" Font-Bold="true" CssClass="lbl"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSaveDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDenial" Text="Denial Reason" runat="Server" Font-Bold="true" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <extddl:ExtendedDropDownList ID="extddenial" Width="140px" runat="server" Connection_Key="Connection_String"
                                                                            Procedure_Name="SP_MST_DENIAL" Flag_Key_Value="DENIAL_LIST" Selected_Text="--- Select ---"
                                                                            CssClass="cinput" Visible="true" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="button" class="Buttons" value="+" height="5px" width="5px" id="btnAddDenial"
                                                                            runat="server" onclick="AddDenial();" />
                                                                        <input type="button" class="Buttons" value="~" height="5px" width="5px" id="btnRemoveDenial"
                                                                            runat="server" onclick=" RemoveDenial();" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:ListBox ID="lbSelectedDenial" Hight="60%" Width="100%" runat="server"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btnSaveDesc" runat="server" Text="Save" OnClick="btnSaveDesc_Click"
                                                                class="Buttons" />
                                                            <%--<asp:Button id = "btnSaveDate" runat="server" Text="Save"  OnClick = "btnSaveDesc_Click" class="Buttons"/> 
                                                        <asp:Button id = "btnSavesent" runat="server" Text="Save"  OnClick = "btnSaveDesc_Click" class="Buttons"/> --%>
                                                            <asp:Button ID="btnCancelDesc" runat="server" Text="Cancel" OnClick="btnCancelDesc_Click"
                                                                class="Buttons" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hfdenialReason" runat="server" />
                                                            <asp:HiddenField runat="server" ID="hfremovedenialreason" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="View" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <table>
                                            <td colspan="3">
                                                <contenttemplate>
                                                        <dx:ASPxGridView ID="grdDenialReason" runat="server" OnRowCommand="grdDenialReason_RowCommand" KeyFieldName="I_DENIAL_ID" AutoGenerateColumns="false"
                                                            Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                            Settings-VerticalScrollableHeight="330" >
                                                            <Columns>
                                                                <%--0--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                </dx:GridViewDataColumn>
                                                                <%--1--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                                                    Width="25px" Settings-AllowSort="true">
                                                                </dx:GridViewDataColumn>
                                                                <%--2--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                </dx:GridViewDataColumn>
                                                                <%--3--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px" >
                                                                </dx:GridViewDataColumn>
                                                                <%--4--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_DENIAL_REASONS" Caption="Denial Reason" HeaderStyle-HorizontalAlign="Center"
                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                                </dx:GridViewDataColumn>
                                                                <%--5--%>
                                                                <dx:GridViewDataColumn FieldName="I_DENIAL_REASONS_CODE" Caption="Reason Code" HeaderStyle-HorizontalAlign="Center"
                                                                    Width="73px" Visible="false">
                                                                </dx:GridViewDataColumn>
                                                                <%--6--%>
                                                                <dx:GridViewDataColumn FieldName="I_DENIAL_ID" Caption="Reason Id" HeaderStyle-HorizontalAlign="Center"
                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px" Visible="false">
                                                                </dx:GridViewDataColumn>
                                                                <%--7--%>
                                                                <dx:GridViewDataColumn FieldName="DT_VERIFICATION_DATE" Caption="Denial Date" HeaderStyle-HorizontalAlign="Center"
                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px" >
                                                                </dx:GridViewDataColumn>
                                                                <%--8--%>
                                                                <dx:GridViewDataTextColumn ReadOnly="True">
                                                                    <DataItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnDelete" runat="server" CommandArgument='<%# Eval("SZ_CASE_ID") %>' OnClientClick="return confirm('Are you sure you want to delete this event?');"
                                                                            Text='Delete' CommandName="Delete" ></asp:LinkButton>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <%--9--%>
                                                                <dx:GridViewDataTextColumn ReadOnly="True" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="5">
                                                                    <DataItemTemplate>
                                                                        <a href="javascript:void(0);" onclick="OpenGeneralDenialScan('<%# Eval("I_DENIAL_ID") %>','<%# Eval("SZ_CASE_ID") %>','<%# Eval("SZ_CASE_NO") %>')">Scan/Upload</a> 
                                                                        <asp:LinkButton ID="lnkBtnScan" runat="server" 
                                                                            CommandArgument='<%# Eval("SZ_CASE_ID") %>' CommandName="Scan" Text="Scan" Visible="false"></asp:LinkButton>
                                                                        
                                                                        <asp:LinkButton ID="lnkBtnUpload" runat="server" 
                                                                            CommandArgument='<%# Eval("SZ_CASE_ID") %>' CommandName="Upload" Text="Upload" Visible="false"></asp:LinkButton>
                                                                        
                                                                   </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </contenttemplate>
                                            </td>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </td>
            </tr>
        </table>
        <dx:ASPxPopupControl ID="UploadPopup" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="UploadPopup" HeaderText="Upload" HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
            EnableAnimation="False" EnableViewState="True" Width="500px" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="200px">
            <ClientSideEvents CloseButtonClick="CloseAddDocumentDenial" />
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <asp:HiddenField ID="hdfCaseID" runat="server" />
        <asp:HiddenField ID="hdfDenialID" runat="server" />
        <asp:HiddenField runat="server" ID="hdnNodeId" />
        <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>
    </div>
    </form>
</body>
</html>
