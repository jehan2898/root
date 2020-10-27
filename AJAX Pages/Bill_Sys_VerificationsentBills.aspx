<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_VerificationsentBills.aspx.cs" Inherits="Bill_Sys_VerificationsentBills" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
    
    <%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="~/validation.js"></script>

    <style type="text/css">
        .displayColumn {
            display: none;
        }
    </style>

     <script type="text/javascript">
         function test(billno, id, spe) {
             debugger;
             alert(billno);
             alert(id);
             alert(spe);
             var caseid = document.getElementById('ctl00_ContentPlaceHolder1_hdnCaseId').value;
             alert(caseid);
             scanVerificationSent(caseid, spe, id, billno, 2);
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

        function confirm_bill_delete() {
            if (confirm("Are you sure want to Delete?") == true) {
                if (confirm("Delete visit , treatment , and test entries attached with this bill?") == true) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lblVisitStatus").value = "DELETE";
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_lblVisitStatus").value = "CHANGE_STATUS";
                }

                return true;
            }
            else {
                return false;
            }
        }

        function showUploadFilePopup() {

            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height = '100px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.top = '200px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.left = '350px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.zIndex = '0';
            // alert("ok");
        }
        function CloseUploadFilePopup() {
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height = '0px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';
            //  document.getElementById('ctl00_ContentPlaceHolder1_txtGroupDateofService').value='';      
        }


        function AddDenial() {
            var e = document.getElementById("ctl00_ContentPlaceHolder1_extddenial");
            var user = e.options[e.selectedIndex].text;
            if (user == "---Select---") {
                alert("Please select Denial Reason!");
                return false;
            }

            //alert(user);
            var vlength = document.getElementById("<%=lbSelectedDenial.ClientID%>").length;


            //alert(vlength);
            var status = "0";

            var i;
            if (vlength != 0) {
                for (i = 0; i < vlength; i++) {
                    if (document.getElementById("ctl00_ContentPlaceHolder1_extddenial").value == document.getElementById("<%=lbSelectedDenial.ClientID %>").options[i].value) {
                        alert("Denial reason already exists");
                        status = "1";
                        return false;
                    }
                }
            }
            if (status != "1") {
                //alert(e.options[e.selectedIndex].text);
                document.getElementById("<%=hfdenialReason.ClientID %>").value = document.getElementById("<%=hfdenialReason.ClientID %>").value + e.options[e.selectedIndex].value + ",";
                var optn = document.createElement("OPTION");
                optn.text = e.options[e.selectedIndex].text;
                optn.value = e.options[e.selectedIndex].value;
                document.getElementById("<%=lbSelectedDenial.ClientID %>").options.add(optn);
                return false;
            }
        }

        function RemoveDenial() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").length <= 0) {
                alert("No Denial reason available to remove.")
                document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").focus();
                return false;
            }

            else if (document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").selectedIndex < 0) {
                alert("Please Select Denail reason to Remove.");
                document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").focus();
                return false;
            }
            else {

                var e = document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial");
                var user = e.options[e.selectedIndex].value;
                //alert(user);
                document.getElementById("ctl00_ContentPlaceHolder1_hfremovedenialreason").value = document.getElementById("ctl00_ContentPlaceHolder1_hfremovedenialreason").value + e.options[e.selectedIndex].value + ",";
                document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").options[document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").selectedIndex] = null;

                var list = document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial");
                var items = list.getElementsByTagName("option");
                return false;
            }
        }

        function confirm_check() {

            var f = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch");

            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {

                if (f.getElementsByTagName("input").item(i).name.indexOf('chkBill') != -1) {
                    // alert(f);
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                            return true;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                return true;
            }
        }

        function Validate() {
            var f = document.getElementById("<%= grdVeriFication.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        return true;
                    }
                }
            }
            alert('Select atleast one (1) bill to proceed.');
            return false;
        }


        function checkedDate() {

            var year1 = "";
            year1 = document.getElementById("<%=txtSaveDate.ClientID %>").value;
            if ((year1.charAt(0) != '' && year1.charAt(1) != '' && year1.charAt(2) == '/' && year1.charAt(3) != '' && year1.charAt(4) != '' && year1.charAt(5) == '/' && year1.charAt(6) != '' && year1.charAt(7) != '' && year1.charAt(8) != '' && year1.charAt(9) != '' && year1.charAt(6) != '0')) {
                return true;
            }
            else {
                alert("Please select verification received date.");
                return false;
            }
        }

        function closeTypePage1() {
            var a = document.getElementById('ctl00_ContentPlaceHolder1_rdlVerification_1');
            a.checked = false;


            $find('modal').hide();

            return true;


        }

    </script>

    <script>

        function showVerificationPopup(objBillNumber, ObjSpec) {



            var obj3 = "";
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left = '300px';
            document.getElementById('divid2').style.top = '70px';
            document.getElementById('divid2').style.visibility = 'visible';
            document.getElementById('iframeVerification').src = 'Bill_Sys_VerificationRequestPopup.aspx?BillNo=' + objBillNumber + '&Spe=' + ObjSpec + '';
            return false;
        }

        function CloseVerificationPopup() {
            document.getElementById('divid2').style.height = '0px';
            document.getElementById('divid2').style.visibility = 'hidden';
            //            document.getElementById('iframeVerification').src='Bill_Sys_VerificationsentBills.aspx'; 
            window.parent.document.location.href = 'Bill_Sys_VerificationsentBills.aspx';
        }

        function unload() {
            // alert("ok");
            document.getElementById("<%=divgrid.ClientID %>").style.height = '0px';
            document.getElementById('<%=divgrid.ClientID %>').style.visibility = 'hidden';
        }



        function showVerificationReceivedPopup(objBillNumber, objVisitDate, objVerDate, objVerNotes) {

            while (objVerNotes.search("#") > 1) {

                objVerNotes = objVerNotes.replace("#", "\r\n");
            }
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.height = '180px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.visibility = 'visible';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.position = "absolute";
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.top = '250px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.left = '450px';
            document.getElementById('ctl00_ContentPlaceHolder1_txtBillNumber').value = objBillNumber;
            document.getElementById('ctl00_ContentPlaceHolder1_txtVRVisitDate').value = objVisitDate;
            document.getElementById('ctl00_ContentPlaceHolder1_txtVerifiactionReceivedDate').value = objVerDate;
            document.getElementById('ctl00_ContentPlaceHolder1_verificationReceivedNotes').value = objVerNotes;


        }

        function CloseVerificationReceivedPopup() {
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.height = '0px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlVerifiactionReceived').style.visibility = 'hidden';
        }

        function checkdenial() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_lbSelectedDenial").length <= 0) {
                alert("Add denial reason.")
                return false;
            }
            else {
                return true;
            }
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdVeriFication.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {




                    f.getElementsByTagName("input").item(i).checked = ival;




                }


            }
        }


        function ShowChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'block';
        }

        function ShowDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'block';
        }

        function HideChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'none';
        }

        function HideDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'none';
        }

        function view(szBillNo, szCaseid) {
            alert("hi " + szBillNo + "," + szCaseid);
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left = '350px';
            document.getElementById('divid').style.top = '150px';
            document.getElementById('divid').style.visibility = 'visible';
            document.getElementById('frameeditexpanse').src = 'Bill_Sys_Verification.aspx?TM_SZ_BILL_ID=' + szBillNo + '&caseID=' + szCaseid + '';
        }

        function confirm_update_bill_status() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_drdUpdateStatus").value == 'NA') {
                alert('Select Bill Status');
                return false;
            }

            var f = document.getElementById("<%=grdVeriFication.ClientID%>");
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
            else {
                return true;
            }
        }
        //Nirmalkumar
        function ShowAddGeneralDenialPopup() {
            var url = "Bill_Sys_General_Denial.aspx";
            AddGeneralDenial.SetContentUrl(url);
            AddGeneralDenial.Show();
        }
        function CloseAddGeneralDenial() {

            AddGeneralDenial.Hide();
            document.getElementById('ctl00_ContentPlaceHolder1_btnGenDenTxt').click();

            //btnGenDenTxt_Click
        }
        function showUploadFilePopupOnAnswerVerification(sz_BillNumber, VerificationID, sz_Specialty, Answer_ID) {
            if (Answer_ID != "") {
                alert(sz_BillNumber);
                alert(VerificationID);
                alert(sz_Specialty);
                alert(Answer_ID);

                var url = "AnswerVerificationPopup.aspx?BillNumber=" + sz_BillNumber + "&Verification_ID=" + VerificationID + "&Specialty=" + sz_Specialty + "&answer=" + Answer_ID + "";

                PanelVerificationAnswer.SetContentUrl(url);

                PanelVerificationAnswer.Show();
                PanelVerificationAnswer.BringToFront();

                return false;
            }
            else {
                alert("Answer can not be empty");
                return false;
            }
        }
    </script>
     <script type="text/javascript">

         function ShowAddGeneralDenialPopup() {
             var url = "Bill_Sys_General_Denial.aspx";
             AddGeneralDenial.SetContentUrl(url);
             AddGeneralDenial.Show();
         }

         function CloseAddGeneralDenial() {

             AddGeneralDenial.Hide();
             document.getElementById('ctl00_ContentPlaceHolder1_btnGenDenTxt').click();

         }

         function CloseDelDocImages() {

             DelDocImages.Hide();
             document.getElementById('ctl00_ContentPlaceHolder1_btnGenDenTxt').click();

         }

         function ShowDelDocImages() {

             var url = "DeleteVerificationDocImages.aspx";
             DelDocImages.SetContentUrl(url);
             DelDocImages.Show();
         }
    </script>
    <script type="text/javascript">
        function popup_Closing() {
            //window.location.reload();
            window.location.href = window.location.href
        }
    </script>
    <script type="text/javascript">
        function clearData() {

            var data = [];
            var table = document.getElementsByClassName("GridTable1");
            for (var i = 0; i < table[0].rows.length; i++) {
                if (i == 0)
                    continue;
                var text = table[0].rows[i].cells[4].children[0].innerHTML;
                var str2 = text.replace(/\n|\r/g, "");

                table[0].rows[i].cells[4].children[0].innerHTML = str2;

            }


        }
    </script>
    <script type="text/javascript">
        function OpenBillVerificationScan() {
            var caseid = document.getElementById('ctl00_ContentPlaceHolder1_hdnCaseId').value;
            var specialityid = document.getElementById('ctl00_ContentPlaceHolder1_hdnSpecialty').value;
            var verificationId = document.getElementById('ctl00_ContentPlaceHolder1_hdnStatusCode').value;
            var billNumber = document.getElementById('ctl00_ContentPlaceHolder1_hdnBillNumber').value;
            scanBillVerification(caseid, specialityid, verificationId, billNumber, '5');
        }
    </script>
    <script type="text/javascript">
        function OnVerificationReceivedClick() {

            var radio = document.getElementById('ctl00_ContentPlaceHolder1_rdoVerReceived');
            var radio1 = document.getElementById('ctl00_ContentPlaceHolder1_rdoVerSent');
            var radio2 = document.getElementById('ctl00_ContentPlaceHolder1_rdoDenial');
            var radio3 = document.getElementById('ctl00_ContentPlaceHolder1_rdoEOR');
            if (radio.checked) {
                if (Validate()) {
                    var checkboxCell;
                    var bills, caseId;
                    var specialty;
                    var billNos = null;
                    var specialities = null;
                    var grid = document.getElementById("<%= grdVeriFication.ClientID %>");
                    if (grid.rows.length > 0) {
                        for (i = 1; i < grid.rows.length; i++) {
                            checkboxCell = grid.rows[i].cells[0];
                            bills = grid.rows[i].cells[1];
                            caseId = grid.rows[i].cells[15];
                            specialty = grid.rows[i].cells[0].children[1];
                            caseId = grid.rows[i].cells[0].children[2];
                            for (j = 0; j < checkboxCell.childNodes.length; j++) {
                                //if childNode type is CheckBox                 
                                if (checkboxCell.childNodes[j].type == "checkbox") {
                                    if (checkboxCell.childNodes[j].checked == true) {
                                        if (billNos == null && specialities == null) {
                                            billNos = bills.innerText;
                                            specialities = specialty.innerText;
                                        }
                                        else { billNos = billNos + ',' + bills.innerText; specialities = specialities + ',' + specialty.innerText; }
                                    }
                                }
                            }
                        }
                    }
                    specialities = specialities.replace(/,(\s+)?$/, '');
                    billNos = billNos.replace(/,(\s+)?$/, '');
                    caseId = caseId.innerText;
                    var url = "VerificationReceived.aspx?billNo=" + billNos + "&CaseId=" + caseId + "&SpecialtyId=" + specialities;

                    parent.$("#dvVerificationPopUp").dialog({
                        autoOpen: false,
                        show: "fade",
                        hide: "fade",
                        modal: true,
                        open: function (ev, ui) {
                            parent.$("#iFrmVerificationPopUp").attr("src", url);
                            parent.$("#iFrmVerificationPopUp").attr("height", "620");
                            parent.$("#iFrmVerificationPopUp").attr("width", "850");
                            parent.$("#iFrmVerificationPopUp").attr("position", "absolute");
                        },
                        height: 580,
                        width: 850,
                        resizable: false,
                        zIndex: 99900000,
                        title: "Verification Received"
                    });
                    parent.$("#dvVerificationPopUp").dialog("open");
                }
            }

            if (radio1.checked) {
                if (Validate()) {
                    var checkboxCell;
                    var bills, caseId;
                    var specialty;
                    var billNos = null;
                    var specialities = null;
                    var grid = document.getElementById("<%= grdVeriFication.ClientID %>");
                    if (grid.rows.length > 0) {
                        for (i = 1; i < grid.rows.length; i++) {
                            checkboxCell = grid.rows[i].cells[0];
                            bills = grid.rows[i].cells[1];
                            caseId = grid.rows[i].cells[15];
                            specialty = grid.rows[i].cells[0].children[1];
                            caseId = grid.rows[i].cells[0].children[2];
                            for (j = 0; j < checkboxCell.childNodes.length; j++) {
                                //if childNode type is CheckBox                 
                                if (checkboxCell.childNodes[j].type == "checkbox") {
                                    if (checkboxCell.childNodes[j].checked == true) {
                                        if (billNos == null && specialities == null) {
                                            billNos = bills.innerText;
                                            specialities = specialty.innerText;
                                        }
                                        else { billNos = billNos + ',' + bills.innerText; specialities = specialities + ',' + specialty.innerText; }
                                    }
                                }
                            }
                        }
                    }
                    specialities = specialities.replace(/,(\s+)?$/, '');
                    billNos = billNos.replace(/,(\s+)?$/, '');
                    caseId = caseId.innerText;
                    var url = "VerificationSent.aspx?billNo=" + billNos + "&CaseId=" + caseId + "&SpecialtyId=" + specialities;

                    parent.$("#dvVerificationSent").dialog({
                        autoOpen: false,
                        show: "fade",
                        hide: "fade",
                        modal: true,
                        open: function (ev, ui) {
                            parent.$("#iFrmVerificationSent").attr("src", url);
                            parent.$("#iFrmVerificationSent").attr("height", "620");
                            parent.$("#iFrmVerificationSent").attr("width", "850");
                            parent.$("#iFrmVerificationSent").attr("position", "absolute");
                        },
                        height: 580,
                        width: 850,
                        resizable: false,
                        zIndex: 99900000,
                        title: "Verification Sent"
                    });
                    parent.$("#dvVerificationSent").dialog("open");
                }
            }

            if (radio2.checked) {
                if (Validate()) {
                    var checkboxCell;
                    var bills, caseId;
                    var specialty;
                    var billNos = null;
                    var specialities = null;
                    var grid = document.getElementById("<%= grdVeriFication.ClientID %>");
                    if (grid.rows.length > 0) {
                        for (i = 1; i < grid.rows.length; i++) {
                            checkboxCell = grid.rows[i].cells[0];
                            bills = grid.rows[i].cells[1];
                            caseId = grid.rows[i].cells[15];
                            specialty = grid.rows[i].cells[0].children[1];
                            caseId = grid.rows[i].cells[0].children[2];
                            for (j = 0; j < checkboxCell.childNodes.length; j++) {
                                //if childNode type is CheckBox                 
                                if (checkboxCell.childNodes[j].type == "checkbox") {
                                    if (checkboxCell.childNodes[j].checked == true) {
                                        if (billNos == null && specialities == null) {
                                            billNos = bills.innerText;
                                            specialities = specialty.innerText;
                                        }
                                        else { billNos = billNos + ',' + bills.innerText; specialities = specialities + ',' + specialty.innerText; }
                                    }
                                }
                            }
                        }
                    }
                    debugger;
                    specialities = specialities.replace(/,(\s+)?$/, '');
                    billNos = billNos.replace(/,(\s+)?$/, '');
                    caseId = caseId.innerText;
                    var url = "BillDenial.aspx?billNo=" + billNos + "&CaseId=" + caseId + "&SpecialtyId=" + specialities;

                    parent.$("#dvDenialPopUp").dialog({
                        autoOpen: false,
                        show: "fade",
                        hide: "fade",
                        modal: true,
                        open: function (ev, ui) {
                            parent.$("#iFrmDenialPopUp").attr("src", url);
                            parent.$("#iFrmDenialPopUp").attr("height", "700");
                            parent.$("#iFrmDenialPopUp").attr("width", "850");
                            parent.$("#iFrmDenialPopUp").attr("position", "absolute");
                        },
                        height: 700,
                        width: 850,
                        resizable: false,
                        zIndex: 99900000,
                        title: "Bill Denial"
                    });
                    parent.$("#dvDenialPopUp").dialog("open");
                }
            }

            if (radio3.checked) {
                if (Validate()) {
                    var checkboxCell;
                    var bills, caseId;
                    var specialty;
                    var billNos = null;
                    var specialities = null;
                    var grid = document.getElementById("<%= grdVeriFication.ClientID %>");
                    if (grid.rows.length > 0) {
                        for (i = 1; i < grid.rows.length; i++) {
                            checkboxCell = grid.rows[i].cells[0];
                            bills = grid.rows[i].cells[1];
                            caseId = grid.rows[i].cells[15];
                            specialty = grid.rows[i].cells[0].children[1];
                            caseId = grid.rows[i].cells[0].children[2];
                            for (j = 0; j < checkboxCell.childNodes.length; j++) {
                                //if childNode type is CheckBox                 
                                if (checkboxCell.childNodes[j].type == "checkbox") {
                                    if (checkboxCell.childNodes[j].checked == true) {
                                        if (billNos == null && specialities == null) {
                                            billNos = bills.innerText;
                                            specialities = specialty.innerText;
                                        }
                                        else { billNos = billNos + ',' + bills.innerText; specialities = specialities + ',' + specialty.innerText; }
                                    }
                                }
                            }
                        }
                    }
                    specialities = specialities.replace(/,(\s+)?$/, '');
                    billNos = billNos.replace(/,(\s+)?$/, '');
                    caseId = caseId.innerText;
                    var url = "EORReceived.aspx?billNo=" + billNos + "&CaseId=" + caseId + "&SpecialtyId=" + specialities;

                    parent.$("#dvEORPopUp").dialog({
                        autoOpen: false,
                        show: "fade",
                        hide: "fade",
                        modal: true,
                        open: function (ev, ui) {
                            parent.$("#iFrmEORPopUp").attr("src", url);
                            parent.$("#iFrmEORPopUp").attr("height", "620");
                            parent.$("#iFrmEORPopUp").attr("width", "850");
                            parent.$("#iFrmEORPopUp").attr("position", "absolute");
                        },
                        height: 580,
                        width: 850,
                        resizable: false,
                        zIndex: 99900000,
                        title: "EOR Received"
                    });
                    parent.$("#dvEORPopUp").dialog("open");
                }
            }
        }
    </script>
        <script type="text/javascript">
            function OpenVerificationPopup(billNo, spId) {
                var url = "VerificationRequestPopup.aspx?";

                url = url + "BillNo=" + billNo + "&spId=" + spId;

                parent.$("#dvVerificationPopUpPage").dialog({
                    autoOpen: false,
                    show: "fade",
                    hide: "fade",
                    modal: true,
                    open: function (ev, ui) {
                        parent.$("#iFrmVerificationPopUpPage").attr("src", url);
                        parent.$("#iFrmVerificationPopUpPage").attr("height", "580");
                        parent.$("#iFrmVerificationPopUpPage").attr("width", "850");
                        parent.$("#iFrmVerificationPopUpPage").attr("position", "absolute");
                    },
                    height: 580,
                    width: 880,
                    resizable: false,
                    zIndex: 99900000,
                    title: "Request"
                });
                parent.$("#dvVerificationPopUpPage").dialog("open");
            }
    </script>

    <script type="text/javascript">
        function OpenGeneralDenialPopup() {
            var url = "Bill_Sys_General_Denial.aspx";

            parent.$("#dvGeneralDenial").dialog({
                autoOpen: false,
                show: "fade",
                hide: "fade",
                modal: true,
                open: function (ev, ui) {
                    parent.$("#iFrmGeneralDenial").attr("src", url);
                    parent.$("#iFrmGeneralDenial").attr("height", "480");
                    parent.$("#iFrmGeneralDenial").attr("width", "650");
                    parent.$("#iFrmGeneralDenial").attr("position", "absolute");
                },
                height: 480,
                width: 700,
                resizable: false,
                zIndex: 99900000,
                title: "Add General Denial"
            });
            parent.$("#dvGeneralDenial").dialog("open");
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
                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <%-- <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="return openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>--%>
                                        <asp:Repeater ID="rptPatientDeskList" runat="server">
                                            <HeaderTemplate>
                                                <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82;">
                                                    <tr>
                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                            <b>Case#</b>
                                                        </td>
                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                            <b>Name</b>
                                                        </td>
                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                            <b>Insurance Name</b>
                                                        </td>
                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                            <b>Accident Date</b>
                                                        </td>
                                                        <td bgcolor="#B5DF82" style="height: 50%">
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%>
                                                    </td>
                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </td>
                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </td>
                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_ACCIDENT", "{0:MM/dd/yyyy}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table></FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                    <td class="lbl" colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButton runat="server" ID="rdoVerReceived" Text="Verification Received" GroupName="RadioButtonList1" onclick="OnVerificationReceivedClick()" />
                                                <asp:RadioButton runat="server" ID="rdoVerSent" Text="Verification Sent" GroupName="RadioButtonList1" onclick="OnVerificationReceivedClick()" />
                                                <asp:RadioButton runat="server" ID="rdoDenial" Text="Denials" GroupName="RadioButtonList1" onclick="OnVerificationReceivedClick()" />
                                                <asp:RadioButton runat="server" ID="rdoEOR" Text="EOR" GroupName="RadioButtonList1" onclick="OnVerificationReceivedClick()" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnClose1" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="test" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdlVerification" runat="server" Visible="false" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rdlVerification_SelectedIndex" AutoPostBack="true">
                                                    <asp:ListItem Value="1">Verification Received</asp:ListItem>
                                                    <asp:ListItem Value="2">Verification Sent</asp:ListItem>
                                                    <asp:ListItem Value="3">Denials</asp:ListItem>
                                                  
                                                </asp:RadioButtonList>
                                                <%-- <asp:Button ID="btnok2" Text="test" runat="server" />--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnClose1" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:UpdatePanel ID="test1" runat="server">
                                            <ContentTemplate>
                                        <asp:LinkButton ID="lnkAddGeneralDenial" runat="server" OnClientClick="javascript:ShowAddGeneralDenialPopup(); return false;" Text="General Denial"></asp:LinkButton>
                                                | <a href="#" onclick="ShowDelDocImages(); return false;" >Delete Document Images</a>
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnGenDenTxt" />
                                                <asp:PostBackTrigger ControlID="btnGenDenTxt"></asp:PostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart" align="right" visible="false">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0">
                                            <progresstemplate>
                                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                    runat="Server">
                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                        Height="25px" Width="24px"></asp:Image>
                                                    Loading...</div>
                                            </progresstemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="pnlgrid" runat="server" Visible="false">
                                            <ContentTemplate>
                                                <%--<asp:Button ID="btnSetDenial" runat="server" Text="Set Denial" CssClass="Buttons" OnClick="btnSetDenial_Click" />--%>
                                                <asp:DataGrid ID="grdBillSearch" runat="Server" Width="100%" CssClass="GridTable"
                                                    AutoGenerateColumns="False" OnSelectedIndexChanged="grdBillSearch_SelectedIndexChanged">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkBill" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Bill Number" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="top">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>
                                                                <%-- <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Edit"></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CASE ID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:dd MMM yyyy}"
                                                            Visible="False">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Provider" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DT_VISIT_DATE" HeaderText="Visit Date" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" DataFormatString="{0:0.00}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" DataFormatString="{0:0.00}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" DataFormatString="{0:0.00}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_VERIFICATION_NOTES" HeaderText="Verification Notes"
                                                            Visible="False">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Verification Request" Visible="False">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.Verification")%>
                                                                <%--<asp:LinkButton ID="lnkDeniel" runat="server" Text="Denial" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Deniel"></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="DT_VERIFICATION_DATE" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DT_VERIFICATION_RECEIVED_DATE" HeaderText="Received Date "
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="top" Visible="false">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NO_OF_DAYS" HeaderText="Verification received  day's"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="top" Visible="false">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_USERNAME" HeaderText="UserName" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top"></asp:BoundColumn>
                                                        <%-- atul--%>
                                                        <asp:TemplateColumn HeaderText="Request" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="top">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <%--<ItemTemplate>
                                                        <a id="lnkVerificationReq" href="#" onclick='<%# "showVerificationPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ",\"" + Eval("DT_VISIT_DATE") +"\",\""+ Eval("DT_CURRENT_DATE") +"\",\""+ Eval("SZ_VERIFICATION_NOTES") +"\");" %>' ><img src="Images/actionEdit.gif" style="border: none;" title="Verification Request"/></a>
                                                    </ItemTemplate>--%>
                                                            <ItemTemplate>
                                                                <a id="lnkVerificationReq" href="#" onclick='<%# "showVerificationPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ");" %>'>
                                                                    <img src="Images/actionEdit.gif" style="border: none;" title="Verification Request" /></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="DT_CURRENT_DATE" HeaderText="Current Date" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Verification Received" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-VerticalAlign="top" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <a id="lnkVerificationReceived" href="#" onclick='<%# "showVerificationReceivedPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ",\"" + Eval("DT_VISIT_DATE") +"\",\""+ Eval("DT_CURRENT_DATE") +"\",\""+ Eval("SZ_VERIFICATION_RECEIVED_NOTES") +"\");" %>'>
                                                                    <img src="Images/addvisit.jpg" style="border: none;" title="Verification  Received" /></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Denial" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="top"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDenial" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Verification" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan"
                                                                    Text="Scan" OnClick="lnkscan_Click"></asp:LinkButton>/
                                                                <asp:LinkButton ID="lnkuplaod" runat="server" CausesValidation="false" CommandName="Upload"
                                                                    Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="SZ_VERIFICATION_LINK" HeaderText="View" Visible="true"></asp:BoundColumn>
                                                        <asp:HyperLinkColumn Text="Scan" Target="_blank" DataNavigateUrlField="SZ_BILL_NUMBER"
                                                            DataNavigateUrlFormatString="scan.aspx?bid={0}" Visible="false"></asp:HyperLinkColumn>
                                                    </Columns>
                                                    <ItemStyle CssClass="GridRow" />
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 800px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <contenttemplate>
                                        <table style="vertical-align: middle; width: 1110px;" border="0">
                                            <tbody>
                                                <tr>
                 
                                                    <td style="vertical-align: middle; width: 300px" align="left">
                                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                            CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td style="width: 420px; text-align:right;">
                                                     <asp:Label ID="lblbillstatus" runat="server" Font-Bold="True" Font-Size="Small" Text="Bill Status"
                                                      Status=""></asp:Label>
                                                       <extddl:ExtendedDropDownList ID="drdUpdateStatus" runat="server" Width="125px" Selected_Text="---Select---"
                                                                        Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL"
                                                                        Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 100px;">
                                                      <asp:Button ID="btnBillUpdateStatus" OnClick="btnBillUpdateStatus_Click" runat="server" Text="Update Status" width="100px" />
                                                     
                                                    </td>
                                                  
                                                    <td style="vertical-align: middle; width: 1010px; text-align: right" align="right"
                                                        colspan="3">
                                                        Record Count:<%= this.grdVeriFication.RecordCount%>| Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                        <%-- OnClick="lnkExportTOExcel_onclick" --%>
                                                        <asp:LinkButton ID="lnkExportToExcel" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcel_onclick">
                                                                                     <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                                <xgrid:XGridViewControl ID="grdVeriFication" runat="server" Height="148px" Width="1110px"
                                                    CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="VeriFication"
                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" ExportToExcelColumnNames="Bill Number,Doctor Name,Provider,Visit Date,Bill Amount,Paid Amount,Balance,Date,UserName,Balance,Date,UserName,Bill Status"
                                                    ExportToExcelFields="SZ_BILL_NUMBER,SZ_DOCTOR_NAME,SZ_OFFICE,DT_VISIT_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,DT_VERIFICATION_DATE,SZ_USERNAME,SZ_BILL_STATUS_NAME"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_VerificationRecived"
                                                    PageRowCount="20" PagerStyle-CssClass="pgr" DataKeyNames="SZ_BILL_NUMBER,SZ_CASE_ID,bt_denial,bt_verification,bt_eor, Speciality,SZ_BILL_STATUS_CODE"
                                                    AllowSorting="true" OnRowCommand="grdVeriFication_RowCommand">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                    ToolTip="Select All" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                <asp:Label ID="lblSpeciality" runat="server" CssClass="displayColumn" Text='<%# Bind("Speciality")%>'></asp:Label>
                                                                <asp:Label ID="lblCaseId" runat="server" CssClass="displayColumn" Text='<%# Bind("SZ_CASE_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--1--%>
                                                        <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill Number" SortExpression=" substring(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER,3,len(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER))">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--2--%>
                                                        <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" SortExpression="SZ_DOCTOR_NAME">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--3--%>
                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="Provider" SortExpression="MST_OFFICE.SZ_OFFICE">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--4--%>
                                                        <asp:BoundField DataField="DT_VISIT_DATE" HeaderText="Visit Date" SortExpression="ISNULL(CONVERT(NVARCHAR(20),(SELECT MIN(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),101),'''')">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--      <asp:TemplateField HeaderText="Bill Status">
                                                                            <itemtemplate>
                                                        <asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>' Visible="false" ></asp:Label>
                                                
                                            </itemtemplate>
                                                                        </asp:TemplateField>--%>
                                                        <%--5--%>
                                                        <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" SortExpression="convert(int,FLT_BILL_AMOUNT)"
                                                            DataFormatString="{0:C}">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--6--%>
                                                        <asp:BoundField DataField="PAID_AMOUNT" HeaderText="Paid Amount" SortExpression="(SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)"
                                                            DataFormatString="{0:C}">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--7--%>
                                                        <asp:BoundField DataField="FLT_BALANCE" HeaderText="Balance" SortExpression="convert(int,FLT_BALANCE)"
                                                            DataFormatString="{0:c}">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--8--%>
                                                        <asp:BoundField DataField="DT_VERIFICATION_DATE" HeaderText="Date" DataFormatString="{0:C}">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--9--%>
                                                        <asp:BoundField DataField="SZ_USERNAME" HeaderText="UserName">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--10--%>
                                                        <asp:BoundField DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status" SortExpression="MST_BILL_STATUS.SZ_BILL_STATUS_NAME">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--11--%>
                                                        <asp:TemplateField HeaderText="Request">
                                                            <ItemTemplate>
                                                                <a id="lnkVerificationReq" href="#" onclick='<%# "showVerificationPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ",\"" +Eval("Speciality") +"\");" %>'>
                                                                    <img src="Images/actionEdit.gif" style="border: none;" title="Verification Request" /></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--12--%>
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.SZ_VERIFICATION_LINK")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--13--%>
                                                        <asp:TemplateField HeaderText="Verification" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="PLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    Font-Size="15px" Visible="false"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <%--  14--%>
                                                        <asp:TemplateField HeaderText="Denials" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="DenialPLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    Font-Size="15px" Visible="false"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <%--15--%>
                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="case id" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <%--  16--%>
                                                        <asp:TemplateField Visible="false" SortExpression="SZ_BILL_NUMBER">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="100%" align="left">
                                                                        <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                            <asp:GridView ID="grdVerification1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                                                Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="verification_request" ItemStyle-Width="85px" HeaderText="Verification Request">
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="verification_date" ItemStyle-Width="85px" DataFormatString="{0:MM/dd/yyyy}"
                                                                                        HeaderText="Verification Date">
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="answer" ItemStyle-Width="85px" HeaderText="Answer">
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="answer_date" ItemStyle-Width="105px" HeaderText="Answer Date"
                                                                                        DataFormatString="{0:MM/dd/yyyy}">
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  17--%>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="50%" align="right">
                                                                        <div id="div1<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                            <asp:GridView ID="grdDenial" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                                                Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SZ_DENIAL_REASONS" ItemStyle-Width="105px" HeaderText="Denial Reason">
                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DT_DENIAL_DATE" ItemStyle-Width="50px" DataFormatString="{0:MM/dd/yyyy}"
                                                                                        HeaderText="Denial Date">
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SZ_DESCRIPTION" ItemStyle-Width="85px" HeaderText="Description">
                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EOR" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="eorPLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkEM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                    CommandName="eorMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    Font-Size="15px" Visible="false"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="50%" align="right">
                                                                        <div id="div2<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                            <asp:GridView ID="grdEOR" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                                                Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SZ_EOR_REASONS" ItemStyle-Width="105px" HeaderText="EOR Reason">
                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="dt_EOR_date" ItemStyle-Width="50px" DataFormatString="{0:MM/dd/yyyy}"
                                                                                        HeaderText="EOR Date">
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SZ_DESCRIPTION" ItemStyle-Width="85px" HeaderText="Description">
                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="bt_denial" ItemStyle-Width="85px" HeaderText="bt_denial"
                                                            Visible="false">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bt_verification" Visible="false" ItemStyle-Width="105px"
                                                            HeaderText="bt_verification">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bt_eor" Visible="false" ItemStyle-Width="105px"
                                                            HeaderText="bt_eor">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Speciality" Visible="false" ItemStyle-Width="105px" HeaderText="Speciality">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SZ_BILL_STATUS_CODE" Visible="false" ItemStyle-Width="105px"
                                                            HeaderText="BILL STATUS CODE">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="View Answer">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.SZ_VIEW_ANSWER")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                        </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="ContentLabel">
                                        <div style="float: left;">
                                            <asp:Button ID="btnBulkPayment" runat="server" Text="Mark As Received" Width="120px"
                                                Visible="false" CssClass="Buttons" OnClick="btnBulkPayment_Click" /></div>
                                        <div align="right" style="float: right;">
                                            <asp:TextBox ID="txtVerification" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:TextBox
                                                ID="txtCompanyID" runat="server" CssClass="btn" Visible="False" Width="10px"></asp:TextBox>&nbsp;<asp:HiddenField
                                                    ID="lblVisitStatus" runat="server" />
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
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <div id="divid2" style="position: absolute; left: 100px; top: 100px; width: 960px;
        height: 280px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseVerificationPopup();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="iframeVerification" src="" frameborder="0" height="380px" width="960px">
        </iframe>
    </div>
    <asp:Panel ID="pnlVerificationReq" runat="server" Style="width: 420px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        <table width="100%" class="TDPart">
            <tr>
                <td colspan="2" width="100%">
                    <table width="100%">
                        <tr>
                            <td width="70%">
                                <b>Verification Information</b>
                            </td>
                            <td align="right" width="30%">
                                <a onclick="CloseVerificationPopup();" style="cursor: pointer;" title="Close">X</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Bill Number
                </td>
                <td>
                    <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ForeColor="Black" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Visit Date
                </td>
                <td>
                    <asp:TextBox ID="txtVisitDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ForeColor="Black"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Verification Date
                </td>
                <td>
                    <asp:TextBox ID="txtVerificationDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ForeColor="Black"> </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" class="lbl">
                    Verification Notes
                </td>
                <td>
                    <asp:TextBox ID="txtVerificationNotes" runat="server" TextMode="MultiLine" Height="80px"
                        Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" width="350px" align="center">
                    <asp:Button ID="btnSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlVerifiactionReceived" runat="server" Style="width: 420px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        <table width="100%" class="TDPart">
            <tr>
                <td colspan="2" width="100%">
                    <table width="100%">
                        <tr>
                            <td width="70%">
                                <b>Verification Received Information</b>
                            </td>
                            <td align="right" width="30%">
                                <a onclick="CloseVerificationReceivedPopup();" style="cursor: pointer;" title="Close">
                                    X</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Bill Number
                </td>
                <td class="lbl">
                    <asp:TextBox ID="txtBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ForeColor="Black" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Visit Date
                </td>
                <td>
                    <asp:TextBox ID="txtVRVisitDate" runat="server" ReadOnly="true" BackColor="Transparent"
                        BorderColor="Transparent" BorderStyle="None" ForeColor="Black"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    Verification Received Date
                </td>
                <td class="lbl">
                    <asp:TextBox ID="txtVerifiactionReceivedDate" runat="server" ReadOnly="true" BackColor="Transparent"
                        BorderColor="Transparent" BorderStyle="None" ForeColor="Black"> </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" class="lbl">
                    Verification Received Notes
                </td>
                <td>
                    <asp:TextBox ID="verificationReceivedNotes" runat="server" TextMode="MultiLine" Height="80px"
                        Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" width="350px" align="center">
                    <asp:Button ID="btnVRSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnVRSave_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel Style="display: none; background-color: #DBE6FA; width: 400px; height: 0px;"
        ID="pnlMakePayment" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 200%; height: 100%">
            <tr>
                <td>
                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDateValue" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVerDate" runat="server" Text="Date" CssClass="lbl" Font-Bold="true"> </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="imgbtnToDate" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Upload Report :
                </td>
                <td>
                    <%--   <cc1:FileUploaderAJAX ID ="fileupload" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkscan1" runat="server" CausesValidation="false" CommandName="Scan"
                        Text="Scan"></asp:LinkButton>/
                    <asp:LinkButton ID="lnkuplaod1" runat="server" CausesValidation="false" CommandName="Upload"
                        Text="Upload"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOk" runat="server" Text="Add" OnClick="btnOk_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: Green;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; display: none;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 98%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%">
                        <tr>
                            <td>
                                Upload Report :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuUploadReport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                    OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Panel Style="display: none; background-color: #DBE6FA; width: 400px; height=0px;"
                ID="pnlSaveDescription" runat="server">
                <div align="left" style="vertical-align: top;">
                    <div style="position: relative; text-align: left; background-color: #8babe4;" id="pnlSaveDescriptionHeader">
                        <asp:Label ID="lblHeader" runat="server" Text="Test" Font-Bold="true" CssClass="lbl"></asp:Label>
                    </div>
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
                                    Visible="false">
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
                                <%-- <asp:TextBox ID="txtSaveDate" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                  <asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" visible="false" />
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSaveDate"
                     PopupButtonID="imgSavebtnToDate" />   --%>
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
                            <td colspan="2">
                                <asp:Button ID="btnSaveDesc" runat="server" Text="Save" OnClick="btnSaveDesc_Click"
                                    class="Buttons" />
                                <asp:Button ID="btnSaveDate" runat="server" Text="Save" OnClick="btnSaveDesc_Click"
                                    class="Buttons" />
                                <asp:Button ID="btnSavesent" runat="server" Text="Save" OnClick="btnSaveDesc_Click"
                                    class="Buttons" />
                                <asp:Button ID="btnCancelDesc" runat="server" Text="Cancel" OnClick="btnCancelDesc_Click"
                                    class="Buttons" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel Style="display: none; background-color: #DBE6FA; width: 800px; height=0px;"
                ID="pnlVerificationSend" runat="server">
                <div style="position: relative; text-align: left; background-color: #8babe4;" id="Div1">
                    <asp:Label ID="Label1" runat="server" Text="  Verification Sent" Font-Bold="true"
                        CssClass="lbl"></asp:Label>
                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #8babe4;">
                        <asp:Button ID="btnClose1" runat="server" Height="19px" Width="50px" CssClass="Buttons"
                            Text="X" OnClientClick="closeTypePage1()"></asp:Button>
                    </div>
                </div>
                <table class="TDPart" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div style="overflow: scroll; height: 400px;">
                                <asp:DataGrid ID="grdVerificationSend" Width="100%" CssClass="GridTable" runat="Server"
                                    AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridRow" />
                                    <Columns>
                                        <asp:BoundColumn DataField="sz_bill_number" HeaderText="Bill#" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="verification_request" HeaderText="Verification Request"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="verification_date" HeaderText="Request Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="request_user" HeaderText="Username" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="left"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Answer">
                                            <ItemTemplate>
                                                <asp:TextBox ID="taxAns" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container,"DataItem.answer")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="answer_date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="answer_user" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="answer_id" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="I_VERIFICATION_ID" HeaderText="id" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="sz_case_id" HeaderText="case_id" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                        <%--Change added on 18/07/2015 for scan and upload--%>
                                        <asp:TemplateColumn HeaderText="Verification">
                                            <ItemTemplate>
                                                <a id="caseDetailScan" href="#" runat="server"  onclick='<%# "test(" + "\""+ Eval("sz_bill_number")+"\""+",\"" + Eval("I_VERIFICATION_ID")  +"\""+ ",\""  + Eval("Specialty")  +"\""+ ");"%>'
                                                    title="Scan/Upload" >Scan/Upload</a>
                                                <asp:LinkButton ID="lnkscanPopup" runat="server" CausesValidation="false" CommandName="Scan" Visible="false"
                                                    Text="Scan" OnClick="lnkscanPopup_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.sz_bill_number") +";"+ DataBinder.Eval(Container, "DataItem.I_VERIFICATION_ID")+";"+ DataBinder.Eval(Container, "DataItem.Specialty")%>'></asp:LinkButton>
                                                <a id="lnkuplaodPopup" href="#" onclick='<%# "showUploadFilePopupOnAnswerVerification(" + "\""+ Eval("sz_bill_number")+"\""+",\"" + Eval("I_VERIFICATION_ID")  +"\""+ ",\"" + Eval("Specialty")  +"\""+ ",\"" + Eval("answer")  +"\""+ ");"%>'>
                                                    Upload</a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Specialty" HeaderText="Speciality" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                        <%--Change added on 18/07/2015 for scan and upload--%>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSaveSendRequest" runat="server" Text="Save" OnClick="btnSaveSendRequest_Click" />&nbsp<asp:Button
                                ID="btnCancelSendRequest" Text="Cancel" runat="server" OnClick="btnCancelSendRequest_Click" />
                        </td>
                    </tr>
                </table>
                <dx:ASPxPopupControl ID="PanelVerificationAnswer" runat="server" CloseAction="CloseButton" 
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="PanelVerificationAnswer" HeaderText="Verification Answer" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="400px" PopupHorizontalOffset="10" PopupVerticalOffset="10"   AutoUpdatePosition="true"
        ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
                <asp:LinkButton ID="lbn_job_det1" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
                <asp:LinkButton ID="lbn_job_det2" runat="server" Text="View Job Details" Font-Names="Verdana">
                    <asp:LinkButton ID="lbn_job_det4" runat="server" Text="View Job Details" Font-Names="Verdana">
                    </asp:LinkButton>
                </asp:LinkButton>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det"
                PopupDragHandleControlID="pnlSaveDescriptionHeader" PopupControlID="pnlSaveDescription"
                DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lbn_job_det1"
                PopupDragHandleControlID="divgridHeader" PopupControlID="divgrid" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="lbn_job_det2"
                PopupDragHandleControlID="pnlUploadFile" PopupControlID="pnlUploadFile">
            </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="lbn_job_det4"
                PopupDragHandleControlID="pnlVerificationSend" BehaviorID="modal" PopupControlID="pnlVerificationSend">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none" ID="Panel1" runat="server" Height="200px" Width="700px"
                BackColor="white">
                <div style="visibility: hidden">
                    <asp:Button ID="btnGenDenTxt" runat="server" Text="" OnClick="btnGenDenTxt_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlupdategrid" runat="server">
        <ContentTemplate>
            <asp:Panel Style="display: none; background-color: #DBE6FA; width: 400px; height=0px;"
                ID="divgrid" runat="server">
                <%--  <asp:Panel  id="divgrid" style="DISPLAY: none;background-color:#DBE6FA;width:400px;height=2px;" runat="server">--%>
                <div style="position: relative; text-align: left; background-color: #8babe4;" id="divgridHeader">
                    <asp:Label ID="lblHeaderValue" runat="server" Font-Bold="true" CssClass="lbl"></asp:Label>
                </div>
                <table class="TDPart">
                    <tr>
                        <td>
                            <asp:Label ID="lblcurrentDate" runat="server" Text="Date" Font-Bold="true" CssClass="lbl"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcurrntDateValue" runat="server" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVeridate" runat="server" Text="Verification Date" Font-Bold="true"
                                CssClass="lbl"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblveridatevalue" runat="server" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblupverification" runat="server" Text="Verification Type" Font-Bold="true"
                                CssClass="lbl"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblupverificationdesc" runat="server" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblBillValue" runat="server" Text="" Visible="false" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDesc" runat="server" Text="Description" Font-Bold="true" CssClass="lbl"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescValue" runat="server" Text="" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="upFile" runat="server" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnlupload" runat="server" Text="Uplaod" Visible="false" OnClick="btnUploadFile_Click"
                                            CssClass="lbl" />
                                        <asp:LinkButton ID="lnkScanuplaod" runat="server" Text="Scan" Visible="false" OnClick="lnkScanuplaod_Click"
                                            CssClass="lbl"></asp:LinkButton>
                                        <a id="caseDetailScan" href="#" runat="server" onclick="OpenBillVerificationScan()"
                                                    title="Scan/Upload" class="lbl scanlbl">Scan/Upload</a> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Cancel" class="Buttons" OnClientClick="javascript:unload();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hfdenialReason" runat="server" />
                            <asp:HiddenField runat="server" ID="hfremovedenialreason" />
                            <asp:HiddenField ID="hdnCaseId" runat="server" />
                            <asp:HiddenField runat="server" ID="hdnBillNumber" />
                            <asp:HiddenField runat="server" ID="hdnSpecialty" />
                            <asp:HiddenField runat="server" ID="hdnStatusCode" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnlupload" />
        </Triggers>
    </asp:UpdatePanel>
    <dx:ASPxPopupControl ID="AddGeneralDenial" runat="server" CloseAction="CloseButton" HeaderStyle-BackColor="#C1DCFF"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="AddGeneralDenial"
        HeaderText="Add General Denial" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="550px"
        ToolTip="Add General Denial" PopupHorizontalOffset="0" PopupVerticalOffset="0"  
        AutoUpdatePosition="true" RenderIFrameForPopupElements="Default" 
        Height="500px" EnableHierarchyRecreation="True">
        <ClientSideEvents CloseButtonClick="CloseAddGeneralDenial" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="DelDocImages" runat="server" CloseAction="CloseButton"  Modal="true"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="DelDocImages" HeaderText="Delete Document Images" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Font-Bold="true" AllowDragging="True" EnableAnimation="False" EnableViewState="True"
        Width="900px" ToolTip="Delete Document Images" PopupHorizontalOffset="0" PopupVerticalOffset="0"
          AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        Height="500px" EnableHierarchyRecreation="True">
        
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
          <ClientSideEvents Closing="popup_Closing" />
    </dx:ASPxPopupControl>

     <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>  
    <div id="dvVerificationPopUp" style="overflow: hidden; background-color: #FFF;">
        <iframe id="iFrmVerificationPopUp" name="iFrmVerificationPopUp" frameborder="0" scrolling="auto"></iframe>
    </div>
     <div id="dvVerificationSent" style="overflow: hidden; background-color: #FFF;">
        <iframe id="iFrmVerificationSent" name="iFrmVerificationSent" frameborder="0" scrolling="auto"></iframe>
    </div>
          <div id="dvDenialPopUp" style="overflow: hidden; background-color: #FFF;">
        <iframe id="iFrmDenialPopUp" name="iFrmDenialPopUp" frameborder="0" scrolling="auto"></iframe>
    </div> 
    <div id="dvEORPopUp" style="overflow: hidden; background-color: #FFF;">
        <iframe id="iFrmEORPopUp" name="iFrmEORPopUp" frameborder="0" scrolling="auto"></iframe>
    </div>      
</asp:Content>