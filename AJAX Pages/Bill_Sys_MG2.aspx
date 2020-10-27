<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_MG2.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_MG2" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">

        function showPopup(caseid, i_id) {
            alert("Page under construction. Will be upload soon.");
            alert(i_id);
            var url = 'Bill_Sys_MG2_Convert_to_Bill.aspx';
            ShowPopup.SetContentUrl(url);
            ShowPopup.Show();
            return false;
        }

        function showPopup2(caseid, path) {
            alert(path);
            window.open(path);

            //alert("Page under construction.  Will be upload soon.");
//            alert(caseid);
//            alert(path);
//            var url = 'Bill_Sys_MG2_Convert_to_Bill.aspx';
//            ShowPopup.SetContentUrl(url);
//            ShowPopup.Show();
            return false;
        }

        function Clear() {
            
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtIndividualProvider').value = '';            
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtTelephone').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtFaxNo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtApproval').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtDateofService').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtDateofApplicable').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtSpoke').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_Txtspecktoanyone').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtAddressRequired').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_Txtaboveon').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtProviderDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtByPrintNameD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtTitleD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtDateD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtSectionE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtMedicalProfes').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtByPrintNameE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtTitleE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtDateE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtByPrintNameF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtTitleF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtDateF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtClaimantDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuislineChar').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline1').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline2').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline3').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline4').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline5').value = '';

            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_Chkdid').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_Chkdidnot').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkAcopy').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkIAmnot').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChktheSelf').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkGranted').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkGrantedinPart').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkWithoutPrejudice').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkDenied').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkBurden').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkSubstantially').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkMadeE').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkChairE').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkIrequestG').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkMadeG').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ChkChairG').checked = false

            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ddlGuidline').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_DDLAttendingDoctors').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_extddlSpeciality').selectedIndex = 0;
                        
            document.getElementById('<%= grdProcedure.ClientID %>').style.display = 'none';

            return false;
        }

        function SplitS(combo) {
            //alert('hi');
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuislineChar').value = '';

            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline1').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline2').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline3').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline4').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline5').value = '';


            var selected = combo.options[combo.selectedIndex].value;
            selected = selected.replace('-', '');
            for (var i = 0; i < selected.length; i++) {
                switch (i) {
                    case 0:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuislineChar').value = selected[0];
                        break;
                    case 1:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline1').value = selected[1];
                        break;
                    case 2:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline2').value = selected[2];
                        break;
                    case 3:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline3').value = selected[3];
                        break;
                    case 4:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline4').value = selected[4];
                        break;
                    case 5:
                        document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_TxtGuidline5').value = selected[5];
                        break;                        
                }
            }
        }

    </script>
    <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
        Width="100%" OnActiveTabChanged="carTabPage_ActiveTabChanged" 
        AutoPostBack="True">
        <TabPages>
            <dx:TabPage Text="NEW MG2" TabStyle-Width="100%">
                <TabStyle Width="100%">
                </TabStyle>
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 414px" align="center">
                                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 226px">
                                </td>
                                <td style="height: 26px" align="center">
                                    <asp:Label ID="lblmsg" runat="server" Text="You already have 1 MG2 document associated with the bill. Click"
                                        Visible="false"></asp:Label>
                                    <asp:HyperLink ID="hyLinkOpenPDF" runat="server" Text="here" Target="_blank" Visible="false"></asp:HyperLink>
                                    <asp:Label ID="lblmsg2" runat="server" Text=" to view." Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 298px; height: 26px">
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="81px" Visible="False" />
                                </td>
                                <td style="width: 90px; height: 26px;">
                                    <asp:Button ID="btnsave" runat="server" Text="Save" Width="71px" OnClick="btnsave_Click" />
                                </td>
                                <td style="height: 26px; width: 103px;">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="76px" OnClick="btnPrint_Click" />
                                </td>
                                <td style="height: 26px">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear"
                                        Width="76px" />                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 298px">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table style="padding-right: 60px; width: 100%">
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; width: 85%">
                                    WCB Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtwcbcasenumber" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Carrier Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtCarrierCaseNo" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Date of Injury :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofInjury" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">
                                    A.&nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Patient's First Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFirstName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Middle Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMiddleName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Last Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtLastName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Social Security No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSocialSecurityNo" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Patient's Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPatientAddress" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Employer's Name & Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtEmployerNameAdd" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Insurance Carrier's Name & Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtInsuranceNameAdd" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">
                                    B.&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Attending Doctor's Name & Address :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLAttendingDoctors" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLAttendingDoctors_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp&nbsp&nbsp Individual Provider's WCB Authorization No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtIndividualProvider" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp;&nbsp;&nbsp;&nbsp; Telephone No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTelephone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp;&nbsp;&nbsp;&nbsp; Fax No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFaxNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">
                                    C.&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    The undersigned requests approval to VARY from the WCB Medical Treatment Guidelines
                                    as indicated below:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;" valign="top">
                                    &nbsp;&nbsp;&nbsp;&nbsp; Guideline Reference :
                                </td>
                                <td valign="top">
                                    <%-- <asp:DropDownList ID="ddlGuidline" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGuidline_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlGuidline" runat="server" OnSelectedIndexChanged="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);">
                                        <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="K-E7E  ">K-E7E  </asp:ListItem>
                                        <asp:ListItem Value="B-D9A  ">B-D9A  </asp:ListItem>
                                        <asp:ListItem Value="N-D10D ">N-D10D </asp:ListItem>
                                        <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                         <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                         <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TxtGuislineChar" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                    -
                                    <asp:TextBox ID="TxtGuidline1" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline2" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline3" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline4" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline5" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    (In first box, indicate body part: K = Knee, S = Shoulder, B = Mid and Low Back,
                                    N = Neck, C = Carpal Tunnel. In remaining boxes, indicate corresponding section
                                    of WCB Medical Treatment Guidelines. If the treatment requested is not addressed
                                    by the Guidelines, in the remaining boxes use NONE.)
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp;&nbsp; Approval Requested for: (one request type per form)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtApproval" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp Date of service of supporting medical in WCB case file, if not attached:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofService" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TxtDateofService"
                                        PopupButtonID="ImageButton1" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp;&nbsp; Date(s) of previously denied variance request for substantially similar
                                    treatment, if applicable:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofApplicable" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TxtDateofApplicable"
                                        PopupButtonID="ImgDate" Enabled="True" />
                                    <%--   <a id="trigger" href="#">
                        <input type="image" name="mgbtnDateofService" id="imgbtnDateofService" runat="server"
                            src="Images/cal.gif" border="0" /></a>--%>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp;&nbsp; I certify that I am making the above request for approval of a variance
                                    and my affirmative statements are true and correct. I certify that I have read and
                                    applied the Medical Treatment Guidelines to the treatment and care in this case
                                    and that I am requesting this variance before rendering any medical care that varies
                                    from the Guidelines. I certify that the claimant understands and agrees to undergo
                                    the proposed medical care. i
                                    <asp:CheckBox ID="Chkdid" runat="server" />
                                    did /
                                    <asp:CheckBox ID="Chkdidnot" runat="server" />
                                    did not contact the carrier by telephone to discuss this variance request before
                                    making the request. I contacted the carrier by telephone on and
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="TxtSpoke" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp spoke to (person spoke to or was not able to speck to anyone)
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtspecktoanyone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp
                                    <asp:CheckBox ID="ChkAcopy" runat="server" />
                                    A copy of this form was sent to the carrier/employer/self-insured employer/Special
                                    Fund by &nbsp&nbsp (fax number or email address required)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAddressRequired" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp A copy was sent to the Workers' Compensation Board, and copies were provided
                                    to the claimant’s legal counsel, if any, to the claimant if not represented, and
                                    to any other parties of interest within two (2) business days of the date below.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp
                                    <asp:CheckBox ID="ChkIAmnot" runat="server" />
                                    &nbsp; I am not equipped to send or receive forms by fax or email. This form was
                                    mailed to the parties indicated above on
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtaboveon" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp In addition, I certify that I do not have a substantially similar request
                                    pending and that this request contains additional supporting medical evidence if
                                    it is substantially similar to a prior denied request.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtProviderDate" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="ImgProviderDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TxtProviderDate"
                                        PopupButtonID="ImgProviderDate" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    THE WORKERS' COMPENSATION BOARD EMPLOYS AND SERVES PEOPLE WITH DISABILITIES WITHOUT
                                    DISCRIMINATION.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp Patient Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPatientName" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp WCB Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtWCBCaseNumber2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp Date of Injury :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofInjury2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">
                                    D.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    &nbsp&nbsp CARRIER'S / EMPLOYER'S NOTICE OF INDEPENDENT MEDICAL EXAMINATION (IME)
                                    OR MEDICAL RECORDS REVIEW
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChktheSelf" runat="server" />
                                    The self-insurer/carrier hereby gives notice that it will have the claimant examined
                                    by an Independent Medical Examiner or the claimant's medical records reviewed by
                                    a Records Reviewer and submit Form IME-4 within 30 calendar days of the variance
                                    request.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameD" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitleD" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <%--  Signature:--%>
                                    <asp:TextBox ID="TxtSignatureD" runat="server" Visible="false"></asp:TextBox>
                                    &nbsp; Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateD" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateD" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TxtDateD"
                                        PopupButtonID="ImgbtnDateD" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">
                                    E.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    CARRIER'S / EMPLOYER'S RESPONSE TO VARIANCE REQUEST
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" valign="top" class="td-CaseDetails-lf-desc-ch">
                                    Carrier's response to the variance request is indicated in the checkboxes on the
                                    right. Carrier denial, when appropriate, should be reviewed by a health professional.
                                    (Attach written report of medical professional.) If request is approved or denied,
                                    sign and date the form in Section E.<br />
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSectionE" runat="server" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    CARRIER'S / EMPLOYER'S RESPONSE
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    If service is denied or granted in part, explain in space provided.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkGranted" runat="server" />
                                    Granted
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkGrantedinPart" runat="server" />
                                    Granted in Part
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkWithoutPrejudice" runat="server" />
                                    Without Prejudice
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkDenied" runat="server" />
                                    Denied
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkBurden" runat="server" />
                                    Burden of Proof Not Met
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkSubstantially" runat="server" />
                                    Substantially Similar Request Pending or Denied
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    Name of the Medical Professional who reviewed the denial, if applicable:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMedicalProfes" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    I certify that copies of this form were sent to the Treating Medical Provider requesting
                                    the variance, the Workers' Compensation Board, the claimant's legal counsel, if
                                    any, and any other parties of interest, with the written report of the medical professional
                                    in the office of the carrier/employer/selfinsured employer/Special Fund attached,
                                    within two (2) business days of the date below.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    (Please complete if request is denied.) If the issue cannot be resolved informally,
                                    I opt for the decision to be made
                                    <%-- </td>
                <td>--%>
                                    <asp:CheckBox ID="ChkMadeE" runat="server" />
                                    <%-- </td> 
                <td class="td-CaseDetails-lf-desc-ch">--%>
                                    by the Medical Arbitrator designated by the Chair
                                    <%--   </td>
                <td>--%>
                                    <asp:CheckBox ID="ChkChairE" runat="server" />
                                    <%--  </td> 
                <td class="td-CaseDetails-lf-desc-ch">--%>
                                    or at a WCB Hearing. I understand that if either party, the carrier or the claimant,
                                    opts in writing for resolution at a WCB hearing; the decision will be made at a
                                    WCB hearing. I understand that if neither party opts for resolution at a hearing,
                                    the variance issue will be decided by a medical arbitrator and the resolution is
                                    binding and not appealable under WCL § 23.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameE" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitleE" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateE" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateE" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtDateE"
                                        PopupButtonID="ImgbtnDateE" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: center;" class="td-CaseDetails-lf-desc-ch">
                                    F.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    DENIAL INFORMALLY DISCUSSED AND RESOLVED BETWEEN PROVIDER AND CARRIER
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    I certify that the provider's variance request initially denied above is now granted
                                    or partially granted.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameF" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitleF" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateF" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateF" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDateF"
                                        PopupButtonID="ImgbtnDateF" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: center;" class="td-CaseDetails-lf-desc-ch">
                                    G.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    CLAIMANT'S / CLAIMANT REPRESENTATIVE'S REQUEST FOR REVIEW OF SELF-INSURED EMPLOYER'S
                                    / CARRIER'S DENIAL
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    NOTE to Claimant's / Claimant Licensed Representative's: The claimant should only
                                    sign this section after the request is fully or partially denied. This section should
                                    not be completed at the time of initial request.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    YOU MUST COMPLETE THIS SECTION IF YOU WANT THE BOARD TO REVIEW THE CARRIER'S DENIAL
                                    OF THE PROVIDER'S VARIANCE REQUEST.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkIrequestG" runat="server" />
                                    I request that the Workers' Compensation Board review the carrier's denial of my
                                    doctor's request for approval to vary from the Medical Treatment Guidelines. I opt
                                    for the decision to be made
                                    
                                    <asp:CheckBox ID="ChkMadeG" runat="server" />
                                    
                                    by the Medical Arbitrator designated by the Chair
                                    <
                                    <asp:CheckBox ID="ChkChairG" runat="server" />
                                    
                                    or at a WCB Hearing. I understand that if either party, the carrier or the claimant,
                                    opts in writing for resolution at a WCB hearing; the decision will be made at a
                                    WCB hearing. I understand that if neither party opts for resolution at a hearing,
                                    the variance issue will be decided by a medical arbitrator and the resolution is
                                    binding and not appealable under WCL § 23.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    <%-- Claimant's / Claimant Representative's Signature:--%>
                                    <asp:TextBox ID="TxtClairmantSignature" runat="server" Visible="false"></asp:TextBox>&nbsp;
                                    Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtClaimantDate" runat="server" ></asp:TextBox>
                                    <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="TxtClaimantDate"
                                        PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED,
                                    OR PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER,
                                    OR SELF-INSURER, ANY INFORMATION CONTAINING ANY FALSE MATERIAL STATEMENT OR CONCEALS
                                    ANY MATERIAL FACT SHALL BE GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND
                                    IMPRISONMENT.
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" align="center" class="td-CaseDetails-lf-desc-ch">
                                    NYS Workers' Compensation Board, Centralized Mailing, PO Box 5205, Binghamton, NY
                                    13902-5205 Customer Service Toll-Free Number: 877-632-4996
                                </td>
                                <td>
                                    &nbsp
                                    <asp:TextBox ID="TxtSignatureF" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td-CaseDetails-lf-desc-ch" style="height: 21px; text-align: right;">
                                    Speciality
                                </td>
                                <td>
                                    <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" AutoPost_back="true"
                                        Connection_Key="Connection_String" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                        OldText="" Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="---Select---"
                                        StausText="False" Width="100px" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td-CaseDetails-lf-desc-ch" style="height: 21px; text-align: right;"
                                    colspan="2">
                                    <div style="width:60%; height:200px; overflow:scroll">
                                    <asp:DataGrid ID="grdProcedure" runat="server" AutoGenerateColumns="False" CssClass="GridTable"
                                        DataKeyField="SZ_TYPE_CODE_ID">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID ID" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" BackColor="#B5DF82" HorizontalAlign="Left" />
                                        <ItemStyle CssClass="GridRow" HorizontalAlign="Left" />
                                    </asp:DataGrid>
                                        </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" align="center" class="td-CaseDetails-lf-desc-ch">
                                    <asp:TextBox ID="TxtSignatureE" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtProviderSig" runat="server" Visible="false"></asp:TextBox>FAX
                                    NUMBER: 877-533-0337 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp;&nbsp; &nbsp;E-MAIL TO: wcbclaimsfiling@wcb.ny.gov
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch">
                                    <%--Provider's Signature:--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 298px; height: 26px">
                                    &nbsp;
                                    <asp:Button ID="btnCancelBottom" runat="server" Text="Cancel" Width="81px" Visible="False" />
                                    <asp:TextBox ID="txtbillno" runat="server" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width: 90px; height: 26px;">
                                    <asp:Button ID="btnSaveBottom" runat="server" Text="Save" Width="71px" OnClick="btnSaveBottom_Click" />
                                </td>
                                <%--Width="86px"--%>
                                <td style="height: 26px; width: 103px;">
                                    <asp:Button ID="btnPrinBottom" runat="server" Text="Print" Width="76px" OnClick="btnPrinBottom_Click" />
                                </td>
                                <td style="height: 26px">
                                    <asp:Button ID="btnClearBottom" runat="server" 
                                        Text="Clear" Width="76px" />                                    
                                </td>
                                <%--<td>
                </td>     --%>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="AVAILABLE MG2" TabStyle-Width="100%">
                <TabStyle Width="100%">
                </TabStyle>
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" KeyFieldName="I_ID" runat="server" AutoGenerateColumns="True"  SettingsPager-PageSize="20" OnRowCommand="ASPxGridView1_RowCommand" >
                            <Columns>
                                <dx:GridViewDataColumn FieldName="VisitDate" Caption="Visit Date" VisibleIndex="1"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CreatedBy" Caption="Created By" VisibleIndex="2"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="View" Settings-AllowSort="False" Width="25px" VisibleIndex="3">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                 <Settings AllowSort="False"></Settings>
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" Text='View'   CommandName="view">
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="" VisibleIndex="4" Visible="false">
                                    <DataItemTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkBill" runat="server" Text="Convert to Bill" Enabled="false" CommandName=""
                                            OnClick='<%# "showPopup(" + "\""+ Eval("SZ_CASE_ID") + "\"" + "," + "\""+ Eval("I_ID") +"\"); return false;" %>'></asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>

                                 <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="SZ_CASE_ID" VisibleIndex="6" Visible="false"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="MG2_Path" Caption="MG2_Path" VisibleIndex="7" Visible="false"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="I_ID" Caption="I_ID" VisibleIndex="7" Visible="false"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>

                                <%--<dx:GridViewDataColumn FieldName="STATUS" Caption="STATUS" VisibleIndex="8" Visible="true"
                                    Width="120px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataColumn>--%>
                                
                            </Columns>

<SettingsPager PageSize="20"></SettingsPager>
                        </dx:ASPxGridView>
                        <asp:HiddenField ID="hdID" runat="server" />
                        <asp:HiddenField ID="hdSpeciality" runat="server" />
                        <asp:HiddenField ID="hdCmpName" runat="server" />
                        <asp:HiddenField ID="hdLogicalpath" runat="server" />
                        <asp:HiddenField ID="hdNodeID" runat="server" />
                        <asp:HiddenField ID="hdNodeName" runat="server" />
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <dx:ASPxPopupControl ID="ShowPopup" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup"
        HeaderText="Convert to Bill" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="900px"
        ToolTip="ConverttoBill" PopupHorizontalOffset="0" PopupVerticalOffset="0"  
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
        
    </dx:ASPxPopupControl>
</asp:Content>