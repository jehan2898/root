<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_EditAll.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_EditAll"  %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />
    <script type="text/javascript">
        function updateParent(pName, dtService, lhrCode, caseType, caseNo) {
            parent.document.getElementById('ctl00_ContentPlaceHolder1_lblPatientNameEditALL').firstChild.nodeValue = pName; //Pateint Name;
            parent.document.getElementById('ctl00_ContentPlaceHolder1_lblDateofServiceEditALL').firstChild.nodeValue = dtService; //date;
            parent.document.getElementById('ctl00_ContentPlaceHolder1_lblLHRCodeEditALL').firstChild.nodeValue = lhrCode; //lhr code;
            parent.document.getElementById('ctl00_ContentPlaceHolder1_lblCaseTypeEditALL').firstChild.nodeValue = caseType; //case type;
            parent.document.getElementById('ctl00_ContentPlaceHolder1_lblCasenoEditALL').firstChild.nodeValue = caseNo; //case #;
        }

        function AddDuplicateProc(id) {
            var f = document.getElementById('grdProCode');
            var str = 0;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    alert(i);
                    if (f.getElementsByTagName("input").item(i).checked == true) {
                        str = str + 1;
                        if (str > 1) {
                            if (confirm("Do you want to add a duplicate procedure code?"))
                                __doPostBack('ReportUpdate', i);
                            break;
                        }
                    }
                }
            }
        }
        function SelectAll(ival) {

            var f = document.getElementById('tabcontainerprocedurecodedocs_tabpnlDignosiscode_grdViewDocuments');
            //alert("<%# grdViewDocuments.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function SelectAlldelete(ival) {
            var f = document.getElementById('tabcontainerprocedurecodedocs_tabpnldelte_grdDelete');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                    //			                    str=str+1;	
                    //			        
                    //			                     if (str < 10)
                    //		                        {
                    //		                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
                    //		                           
                    //		                           alert(statusnameid1.innerHTML);
                    //		                              statusname  = statusnameid1.innerHTML;
                    //		                            
                    //		                              
                    //		                                    if(statusname.toLowerCase() != "transferred")
                    //		                                    {  alert(str); 
                    //		                                         f.getElementsByTagName("input").item(i).checked=ival; 
                    //        		                                
                    //		                                    }
                    //		                           }else
                    //		                            {
                    //		                                var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl"+str+"_lblStatus");
                    //		                                    statusname  = statusnameid2.innerHTML;
                    //		                                      alert(statusname);
                    //		                                    if (statusname.toLowerCase() != "transferred")
                    //		                                    {  
                    //		                                         f.getElementsByTagName("input").item(i).checked=ival;
                    //		                                    }
                    //			                        }        
                    //			                 				

                }


            }
        }

        function validate() {
            if (document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality') != null) {
                if (document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality').value == 'NA') {
                    alert('Select Speciality ...!');
                    return false;
                }
                else
                    return true;
            }
            else {
                alert('Select Speciality ...!');
                return false;
            }
        }

        function confirm_update_bill_status_procode() {




            var f = document.getElementById('grdProCode');
            var bfFlag = false;
            var cnt = 0;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkSelectProc') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                            cnt = cnt + 1;

                        }

                    }
                }
            }

            if (bfFlag == false) {
                //alert('Please select record.');
                // return false;
            }
            else {
                if (cnt > 1) {
                    return true;
                }
            }
        }

        function confirm_delete_document() {

            var f = document.getElementById('tabcontainerprocedurecodedocs_tabpnldelte_grdDelete');


            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        if (confirm("Are you sure to continue for delete?"))
                            return true;
                        return false;
                    }
                }
            }
            alert('Please select docuemnt');
            return false;
        }

        function ReturnToParentPage() {
            var parentWindow = window.parent;
            parentWindow.SelectAndClosePopup();
        }


    </script>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td valign="top" style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div style="height: auto; width: 100%; float: left;">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td align="center">
                                                        [&nbsp;
                                                        <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous" OnClick="lnkPrevious_Click"></asp:LinkButton>
                                                        &nbsp;] &nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp; [&nbsp;
                                                        <asp:LinkButton ID="lnkNext" runat="server" Text="Next" OnClick="lnkNext_Click"></asp:LinkButton>
                                                        &nbsp;]&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp; [&nbsp;
                                                        <asp:LinkButton ID="lnkunassociatedoc" runat="server" Text="Jump to Unassociate Documents"
                                                            OnClick="lnkUnassociate_Click"></asp:LinkButton>
                                                        &nbsp;]
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <UserMessage:MessageControl runat="server" ID="MessageControl2" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 82%;">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: auto;
                                                                        vertical-align: top;">
                                                                        <tr>
                                                                            <td style="width: 100%; height: auto; float: left;">
                                                                                <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                                                                    cellspacing="0">
                                                                                    <tr id="Tr2" runat="server">
                                                                                        <td colspan="6">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ContentLabel" style="height: auto;">
                                                                                            <table width="100%" border="0">
                                                                                                <tr>
                                                                                                    <td style="width: 100%" align="left">
                                                                                                        <cc1:TabContainer ID="tabcontainerprocedurecodedocs" Width="100%" runat="Server"
                                                                                                            ActiveTabIndex="4" TabStripPlacement="Top" Height="420px" 
                                                                                                            onactivetabchanged="tabcontainerprocedurecodedocs_ActiveTabChanged">
                                                                                                            <cc1:TabPanel runat="server" ID="tabpnlDignosiscode" TabIndex="0">
                                                                                                                <HeaderTemplate>
                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                        Procedure Code and Documents
                                                                                                                    </div>
                                                                                                                </HeaderTemplate>
                                                                                                                <ContentTemplate>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td width="100%" scope="col" align="left">
                                                                                                                                <div class="blocktitle">
                                                                                                                                    <div class="div_blockcontent">
                                                                                                                                        <table width="100%">
                                                                                                                                            <tr>
                                                                                                                                                <td valign="top" style="width: 100%">
                                                                                                                                                    <asp:UpdatePanel ID="ReportUpdate" runat="server">
                                                                                                                                                        <ContentTemplate>
                                                                                                                                                            <div style="overflow: auto">
                                                                                                                                                                <table width="100%">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="2">
                                                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                                                                                                                <ContentTemplate>
                                                                                                                                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                                                                                                                                                </ContentTemplate>
                                                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr id="table_row_specialty_drpdwn" runat="server" visible="false">
                                                                                                                                                                        <td>
                                                                                                                                                                            <extddl:ExtendedDropDownList ID="extddlSpecialty" runat="server" Connection_Key="Connection_String"
                                                                                                                                                                                Flag_Key_Value="GET_SPECIALTY" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlSpecialty_SelectedIndexChanged"
                                                                                                                                                                                Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" />
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="2">
                                                                                                                                                                            <div style="overflow: scroll; height: 270px; width: 100%;">
                                                                                                                                                                                <asp:DataGrid Width="100%" ID="grdProCode" runat="server" AutoGenerateColumns="False"
                                                                                                                                                                                    CssClass="GridTable" DataKeyField="sz_procedure_Group_id">
                                                                                                                                                                                    <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:BoundColumn DataField="CODE" HeaderText="Procedure CodeID" Visible="False">
                                                                                                                                                                                        </asp:BoundColumn>
                                                                                                                                                                                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText=" Edit Procedure Code"></asp:BoundColumn>
                                                                                                                                                                                        <asp:TemplateColumn>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:CheckBox ID="chkSelectProc" runat="server" />
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateColumn>
                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                    <PagerStyle Mode="NumericPages" />
                                                                                                                                                                                </asp:DataGrid>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </div>
                                                                                                                                                        </ContentTemplate>
                                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td style="width: 100%" colspan="2">
                                                                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                                                        <ContentTemplate>
                                                                                                                                                            <div style="height: 100px; overflow: scroll;">
                                                                                                                                                                <table border="0" cellpadding="4" cellspacing="4" style="width: 100%; height: 100%">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td align="left">
                                                                                                                                                                            Reading Doctor :
                                                                                                                                                                            <%--<asp:DropDownList ID="ddlDoctor" runat="server" Width="200px">
                                                                                                                                                                                </asp:DropDownList>--%>
                                                                                                                                                                            <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="213px"
                                                                                                                                                                                Connection_Key="Connection_String" Procedure_Name="SP_GET_LHR_READINGDOCTORS"
                                                                                                                                                                                Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();"
                                                                                                                                                                                AutoPost_back="True" />
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                        Co-Signed By :
                                                                                                                                                                            <%--<asp:DropDownList ID="ddlDoctor" runat="server" Width="200px">
                                                                                                                                                                                </asp:DropDownList>--%>
                                                                                                                                                                            <extddl:ExtendedDropDownList ID="extddlCoSignedby" runat="server" Width="213px"
                                                                                                                                                                                Connection_Key="Connection_String" Procedure_Name="SP_GET_LHR_READINGDOCTORS"
                                                                                                                                                                                Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();"
                                                                                                                                                                                AutoPost_back="false" Enabled="false" />
                                                                                                                                                                        </td>
                                                                                                                                                                        <%--<td align="left">
                                                                                                                                                                                <asp:UpdatePanel ID="upDelete" runat="server">
                                                                                                                                                                                    <ContentTemplate>
                                                                                                                                                                                        <ajaxToolkit:ModalPopupExtender ID="mpDelete" runat="server" TargetControlID="lnkDelete"
                                                                                                                                                                                            DropShadow="false" PopupControlID="pnlDelete" BehaviorID="modal1" PopupDragHandleControlID="divDelete">
                                                                                                                                                                                        </ajaxToolkit:ModalPopupExtender>
                                                                                                                                                                                        <asp:Panel ID="pnlDelete" runat="server" Style="display: none; width: 100%; height: 330px;
                                                                                                                                                                                            background-color: white; border: 1px solid #B5DF82;">
                                                                                                                                                                                            <div id="divDelete" runat="server">
                                                                                                                                                                                            </div>
                                                                                                                                                                                        </asp:Panel>
                                                                                                                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                                                                                                                                                    </ContentTemplate>
                                                                                                                                                                                </asp:UpdatePanel>
                                                                                                                                                                            </td>--%>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td align="left" colspan="2" style="width: 80%">
                                                                                                                                                                            <div>
                                                                                                                                                                                <asp:DataGrid ID="grdViewDocuments" Width="100%" runat="Server" OnItemCommand="grdViewDocuments_ItemCommand"
                                                                                                                                                                                    AutoGenerateColumns="False" CssClass="GridTable">
                                                                                                                                                                                    <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                    <ItemStyle CssClass="GridViewHeader" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:TemplateColumn>
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                                                                                                                                                    ToolTip="Select All" />
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:CheckBox ID="chkView" runat="server" />
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateColumn>
                                                                                                                                                                                        <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateColumn>
                                                                                                                                                                                        <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                                                                                                                                                                        <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                                                                                                                                                                        <asp:TemplateColumn>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                                                                                                                                                                    <asp:ListItem Value="8" Selected="true">--Select--</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="0">Report</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="1">Referral</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="2">AOB</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="6">Misc</asp:ListItem>
                                                                                                                                                                                                    <asp:ListItem Value="7">Additional Reports</asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateColumn>
                                                                                                                                                                                    </Columns>
                                                                                                                                                                                </asp:DataGrid>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td align="center" colspan="2">
                                                                                                                                                                            <%--<asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"/>--%>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td align="center">
                                                                                                                                                                            <asp:Button ID="btnView" runat="server" Text="Received Document" Visible="false" />
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </div>
                                                                                                                                                        </ContentTemplate>
                                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="center" colspan="2">
                                                                                                                                                    <asp:Button ID="btnopen" runat="server" Text="Open Documents" OnClick="btnopen_Click" />
                                                                                                                                                    <asp:Button ID="btnUPdate" runat="server" Text="Save" Width="104px" OnClick="btnUPdate_Click" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="ReportUpdate" runat="server"
                                                                                                                        DisplayAfter="0">
                                                                                                                        <ProgressTemplate>
                                                                                                                            <asp:Image ID="img1" runat="server" Style="position: absolute; z-index: 1; left: 50%;
                                                                                                                                top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                                                                                                                                Height="60px" Width="60px"></asp:Image>
                                                                                                                        </ProgressTemplate>
                                                                                                                    </asp:UpdateProgress>
                                                                                                                </ContentTemplate>
                                                                                                            </cc1:TabPanel>
                                                                                                            <cc1:TabPanel runat="server" ID="tabpnldelte" TabIndex="1">
                                                                                                                <HeaderTemplate>
                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                        Delete Documents
                                                                                                                    </div>
                                                                                                                </HeaderTemplate>
                                                                                                                <ContentTemplate>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table width="100%" border="0" cellpadding="4" cellspacing="4">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            &nbsp;
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <%--<tr>
                                                                                                                                            <td>
                                                                                                                                                <div style="left: 0px; width: 100%; position: absolute; top: 0px; height: 21px; background-color: #B5DF82;
                                                                                                                                                    text-align: left" id="Div2">
                                                                                                                                                    <b>Delete File</b>
                                                                                                                                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;
                                                                                                                                                        border: 1px solid #B5DF82;">
                                                                                                                                                        <asp:Button ID="btnbillnotesclose" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                                                                                                            Text="X" OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>--%>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            &nbsp;
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                                                                                                                                </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="center" colspan="2" style="width: 80%">
                                                                                                                                            <%--<div style="overflow: scroll; height: 150px; width: 100%;">--%>
                                                                                                                                            <asp:DataGrid ID="grdDelete" Width="100%" runat="Server" AutoGenerateColumns="False"
                                                                                                                                                CssClass="GridTable" OnItemCommand="grdDelete_ItemCommand">
                                                                                                                                                <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                <ItemStyle CssClass="GridViewHeader" />
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateColumn>
                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAlldelete(this.checked);"
                                                                                                                                                                ToolTip="Select All" />
                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkView" runat="server" />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateColumn>
                                                                                                                                                    <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateColumn>
                                                                                                                                                    <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                                                                                                                                    <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                                                                                                                                    <%--<asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                                                        <asp:ListItem Value="7" Selected="true">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="0">Report</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Referral</asp:ListItem>
                                                                                        <asp:ListItem Value="2">AOB</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                                                        <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                                                        <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                                                        <asp:ListItem Value="6">Misc</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>--%>
                                                                                                                                                </Columns>
                                                                                                                                            </asp:DataGrid>
                                                                                                                                            <%--</div>--%>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="center">
                                                                                                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </cc1:TabPanel>
                                                                                                            <cc1:TabPanel runat="server" ID="tabpnlprocedurecodedocs" TabIndex="2">
                                                                                                                <HeaderTemplate>
                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                        Diagnosis Code Section
                                                                                                                    </div>
                                                                                                                </HeaderTemplate>
                                                                                                                <ContentTemplate>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <div style="height: auto; width: 100%; float: left;">
                                                                                                                                            <table width="100%" border="0" class="table-content">
                                                                                                                                                <tr>
                                                                                                                                                    <td align="left">
                                                                                                                                                        <cc1:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                                                                                                                                            TabStripPlacement="Top" Height="350px" Width="100%">
                                                                                                                                                            <cc1:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <div style="width: 100%; height: 240px; float: left; display: inline;" class="lbl">
                                                                                                                                                                        Associate Diagnosis Code</div>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ContentTemplate>
                                                                                                                                                                    <table width="100%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="100%" scope="col" align="left">
                                                                                                                                                                                <div class="blocktitle">
                                                                                                                                                                                    <div class="div_blockcontent">
                                                                                                                                                                                        <table width="100%" border="0">
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td colspan="2">
                                                                                                                                                                                                    <table border="0" cellpadding="3" cellspacing="4" class="ContentTable" style="width: 85%">
                                                                                                                                                                                                        <tr runat="server" id="trDoctorType">
                                                                                                                                                                                                            <td id="Td1" class="ContentLabel" runat="server">
                                                                                                                                                                                                                Diagnosis Type :
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td id="Td2" class="ContentLabel" runat="server">
                                                                                                                                                                                                                <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                                                                                                                                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                                                                                                                                                                                    Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td id="Td3" class="ContentLabel" runat="server">
                                                                                                                                                                                                                Code :
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td id="Td4" runat="server">
                                                                                                                                                                                                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                        <tr>
                                                                                                                                                                                                            <td id="Td5" class="ContentLabel" runat="server">
                                                                                                                                                                                                                Description :
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td id="Td6" class="ContentLabel" runat="server">
                                                                                                                                                                                                                <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td>
                                                                                                                                                                                                                Speciality :
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                            <td>
                                                                                                                                                                                                                <extddl:ExtendedDropDownList ID="extddlSpecialityDia" runat="server" Connection_Key="Connection_String"
                                                                                                                                                                                                                    Flag_Key_Value="GET_SPECIALTY" Procedure_Name="SP_MST_SPECIALTY_LHR"
                                                                                                                                                                                                                    Selected_Text="---Select---" />
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                        <tr>
                                                                                                                                                                                                            <td colspan="4" align="center">
                                                                                                                                                                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                                                                                                                                                <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" OnClick="btnSeacrh_Click" />
                                                                                                                                                                                                                &nbsp;<asp:Button ID="btnAssign" OnClick="btnAssign_Click" runat="server" Text="Assign"
                                                                                                                                                                                                                    Width="80px" Visible="true" />
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                    </table>
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td colspan="2">
                                                                                                                                                                                                    <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                                                                                                                                        <tr runat="server" id="Tr1">
                                                                                                                                                                                                            <td width="100%" runat="server" id="Td8">
                                                                                                                                                                                                                <div style="overflow: scroll; height: 250px; width: 100%; border:1px">
                                                                                                                                                                                                                    <asp:DataGrid Width="99%" ID="grdNormalDgCode" CssClass="GridTable" runat="server"
                                                                                                                                                                                                                        AutoGenerateColumns="False" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                                                                                                        PagerStyle-Mode="NumericPages" OnSelectedIndexChanged="grdNormalDgCode_SelectedIndexChanged">
                                                                                                                                                                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                                                                                                                                                        <Columns>
                                                                                                                                                                                                                            <asp:TemplateColumn>
                                                                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                                                                            </asp:TemplateColumn>
                                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                                                                                                                                                                                                                Visible="False"></asp:BoundColumn>
                                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                                                                                                                                                                                                            </asp:BoundColumn>
                                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                                                                                                                                                                                                        </Columns>
                                                                                                                                                                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                                                    </asp:DataGrid>
                                                                                                                                                                                                                </div>
                                                                                                                                                                                                            </td>
                                                                                                                                                                                                        </tr>
                                                                                                                                                                                                    </table>
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td colspan="2">
                                                                                                                                                                                                    <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td colspan="2">
                                                                                                                                                                                                    <div style="overflow: auto; height: 10%; width: 100%;">
                                                                                                                                                                                                        &nbsp;<asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable"
                                                                                                                                                                                                            runat="server" AutoGenerateColumns="False" Visible="false">
                                                                                                                                                                                                            <ItemStyle CssClass="GridRow" />
                                                                                                                                                                                                            <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                                                                                                                                                </asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                                                                                                                                                </asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                                                                                                                                                                                                </asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                                                                                                                                                                                    Visible="False"></asp:BoundColumn>
                                                                                                                                                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                                                                                                                                                                                            </Columns>
                                                                                                                                                                                                            <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                                        </asp:DataGrid>
                                                                                                                                                                                                    </div>
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td class="tablecellLabel" colspan="2" rowspan="3">
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                        </table>
                                                                                                                                                                                    </div>
                                                                                                                                                                                </div>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </ContentTemplate>
                                                                                                                                                            </cc1:TabPanel>
                                                                                                                                                            <cc1:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                                                                        De-associate Diagnosis Code</div>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ContentTemplate>
                                                                                                                                                                    <table width="100%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td width="100%" scope="col">
                                                                                                                                                                                <div class="blocktitle">
                                                                                                                                                                                    <div class="div_blockcontent">
                                                                                                                                                                                        <table width="100%" border="0">
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td colspan="2">
                                                                                                                                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server" CssClass="GridTable"
                                                                                                                                                                                                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged">
                                                                                                                                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                                                                                                                                        <Columns>
                                                                                                                                                                                                            <asp:TemplateColumn>
                                                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                                                            </asp:TemplateColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                                                                                                                                            </asp:BoundColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                                                                                                                                            </asp:BoundColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                                                                                                                                                                                            </asp:BoundColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                                                                                                                                                                                Visible="False"></asp:BoundColumn>
                                                                                                                                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                                                                                                                                                                                        </Columns>
                                                                                                                                                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                                                                        <PagerStyle Mode="NumericPages" />
                                                                                                                                                                                                    </asp:DataGrid>
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                            <tr>
                                                                                                                                                                                                <td align="center">
                                                                                                                                                                                                    <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="104px"
                                                                                                                                                                                                        OnClick="btnDeassociateDiagCode_Click" />
                                                                                                                                                                                                </td>
                                                                                                                                                                                            </tr>
                                                                                                                                                                                        </table>
                                                                                                                                                                                    </div>
                                                                                                                                                                                </div>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </ContentTemplate>
                                                                                                                                                            </cc1:TabPanel>
                                                                                                                                                        </cc1:TabContainer>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center">
                                                                                                                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="104px" CssClass="Buttons"
                                                                                                                                                            Visible="false" />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                                                                                                        <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                    </ContentTemplate>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </cc1:TabPanel>
                                                                                                            <cc1:TabPanel runat="server" ID="tabpnlSpeciality" TabIndex="3">
                                                                                                                <HeaderTemplate>
                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                        Specialty</div>
                                                                                                                </HeaderTemplate>
                                                                                                                <ContentTemplate>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td align="center">
                                                                                                                                <table width="60%">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            Specialty :
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <extddl:ExtendedDropDownList ID="extddlSpecialityUp" runat="server"  Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                                                                                Procedure_Name="SP_MST_PROCEDURE_GROUP" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="2" align="center">
                                                                                                                                            <asp:Button ID="btnUpdateSpec" runat="server" Text="Update" OnClick="btnUpdateSpec_Click" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </cc1:TabPanel>
                                                                                                            <cc1:TabPanel runat="server" ID="tpAudit" TabIndex="1" Visible="false">
                                                                                                                <HeaderTemplate>
                                                                                                                    <div style="width: 100%; height: 200px; display: inline;" class="lbl">
                                                                                                                        Audit
                                                                                                                    </div>
                                                                                                                </HeaderTemplate>
                                                                                                                <ContentTemplate>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table width="100%" border="0" cellpadding="4" cellspacing="4">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <UserMessage:MessageControl runat="server" ID="MessageControl1" />
                                                                                                                                                </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td align="center" style="width: 80%">
                                                                                                                                            <div style="overflow: scroll; height: 400px; width: 100%;">
                                                                                                                                            <asp:DataGrid ID="dgAudit" Width="95%" runat="server" AutoGenerateColumns="False"
                                                                                                                                                CssClass="GridTable">
                                                                                                                                                <HeaderStyle CssClass="GridHeader1" />
                                                                                                                                                <ItemStyle CssClass="GridViewHeader" />
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:BoundColumn DataField="dt_created_date" HeaderText="Created Date"></asp:BoundColumn>
                                                                                                                                                    <asp:BoundColumn DataField="sz_description" HeaderText="Description"></asp:BoundColumn>
                                                                                                                                                    <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                                                                                                                                                    <asp:BoundColumn DataField="i_event_id" HeaderText="Event ID"></asp:BoundColumn>
                                                                                                                                                </Columns>
                                                                                                                                            </asp:DataGrid>
                                                                                                                                            </div>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </cc1:TabPanel>
                                                                                                        </cc1:TabContainer>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="right">
                                                                                                        <asp:CheckBox ID="chkClose" runat="server" Text=" Always close this window on save operation" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="img12" runat="server" Style="position: absolute; z-index: 1; left: 50%;
                                                            top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                                                            Height="60px" Width="60px"></asp:Image>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </table>
                                            <asp:TextBox ID="txtEventProcId" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtSpecility" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtProcGroupId" runat="server" Visible="false"></asp:TextBox>
                                            <%-- <asp:TextBox ID="txtCaseId" runat="server" Visible="false"></asp:TextBox>--%>
                                            <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox><asp:TextBox
                                                ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtdesc" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtcode" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtprocid" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtProcDesc" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtUserId" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtUserName" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtUserRole" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txt_event_ProcID" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txt_event_ID" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
