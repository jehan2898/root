<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ReferralBillTransaction.aspx.cs" Inherits="Bill_Sys_ReferralBillTransaction" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <script type="text/javascript" src="../js/lib/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="validation.js"></script>
    <script language="javascript" type="text/javascript">


       function FormValication() {
           var objDoctor = document.getElementById('_ctl0_ContentPlaceHolder1_extddlDoctor').value;
           var objReadingDoctor = document.getElementById('_ctl0_ContentPlaceHolder1_extddlReadingDoctor').value;
           var objDiagCodes = document.getElementById('_ctl0_ContentPlaceHolder1_lstDiagnosisCodes').length;

           var objGrid = document.getElementById("_ctl0_ContentPlaceHolder1_grdTransactionDetails");
           var objGridCount = 0;
           if (objGrid != null) {
               objGridCount = objGrid.getElementsByTagName("input").length;
           }

           var szMessage = "";

           szMessage = "Select ";
           if (objDoctor == "NA")
               szMessage += "Doctor ,";



           if (objReadingDoctor == "NA")
               szMessage += "Reading Doctor ,";

           if (objDiagCodes == "0")
               szMessage += "Diagnosis Code ,";

           if (objGridCount == "0")
               szMessage += "Procedure Code ,";

           if (szMessage == "Select ")
               return true;
           else {
               alert(szMessage);
               return false;
           }
       }


       function Amountvalidate() {
           var status = formValidator('frmBillTrans', 'txtBillNo,extddlIC9Code,txtUnit,txtAmount,txtWriteOff,txtDescription');
           if (status != false) {

               if (document.getElementById('_ctl0_ContentPlaceHolder1_txtAmount').value > 0)//&& document.getElementById('txtAmount')!= '' && document.getElementById('txtAmount')!= '0.00')
               {
                   return true;
               }
               else {

                   document.getElementById('ErrorDiv').innerHTML = 'Enter the amount greater than 0';
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtAmount').focus();
                   return false;
               }
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

       function ShowGrid() {
           document.getElementById('divCollapsablegrid').style.visibility = "visible";
       }

       function off() {
           document.getElementById('divCollapsablegrid').style.visibility = "visible";
       }

       function CalculateAmount() {
           var txtAmount = document.getElementById('_ctl0_ContentPlaceHolder1_txtAmount');
           var txtUnit = document.getElementById('_ctl0_ContentPlaceHolder1_txtUnit');
           var tempAmt = document.getElementById('_ctl0_ContentPlaceHolder1_txtTempAmt');
           if (txtAmount.value != "") {
               txtAmount.value = tempAmt.value * txtUnit.value;
           }
       }

       function clickButton1(e, charis) {
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

       function openDiagnosisPage(obj) {
           document.getElementById('divid2').style.zIndex = 1;
           document.getElementById('divid2').style.position = 'absolute';
           document.getElementById('divid2').style.left = '350px';
           document.getElementById('divid2').style.top = '200px';
           document.getElementById('divid2').style.visibility = 'visible';
           document.getElementById('iframeAddDiagnosis').src = "Bill_Sys_ReferalPopUpDiagnosisCode.aspx";
           return false;
       }

       function CloseSource() {
           document.getElementById('divid2').style.visibility = 'hidden';
           document.getElementById('iframeAddDiagnosis').src = '';
           window.parent.document.location.href = 'Bill_Sys_ReferralBillTransaction.aspx';
       }


       function showDiagnosisCodePopup() {
           document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.height = '180px';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.position = "absolute";
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.top = '300px';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.left = '700px';
            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 

            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
            MA.length = 0;
        }

        function CloseDiagnosisCodePopup() {
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.height = '0px';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.visibility = 'hidden';
            //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
        }




        //        function checkExistingBills(obj)
        //       {
        //            if(obj != "0")
        //            {
        //                if(confirm('You are regenerating bill on today’s date, Do you want to proceed?'))
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //       }

        function openExistsPage1() {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute';
            document.getElementById('div1').style.left = '360px';
            document.getElementById('div1').style.top = '100px';
            document.getElementById('div1').style.visibility = 'visible';
            return false;
        }

        function CancelExistPatient1() {
            //document.getElementById('divid2').style.zIndex = 1;
            //document.getElementById('divid2').style.position = 'absolute'; 
            //document.getElementById('divid2').style.left= '360px'; 
            //document.getElementById('divid2').style.top= '100px';
            document.getElementById('div1').style.height = '0px';
            document.getElementById('div1').style.visibility = 'hidden';
            return false;
        }

        function checkExistingBills(obj, objCaseType, objBillNumber, objSpeciality, Comp) {

            if (obj != "0") {
                if (confirm('The bill will be re-generated with the original bill date. Click [OK] to PROCEED [Cancel] to ABORT.')) {
                    if (objCaseType == "WC000000000000000001") {
                        if (Comp != "True") {
                            document.getElementById('<%= hdnWCPDFBillNumber.ClientID %>').value = objBillNumber
                            document.getElementById('<%= hdnSpeciality.ClientID %>').value = objSpeciality;
                            showPDFWorkerComp();
                            return false;
                        }
                        else {
                            // alert("Reffering facility");
                            document.getElementById('<%= hdnWCPDFBillNumber.ClientID %>').value = objBillNumber
                            document.getElementById('<%= hdnSpeciality.ClientID %>').value = objSpeciality;
                            //btnGenerateWCPDF.click();
                            return true;
                        }
                    }
                    else if (objCaseType == "WC000000000000000002") {
                        return true;
                    }
                }
                else {
                    return false;
                }

            } else {
                if (objCaseType == "WC000000000000000001") {
                    if (Comp != "True") {
                        document.getElementById('<%= hdnWCPDFBillNumber.ClientID %>').value = objBillNumber
                        document.getElementById('<%= hdnSpeciality.ClientID %>').value = objSpeciality;
                        showPDFWorkerComp();
                        return false;
                    }
                    else {
                        // alert("Reffering facility");
                        document.getElementById('<%= hdnWCPDFBillNumber.ClientID %>').value = objBillNumber
                        document.getElementById('<%= hdnSpeciality.ClientID %>').value = objSpeciality;
                        //btnGenerateWCPDF.click();

                        return true;
                    }

                }
                else if (objCaseType == "WC000000000000000002") {
                    return true;
                }
            }
        }

        function showPDFWorkerComp() {
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.height = '180px';
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.position = "absolute";
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.top = '500px';
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.left = '650px';
        }
        function ClosePDFWorkerComp() {
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.height = '0px';
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.visibility = 'hidden';
        }

    </script>
    <script type="text/javascript">
        function ShowDignosisPopup() {
            //var url = "Dignosis_Code_Popup.aspx";
            var url = "AJAX Pages/Dignosis_Code_Popup.aspx";
            DignosisPopup.SetContentUrl(url);
            DignosisPopup.Show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function fncParent(array) {
            var htmlSelect = document.getElementById('<%=lstDiagnosisCodes.ClientID%>');
            var SeletedDGCodes = document.getElementById('<%=hndSeletedDGCodes.ClientID%>');
            SeletedDGCodes.value = array;
            var button = document.getElementById('<%=btnAddDGCodes.ClientID%>');
            DignosisPopup.Hide();
            button.click();
        }
    </script>
    <script type="text/javascript">
        function DeleteDignosisCodes12() {
            var count = 0;
            var list = document.getElementById("<%= lstDiagnosisCodes.ClientID %>");
            for (var i = 0; i < list.options.length; i++) {
                if (list.options[i].selected == true) {
                    list.options[i].remove();
                    count++;
                }
            }

            if (count == 0) {
                alert('Select Diagnosis codes from list.');
            }
        }
    </script>
    <script type="text/javascript">
        function DeleteDignosisCodes() {
            var button = document.getElementById('<%=btnRemoveDGCodes.ClientID%>');
            button.click();
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
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <a href="#" onclick="return openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="3" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" runat="server" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case No:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtCaseNo" runat="server" BorderStyle="None" Style="border-top-style: none;
                                                        border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                        BorderColor="Transparent" ReadOnly="True"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseID" runat="server" BorderStyle="None" Style="border-top-style: none;
                                                        border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                        BorderColor="Transparent" ReadOnly="True" Visible="false"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Date:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtBillDate" runat="server" Width="150px" MaxLength="10" ReadOnly="true"
                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <%--<td class="ContentLabel" style="width: 15%">
                                                    Bill Date:</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtBillDate" runat="server" Width="150px" MaxLength="10" ReadOnly="true" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                   <%-- <asp:ImageButton ID="imgbtnOpenedDate" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBillDate"
                                                        PopupButtonID="imgbtnOpenedDate" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Billing Doctor:
                                                </td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Reading Doctor:
                                                </td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="97%"
                                                        Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_READINGDOCTOR"
                                                        Selected_Text="---Select---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Speciality:
                                                </td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlspeciality" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETROOMID" Procedure_Name="SP_GET_REFF_PROCEDURE_CODES" Selected_Text="---Select---"
                                                        OnextendDropDown_SelectedIndexChanged="extddlspeciality_SelectedIndexChanged"
                                                        AutoPost_back="true" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 10px;">
                                                </td>
                                                <td style="width: 35%; height: 10px;">
                                                    <asp:DropDownList ID="ddlType" runat="server" Visible="false" Width="94%">
                                                        <asp:ListItem Selected="True" Value="0"> --Select--</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000001">Visit</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000002">Treatment</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000003">Test</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 35%; height: 10px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; vertical-align: top;">
                                                    Diagnosis Code:
                                                </td>
                                                <td colspan="2" style="width: 50%; vertical-align: top;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="60%">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="lstDiagnosisCodes" runat="server" Width="100%" SelectionMode="Multiple">
                                                                            </asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <%--<a href="#" onclick="showDiagnosisCodePopup();" style="font-size: 12px; vertical-align: top;">
                                                                                Add Diagnosis</a></td>--%>
                                                                            <asp:LinkButton ID="lnkAddDiagnosis" runat="server" Text="Add Diagnosis" OnClientClick="ShowDignosisPopup();return false"
                                                                                Style="text-align: right; font-size: 12px; vertical-align: top;">
                                                                            </asp:LinkButton>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%-- <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px;
                                                                                vertical-align: top;" OnClick="lnkbtnRemoveDiag_Click"></asp:LinkButton></td>--%>
                                                                            <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px;
                                                                                vertical-align: top;" OnClientClick="DeleteDignosisCodes();return false"></asp:LinkButton>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 35%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; vertical-align: top;" visible="false">
                                                </td>
                                                <td style="width: 35%; vertical-align: top;">
                                                    <asp:RadioButton ID="rdoOn" runat="server" Visible="false" Text="On" Checked="True"
                                                        GroupName="OnFromTO" AutoPostBack="true" OnCheckedChanged="rdoOn_CheckedChanged" />
                                                    <asp:RadioButton ID="rdoFromTo" runat="server" Visible="false" Text="From To" GroupName="OnFromTO"
                                                        AutoPostBack="true" OnCheckedChanged="rdoFromTo_CheckedChanged" />
                                                </td>
                                                <td style="width: 50%" colspan="2">
                                                    <asp:Label ID="lblDateOfService" runat="server" Text="From" Font-Bold="False" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtDateOfservice" runat="server" Width="120px" MaxLength="50" ReadOnly="false"
                                                        onkeypress="return CheckForInteger(event,'/')" Visible="false"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofService" runat="server" ImageUrl="~/Images/cal.gif"
                                                        Visible="false" />&nbsp;
                                                    <asp:Label ID="lblTo" runat="server" Text="To" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtDateOfServiceTo" runat="server" MaxLength="50" onkeypress="return CheckForInteger(event,'/')"
                                                        ReadOnly="false" Width="120px" Visible="false"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromTo" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfservice"
                                                        PopupButtonID="imgbtnDateofService" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateOfServiceTo"
                                                        PopupButtonID="imgbtnFromTo" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Button ID="btnAddService" runat="server" Text="Add Services" Width="80px" CssClass="Buttons"
                                                        OnClick="btnAddService_Click" Visible="false" />
                                                    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="80px" CssClass="Buttons" Visible="false" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtBillID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtTransDetailID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtReferringCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
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
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" class="TDPart">
                                        <div style="overflow: scroll; height: 150px; width: 95%;">
                                            <asp:DataGrid ID="grdTransactionDetails" runat="server" PageSize="5" OnSelectedIndexChanged="grdTransactionDetails_SelectedIndexChanged"
                                                AutoGenerateColumns="False" Width="96%" CssClass="GridTable">
                                                <PagerStyle Mode="NumericPages" />
                                                <ItemStyle CssClass="GridRow" />
                                                <HeaderStyle CssClass="GridHeader" />
                                                <Columns>
                                                    <%--   0--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_TXN_DETAIL_ID" HeaderText="Transaction Detail ID"
                                                        Visible="False"></asp:BoundColumn>
                                                    <%--    1--%>
                                                    <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Services" DataFormatString="{0:MM/dd/yyyy}">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <%--    2--%>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedural Code ID" Visible="False">
                                                    </asp:BoundColumn>
                                                    <%--    3--%>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Procedure Code">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <%-- 4--%>
                                                    <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Code Description">
                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                    </asp:BoundColumn>
                                                    <%-- 5--%>
                                                    <asp:TemplateColumn HeaderText="Price * Factor" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" runat="server" Style="text-align: right;" Font-Size="12px"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FACTOR_AMOUNT") %>'> </asp:Label>
                                                            &nbsp;*&nbsp;
                                                            <asp:Label ID="lblFactor" runat="server" Style="text-align: right;" Font-Size="12px"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FACTOR") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <%-- 6--%>
                                                    <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" DataFormatString="{0:0.00}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 7--%>
                                                    <asp:TemplateColumn HeaderText="Amount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmt" runat="server" Style="text-align: right;" Width="58px" Text='<%# Eval("FLT_AMOUNT") %>'> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <%-- 8--%>
                                                    <asp:TemplateColumn HeaderText="Unit" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnit" runat="server" Style="text-align: right;" Width="58px"
                                                                Text="1"> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <%-- 9--%>
                                                    <asp:BoundColumn DataField="I_UNIT" HeaderText="Unit" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 10--%>
                                                    <asp:BoundColumn DataField="PROC_AMOUNT" HeaderText="Unit" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 11--%>
                                                    <asp:BoundColumn DataField="DOCT_AMOUNT" HeaderText="Unit" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 12--%>
                                                    <asp:TemplateColumn HeaderText="Remove">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%-- 13--%>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="Type Code" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 14--%>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="I_EVENT_PROCID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <%-- 15--%>
                                                    <asp:BoundColumn DataField="SZ_STUDY_NUMBER" HeaderText="SZ_STUDY_NUMBER" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                        <div align="right">
                                            <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="80px" CssClass="Buttons"
                                                OnClick="btnRemove_Click" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" colspan="4" align="right">
                                        <asp:TextBox ID="txtAmount" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                            OnClick="btnSave_Click1" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                            OnClick="btnUpdate_Click1" />
                                        <asp:Button ID="btnClearService" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                            OnClick="btnClearService_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdLatestBillTransaction" runat="server" OnSelectedIndexChanged="grdLatestBillTransaction_SelectedIndexChanged"
                                            OnItemCommand="grdLatestBillTransaction_ItemCommand" OnItemDataBound="grdLatestBillTransaction_ItemDataBound"
                                            AutoGenerateColumns="False" Width="100%">
                                            <ItemStyle CssClass="GridRow" />
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                                    <ItemStyle CssClass="grid-item-left" />
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Make Payment">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="BIT_WRITE_OFF_FLAG" HeaderText="WRITEOFFFLAG" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkInitialReport" runat="server" Text="Edit W.C. 4.0 " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Initial Report"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkProgressReport" runat="server" Text="Edit W.C. 4.2  " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Progress Report"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkMIReport" runat="server" Text="Edit W.C. 4.3 " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Report Of MMI"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--  <asp:TemplateColumn HeaderText="Generate bill">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill" OnClientClick='<%# "return checkExistingBills(" +Eval("SZ_BILL_COUNT") + " );" %>' > <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="Generate bill">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill" OnClientClick='<%#"return checkExistingBills(" +Eval("SZ_BILL_COUNT") + ",\"" + Eval("SZ_CASE_TYPE") + "\",\"" + Eval("SZ_BILL_NUMBER") + "\",\"" + Eval("GP")+"\", \"" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY + "\");" %>'> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_READING_DOCTOR_ID" HeaderText="ReadingDoctor" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="GP" HeaderText="Speciality" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PG_ID" HeaderText="Speciality ID" Visible="False"></asp:BoundColumn>
                                                <%-- <asp:BoundColumn DataField="RDI" HeaderText="ReadingDoctor" Visible="False"></asp:BoundColumn>--%>
                                            </Columns>
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
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <div id="divid2" style="position: absolute; left: 100px; top: 100px; width: 500px;
        height: 380px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseSource();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="iframeAddDiagnosis" src="" frameborder="0" height="380" width="500">
        </iframe>
    </div>
    <div id="div1" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient1();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="popupmsg" runat="server"></span>
            <br />
            <br />
            <br />
            <%--<asp:Button ID="btnOK1" runat ="server" OnClick="btnOK1_Click" Text = "OK" CssClass="Buttons" />--%>
            <input type="button" class="Buttons" value="Cancel" id="btnCancel1" onclick="CancelExistPatient1();"
                style="width: 80px;" />
        </div>
        <br />
        <%--div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />--%>
        <div style="text-align: center;">
        </div>
    </div>
    <asp:Panel ID="pnlDiagnosisCode" runat="server" Style="width: 450px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right">
                    <a onclick="CloseDiagnosisCodePopup();" style="cursor: pointer;" title="Close">X</a>
                </td>
            </tr>
            <tr>
                <td style="width: 102%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%" valign="top">
                        <tr runat="server" id="trDoctorType">
                            <td class="ContentLabel" style="width: 15%; height: 18px;">
                                Diagnosis Type:
                            </td>
                            <td style="width: 35%; height: 18px;">
                                <cc1:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px" Connection_Key="Connection_String"
                                    Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---" Flag_Key_Value="DIAGNOSIS_TYPE_LIST">
                                </cc1:ExtendedDropDownList>
                            </td>
                            <td class="ContentLabel" style="width: 15%; height: 18px;">
                                Code :
                            </td>
                            <td style="width: 35%; height: 18px;">
                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td class="ContentLabel" style="width: 15%; height: 18px;">
                                Description :
                            </td>
                            <td style="width: 35%; height: 18px;">
                                <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ContentLabel" colspan="6">
                                <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                    OnClick="btnSeacrh_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                    OnClick="btnOK_Click" />&nbsp;
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"  cssclass="Buttons" OnClick="btnCancel_Click"/>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 200px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="100%" CssClass="GridTable"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                            OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <dx:ASPxPopupControl ID="DGCODEPOPUP" runat="server" CloseAction="CloseButton" Modal="true"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DignosisPopup"
            HeaderText="Diagnosis Codes" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" AllowDragging="True" EnableAnimation="False"
            EnableViewState="True" Width="900px" PopupHorizontalOffset="0" PopupVerticalOffset="0"
              AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
            Height="540px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <div style="visibility: hidden;">
            <asp:Button ID="btnAddDGCodes" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                OnClick="btnAddDGCodes_Click" />
        </div>
        <div style="visibility: hidden;">
            <asp:Button ID="btnRemoveDGCodes" Text="X" BackColor="#B5DF82" BorderStyle="None"
                runat="server" OnClick="lnkbtnRemoveDiag_Click" />
        </div>
        <asp:Panel ID="pnlPDFWorkerComp" runat="server" Style="width: 250px; height: 0px;
            background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
            visibility: hidden;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                class="TDPart">
                <tr>
                    <td align="right" valign="top">
                        <a onclick="ClosePDFWorkerComp();" style="cursor: pointer;" title="Close">X</a>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="text-align: left;" class="ContentLabel">
                        <asp:RadioButtonList ID="rdbListPDFType" runat="server">
                            <asp:ListItem Value="1" Text="Doctor's Initial Report" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Doctor's Progress Report"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Doctor's Report Of MMI"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center">
                        <asp:Button ID="btnGenerateWCPDF" runat="server" Text="Generate PDF" OnClick="btnGenerateWCPDF_Click"
                            CssClass="Buttons" />
                        <input type="hidden" runat="server" id="hndSeletedDGCodes" />
                        <asp:HiddenField ID="hdnWCPDFBillNumber" runat="server" />
                        <asp:HiddenField ID="hdnSpeciality" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
