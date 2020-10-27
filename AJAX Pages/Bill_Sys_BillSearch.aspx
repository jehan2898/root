
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillSearch.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_BillSearch"
    AsyncTimeout="10000" Title="Bill Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, 
PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //var postBackElementID;
        //var postBackElementClass;
        //$(document).ready(function () {
        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    prm.add_initializeRequest(InitializeRequest);
        //    prm.add_endRequest(EndRequest);


        //    function InitializeRequest(sender, args) {
        //        debugger;
        //        postBackElementID = args._postBackElement.id;
        //        var ele = document.getElementById(postBackElementID);

        //        postBackElementClass = ele.className;
        //        ele.disabled = true;
        //        ele.className = ''
        //    }
        //    function EndRequest(sender, args) {
        //        debugger;
        //        document.getElementById(postBackElementID).disabled = false;
        //        document.getElementById(postBackElementID).className = postBackElementClass
        //    }
        //});


    </script>
    <script type="text/javascript">
        function paymentscan(caseid, specialty, nodetype, billno, paymentid) {
            debugger;
            scanPayment(caseid, specialty, nodetype, billno, paymentid, '4');
        }
    </script>

    <script type="text/javascript">
        
        function ValidateInterestAmount()
        {
            debugger;
            var save = document.getElementById('ctl00_ContentPlaceHolder1_rdbList_2');
            var writeoff = document.getElementById('ctl00_ContentPlaceHolder1_rdbList_1');
            var litigation = document.getElementById('ctl00_ContentPlaceHolder1_rdbList_0');

            if (writeoff.checked) {
                $("#"+"<%=txtInterestAmount.ClientID%>").prop("disabled", "disabled");
                document.getElementById("<%=txtInterestAmount.ClientID%>").value = '0';
            }
            else if (litigation.checked) {
                $("#" + "<%=txtInterestAmount.ClientID%>").prop("disabled", "disabled");
                document.getElementById("<%=txtInterestAmount.ClientID%>").value = '0';
            }
            else if (save.checked) {
$("#" + "<%=txtInterestAmount.ClientID%>").prop("disabled", "");
            }

        }
        function showPopup(billnumber, caseid, speciality, caseno) {
            var url = 'FrmPopupPage.aspx?billnumber=' + billnumber + '&caseid=' + caseid + '&speciality=' + speciality + '&caseno=' + caseno;
            ShowPopup.SetContentUrl(url);
            ShowPopup.Show();

            return false;
        }
        function showPopup2(billnumber, caseid, speciality, caseno) {
            var url = 'Bill_Sys_Bill_Hp1Form.aspx?billnumber=' + billnumber + '&caseid=' + caseid + '&speciality=' + speciality + '&caseno=' + caseno;
            ShowPopup2.SetContentUrl(url);
            ShowPopup2.Show();

            return false;
        }
        function showHPJ1form(billnumber, caseid, speciality, caseno) {

            var url = 'HPJ1Form.aspx?billnumber=' + billnumber + '&caseid=' + caseid + '&speciality=' + speciality + '&caseno=' + caseno;
            showHPJ1.SetContentUrl(url);
            showHPJ1.Show();

            return false;
        }

        function showOTPTinfoPopup(billnumber, caseid) {

            document.getElementById('divid4').style.zIndex = 1;
            document.getElementById('divid4').style.position = 'fixed';
            document.getElementById('divid4').style.left = '100px';
            document.getElementById('divid4').style.top = '30px';
            document.getElementById('divid4').style.visibility = 'visible';
            document.getElementById('frameeditexpanse1').src = '../Bill_Sys_PatientInformationOT-PT.aspx?billnumber=' + billnumber + '&caseid=' + caseid;
            return false;

        }
        //On document ready
        $(function () {
            $("input[type='radio']").on('click', function (e) {
                alert(e);
            });
        });



        function HideDownloadButton() {
            alert('1');

        }
<%--        function Validate(id) {
            //var drpValue = document.getElementById("<%=ddlPayemntType.ClientID%>").options[document.getElementById("<%=ddlPayemntType.ClientID%>").selectedIndex].text;

            if (drpValue == "Received as Interest") {

                //           var drpValue = document.getElementById("<%=rdbList.ClientID%>").value;  
                //           var val = 0;
                //                for( var i = 0; i < drpValue.length; i++ )
                //                {
                //                    alert('1');
                //                }

                //            var rdbValues = document.getElementById("<%=rdbList.ClientID%>").childNodes; 
                //            alert(rdbValues.length);
                //                  for (var j = 0; j <= rdbValues.length; j++)            
                //                  { 
                //                    alert(j);
                //                    if (rdbValues[ j ].checked) 
                //                    {  
                //                       alert('1');
                //                    }  
                //                    
                //                   }
                var RB1 = document.getElementById("<%=rdbList.ClientID%>");
                var radio = RB1.getElementsByTagName("input");

                for (var i = 0; i < radio.length; i++) {
                    if (i != 2) {
                        radio[i].disabled = true;
                    }
                    //                        if (radio[ i ].checked) 
                    //                        {  
                    //                            
                    //                        }  
                    //                        else
                    //                        {
                    //                            radio[ i ].disabled = true;
                    //                            
                    //                        }

                }

            }
            else {
                var RB1 = document.getElementById("<%=rdbList.ClientID%>");
                var radio = RB1.getElementsByTagName("input");

                for (var i = 0; i < radio.length; i++) {

                    radio[i].disabled = false;


                }
            }
        }--%>



        //        ddlPayemntType
        //        function showBillNotesPopup(BillNo)
        //       {
        //            //alert(BillNo);
        //            document.getElementById('divid4').style.zIndex = 1;
        //            document.getElementById('divid4').style.position = 'absolute';
        //            document.getElementById('divid4').style.left= '300px'; 
        //            document.getElementById('divid4').style.top= '100px';              
        //            document.getElementById('divid4').style.visibility='visible'; 
        //            document.getElementById('frameeditexpanse1').src ='Bill_Sys_Billing_Notes.aspx?BillNo='+BillNo;  
        //            //alert(document.getElementById('frameeditexpanse1').src);
        //            return false;
        //            
        //       }





        function OpenDowloadPanel() {
            document.getElementById("<%= pnl_Download.ClientID%>").style.height = '100px';
            document.getElementById("<%= pnl_Download.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%= pnl_Download.ClientID%>").style.position = "absolute";
            document.getElementById("<%= pnl_Download.ClientID%>").style.top = '200px';
            document.getElementById("<%= pnl_Download.ClientID%>").style.left = '350px';
        }

        function CloseDownloadPanel() {
            document.getElementById("<%= pnl_Download.ClientID%>").style.visibility = 'hidden';
        }


        function ooValidate() {

            var Controls = val_CheckControls();

            checkdate = "1";


            if (Controls != false) {

                var plz = document.getElementById('ctl00_ContentPlaceHolder1_rngDate').style.visibility;
                var checkdate = document.getElementById('ctl00_ContentPlaceHolder1_MaskedEditValidator4').style.visibility;

                if (plz == "visible") {
                    checkdate.value = "0";
                    return false;
                } else if (checkdate == "visible") {
                    checkdate.value = "0";
                    return false;
                }
                else {
                    var radio = document.getElementById('ctl00_ContentPlaceHolder1_rdbList_2');

                    if (radio.checked) {

                        var amount1 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtChequeAmount').value);

                        var amount2 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdnBal').value);


                        if (amount1 > amount2) {
                            if (confirm('Entered check amount is greated than bill amount, do you want to continue?')) {
                                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1'
                                return true;
                            }
                            else {
                                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0'
                                return false;
                            }
                            //                                      if(alert('Entered check amount is greater bill amount'))
                            //                                         {    document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0'
                            //                                             return false;
                            //                                             
                            //                                         }else
                            //                                         {
                            //                                             document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0'
                            //                                             return false;
                            //                                         }
                            //                                        
                        } else {
                            document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1'
                            return true;
                        }


                    } else {
                        document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1'
                        return true;
                    }
                }

            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                return false;
            }

        }
        function val_CheckControls() {
            // alert("hi "+document.getElementById('<%=txtAll.ClientID%>').value);
            checkdate = "1";

            var plz = document.getElementById('ctl00_ContentPlaceHolder1_rngDate').style.visibility;

            var checkdate = document.getElementById('ctl00_ContentPlaceHolder1_MaskedEditValidator4').style.visibility;

            if (plz == "visible") {
                checkdate.value = "0";
                return false;
            } else if (checkdate == "visible") {
                checkdate.value = "0";
                return false;
            }

            if (document.getElementById('<%=txtChequeNumber.ClientID%>').value == '') {
                alert('Please Select Cheque Number');
                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                document.getElementById('<%=txtChequeNumber.ClientID%>').focus();

                return false;
            }

            if (document.getElementById('<%=txtChequeDate.ClientID%>').value == '') {
                alert('Please Select Cheque Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                document.getElementById('<%=txtChequeDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtAll.ClientID%>').value == '') {
                alert('Please Select Paid By.');
                return false;

                document.getElementById('<%=txtAll.ClientID%>').focus();

            }

            if (document.getElementById('<%=txtChequeAmount.ClientID%>').value == '')
            {
                document.getElementById('<%=txtChequeAmount.ClientID%>').value == '0';
            }

            if (document.getElementById('<%=txtChequeAmount.ClientID%>').value == '') {
                alert('Please enter cheque amount.');
                document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                document.getElementById('<%=txtChequeAmount.ClientID%>').focus();
                return false;
            } else {
                var amount11 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtChequeAmount').value);

                var amount21 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdnBal').value);

                var acacmount = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtPrev').value);

                var total = acacmount + amount21;

                if (amount11 > total) {
                    if (confirm('Entered check amount is greated than bill amount, do you want to continue?')) {
                        document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1'
                        return true;
                    }
                    else {
                        document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1'
                        return false;
                    }
                    //                      if(alert('Entered check amount is greater bill amount'))
                    //                      {    
                    //                         document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1';
                    //                          return false;
                    //                      }else
                    //                      {
                    //                        document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                    //                          return false;
                    //                      }
                    //                     
                } else {
                    document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '1';
                    return true;
                }
            }

        }


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
        function confirm_update_bill_status() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_drdUpdateStatus").value == 'NA') {
                alert('Select Status');
                return false;
            }


            var f = document.getElementById("<%=grdBillSearch.ClientID%>");
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
        //        ButtonPacket Function
        function confirm_packet_bill_status() {

            var f = document.getElementById("<%=grdBillSearch.ClientID%>");
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
        //ButtonPacket Function

        function confirm_bill_delete() {

            var f = document.getElementById("<%=grdPaymentTransaction.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') != -1) {
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

        function confirm_Payment_bill_delete() {
            var f = document.getElementById("<%=grdBillSearch.ClientID%>");
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
        //		function confirm_Payment_bill_delete()
        //		 {
        //                alert("hi");		 
        //		                  var statusname="";
        //		             
        //		        var f= document.getElementById("<%=grdBillSearch.ClientID%>");
        //		        var bfFlag = false;	
        //		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
        //		        {		
        //				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
        //		            {
        //		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
        //			            {						
        //			                if(f.getElementsByTagName("input").item(i).checked != false)
        //			                {
        //			                    
        //			                    bfFlag = true;
        //			                    
        ////			                    var str = i+2;
        ////			                     if (str < 10)
        ////		                        {
        ////		                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
        ////		                           
        ////		                              statusname  = statusnameid1.innerHTML;
        ////		                              
        ////		                            if(statusname!="Billed")
        ////		                            {   alert(statusname);
        ////		                                alert("can not delete Bill No, Because Bill status is " + statusname);
        ////		                                return false;
        ////		                            }
        //		                        }else
        //		                            {
        ////		                                var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
        ////		                                    statusname  = statusnameid2.innerHTML;
        ////		                                    if (statusname.toLowerCase() != "billed")
        ////		                                    {     alert(statusname);  
        ////		                                         alert(" can not delete Bill No. ," + statusname );
        ////		                                    return false;
        ////		                                    }
        //			                       }
        //			            }
        //			        }			
        //		        }
        //		        }
        //		        if(bfFlag == false)
        //		        {
        //		            alert('Please select record.');
        //		            return false;
        //		        }
        //		        
        //		 

        //                if(confirm("Are you sure want to Delete?")==true)
        //				{
        //				  
        //				   return true;
        //				}
        //				else
        //				{
        //					return false;
        //				}
        //		}

        function SetDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtToDate.ClientID %>").value = "";
                document.getElementById("<%=txtFromDate.ClientID %>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtToDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtFromDate.ClientID %>").value = quarteryear('startquaeter');
            }
}


function SetVisitDate() {
    getWeek();
    var selValue = document.getElementById("<%=ddlVisit.ClientID %>").value;
    if (selValue == 0) {
        document.getElementById("<%=txtToVisitDate.ClientID %>").value = "";
        document.getElementById("<%=txtVisitDate.ClientID %>").value = "";
    }
    else if (selValue == 1) {
        document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('today');
        document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('today');
    }
    else if (selValue == 2) {
        document.getElementById("<%=txtToVisitDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = quarteryear('startquaeter');
            }
}

function getLastWeek(type) {
    var d = new Date();
    d.setDate(d.getDate() - 7);
    var day = d.getDay();
    d.setDate(d.getDate() - day);
    if (type == 'startweek')
        return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
    if (type == 'endweek') {
        d.setDate(d.getDate() + 6);
        return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
    }
}

function lastmonth(type) {

    var d = new Date();
    var t_date = d.getDate();      // Returns the day of the month
    var t_mon = d.getMonth() + 1;      // Returns the month as a digit
    var t_year = d.getFullYear();

    if (type == 'startmonth') {
        if (t_mon == 1) {
            var y = t_year - 1;
            return ('12/1/' + y);

        }
        else {
            var m = t_mon - 1;
            return (m + '/1/' + t_year);
        }
    }
    else if (type == 'endmonth') {
        if (t_mon == 1) {
            var y = t_year - 1;
            return ('12/31/' + y);
        } else {
            var m = t_mon - 1;
            var d = daysInMonth(t_mon - 1, t_year);
            return (m + '/' + d + '/' + t_year);
        }
    }

}



function quarteryear(type) {
    var d = new Date();
    var t_date = d.getDate();      // Returns the day of the month
    var t_mon = d.getMonth() + 1;      // Returns the month as a digit
    var t_year = d.getFullYear();

    if (type == 'startquaeter') {
        if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
            var y = t_year - 1;
            return ('10/1/' + y);
        }
        else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
            return ('1/1/' + t_year);

        } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
            return ('4/1/' + t_year);


        } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
            return ('7/1/' + t_year);

        }

    } else if (type == 'endquaeter') {
        if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
            //
            var y = t_year - 1;
            return ('12/31/' + y);
        }
        else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
            return ('3/31/' + t_year);

        } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
            return ('6/30/' + t_year);


        } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
            return ('9/30/' + t_year);
        }

    }

}

function lastyear(type) {
    var d = new Date();

    var t_year = d.getFullYear();
    if (type == 'startyear') {
        y = t_year - 1;
        return ('1/1/' + y);
    }
    else if (type == 'endyear') {
        y = t_year - 1;
        return ('12/31/' + y);
    }
}



function getDate(type) {
    var d = new Date();
    var t_date = d.getDate();      // Returns the day of the month
    var t_mon = d.getMonth();      // Returns the month as a digit
    var t_year = d.getFullYear();  // Returns 4 digit year

    var q_start = 0;
    var q_end = 0;
    if ((t_mon + 1) >= 1 && (t_mon + 1) <= 3) {
        q_start = 1;
        q_end = 3;
    }
    else if ((t_mon + 1) >= 4 && (t_mon + 1) <= 6) {
        q_start = 4;
        q_end = 6;
    }
    else if ((t_mon + 1) >= 7 && (t_mon + 1) <= 9) {
        q_start = 7;
        q_end = 9;
    }
    else if ((t_mon + 1) >= 10 && (t_mon + 1) <= 12) {
        q_start = 10;
        q_end = 12;
    }

    if (type == 'today')
        return (getFormattedDate(t_date, t_mon, t_year));
    if (type == 'monthstart')
        return (getFormattedDate(1, t_mon, t_year));
    if (type == 'monthend')
        return (getFormattedDate(daysInMonth(t_mon + 1, t_year), t_mon, t_year));
    if (type == 'quarterstart') {
        return (getFormattedDateForMonth(1, q_start, t_year));
    }
    if (type == 'quarterend') {
        return (getFormattedDateForMonth(daysInMonth(q_end), q_end, t_year));
    }
    if (type == 'yearstart')
        return (getFormattedDate(1, 0, t_year));
    if (type == 'yearend')
        return (getFormattedDate(31, 11, t_year));
}

function daysInMonth(month, year) {
    var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    if (month != 2) return m[month - 1];
    if (year % 4 != 0) return m[1];
    if (year % 100 == 0 && year % 400 != 0) return m[1];
    return m[1] + 1;
}

function getFormattedDate(day, month, year) {
    return '' + (month + 1) + '/' + day + '/' + year;
}

function getFormattedDateForMonth(day, month, year) {
    return '' + (month) + '/' + day + '/' + year;
}

function getWeek(type) {
    var d = new Date();
    var day = d.getDay();
    d.setDate(d.getDate() - day);
    if (type == 'startweek')
        return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
    if (type == 'endweek') {
        d.setDate(d.getDate() + 6);
        return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
    }
}

function OpenPage(BillNo) {

    window.open('Bill_Sys_OpenBill.aspx?bno=' + BillNo, 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

}
function OpenVerificationPage(BillNo) {

    window.open('Bill_sys_openVerificationDocs.aspx?bno=' + BillNo + '&doctype=1', 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

}
function OpenDenialPage(BillNo) {

    window.open('Bill_sys_openVerificationDocs.aspx?bno=' + BillNo + '&doctype=3', 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

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

function showUploadFilePopup() {

    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height = '100px';
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.top = '200px';
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.left = '350px';
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.zIndex = '0';
}
function CloseUploadFilePopup() {
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height = '0px';
    document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';
    //  document.getElementById('ctl00_ContentPlaceHolder1_txtGroupDateofService').value='';      
}
function Clear() {
    //alert("call");
    document.getElementById("<%=txtBillNo.ClientID%>").value = '';
    document.getElementById("<%=txtCasNO.ClientID %>").value = '';
    document.getElementById("<%=txtFromDate.ClientID %>").value = '';
    document.getElementById("<%=txtToDate.ClientID %>").value = '';
    //            document.getElementById("<%=txtVisitDate.ClientID %>").value ='';
    document.getElementById("<%=txtInsuranceCompany.ClientID %>").value = '';
    document.getElementById("<%=txtPatientName.ClientID %>").value = '';
    document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_0").checked = false;
    document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_1").checked = false;
    document.getElementById("ctl00_ContentPlaceHolder1_radioList_0").checked = false;
    document.getElementById("ctl00_ContentPlaceHolder1_radioList_1").checked = false;
    //            document.getElementById("<%=txtToVisitDate.ClientID %>").value ='';

    //            document.getElementById("ctl00_ContentPlaceHolder1_ddlVisit").value=0;
    document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value = 'NA';
    document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
    document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = 'NA';
    var txtamt1 = document.getElementById("<%=txtAmount.ClientID %>");
    //alert(txtamt1);
    if (txtamt1 != null) {
        document.getElementById("<%=txtAmount.ClientID %>").value = '';
        document.getElementById("<%=txtAmount.ClientID %>").style.visibility = 'hidden';
    }

    var txtamt2 = document.getElementById("<%=txtToAmt.ClientID %>");
    if (txtamt2 != null) {
        document.getElementById("<%=txtToAmt.ClientID %>").value = '';
        document.getElementById("<%=txtToAmt.ClientID %>").style.visibility = 'hidden';
    }
    document.getElementById("ctl00_ContentPlaceHolder1_ddlAmount").value = '---Select---';
    document.getElementById("<%=txtPatientName.ClientID %>").value = '';
    if (document.getElementById("ctl00_ContentPlaceHolder1_lblfrom") != null) {
        document.getElementById("ctl00_ContentPlaceHolder1_lblfrom").style.visibility = 'hidden';
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_lblTamt") != null) {
        document.getElementById("ctl00_ContentPlaceHolder1_lblTamt").style.visibility = 'hidden';
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_lblFamt") != null) {
        document.getElementById("ctl00_ContentPlaceHolder1_lblFamt").style.visibility = 'hidden';
    }



}

function Clear1() {
    // alert("call"); 
    document.getElementById("<%=txtBillNo.ClientID%>").value = '';

            document.getElementById("<%=txtFromDate.ClientID %>").value = '';
            document.getElementById("<%=txtToDate.ClientID %>").value = '';
            //            document.getElementById("<%=txtVisitDate.ClientID %>").value ='';

            document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_0").checked = false;
            document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_1").checked = false;
            document.getElementById("ctl00_ContentPlaceHolder1_radioList_0").checked = false;
            document.getElementById("ctl00_ContentPlaceHolder1_radioList_1").checked = false;
            //            document.getElementById("<%=txtToVisitDate.ClientID %>").value ='';

            //            document.getElementById("ctl00_ContentPlaceHolder1_ddlVisit").value=0;
            document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value = 'NA';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = 'NA';
            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
            var txtamt1 = document.getElementById("<%=txtAmount.ClientID %>");
            // alert(txtamt1);
            if (txtamt1 != null) {
                document.getElementById("<%=txtAmount.ClientID %>").value = '';
                document.getElementById("<%=txtAmount.ClientID %>").style.visibility = 'hidden';
            }

            var txtamt2 = document.getElementById("<%=txtToAmt.ClientID %>");
            if (txtamt2 != null) {
                document.getElementById("<%=txtToAmt.ClientID %>").value = '';
                document.getElementById("<%=txtToAmt.ClientID %>").style.visibility = 'hidden';
            }

            document.getElementById("ctl00_ContentPlaceHolder1_ddlAmount").value = '---Select---';
            document.getElementById("<%=txtPatientName.ClientID %>").value = '';
            if (document.getElementById("ctl00_ContentPlaceHolder1_lblfrom") != null) {
                document.getElementById("ctl00_ContentPlaceHolder1_lblfrom").style.visibility = 'hidden';
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_lblTamt") != null) {
                document.getElementById("ctl00_ContentPlaceHolder1_lblTamt").style.visibility = 'hidden';
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_lblFamt") != null) {
                document.getElementById("ctl00_ContentPlaceHolder1_lblFamt").style.visibility = 'hidden';
            }
        }

        function callfunction(szbillno) {
            alert(szbillno);
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdBillSearch.ClientID %>");
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

        function checkAmountBox() {
            var txtamt1 = document.getElementById("<%=txtAmount.ClientID %>");
            var txtamt2 = document.getElementById("<%=txtToAmt.ClientID %>");
            if (document.getElementById("<%=txtAmount.ClientID %>").value != '' && document.getElementById("<%=txtToAmt.ClientID %>").value == '') {

                alert("Enter amount in range.");
                return false;
            } else if (document.getElementById("<%=txtAmount.ClientID %>").value == '' && document.getElementById("<%=txtToAmt.ClientID %>").value != '') {

                alert("Enter amount in range.");
                return false;
            } else {
                return true;
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
        function Hidediv() {

            var div1 = document.getElementById('dvSrch').style.visibility = 'hidden';

        }
        function Showdiv() {
            var div1 = document.getElementById('dvSrch').style.visibility = 'visible';

        }

        function ConfirmAll() {
            if (confirm("Are you want Download All Bills?") == true) {

                return true;
            }
            else {
                return false;
            }
        }




        function CheckSelect() {


            var f = document.getElementById("<%=grdBillSearch.ClientID%>");
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




        function ClosePopup() {
            PatientInfoPop.Hide();
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }
        function OnMoreInfoClick(szBillNo, szCaseId) {
            var url = 'Bill_Sys_DenailPopup.aspx?szBillNo=' + szBillNo + '&szCaseId=' + szCaseId;
            PatientInfoPop.SetContentUrl(url);
            PatientInfoPop.Show();
            $find('modal').hide();

        }

        function OnVerificationInfoClick(szBillNo, szCaseId) {
            var url = 'Bill_Sys_VerificationPopup.aspx?szBillNo=' + szBillNo + '&szCaseId=' + szCaseId;
            VerificationInfoPopup.SetContentUrl(url);
            VerificationInfoPopup.Show();
            $find('modal').hide();

        }
        function CloseVrificationPopup() {
            VerificationInfoPopup.Hide();
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }
        function OnEORInfoClick(szBillNo, szCaseId) {
            var url = 'Bill_Sys_EORPopup.aspx?szBillNo=' + szBillNo + '&szCaseId=' + szCaseId;
            EorInfoPopup.SetContentUrl(url);
            EorInfoPopup.Show();
            $find('modal').hide();

        }
        function CloseEorPopup() {
            EorInfoPopup.Hide();
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
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
                        <td style="height: 100%"></td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%; background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 50%; vertical-align: top;">
                                                    <table style="text-align: left; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 40%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82"
                                                                                onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Bill No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Case No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Bill Status
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtBillNo" runat="server" Width="90%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCasNO" runat="server" Width="90%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="94%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="GET_SELECTED_STATUS_LIST_WITHOUT_VR_VS_DEN_FBP" Procedure_Name="SP_MST_BILL_STATUS_BILL_SEARCH"
                                                                                            Selected_Text="---Select---" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:CheckBoxList AutoPostBack="false" RepeatColumns="2" RepeatDirection="Horizontal"
                                                                                            runat="server" ID="checkboxlst" Visible="False">
                                                                                            <asp:ListItem Text="Denial" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Paid in Full" Value="3"></asp:ListItem>
                                                                                        </asp:CheckBoxList>
                                                                                    </td>
                                                                                    <td colspan="2" style="text-align: left">
                                                                                        <asp:RadioButtonList ID="radioList" runat="server" RepeatDirection="Horizontal" Visible="False">
                                                                                            <asp:ListItem Text="Verification Sent" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Verification Received" Value="2"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Billing Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="90%">
                                                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Today</asp:ListItem>
                                                                                            <asp:ListItem Value="2">This Week</asp:ListItem>
                                                                                            <asp:ListItem Value="3">This Month</asp:ListItem>
                                                                                            <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                                                                            <asp:ListItem Value="5">This Year</asp:ListItem>
                                                                                            <asp:ListItem Value="6">Last Week</asp:ListItem>
                                                                                            <asp:ListItem Value="7">Last Month</asp:ListItem>
                                                                                            <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                                                                            <asp:ListItem Value="8">Last Year</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="76%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                MaxLength="10" Width="70%"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="60%">
                                                                                        </ajaxcontrol:MaskedEditValidator>
                                                                                    </td>
                                                                                    <td>
                                                                                        <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToDate" CssClass="search-input" EmptyValueMessage="Date is required"
                                                                                            InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"
                                                                                            Visible="true" Width="60%">
                                                                                        </ajaxcontrol:MaskedEditValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <%--          <tr visible="false">
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Visit Date
                                                                                    </td>
                                                                                    <td visible="false" class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" visible="false">
                                                                                        To
                                                                                    </td>
                                                                                </tr>--%>
                                                                                <tr visible="false">
                                                                                    <td visible="false">
                                                                                        <asp:DropDownList ID="ddlVisit" runat="Server" Width="90%" Visible="false">
                                                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Today</asp:ListItem>
                                                                                            <asp:ListItem Value="2">This Week</asp:ListItem>
                                                                                            <asp:ListItem Value="3">This Month</asp:ListItem>
                                                                                            <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                                                                            <asp:ListItem Value="5">This Year</asp:ListItem>
                                                                                            <asp:ListItem Value="6">Last Week</asp:ListItem>
                                                                                            <asp:ListItem Value="7">Last Month</asp:ListItem>
                                                                                            <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                                                                            <asp:ListItem Value="8">Last Year</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td visible="false">
                                                                                        <asp:TextBox ID="txtVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="76%" Visible="false"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgVisit" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                                                        <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="false">
                                                                                        </ajaxcontrol:MaskedEditValidator>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td visible="false">
                                                                                        <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%" Visible="false"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgVisite1" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                                                        <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="false">
                                                                                        </ajaxcontrol:MaskedEditValidator>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Patient Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Specialty
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch1">Case Type
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <%--<asp:TextBox ID="txtPatientName" runat="server" Width="90%"></asp:TextBox>--%>
                                                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="90%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                                        <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="0%" Selected_Text="--- Select ---"
                                                                                            Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                                                            AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged"
                                                                                            Visible="false"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="96%" Selected_Text="---Select---"
                                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Amount
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch1" colspan="2">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblfrom" runat="server" Text="Between" Font-Size="12px" Font-Bold="true"
                                                                                                    Visible="false" Width="250px"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <%-- <td class="td-widget-bc-search-desc-ch">
                                                                                         <ajaxToolkit:UpdatePanel ID="UpdatePanelforlabale" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblTo" runat="server" Text="To" Font-Size="12px" Font-Bold="true"
                                                                                                    Visible="false"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                            </ajaxToolkit:UpdatePanel>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:UpdatePanel ID="UpdatePanelamount" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="ddlAmount" runat="server" Width="90%" OnSelectedIndexChanged="ddlAmount_SelectedIndexChanged"
                                                                                                    AutoPostBack="true">
                                                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                                                    <asp:ListItem Value="0">Range</asp:ListItem>
                                                                                                    <asp:ListItem Value=">">Greater Than</asp:ListItem>
                                                                                                    <asp:ListItem Value="<">Less Than</asp:ListItem>
                                                                                                    <asp:ListItem Value="=">Equal To</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:UpdatePanel ID="UpdatePanelfortxtamount" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblFamt" runat="server" Font-Bold="true" CssClass="lbl" Text="$" Width="3%"
                                                                                                    Visible="False"></asp:Label>
                                                                                                <asp:TextBox ID="txtAmount" runat="server" Width="80%" Visible="true"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <asp:UpdatePanel ID="UpdatePanelfortxtToamount" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblTamt" runat="server" Font-Bold="true" CssClass="lbl" Text="$" Width="3%"
                                                                                                    Visible="False"></asp:Label>
                                                                                                <asp:TextBox ID="txtToAmt" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch1">Insurance
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch3">
                                                                                        <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="90%" autocomplete="off"
                                                                                            CssClass="search-input"></asp:TextBox>
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="50%" Selected_Text="---Select---"
                                                                                            Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST" Connection_Key="Connection_String"
                                                                                            AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlInsurance_extendDropDown_SelectedIndexChanged"
                                                                                            Visible="false" AccessKeyVisible="false"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                                                    DisplayAfter="10">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                                            runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Loading...
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                                &nbsp;
                                                                                                <asp:Button Style="width: 80px" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                                                                    Text="Search"></asp:Button>
                                                                                                &nbsp;
                                                                                                <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                                                                    runat="server" visible="false" />
                                                                                                <input style="width: 80px" id="btnClear2" onclick="Clear1();" type="button" value="Clear"
                                                                                                    runat="server" visible="false" />
                                                                                                <input id="btn_Download_Bill" type="button" onclick="OpenDowloadPanel()" style="width: 100px;"
                                                                                                    value="Download" runat="server" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <%-- <td>
                                                                                    
                                                                                    </td>--%>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 40%; vertical-align: top" class="td-widget-lf-holder-ch" id="td2"
                                                                            runat="server">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 100%;">
                                                                                        <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" BorderColor="#DEBA84"
                                                                                            RepeatColumns="1" Width="100%" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <table style="width: 100%; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tr style="background-color: #B5DF82">
                                                                                                        <td class="td-widget-lf-desc-ch">Name
                                                                                                        </td>
                                                                                                        <td class="td-widget-lf-desc-ch">Insurance Company
                                                                                                        </td>
                                                                                                        <td class="td-widget-lf-desc-ch">Accident Date
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center" class="lbl">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                                                                                        </td>
                                                                                                        <td align="center">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                                                                                        </td>
                                                                                                        <td align="center">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:dd MMM yyyy}")%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ItemTemplate>
                                                                                        </asp:DataList>
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
                                <tr>
                                    <td style="width: 100%">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td style="width: 20%" align="left">
                                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:UpdateProgress ID="UpdateProgress1233" runat="server" AssociatedUpdatePanelID="UpdatePanel13"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="Div1" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                runat="Server">
                                                                                <asp:Image ID="img1234" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 80%" align="right">
                                                            <asp:RadioButtonList ID="rdoDownload" Width="40%" runat="server" RepeatLayout="Flow"
                                                                Font-Size="14px" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="0">By Provider</asp:ListItem>
                                                                <asp:ListItem Value="1">By Specialty</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            &nbsp;
                                                            <asp:Button ID="btn_BillPacket" runat="server" Text="Bill Packet" OnClick="btn_BillPacket_Click"></asp:Button>
                                                            <asp:Button ID="btn_PacketDocument" runat="server" Text="Packet Document" OnClick="btn_PacketDocument_Click"></asp:Button>
                                                            <asp:Button ID="btn_Both" runat="server" Text="Both" OnClick="btn_Both_Click"></asp:Button>
                                                            <asp:Button ID="btn_Download_id" runat="server" Text="Download" OnClick="btn_Download_id_Click"></asp:Button>
                                                            <asp:Button ID="btn_Download_All" runat="server" Text="Download All" OnClick="btn_Download_All_Click"></asp:Button>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: auto;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div style="width: 100%" id="dvSrch">
                                                    <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid; width: 100%; border-bottom: #b5df82 1px solid; height: auto"
                                                        cellspacing="0"
                                                        cellpadding="0" align="left">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                                                                    colspan="6" height="28">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Bills"></asp:Label>
                                                                    <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                            DisplayAfter="10">
                                                            <ProgressTemplate>
                                                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                    runat="Server">
                                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                        Height="25px" Width="24px"></asp:Image>
                                                                    Loading...</div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>--%>
                                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                runat="Server">
                                                                                <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTotalBillAmount" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPdAmount" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOutSratingAmount" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalWOF" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">
                                                                    <%-- <asp:UpdatePanel ID="UpdatePaneamount" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <%--  </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                                </td>
                                                                <td class="lbl">
                                                                    <%--    <asp:UpdatePanel ID="UpdatePaneamount1" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                                </td>
                                                                <td class="lbl">
                                                                    <asp:Label ID="lblbillstatus" runat="server" Font-Bold="True" Font-Size="Small" Text="Bill"
                                                                        Status=" "></asp:Label>
                                                                    <extddl:ExtendedDropDownList ID="drdUpdateStatus" runat="server" Width="125px" Selected_Text="---Select---"
                                                                        Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL"
                                                                        Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td colspan="2">
                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <asp:Button ID="btnUpdateStatus" OnClick="btnUpdateStatus_Click" runat="server" Text="Update Status"></asp:Button>
                                                                    &nbsp;
                                                                    <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete"></asp:Button>
                                                                    <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                                </td>
                                                                <%--    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 1017px" colspan="5">
                                                                    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <table style="vertical-align: middle; width: 100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Search:"></asp:Label>
                                                                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                        CssClass="search-input">
                                                                                    </gridsearch:XGridSearchTextBox>
                                                                                </td>
                                                                                <td style="vertical-align: middle; width: 30%" align="left"></td>
                                                                                <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdBillSearch.RecordCount %>| Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                        Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Height="148px" Width="100%"
                                                                        CssClass="mGrid" OnRowCommand="grdBillSearch_RowCommand" AllowSorting="true"
                                                                        PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="SZ_BILL_NUMBER,SZ_BILL_STATUS_CODE,SZ_CASE_ID,FLT_BILL_AMOUNT,FLT_BALANCE,PROC_DATE,BT_TRANSFER,SZ_CASE_NO,SZ_ABBRIVATION_ID,SZ_BILL_STATUS_NAME,bt_denial,bt_verification,bt_eor,SPECIALITY_ID,SZ_BILL_NOTES,bt_verDocs,bt_denialDocs,SPECIALITY,SZ_INSURANCE_ID"
                                                                        XGridKey="Bii_Sys_BillSearch" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                        ShowExcelTableBorder="true" ExcelFileNamePrefix="BillSearch" MouseOverColor="0, 153, 153"
                                                                        AutoGenerateColumns="false" ExportToExcelColumnNames="Bill#,Case#,Specialty,Visit Date,Bill Date,Status,Bill Amount,Paid,Balance,Provider Name,Assign LawFirm"
                                                                        ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SPECIALITY,PROC_DATE,DT_BILL_DATE,SZ_BILL_STATUS_NAME,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,sz_provider_name,AssignLawFirm">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <%--  0--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                Visible="false" HeaderText="Case ID" DataField="SZ_CASE_ID" />
                                                                            <%-- 1--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                Visible="false" HeaderText="BillNo" DataField="SZ_BILL_NUMBER" />
                                                                            <%--  2--%>
                                                                            <xlink:XGridHyperlinkField SortExpression="bill_sort" HeaderText="Bill#" DataNavigateUrlFields="SZ_CASE_ID,SZ_BILL_NUMBER"
                                                                                DataNavigateUrlFormatString="Bill_Sys_BillTransaction.aspx?Type=Search&CaseID={0}&bno={1}"
                                                                                DataTextField="SZ_BILL_NUMBER">
                                                                            </xlink:XGridHyperlinkField>
                                                                            <%--CAST(MCM.SZ_CASE_NO as int)"--%>
                                                                            <%--  3--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                SortExpression="SZ_CASE_NO" HeaderText="Case#" DataField="SZ_CASE_NO" />
                                                                            <%--  4--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                SortExpression="SPECIALITY" HeaderText="Specialty" DataField="SPECIALITY" />
                                                                            <%--  5--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                SortExpression="date_sort" HeaderText="Visit Date" DataField="PROC_DATE" />
                                                                            <%--  6--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                DataFormatString="{0:MM/dd/yyyy}" SortExpression="DT_BILL_DATE" HeaderText="Bill Date"
                                                                                DataField="DT_BILL_DATE" />
                                                                            <%--  7--%>
                                                                            <%--<asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                            SortExpression="MBS.SZ_BILL_STATUS_NAME" headertext="Status" DataField="SZ_BILL_STATUS_NAME" />--%>
                                                                            <asp:TemplateField HeaderText="Bill Status" SortExpression="SZ_BILL_STATUS_NAME">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--  8 --%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                                SortExpression="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:C}"
                                                                                DataField="FLT_WRITE_OFF" Visible="true" />
                                                                            <%--  9 --%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                                SortExpression="CAST(FLT_BILL_AMOUNT  as float)" HeaderText="Bill Amount" DataFormatString="{0:C}"
                                                                                DataField="FLT_BILL_AMOUNT" />
                                                                            <%--  10--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                                SortExpression="PAID_AMOUNT" HeaderText="Paid" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                                                            <%--  11--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                                SortExpression="CAST(FLT_BALANCE as float)" HeaderText="Outstanding" DataFormatString="{0:C}"
                                                                                DataField="FLT_BALANCE" />
                                                                            <%--  12--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                SortExpression="ltrim(SZ_INSURANCE_NAME)" HeaderText="Insurance" DataField="SZ_INSURANCE_NAME" />
                                                                            <%--  13--%>
                                                                            
                                                                            <asp:TemplateField HeaderText="Make   Payment" HeaderStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkMakePayment" runat="server" Text="Make Payment" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        CommandName="Make Payment" CausesValidation="false"> </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <%--  14--%>
                                                                            
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <%--   <asp:LinkButton ID="c421" runat="server" Text="link" OnClientClick='<%# "callfunction(" +Eval("SZ_BILL_NUMBER") + "\");" %>'></asp:LinkButton> --%>
                                                                                    <asp:HyperLink ID="C42" runat="server" Target="_blank" NavigateUrl='<%# "../Bill_Sys_PatientInformationC4_2.aspx?BillNumber=" + DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER") %>'>Edit W.C. 4.2 </asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--  15--%>
                                                                            
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="C43" runat="server" Target="_blank" NavigateUrl='<%# "../Bill_Sys_PatientInformationC4_3.aspx?BillNumber=" + DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER") %>'>Edit W.C. 4.3 </asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--  16--%>
                                                                            
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                HeaderText="View POM" DataField="SZ_POM_PATH" Visible="false" />
                                                                            <%--  17--%>
                                                                           
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="OT_PT" runat="server" Text='Edit W.C. OT-PT' CommandName="" OnClick='<%# "showOTPTinfoPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\");" %>'></asp:LinkButton>
                                                                                    <%--<a id="OT_PT" href="#"  onclick='<%# "showOTPTinfoPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\");" %>' >Edit W.C. OT-PT</a>
                                                                                    --%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <%--  18--%>
                                                                            
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <a id="lnkBill" href="#" onclick="javascript:OpenPage('<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%> ');">
                                                                                        <asp:Label ID="lblBill" runat="server" Text="View Bill"></asp:Label></a> <a id="lnkVerification"
                                                                                            href="#" onclick="javascript:OpenVerificationPage('<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%> ');">
                                                                                            <asp:Label ID="lblVerification" runat="server" Text="| Verification" Visible="false"></asp:Label></a>
                                                                                    <a id="lnkDenial" href="#" onclick="javascript:OpenDenialPage('<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%> ');">
                                                                                        <asp:Label ID="lblDenial" runat="server" Text="| Denial" Visible="false"></asp:Label></a>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <%-- <asp:TemplateField HeaderText="">
                                                                            <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                                            <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>--%>
                                                                            <%--  19--%>
                                                                            
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                Visible="false" HeaderText="bill status code" DataField="SZ_BILL_STATUS_CODE" />
                                                                            <%--  20--%>
                                                                            <%-- change index 19 to 20--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="Bill Amount" DataField="FLT_BILL_AMOUNT" Visible="false" />
                                                                            <%--  21--%>
                                                                            <%-- change index 20 to 21--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="Balance" Visible="false" DataField="FLT_BALANCE" />
                                                                            <%--  22--%>
                                                                            <%-- change index 21 to 22--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="Balance" Visible="false" DataField="BT_TRANSFER" />
                                                                            <%--  23--%>
                                                                            <%-- change index 22 to 23--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="CaseType" Visible="false" DataField="SZ_ABBRIVATION_ID" />
                                                                            <%-- 24--%>
                                                                            <%-- change index 23 to 24--%>
                                                                            <asp:TemplateField HeaderText="Verification" Visible="true" SortExpression="bt_verification">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="PLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--  25--%>
                                                                            <%-- change index 24 to 25--%>
                                                                            <asp:TemplateField HeaderText="Denials" Visible="true" SortExpression="bt_denial">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="DenialPLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <%--  26--%>
                                                                            <asp:TemplateField HeaderText="EOR" Visible="true" SortExpression="bt_eor">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="EorPLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkEM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="EorMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--27--%>
                                                                            <%-- change index 25 to 26--%>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkBillNotes" runat="server" Text='Bill Notes' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        CommandName="Bill Notes"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%-- Avinash Start 12/4/13--%>
                                                                            <%--28--%>
                                                                            <%-- change index 26 to 27--%>
                                                                            <asp:TemplateField Visible="true" HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <%--<a href="javascript:void(0);" onclick="showPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>')">
                                                                                          MG2</a>--%>
                                                                                    <asp:LinkButton ID="lnkMG2" runat="server" Text='MG2' CommandName="" OnClick='<%# "showPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\""+ ", " + "\""+ Eval("SPECIALITY") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\");" %>'>
                                                                                  
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%-- Avinash End 12/4/13--%>
                                                                            <%-- Nirmal Start 12/4/13--%>
                                                                            <%--29--%>
                                                                            <%-- change index 26 to 27--%>
                                                                            <asp:TemplateField Visible="true" HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <%--<a href="javascript:void(0);" onclick="showPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>')">
                                                                                          MG2</a>--%>
                                                                                    <asp:LinkButton ID="lnkHP1" runat="server" Text='HP1' CommandName="" OnClick='<%# "showPopup2(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\""+ ", " + "\""+ Eval("SPECIALITY") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\");" %>'>
                                                                                  
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <%--30--%>
                                                                            <asp:TemplateField Visible="true" HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkHPJ1" runat="server" Text="HPJ1" 
                                                                                        OnClick ='<%# "showHPJ1form(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\""+ ", " + "\""+ Eval("SPECIALITY") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\");" %>'>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <%--31--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                               HeaderText="Assign LawFirm" DataField="AssignLawFirm" />
                                                                              <%--32--%>

                                                                            <asp:TemplateField HeaderText="">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                                        ToolTip="Select All" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--33--%>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%" align="left">
                                                                                            <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                                                <asp:GridView ID="grdVerification" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
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
                                                                            <%--  34--%>
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
                                                                            <%--  35--%>
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
                                                                            <%--  36--%>
                                                                            <asp:BoundField DataField="bt_denial" ItemStyle-Width="85px" HeaderText="bt_denial"
                                                                                Visible="false">
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <%--  37--%>
                                                                            <asp:BoundField DataField="bt_verification" Visible="false" ItemStyle-Width="105px"
                                                                                HeaderText="bt_verification">
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <%--  38--%>
                                                                            <asp:BoundField DataField="sz_provider_name" Visible="false" ItemStyle-Width="105px"
                                                                                HeaderText="sz_provider_name">
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <%--  33--%>
                                                                            <%-- change index 31 to 32--%>
                                                                            <%--<asp:TemplateField HeaderText="">
                                                                                <itemtemplate>
                                                                           <asp:LinkButton ID="lnkBillNotes" runat="server" Text='Bill Notes' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                  CommandName="Bill Notes"></asp:LinkButton>
                                                                                </itemtemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <%--  39--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="SPECIALITY_ID" Visible="false" DataField="SPECIALITY_ID" />
                                                                            <%--40--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="SZ_BILL_NOTES" Visible="false" DataField="SZ_BILL_NOTES" />
                                                                            <%--38--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="SZ_INSURANCE_ID" Visible="false" DataField="SZ_INSURANCE_ID" />
                                                                        </Columns>
                                                                    </xgrid:XGridViewControl>
                                                                    <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px"></td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    <asp:TextBox ID="txtRange" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true"></ajaxcontrol:MaskedEditExtender>
                                                    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true"></ajaxcontrol:MaskedEditExtender>
                                                    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxcontrol:MaskedEditExtender>
                                                    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxcontrol:MaskedEditExtender>
                                                    <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtVisitDate"
                                                        PopupButtonID="imgVisit" />
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToVisitDate"
                                                        PopupButtonID="imgVisit1" />
                                                    <asp:TextBox ID="txtGroupId" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtBillStatusID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtFromAmount" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtxToAmount" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtden" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtvs" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtvr" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtfbp" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtIsBillNotes" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <%--<td style="width: 10px">
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <ajaxcontrol:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det"
                DropShadow="false" PopupControlID="pnlSaveDescription" BehaviorID="modal" PopupDragHandleControlID="pnlSaveDescriptionHeader">
            </ajaxcontrol:ModalPopupExtender>
            <ajaxcontrol:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lbnTest"
                DropShadow="false" PopupControlID="pnlSaveBillnotes" BehaviorID="modal1" PopupDragHandleControlID="Div2">
            </ajaxcontrol:ModalPopupExtender>
            <asp:Panel Style="display: none; width: 800px; height: 300px; background-color: #dbe6fa;"
                ID="pnlSaveDescription" runat="server">
                <%-- <table style="width: 100%; height: 100%" id="Table1" cellspacing="0" cellpadding="0"
                    border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; vertical-align: top;
                                width: 100%; padding-top: 0px; height: 100%">--%>
                <table style="width: 100%" id="MainBodyTable" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="height: 100%" class="LeftCenter"></td>
                            <td style="width: 700px" class="Center" valign="top">
                                <%--  <table  style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0"; >--%>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; background-color: White;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; background-color: White;" class="TDPart">
                                                <table style="width: 800px" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <div style="left: 0px; width: 830px; position: absolute; top: 0px; height: 18px; background-color: #B5DF82; text-align: left"
                                                                    id="pnlSaveDescriptionHeader">
                                                                    <b>Make Payment</b>
                                                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;">
                                                                        <asp:Button ID="btnClose" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                            Text="X" OnClientClick="$find('modal').hide(); return false;"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td style="width: 10%; height: 100%" class="ContentLabel">
                                                                <%--<asp:Button ID="btnClose" runat="server" Height="21px" Width="50px" CssClass="Buttons"
                                                                                        Text="Close" OnClientClick="$find('modal').hide(); return false;"></asp:Button>--%>
                                                                <%-- <asp:Label ID="lblHeader" runat="server" Width="173px" CssClass="lbl" Text="Make Payment"
                                                                                        Font-Bold="True">
                                                                                    </asp:Label>--%>
                                                                <%-- </div>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <div class="lbl">
                                                                    <asp:Label ID="lblMsg" runat="server" CssClass="message-text" Visible="false"></asp:Label>
                                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                                        <UserMessage:MessageControl ID="usrMessage1" runat="server" />
                                                                        <asp:UpdateProgress ID="UpdatePanel15" runat="server" AssociatedUpdatePanelID="UpdatePanel11"
                                                                            DisplayAfter="10">
                                                                            <ProgressTemplate>
                                                                                <div id="Div10" runat="Server" class="PageUpdateProgress" style="text-align: center; vertical-align: bottom;">
                                                                                    <asp:Image ID="img40" runat="server" AlternateText="Loading....." Height="25px" ImageUrl="~/Images/rotation.gif"
                                                                                        Width="24px" />
                                                                                    Loading...
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 18px; text-align: right" colspan="4">
                                                                <asp:Button ID="Button1" OnClick="btnPaymentDelete_Click" runat="server" Text="Delete"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%" class="SectionDevider" colspan="4">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DataGrid ID="grdPaymentTransaction" runat="server" Width="800px" CssClass="GridTable"
                                                                                AutoGenerateColumns="false" OnItemCommand="grdPaymentTransaction_ItemCommand">
                                                                                <FooterStyle />
                                                                                <SelectedItemStyle />
                                                                                <PagerStyle />
                                                                                <AlternatingItemStyle />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_PAYMENT_ID" HeaderText="Payment ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="SZ_BILL_ID" HeaderText="Bill Number"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_CHECK_NUMBER" HeaderText="Check Number"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="DT_PAYMENT_DATE" HeaderText="Posted Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Check Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="FLT_CHECK_AMOUNT" HeaderText="Check Amount" DataFormatString="{0:C}"
                                                                                        ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <%-- <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:C}"
                                                                                        ItemStyle-HorizontalAlign="right"></asp:BoundColumn>--%>
                                                                                    <asp:BoundColumn DataField="SZ_PAID_TYPE" HeaderText="Payment Type" Visible="True"></asp:BoundColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="mn_received_as_interest" HeaderText="Interest" DataFormatString="{0:C}"
                                                                                        ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMMENT" Visible="true" HeaderText="Description"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:TemplateColumn HeaderText="Check">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan"
                                                                                                Text="Scan" Visible="false" OnClick="lnkscan_Click"></asp:LinkButton><asp:LinkButton ID="lnkuplaod"
                                                                                                    runat="server" CausesValidation="false" CommandName="Upload" Text="Upload" Visible="false" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                                                            <a id="caseDetailScan" href="#" runat="server" onclick='<%# "paymentscan(" + "\""+ Eval("SZ_CASE_ID")+"\""+"," + "\""+ Eval("SZ_SPECIALITY_ID")+"\""+",\""  + Eval("NodeType")  +"\""+ ",\""  + Eval("SZ_BILL_ID")  +"\""+ ",\""  + Eval("SZ_PAYMENT_ID")  +"\""+ ");"%>'
                                                                                                title="Scan/Upload">Scan/Upload</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="SZ_PAYMENT_DOCUMENT_LINK" HeaderText="View" Visible="True"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:TemplateColumn HeaderText="Denial">
                                                                                        <ItemTemplate>
                                                                                            <a href="javascript:void(0);" onclick="OnMoreInfoClick('<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>')">Enter Denial</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Verification">
                                                                                        <ItemTemplate>
                                                                                            <a href="javascript:void(0);" onclick="OnVerificationInfoClick('<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>')">Enter Verification</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="EOR">
                                                                                        <ItemTemplate>
                                                                                            <a href="javascript:void(0);" onclick="OnEORInfoClick('<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>')">Enter EOR</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Delete">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--15--%>
                                                                                    <asp:BoundColumn DataField="SZ_PAYMENT_TYPE" HeaderText="SZ_PAYMENT_TYPE" Visible="false"></asp:BoundColumn>
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="SZ_PAYMENT_VALUE" HeaderText="SZ_PAYMENT_VALUE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="GridHeader1" />
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;" class="GridHeader1" colspan="4">
                                                                <b>Make Payment</b>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="width: 100%" class="ContentTable" cellspacing="2" cellpadding="3" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: center" class="ContentLabel" colspan="4" rowspan="1">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>BillNo: </b>
                                                            </td>
                                                            <td style="width: 35%" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblBillNo" runat="server" Font-Size="X-Small" Font-Bold="True">
                                                                </asp:Label>
                                                            </td>
                                                            <td style="width: 25%" class="ContentLabel">
                                                                <b>Visit Date : </b>
                                                            </td>
                                                            <td style="width: 60%" align="left">
                                                                <asp:Label ID="lblVisitDate" runat="server" Font-Size="X-Small" Font-Bold="True">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Balance: </b>
                                                            </td>
                                                            <td style="width: 35%" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $
                                                                <asp:Label ID="lblBalance" runat="server" Font-Size="X-Small" Font-Bold="True">
                                                                </asp:Label>
                                                            </td>
                                                            <td style="width: 25%" class="ContentLabel">
                                                                <b>Posted Date : </b>
                                                            </td>
                                                            <td style="width: 35%" align="left">
                                                                <asp:Label ID="lblPosteddate" runat="server" Font-Size="X-Small" Font-Bold="True">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Check Date : </b>
                                                            </td>
                                                            <td style="width: 35%">&nbsp;&nbsp;<asp:TextBox ID="txtChequeDate" onkeypress="return CheckForInteger(event,'/')"
                                                                runat="server" Width="76px" MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgbtnChequeDate"
                                                                    runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                <%-- <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>--%>
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 25%" class="ContentLabel">
                                                                <b>Check Number : </b>&nbsp;
                                                            </td>
                                                            <td style="width: 35%">
                                                                <asp:TextBox ID="txtChequeNumber" runat="server" Width="78%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                                    ControlToValidate="txtChequeDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                    IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxcontrol:MaskedEditValidator>
                                                                <%--<ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" Visible="true"
                                                                                    TooltipMessage="Input a Date" IsValidEmpty="True" InvalidValueMessage="Date is invalid"
                                                                                    EmptyValueMessage="Date is required" ControlToValidate="txtChequeDate" ControlExtender="MaskedEditExtender1"
                                                                                    ValidationGroup="valDate"></ajaxToolkit:MaskedEditValidator>--%>
                                                                <asp:RangeValidator ID="rngDate" runat="server" ControlToValidate="txtChequeDate"
                                                                    Type="Date" MinimumValue="01/01/1901" MaximumValue="12/31/2099" ErrorMessage="Enter a valid date "></asp:RangeValidator>
                                                            </td>
                                                        </tr>

                                                                     <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Check Amount : </b>
                                                            </td>
                                                            <td style="width: 35%">&nbsp;
                                                                                                                                                       <asp:TextBox ID="txtChequeAmount" runat="server" Width="58%" MaxLength="50"></asp:TextBox>
                                                                <asp:Label ID="lbldollar" runat="server" CssClass="lbl" Text="$"></asp:Label>
                                                            </td>
                                                            <td style="width: 100%; text-align: left" class="ContentLabel" colspan="2">
                                                                          <asp:CheckBox ID="chkTypeWriteOff" Text="Write off and keep balance for litigation"
                                                                    runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Interest Amount : </b>
                                                            </td>
                                                            <td style="width: 35%">&nbsp;
                                                                <asp:TextBox ID="txtInterestAmount" runat="server" Width="58%" MaxLength="50"></asp:TextBox>
                                                                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="$"></asp:Label>
                                                            </td>
                                                            <td style="width: 100%; text-align: left" class="ContentLabel" colspan="2">
                                                                <asp:RadioButtonList ID="rdbList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Litigation"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Write-Off"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="Save" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Paid By: </b>
                                                            </td>
                                                            <td>&nbsp;
                                                                <asp:DropDownList ID="ddlAll" runat="Server" Width="60%" OnSelectedIndexChanged="ddlAll_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <%--<asp:ListItem Value="0">Carrier</asp:ListItem>
                                                                    <asp:ListItem Value="1">Lawfirm</asp:ListItem>
                                                                    <asp:ListItem Value="2">Others</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 35%" colspan="2">
                                                                <asp:TextBox ID="txtAll" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Description : </b>
                                                            </td>
                                                            <td colspan="3">&nbsp;&nbsp;<asp:TextBox ID="txtDescription" runat="server" Height="60px" Width="309px"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tdLitti_Write" runat="server" visible="false">
                                                            <td style="height: 22px" class="ContentLabel" colspan="4">
                                                                <asp:Button ID="btnLitigation" runat="server" Width="80px" CssClass="Buttons" Text="Litigation"></asp:Button><asp:Button ID="btnWriteoff" runat="server" Width="80px" CssClass="Buttons"
                                                                    Text="Write off"></asp:Button><asp:Button ID="btnCancel" runat="server" Width="80px"
                                                                        CssClass="Buttons" Text="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr id="tdAddUpdate" runat="server">
                                                            <td class="ContentLabel" colspan="4">
                                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Text="Add"></asp:Button>
                                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="80px"
                                                                    Text="Update"></asp:Button>
                                                                <asp:Button ID="Button2" OnClick="btnCancel_Click" runat="server" Width="80px" Text="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:TextBox ID="txtPaymentvalue" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtPaymentDate" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtCompanyID1" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtPaymentID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtPaymentType" runat="server" Visible="false" Text="0"></asp:TextBox>
                                <asp:TextBox ID="txtID" runat="server" Visible="false" Text="0"></asp:TextBox>
                                <asp:TextBox ID="txtPrev" runat="server" Style="visibility: hidden" Text="0"></asp:TextBox>
                                <asp:TextBox ID="txtBal" runat="server" Visible="false" Text="0"></asp:TextBox>
                                <asp:HiddenField ID="hdnBal" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnBillStatusid" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnDenialflag" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="checkdate" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdlupdateamount" runat="server"></asp:HiddenField>
                                <input id="hiddenconfirmBox" type="hidden" name="hiddenconfirmBox" />
                                <input id="checkdate" type="hidden" name="hiddenconfirmBox" />
                                <asp:TextBox ID="txthdcheckamount" runat="server" Width="0%" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td visible="false"></td>
                            <td style="width: 446px">
                                <ajaxcontrol:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtChequeDate"
                                    PopupButtonID="imgbtnChequeDate"></ajaxcontrol:CalendarExtender>
                                <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender6" runat="server" AutoComplete="true"
                                    PromptCharacter="_" TargetControlID="txtChequeDate" MaskType="Date" Mask="99/99/9999"></ajaxcontrol:MaskedEditExtender>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <%-- </td>
                        </tr>
                    </tbody>
                </table>--%>
            </asp:Panel>
            <asp:Panel Style="display: none; width: 350px; height: 200px; background-color: white; border: 1px solid #B5DF82;"
                ID="pnlSaveBillnotes" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div style="left: 0px; width: 350px; position: absolute; top: 0px; height: 18px; background-color: #B5DF82; text-align: left"
                                                id="Div2">
                                                <b>Bill Notes</b>
                                                <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82; border: 1px solid #B5DF82;">
                                                    <asp:Button ID="btnbillnotesclose" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                        Text="X" OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <UserMessage:MessageControl runat="server" ID="MessageControl1"></UserMessage:MessageControl>
                                                    <asp:UpdateProgress ID="UpdatePanel19" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel11">
                                                        <ProgressTemplate>
                                                            <div id="Div3" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img46" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ContentLabel">
                                            <b>Bill No: </b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNotesBillno" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 18px; width: 40%; text-align: left;" class="ContentLabel">
                                            <b>Bill Notes</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBillNotes" runat="server" Width="180px" Height="70px" TextMode="multiLine">
                                
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnnillsave" runat="server" Text="Save" OnClick="btnBillSave_Click"
                                                Width="24%" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <%-- <table style="width: 450px" border="0">
               
                
                </table>--%>
                <%-- <table>
                            <tr>
                                <td style="width: 20%" class="ContentLabel">
                                    <b>BillNo: </b>
                                </td>
                                <td>
                                    <asp:Label ID="lblbillnotesBillno" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 18px; width: 40%; text-align: left;">
                                    Bill Notes
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBillNotes" runat="server" Width="100%" Height="100%" TextMode="MultiLine">
                                
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnBillnotessave" runat="server" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>--%>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
                <asp:LinkButton ID="lbnTest" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
                <div style="position: relative; text-align: right; background-color: #8babe4;">
                    <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
                </div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                    <tr>
                        <td style="width: 98%;" valign="top">
                            <table border="0" class="ContentTable" style="width: 100%">
                                <tr>
                                    <td>Upload Report :
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuUploadReport" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                            OnClick="btnUploadFile_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadFile" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel Style="border-right: buttonface 1px solid; border-top: buttonface 1px solid; visibility: hidden; border-left: buttonface 1px solid; width: 450px; border-bottom: buttonface 1px solid; height: 0px; background-color: white"
                ID="pnl_Download" runat="server">
                <div style="position: relative; background-color: #8babe4; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="CloseDownloadPanel();">X</a>
                </div>
                <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 98%" valign="top">
                                <table style="width: 100%" class="ContentTable" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 149px" colspan="2">&nbsp;
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="243px" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True">By Provider</asp:ListItem>
                                                    <asp:ListItem>By Speciality</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 149px" align="center" colspan="2">
                                                <asp:Button ID="btn_Download" runat="server" Text="Download" OnClick="btn_Download_Click"></asp:Button>
                                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClientClick="CloseDownloadPanel"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<div id="divid4" style="position:absolute; width: 400px; height: 200px; background-color:white;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
         
        <div style="position: relative; background-color: #B5DF82;  width: 400px; text-align: right" >
        <b>Bill Notes</b>
        <asp:Button ID="txtUpdate4" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server" OnClick="txtUpdate_Click" />
    </div>
        <iframe id="frameeditexpanse1" src="" frameborder="0" height="600px" width="800px"></iframe>
    </div>--%>
    <div id="divid4" style="position: absolute; width: 90%; height: 90%; background-color: White; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; background-color: #B5DF82; width: 100%; text-align: center">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="txtUpdate4" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                            OnClick="txtUpdatepopup_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <iframe id="frameeditexpanse1" src="" frameborder="0" height="96%" width="100%"></iframe>
    </div>
    <div style="visibility: hidden;">
        <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="txtUpdate1_Click" />
    </div>
    <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" HeaderStyle-BackColor="#B5DF82" ClientInstanceName="PatientInfoPop"
        HeaderText="Denial" HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="900px" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        RenderIFrameForPopupElements="Default" Height="564px">
        <ClientSideEvents CloseButtonClick="ClosePopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
        ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true"
        ContextKey="SZ_COMPANY_ID">
    </ajaxcontrol:AutoCompleteExtender>
    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
        ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true"
        ContextKey="SZ_COMPANY_ID">
    </ajaxcontrol:AutoCompleteExtender>
    <dx:ASPxPopupControl ID="VerificationInfoPopup" runat="server" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" HeaderStyle-BackColor="#B5DF82" ClientInstanceName="VerificationInfoPopup"
        HeaderText="Verification" HeaderStyle-HorizontalAlign="Left" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="900px" PopupHorizontalOffset="0"
        PopupVerticalOffset="0" RenderIFrameForPopupElements="Default"
        Height="564px">
        <ClientSideEvents CloseButtonClick="CloseVrificationPopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" HeaderStyle-BackColor="#B5DF82" ClientInstanceName="EorInfoPopup"
        HeaderText="EOR" HeaderStyle-HorizontalAlign="Left" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="900px" PopupHorizontalOffset="0"
        PopupVerticalOffset="0" RenderIFrameForPopupElements="Default"
        Height="564px">
        <ClientSideEvents CloseButtonClick="CloseEorPopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ShowPopup" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup"
        HeaderText="MG2 Print" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="800px"
        ToolTip="Select Cost Center" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ShowPopup2" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup2"
        HeaderText="HP1 Print" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="800px"
        ToolTip="Select Cost Center" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    </dx:ASPxPopupControl>
         <dx:ASPxPopupControl ID="showHPJ1" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="showHPJ1"
        HeaderText="HPJ1 Print" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="1000px"
        ToolTip="" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <contentstyle>
        <Paddings PaddingBottom="5px" />
    </contentstyle>
    </dx:ASPxPopupControl>
    <asp:TextBox ID="txtbillnumber" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtspec" runat="server" Visible="false"></asp:TextBox>
    <div id="dialog" style="overflow: hidden" />
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
    </div>   
</asp:Content>
