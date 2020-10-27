<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupWC42.aspx.cs" Inherits="AJAX_Pages_PopupWC42" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" type="text/css" rel="stylesheet" />
    <script src="calendarPopup.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td height="20px" colspan="6" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 454px">
                        <table width="100%" id="content_table" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="15%" valign="top" scope="col">
                                    <div class="blocktitle_ql">
                                        <div align="left" class="blocktitle_adv">
                                            Jump To
                                        </div>
                                        <div class="div_blockcontent">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;">
                                                            <li><a href="Bill_Sys_PatientInformation.aspx">Patient Information </a></li>
                                                            <li><a href="Bill_Sys_WorkerTemplate.aspx">Employer Information </a></li>
                                                            <li><a href="Bill_Sys_DoctorsInformation.aspx">Doctor's Information </a></li>
                                                            <li><a href="Bill_Sys_BillingInformation.aspx">Billing Information </a></li>
                                                            <li><a href="Bill_Sys_History.aspx">History </a></li>
                                                            <li><a href="Bill_Sys_ExamInformation.aspx">Exam Information </a></li>
                                                            <li><a href="Bill_Sys_DoctorOpinion.aspx">Doctor's Opinion </a></li>
                                                            <li><a href="Bill_Sys_PlanOfCare.aspx">Plan Of Care </a></li>
                                                            <li><a href="Bill_Sys_WorkStatus.aspx">Work Status </a>
                                                                <br />
                                                            </li>
                                                        </ul>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                                <td width="69%" scope="col">
                                    <div class="blocktitle">
                                        Patient Information
                                        <div class="div_blockcontent">
                                            <table>
                                                <tr>
                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                        <div align="left">
                                                            <table>
                                                                <tr>
                                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                                        <div align="left">
                                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            1.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            Patient Name:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtPatientFirstName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPatientMiddleName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPatientLastName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <div class="lbl">
                                                                                            First Name</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Middle Name</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Last Name</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            2.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Soc. Sec. #</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtSocialSecurity" runat="server"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            3. Home Phone #</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtHomePhone" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            4.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            WCB Case #</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWCBCase" runat="server"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            5. CARRIER Case #</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtCarrierCaseNo" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            6.</div>
                                                                                    </td>
                                                                                    <td style="width: 200px">
                                                                                        <div class="lbl">
                                                                                            Mailing Address</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 58px">
                                                                                    </td>
                                                                                    <td style="width: 93px; height: 58px;">
                                                                                        <div class="lbl">
                                                                                            Number and Street</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 58px;">
                                                                                        <asp:TextBox ID="txtPatientStreet" runat="server"></asp:TextBox></td>
                                                                                    <td style="height: 58px">
                                                                                        <div class="lbl">
                                                                                            City</div>
                                                                                    </td>
                                                                                    <td style="height: 58px">
                                                                                        <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                        <div class="lbl">
                                                                                            State</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <cc1:ExtendedDropDownList ID="txtPatientState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                                        </cc1:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                        <div class="lbl">
                                                                                            Zip Code</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            7.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Date of injury/ Illness</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <div class="lbl">
                                                                                            <asp:TextBox ID="txtDateOfInjury" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnDateOfInjury" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfInjury"
                                                                                                PopupButtonID="imgbtnDateOfInjury" />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            8.Date of Birth</div>
                                                                                    </td>
                                                                                    <td style="width: 80px">
                                                                                        <div class="lbl">
                                                                                            <asp:TextBox ID="txtDateOfBirth" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnDateOfBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfBirth"
                                                                                                PopupButtonID="imgbtnDateOfBirth" />
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 52px">
                                                                                        <div class="lbl">
                                                                                            9.</div>
                                                                                    </td>
                                                                                    <td style="width: 93px; height: 52px;">
                                                                                        <div class="lbl">
                                                                                            Gender</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 52px;">
                                                                                        <asp:CheckBox ID="chkGenderMale" runat="server" Text="Male" Font-Bold="false" Font-Size="Small" />
                                                                                        <asp:CheckBox ID="ChkGenderFemale" runat="server" Text="Female" Font-Bold="false"
                                                                                            Font-Size="Small" />
                                                                                    </td>
                                                                                    <td style="height: 52px;">
                                                                                        <div class="lbl">
                                                                                            10.Patient Account #.</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 52px;">
                                                                                        <asp:TextBox ID="txtCaseID" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                                                    <td style="height: 52px">
                                                                                    </td>
                                                                                    <td style="height: 52px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            11.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            On date of injury/illness what was Patient's job and description:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtJobTitle" runat="Server" TextMode="MultiLine" 
                                                                                            Width="300px" onkeyDown="return checkTextAreaMaxLength(this,event,'10');"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            12.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            On date of injury/illness what were Patient's usual work activities:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtWorkActivities" runat="Server"  TextMode="MultiLine"
                                                                                            Width="300px" MaxLength="233"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div id="div_contect_field_buttons">
                                                                            <table>
                                                                                <tr>
                                                                                    <td colspan="4" width="100%" align="center">
                                                                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>   
                                                                                        <asp:TextBox ID="txtchkmale" runat="server" Width="10px" Style="visibility: hidden;"></asp:TextBox>
                                                                                        <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                                            CssClass="btn-gray"  /><%--OnClick="btnSaveAndGoToNext_Click"--%>
                                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                                        <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                                                                    <asp:HiddenField ID="btnhidden" runat="server"  />
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </th>
                                                                </tr>
                                                            </table>
                                                    </th>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    <div>
    
    <asp:HiddenField ID="hdlCasID" runat="server">
    </asp:HiddenField>
    <asp:HiddenField ID="hdnPatientID" runat="server">
    </asp:HiddenField>
    </div>
    </form>
</body>
</html>
