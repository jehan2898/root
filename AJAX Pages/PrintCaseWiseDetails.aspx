<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="PrintCaseWiseDetails.aspx.cs" Inherits="PrintCaseWiseDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="pdftextbox" Namespace="pdftextbox" TagPrefix="ptb" %>
<%@ Register Assembly="pdfcheckbox" Namespace="pdfcheckbox" TagPrefix="pcb" %>
<%@ Register Assembly="pdfradiobutton" Namespace="pdfradiobutton" TagPrefix="prb" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">

        function showPopup(caseid, i_id) {
            alert("Page under construction. Will be upload soon.");
            alert(i_id);
            var url = 'Bill_Sys_MG2_Convert_to_Bill.aspx';
            ShowPopup.SetContentUrl(url);
            ShowPopup.Show();
            return false;
        }

        function Clear() {
            
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtIndividualProvider').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtTelephone').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtFaxNo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtApproval').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtDateofService').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtDateofApplicable').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtSpoke').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_Txtspecktoanyone').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtAddressRequired').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_Txtaboveon').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtProviderDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtByPrintNameD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtTitleD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtDateD').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtSectionE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtMedicalProfes').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtByPrintNameE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtTitleE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtDateE').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtByPrintNameF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtTitleF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtDateF').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtClaimantDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuislineChar').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline1').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline2').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline3').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline4').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline5').value = '';

            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_Chkdid').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_Chkdidnot').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkAcopy').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkIAmnot').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChktheSelf').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkGranted').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkGrantedinPart').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkWithoutPrejudice').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkDenied').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkBurden').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkSubstantially').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkMadeE').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkChairE').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkIrequestG').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkMadeG').checked = false
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ChkChairG').checked = false

            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_ddlGuidline').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_DDLAttendingDoctors').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_extddlSpeciality').selectedIndex = 0;

            document.getElementById('<%= grdProcedure.ClientID %>').style.display = 'none';

             return false;
         }

        function SplitS(combo) {
            debugger;
             //  alert('hi');
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuislineChar').value = '';

             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline1').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline2').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline3').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline4').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline5').value = '';


             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');

                 for (var i = 0; i < selected.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuislineChar').value = selected[0];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef1').value = selected[0];
                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline1').value = selected[1];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef2').value = selected[1];
                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline2').value = selected[2];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef3').value = selected[2];
                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline3').value = selected[3];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef4').value = selected[3];
                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline4').value = selected[4];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef5').value = selected[4];
                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel3_TxtGuidline5').value = selected[5];
                             //document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_txtMG2_GudRef6').value = selected[5];
                             break;
                     }
                 }

                 //-- MG2.1

                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef1').value = '';
                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef2').value = '';
                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef3').value = '';
                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef4').value = '';
                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef5').value = '';
                 document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef6').value = '';

                 var selectGudRef1 = combo.options[combo.selectedIndex].value;
                 selectGudRef1 = selectGudRef1.replace('-', '');

                 for (var i = 0; i < selectGudRef1.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef1').value = selectGudRef1[0];
                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef2').value = selectGudRef1[1];
                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef3').value = selectGudRef1[2];
                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef4').value = selectGudRef1[3];
                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef5').value = selectGudRef1[4];
                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef6').value = selectGudRef1[5];
                             break;
                     }
                 }
             }
         }

         function SplitSMG1(combo) {
             //alert('hi');
             
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1Guideline').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineOne').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineTwo').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineThree').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineFour').value = '';



             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');
                 for (var i = 0; i < selected.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1Guideline').value = selected[0];
                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineOne').value = selected[1];
                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineTwo').value = selected[2];
                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineThree').value = selected[3];
                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel1_txtMG1GuidelineFour').value = selected[4];
                             break;
                     }
                 }
             }
         }

         function SplitSMG21(combo) {
             //  alert('hi');
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef1').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef2').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef3').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef4').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef5').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef6').value = '';

             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');

                 for (var i = 0; i < selected.length; i++) {
                     debugger;
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef1').value = selected[0];

                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef2').value = selected[1];

                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef3').value = selected[2];

                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef4').value = selected[3];

                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef5').value = selected[4];

                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef6').value = selected[5];

                             break;
                     }
                 }
             }
         }

         function SplitSMG21_3(combo) {
             //  alert('hi');
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef11').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef12').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef13').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef14').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef15').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef16').value = '';

             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');

                 for (var i = 0; i < selected.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef11').value = selected[0];

                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef12').value = selected[1];

                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef13').value = selected[2];

                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef14').value = selected[3];

                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef15').value = selected[4];

                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef16').value = selected[5];

                             break;
                     }
                 }
             }
         }

         function SplitSMG21_4(combo) {

             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef21').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef22').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef23').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef24').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef25').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef26').value = '';

             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');

                 for (var i = 0; i < selected.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef21').value = selected[0];

                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef22').value = selected[1];

                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef23').value = selected[2];

                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef24').value = selected[3];

                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef25').value = selected[4];

                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef26').value = selected[5];

                             break;
                     }
                 }

             }

         }

         function SplitSMG21_5(combo) {

             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef31').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef32').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef33').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef34').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef35').value = '';
             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef36').value = '';

             var selected = combo.options[combo.selectedIndex].value.trim();
             if (selected != '--Select--') {
                 selected = selected.replace('-', '');

                 for (var i = 0; i < selected.length; i++) {
                     switch (i) {
                         case 0:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef31').value = selected[0];

                             break;
                         case 1:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef32').value = selected[1];

                             break;
                         case 2:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef33').value = selected[2];

                             break;
                         case 3:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef34').value = selected[3];

                             break;
                         case 4:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef35').value = selected[4];

                             break;
                         case 5:
                             document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel4_txtMG2_GudRef36').value = selected[5];

                             break;
                     }
                 }
             }
         }

    </script>

    <script type="text/javascript">
        function SplitSMG11_One(combo) {                       
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_One').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxOne').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxTwo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxThree').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxFour').value = '';

            var selected = combo.options[combo.selectedIndex].value.trim();
            if (selected != '--Select--') {
                selected = selected.replace('-', '');

                for (var i = 0; i < selected.length; i++) {
                    switch (i) {
                        case 0:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_One').value = selected[0];

                            break;
                        case 1:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxOne').value = selected[1];

                            break;
                        case 2:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxTwo').value = selected[2];

                            break;
                        case 3:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxThree').value = selected[3];

                            break;
                        case 4:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_BoxFour').value = selected[4];

                            break;
                    }
                }
            }
        }

        function SplitSMG11_Two(combo) {
            //  alert('hi');
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_two').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Five').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_Box_Six').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Seven').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Eight').value = '';

            var selected = combo.options[combo.selectedIndex].value.trim();
            if (selected != '--Select--') {
                selected = selected.replace('-', '');

                for (var i = 0; i < selected.length; i++) {
                    switch (i) {
                        case 0:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_two').value = selected[0];

                            break;
                        case 1:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Five').value = selected[1];

                            break;
                        case 2:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_Box_Six').value = selected[2];

                            break;
                        case 3:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Seven').value = selected[3];

                            break;
                        case 4:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_Eight').value = selected[4];

                            break;
                    }
                }
            }
        }

        function SplitSMG11_Three(combo) {
            //  alert('hi');
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_three').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_BoxNine').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_ten').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_Box_eleven').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_twelve').value = '';

            var selected = combo.options[combo.selectedIndex].value.trim();
            if (selected != '--Select--') {
                selected = selected.replace('-', '');

                for (var i = 0; i < selected.length; i++) {
                    switch (i) {
                        case 0:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_three').value = selected[0];

                            break;
                        case 1:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_BoxNine').value = selected[1];

                            break;
                        case 2:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_ten').value = selected[2];

                            break;
                        case 3:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txtGuideline_Box_eleven').value = selected[3];

                            break;
                        case 4:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_twelve').value = selected[4];

                            break;
                    }
                }
            }
        }

        function SplitSMG11_Four(combo) {
            //  alert('hi');
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_four').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_thirteen').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_fourteen').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_fifteen').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Sixteen').value = '';

            var selected = combo.options[combo.selectedIndex].value.trim();
            if (selected != '--Select--') {
                selected = selected.replace('-', '');

                for (var i = 0; i < selected.length; i++) {
                    switch (i) {
                        case 0:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_four').value = selected[0];

                            break;
                        case 1:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_thirteen').value = selected[1];

                            break;
                        case 2:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_fourteen').value = selected[2];

                            break;
                        case 3:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Box_fifteen').value = selected[3];

                            break;
                        case 4:
                            document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_TabPanel2_txt_Guideline_Sixteen').value = selected[4];

                            break;
                    }
                }
            }
        }

    </script>
    <asp:UpdatePanel ID="UP_MG" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <asp:Image ID="img1" runat="server" Style="position: absolute; z-index: 1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....." Height="50px" Width="50px"></asp:Image>
                    <asp:Image ID="Image1" runat="server" Style="position: absolute; z-index: 1; left: 50%; top: 290%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....." Height="50px" Width="50px"></asp:Image>
                    <asp:Image ID="Image2" runat="server" Style="position: absolute; z-index: 1; left: 50%; top: 400%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....." Height="50px" Width="50px"></asp:Image>
                    <asp:Image ID="Image3" runat="server" Style="position: absolute; z-index: 1; left: 50%; top: 150%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....." Height="50px" Width="50px"></asp:Image>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxcontrol:TabContainer ID="tabcontainerPatientEntry" OnActiveTabChanged="tabcontainerPatientEntry_ActiveTabChanged"
                runat="Server" AutoPostBack="True" ActiveTabIndex="1">
                <ajaxcontrol:TabPanel runat="server" ID="tabPnlNF2" TabIndex="0" Visible="false" >
                    <HeaderTemplate>
                        <div style="width: 60px;" class="lbl">
                            Print NF2
                        </div>
                    </HeaderTemplate>

                    <ContentTemplate>
                        <div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <UserMessage:MessageControl ID="usrMsgNF2" runat="server"></UserMessage:MessageControl>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <tr>

                                    <td class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Name
                                        </div>
                                    </td>
                                    <td class="tablecellSpace"></td>
                                    <td>
                                        <ptb:PDFTextBox ID="txt_NF2_Insurer_Name" AssociatedPDFControlName="SZ_INSURANCE_NAME" runat="server" Width="50%"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Address
                                        </div>
                                    </td>
                                    <td class="tablecellSpace">&nbsp;</td>
                                    <td>
                                        <ptb:PDFTextBox Width="100%" ID="txt_NF2_Insurer_Address" AssociatedPDFControlName="SZ_INSURANCE_ADDRESS" AutoPostBack="True" runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="13%" class="tablecellSpace">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Phone Number
                                        </div>
                                    </td>
                                    <td class="tablecellSpace">&nbsp;</td>
                                    <td>
                                        <ptb:PDFTextBox ID="txt_NF2_Phone_Number" runat="server" Width="13%"></ptb:PDFTextBox>
                                </tr>
                                <tr>
                                    <td width="13%" class="tablecellSpace">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Insurer's Claim Representative*
                                        </div>
                                    </td>
                                    <td class="tablecellSpace"></td>
                                    <td>
                                        <ptb:PDFTextBox Width="99%" ID="txt_NF2_Insurer_Claim" AssociatedPDFControlName="SZ_INSURER_CLAIM_REPRESENTATIVE" runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <%--   <tr>
                            <td colspan="10" height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 100%;">&nbsp;<b class="txt3"></b>
                            </td>
                        </tr>--%>
                                <tr>
                                    <td class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Date:
                                        </div>
                                    </td>
                                    <td class="tablecellSpace"></td>
                                    <td>
                                        <span class="tablecellControl">
                                            <ptb:PDFTextBox Width="15%" ID="txt_NF2_Date" AssociatedPDFControlName="DT_TODAY_DATE" runat="server" onkeypress="return clickButton1(event,'/')"
                                                MaxLength="10"></ptb:PDFTextBox>
                                            <asp:ImageButton ID="imgbtnDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <ajaxcontrol:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_NF2_Date"
                                                PopupButtonID="imgbtnDate" Enabled="True" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Policy Holder
                                        </div>
                                    </td>
                                    <td class="tablecellSpace">&nbsp;</td>
                                    <td>
                                        <ptb:PDFTextBox Width="100%" ID="txt_NF2_Policy_Holder" AssociatedPDFControlName="SZ_POLICY_HOLDER" AutoPostBack="True" runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="13%" class="tablecellSpace">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Policy Number
                                        </div>
                                    </td>
                                    <td class="tablecellSpace">&nbsp;</td>
                                    <td>
                                        <ptb:PDFTextBox ID="txt_NF2_Policy_Number" AssociatedPDFControlName="SZ_POLICY_NUMBER" runat="server" Width="13%"></ptb:PDFTextBox>
                                </tr>
                                <tr>
                                    <td class="tablecellLabel">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Date of Accident:
                                        </div>
                                    </td>
                                    <td class="tablecellSpace"></td>
                                    <td>
                                        <span class="tablecellControl">
                                            <ptb:PDFTextBox Width="15%" ID="txt_NF2_Date_OF_Accident" AssociatedPDFControlName="DT_ACCIDENT_DATE" runat="server" onkeypress="return clickButton1(event,'/')"
                                                MaxLength="10"></ptb:PDFTextBox>
                                            <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <ajaxcontrol:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txt_NF2_Date_OF_Accident"
                                                PopupButtonID="imgbtnDateofAccident" Enabled="True" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="13%" class="tablecellSpace">
                                        <div class="td-PatientInfo-lf-desc-ch">
                                            Claim Number
                                        </div>
                                    </td>
                                    <td class="tablecellSpace">&nbsp;</td>
                                    <td>
                                        <ptb:PDFTextBox ID="txt_NF2_Claim_Number" AssociatedPDFControlName="SZ_CLAIM_NUMBER" runat="server" Width="13%"></ptb:PDFTextBox>
                                </tr>


                            </table>
                        </div>
                        <div>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 90%">
                                                    <div>
                                                        To enable us to determine if you are entitled to benefits under the New York No-Fault Law, Please complete this form and return it promptly.
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 90%">
                                                    <div>
                                                        IMPORTANT:  1. To be eligible for benefits you must complete and sign this Application<br></br>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. You must sign any attached authorization(s).<br></br>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. Return promptly with copies of any bills you have received to date.
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <%--<tr>
                            <td colspan="10" height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 100%;">&nbsp;<b class="txt3"></b>
                            </td>
                        </tr>--%>

                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                <div>
                                                    Name and Address of Applicant
                                                </div>
                                            </td>
                                            <td>
                                                <ptb:PDFTextBox Width="60%" ID="txt_NF2_Patient_Name" AssociatedPDFControlName="SZ_PATIENT_NAME" runat="server"></ptb:PDFTextBox>
                                            </td>
                                            <td>
                                                <ptb:PDFTextBox Width="60%" ID="txt_NF2_Patient_Address" AssociatedPDFControlName="SZ_PATIENT_ADDRESS" runat="server"></ptb:PDFTextBox>
                                            </td>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <div class="td-PatientInfo-lf-desc-ch">
                                                        Your Name
                                                    </div>
                                                </td>
                                                <td align="left">
                                                    <ptb:PDFTextBox ID="txt_NF2_Name" AssociatedPDFControlName="SZ_PATIENT_NAME" runat="server" Width="53%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" class="td-PatientInfo-lf-desc-ch">
                                                    <div>
                                                        Phone Nos.
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_PhoneNo" AssociatedPDFControlName="SZ_PATIENT_PHONE" runat="server"></ptb:PDFTextBox>

                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">Home
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_HomeNo" AssociatedPDFControlName="SZ_HOME_PHONE" runat="server"></ptb:PDFTextBox>

                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">Business
                                        </>
                                        <td>
                                            <ptb:PDFTextBox ID="txt_NF2_Business" AssociatedPDFControlName="SZ_WORK_PHONE" runat="server"></ptb:PDFTextBox>
                                        </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Address
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox Width="100%" ID="txt_NF2_Address" AssociatedPDFControlName="SZ_PATIENT_ADDRESS" runat="server"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                <div>
                                                    Date of birth:
                                                </div>
                                            </td>
                                            <td>
                                                <span class="tablecellControl">
                                                    <ptb:PDFTextBox ID="txt_NF2_Date_Of_Birth" AssociatedPDFControlName="DT_BIRTH_DATE" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        MaxLength="10"></ptb:PDFTextBox>
                                                    <asp:ImageButton ID="imgbtnDateofBirth1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_NF2_Date_Of_Birth"
                                                        PopupButtonID="imgbtnDateofBirth1" Enabled="True" />
                                                </span>
                                            </td>

                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                <div>
                                                    Social Security No:
                                                </div>
                                            </td>
                                            <td>
                                                <ptb:PDFTextBox ID="txt_NF2_SSN" AssociatedPDFControlName="SZ_SSN" runat="server" Width="50%"></ptb:PDFTextBox>
                                            </td>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Date and Time of Accident:
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox Width="15%" ID="txt_NF2_Date_Time_Of_Accident" AssociatedPDFControlName="DT_ACCIDENT_DATE" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnDateTimeofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_NF2_Date_Time_Of_Accident"
                                                            PopupButtonID="imgbtnDateTimeofBirth" Enabled="True" />
                                                        &nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtAccidentTime" AssociatedPDFControlName="DT_ACCIDENT_TIME" runat="server" Width="20%"></ptb:PDFTextBox>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                                <div>
                                                                    Place of Accident (Street), City or Town and State
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <ptb:PDFTextBox ID="txt_NF2_Place_Of_Accident" AssociatedPDFControlName="SZ_PLACE_OF_ACCIDENT" runat="server" Width="80%"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" class="td-PatientInfo-lf-desc-ch">
                                                    <div>
                                                        Brief Description of Accident
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_Accident_Description" AssociatedPDFControlName="SZ_BRIEF_DESCRIPTION" runat="server" Width="100%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%" class="td-PatientInfo-lf-desc-ch">
                                                    <div>
                                                        Describe your injury
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_Injury" AssociatedPDFControlName="SZ_DESCRIBE_INJURY" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 80%">
                                                    <div>
                                                        Identity of Vehicle you Occupied or Operated at the time of Accident:
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 29%" align="left">
                                                    <div class="td-PatientInfo-lf-desc-ch">
                                                        Owner's Name
                                                    </div>
                                                </td>
                                                <td align="left">
                                                    <ptb:PDFTextBox ID="txt_NF2_Owner_Name" AssociatedPDFControlName="SZ_PATIENT_POLICY_NAME" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">This vehicle was:
                                            
                                                </td>
                                                <td colspan="4" width="50%">
                                                    <prb:PDFRadioButton ID="chkBus" GroupName="grpVehicle" Checked="true" AssociatedPDFControlName="CHK_BUS" runat="server" Text="A Bus or School Bus" Enabled="true"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkTruck" GroupName="grpVehicle" Checked="false" AssociatedPDFControlName="CHK_TRUCK" runat="server" Text="A Truck" Enabled="true"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkAutoMobile" GroupName="grpVehicle" Checked="false" AssociatedPDFControlName="CHK_AUTOMOBILE" runat="server" Text="An Automobile" Enabled="true"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkMotorCycle" GroupName="grpVehicle" Checked="false" AssociatedPDFControlName="CHK_MOTORCYCLE" runat="server" Text="A Motorcycle" Enabled="true"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Were you the driver of the motor vehicle?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkVehicleYes" GroupName="grpDriver" AssociatedPDFControlName="CHK_YES_DRIVERMOTORVEHICLE" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkVehicleNo" GroupName="grpDriver" AssociatedPDFControlName="CHK_NO_DRIVERMOTORVEHICLE" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Were you a passenger in the motor vehicle?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkPassengerYes" GroupName="grpPassenger" AssociatedPDFControlName="CHK_YES_PASSENGERMOTORVEHICLE" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkPassengerNo" GroupName="grpPassenger" AssociatedPDFControlName="CHK_NO_PASSENGERMOTORVEHICLE" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Were you a pedestrian?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkPedestrianYes" GroupName="grppedestrian" AssociatedPDFControlName="CHK_YES_PEDESTRIAN" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkPedestrianNo" GroupName="grppedestrian" AssociatedPDFControlName="CHK_NO_PEDESTRIAN" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Were you a member of our policy holder's household?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkHouseHoldYes" GroupName="grpHousehold" AssociatedPDFControlName="CHK_YES_POLICYHOLDERHOUSEHOLD" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkHouseHoldNo" GroupName="grpHousehold" AssociatedPDFControlName="CHK_NO_POLICYHOLDERHOUSEHOLD" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Do you or a relative with whom you reside on a motor cycle?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkRelativeYes" GroupName="grpRelative" AssociatedPDFControlName="CHK_YES_RELATIVERESIDEOWN" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkRelativeNo" GroupName="grpRelative" AssociatedPDFControlName="CHK_NO_RELATIVERESIDEOWN" runat="server" Text="No"></prb:PDFRadioButton>
                                                    <%--<pcb:PDFCheckBox ID="chkRelativeYes" AssociatedPDFControlName="CHK_YES_RELATIVERESIDEOWN" runat="server" Text="Yes" />
                                            <pcb:PDFCheckBox ID="chkRelativeNo" AssociatedPDFControlName="CHK_NO_RELATIVERESIDEOWN" runat="server" Text="No" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 20%">Were you treated by a doctor(s) or other person(s) furnishing health services?
                                                </td>
                                                <td width="50%">
                                                    <prb:PDFRadioButton ID="chkTreatedYes" GroupName="grpFurnishing" AssociatedPDFControlName="CHK_YES_FURNISHINGHEALTHSERVICE" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkTreatedNo" GroupName="grpFurnishing" AssociatedPDFControlName="CHK_NO_FURNISHINGHEALTHSERVICE" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">If yes, Name and Address of such Doctor(s) or Person(s)
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txtCompanyName" AssociatedPDFControlName="SZ_COMPANY_NAME" runat="server"></ptb:PDFTextBox>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txtCompanyAddress" AssociatedPDFControlName="SZ_COMPANY_ADDRESS" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                <div>
                                                    If you were treated at a Hospital(s), you were an:
                                                </div>
                                            </td>
                                            <td>
                                                <prb:PDFRadioButton ID="chkPatientYes" GroupName="grpHospital" AssociatedPDFControlName="CHK_OUT_PATIENT" runat="server" Text="Out-Patient?"></prb:PDFRadioButton>
                                                <prb:PDFRadioButton ID="chkPatientNo" GroupName="grpHospital" AssociatedPDFControlName="CHK_IN_PATIENT" runat="server" Text="In-Patient?"></prb:PDFRadioButton>
                                            </td>
                                        </table>
                                        <td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Date of Admission
                                                    </div>
                                                </td>
                                                <td>
                                                    <td>
                                                        <span class="tablecellControl">
                                                            <ptb:PDFTextBox Width="15%" ID="txt_NF2_DateOfAdmission" AssociatedPDFControlName="SZ_DATE_OF_ADMISSION" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                MaxLength="10"></ptb:PDFTextBox>
                                                            <asp:ImageButton ID="imgbtnDateOfAdmission" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxcontrol:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt_NF2_DateOfAdmission"
                                                                PopupButtonID="imgbtnDateOfAdmission" Enabled="True" />
                                                        </span>
                                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Hospital's Name and Address:
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_HospitalAddress" AssociatedPDFControlName="SZ_HOSPITAL_ADDRESS" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Amount of Health Bills to date:
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_Amount" AssociatedPDFControlName="SZ_AMOUNT_HEALTHBILL" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Will you have more Health(s) Treatment?
                                                    </div>
                                                </td>
                                                <td>

                                                    <prb:PDFRadioButton ID="chkHealthYes" GroupName="grpTreatment" AssociatedPDFControlName="CHK_YES_MOREHEALTHTREATMENT" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkHealthNo" GroupName="grpTreatment" AssociatedPDFControlName="CHK_NO_MOREHEALTHTREATMENT" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 50%">At the time of your accident were you in the course of your Employeement?
                                            </td>
                                            <td width="50%">

                                                <prb:PDFRadioButton ID="chkAccidentYes" GroupName="grpEmployeement" AssociatedPDFControlName="CHK_YES_COURSEOFEMPLOYMENT" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                <prb:PDFRadioButton ID="chkAccidentNo" GroupName="grpEmployeement" AssociatedPDFControlName="CHK_NO_COURSEOFEMPLOYMENT" runat="server" Text="No"></prb:PDFRadioButton>
                                            </td>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Did you Lose time from work?
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_LoseTime" AssociatedPDFControlName="SZ_DID_YOU_LOOSETIME" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Date absence from work began:
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox Width="15%" ID="txt_NF2_AbsenceDate" AssociatedPDFControlName="DT_ABSENCEFROM_WORK_BEGIN" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnDateofAbsence" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_NF2_AbsenceDate"
                                                            PopupButtonID="imgbtnDateofAbsence" Enabled="True" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Have you returned to work?
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_Work" AssociatedPDFControlName="SZ_RETURNED_TO_WORK" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        If yes, Date returned to work:
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox Width="15%" ID="txt_NF2_DateReturnedToWork" AssociatedPDFControlName="DT_RETURN_TO_WORK" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnDateReturnedToWork" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txt_NF2_DateReturnedToWork"
                                                            PopupButtonID="imgbtnDateReturnedToWork" Enabled="True" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                        <div>
                                                            Amount of time lost from work:
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td>
                                                        <ptb:PDFTextBox ID="txt_NF2_AmountOfTime" AssociatedPDFControlName="SZ_AMOUNT_OF_TIME" runat="server" Width="50%"></ptb:PDFTextBox>
                                                    </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        What are your gross average weekly earnings?
                                                    </div>
                                                </td>
                                                <td class="tablecellSpace"></td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_Average" AssociatedPDFControlName="FLT_GROSS_WEEKLY_EARNINGS" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Number of days you work per week:
                                                    </div>
                                                    <td class="tablecellSpace"></td>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_NumberOfDays" AssociatedPDFControlName="I_NUMBEROFDAYS_WORK_WEEKLY" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <div>
                                                        Number of hours you work per day:
                                                    </div>
                                                </td>
                                                <td>
                                                    <ptb:PDFTextBox ID="txt_NF2_NumberOfHours" AssociatedPDFControlName="I_NUMBEROFHOURS_WORK_WEEKLY" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">Were you receiving unemployeement benefits at the time of Accident?
                                                </td>
                                                <td width="35%">

                                                    <prb:PDFRadioButton ID="chkUnemployeementYes" GroupName="grpAccident" AssociatedPDFControlName="CHK_YES_RECIEVING_UNEMPLOYMENT" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkUnemployeementNo" GroupName="grpAccident" AssociatedPDFControlName="CHK_NO_RECIEVING_UNEMPLOYMENT" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 100%">List names and adress of your employer and other employers for one year prior to accident date and give occupation and dates of employment.
                                            
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">Employer and Address
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">Occupation
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">From
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">To
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Employer" AssociatedPDFControlName="SZ_EMPLOYERONE_NAMEADDRESS" runat="server" Width="60%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Occupation" AssociatedPDFControlName="SZ_EMPLOYERONE_OCCUPATION" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_From" AssociatedPDFControlName="SZ_EMPLOYERONE_FROM" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2From" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txt_NF2_From"
                                                            PopupButtonID="imgbtnNF2From" Enabled="True" />
                                                    </span>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_To" AssociatedPDFControlName="SZ_EMPLOYERONE_TO" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2To" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txt_NF2_To"
                                                            PopupButtonID="imgbtnNF2To" Enabled="True" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Employer_Two" AssociatedPDFControlName="SZ_EMPLOYERTWO_NAMEADDRESS" runat="server" Width="60%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Occupation_Two" AssociatedPDFControlName="SZ_EMPLOYERTWO_OCCUPATION" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_From_Two" AssociatedPDFControlName="SZ_EMPLOYERTWO_FROM" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2FromTwo" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txt_NF2_From_Two"
                                                            PopupButtonID="imgbtnNF2FromTwo" Enabled="True" />
                                                    </span>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_To_Two" AssociatedPDFControlName="SZ_EMPLOYERTWO_TO" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2ToTwo" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txt_NF2_To_Two"
                                                            PopupButtonID="imgbtnNF2ToTwo" Enabled="True" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Employer_Three" AssociatedPDFControlName="SZ_EMPLOYERTHREE_NAMEADDRESS" runat="server" Width="60%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">
                                                    <ptb:PDFTextBox ID="txt_NF2_Occupation_Three" AssociatedPDFControlName="SZ_EMPLOYERTHREE_OCCUPATION" runat="server" Width="50%"></ptb:PDFTextBox>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_From_Three" AssociatedPDFControlName="SZ_EMPLOYERTHREE_FROM" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2FromThree" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txt_NF2_From_Three"
                                                            PopupButtonID="imgbtnNF2FromThree" Enabled="True" />
                                                    </span>
                                                </td>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 25%">
                                                    <span class="tablecellControl">
                                                        <ptb:PDFTextBox ID="txt_NF2_To_Three" AssociatedPDFControlName="SZ_EMPLOYERTHREE_TO" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></ptb:PDFTextBox>
                                                        <asp:ImageButton ID="imgbtnNF2ToThree" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txt_NF2_To_Three"
                                                            PopupButtonID="imgbtnNF2ToThree" Enabled="True" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td class="td-PatientInfo-lf-desc-ch" style="width: 30%">As a result of your injury Have you had any other expenses?
                                                </td>
                                                <td>
                                                    <prb:PDFRadioButton ID="chkInjuryYes" GroupName="grpExpenses" AssociatedPDFControlName="CHK_YES_OTHER_EXP" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkInjuryNo" GroupName="grpExpenses" AssociatedPDFControlName="CHK_NO_OTHER_EXP" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 40%">Due to this accident have you received or are you eligible for payments?
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%" class="td-PatientInfo-lf-desc-ch">
                                                    <div>
                                                        Newyork state disability
                                                    </div>
                                                </td>
                                                <td>
                                                    <prb:PDFRadioButton ID="chkDisabilityYes" GroupName="grpDisability" AssociatedPDFControlName="CHK_YES_STATE_DISABILITY" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkDisabilityNo" GroupName="grpDisability" AssociatedPDFControlName="CHK_NO_STATE_DISABILITY" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%" class="td-PatientInfo-lf-desc-ch">
                                                    <div>
                                                        Worker's Compensation
                                                    </div>
                                                </td>
                                                <td>
                                                    <prb:PDFRadioButton ID="chkWCYes" GroupName="grpCompensation" AssociatedPDFControlName="CHK_YES_WORKERS_COMP" runat="server" Text="Yes"></prb:PDFRadioButton>
                                                    <prb:PDFRadioButton ID="chkWCNo" GroupName="grpCompensation" AssociatedPDFControlName="CHK_NO_WORKERS_COMP" runat="server" Text="No"></prb:PDFRadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnAddNF2Details" runat="server" OnClick="btnAddNF2Details_Click" Text="Save NF2 Details" />
                                        <asp:Button ID="btnNF2Print" runat="server" OnClick="btnNF2Print_Click" Text="Print NF2 Pdf" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                            </div>
                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="tabC4Auth" TabIndex="1"
                    >
                    <HeaderTemplate>
                        <div style="width: 85px;" class="lbl">
                            Print C4-AUTH
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <UserMessage:MessageControl ID="usrMSGC4" runat="server"></UserMessage:MessageControl>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <div align="left">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">WCB Case Number</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox
                                                runat="server"
                                                AssociatedPDFControlName="topmostSubform[0].Page1[0].tWCBNumber"
                                                ID="Txt_C4_wcb_case_number"></ptb:PDFTextBox>
                                        </td>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Carrier Case Number</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox
                                                runat="server"
                                                AssociatedPDFControlName="topmostSubform[0].Page1[0].tCarrierNumber"
                                                ID="Txt_C4_Carrier_case_number"></ptb:PDFTextBox>
                                        </td>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Date of Injury</div>
                                        </td>
                                        <td>
                                            <span class="tablecellControl">
                                                <ptb:PDFTextBox Width="69%" ID="Txt_C4_DateOfBirth" AssociatedPDFControlName="topmostSubform[0].Page1[0].tDateOfInjury"
                                                    runat="server" onkeypress="return clickButton1(event,'/')"
                                                    MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="Txt_C4_DateOfBirth"
                                                    PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                            </span>
                                        </td>

                                    </tr>
                                </table>

                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="6" height="28" align="left" valign="middle" class="td-CaseDetails-lf-desc-ch" style="width: 100%;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="28" align="left" valign="middle" class="td-CaseDetails-lf-desc-ch" style="width: 100%;">&nbsp;<b class="txt3">A.</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Patient's Name</div>
                                        </td>
                                        <td colspan="3">
                                            <ptb:PDFTextBox ID="Txt_C4_Patient_Name" runat="server" Width="550px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tPatientName"></ptb:PDFTextBox>
                                        </td>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">SSN #</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox ID="Txt_C4_SSN" runat="server" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSSN"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Address</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Patient_Address" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tPatientAddress"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Address </td>
                                        <td>City </td>
                                        <td>State </td>
                                        <td>Zip </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Employer's Name</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Emp_Name" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tEmpName"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Address</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Emp_Address" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tEmpAddress"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Address </td>
                                        <td>City </td>
                                        <td>State </td>
                                        <td>Zip </td>
                                        <td></td>
                                    </tr>
                                    <%--<tr>
                    <td>
                    <div class="td-PatientInfo-lf-desc-ch">City</div>
                    </td>
                     <td>
                      <ptb:PDFTextBox ID="Txt_C4_Emp_City" runat="server" AssociatedPDFControlName="topmostSubform[0].Page1[0].tInsAddress" ></ptb:PDFTextBox>                                 
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch"> State</div>
                    </td>
                    <td>
                        <ptb:PDFTextBox ID="Txt_C4_Emp_State" runat="server"  AssociatedPDFControlName="tInsAddress" ></ptb:PDFTextBox>
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch">Zip</div>
                    </td>
                     <td>
                        <ptb:PDFTextBox ID="Txt_C4_Emp_Zip" runat="server" AssociatedPDFControlName="tInsAddress"  ></ptb:PDFTextBox>                                 
                    </td>
                </tr>--%>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Insurance Carrier's Name</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Ins_Carrier_Name" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tInsCarrierName"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Address</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Ins_Address" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tInsAddress"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Address </td>
                                        <td>City </td>
                                        <td>State </td>
                                        <td>Zip </td>
                                        <td></td>
                                    </tr>
                                    <%--<tr>
                    <td>
                    <div class="td-PatientInfo-lf-desc-ch">City</div>
                    </td>
                     <td>
                      <ptb:PDFTextBox ID="Txt_C4_Ins_City" runat="server" AssociatedPDFControlName="tInsAddress" ></ptb:PDFTextBox>                                 
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch"> State</div>
                    </td>
                    <td>
                        <ptb:PDFTextBox ID="Txt_C4_Ins_State" runat="server" AssociatedPDFControlName="tInsAddress" ></ptb:PDFTextBox>
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch">Zip</div>
                    </td>
                     <td>
                        <ptb:PDFTextBox ID="Txt_C4_Ins_Zip" runat="server" AssociatedPDFControlName="tInsAddress" ></ptb:PDFTextBox>                                 
                    </td>
                </tr>--%>
                                </table>

                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="6" height="28" align="left" valign="middle" class="td-CaseDetails-lf-desc-ch" style="width: 100%;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="28" align="left" valign="middle" class="td-CaseDetails-lf-desc-ch" style="width: 100%;">&nbsp;<b class="txt3">B.</b>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Attending Doctor's Name</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_AttendingDrName" runat="server" Width="850px" Visible="false" AssociatedPDFControlName="tAttendingDoctorName"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_AttendingDrName_SelectedText" runat="server" Width="850px" Visible="false" AssociatedPDFControlName="topmostSubform[0].Page1[0].tAttendingDoctorName"></ptb:PDFTextBox>
                                            <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="850px" Connection_Key="Connection_String"
                                                Procedure_Name="SP_MST_DOCTOR" Flag_Key_Value="GETDOCTORLIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                OnextendDropDown_SelectedIndexChanged="extddlDoctor_SelectedIndexChanged"></extddl:ExtendedDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Address</div>
                                        </td>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Doc_Address" runat="server" Width="850px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tDoctorAddress"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Address </td>
                                        <td>City </td>
                                        <td>State </td>
                                        <td>Zip </td>
                                        <td></td>
                                    </tr>
                                    <%--<tr>
                    <td>
                    <div class="td-PatientInfo-lf-desc-ch">City</div>
                    </td>
                     <td>
                      <ptb:PDFTextBox ID="Txt_C4_Doc_City" runat="server" AssociatedPDFControlName="tDoctorAddress" ></ptb:PDFTextBox>                                 
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch"> State</div>
                    </td>
                    <td>
                        <ptb:PDFTextBox ID="Txt_C4_Doc_State" runat="server" AssociatedPDFControlName="tDoctorAddress"></ptb:PDFTextBox>
                    </td>
                     <td>
                     <div class="td-PatientInfo-lf-desc-ch">Zip</div>
                    </td>
                     <td>
                        <ptb:PDFTextBox ID="Txt_C4_Doc_Zip" runat="server" AssociatedPDFControlName="tDoctorAddress" ></ptb:PDFTextBox>                                 
                    </td>
                </tr>--%>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Provider's Authorization No</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox ID="Txt_C4_Doc_Provider_Auth_No" runat="server" AssociatedPDFControlName="topmostSubform[0].Page1[0].tProviderAuthorizationNo"></ptb:PDFTextBox>
                                        </td>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Telephone No</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox ID="Txt_C4_TelePhn_No" runat="server" AssociatedPDFControlName="topmostSubform[0].Page1[0].tDoctorTelephnNo"></ptb:PDFTextBox>
                                        </td>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Fax No</div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox ID="Txt_C4_Fax_No" runat="server" AssociatedPDFControlName="topmostSubform[0].Page1[0].tDoctorFaxNo"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="5" class="td-CaseDetails-lf-desc-ch"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" height="28" align="left" valign="middle"
                                            class="td-CaseDetails-lf-desc-ch" style="width: 100%;">&nbsp;<b class="txt3">C.  AUTHORIZATION REQUEST</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                The undersigned requests written authorization for the following <b>special service(s)
                            costing over $1,000</b> or requiring pre-authorization pursuant to the Medical Treatment
                            Guidelines. Do NOT use this form for injuries/illnesses involving the Mid and Low
                            Back, Neck, Knee, and Shoulder; except for the treatment/procedures listed below
                            under Medical Treatment Guideline Procedures Requiring Pre-Authorization. Please
                            use the appropriate Medical Treatment Guideline form if any other procedure/test
                            is being requested.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div><b class="txt3">Authorization Requested:</b></div>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td align="right">
                                            <div class="txt3">
                                                <b class="txt3">Carrier Response: if any service is denied, explain on reverse. </b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="td-PatientInfo-lf-desc-ch">Diagnostic Tests:</div>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Radiology_Services" AssociatedPDFControlName="topmostSubform[0].Page1[0].cRadiologyService"
                                                    runat="server" />
                                                <b class="txt3">Radiology Services (X-Rays, CT Scans, MRI) indicate body part:</b>

                                            </div>
                                        </td>
                                        <td>
                                            <ptb:PDFTextBox ID="Txt_C4_Dia_Test_Radiology_Services" runat="server" Width="200px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tRadiologyService"></ptb:PDFTextBox>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Radiology_Granted" GroupName="c4_Radiology" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticRadiologyGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Radiology_Prejudice" GroupName="c4_Radiology" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticRadiologyPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Radiology_Denied" GroupName="c4_Radiology" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticRadiologyDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Radiology_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnostic_Other"
                                                    runat="server" />
                                                <b class="txt3">Other</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Dia_Test_Other" runat="server" Width="550px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tDiagnosticOther"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Radiology_Other_Granted" GroupName="c4_Radiology_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticOtherGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Radiology_Other_Prejudice" GroupName="c4_Radiology_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticOtherPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Radiology_Other_Denied" GroupName="c4_Radiology_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cDiagnisticOtherDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div style="height: 5px">
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">
                                            <div><b class="txt3">Therapy (including Post Operative):</b></div>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Radiology_Physical_Therapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyPhysicalTherapy"
                                                    runat="server" />
                                                <b class="txt3">Physical Therapy:</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Therapy_Physical_Therapy" runat="server" Width="280px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyPhysicalTherapy"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="3">
                                            <div>
                                                <ptb:PDFTextBox ID="Txt_C4_PhysicalTherapy_times_per_week" runat="server" Width="15px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyPhysicalTimesPerWeek"></ptb:PDFTextBox>
                                                &nbsp;<b class="txt3">times per week for</b>&nbsp;&nbsp;&nbsp;
                      <ptb:PDFTextBox ID="Txt_C4_PhysicalTherapy_weeks" runat="server" Width="15px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyPhysicalWeeks"></ptb:PDFTextBox>
                                                &nbsp;<b class="txt3">weeks</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Therapy_Physical_Therapy_Granted" GroupName="C4_PhysicalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyPhysicalGranted"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                      <prb:PDFRadioButton
                          ID="chk_c4_Therapy_Physical_Therapy_Prejudice" GroupName="C4_PhysicalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyPhysicalPrejudice"
                          runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Therapy_Physical_Therapy_Denied" GroupName="C4_PhysicalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyPhysicalDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Therapy_OccupationalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOccupationalTherapy"
                                                    runat="server" />
                                                <b class="txt3">OccupationalTherapy:</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Therapy_OccupationalTherapy" runat="server" Width="250px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyOccupationalTherapy"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="3">
                                            <div>
                                                <ptb:PDFTextBox ID="Txt_C4_OccupationalTherapy_times_per_week" runat="server" Width="15px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyOccupationalTimesPerWeekFor"></ptb:PDFTextBox>
                                                &nbsp;<b class="txt3">times per week for</b>&nbsp;&nbsp;&nbsp;
                      <ptb:PDFTextBox ID="Txt_C4_OccupationalTherapy_weeks" runat="server" Width="15px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyOccupationallWeeks"></ptb:PDFTextBox>
                                                &nbsp;<b class="txt3">weeks</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_OccupationalTherapy_Granted" GroupName="C4_OccupationalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOccupationalGranted"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_OccupationalTherapy_Prejudice" GroupName="C4_OccupationalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOccupationalPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_OccupationalTherapy_Denied" GroupName="C4_OccupationalTherapy" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOccupationalDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Therapy_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOther"
                                                    runat="server" />
                                                <b class="txt3">Other</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Therapy_Other" runat="server" Width="550px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTherapyOther"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Therapy_Other_Granted" GroupName="C4_Therapy_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOtherGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Therapy_Other_Prejudice" GroupName="C4_Therapy_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOtherPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Therapy_Other_Denied" GroupName="C4_Therapy_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTherapyOtherDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div><b class="txt3">Surgery:</b></div>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Surgery_Type" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergery"
                                                    runat="server" />
                                                <b class="txt3">Type of Surgery (Describe, include use of hardware/surgical implants)</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Surgery_Type" runat="server" Width="170px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSurgeryTypeOfSergery"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Surgery_Type_Granted" GroupName="C4_Surgery_Type" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Surgery_Type_Prejudice" GroupName="C4_Surgery_Type" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Surgery_Type_Denied" GroupName="C4_Surgery_Type" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <ptb:PDFTextBox ID="Txt_C4_Surgery_Type_Descibe" runat="server" Width="650px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSurgeryTypeOfSergeryDescribe"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Surgery_Other_Granted" GroupName="C4_Surgery_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryDescribeGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Surgery_Other_Prejudice" GroupName="C4_Surgery_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryDescribePrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Surgery_Other_Denied" GroupName="C4_Surgery_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSurgeryTypeOfSergeryDescribeDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div><b class="txt3">Treatment:</b></div>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Treatment_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTreatmentOther"
                                                    runat="server" />
                                                <b class="txt3">Other</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Treatment_Other" runat="server" Width="550px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tTreatmentOther"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Treatment_Other_Granted" GroupName="C4_Treatment_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTreatmentOtherGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Treatment_Other_Prejudice" GroupName="C4_Treatment_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTreatmentOtherPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Treatment_Other_Denied" GroupName="C4_Treatment_Other" AssociatedPDFControlName="topmostSubform[0].Page1[0].cTreatmentOtherDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                Medical Treatment Guidelines Procedures Requiring Pre-Authorization (Complete Guideline
                            Reference for each item checked, if necessary. In first box, indicate body part:
                            K = <b>K</b>nee, S = <b>S</b>houlder, B = Mid and Low <b>B</b>ack, N = <b>N</b>eck. In remaining boxes, indicate
                            corresponding section of WCB Medical Treatment Guidelines.)
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div style="height: 10px">
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Lumbar" AssociatedPDFControlName="topmostSubform[0].Page1[0].cLumbar"
                                                    runat="server" />
                                                <b class="txt3">1. Lumbar Fusions</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart1" runat="server" Text="B" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="Txt_C4_Lumbar_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Lumbar_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Lumbar_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart4" runat="server" Text="4" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Lumbar_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart5" runat="server" Text="a" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Lumbar_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Lumbar_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="tLumbarBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>1.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Lumbar_Granted" GroupName="C4_Lumbar" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cLumbarGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Lumbar_Prejudice" GroupName="C4_Lumbar" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cLumbarPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Lumbar_Denied" GroupName="C4_Lumbar" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cLumbarDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Artificial" AssociatedPDFControlName="topmostSubform[0].Page1[0].cArtificial"
                                                    runat="server" />
                                                <b class="txt3">2. Artificial Disk Replacement</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart1" runat="server" Width="18px" AssociatedPDFControlName="tArtificialBodypart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Artificial_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Artificial_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart4" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].Text36\.1\.tArtificialBodypart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart5" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].Text36\.1\.tArtificialBodypart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Artificial_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].Text36\.1\.tArtificialBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>2.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Artificial_Granted" GroupName="C4_Artificial" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cArtificialGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Artificial_Prejudice" GroupName="C4_Artificial" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cArtificialPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Artificial_Denied" GroupName="C4_Artificial" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cArtificialDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Vertebroplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].cVertebroplasty"
                                                    runat="server" />
                                                <b class="txt3">3. Vertebroplasty</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart1" runat="server" Text="B" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart4" runat="server" Text="7" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart5" runat="server" Text="a" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="txt_c4_Vertebroplasty_BodyPart6" runat="server" Text="i" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="txt_c4_Vertebroplasty_BodyPart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>3.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Vertebroplasty_Granted" GroupName="C4_Vertebroplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cVertebroplastyGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Vertebroplasty_Prejudice" GroupName="C4_Vertebroplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cVertebroplastyPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Vertebroplasty_Denied" GroupName="C4_Vertebroplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cVertebroplastyDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Kyphoplasty" AssociatedPDFControlName="cKyphoplasty"
                                                    runat="server" />
                                                <b class="txt3">4. Kyphoplasty</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart1" runat="server" Text="B" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart4" runat="server" Text="7" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart5" runat="server" Text="a" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Kyphoplasty_BodyPart6" runat="server" Text="i" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Kyphoplasty_BodyPart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>4.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Kyphoplasty_Granted" GroupName="C4_Kyphoplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cKyphoplastyGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Kyphoplasty_Prejudice" GroupName="C4_Kyphoplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cKyphoplastyPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Kyphoplasty_Denied" GroupName="C4_Kyphoplasty" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cKyphoplastyDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Electrical" AssociatedPDFControlName="cElectrical"
                                                    runat="server" />
                                                <b class="txt3">5. Electrical Bone Growth Stimulators</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart1" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tElectricalBodypart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Electrical_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Electrical_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart4" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tElectricalBodypart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart5" runat="server" Text="a" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Electrical_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Electrical_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tElectricalBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>5.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Electrical_Granted" GroupName="C4_Electrical" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cElectricalGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Electrical_Prejudice" GroupName="C4_Electrical" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cElectricalPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Electrical_Denied" GroupName="C4_Electrical" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cElectricalDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Spinal" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSpinal"
                                                    runat="server" />
                                                <b class="txt3">6. Spinal Cord Stimulators</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart1" runat="server" Text="B" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="chk_c4_Spinal_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Spinal_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart3" runat="server" Text="E" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Spinal_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart4" runat="server" Text="10" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Spinal_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart5" runat="server" Text="a" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Spinal_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="chk_c4_Spinal_BodyPart6" runat="server" Text="i" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="chk_c4_Spinal_BodyPart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>6.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Spinal_Granted" GroupName="C4_Spinal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSpinalGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Spinal_Prejudice" GroupName="C4_Spinal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSpinalPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Spinal_Denied" GroupName="C4_Spinal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSpinalDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Osteochondral" AssociatedPDFControlName="cOsteochondral"
                                                    runat="server" />
                                                <b class="txt3">7. Osteochondral Autograft</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart1" runat="server" Text="K" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="Txt_C4_Osteochondral_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Osteochondral_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart3" runat="server" Text="D" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Osteochondral_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart4" runat="server" Text="1" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Osteochondral_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart5" runat="server" Text="f" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Osteochondral_BodyPart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Osteochondral_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tOsteochondralBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>7.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Osteochondral_Granted" GroupName="C4_Osteochondral" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cOsteochondralGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Osteochondral_Prejudice" GroupName="C4_Osteochondral" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cOsteochondralPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Osteochondral_Denied" GroupName="C4_Osteochondral" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cOsteochondralDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Autologus" AssociatedPDFControlName="cAutologus"
                                                    runat="server" />
                                                <b class="txt3">8. Autologus Chondrocyte Implantation</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart1" runat="server" Text="K" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="Txt_C4_Autologus_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Autologus_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart3" runat="server" Text="D" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Autologus_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart4" runat="server" Text="1" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Autologus_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart5" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tAutologusBodypart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Autologus_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tAutologusBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>8.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Autologus_Granted" GroupName="C4_Autologus" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cAutologusGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Autologus_Prejudice" GroupName="C4_Autologus" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cAutologusPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Autologus_Denied" GroupName="C4_Autologus" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cAutologusDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Meniscal" AssociatedPDFControlName="topmostSubform[0].Page1[0].cMeniscal"
                                                    runat="server" />
                                                <b class="txt3">9. Meniscal Allograft Transplantation</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart1" runat="server" Text="K" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="Txt_C4_Meniscal_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Meniscal_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart3" runat="server" Text="D" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Meniscal_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart4" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tMeniscalBodypart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart5" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tMeniscalBodypart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Meniscal_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tMeniscalBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>9.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Meniscal_Granted" GroupName="C4_Meniscal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cMeniscalGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Meniscal_Prejudice" GroupName="C4_Meniscal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cMeniscalPrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Meniscal_Denied" GroupName="C4_Meniscal" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cMeniscalDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Knee" AssociatedPDFControlName="topmostSubform[0].Page1[0].cknee"
                                                    runat="server" />
                                                <b class="txt3">10. Knee Arthroplasty (total or partial knee joint replacement)</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart1" runat="server" Text="K" Width="18px" ReadOnly="false"
                        AssociatedPDFControlName="Txt_C4_Knee_BodyPart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Knee_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart3" runat="server" Text="F" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Knee_BodyPart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart4" runat="server" Text="2" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Knee_BodyPart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart5" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tkneeBodypart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Knee_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tkneeBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>10.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Knee_Granted" GroupName="C4_Knee" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].ckneeGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Knee_Prejudice" GroupName="C4_Knee" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].ckneePrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Knee_Denied" GroupName="C4_Knee" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].ckneeDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <pcb:PDFCheckBox
                                                    ID="chk_c4_Subsequent" AssociatedPDFControlName="topmostSubform[0].Page1[0].cSecondOrSubsequentProcedure"
                                                    runat="server" />
                                                <b class="txt3">11. Second or Subsequent Procedure</b>&nbsp;&nbsp;&nbsp;&nbsp;
                    <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart1" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSecondOrSubsequentProcedureBodypart1"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart2" runat="server" Text="-" Width="18px" ReadOnly="false"
                                                    AssociatedPDFControlName="Txt_C4_Subsequent_Proc_BodyPart2"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart3" runat="server" Width="18px" AssociatedPDFControlName="tSecondOrSubsequentProcedureBodypart3"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart4" runat="server" Width="18px" AssociatedPDFControlName="tSecondOrSubsequentProcedureBodypart4"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart5" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSecondOrSubsequentProcedureBodypart5"></ptb:PDFTextBox>
                                                <ptb:PDFTextBox ID="Txt_C4_Subsequent_Proc_BodyPart6" runat="server" Width="18px" AssociatedPDFControlName="topmostSubform[0].Page1[0].tSecondOrSubsequentProcedureBodypart6"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b>11.</b>
                                                <prb:PDFRadioButton
                                                    ID="chk_c4_Subsequent_Granted" GroupName="C4_Subsequent" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSecondOrSubsequentProcedureGranted"
                                                    runat="server" />
                                                &nbsp;<b class="txt3">Granted</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Subsequent_Prejudice" GroupName="C4_Subsequent" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSecondOrSubsequentProcedurePrejudice"
                           runat="server" />
                                                &nbsp;<b class="txt3">Granted w/o Prejudice</b>&nbsp;&nbsp;&nbsp;
                       <prb:PDFRadioButton
                           ID="chk_c4_Subsequent_Denied" GroupName="C4_Subsequent" AssociatedPDFControlName="topmostSubform[0].Page1[0].#area[0].cSecondOrSubsequentProcedureDenied"
                           runat="server" />
                                                &nbsp;<b class="txt3">Denied</b>&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="td-CaseDetails-lf-desc-ch">
                                            <div style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" height="28" align="left" valign="middle"
                                            class="td-CaseDetails-lf-desc-ch" style="width: 100%;">&nbsp;<b class="txt3">STATEMENT OF MEDICAL NECESSITY</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <b>Pursuant to 12 NYCRR 325-1.4(a)(1), it is the attending physician's burden to set
                                forth the medical necessity of the special services required. Failure to do so may
                                delay the authorization process.</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <ptb:PDFTextBox ID="Txt_C4_Statement_Medical1" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tStatement1"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Statement_Medical2" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tStatement2"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Statement_Medical3" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tStatement3"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Statement_Medical4" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tStatement4"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b class="txt3">Date of service of supporting medical in WCB Case File: </b>
                                                <%--<ptb:PDFTextBox ID="Txt_C4_WCB_Case_file" runat="server" Width="250px" AssociatedPDFControlName="tDOS_OfSupportingMedical"></ptb:PDFTextBox>  --%>
                                                <ptb:PDFTextBox ID="Txt_C4_WCB_Case_file1" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="topmostSubform[0].Page2[0].tDOS_OfSupportingMedical"
                                                    MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDOS_WCB" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="Txt_C4_WCB_Case_file1"
                                                    PopupButtonID="imgbtnDOS_WCB" Enabled="True" />
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b class="txt3">(If not already in file, supporting medical must be attached.)</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <b>I</b> certify that I am making the above request for authorization. This request was made to the insurance carrier/self-insurer: (Complete A<b> or</b> B)
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <b class="txt3">A.</b> By fax on (date)
                    <ptb:PDFTextBox ID="Txt_C4_A_Fax_Date" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="topmostSubform[0].Page2[0].tDateFaxOn"
                        MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateFaxOn" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="Txt_C4_A_Fax_Date"
                                                    PopupButtonID="imgbtnDateFaxOn" Enabled="True" />
                                                to (person contacted)
                    <ptb:PDFTextBox ID="Txt_C4_A_Contact_Person" runat="server" Width="250px" AssociatedPDFControlName="tFaxContactPerson"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <b class="txt3">B.</b> By telephone on (date)
                   <ptb:PDFTextBox ID="Txt_C4_B_TelePhn_Date" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="tDateTelephoneOn"
                       MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateTelephoneOn" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="Txt_C4_B_TelePhn_Date"
                                                    PopupButtonID="imgbtnDateTelephoneOn" Enabled="True" />
                                                to (person contacted)
                    <ptb:PDFTextBox ID="Txt_C4_B_Contact_Person" runat="server" Width="250px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tTelephoneContactPerson"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <b class="txt3">and</b> e-mailed/faxed/mailed on (date)
                          <ptb:PDFTextBox ID="Txt_C4_Email_Date" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="tDateEmailOn"
                              MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateMailOn" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender18" runat="server" TargetControlID="Txt_C4_Email_Date"
                                                    PopupButtonID="imgbtnDateMailOn" Enabled="True" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                A copy of this form was sent to the Board on the date below.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div>
                                                <b class="txt3">Provider's Signature:</b>
                                                <%--<ptb:PDFTextBox ID="TextBox101" runat="server" Width="250px" 
                             AssociatedPDFControlName="TextBox101"></ptb:PDFTextBox>  --%>
                                            </div>
                                        </td>
                                        <td align="center">
                                            <div>
                                                <b class="txt3">Date:</b>
                                                <ptb:PDFTextBox ID="Txt_C4_C_Date" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="topmostSubform[0].Page2[0].tDateStatementOfMedicalNecessity"
                                                    MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateC4" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender19" runat="server" TargetControlID="Txt_C4_C_Date"
                                                    PopupButtonID="imgbtnDateC4" Enabled="True" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="5" class="td-CaseDetails-lf-desc-ch"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" height="28" align="left" valign="middle"
                                            class="td-CaseDetails-lf-desc-ch" style="width: 100%;">&nbsp;<b class="txt3">D. SELF-INSURED EMPLOYER / CARRIER RESPONSE TO AUTHORIZATION REQUEST</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>Response Time and Notification Required:</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                The self-insured employer/carrier must respond to the authorization request orally
                            and in writing via e-mail, fax or regular mail with confirmation of delivery within
                            30 days. The 30 day time period for response begins to run from the completion date
                            of this form if e-mailed or faxed, or the completion date plus five days if sent
                            via regular mail. The written response shall be on a copy of this form completed
                            by the physician seeking authorization and shall clearly state whether the authorization
                            has been granted, granted without prejudice, or denied. Authorization can only be
                            granted without prejudice when the compensation case is controverted or the body
                            part has not yet been established. Authorization without prejudice shall not be
                            construed as an admission that the condition for which these services are required
                            is compensable or the employer/carrier is liable. The employer/carrier shall not
                            be responsible for the payment of such services until the question of compensability
                            and liability is resolved. Written response must be sent to the health care provider,
                            claimant, claimant's legal counsel, if any, the Workers' Compensation Board and
                            any other parties of interest.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 10px">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>Denial of the Request for Authorization of a Special Service:</b> A denial of authorization
                            of a special service <b>must</b> be based upon and accompanied by a <b>conflicting second
                            opinion</b> rendered by a physician authorized to conduct IMEs, or record review, or
                            qualified medical professional, or a physician authorized to treat workers' compensation
                            claimants. (If authorization is denied in a controverted case, the conflicting second
                            opinion must address medical necessity only.) When denying authorization for a special
                            service, the employer/carrier must also file with the Board within 5 days of such
                            denial Board Form C-8.1 Part A (Notice of Treatment Issue(s)/Disputed Bill Issue(s)).
                            Failure to file timely the conflicting second opinion and Board Form C-8.1 Part
                            A will render the denial defective. If denial of an authorization is based upon
                            claimant's failure to attend an IME examination scheduled within the 30 day authorization
                            period, contemporaneous supporting evidence of claimant's failure must be attached.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 10px">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>Failure to Timely Respond to C-4 AUTH:</b> The special service(s) for which authorization
                            has been requested will be <b>deemed authorized</b> by Order of the Chair if the
                            self-insured employer/carrier fails to respond within the time specified above.
                            An Order of the Chair is not subject to an appeal under Section 23 of the Workers'
                            Compensation Law.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 10px">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>REASON FOR DENIAL(S), IF ANY. (ATTACH OR REFERENCE CONFLICTING SECOND MEDICAL OPINION AS EXPLAINED ABOVE.)</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <ptb:PDFTextBox ID="Txt_C4_Reason_For_Denial_1" runat="server" Width="1000px" AssociatedPDFControlName="tReasonForDenial1"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Reason_For_Denial_2" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tReasonForDenial2"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Reason_For_Denial_3" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tReasonForDenial3"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Reason_For_Denial_4" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tReasonForDenial4"></ptb:PDFTextBox>
                                            <ptb:PDFTextBox ID="Txt_C4_Reason_For_Denial_5" runat="server" Width="1000px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tReasonForDenial5"></ptb:PDFTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <b class="txt3">Date of service of supporting medical in WCB case file: </b>
                                                <%--<ptb:PDFTextBox ID="Txt_C4_D_WCB_Case_File" runat="server" Width="250px"  AssociatedPDFControlName="tDOS_OfSupportingMedical_2"></ptb:PDFTextBox>  --%>
                                                <ptb:PDFTextBox ID="Txt_C4_D_WCB_Case_File" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="topmostSubform[0].Page2[0].tDOS_OfSupportingMedical_2"
                                                    MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDOS_WCB1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender20" runat="server" TargetControlID="Txt_C4_D_WCB_Case_File"
                                                    PopupButtonID="imgbtnDOS_WCB1" Enabled="True" />
                                            </div>
                                        </td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>I</b> certify that the self-insured employer/carrier <b>telephoned</b> the office
                            of the health care provider listed above within the response time-frame indicated
                            above and advised that the self-insured employer/carrier had either granted or denied
                            approval for the special services for which authorization was sought, as indicated
                            above, on the date below:
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>and</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div>
                                                <b>I</b> certify that copies of this form were e-mailed, faxed, or mailed to the
                            health care provider, the claimant, the claimant's legal counsel, if any, the Workers'
                            Compensation Board and all parties of interest on the date below:
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 10px">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <b class="txt3">By: (print name) </b>
                                                <ptb:PDFTextBox ID="Txt_C4_Print_By" runat="server" Width="250px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tPrintBy"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center" colspan="2">
                                            <div>
                                                <b class="txt3">Title:</b>
                                                <ptb:PDFTextBox ID="Txt_C4_Title" runat="server" Width="250px" AssociatedPDFControlName="topmostSubform[0].Page2[0].tTitle"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div>
                                                <b class="txt3">Signature: </b>
                                                <ptb:PDFTextBox ID="Txt_C4_Flag" runat="server" Width="250px" Visible="false"
                                                    AssociatedPDFControlName="TextBox107"></ptb:PDFTextBox>
                                            </div>
                                        </td>
                                        <td align="center">
                                            <div>
                                                <b class="txt3">Date:</b>
                                                <ptb:PDFTextBox ID="Txt_C4_D_Date" runat="server" onkeypress="return clickButton1(event,'/')" AssociatedPDFControlName="topmostSubform[0].Page2[0].tDate_SelfInsuredEmployer"
                                                    MaxLength="10"></ptb:PDFTextBox>
                                                <asp:ImageButton ID="imgbtnDateof" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender21" runat="server" TargetControlID="Txt_C4_D_Date"
                                                    PopupButtonID="imgbtnDateof" Enabled="True" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="height: 10px">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <div>
                                                <asp:Button ID="BtnSubmit" runat="server" Text="Save" Width="80px"
                                                    OnClick="BtnSubmit_Click" />
                                                <asp:Button ID="BtnPrint" runat="server" Text="Print" Width="80px"
                                                    OnClick="BtnPrint_Click" />
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="TabPanel1" TabIndex="2" Height="800px" Visible="false" >
                    <HeaderTemplate>
                        <div style="width: 40px;" class="lbl">
                            MG1
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>

                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="width: 414px" align="center">
                                        <UserMessage:MessageControl ID="MessageControl2" runat="server"></UserMessage:MessageControl>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btnSaveTopMG1" Text="Save" runat="server" OnClick="btnSaveTopMG1_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnPrintTopMG1" Text="Print" runat="server" OnClick="btnPrintTopMG1_Click" />
                        </div>
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 22%">WCB Case Number :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1WCBCaseNumber" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; font-size: small; width: 22%">Carrier Case Number :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1CarrierCaseNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; font-size: small; width: 22%">Date of Injury :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1DateOfInjury" runat="server"></asp:TextBox>
                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender22" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateOfInjury">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 10%">
                                        <h4>A. Patient's Name:
                                        </h4>
                                    </td>
                                    <td style="width: 703px">
                                        <asp:TextBox ID="txtMG1PatientName" Width="450px" runat="server"></asp:TextBox>
                                        Social Security No.:
                                                    <asp:TextBox ID="txtMG1socialSecurityNo" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 300px">Patient's Address:
                                    </td>
                                    <td style="width: 703px">
                                        <asp:TextBox ID="txtMG1PatientAddress" Width="800px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 30%">Employer's Name & Address:
                                    </td>
                                    <td style="width: 703px">
                                        <asp:TextBox ID="txtMG1EmpNameAndAddress" Width="800px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 35%">Insurance Carrier's Name & Address:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1InsuranceCarrier" runat="server" Width="763px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 800px; margin: 0 auto;">
                            <h3>Note: This form is used only if the employer/carrier participates in the Optional
                                            Prior Approval program. You can obtain participation status from the WCB Website.</h3>
                        </div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small">
                                        <h3>B. Attending Doctor's Name & Address:</h3>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAttendingDoctor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAttendingDoctor_SelectedIndexChanged"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 325px">Individual Provider's WCB Authorization No.:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1WCBAuthOne" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthTwo" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthThree" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthFour" Width="29px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthFive" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthSix" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthSeven" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1WCBAuthEight" Width="25px" runat="server"></asp:TextBox>
                                        <strong>Telephone No:</strong>
                                        <asp:TextBox ID="txtMG1Telephone" runat="server"></asp:TextBox>
                                        <strong>Fax No.:</strong>
                                        <asp:TextBox ID="txtMG1FaxNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 260px">
                                        <h3>C. DATE REQUEST SUBMITTED:</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1DateSubmitted" Width="300px" runat="server"></asp:TextBox>
                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender23" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateSubmitted">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <strong><i>The undersigned requests optional prior approval under the WCB Medical Treatment
                                                Guidelines as indicateUserd below:</i></strong>
                            </div>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 30%">Treatment/Procedure Requested:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1Procedure" Width="1000px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small">Guideline Reference:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMG1GuidelineReference" runat="server"
                                            onchange="javascript:SplitSMG1(this);" Width="78px">
                                            <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                            <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                            <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                            <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                            <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                            <%--OnSelectedIndexChanged="ddlGuidline_SelectedIndexChanged--%>
                                            <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--    <asp:TextBox ID="txtMG1Guideline" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                         ReadOnly="false"></asp:TextBox>
                                         ---%>
                                        <%--                     <asp:TextBox ID="TextBox12" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                         ReadOnly="false"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtMG1Guideline" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                            ReadOnly="false"></asp:TextBox>
                                        -
                                                    <asp:TextBox ID="txtMG1GuidelineOne" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1GuidelineTwo" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1GuidelineThree" Width="25px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtMG1GuidelineFour" Width="25px" runat="server"></asp:TextBox>
                                        (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                    and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,
                                                    indicate corresponding section of WCB Medical Treatment Guidelines.)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small; width: 600px">Date of Service of Supporting Medical in WCB Case File:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1DateOfService" Width="300px" runat="server"></asp:TextBox>
                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender24" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateOfService">
                                        </ajaxcontrol:CalendarExtender>
                                        <strong>(Attach if not already submitted.)</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; font-size: small">Comments:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMG1Comments" Width="1000px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">
                                I certify that I am making the above request for optional prior approval and my
                                            affirmative statements are true and correct. I
                                           <%-- <asp:CheckBox ID="chkMG1Approval" runat="server" />--%>  <prb:PDFRadioButton
                                                                ID="chkMG1Approval" GroupName="MG1did"
                                                                runat="server" />did /<%--<asp:CheckBox ID="ChkMG1DidNotContact"
                                                runat="server" />--%><prb:PDFRadioButton
                                                                ID="ChkMG1DidNotContact" GroupName="MG1did"
                                                                runat="server" />
                                did not contact the carrier by telephone to discuss this request before making it.
                                            I contacted the carrier by telephone on (date)<asp:TextBox ID="txtMG1TelephoneTwo"
                                                runat="server" />
                                <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                    Enabled="True" TargetControlID="txtMG1TelephoneTwo">
                                </ajaxcontrol:CalendarExtender>
                                and spoke to (person spoken to or was not able to speak to anyone)<asp:TextBox ID="txtMG1SpokenTo"
                                    Width="350px" runat="server" />
                            </div>
                            <div class="td-PatientInfo-lf-desc-ch" style="width: 1150px; font-size: small">
                                <asp:CheckBox ID="chkMG1CopyOfForm" runat="server" />
                                A copy of this form was sent to the carrier/employer/self-insured employer/Special
                                            Fund by (fax number or e-mail address required)
                                            <asp:TextBox ID="txtMG1CopyOfForm" Width="165px" runat="server" />
                                A copy was sent to the Workers' Compensation Board (see the Board's email address
                                            and fax number on the reverse), and copies were provided to the claimant’s legal
                                            counsel, if any, and to any other parties of interest on the date below.
                            </div>
                            <%--<div class="td-PatientInfo-lf-desc-ch" style="width: 1150px; font-size: small">
                                Provider's Signature:__________________________________________________________
                                Date:
                                <asp:TextBox ID="txtMG1DateC" Width="300px" runat="server" />
                                 <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender1" runat="server"  Format="MM/dd/yyyy"
                                           Enabled="True" TargetControlID="txtMG1DateC">
                       </ajaxcontrol:CalendarExtender>

                            </div>--%>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1475px; font-size: small">Provider's Signature:______________________________________________________________
                                    </td>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1475px; font-size: small">Date:
                                                    <asp:TextBox ID="txtMG1DateC" Width="300px" runat="server" />
                                        <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateC">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">
                                        <h3>D. <u>CARRIER'S / EMPLOYER'S RESPONSE</u></h3>
                                        (Response is due within 8 business days of receipt of this request or medical care
                                                    is deemed approved (12 NYCRR 324.4(c)). The provider's request is:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <prb:PDFRadioButton ID="chkMG1Granted" GroupName="MG1EmpRes" runat="server" />
                                        <%--<asp:CheckBox ID="chkMG1Granted" runat="server" />--%>
                                        <b>Granted</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <prb:PDFRadioButton ID="chkMG1GrantedPrejudice" GroupName="MG1EmpRes" runat="server" />
                                       <%-- <asp:CheckBox ID="chkMG1GrantedPrejudice" runat="server" />--%>
                                        <b>Granted without Prejudice</b> (see item 7 on reverse)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                             <prb:PDFRadioButton ID="chkMG1DeniedDenial" GroupName="MG1EmpRes" runat="server" />
                                           <%-- <asp:CheckBox ID="chkMG1DeniedDenial" runat="server" />--%>
                                            <b>Denied</b> IF DENIED, STATE THE BASIS FOR THE DENIAL IN THE SPACE PROVIDED BELOW.
                                                        <u>SEE IMPORTANT INFORMATION FOR CARRIER ON REVERSE</u>.<br />
                                            <asp:TextBox ID="txtCarrierReverse" runat="server" Width="400px"></asp:TextBox>
                                            <br />
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name of the Medical Professional who
                                                    Reviewed the Denial:
                                                    <asp:TextBox ID="txtMG1MedicalProfessional" Width="400px" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <div class="td-PatientInfo-lf-desc-ch" style="width: 1180px; font-size: small">
                                certify that copies of this form were sent to the Treating Medical Provider requesting
                                            optional prior approval, the Workers' Compensation Board (see email address and
                                            fax number on the reverse), the claimant's legal counsel, if any, and any other
                                            parties of interest, on the date below.
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">By: (print name)
                                                        <asp:TextBox ID="txtMG1PrintNameOne" Width="500px" runat="server" />
                                        </td>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Title
                                                        <asp:TextBox ID="txtMG1Title" Width="300px" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%-- <div class="td-PatientInfo-lf-desc-ch" style="width: 1150px; font-size: small">
                                 Signature:__________________________________________________________ Date:
                                <asp:TextBox ID="txtMG1DateD" Width="300px" runat="server" />
                                <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender2" runat="server"  Format="MM/dd/yyyy"
                                           Enabled="True" TargetControlID="txtMG1DateD">
                       </ajaxcontrol:CalendarExtender>

                            </div>--%>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Signature:______________________________________________________________
                                    </td>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Date:
                                                    <asp:TextBox ID="txtMG1DateD" Width="300px" runat="server" />
                                        <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender2" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateD">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">
                                        <h3>E. MEDICAL PROVIDER'S REQUEST FOR REVIEW BY MEDICAL ARBITRATOR OF DENIAL</h3>
                                    </td>
                                </tr>
                            </table>
                            <div class="td-PatientInfo-lf-desc-ch" style="width: 1180px; font-size: small">
                                <asp:CheckBox ID="chkMG1MedicalArbitrator" runat="server" />
                                I hereby request review by a medical arbitrator designated by the Chair of the carrier's
                                            decision to deny optional prior approval of the above request. I understand that
                                            resolution by the medical arbitrator is binding and is not appealable under Workers'
                                            Compensation Law §23. (Request is due within 14 calendar days of the date of denial.)
                                            Supporting medical report(s) dated
                                            <asp:TextBox ID="txtMG1MedicalReport" Width="300px" runat="server" />
                                <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender5" runat="server" Format="MM/dd/yyyy"
                                    Enabled="True" TargetControlID="txtMG1MedicalReport">
                                </ajaxcontrol:CalendarExtender>
                                is/are attached or is/are available in the WCB case file.
                            </div>
                            <%--<div class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">
                                Signature:_____________________________________________________________ Date:
                                <asp:TextBox ID="txtMG1DateE" Width="300px" runat="server" />--%>
                            <%--<ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender3" runat="server"  Format="MM/dd/yyyy"
                                           Enabled="True" TargetControlID="txtMG1DateE">
                       </ajaxcontrol:CalendarExtender>--%>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Provider's Signature:______________________________________________________________
                                    </td>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Date:
                                                    <asp:TextBox ID="txtMG1DateE" Width="300px" runat="server" />
                                        <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender3" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txtMG1DateE">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">
                                        <h3>F. <u>CARRIER / EMPLOYER IS APPROVING THIS REQUEST FOR OPTIONAL PRIOR APPROVAL AFTER
                                                            AN INITIAL DENIAL</u></h3>
                                    </td>
                                </tr>
                            </table>
                            <div class="td-PatientInfo-lf-desc-ch" style="width: 1180px; font-size: small">
                                <asp:CheckBox ID="chkMG1Certify" runat="server" />
                                I certify that the provider's request for optional prior approval given above, which
                                            was initially denied on
                                            <asp:TextBox ID="txtMG1Denied" Width="300px" runat="server" />
                                <ajaxcontrol:CalendarExtender ID="CalendarExtender25" runat="server" Format="MM/dd/yyyy"
                                    Enabled="True" TargetControlID="txtMG1Denied">
                                </ajaxcontrol:CalendarExtender>
                                , is now granted.
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">By: (print name)
                                                        <asp:TextBox ID="txtMG1PrintNameTwo" Width="500px" runat="server" />
                                        </td>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Title
                                                        <asp:TextBox ID="txtMG1TitleTwo" Width="300px" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Signature:______________________________________________________________
                                        </td>
                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 1175px; font-size: small">Date:
                                                        <asp:TextBox ID="txtMG1DateF" Width="300px" runat="server" />
                                            <ajaxcontrol:CalendarExtender ID="txt_from_CalendarExtender4" runat="server" Format="MM/dd/yyyy"
                                                Enabled="True" TargetControlID="txtMG1DateF">
                                            </ajaxcontrol:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btnSaveMG1" Text="Save" runat="server" OnClick="btnSaveMG1_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnPrintMG1" Text="Print" runat="server" OnClick="btnPrintMG1_Click" />
                        </div>

                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="TabPanel2" TabIndex="3" Height="800px" Visible="false">
                    <HeaderTemplate>
                        <div style="width: 40px;" class="lbl">
                            MG1.1
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="width: 414px" align="center">
                                        <UserMessage:MessageControl ID="UserMessageMG11" runat="server"></UserMessage:MessageControl>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                            <tr>
                                <td style="width: 422px" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnMG11_save_top" runat="server" Text="Save" Width="80px" OnClick="btnMG11_save_top_Click1" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPrint_MG11_top" runat="server"
                                        Text="Print" Width="80px" OnClick="btnPrint_MG11_top_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                               <%-- <asp:Button ID="btnClear_MG11_top" runat="server" Text="Clear" Width="80px" />--%>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="margin-bottom: 20px; width: 100%; font-family: Verdana">
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;" colspan="2">WCB Case Number :&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <ptb:PDFTextBox ID="txtWCBNumber" AssociatedPDFControlName="[txtMG2_WCBCaseNo]" runat="server"></ptb:PDFTextBox>
                                        &nbsp;&nbsp; Carrier Case Number :&nbsp;&nbsp;<ptb:PDFTextBox ID="txtCasenumber_MG11"
                                            AssociatedPDFControlName="[txtMG2_CarrierCaseNo]" runat="server"></ptb:PDFTextBox>
                                        &nbsp;&nbsp; Date of Injury :&nbsp;&nbsp;<ptb:PDFTextBox ID="txtInjuryDate_MG11"
                                            AssociatedPDFControlName="[txtInjuryDate]" runat="server"></ptb:PDFTextBox><ajaxcontrol:CalendarExtender
                                                ID="CalendarExtender26" runat="server" Format="MM/dd/yyyy" Enabled="True" TargetControlID="txtInjuryDate_MG11">
                                            </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;" colspan="2">Patient's First Name :&nbsp;&nbsp;
                                                    <ptb:PDFTextBox ID="txtPatientName_MG11" AssociatedPDFControlName="[txtMG2_PatientFirstName]"
                                                        runat="server"></ptb:PDFTextBox>
                                        &nbsp;&nbsp;Middle Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox
                                            ID="txtMiddleName_MG11" AssociatedPDFControlName="[txtMG2_PatientMiddleName]"
                                            runat="server"></ptb:PDFTextBox>
                                        &nbsp;&nbsp; Last Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <ptb:PDFTextBox ID="txtLastName_MG11" AssociatedPDFControlName="[txtMG2_PatientLastName]"
                                                        runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;" colspan="2">Social Security No. :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtSocialNumber_MG11"
                                        AssociatedPDFControlName="[txtMG2_SocialSecurityNo]" runat="server"></ptb:PDFTextBox>&nbsp;&nbsp;
                                                    Patient's Address :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox
                                                        ID="txtPatient_Address_MG11" AssociatedPDFControlName="[txtMG2_PatientAddress]"
                                                        runat="server"></ptb:PDFTextBox>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;" colspan="2">Doctor's Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                        ID="ddlDoctor_Name_MG11" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDoctor_Name_MG11_SelectedIndexChanged">
                                    </asp:DropDownList>
                                        &nbsp;&nbsp;Doctor's WCB Authorization Number :
                                                    <ptb:PDFTextBox ID="txtDoctor_WCB" AssociatedPDFControlName="[txtMg2_DoctorWCBAuthNo]"
                                                        runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: left">INSTRUCTIONS TO ATTENDING DOCTOR: This form must be filed attached to a completed
                                                    Form MG-1 if requesting optional prior approval for additional treatment(s) or procedure(s)
                                                    in the same case.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;" colspan="2">A. The undersigned requests additional approval(s) to VARY from the WCB Medical
                                                    Treatment Guidelines as indicated below:
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%; border: solid 1px; height: 180px">
                                            <tr style="width: 100%">
                                                <td style="width: 80%; border-right: solid 1px">
                                                    <table>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Treatment / Procedure requested : </b>&nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Treatment_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server" Width="680px"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; font-weight: bold; border-bottom: 1px solid #e1e1e1;">Guideline Reference :
                                                                            <asp:DropDownList ID="ddlMG11GudlineReferance1" runat="server" onchange="javascript:SplitSMG11_One(this);">
                                                                                <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                                                                <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                                                                <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                                                <asp:ListItem Value="S-D10E">S-D10E</asp:ListItem>
                                                                                <asp:ListItem Value="N-D11D">N-D11D</asp:ListItem>
                                                                                <asp:ListItem Value="B-D101">B-D101</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                <ptb:PDFTextBox ID="txtGuideline_One" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                -
                                                                            <ptb:PDFTextBox ID="txtGuideline_BoxOne" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                                runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txtGuideline_BoxTwo" AssociatedPDFControlName="[txtMG2_GudRef2]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txtGuideline_BoxThree" AssociatedPDFControlName="[txtMG2_GudRef3]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txtGuideline_BoxFour" AssociatedPDFControlName="[txtMG2_GudRef4]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                                            and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,indicate
                                                                            corresponding section of WCB Medical Treatment Guidelines. If the treatment requested
                                                                            is not addressed by the Guidelines,in the remaining boxes use NONE.)
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Date of Service of Supporting Medical in WCB Case File (Attach if not already submitted.):</b>
                                                                &nbsp;
                                                                            <ptb:PDFTextBox ID="txt_DateOfService" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server"></ptb:PDFTextBox>
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender27" runat="server" Format="MM/dd/yyyy"
                                                                    Enabled="True" TargetControlID="txt_DateOfService">
                                                                </ajaxcontrol:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; vertical-align: top;">
                                                                <b>Comments : </b>
                                                                <br></br>
                                                                <ptb:PDFTextBox ID="txtComments_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="98%" TextMode="MultiLine"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <table style="width: 100%">
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: center;">
                                                                <b>CARRIER'S/EMPLOYER'S
                                                                                <br></br>
                                                                    RESPONSE</b><br></br>
                                                                (Carrier/employer must complete certification on reverse of this form)
                                                            </td>
                                                        </tr>                                                       
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; text-align: left;">
                                                                 <prb:PDFRadioButton
                                                                ID="cbGranted_One" GroupName="MG11A" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                              <%--  <pcb:PDFCheckBox ID="cbGranted_One" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Granted</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbGrantedPrejudice_One" GroupName="MG11A" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                               <%-- <pcb:PDFCheckBox ID="cbGrantedPrejudice_One" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />--%>
                                                                <b>Granted without Prejudice</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbDenied_One" GroupName="MG11A" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                               <%-- <pcb:PDFCheckBox ID="cbDenied_One" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Denied</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%; border: solid 1px; height: 180px">
                                            <tr style="width: 100%">
                                                <td style="width: 80%; border-right: solid 1px">
                                                    <table>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Treatment / Procedure requested : </b>&nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Treatement_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server" Width="680px"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; font-weight: bold; border-bottom: 1px solid #e1e1e1;">Guideline Reference :
                                                                            <asp:DropDownList ID="ddlMG11GudlineReferance2" runat="server" onchange="javascript:SplitSMG11_Two(this);">
                                                                                <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="K-E7E ">K-E7E </asp:ListItem>
                                                                                <asp:ListItem Value="B-D9A ">B-D9A </asp:ListItem>
                                                                                <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                                                <asp:ListItem Value="S-D10E">S-D10E</asp:ListItem>
                                                                                <asp:ListItem Value="N-D11D">N-D11D</asp:ListItem>
                                                                                <asp:ListItem Value="B-D101">B-D101</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                <ptb:PDFTextBox ID="txt_Guideline_two" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                -
                                                                            <ptb:PDFTextBox ID="txt_Guideline_Box_Five" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                                runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txtGuideline_Box_Six" AssociatedPDFControlName="[txtMG2_GudRef2]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_Seven" AssociatedPDFControlName="[txtMG2_GudRef3]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_Eight" AssociatedPDFControlName="[txtMG2_GudRef4]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                                            and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,indicate
                                                                            corresponding section of WCB Medical Treatment Guidelines. If the treatment requested
                                                                            is not addressed by the Guidelines,in the remaining boxes use NONE.)
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Date of Service of Supporting Medical in WCB Case File (Attach if not already submitted.):</b>
                                                                &nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Date_of_Service_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server"></ptb:PDFTextBox>
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender28" runat="server" Format="MM/dd/yyyy"
                                                                    Enabled="True" TargetControlID="txt_Date_of_Service_two">
                                                                </ajaxcontrol:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; vertical-align: top;">
                                                                <b>Comments : </b>
                                                                <br></br>
                                                                <ptb:PDFTextBox ID="txt_Comments_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="98%" TextMode="MultiLine"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <table style="width: 100%">
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: center;">
                                                                <b>CARRIER'S/EMPLOYER'S
                                                                                <br></br>
                                                                    RESPONSE</b><br></br>
                                                                (Carrier/employer must complete certification on reverse of this form)
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; text-align: left;">
                                                                   <prb:PDFRadioButton
                                                                ID="cbGranted_two" GroupName="MG11B" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                              <%--  <pcb:PDFCheckBox ID="cbGranted_two" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Granted</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbGrantedPrejudice_two" GroupName="MG11B" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                             <%--   <pcb:PDFCheckBox ID="cbGrantedPrejudice_two" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />--%>
                                                                <b>Granted without Prejudice</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbDenied_two" GroupName="MG11B" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                               <%-- <pcb:PDFCheckBox ID="cbDenied_two" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Denied</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%; border: solid 1px; height: 180px">
                                            <tr style="width: 100%">
                                                <td style="width: 80%; border-right: solid 1px">
                                                    <table>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Treatment / Procedure requested : </b>&nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Treatment_three" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server" Width="680px"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; font-weight: bold; border-bottom: 1px solid #e1e1e1;">Guideline Reference :
                                                                            <asp:DropDownList ID="ddlMG11GudlineReferance3" runat="server" onchange="javascript:SplitSMG11_Three(this);">
                                                                                <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="K-E7E ">K-E7E </asp:ListItem>
                                                                                <asp:ListItem Value="B-D9A ">B-D9A </asp:ListItem>
                                                                                <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                                                <asp:ListItem Value="S-D10E">S-D10E</asp:ListItem>
                                                                                <asp:ListItem Value="N-D11D">N-D11D</asp:ListItem>
                                                                                <asp:ListItem Value="B-D101">B-D101</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                <ptb:PDFTextBox ID="txt_Guideline_three" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                -
                                                                            <ptb:PDFTextBox ID="txt_Guideline_BoxNine" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                                runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_ten" AssociatedPDFControlName="[txtMG2_GudRef2]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txtGuideline_Box_eleven" AssociatedPDFControlName="[txtMG2_GudRef3]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_twelve" AssociatedPDFControlName="[txtMG2_GudRef4]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                                            and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,indicate
                                                                            corresponding section of WCB Medical Treatment Guidelines. If the treatment requested
                                                                            is not addressed by the Guidelines,in the remaining boxes use NONE.)
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Date of Service of Supporting Medical in WCB Case File (Attach if not already submitted.):</b>
                                                                &nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Date_Of_Service_three" AssociatedPDFControlName="[txt_Date_Of_Service_three]"
                                                                                runat="server"></ptb:PDFTextBox>
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender29" runat="server" Format="MM/dd/yyyy"
                                                                    Enabled="True" TargetControlID="txt_Date_Of_Service_three">
                                                                </ajaxcontrol:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; vertical-align: top;">
                                                                <b>Comments : </b>
                                                                <br></br>
                                                                <ptb:PDFTextBox ID="txt_Comments_three" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="98%" TextMode="MultiLine"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <table style="width: 100%">
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: center;">
                                                                <b>CARRIER'S/EMPLOYER'S
                                                                                <br></br>
                                                                    RESPONSE</b><br></br>
                                                                (Carrier/employer must complete certification on reverse of this form)
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; text-align: left;">
                                                                   <prb:PDFRadioButton
                                                                ID="cbGranted_three" GroupName="MG11C" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                               <%-- <pcb:PDFCheckBox ID="cbGranted_three" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />--%>
                                                                <b>Granted</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                   <prb:PDFRadioButton
                                                                ID="cbGrantedPrejudice_three" GroupName="MG11C" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                             <%--   <pcb:PDFCheckBox ID="cbGrantedPrejudice_three" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />--%>
                                                                <b>Granted without Prejudice</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbDenied_three" GroupName="MG11C" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                                <%--<pcb:PDFCheckBox ID="cbDenied_three" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Denied</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%; border: solid 1px; height: 180px">
                                            <tr style="width: 100%">
                                                <td style="width: 80%; border-right: solid 1px">
                                                    <table>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Treatment / Procedure requested : </b>&nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Treatment_four" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server" Width="680px"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; font-weight: bold; border-bottom: 1px solid #e1e1e1;">Guideline Reference :
                                                                            <asp:DropDownList ID="ddlMG11GudlineReferance4" runat="server" onchange="javascript:SplitSMG11_Four(this);">
                                                                                <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="K-E7E ">K-E7E </asp:ListItem>
                                                                                <asp:ListItem Value="B-D9A ">B-D9A </asp:ListItem>
                                                                                <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                                                <asp:ListItem Value="S-D10E">S-D10E</asp:ListItem>
                                                                                <asp:ListItem Value="N-D11D">N-D11D</asp:ListItem>
                                                                                <asp:ListItem Value="B-D101">B-D101</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                <ptb:PDFTextBox ID="txt_Guideline_four" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_thirteen" AssociatedPDFControlName="[txtMG2_GudRef2]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_fourteen" AssociatedPDFControlName="[txtMG2_GudRef3]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Box_fifteen" AssociatedPDFControlName="[txtMG2_GudRef4]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                <ptb:PDFTextBox ID="txt_Guideline_Sixteen" AssociatedPDFControlName="[txtMG2_GudRef1]"
                                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                                                (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                                            and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,indicate
                                                                            corresponding section of WCB Medical Treatment Guidelines. If the treatment requested
                                                                            is not addressed by the Guidelines,in the remaining boxes use NONE.)
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; border-bottom: 1px solid #e1e1e1; vertical-align: top;">
                                                                <b>Date of Service of Supporting Medical in WCB Case File (Attach if not already submitted.):</b>
                                                                &nbsp;
                                                                            <ptb:PDFTextBox ID="txt_Date_Of_Service_four" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                                runat="server"></ptb:PDFTextBox>
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender30" runat="server" Format="MM/dd/yyyy"
                                                                    Enabled="True" TargetControlID="txt_Date_Of_Service_four">
                                                                </ajaxcontrol:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px; border-top: solid 1px #e1e1e1;">
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Verdana; font-size: 0.8em; vertical-align: top;">
                                                                <b>Comments : </b>
                                                                <br></br>
                                                                <ptb:PDFTextBox ID="txt_Comments_four" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="98%" TextMode="MultiLine"></ptb:PDFTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <table style="width: 100%">
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: center;">
                                                                <b>CARRIER'S/EMPLOYER'S
                                                                                <br></br>
                                                                    RESPONSE</b><br></br>
                                                                (Carrier/employer must complete certification on reverse of this form)
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbGranted_four" GroupName="MG11D" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                              <%--  <pcb:PDFCheckBox ID="cbGranted_four" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Granted</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbGrantedPrejudice_four" GroupName="MG11D" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                              <%--  <pcb:PDFCheckBox ID="cbGrantedPrejudice_four" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />--%>
                                                                <b>Granted without Prejudice</b>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td style="width: 100%; text-align: left;">
                                                                  <prb:PDFRadioButton
                                                                ID="cbDenied_four" GroupName="MG11D" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                               <%-- <pcb:PDFCheckBox ID="cbDenied_four" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                                                <b>Denied</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">I certify that I am making the above request for optional prior approval and my
                                                    affirmative statements are true and correct. I
                                                           <prb:PDFRadioButton
                                                                ID="cb_Contact_One" GroupName="MG11E" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                   <%-- <pcb:PDFCheckBox ID="cb_Contact_One" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                        did/
                                                     <prb:PDFRadioButton
                                                                ID="cb_Contact_two" GroupName="MG11E" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                runat="server" />
                                                  <%--  <pcb:PDFCheckBox ID="cb_Contact_two" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                        did not contact the carrier by telephone to discuss this request before making it.
                                                    I contacted the carrier by telephone on (date)
                                                    <ptb:PDFTextBox ID="txt_Carrier_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server"></ptb:PDFTextBox><ajaxcontrol:CalendarExtender ID="CalendarExtender35"
                                                            runat="server" Format="MM/dd/yyyy" Enabled="True" TargetControlID="txt_Carrier_One">
                                                        </ajaxcontrol:CalendarExtender>
                                        and spoke to (person spoke to or was not able to speak to anyone)
                                                    <ptb:PDFTextBox ID="txt_Carrier_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                        <pcb:PDFCheckBox ID="cb_Carrier_One" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />
                                        A copy of this form was sent to the carrier/employer/self-insured employer/Special
                                                    Fund by (fax number or e-mail address required)
                                                    <ptb:PDFTextBox ID="txt_Carrier_three" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server"></ptb:PDFTextBox>
                                        A copy was sent to the Workers' Compensation Board, and copies were provided to
                                                    the claimant's legal counsel, if any, and to any other parties of interest on the
                                                    date below.
                                    </td>
                                </tr>
                                <tr style="height: 6px;">
                                    <td></td>
                                </tr>
                                <tr style="width: 100;">
                                    <td style="width: 100; font-weight: bold; font-size: 0.8em; font-family: Verdana">Provider's Signature ___________________________________________________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date
                                                    :
                                                    <ptb:PDFTextBox ID="txt_Date" AssociatedPDFControlName="[txtMG2_WCBCaseNo]" runat="server"></ptb:PDFTextBox>
                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender31" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txt_Date">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                                <tr style="height: 10px;">
                                    <td></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-family: Verdana">
                                        <table>
                                            <tr>
                                                <td style="width: 2%; font-weight: bolder; font-size: 0.8em; font-family: Verdana">B .
                                                </td>
                                                <td style="width: 98%; font-size: 0.8em; font-family: Verdana">
                                                    <b><u>CARRIER'S / EMPLOYER'S RESPONSE</u></b> (Response is due within 8 business
                                                                days of receipt of this request or medical care is deemed approved (12 NYCRR 324
                                                                (c)). IF ANY REQUESTS ARE DENIED, GIVE REASON(S) IN THE SPACE PROVIDED BELOW. Identify
                                                                reasons according to Request No. 2-5 on the front of this form.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 98%;">
                                                    <ptb:PDFTextBox ID="txt_employer" AssociatedPDFControlName="[txtMG2_WCBCaseNo]" runat="server"
                                                        Height="300px" Width="1048px" TextMode="MultiLine"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 6px;">
                                    <td></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">Name of the medical professional who reviewed the denial(s):
                                                    <ptb:PDFTextBox ID="txt_Medical_Professional" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server" Width="300px"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr style="height: 6px;">
                                    <td></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">I certify that copies of this form were sent to the Treating Medical Provider requesting
                                                    optional prior approval, the Workers' Compensation Board (see mailing and email
                                                    addresses and fax number on Form MG-1), the claimant's legal counsel, if any, and
                                                    any other parties of interest, on the date below.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 3px;"></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">By : (Print Name)
                                                    <ptb:PDFTextBox ID="txt_Print_Name_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server" Width="300px"></ptb:PDFTextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    Title :&nbsp;
                                                    <ptb:PDFTextBox ID="txt_Title_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server" Width="300px"></ptb:PDFTextBox>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%; font-weight: bold; font-size: 0.8em; font-family: Verdana">Signature : ___________________________________________________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    Date :
                                                    <ptb:PDFTextBox ID="txt_Date_employer_One" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server" Width="300px"></ptb:PDFTextBox>
                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender32" runat="server" Format="MM/dd/yyyy"
                                            Enabled="True" TargetControlID="txt_Date_employer_One">
                                        </ajaxcontrol:CalendarExtender>
                                    </td>
                                </tr>
                                <tr style="height: 3px;">
                                    <td></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%; border: 1px solid; height: 200px;">
                                            <tr style="width: 100%">
                                                <td style="width: 2%; font-weight: bold; font-size: 0.8em; font-family: Verdana">C .
                                                </td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                                    <b><u>MEDICAL PROVIDER'S REQUEST FOR BOARD REVIEW OF DENIAL</u></b>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                                    <pcb:PDFCheckBox ID="cbProvider" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />
                                                    I hereby request review by a medical arbitrator designated by the Chair of the carrier's
                                                                decision to deny optional prior approval of the request(s) checked below. I understand
                                                                that resolution by the medical arbitrator is binding and is not appealable under
                                                                Workers' Compensation Law Section 23. (Request is due within 14 calendar days of
                                                                the date of denial.) Supporting medical report(s) dated
                                                                <ptb:PDFTextBox ID="txt_Date_Medical" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server"></ptb:PDFTextBox>
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender36" runat="server" Format="MM/dd/yyyy"
                                                        Enabled="True" TargetControlID="txt_Date_Medical">
                                                    </ajaxcontrol:CalendarExtender>
                                                    is/are attached or is/are available in the WCB case file.
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                                    <pcb:PDFCheckBox ID="cb_Medical_request_two" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                        runat="server" />
                                                    Request No.2
                                                                <pcb:PDFCheckBox ID="cb_Medical_request_three" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.3
                                                                <pcb:PDFCheckBox ID="cb_Medical_request_four" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.4
                                                                <pcb:PDFCheckBox ID="cb_Medical_request_five" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.5
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">Provider's Signature : ________________________________________________________________________________________
                                                                Date :
                                                                <ptb:PDFTextBox ID="txt_Provider_Date" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server"></ptb:PDFTextBox>
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender33" runat="server" Format="MM/dd/yyyy"
                                                        Enabled="True" TargetControlID="txt_Provider_Date">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr style="width: 100%">
                                                <td style="width: 2%; font-weight: bold; font-size: 0.8em; font-family: Verdana">D .
                                                </td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                                    <b><u>CARRIER / EMPLOYER IS APPROVING ADDITIONAL REQUEST(S) FOR OPTIONAL PRIOR APPROVAL
                                                                    AFTER AN INITIAL DENIAL</u></b>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-size: 0.8em; font-family: Verdana">
                                                    <pcb:PDFCheckBox ID="cb_Provider_request" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                        runat="server" />
                                                    I certify that the provider's request for optional prior approval given above, <b>which
                                                                    was initially denied on</b>
                                                    <ptb:PDFTextBox ID="txt_Provider_request" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                        runat="server"></ptb:PDFTextBox>
                                                    ,is now granted for the following request(s):
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">
                                                    <pcb:PDFCheckBox ID="cb_request_two" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />
                                                    Request No.2
                                                                <pcb:PDFCheckBox ID="cb_request_three" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.3
                                                                <pcb:PDFCheckBox ID="cb_request_four" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.4
                                                                <pcb:PDFCheckBox ID="cb_request_five" AssociatedPDFControlName="[chkMg2_Grant4]"
                                                                    runat="server" />
                                                    Request No.5
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">By : ( Print Name )
                                                                <ptb:PDFTextBox ID="txt_Print_Name_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="300px"></ptb:PDFTextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Title : &nbsp;
                                                                <ptb:PDFTextBox ID="txt_Title_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server" Width="300px"></ptb:PDFTextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 2%"></td>
                                                <td style="width: 98%; font-weight: bold; font-size: 0.8em; font-family: Verdana">Signature : ___________________________________________________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Date : &nbsp;
                                                                <ptb:PDFTextBox ID="txt_Date_employer_two" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                                    runat="server"></ptb:PDFTextBox>
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender34" runat="server" Format="MM/dd/yyyy"
                                                        Enabled="True" TargetControlID="txt_Date_employer_two">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                            <tr>
                                <td style="width: 422px" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnMG11_save_bottom" runat="server" Text="Save" Width="80px" OnClick="btnMG11_save_bottom_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPrint_MG11_bottom" runat="server"
                                        Text="Print" Width="80px" OnClick="btnPrint_MG11_bottom_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                              <%--  <asp:Button ID="btnClear_MG11_bottom" runat="server" Text="Clear" Width="80px" />--%>
                                </td>
                            </tr>
                        </table>

                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="TabPanel3" TabIndex="4">
                    <HeaderTemplate>
                        <div style="width: 40px;" class="lbl">
                            MG2
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 414px" align="center">
                                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 226px"></td>
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
                                <td style="width: 298px; height: 26px">&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="81px" Visible="False" />
                                </td>
                                <td style="width: 90px; height: 26px;">
                                    <asp:Button ID="btnsave" runat="server" Text="Save" Width="71px" OnClick="btnsave_Click" />
                                </td>
                                <td style="height: 26px; width: 103px;">
                                    <asp:Button ID="Button1" runat="server" Text="Print" Width="76px" OnClick="btnPrint_Click" />
                                </td>
                                <td style="height: 26px">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="76px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 298px">&nbsp;
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <table style="padding-right: 60px; width: 100%">
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: right; width: 85%">WCB Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtwcbcasenumber" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Carrier Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtCarrierCaseNo" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Date of Injury :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofInjury" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">A.&nbsp
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Patient's First Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFirstName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Middle Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMiddleName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Last Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtLastName" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Social Security No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSocialSecurityNo" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Patient's Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPatientAddress" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Employer's Name & Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtEmployerNameAdd" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Insurance Carrier's Name & Address :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtInsuranceNameAdd" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">B.&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Attending Doctor's Name & Address :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLAttendingDoctors" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLAttendingDoctors_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp&nbsp&nbsp Individual Provider's WCB Authorization No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtIndividualProvider" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp;&nbsp;&nbsp;&nbsp; Telephone No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTelephone" runat="server" MaxLength="15"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp;&nbsp;&nbsp;&nbsp; Fax No. :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFaxNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">C.&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">The undersigned requests approval to VARY from the WCB Medical Treatment Guidelines
                                                as indicated below:
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;" valign="top">&nbsp;&nbsp;&nbsp;&nbsp; Guideline Reference :
                                </td>
                                <td valign="top">
                                    <%-- <asp:DropDownList ID="ddlGuidline" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGuidline_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlGuidline" runat="server"
                                        onchange="javascript:SplitS(this);" Width="164px">
                                        <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                        <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                        <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                        <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                        <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                        <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TxtGuislineChar" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="false"></asp:TextBox>
                                    -
                                                <asp:TextBox ID="TxtGuidline1" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                                    ReadOnly="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline2" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline3" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline4" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtGuidline5" runat="server" MaxLength="1" Width="18px" Enabled="true"
                                        ReadOnly="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">(In first box, indicate body part: K = Knee, S = Shoulder, B = Mid and Low Back,
                                                N = Neck, C = Carpal Tunnel. In remaining boxes, indicate corresponding section
                                                of WCB Medical Treatment Guidelines. If the treatment requested is not addressed
                                                by the Guidelines, in the remaining boxes use NONE.)
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp;&nbsp; Approval Requested for: (one request type per form)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtApproval" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp Date of service of supporting medical in WCB case file, if not attached:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofService" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender37" runat="server" TargetControlID="TxtDateofService"
                                        PopupButtonID="ImageButton1" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp;&nbsp; Date(s) of previously denied variance request for substantially similar
                                                treatment, if applicable:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofApplicable" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender38" runat="server" TargetControlID="TxtDateofApplicable"
                                        PopupButtonID="ImgDate" Enabled="True" />
                                    <%--   <a id="trigger" href="#">
                        <input type="image" name="mgbtnDateofService" id="imgbtnDateofService" runat="server"
                            src="Images/cal.gif" border="0" /></a>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp;&nbsp; I certify that I am making the above request for approval of a variance
                                                and my affirmative statements are true and correct. I certify that I have read and
                                                applied the Medical Treatment Guidelines to the treatment and care in this case
                                                and that I am requesting this variance before rendering any medical care that varies
                                                from the Guidelines. I certify that the claimant understands and agrees to undergo
                                                the proposed medical care. i
                                               <%-- <asp:CheckBox ID="Chkdid" runat="server" />--%>
                                    <prb:PDFRadioButton ID="Chkdid" GroupName="MG2did" runat="server" />
                                    did /
                                                <%--<asp:CheckBox ID="Chkdidnot" runat="server" />--%>
                                    <prb:PDFRadioButton ID="Chkdidnot" GroupName="MG2did" runat="server" />
                                    did not contact the carrier by telephone to discuss this variance request before
                                                making the request. I contacted the carrier by telephone on and
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="TxtSpoke" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp spoke to (person spoke to or was not able to speck to anyone)
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtspecktoanyone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp
                                                <asp:CheckBox ID="ChkAcopy" runat="server" />
                                    A copy of this form was sent to the carrier/employer/self-insured employer/Special
                                                Fund by &nbsp&nbsp (fax number or email address required)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAddressRequired" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp A copy was sent to the Workers' Compensation Board, and copies were provided
                                                to the claimant’s legal counsel, if any, to the claimant if not represented, and
                                                to any other parties of interest within two (2) business days of the date below.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp
                                                <asp:CheckBox ID="ChkIAmnot" runat="server" />
                                    &nbsp; I am not equipped to send or receive forms by fax or email. This form was
                                                mailed to the parties indicated above on
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtaboveon" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp In addition, I certify that I do not have a substantially similar request
                                                pending and that this request contains additional supporting medical evidence if
                                                it is substantially similar to a prior denied request.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtProviderDate" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgProviderDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender39" runat="server" TargetControlID="TxtProviderDate"
                                        PopupButtonID="ImgProviderDate" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">THE WORKERS' COMPENSATION BOARD EMPLOYS AND SERVES PEOPLE WITH DISABILITIES WITHOUT
                                                DISCRIMINATION.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp Patient Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPatientName" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp WCB Case Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtWCBCaseNumber2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp Date of Injury :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateofInjury2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">D.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">&nbsp&nbsp CARRIER'S / EMPLOYER'S NOTICE OF INDEPENDENT MEDICAL EXAMINATION (IME)
                                                OR MEDICAL RECORDS REVIEW
                                </td>
                                <td>&nbsp
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
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameD" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Title:
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
                                    <asp:TextBox ID="TxtDateD" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateD" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender40" runat="server" TargetControlID="TxtDateD"
                                        PopupButtonID="ImgbtnDateD" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: center;">E.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">CARRIER'S / EMPLOYER'S RESPONSE TO VARIANCE REQUEST
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" valign="top" class="td-CaseDetails-lf-desc-ch">Carrier's response to the variance request is indicated in the checkboxes on the
                                                right. Carrier denial, when appropriate, should be reviewed by a health professional.
                                                (Attach written report of medical professional.) If request is approved or denied,
                                                sign and date the form in Section E.<br />
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSectionE" runat="server" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">CARRIER'S / EMPLOYER'S RESPONSE
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">If service is denied or granted in part, explain in space provided.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <prb:PDFRadioButton ID="ChkGranted" GroupName="MG2Granted" runat="server" />
                                  <%--  <asp:CheckBox ID="ChkGranted" runat="server" />--%>

                                    Granted
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <prb:PDFRadioButton ID="ChkGrantedinPart" GroupName="MG2Granted" runat="server" />
                                    <%--<asp:CheckBox ID="ChkGrantedinPart" runat="server" />--%>
                                    Granted in Part
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                   <prb:PDFRadioButton ID="ChkWithoutPrejudice" GroupName="MG2Granted" runat="server" />
                                   <%-- <asp:CheckBox ID="ChkWithoutPrejudice" runat="server" />--%>
                                    Without Prejudice
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <prb:PDFRadioButton ID="ChkDenied" GroupName="MG2Granted" runat="server" />
                                    <%--<asp:CheckBox ID="ChkDenied" runat="server" />--%>
                                    Denied
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <prb:PDFRadioButton ID="ChkBurden" GroupName="MG2Granted" runat="server" />
                                    <%--<asp:CheckBox ID="ChkBurden" runat="server" />--%>
                                    Burden of Proof Not Met
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                   <prb:PDFRadioButton ID="ChkSubstantially" GroupName="MG2Granted" runat="server" />
                                   <%-- <asp:CheckBox ID="ChkSubstantially" runat="server" />--%>
                                    Substantially Similar Request Pending or Denied
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">Name of the Medical Professional who reviewed the denial, if applicable:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMedicalProfes" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">I certify that copies of this form were sent to the Treating Medical Provider requesting
                                                the variance, the Workers' Compensation Board, the claimant's legal counsel, if
                                                any, and any other parties of interest, with the written report of the medical professional
                                                in the office of the carrier/employer/selfinsured employer/Special Fund attached,
                                                within two (2) business days of the date below.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">(Please complete if request is denied.) If the issue cannot be resolved informally,
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
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameE" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitleE" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateE" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateE" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender41" runat="server" TargetControlID="TxtDateE"
                                        PopupButtonID="ImgbtnDateE" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: center;" class="td-CaseDetails-lf-desc-ch">F.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">DENIAL INFORMALLY DISCUSSED AND RESOLVED BETWEEN PROVIDER AND CARRIER
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">I certify that the provider's variance request initially denied above is now granted
                                                or partially granted.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">By: (print name)
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtByPrintNameF" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="td-CaseDetails-lf-desc-ch">Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitleF" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDateF" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgbtnDateF" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender42" runat="server" TargetControlID="TxtDateF"
                                        PopupButtonID="ImgbtnDateF" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: center;" class="td-CaseDetails-lf-desc-ch">G.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">CLAIMANT'S / CLAIMANT REPRESENTATIVE'S REQUEST FOR REVIEW OF SELF-INSURED EMPLOYER'S
                                                / CARRIER'S DENIAL
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">NOTE to Claimant's / Claimant Licensed Representative's: The claimant should only
                                                sign this section after the request is fully or partially denied. This section should
                                                not be completed at the time of initial request.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">YOU MUST COMPLETE THIS SECTION IF YOU WANT THE BOARD TO REVIEW THE CARRIER'S DENIAL
                                                OF THE PROVIDER'S VARIANCE REQUEST.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right;">
                                    <asp:CheckBox ID="ChkIrequestG" runat="server" />
                                    I request that the Workers' Compensation Board review the carrier's denial of my
                                                doctor's request for approval to vary from the Medical Treatment Guidelines. I opt
                                                for the decision to be made
                                                <asp:CheckBox ID="ChkMadeG" runat="server" />
                                    by the Medical Arbitrator designated by the Chair <
                                                <asp:CheckBox ID="ChkChairG" runat="server" />
                                    or at a WCB Hearing. I understand that if either party, the carrier or the claimant,
                                                opts in writing for resolution at a WCB hearing; the decision will be made at a
                                                WCB hearing. I understand that if neither party opts for resolution at a hearing,
                                                the variance issue will be decided by a medical arbitrator and the resolution is
                                                binding and not appealable under WCL § 23.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">
                                    <%-- Claimant's / Claimant Representative's Signature:--%>
                                    <asp:TextBox ID="TxtClairmantSignature" runat="server" Visible="false"></asp:TextBox>&nbsp;
                                                Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtClaimantDate" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    <ajaxcontrol:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="TxtClaimantDate"
                                        PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" class="td-CaseDetails-lf-desc-ch">ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED,
                                                OR PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER,
                                                OR SELF-INSURER, ANY INFORMATION CONTAINING ANY FALSE MATERIAL STATEMENT OR CONCEALS
                                                ANY MATERIAL FACT SHALL BE GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND
                                                IMPRISONMENT.
                                </td>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;" align="center" class="td-CaseDetails-lf-desc-ch">NYS Workers' Compensation Board, Centralized Mailing, PO Box 5205, Binghamton, NY
                                                13902-5205 Customer Service Toll-Free Number: 877-632-4996
                                </td>
                                <td>&nbsp
                                                <asp:TextBox ID="TxtSignatureF" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="visibility:hidden; position:absolute;">
                                <td align="center" class="td-CaseDetails-lf-desc-ch" style="height: 21px; text-align: right;">Specialty
                                </td>
                                <td>
                                    <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" AutoPost_back="true"
                                        Connection_Key="Connection_String" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                        OldText="" Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="---Select---"
                                        StausText="False" Width="100px" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr style="visibility:hidden; position:absolute;">
                                <td align="center" class="td-CaseDetails-lf-desc-ch" style="height: 21px; text-align: right;"
                                    colspan="2">
                                    <div style="width: 60%; height: 200px; overflow: scroll;">
                                        <asp:DataGrid ID="grdProcedure" runat="server" AutoGenerateColumns="False" CssClass="GridTable"
                                            DataKeyField="SZ_TYPE_CODE_ID">
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID ID" Visible="False"></asp:BoundColumn>
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
                        <table width="100%">                           
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 298px; height: 26px">&nbsp;
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
                                    <asp:Button ID="btnClearBottom" runat="server" Text="Clear" Width="76px" />
                                </td>
                                <%--<td>
                </td>     --%>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="TabPanel4" TabIndex="5" >
                    <HeaderTemplate>
                        <div style="width: 40px;" class="lbl">
                            MG2.1
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 414px" align="center">
                                    <UserMessage:MessageControl ID="UserMsgMG21" runat="server"></UserMessage:MessageControl>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 226px"></td>
                                <td style="height: 26px" align="center">
                                    <asp:Label ID="lblMG21Msg" runat="server" Text="You already have 1 MG2 document associated with the bill. Click"
                                        Visible="false"></asp:Label>
                                    <asp:HyperLink ID="HyperLink21" runat="server" Text="here" Target="_blank" Visible="false"></asp:HyperLink>
                                    <asp:Label ID="lblMG21" runat="server" Text=" to view." Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 298px">&nbsp;
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <table style="margin-bottom: 20px; width: 100%">
                            <tr style="padding-bottom: 10px">
                                <td style="height: 26px;" align="center">
                                    <asp:Button ID="btnTopMG2Save" runat="server" Text="Save" Width="71px" OnClick="btnTopMG2Save_Click" />
                                    <asp:Button ID="btnTopMG2Print" runat="server" OnClick="btnTopMG2Print_Click" Text="Print"
                                        Width="76px" />
                                   <%-- <asp:Button ID="btnTopMG2Clear" runat="server" Text="Clear" Width="76px" OnClientClick="ClearMG21()" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">WCB Case Number :&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_WCBCaseNo" AssociatedPDFControlName="[txtMG2_WCBCaseNo]"
                                                    runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp; Carrier Case Number :&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_CarrierCaseNo"
                                        AssociatedPDFControlName="[txtMG2_CarrierCaseNo]" runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp; Date of Injury :&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_DateOfInjury"
                                        AssociatedPDFControlName="[txtMG2_DateOfInjury]" runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender43" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfInjury">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Patient's First Name :&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_PatientFirstName" AssociatedPDFControlName="[txtMG2_PatientFirstName]"
                                                    runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp;Middle Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox
                                        ID="txtMG2_PatientMiddleName" AssociatedPDFControlName="[txtMG2_PatientMiddleName]"
                                        runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp; Last Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_PatientLastName" AssociatedPDFControlName="[txtMG2_PatientLastName]"
                                                    runat="server"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Social Security No. :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_SocialSecurityNo"
                                    AssociatedPDFControlName="[txtMG2_SocialSecurityNo]" runat="server"></ptb:PDFTextBox>&nbsp;&nbsp;
                                                Patient's Address :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox
                                                    ID="txtMG2_PatientAddress" AssociatedPDFControlName="[txtMG2_PatientAddress]"
                                                    runat="server"></ptb:PDFTextBox>&nbsp;&nbsp;&nbsp; Insurance Carrier's Name
                                                & Address :<ptb:PDFTextBox ID="txtMG2_InsuCarrAddress" AssociatedPDFControlName="[txtMG2_InsuCarrAddress]"
                                                    runat="server"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Doctor's Name :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                    ID="ddlMG2Doctor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMG2Doctor_SelectedIndexChanged">
                                </asp:DropDownList>
                                    &nbsp;&nbsp;Doctor's WCB Authorization Number :
                                                <ptb:PDFTextBox ID="txtMg2_DoctorWCBAuthNo" AssociatedPDFControlName="[txtMg2_DoctorWCBAuthNo]"
                                                    runat="server"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">A. The undersigned requests additional approval(s) to VARY from the WCB Medical
                                                Treatment Guidelines as indicated below:
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">2.Guideline Reference:
                                                <asp:DropDownList ID="ddl2MG2GuideRef" runat="server"
                                                    onchange="javascript:SplitSMG21(this);">
                                                    <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                                    <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                                    <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                    <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                                    <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                                    <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                                </asp:DropDownList>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef1" AssociatedPDFControlName="[txtMG2_GudRef1]" runat="server"
                                        Width="20px"></ptb:PDFTextBox>-
                                                <ptb:PDFTextBox ID="txtMG2_GudRef2" AssociatedPDFControlName="[txtMG2_GudRef2]" runat="server"
                                                    Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef3" AssociatedPDFControlName="[txtMG2_GudRef3]" runat="server"
                                        Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef4" AssociatedPDFControlName="[txtMG2_GudRef4]" runat="server"
                                        Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef5" AssociatedPDFControlName="[txtMG2_GudRef5]" runat="server"
                                        Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef6" AssociatedPDFControlName="[txtMG2_GudRef6]" runat="server"
                                        Width="20px"></ptb:PDFTextBox>
                                    Date(s) of previously denied variance request (for substantially similar treatment,
                                                if applicable): (In first box, indicate injury and/or condition: K = Knee, S = Shoulder,
                                                B = Mid and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining
                                                boxes, indicate corresponding section of WCB Medical Treatment Guidelines. If the
                                                treatment requested is not addressed by the Guidelines, in the remaining boxes use
                                                NONE.)
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date of service of supporting medical in WCB case file (Attach if not already submitted.):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfServiceWCB" AssociatedPDFControlName="[txtMG2_DateOfServiceWCB]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="txtMG2_DateOfServiceWCB_CalendarExtender" runat="server"
                                        Format="MM/dd/yyyy" Enabled="True" TargetControlID="txtMG2_DateOfServiceWCB">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date(s) of previously denied variance request (for substantially similar treatment,
                                                if applicable):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfPrevDenied" AssociatedPDFControlName="[txtMG2_DateOfPrevDenied]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender44" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfPrevDenied">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Approval Requested for:
                                                <ptb:PDFTextBox ID="txtMG2_ApprovalRequest" AssociatedPDFControlName="[txtMG2_ApprovalRequest]"
                                                    runat="server"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Necessity: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_MedicalNecessity" AssociatedPDFControlName="[txtMG2_MedicalNecessity]"
                                                    runat="server" TextMode="MultiLine" Width="700px"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">3.Guideline Reference:
                                                <asp:DropDownList ID="ddl3MG2_GudRef" runat="server"
                                                    onchange="javascript:SplitSMG21_3(this);">
                                                    <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                                    <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                                    <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                    <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                                    <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                                    <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                                </asp:DropDownList>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef11" AssociatedPDFControlName="[txtMG2_GudRef11]"
                                        runat="server" Width="18px "></ptb:PDFTextBox>-
                                                <ptb:PDFTextBox ID="txtMG2_GudRef12" AssociatedPDFControlName="[txtMG2_GudRef12]"
                                                    runat="server" Width="18px "></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef13" AssociatedPDFControlName="[txtMG2_GudRef13]"
                                        runat="server" Width="18px "></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef14" AssociatedPDFControlName="[txtMG2_GudRef14]"
                                        runat="server" Width="18px "></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef15" AssociatedPDFControlName="[txtMG2_GudRef15]"
                                        runat="server" Width="18px "></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef16" AssociatedPDFControlName="[txtMG2_GudRef16]"
                                        runat="server" Width="18px "></ptb:PDFTextBox>
                                    (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,
                                                indicate corresponding section of WCB Medical Treatment Guidelines. If the treatment
                                                requested is not addressed by the Guidelines, in the remaining boxes use NONE.)
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date of service of supporting medical in WCB case file (Attach if not already submitted.):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfService2" AssociatedPDFControlName="[txtMG2_DateOfService2]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender45" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfService2">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date(s) of previously denied variance request(for substantially similar treatment,
                                                if applicable):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfPrev2" AssociatedPDFControlName="[txtMG2_DateOfPrev2]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender46" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfPrev2">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Approval Requested for:
                                                <ptb:PDFTextBox ID="txtMG2_ApprovalRequest2" AssociatedPDFControlName="[txtMG2_ApprovalRequest2]"
                                                    runat="server"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Necessity: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_MedicalNecessity2" AssociatedPDFControlName="[txtMG2_MedicalNecessity2]"
                                                    runat="server" Width="700px" TextMode="MultiLine"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">4.Guideline Reference:
                                                <asp:DropDownList ID="ddl4MG2_GudRef" runat="server"
                                                    onchange="javascript:SplitSMG21_4(this);">
                                                    <%-- AutoPostBack ="true" OnSelectedIndexChanged ="ddlGuidline_SelectedIndexChanged" onchange="javascript:SplitS(this);" --%>
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                                    <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                                    <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                    <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                                    <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                                    <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                                </asp:DropDownList>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef21" AssociatedPDFControlName="[txtMG2_GudRef21]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>-
                                                <ptb:PDFTextBox ID="txtMG2_GudRef22" AssociatedPDFControlName="[txtMG2_GudRef22]"
                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef23" AssociatedPDFControlName="[txtMG2_GudRef23]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef24" AssociatedPDFControlName="[txtMG2_GudRef24]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef25" AssociatedPDFControlName="[txtMG2_GudRef25]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef26" AssociatedPDFControlName="[txtMG2_GudRef26]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,
                                                indicate corresponding section of WCB Medical Treatment Guidelines. If the treatment
                                                requested is not addressed by the Guidelines, in the remaining boxes use NONE.)
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date of service of supporting medical in WCB case file (Attach if not already submitted.):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfService3" AssociatedPDFControlName="[txtMG2_DateOfService3]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender47" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfService3">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date(s) of previously denied variance request (for substantially similar treatment,
                                                if applicable):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfPrev3" AssociatedPDFControlName="[txtMG2_DateOfPrev3]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender48" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfPrev3">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Approval Requested for:
                                                <ptb:PDFTextBox ID="txtMG2_ApprovalRequest3" AssociatedPDFControlName="[txtMG2_ApprovalRequest3]"
                                                    runat="server" Width="700px"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Necessity: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_MedicalNecessity3" AssociatedPDFControlName="[txtMG2_MedicalNecessity3]"
                                                    runat="server" Width="700px" TextMode="MultiLine"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">5.Guideline Reference:
                                                <asp:DropDownList ID="ddl5MG2_GudRef" runat="server" onchange="javascript:SplitSMG21_5(this);">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="K-E7E">K-E7E</asp:ListItem>
                                                    <asp:ListItem Value="B-D9A">B-D9A</asp:ListItem>
                                                    <asp:ListItem Value="N-D10D">N-D10D</asp:ListItem>
                                                    <asp:ListItem Value="S-D10EI">S-D10EI</asp:ListItem>
                                                    <asp:ListItem Value="N-D11DI">N-D11DI</asp:ListItem>
                                                    <asp:ListItem Value="B-D10a1">B-D10a1</asp:ListItem>
                                                </asp:DropDownList>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef31" AssociatedPDFControlName="[txtMG2_GudRef31]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>-
                                                <ptb:PDFTextBox ID="txtMG2_GudRef32" AssociatedPDFControlName="[txtMG2_GudRef32]"
                                                    runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef33" AssociatedPDFControlName="[txtMG2_GudRef33]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef34" AssociatedPDFControlName="[txtMG2_GudRef34]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef35" AssociatedPDFControlName="[txtMG2_GudRef35]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    <ptb:PDFTextBox ID="txtMG2_GudRef36" AssociatedPDFControlName="[txtMG2_GudRef36]"
                                        runat="server" Width="20px"></ptb:PDFTextBox>
                                    (In first box, indicate injury and/or condition: K = Knee, S = Shoulder, B = Mid
                                                and Low Back, N = Neck, C = Carpal Tunnel, P = Non-Acute Pain. In remaining boxes,
                                                indicate corresponding section of WCB Medical Treatment Guidelines. If the treatment
                                                requested is not addressed by the Guidelines, in the remaining boxes use NONE.)
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date of service of supporting medical in WCB case file (Attach if not already submitted.):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfService4" AssociatedPDFControlName="[txtMG2_DateOfService4]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender49" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfService4">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Date(s) of previously denied variance request(for substantially similar treatment,
                                                if applicable):
                                                <ptb:PDFTextBox ID="txtMG2_DateOfPrev4" AssociatedPDFControlName="[txtMG2_DateOfPrev4]"
                                                    runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender50" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfPrev4">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Approval Requested for:
                                                <ptb:PDFTextBox ID="txtMG2_ApprovalRequest4" AssociatedPDFControlName="[txtMG2_ApprovalRequest4]"
                                                    runat="server" Width="700px"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Necessity: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_MedicalNecessity4" AssociatedPDFControlName="[txtMG2_MedicalNecessity4]"
                                                    runat="server" Width="700px" TextMode="MultiLine"></ptb:PDFTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">STATEMENT OF MEDICAL NECESSITY - See requirements on Form MG-2.
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Your explanation must provide the following information:
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- the basis for your opinion that the medical care you propose is appropriate
                                                for the claimant and is medically necessary at this time; and
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- an explanation why alternatives set forth in the Medical Treatment
                                                Guidelines are not appropriate or sufficient.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Additionally, variance requests to extend treatment beyond recommended maximum duration/frequency
                                                must include:
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- a description of the functional outcomes that, as of the date of the
                                                variance request, have continued to demonstrate objective improvement from that
                                                treatment and are reasonably expected &nbsp;&nbsp; &nbsp;&nbsp; to further improve
                                                with additional treatment; and
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- the specific duration or frequency of treatment for which a variance
                                                is requested.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Variance requests for treatment or testing that is not recommended or not addressed,
                                                must include:
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- the signs and symptoms that have failed to improve with previous treatments
                                                provided according to the Medical Treatment Guidelines; and.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">&nbsp;&nbsp;- medical evidence in support of efficacy of the proposed treatment
                                                or testing- may include relevant medical literature published in recognized peer
                                                reviewed journals.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">Patient Name: &nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_PatientName" AssociatedPDFControlName="[txtMG2_PatientName]"
                                    runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp;&nbsp; WCB Case Number:&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_WCBCaseNo1"
                                        AssociatedPDFControlName="[txtMG2_WCBCaseNo1]" runat="server"></ptb:PDFTextBox>
                                    &nbsp;&nbsp;&nbsp; Date of Injury :&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_DateOfInjury1"
                                        AssociatedPDFControlName="[txtMG2_DateOfInjury1]" runat="server"></ptb:PDFTextBox>
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender51" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DateOfInjury1">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">HEALTH PROVIDER'S CERTIFICATION
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">I certify that I am making the above request for approval of a variance and my affirmative
                                                statements are true and correct. I certify that I have read and applied the
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Treatment Guidelines to the treatment and care in this case and that I am
                                                requesting this variance before rendering any medical care that varies from the
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Medical Treatment Guidelines. I certify that the claimant understands and agrees
                                                to undergo the proposed medical care.I
                                                 <prb:PDFRadioButton ID="chkMG2_I" GroupName="MG21did" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMG2_I" runat="server" />--%>
                                    did /
                                                <%--<pcb:PDFCheckBox ID="chkMG2_Did" runat="server" />--%>
                                                <prb:PDFRadioButton ID="chkMG2_Did" GroupName="MG21did" runat="server" />
                                    did not contact the carrier by
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">telephone to discuss this variance request before making the request. I contacted
                                                the carrier by telephone on (date)
                                                <ptb:PDFTextBox ID="txtMG2_TelephoneDate" runat="server" />
                                    and spoke to (person spoke
                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender52" runat="server" Format="MM/dd/yyyy"
                                                    Enabled="True" TargetControlID="txtMG2_TelephoneDate">
                                                </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">to or was not able to speak to anyone)
                                                <ptb:PDFTextBox ID="txtMG2_PersonSpoke" runat="server" Width="705px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <pcb:PDFCheckBox ID="chkMG2_Copy" runat="server" />A copy of this form was sent
                                                to the carrier/employer/self-insured employer/Special Fund (fax number or e-mail
                                                address required)
                                                <ptb:PDFTextBox ID="txtMG2_Fax" runat="server" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">A copy was sent (see address on instruction page) to the Workers' Compensation Board,
                                                and copies were provided to the claimant’s legal counsel, if any, to the claimant
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">if not represented, and to any other parties of interest within two (2) business
                                                days of the date below.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">In addition, I certify that I do not have a substantially similar request pending
                                                and that this request contains additional supporting medical evidence if it is substantially
                                                similar to a prior denied request.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Provider's Signature: _____________________________________________________ Date:&nbsp;&nbsp;<ptb:PDFTextBox
                                    ID="txtMg2ProviderDate" AssociatedPDFControlName="[txtMg2ProviderDate]" runat="server" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender53" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMg2ProviderDate">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">B. CARRIER'S/EMPLOYER'S NOTICE OF INDEPENDENT MEDICAL EXAMINATION (IME) OR MEDICAL
                                                RECORDS REVIEW
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <pcb:PDFCheckBox ID="chkMG2_Employer" runat="server" />
                                    The carrier/employer hereby gives notice that it will have the claimant examined
                                                by an Independent Medical Examiner and submit Form IME-4 within 30 calendar
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">days of the Variance Request, with respect to:
                                                <pcb:PDFCheckBox ID="chkMG2_RequestCarrier2" runat="server" />Request No. 2
                                                <pcb:PDFCheckBox ID="chkMG2_RequestCarrier3" runat="server" />
                                    Request No. 3<pcb:PDFCheckBox ID="chkMG2_RequestCarrier4" runat="server" />Request
                                                No. 4
                                                <pcb:PDFCheckBox ID="chkMG2_RequestCarrier5" runat="server" />Request No. 5
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">By: (print name): &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2Carrier_PrintName"
                                    AssociatedPDFControlName="[txtMG2_PrintName]" runat="server" />&nbsp;&nbsp;&nbsp;
                                                Title:&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2CarrierPrint_Title" AssociatedPDFControlName="[txtMG2_Title]"
                                                    runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">Signature: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; __________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Date: &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2Carrier_PrintDate" AssociatedPDFControlName="[txtMG2_Date]"
                                                    runat="server" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender54" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2Carrier_PrintDate">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">C. CARRIER'S/EMPLOYER'S RESPONSE TO ADDITIONAL VARIANCE REQUEST(S)
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Carrier's response to the variance request is indicated in the checkboxes below.
                                                If any additional request(s) are denied, give reason(s) for denial or partial granted
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">below. Identify reasons by Request No. 2-5. (Attach written report of medical professional
                                                for each denial as explained on Form MG-2.)
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Request No. 2:
                                                <prb:PDFRadioButton ID="chkMg2_Grant1" GroupName="MG21CareerRes1" runat="server" />
                                               <%-- <pcb:PDFCheckBox ID="chkMg2_Grant1" runat="server" />--%>Granted

                                             <prb:PDFRadioButton ID="chkMg2_GrantPart1" GroupName="MG21CareerRes1" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_GrantPart1" runat="server" />--%>Granted in Part

                                             <prb:PDFRadioButton ID="chkMg2_Denied" GroupName="MG21CareerRes1" AssociatedPDFControlName="[chkMg2_Denied]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Denied" AssociatedPDFControlName="[chkMg2_Denied]" runat="server" />--%>Denied

                                             <prb:PDFRadioButton ID="chkMg2_Burden" GroupName="MG21CareerRes1" AssociatedPDFControlName="[chkMg2_Burden]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Burden" AssociatedPDFControlName="[chkMg2_Burden]" runat="server" />--%>Burden
                                                of Proof Not Met

                                             <prb:PDFRadioButton ID="chkMg2_Substantially" GroupName="MG21CareerRes1" AssociatedPDFControlName="[chkMg2_Substantially]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Substantially" AssociatedPDFControlName="[chkMg2_Substantially]"
                                                    runat="server" />--%>Substantially Similar Request Pending or Denied

                                             <prb:PDFRadioButton ID="chkMG2_WithoutPre" GroupName="MG21CareerRes1" AssociatedPDFControlName="[chkMG2_WithoutPre]" runat="server" />
                                               <%-- <pcb:PDFCheckBox ID="chkMG2_WithoutPre" AssociatedPDFControlName="[chkMG2_WithoutPre]"
                                                    runat="server" />--%>Without Prejudice
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Request No. 3:
                                    <prb:PDFRadioButton ID="chkMg2_Grant2" GroupName="MG21CareerRes2" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Grant2" runat="server" />--%>Granted

                                    <prb:PDFRadioButton ID="chkMg2_GrantPart2" GroupName="MG21CareerRes2" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_GrantPart2" runat="server" />--%>Granted in Part

                                      <prb:PDFRadioButton ID="chkMg2_Denied1" GroupName="MG21CareerRes2" AssociatedPDFControlName="[chkMg2_Denied1]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Denied1" AssociatedPDFControlName="[chkMg2_Denied1]"
                                                    runat="server" />--%>Denied

                                    <prb:PDFRadioButton ID="chkMg2_Burden1" GroupName="MG21CareerRes2" AssociatedPDFControlName="[chkMg2_Burden1]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Burden1" AssociatedPDFControlName="[chkMg2_Burden1]"
                                                    runat="server" />--%>Burden of Proof Not Met

                                    <prb:PDFRadioButton ID="chkMg2_Substantially1" GroupName="MG21CareerRes2" AssociatedPDFControlName="[chkMg2_Substantially1]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Substantially1" AssociatedPDFControlName="[chkMg2_Substantially1]"
                                                    runat="server" />--%>Substantially Similar Request Pending or Denied
                                                
                                    <prb:PDFRadioButton ID="chkMG2_WithoutPre1" GroupName="MG21CareerRes2" AssociatedPDFControlName="[chkMG2_WithoutPre1]" runat="server" />
                                    <%--<pcb:PDFCheckBox ID="chkMG2_WithoutPre1" AssociatedPDFControlName="[chkMG2_WithoutPre1]"
                                                    runat="server" />--%>Without Prejudice
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Request No. 4:
                                    <prb:PDFRadioButton ID="chkMg2_Grant3" GroupName="MG21CareerRes3" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Grant3" runat="server" />--%>Granted

                                      <prb:PDFRadioButton ID="chkMg2_GrantPart3" GroupName="MG21CareerRes3" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_GrantPart3" runat="server" />--%>Granted in Part

                                      <prb:PDFRadioButton ID="chkMg2_Denied2" GroupName="MG21CareerRes3" AssociatedPDFControlName="[chkMg2_Denied2]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Denied2" AssociatedPDFControlName="[chkMg2_Denied2]"
                                                    runat="server" />--%>Denied

                                    <prb:PDFRadioButton ID="chkMg2_Burden2" GroupName="MG21CareerRes3" AssociatedPDFControlName="[chkMg2_Burden2]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Burden2" AssociatedPDFControlName="[chkMg2_Burden2]"
                                                    runat="server" />--%>Burden of Proof Not Met
                                    
                                    <prb:PDFRadioButton ID="chkMg2_Substantially2" GroupName="MG21CareerRes3" AssociatedPDFControlName="[chkMg2_Substantially2]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Substantially2" AssociatedPDFControlName="[chkMg2_Substantially2]"
                                                    runat="server" />--%>Substantially Similar Request Pending or Denied

                                    <prb:PDFRadioButton ID="chkMG2_WithoutPre2" GroupName="MG21CareerRes3" AssociatedPDFControlName="[chkMG2_WithoutPre2]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMG2_WithoutPre2" AssociatedPDFControlName="[chkMG2_WithoutPre2]"
                                                    runat="server" />--%>Without Prejudice
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Request No. 5:
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Grant4" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />--%>
                                    <prb:PDFRadioButton ID="chkMg2_Grant4" GroupName="MG21CareerRes4" AssociatedPDFControlName="[chkMg2_Grant4]" runat="server" />Granted

                                    <prb:PDFRadioButton ID="chkMg2_GrantPart4" GroupName="MG21CareerRes4" AssociatedPDFControlName="[chkMg2_GrantPart4]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_GrantPart4" AssociatedPDFControlName="[chkMg2_GrantPart4]"
                                                    runat="server" />--%>Granted in Part

                                    <prb:PDFRadioButton ID="chkMg2_Denied3" GroupName="MG21CareerRes4" AssociatedPDFControlName="[chkMg2_Denied3]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Denied3" AssociatedPDFControlName="[chkMg2_Denied3]"
                                                    runat="server" />--%>Denied

                                    <prb:PDFRadioButton ID="chkMg2_Burden3" GroupName="MG21CareerRes4" AssociatedPDFControlName="[chkMg2_Burden3]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Burden3" AssociatedPDFControlName="[chkMg2_Burden3]"
                                                    runat="server" />--%>Burden of Proof Not Met

                                    <prb:PDFRadioButton ID="chkMg2_Substantially3" GroupName="MG21CareerRes4" AssociatedPDFControlName="[chkMg2_Substantially3]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMg2_Substantially3" AssociatedPDFControlName="[chkMg2_Substantially3]"
                                                    runat="server" />--%>Substantially Similar Request Pending or Denied

                                    <prb:PDFRadioButton ID="chkMG2_WithoutPre3" GroupName="MG21CareerRes4" AssociatedPDFControlName="[PDFCheckBox6]" runat="server" />
                                                <%--<pcb:PDFCheckBox ID="chkMG2_WithoutPre3" AssociatedPDFControlName="[PDFCheckBox6]"
                                                    runat="server" />--%>Without Prejudice
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <ptb:PDFTextBox ID="txtMG2_Carrier" runat="server" AssociatedPDFControlName="[txtMG2_Carrier]"
                                        Width="834px" TextMode='MultiLine' />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Name of the Medical Professional who reviewed the denial, if appropriate:
                                                <ptb:PDFTextBox ID="txtMG2_ProfessinalName" runat="server" AssociatedPDFControlName="[txtMG2_ProfessinalName]"
                                                    Width="445px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">I certify that copies of this form were sent to the Treating Medical Provider requesting
                                                the variance, the Workers' Compensation Board, the claimant's legal counsel, if
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">any, and any other parties of interest, with the written report of the medical professional
                                                in the office of the carrier/employer/self-insured employer/Special Fund attached,
                                                within two (2) business days of the date below.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">(Please complete if request is denied.) If the issue cannot be resolved informally,
                                                I opt for the decision to be made
                                                <pcb:PDFCheckBox ID="chkMG2_MedicalArb" runat="server" AssociatedPDFControlName="[chkMG2_MedicalArb]" />
                                    by the Medical Arbitrator designated by the
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Chair or
                                                <pcb:PDFCheckBox ID="chkMG2_Chair" AssociatedPDFControlName="[chkMG2_Chair]" runat="server" />at
                                                a WCB Hearing. I understand that if either party, the carrier or the claimant, opts
                                                in writing for resolution at a WCB hearing; the decision will be made at a WCB hearing.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">I understand that if neither party opts for resolution at a hearing, the variance
                                                issue will be decided by a medical arbitrator and the resolution is binding and
                                                not appealable under WCL § 23.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">By: (print name): &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_PrintName" AssociatedPDFControlName="[txtMG2_PrintName]"
                                    runat="server" />&nbsp;&nbsp;&nbsp; Title:&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_Title"
                                        AssociatedPDFControlName="[txtMG2_Title]" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">Signature: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; __________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Date: &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_Date" AssociatedPDFControlName="[txtMG2_Date]"
                                                    runat="server" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender55" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_Date">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">D.DENIAL INFORMALLY DISCUSSED AND RESOLVED BETWEEN PROVIDER AND CARRIER
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">certify that the provider's variance request initially denied above is now granted
                                                or partially granted for the following requests:
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <pcb:PDFCheckBox ID="chkMG2_DRequest2" AssociatedPDFControlName="[chkMG2_RequestNo2]"
                                        runat="server" />
                                    Request No. 2
                                                <pcb:PDFCheckBox ID="chkMG2_DRequest3" AssociatedPDFControlName="[chkMG2_RequestNo3]"
                                                    runat="server" />
                                    Request No. 3
                                                <pcb:PDFCheckBox ID="chkMG2_DRequest4" AssociatedPDFControlName="[chkMG2_RequestNo4]"
                                                    runat="server" />
                                    Request No. 4
                                                <pcb:PDFCheckBox ID="chkMG2_DRequest5" AssociatedPDFControlName="[chkMgG2_RequestNo5]"
                                                    runat="server" />
                                    Request No. 5
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">By: (print name): &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_DenPrintName" AssociatedPDFControlName="[txtMG2_PrintName]"
                                    runat="server" />&nbsp;&nbsp;&nbsp; Title:&nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_DenTitle"
                                        AssociatedPDFControlName="[txtMG2_Title]" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">Signature: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; __________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Date: &nbsp;&nbsp;&nbsp;<ptb:PDFTextBox ID="txtMG2_DenDate" AssociatedPDFControlName="[txtMG2_Date]"
                                                    runat="server" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender56" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_DenDate">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">E.CLAIMANT'S/CLAIMANT'S REPRESENTATIVE REQUEST FOR REVIEW OF SELF-INSURED EMPLOYER'S/CARRIER'S
                                                DENIAL
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">NOTE to Claimant/Claimant's Attorney or Licensed Representative: The claimant should
                                                only sign this section after the request is denied. This section should not be completed
                                                at the time of initial request.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">
                                    <pcb:PDFCheckBox ID="chkMG2_Irequest" AssociatedPDFControlName="[chkMG2_Irequest]"
                                        runat="server" />
                                    I request that the Workers' Compensation Board review the carrier's denial of my
                                                doctor's
                                                <pcb:PDFCheckBox ID="chkMG2_RequestNo2" AssociatedPDFControlName="[chkMG2_RequestNo2]"
                                                    runat="server" />
                                    Request No. 2
                                                <pcb:PDFCheckBox ID="chkMG2_RequestNo3" AssociatedPDFControlName="[chkMG2_RequestNo3]"
                                                    runat="server" />
                                    Request No. 3
                                                <pcb:PDFCheckBox ID="chkMG2_RequestNo4" AssociatedPDFControlName="[chkMG2_RequestNo4]"
                                                    runat="server" />
                                    Request No. 4
                                                <pcb:PDFCheckBox ID="chkMgG2_RequestNo5" AssociatedPDFControlName="[chkMgG2_RequestNo5]"
                                                    runat="server" />
                                    Request No. 5
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">for approval to vary from the Medical Treatment Guidelines. I opt for the decision
                                                to be made
                                                <pcb:PDFCheckBox ID="chkMG2_MedicalAbr" AssociatedPDFControlName="[chkMG2_MedicalAbr]"
                                                    runat="server" />by the Medical Arbitrator designated by the Chair or
                                                <pcb:PDFCheckBox ID="chkMG2_WCB" AssociatedPDFControlName="[chkMG2_WCB]" runat="server" />
                                    at a WCB
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">Hearing. I understand that if either party, the carrier or the claimant, opts in
                                                writing for resolution at a WCB hearing;the decision will be made at a WCB hearing.
                                                I
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" style="text-align: left;">understand that if neither party opts for resolution at a hearing, the variance
                                                issue will be decided by a medical arbitrator and the resolution is binding and
                                                not appealable under WCL § 23.
                                </td>
                            </tr>
                            <tr>
                                <td class="td-CaseDetails-lf-desc-ch" align="right" style="text-align: left;">Claimant's / Claimant Representative's Signature: &nbsp;&nbsp;&nbsp; _________________________
                                                Date:&nbsp;&nbsp;&nbsp;
                                                <ptb:PDFTextBox ID="txtMG2_ClaimDate" AssociatedPDFControlName="[txtMG2_ClaimDate]"
                                                    runat="server" />
                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender57" runat="server" Format="MM/dd/yyyy"
                                        Enabled="True" TargetControlID="txtMG2_ClaimDate">
                                    </ajaxcontrol:CalendarExtender>
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td>
                                    <asp:HiddenField ID="hdnMG21_Id" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px;" align="center">
                                    <asp:Button ID="btnMG2Save" runat="server" Text="Save" Width="71px" OnClick="btnMG2Save_Click" />
                                    <asp:Button ID="btnMG2Print" runat="server" OnClick="btnMG2Print_Click" Text="Print"
                                        Width="76px" />
                                   <%-- <asp:Button ID="btnMG2Clear" runat="server" Text="Clear" Width="76px" OnClientClick="ClearMG21()" />--%>
                                </td>
                            </tr>
                        </table>

                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

                <ajaxcontrol:TabPanel runat="server" ID="TabPanel5" TabIndex="6" Height="800px">
                    <HeaderTemplate>
                        <div style="width: 105px;" class="lbl">
                            Created MG2/MG2.1
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <dx:ASPxGridView ID="ASPxGridView4" KeyFieldName="I_ID" runat="server" AutoGenerateColumns="True"
                            SettingsPager-PageSize="20" OnRowCommand="ASPxGridView4_RowCommand">
                            <columns>
                                            <dx:GridViewDataColumn FieldName="VisitDate" Caption="Visit Date" VisibleIndex="1"
                                                Width="120px" CellStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CreatedBy" Caption="Created By" VisibleIndex="2"
                                                Width="120px" CellStyle-HorizontalAlign="Center">
                                                 <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="View" Settings-AllowSort="False" Width="25px" VisibleIndex="3">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <Settings AllowSort="False"></Settings>
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" Text='View' CommandName="view">
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True" BackColor="#B5DF82"  />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="" VisibleIndex="4" Visible="false">
                                                <DataItemTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                             <dx:GridViewDataColumn FieldName="Type" Caption="Type" VisibleIndex="2"
                                                Width="120px" CellStyle-HorizontalAlign="Center">
                                                 <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>

                                            <%--<dx:GridViewDataTextColumn FieldName="" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkBill" runat="server" Text="Convert to Bill" Enabled="false" CommandName=""
                                            OnClick='<%# "showPopup(" + "\""+ Eval("SZ_CASE_ID") + "\"" + "," + "\""+ Eval("I_ID") +"\"); return false;" %>'></asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="SZ_CASE_ID" VisibleIndex="6"
                                                Visible="false" Width="120px" CellStyle-HorizontalAlign="Center">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="MG2_Path" Caption="MG2_Path" VisibleIndex="7" Visible="false"
                                                Width="120px" CellStyle-HorizontalAlign="Center">
                                                 <HeaderStyle HorizontalAlign="Center" />
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
                                        </columns>
                            <settingspager pagesize="20">
                                        </settingspager>
                        </dx:ASPxGridView>
                        <asp:HiddenField ID="hdID" runat="server" />
                        <asp:HiddenField ID="hdSpeciality" runat="server" />
                        <asp:HiddenField ID="hdCmpName" runat="server" />
                        <asp:HiddenField ID="hdLogicalpath" runat="server" />
                        <asp:HiddenField ID="hdNodeID" runat="server" />
                        <asp:HiddenField ID="hdNodeName" runat="server" />
                    </ContentTemplate>
                </ajaxcontrol:TabPanel>

            </ajaxcontrol:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dx:ASPxPopupControl ID="ShowPopup" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup"
        HeaderText="Convert to Bill" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="900px"
        ToolTip="ConverttoBill" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <contentstyle>
            <Paddings PaddingBottom="5px" />
        </contentstyle>
    </dx:ASPxPopupControl>
</asp:Content>
