<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AssignSysSettings.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_AssignSysSettings"
    Title="Green Your Bills - Assign Sys Settings" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function doctor_notes() {


            var url = 'Bill_Sys_Doctor_Notes.aspx';
            ShowPopup.SetContentUrl(url);
            ShowPopup.Show();
            return false;

        }
        function SelectAll(ival) {

            var f = document.getElementById("<%= grdSettings.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }

            }
        }


        function Check() {

            var key = document.getElementById("ctl00_ContentPlaceHolder1_extddlSysKeys");
            if (key.value == "NA") {
                alert('Please select Key.');
                return false;
            } else {

                var iFlag = "1";
                var txt = document.getElementById("<%=txtSystemKeyValue.ClientID%>");
                if (txt != null) {
                    if (txt.value == "") {
                        iFlag = "0";
                    }
                }

                var drd1 = document.getElementById("ctl00_ContentPlaceHolder1_extddlUserLawFirm");

                if (drd1 != null) {


                    if (drd1.value == "NA") {
                        iFlag = "0";
                    }
                }
            }
            if (iFlag == 0) {
                alert('Please select Value.');
                return false;
            } else {
                return true;
            }

        }


        function Clear() {
            var chk = document.getElementById("<%=chkChartNumber.ClientID%>");
            if (chk != null) {
                chk.checked = false;
            }



            document.getElementById("ctl00_ContentPlaceHolder1_extddlSysKeys").value = 'NA';

            var txt = document.getElementById("<%=txtSystemKeyValue.ClientID%>");

            if (txt != null) {
                txt.value = "";
            }

            var drd = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchTime");
            if (drd != null) {
                drd.value = "AM";
            }

            var drd1 = document.getElementById("ctl00_ContentPlaceHolder1_extddlUserLawFirm");
            if (drd1 != null) {
                drd1.value = "NA";
            }

        }

        function confirm_delete() {
            var f = document.getElementById("<%=grdSettings.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            bfFlag = true;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }



            if (confirm("Are you sure want to Delete?") == true) {

                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="border: 0px solid #B5DF82; height: 140px; width:70%" >
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                            width: 100%; border-bottom: #b5df82 1px solid; height: 140px" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                            cellspacing="0" cellpadding="0" align="left" border="0">
                            <tbody>
                                <tr>
                                    <td class="txt2" valign="middle" align="left" bgcolor="#b5df82" colspan="2" height="28">
                                        <b class="txt3">Search Parameters</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 700px">
                                        <table style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 50%" align="center">
                                                        <asp:Label ID="lblKey" runat="server" Font-Bold="true" Text="Key"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%" align="center">
                                                        <asp:Label ID="lblVlue" runat="server" Font-Bold="true" Text="Value"></asp:Label>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%" align="center">
                                                        <extddl:ExtendedDropDownList ID="extddlSysKeys" runat="server" Width="85%" AutoPost_back="True"
                                                            Selected_Text="---Select---" Procedure_Name="SP_MST_SYS_SETTING_KEY" Flag_Key_Value="GET_KEY_LIST"
                                                            Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlSysKeys_extendDropDown_SelectedIndexChanged">
                                                        </extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 50%" align="center">
                                                        <asp:TextBox ID="txtSystemKeyValue" runat="server" Width="80%" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                        <extddl:ExtendedDropDownList ID="extddlUserLawFirm" runat="server" Width="150px"
                                                            Visible="false" Selected_Text="---Select---" Procedure_Name="SP_MST_LEGAL_LOGIN"
                                                            Flag_Key_Value="GET_USER_LIST" Connection_Key="Connection_String" Maintain_State="true">
                                                        </extddl:ExtendedDropDownList>
                                                         <extddl:ExtendedDropDownList ID="extdllUserName" runat="server" Width="150px"
                                                            Visible="false" Selected_Text="---Select---" Procedure_Name="SP_GET_USERS"
                                                            Flag_Key_Value="GET_USER_LIST" Connection_Key="Connection_String" Maintain_State="true"> </extddl:ExtendedDropDownList > 
                                                         <extddl:ExtendedDropDownList ID="extddlTreatmentAddress" runat="server" Width="150px"
                                                            Visible="false" Selected_Text="---Select---" Procedure_Name="sp_get_system_settings_treatment_address"
                                                            Flag_Key_Value="GET_TREATMENT_ADDRESS_LIST" Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlTreatmentAddress_extendDropDown_SelectedIndexChanged">
                                                        </extddl:ExtendedDropDownList > 

                                                        <extddl:ExtendedDropDownList ID="extdlPOS" runat="server" Width="150px"
                                                            Visible="false" Selected_Text="---Select---" Procedure_Name="sp_get_system_settings_place_of_service"
                                                            Flag_Key_Value="GET_POS_LIST" Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extdlPOS_extendDropDown_SelectedIndexChanged">
                                                        </extddl:ExtendedDropDownList > 
                                                        
                                                          <extddl:ExtendedDropDownList ID="extdlLDateFormate" runat="server" Width="150px"
                                                            Visible="false" Selected_Text="---Select---" Procedure_Name="SP_GET_DATE_FORMATE"
                                                            Flag_Key_Value="GET_DATE_FORMATE" Connection_Key="Connection_String" Maintain_State="true">
                                                        </extddl:ExtendedDropDownList > 
 
                                                        <asp:CheckBox ID="chkChartNumber" runat="server" Visible="false"></asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%" align="center">
                                                        <asp:Label ID="lblSubValue" runat="server" Font-Bold="true" Text="Sub Value"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%" align="center">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%" align="center">
                                                        <asp:DropDownList ID="ddlSchTime" runat="server" Width="45px">
                                                            <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="200px" AutoPost_back="false"
                                                    Selected_Text="---Select---" Procedure_Name=""
                                                    Flag_Key_Value="GET_VISIT_TYPE" Connection_Key="Connection_String" Visible="false">
                                                    </extddl:ExtendedDropDownList>
                                                    <asp:textbox id="txtSubValue" runat="server" visible="false" />
                                                    </td>
                                                    <td style="width: 50%" align="center">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Width="80px" Text="Add" OnClick="btnSave_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnUpdate" runat="server" Width="80px" Text="Update" OnClick="btnUpdate_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnClear" runat="server" Width="80px" Text="Clear" OnClick="btnClear_Click">
                                        </asp:Button>
                                          <a href="javascript:void(0);" onclick="doctor_notes();return false;" class="regular">Mandatory Doctor's Notes</a>
                                        <%-- <input style="width: 80px" id="Button1" onclick="Clear();" type="button" value="Clear" />--%>
                                    </td>
                                   

                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
            </td>
            <td valign="top">
            
                <table width="350px" style="border: 1px solid #B5DF82; height: 140px">
                    <tr>
                        <td align="left" valign="middle" bgcolor="#B5DF82" style="width: 500px; text-align: left"
                            class="txt2">
                            <b>Upload Private Bill Template</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                            <asp:Label ID="lblUpload" runat="server" Text="" Visible="false" ForeColor="red"
                                Font-Size="small"></asp:Label>
                                 </ContentTemplate>
                </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadControl" runat="server" Height="25px"></asp:FileUpload><%--OnClick="btnupload_Click"--%>
                            <asp:Button ID="btnupload" runat="server" Height="25px" OnClick="btnupload_Click"
                                Text="Upload" Width="60px" />&nbsp;
                            
                        </td>
                    </tr>
                    <tr>
                    <td align="left" valign="top">
                    <asp:CheckBox ID="chkUpload" runat="server" Checked="true" Text="OverWrite" />
                    </td>
                    </tr>
                </table>
               
            </td>
        </tr>
        <tr>
            <td style="width: 100%; ">
                <div style="width: 100%;">
                    <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" class="txt2"
                        align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 700px">
                                <b class="txt3">Key Settings</b>
                            </td>
                        </tr>
                        <tr>
                       
                            <td style="width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                     <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" >
                                <ProgressTemplate>
                         
                                        <asp:Image ID="img1" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....."
                                            Height="50px" Width="50px"></asp:Image>
                                            
                           
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                        <table style="vertical-align: middle; width: 100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 60%" align="left">
                                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" Visible="false"
                                                            CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                          Record Count:<%= this.grdSettings.RecordCount%>| Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 10%;" align="left">
                                                        
                                                    </td>
                                                    <td style="vertical-align: middle; width: 30%; text-align: right" align="right">
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="100%" colspan="3">
                                                      
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <xgrid:XGridViewControl ID="grdSettings" runat="server" Height="150px" Width="100%"
                                            CssClass="mGrid" AllowSorting="true" DataKeyNames="SZ_SYS_SETTING_ID,SZ_SYS_SETTING_KEY_ID,BT,SZ_SETTING_VALUE,SZ_SYS_SETTING_VALUE,SZ_SYS_SUB_SETTING_VALUE"
                                            PagerStyle-CssClass="pgr" PageRowCount="50" XGridKey="Bill_Sys_Sys_Setting" GridLines="None"
                                            AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields=""
                                            ExportToExcelColumnNames="" HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1"
                                            EnableRowClick="false" ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelLitigation"
                                            MouseOverColor="0, 153, 153" AutoGenerateColumns="false" OnRowCommand="grdSettings_RowCommand">
                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkSelect" Font-Underline="false" runat="server" CausesValidation="false" CommandName="Select"   
                                                                                Text="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                                                                                              
                                                                            </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SZ_SYS_SETTING_KEY" HeaderText="Setting Key">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SZ_SETTING_VALUE" HeaderText="Setting Key Value">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                    <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SZ_SYS_SETTING_ID" HeaderText="SZ_SYS_SETTING_ID" Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SZ_SYS_SETTING_KEY_ID" HeaderText="SZ_SYS_SETTING_KEY_ID"
                                                    Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SZ_SETTING_VALUE" HeaderText="SZ_SYS_SUB_SETTING_VALUE"
                                                    Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BT" HeaderText="SZ_SYS_SUB_SETTING_VALUE" Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BT" HeaderText="SZ_SYS_SUB_SETTING_VALUE" Visible="False">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </xgrid:XGridViewControl>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                
            </td>
            
            <td valign="top" style="width:100%">
            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                <ContentTemplate>
                <table style="text-align: left; width: 200px;" border="0">
                    <tr>
                        <td style="text-align: left; width: 200px; vertical-align: top;">
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 350px;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td style="width: 200px; height: 0px;" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 350px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                            <tr>
                                                <td align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 500px;">
                                                    <b class="txt3">Email Notifications - File transfer</b>
                                                </td>
                                                <td height="28" align="center" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 60%;">
                                                    <asp:Label ID="lblmsgEmail" runat="server" ForeColor="red" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch" style="width: 40%;">
                                                    Law Firm</td>
                                                <td class="td-widget-bc-search-desc-ch" style="width: 60%;">
                                                    Email Address</td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 24px; width: 40%; vertical-align: top;">
                                                    <extddl:ExtendedDropDownList ID="extddlLawFirm" runat="server" Connection_Key="Connection_String"
                                                        AutoPost_back="true" Flag_Key_Value="GET_USER_LIST" Procedure_Name="SP_MST_LEGAL_LOGIN"
                                                        Width="100%" OnextendDropDown_SelectedIndexChanged="OnextendDropDown_SelectedIndexChanged" />
                                                </td>
                                                <td style="width: 60%; height: 24px;" align="center">
                                                    <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="MultiLine" Width="90%"
                                                        Height="80px" MaxLength="4000"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;">
                                                    <asp:Button ID="btnSaveEmail" runat="server" Text="Save" Width="57px" OnClick="btnSaveEmail_OnClick"
                                                        Style="float: right;"></asp:Button>
                                                </td>
                                                <td style="width: 60%;" align="center">
                                                    <asp:Label ID="lblNote" runat="server" ForeColor="red" Font-Size="Small" Text="(Comma separated email addresses)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
<dx:ASPxPopupControl ID="ShowPopup" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup"
        HeaderText="Mandatory Doctor's Notes" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="700px"
        ToolTip="Select Cost Center" PopupHorizontalOffset="0" PopupVerticalOffset="0" 
          AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="350px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>