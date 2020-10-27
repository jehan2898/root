
<%@ Page Language="C#" AutoEventWireup="true" masterpagefile="~/MasterPage.master" CodeFile="Bill_Sys_AppointPatientEntry.aspx.cs" Inherits="Bill_Sys_AppointPatientEntry"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../validation.js"></script>

    <script type="text/javascript" src="../Registration/validation.js"></script>
    
    <script type="text/javascript">   
    
    // Function used to close AJAX modal popup using javascript.
    
    function CloseModalPopup()
    {
      //var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender');
      var modal = $find("<%=ModalPopupExtender.ClientID %>");
      modal.hide();
    }
    
    // Function used to display exist patient div 
    
//        function SaveExistPatient()
//        {
//            document.getElementById('ctl00_ContentPlaceHolder1_btnPEOK').click(); 
//            document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.visibility='hidden';
//        }
        function CancelExistPatient()
        {
            //document.getElementById('ctl00_ContentPlaceHolder1_txtPatientExistMsg').value = '';
            document.getElementById("<%=txtPatientExistMsg.ClientID %>").value = '';
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.visibility='hidden';
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.visibility='hidden';
            //document.getElementById('ctl00_ContentPlaceHolder1_btnPECancel').click(); 
            document.getElementById("<%=btnPECancel.ClientID %>").click(); 
        }
        
        

        function openExistsPage()
        {
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.zIndex = 1;
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.zIndex = 1;
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.position = 'absolute'; 
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.position = 'absolute'; 
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.left= '300px'; 
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.left= '300px'; 
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.top= '200px'; 
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.top= '200px'; 
            //document.getElementById('ctl00_ContentPlaceHolder1_divDisplayMsg').style.visibility='visible';           
            document.getElementById("<%=divDisplayMsg.ClientID %>").style.visibility='visible';           
            return false;            
        }
    
    // end
    
    
    
    // function : to check patient is exists with entered details or not.
    // if yes then ask for confirmation otherwise do not add patient
    
//    function checkForPatientExists()
//    {
//        //var szMsg = document.getElementById('ctl00_ContentPlaceHolder1_txtPatientExistMsg').value;
//        var szMsg = document.getElementById("<%=txtPatientExistMsg.ClientID %>").value;
//        if(confirm(szMsg))
//        {
//           //document.getElementById('ctl00_ContentPlaceHolder1_btnSave').click();
//           document.getElementById("<%=btnSave.ClientID %>").click();
//            return true;
//        }
//        else
//        {
//           // document.getElementById('ctl00_ContentPlaceHolder1_txtPatientExistMsg').value = '';
//             document.getElementById("<%=txtPatientExistMsg.ClientID %>").value = '';
//            return false;
//        }
//    }
    
    function Test()
    {
      //  document.getElementById('ctl00_ContentPlaceHolder1_btnShowGrid').click();

        return true;
    }
    
    // Start : allow user to enter only integer and / for date
    
    function ascii_value(c)
        {
            c = c . charAt (0);
            var i;
            for (i = 0; i < 256; ++ i)
            {
                var h = i . toString (16);
                if (h . length == 1)
                    h = "0" + h;
                    h = "%" + h;
                    h = unescape (h);
                if (h == c)
                    break;
            }
            return i;
        }
    
        function clickButton1(e,charis)
        {
            var keychar;
            if(navigator.appName.indexOf("Netscape")>(-1))
            {    
                var key = ascii_value(charis);
                if(e.charCode == key || e.charCode==0){
                return true;
               }else{
                     if (e.charCode < 48 || e.charCode > 57)
                     {             
                            return false;
                     } 
                 }
             }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
  }
    
    // End
    
    
    
   
    function checkForTestFacility()
    {
       //if(document.getElementById('ctl00_ContentPlaceHolder1_extddlReferringFacility') != null)
       if(document.getElementById("<%=extddlReferringFacility.ClientID %>") != null)
        {
           // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlReferringFacility').value == 'NA')
            if(document.getElementById("<%=extddlReferringFacility.ClientID %>").value == 'NA')
            {
                alert('Please select test facility.');
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    
        // Function is used validate date 
        
        
        
//        function FromDateValidation()
//      {
//         var year1="";
//         year1=document.getElementById("<%=txtEnteredDate.ClientID%>").value; 
//         
//        
//        
//         if(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_')
//         {
//             return true;   
//         }
//         
//         
//         if((year1.charAt(6)=='_' && year1.charAt(7)=='_') || (year1.charAt(8)=='_' && year1.charAt(9)=='_') || (year1.charAt(6)=='0' && year1.charAt(7)=='0') || (year1.charAt(6)=='0') || (year1.charAt(9)=='_'))
//         {
//            
////           document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = 'Date is Invalid';
//           document.getElementById("<%=lblValidator1.ClientID%>").innerText = 'Date is Invalid';
//          
//         
//            document.getElementById("<%=txtEnteredDate.ClientID%>").focus();
//            return false; 
//         }
//         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
//         {
//           // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = '';
//            document.getElementById("<%=lblValidator1.ClientID%>").innerText = '';
//            return true;
//         }
//        
//      }
     
        function DisplayControl()
        {
            document.getElementById('contentSearch').style.visibility='visible';
            document.getElementById('griddiv').style.visibility='hidden';   
            document.getElementById('searchbuttondiv').style.visibility='hidden';   
           // document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').value='';  
            document.getElementById("<%=txtPatientFName.ClientID %>").value='';  
            // document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').value='';
             document.getElementById("<%=txtPatientLName.ClientID %>").value='';    
          }
        function HideControl()
        {
            document.getElementById('contentSearch').style.visibility='hidden';
            document.getElementById('griddiv').style.visibility='hidden';   
            document.getElementById('searchbuttondiv').style.visibility='visible';   
        }
        
        function Val_Add_UpdateVisitForBillingCompany()
        {
            //if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').value == '')
            if(document.getElementById("<%=txtPatientFName.ClientID %>").value == '')
            {
                alert('Patient First Name should not be Empty.');
               // document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').focus();
                    document.getElementById("<%=txtPatientFName.ClientID %>").focus();
                return false;
            }
            
            //if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').value == '')
            if(document.getElementById("<%=txtPatientLName.ClientID %>").value == '')
            {
                alert('Patient Last name should not be Empty.');
               //document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').focus();
               document.getElementById("<%=txtPatientLName.ClientID %>").focus();
                return false;
            } 
                        
          
            
            //if(document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').value == 'NA')
            if(document.getElementById("<%=extddlDoctor.ClientID %>").value == 'NA')
            {
                alert('Please Select any one Referring Doctor.');
               // document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').focus();
                document.getElementById("<%=extddlDoctor.ClientID %>").focus();
                return false;
            }    
            
           // if(document.getElementById('ctl00_ContentPlaceHolder1_chkTransportation').checked == true)
            if(document.getElementById("<%=chkTransportation.ClientID %>").checked == true)
            {
               // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').value == 'NA')
                if(document.getElementById("<%=extddlTransport.ClientID %>").value == 'NA')
                {
                    alert('Please Select any one Transport company.');
                    //document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').focus();
                    document.getElementById("<%=extddlTransport.ClientID %>").focus();
                    return false;
                } 
            } 
            
            
//            document.getElementById('ctl00_ContentPlaceHolder1_btnSave').click();
//            return true;   
        }
        function Val_AddVisitForTestFacility()
        {           
            //if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').value == '')
            if(document.getElementById("<%=txtPatientFName.ClientID %>").value == '')
            {
                alert('Patient First Name should not be Empty.');
               // document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').focus();
                document.getElementById("<%=txtPatientFName.ClientID %>").focus();
                return false;
            }
            
           //if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').value == '')
           if(document.getElementById("<%=txtPatientLName.ClientID %>").value == '')
            {
                alert('Patient Last name should not be Empty.');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').focus();
                document.getElementById("<%=txtPatientLName.ClientID %>").focus();
                return false;
            } 
            
           // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlCaseType').value == 'NA')
            if(document.getElementById("<%=extddlCaseType.ClientID %>").value == 'NA')
            {
                alert('Please Select any one Case Type.');
               // document.getElementById('ctl00_ContentPlaceHolder1_extddlCaseType').focus();
                document.getElementById("<%=extddlCaseType.ClientID %>").focus();
                return false;
            } 
            
         //   if(document.getElementById('ctl00_ContentPlaceHolder1_extddlMedicalOffice').value == 'NA')
            if(document.getElementById("<%=extddlMedicalOffice.ClientID %>").value == 'NA')
            {
                alert('Please Select any one Office Name.');
              //  document.getElementById('ctl00_ContentPlaceHolder1_extddlMedicalOffice').focus();
                document.getElementById("<%=extddlMedicalOffice.ClientID %>").focus();
                return false;
            } 
            
           // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').value == 'NA')
            if(document.getElementById("<%=extddlDoctor.ClientID %>").value == 'NA')
            {
                alert('Please Select any one Referring Doctor.');
               // document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').focus();
                document.getElementById("<%=extddlDoctor.ClientID %>").focus();
                return false;
            }  
            
           // if(document.getElementById('ctl00_ContentPlaceHolder1_chkTransportation').checked == true)
            if(document.getElementById("<%=chkTransportation.ClientID %>").checked == true)
            {
               // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').value == 'NA')
               if(document.getElementById("<%=extddlTransport.ClientID %>").value == 'NA')
                
                {
                    alert('Please Select any one Transport company.');
                    //document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').focus();
                    document.getElementById("<%=extddlTransport.ClientID %>").focus();
                    return false;
                } 
            }                     
//            document.getElementById('ctl00_ContentPlaceHolder1_btnSave').click();
//            return true;
              
        }
        function val_updateTestFacility()
        {
          //  if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').value == '')
            if(document.getElementById("<%=txtPatientFName.ClientID %>").value == '')
            {
                alert('Patient First Name should not be Empty.');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtPatientFName').focus();
                document.getElementById("<%=txtPatientFName.ClientID %>").focus();
                return false;
            }
            
           // if(document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').value == '')
            if(document.getElementById("<%=txtPatientLName.ClientID %>").value == '')
            {
                alert('Patient Last name should not be Empty.');
              //document.getElementById('ctl00_ContentPlaceHolder1_txtPatientLName').focus();
              document.getElementById("<%=txtPatientLName.ClientID %>").focus();
                return false;
            }          
                        
           // if(document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').value == 'NA')
            if(document.getElementById("<%=extddlDoctor.ClientID %>").value == 'NA')
            {
                alert('Please Select any one Referring Doctor.');
                //document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').focus();
                document.getElementById("<%=extddlDoctor.ClientID %>").focus();
                return false;
            }  
            
            //if(document.getElementById('ctl00_ContentPlaceHolder1_chkTransportation').checked == true)
            if(document.getElementById("<%=chkTransportation.ClientID %>").checked == true)
            {
                //if(document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').value == 'NA')
                if(document.getElementById("<%=extddlTransport.ClientID %>").value == 'NA')
                {
                    alert('Please Select any one Transport company.');
                    //document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').focus();
                    document.getElementById("<%=extddlTransport.ClientID %>").focus();
                    return false;
                } 
            } 
        }
        
        
        //Nirmalkumar
         function yesnopopup() {
          //
          alert("kk");
              document.getElementById('div2').style.zIndex = 1;
            document.getElementById('div2').style.position = 'absolute'; 
            document.getElementById('div2').style.left= '360px'; 
            document.getElementById('div2').style.top= '250px'; 
            document.getElementById('div2').style.visibility='visible';  
            return false;
}

        //END Nirmalkumar
        
//        function DisplayTransportCompany()
//        {   
//            if(document.getElementById('ctl00_ContentPlaceHolder1_chkTransportation').checked == true)
//            {
//                document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').style.visibility = "visible";
//            }
//            else
//            {
//                document.getElementById('ctl00_ContentPlaceHolder1_extddlTransport').style.visibility = "hidden";
//            }
//        }
    </script>

    <script language="javascript" type="text/javascript">
   
       function checkDate(year,month,day,hours,min)
       {
       //   alert('year : ' + year + ' month ' + month + ' day ' + day + ' hours' + hours + '  min' + min);
          var myDate=new Date(year,month-1,day,hours,min,0);
       //   myDate.setFullYear(year,month-1,day,hours,min,0);


          var today = new Date();
       //   alert('myDate ' + myDate + 'today' + today);
          if (myDate>today)
          {
                return true;
          }
          else
          {
                // return false;
                return true; // allow user to add visit at previous date.
          }
       }
       
       function EventDelete(obj)
       {
            if(confirm('Do you want to delete appointment and its treatment codes?'))
            {
                var eventid = obj.split('-');
               // document.getElementById('ctl00_ContentPlaceHolder1_hdnEventDeleteDate').value =  eventid[0]+'-'+eventid[1];

               document.getElementById("<%=hdnEventDeleteDate.ClientID %>").value =  eventid[0]+'-'+eventid[1];
               // document.getElementById('ctl00_ContentPlaceHolder1_hdnEventId').value =  eventid[1];

               document.getElementById("<%=hdnEventId.ClientID %>").value =  eventid[1];
                //document.getElementById('ctl00_ContentPlaceHolder1_btnDelete').click();

               document.getElementById("<%=btnDelete.ClientID %>").click();
            }
       }
       
       function FunctionValidationDelete()
       {
            if(confirm('Do you want to delete appointment?'))
                return true;
            else
                return false;
       }
    
       
       function openTypePage(obj)
       {           
            var p_vCheckValid = obj.split('~');
            var p_vCheckEventValid;
            
            var tmp = obj.split(' ');
            var partPassDate = tmp[0].split('/');
            
            var tmp1 =  obj.split('|');
            var tmp2 =  tmp1[0].split(' ');
            var aaa =   tmp2[1].split('.');        
            var tmp3 =  obj.split('!');
            var tmp4 =  tmp3[0].split('|');
            
            if(tmp4[1] == 'PM' && aaa[0] != '12' )
            {
                aaa[0] = parseInt(aaa[0]) + 12 ;
            }            
           
            var today = new Date();
         //    alert('today' + today);         
        
            if(p_vCheckValid.length > 1)
            {           
                p_vCheckEventValid = p_vCheckValid[1].split('^')            
                if(p_vCheckEventValid[0]!='undefined' || p_vCheckEventValid[0]!='')
                {
//                    document.getElementById('divid').style.zIndex = 1;
//                    document.getElementById('divid').style.position = 'absolute'; 
//                    document.getElementById('divid').style.left= '300px'; 
//                    document.getElementById('divid').style.top= '100px'; 
//                    document.getElementById('divid').style.zindex= '1'; 
//                    document.getElementById('divid').style.visibility='visible'; 
                   // document.getElementById('frameeditexpanse').src="Bill_Sys_AddAppointment.aspx?_date=" + obj  ;             
                   // document.getElementById('ctl00_ContentPlaceHolder1_hdnOperationType').value =  'BillngCompanyUpdate';
                    document.getElementById("<%=hdnOperationType.ClientID %>").value =  'BillngCompanyUpdate';
                    //document.getElementById('ctl00_ContentPlaceHolder1_hdnObj').value =  tmp1[0]+'|'+tmp1[1];
                    document.getElementById("<%=hdnObj.ClientID %>").value =  tmp1[0]+'|'+tmp1[1];
                   // document.getElementById('ctl00_ContentPlaceHolder1_btnAdd').click();
                    document.getElementById("<%=btnAdd.ClientID %>").click();
                    return true;
                }
                else
                {
                    alert('Cannot add schedule ...!');
                    return false;
                }
            }
            else if(checkDate(partPassDate[2],partPassDate[0],partPassDate[1],aaa[0],aaa[1])) // Add Appointment(Billing System / Test Facility)
            {            
//                document.getElementById('divid').style.zIndex = 1;
//                document.getElementById('divid').style.position = 'absolute'; 
//                document.getElementById('divid').style.left= '300px'; 
//                document.getElementById('divid').style.top= '100px'; 
//                document.getElementById('divid').style.zindex= '1'; 
//                document.getElementById('divid').style.visibility='visible'; 
                //document.getElementById('frameeditexpanse').src="Bill_Sys_AddAppointment.aspx?_date=" + obj  ;
               // document.getElementById('ctl00_ContentPlaceHolder1_hdnOperationType').value =  ''; 
                document.getElementById("<%=hdnOperationType.ClientID %>").value =  ''; 
                //document.getElementById('ctl00_ContentPlaceHolder1_hdnObj').value =  tmp1[0]+'|'+tmp1[1];
                document.getElementById("<%=hdnObj.ClientID %>").value =  tmp1[0]+'|'+tmp1[1];
               // document.getElementById('ctl00_ContentPlaceHolder1_btnAdd').click();
                document.getElementById("<%=btnAdd.ClientID %>").click();
            }
            else
            {
                alert('Cannot add schedule ...!');
                return false;
            }
  
       }
        function openReferredTypePage(obj)
       {
            var tmp = obj.split(' ');
            var partPassDate = tmp[0].split('/');
            
            var tmp1 =  obj.split('|');
            var tmp2 =  tmp1[0].split(' ');
            var aaa =   tmp2[1].split('.');        
            var tmp3 =  obj.split('!');
            var tmp4 =  tmp3[0].split('|');
            
            if(tmp4[1] == 'PM' && aaa[0] != '12' )
            {
                aaa[0] = parseInt(aaa[0]) + 12 ;
            }
            
//            if(checkDate(partPassDate[2],partPassDate[0],partPassDate[1],aaa[0],aaa[1]))
//            {
           
//                document.getElementById('divid').style.zIndex = 1;
//                document.getElementById('divid').style.position = 'absolute'; 
//                document.getElementById('divid').style.left= '300px'; 
//                document.getElementById('divid').style.top= '100px'; 
//                document.getElementById('divid').style.zindex= '1'; 
//                document.getElementById('divid').style.visibility='visible'; 
                //document.getElementById('frameeditexpanse').src="Bill_Sys_AddReferredAppointment.aspx?_date=" + obj  ;
              //  document.getElementById('ctl00_ContentPlaceHolder1_hdnOperationType').value =  'TestFacilityUpdate';            
                document.getElementById("<%=hdnOperationType.ClientID %>").value =  'TestFacilityUpdate';            
                //document.getElementById('ctl00_ContentPlaceHolder1_hdnObj').value =  tmp1[0]+'|'+tmp1[1];            
                document.getElementById("<%=hdnObj.ClientID %>").value =  tmp1[0]+'|'+tmp1[1];            
                //document.getElementById('ctl00_ContentPlaceHolder1_btnAdd').click();

                document.getElementById("<%=btnAdd.ClientID %>").click();
//            }
//             else
//            {
//                alert('Cannot add schedule ...!');
//                return false;
//            }
         //   document.getElementById('frameeditexpanse').src="Bill_Sys_AddReferredAppointment.aspx?_date=" + obj  ;

       }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbltest" runat="server"></asp:Label>
            <table style="vertical-align: top; width: 100%; height: 100%" id="First" cellspacing="0"
                cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                            width: 97%; padding-top: 3px; height: 100%">
                            <table style="vertical-align: top; width: 100%" id="MainBodyTable" cellspacing="0"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="LeftTop">
                                        </td>
                                        <td class="CenterTop">
                                        </td>
                                        <td class="RightTop">
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td class="Center" valign="top">
                                            <table style="vertical-align: top; width: 100%; height: 100%" cellspacing="0" cellpadding="0"
                                                border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="TDPart" align="center">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 19px" colspan="5">
                                                                            <!-- This should be a reserved space for system messages. -->
                                                                            <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 19px" width="15%">
                                                                            <asp:Label ID="lblHeaderPatientName" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 19px" width="15%">
                                                                        </td>
                                                                        <td style="height: 19px" width="25%">
                                                                        </td>
                                                                        <td style="height: 19px; text-align: right" width="35%" colspan="2">
                                                                            <asp:Label ID="lblCurrentDate" runat="server" Width="100%"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="100%" colspan="5">
                                                                            <table style="text-align: left" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 12%">
                                                                                            <asp:Label ID="lblTestFacility1" runat="server">Test Facility :</asp:Label>&nbsp;
                                                                                        </td>
                                                                                        <td width="33%">
                                                                                            <extddl:ExtendedDropDownList ID="extddlReferringFacility" runat="server" Width="150px"
                                                                                                Connection_Key="Connection_String" Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY"
                                                                                                Selected_Text="--- Select ---"></extddl:ExtendedDropDownList>
                                                                                        </td>
                                                                                        <td width="33%">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 12%">
                                                                                            <asp:Label ID="lblEnteredDate" runat="server">Date :</asp:Label></td>
                                                                                        <td width="33%">
                                                                                            <asp:TextBox ID="txtEnteredDate" onkeypress="return clickButton1(event,'/')" runat="server"
                                                                                                Width="80px"></asp:TextBox>
                                                                                                
                                                                                                
                                                                                                <a id="trigger" href="#">
                                                                                                <input type="image" name="mgbtnDateofService" id="imgbtnDateofService" runat="server"
                                                                                                    src="~/Images/cal.gif" border="0" /></a>
                                                                                                    
                                                                                                    
                                                                                                    
                                                                                            <%--<asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>--%>
                                                                                            <asp:Label ID="lblValidator1" runat="server" ForeColor="Red" Font-Bold="False"></asp:Label>
                                                                                            
                                                                                            <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" PopupButtonID="imgbtnDateofService"
                                                                                                TargetControlID="txtEnteredDate">
                                                                                            
                                                                                            <%--<ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" PopupButtonID="imgbtnFromDate"
                                                                                                TargetControlID="txtEnteredDate">--%>
                                                                                            </ajaxToolkit:CalendarExtender>
                                                                                        </td>
                                                                                        <td width="33%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 12%">
                                                                                            <asp:Label ID="lblInterval" runat="server">Time Interval :</asp:Label></td>
                                                                                        <td width="33%">
                                                                                            <asp:DropDownList ID="ddlInterval" runat="server" Width="60px">
                                                                                                <asp:ListItem Text="0.15" Value="0.15"></asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Text="0.30" Value="0.30"></asp:ListItem>
                                                                                                <asp:ListItem Text="0.45" Value="0.45"></asp:ListItem>
                                                                                                <asp:ListItem Text="0.60" Value="0.60"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtUserId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                            <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Width="25px" CssClass="Buttons"
                                                                                                Text="Go"></asp:Button></td>
                                                                                                
                                                                                        <td width="33%">
                                                                                        
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TDPart" align="center">
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                <ProgressTemplate>
                                                                    <div id="DivStatus11" runat="Server" class="PageUpdateProgress">
                                                                        <asp:Image ID="img1" AlternateText="Loading....." runat="server" ImageUrl="~/Images/rotation.gif" />
                                                                        Loading....
                                                                        <%-- <img id="img1" alt="Loading. Please wait..." ImageUrl="~/Images/rotation.gif" /> Loading...--%>
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">
                                                            <table style="width: 100%" class="ContentTable" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="vertical-align: top" width="14%">
                                                                            <div style="text-align: left">
                                                                                <asp:DropDownList ID="ddlMonthList" runat="server" Width="45px">
                                                                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                                                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                                                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                                                    <asp:ListItem Value="9" Text="Sept"></asp:ListItem>
                                                                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                                                </asp:DropDownList><asp:DropDownList ID="ddlYearList" runat="server">
                                                                                </asp:DropDownList><asp:Button ID="btnLoadCalendar" OnClick="btnLoadCalendar_Click"
                                                                                    runat="server" Width="23px" CssClass="Buttons" Text="Go"></asp:Button>
                                                                            </div>
                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                            </asp:Panel>
                                                                            <%-- </contenttemplate>
                                                    </asp:UpdatePanel>--%>
                                                                        </td>
                                                                        <td style="vertical-align: top" width="86%">
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <contenttemplate>--%>
                                                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                            <asp:Panel ID="Panel2" runat="server">
                                                                            </asp:Panel>
                                                                            <%--</contenttemplate>
                                                    </asp:UpdatePanel>--%>
                                                                            <asp:DataGrid ID="grdScheduleReport" runat="server" Width="100%" CssClass="GridTable"
                                                                                AutoGenerateColumns="true">
                                                                                <FooterStyle />
                                                                                <SelectedItemStyle />
                                                                                <PagerStyle />
                                                                                <AlternatingItemStyle />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">
                                                            <asp:Button Style="display: none" ID="btnAdd" OnClick="btnAdd_Click" runat="server">
                                                            </asp:Button>&nbsp;
                                                            <asp:Button Style="display: none" ID="btnDelete" OnClick="btnDelete_Click" runat="server"
                                                                Text="Button"></asp:Button>
                                                            <asp:HiddenField ID="hdnObj" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hdnEventDeleteDate" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hdnEventId" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hdnOperationType" runat="server"></asp:HiddenField>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td style="width: 10px; height: 100%" class="RightCenter">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftBottom">
                                        </td>
                                        <td class="CenterBottom">
                                        </td>
                                        <td style="width: 10px" class="RightBottom">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="display: none">
                                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                                </asp:LinkButton>
                            </div>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="lbn_job_det"
                                PopupDragHandleControlID="pnlMove" PopupControlID="Panel3" DropShadow="true"
                                BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:Panel Style="display: none" ID="Panel3" runat="server" Width="750px" CssClass="modalPopup">
                <table style="vertical-align: top" class="TDPart" cellspacing="0" cellpadding="0"
                    border="0">
                    <tbody>
                        <tr>
                            <td>
                                <table class="ContentTable" cellspacing="0" cellpadding="0" width="100%" align="center"
                                    border="0">
                                    <tbody>
                                        <tr>
                                            <td style="background-color: graytext" valign="top" align="right" colspan="6">
                                                <asp:Panel ID="pnlMove" runat="server">
                                                    <a hef="#" id="lnkCloseModalPopup" onclick="javascript:CloseModalPopup();" style="cursor: pointer;
                                                        color: Blue;">X </a>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr id="tdSerach" runat="server">
                                            <td class="ContentLabel" valign="top" align="center" colspan="6">
                                                <%--<asp:UpdatePanel id="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>--%>
                                                <table class="ContentTable" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: left" class="ContentLabel" width="100%" colspan="6">
                                                                <div style="left: 0px; float: left; visibility: visible; position: relative; top: 0px"
                                                                    id="searchbuttondiv">
                                                                    <input id="btnClickSearch" class="Buttons" onclick="DisplayControl()" type="button"
                                                                        value="Search" runat="server" />
                                                                    <asp:Button ID="btnAddPatient" class="Buttons" OnClick="btnAddPatient_Click" runat="server"
                                                                        Text="Add Patient"></asp:Button>
                                                                </div>
                                                                <div style="left: 0px; float: left; visibility: hidden; position: relative; top: 0px"
                                                                    id="contentSearch">
                                                                    <asp:Label ID="lblFirstname" runat="server" Font-Names="Verdana" Font-Size="12px">First Name :</asp:Label>
                                                                    <asp:TextBox ID="txtPatientFirstName" runat="server" Width="120px"></asp:TextBox>
                                                                    <asp:Label ID="lblLastname" runat="server" Font-Names="Verdana" Font-Size="12px">Last Name :</asp:Label>
                                                                    <asp:TextBox ID="txtPatientLastName" runat="server" Width="120px"></asp:TextBox>
                                                                    <asp:Button ID="btnSearhPatientList" OnClick="btnSearhPatientList_Click" runat="server"
                                                                        CssClass="Buttons" Text="Submit"></asp:Button>
                                                                    <input id="btnPatientCancel" class="Buttons" onclick="HideControl()" type="button"
                                                                        value="Cancel" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%--</ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearhPatientList" EventName="Click"></asp:AsyncPostBackTrigger>
                                    </Triggers>
                                    </asp:UpdatePanel> 
                                     <asp:UpdatePanel id="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                      <ContentTemplate>--%>
                                                <table class="ContentTable" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="height: 20px; text-align: left" class="ContentLabel" colspan="6">
                                                                <div style="left: 0px; float: left; position: relative; top: 0px" id="griddiv">
                                                                    <asp:DataGrid ID="grdPatientList" runat="server" Width="100%" CssClass="GridTable"
                                                                        AutoGenerateColumns="false" OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged"
                                                                        OnPageIndexChanged="grdPatientList_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                                                        PageSize="3" AllowPaging="true">
                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="I_PATIENT_AGE" HeaderText="Age" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Address" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_STREET" HeaderText="Street" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_CITY" HeaderText="City" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_ZIP" HeaderText="Zip" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_EMAIL" HeaderText="Email" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="MI" HeaderText="MI" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_SOCIAL_SECURITY_NO" HeaderText="Social Security No"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DT_DOB" HeaderText="Date Of Birth" DataFormatString="{0:MM/dd/yyyy}"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_GENDER" HeaderText="Gender" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DT_INJURY" HeaderText="Date of Injury" DataFormatString="{0:MM/dd/yyyy}"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_JOB_TITLE" HeaderText="Job Title" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_WORK_ACTIVITIES" HeaderText="Work Activities" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_STATE" HeaderText="State" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_CARRIER_CASE_NO" HeaderText="Carrier Case No" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer Name" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Employer Phone" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_ADDRESS" HeaderText="Employer Address" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_CITY" HeaderText="Employer City" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_STATE" HeaderText="Employer State" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Employer Zip" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="WORK_PHONE" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_WORK_PHONE_EXTENSION" HeaderText="WORK_PHONE" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="BT_WRONG_PHONE" HeaderText="WRONG_PHONE" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="BT_TRANSPORTATION" HeaderText="TRANSPORTATION" Visible="False">
                                                                            </asp:BoundColumn>
                                                                        </Columns>
                                                                        <PagerStyle Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%--</ContentTemplate>                                                  
                                                    </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DivStatus2" runat="Server" class="PageUpdateProgress">
                                                            <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                                            <asp:Image ID="img2" AlternateText="Loading....." runat="server" ImageUrl="~/Images/rotation.gif" />
                                                            Loading....
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 493px" class="ContentLabel" valign="top" align="center" colspan="6">
                                                <%--<asp:UpdatePanel id="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>--%>
                                                <table width="100%">
                                                    <%--<tr>
                                                        <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                         Add Patient 
                                                        </td>
                                                    </tr>--%>
                                                    <tbody>
                                                        <tr>
                                                            <td style="height: 20px; text-align: left" class="ContentLabel" colspan="6">
                                                                <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                <div style="color: red" id="ErrorDiv" visible="true">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                <asp:Label ID="lblChartNumber" runat="server" Text="Chart Number" Font-Names="Verdana"
                                                                    Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 201px" align="left">
                                                                <asp:TextBox ID="txtRefChartNumber" onkeypress="return CheckForInteger(event,'/')"
                                                                    runat="server" CssClass="cinput" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                            </td>
                                                            <td width="10%">
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:LinkButton ID="lnkPatientDesk" OnClick="lnkPatientDesk_Click" runat="server"
                                                                    Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                    ToolTip="Patient Desk"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <%--Nirmal--%>
                                                        <tr>
                                                            <td style="height: 20px; text-align: left" class="ContentLabel" colspan="6">
                                                                <asp:Label ID="lblMsg2" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                <div style="color: red" id="Div1" visible="true">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <%--END--%>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                First name</td>
                                                            <td style="width: 201px" align="left">
                                                                <asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                Middle
                                                            </td>
                                                            <td align="left" width="20%">
                                                                <asp:TextBox ID="txtMI" runat="server" MaxLength="2"></asp:TextBox>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                Last name
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                Phone</td>
                                                            <td style="width: 201px" align="left">
                                                                <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox></td>
                                                            <td style="width: 20%" align="left">
                                                                Address</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPatientAddress" runat="server"></asp:TextBox></td>
                                                            <td align="left" width="10%">
                                                                City</td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                State</td>
                                                            <td style="width: 201px" align="left">
                                                                <asp:TextBox ID="txtState" runat="server" Visible="false"></asp:TextBox>
                                                                <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="150px"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                                    Selected_Text="--- Select ---" Enabled="false"></extddl:ExtendedDropDownList>
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                Insurance</td>
                                                            <td align="left" width="10%">
                                                                <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Width="150px"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                    Selected_Text="--- Select ---" Enabled="False"></extddl:ExtendedDropDownList>
                                                                &nbsp;
                                                                <%--<asp:ImageButton ID="imgbtnBirthdate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <cc1:CalendarExtender ID="calBirthdate" runat="server" PopupButtonID="imgbtnBirthdate"
                                                                TargetControlID="txtBirthdate" >
                                                            </cc1:CalendarExtender>--%>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                Case Type</td>
                                                            <td style="width: 20%" align="left">
                                                                <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---"
                                                                    Enabled="False"></extddl:ExtendedDropDownList>
                                                                <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="CASESTATUS_LIST" Procedure_Name="SP_MST_CASE_STATUS" Selected_Text="--- Select ---"
                                                                    Visible="false" Enabled="False" Flag_ID="txtCompanyID.Text.ToString();"></extddl:ExtendedDropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                <asp:Label ID="lblSSN" runat="server" Text="SS #" Font-Names="Verdana" Font-Size="12px"></asp:Label></td>
                                                            <td style="width: 201px" align="left">
                                                                <asp:TextBox ID="txtSocialSecurityNumber" runat="server" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:Label ID="lblBirthdate" runat="server" Text="Birthdate" Font-Names="Verdana"
                                                                    Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                <asp:TextBox ID="txtBirthdate" runat="server" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                <asp:Label ID="lblAge" runat="server" Text="Age" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:TextBox ID="txtPatientAge" runat="server" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px; height: 31px" align="left">
                                                                <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility" Font-Names="Verdana"
                                                                    Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 201px; height: 31px" align="left">
                                                                <extddl:ExtendedDropDownList ID="extddlMedicalOffice" runat="server" Width="150px"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE"
                                                                    Selected_Text="--- Select ---" Visible="false" OnextendDropDown_SelectedIndexChanged="extddlMedicalOffice_extendDropDown_SelectedIndexChanged"
                                                                    AutoPost_back="true"></extddl:ExtendedDropDownList>
                                                                <extddl:ExtendedDropDownList ID="extddlReferenceFacility" runat="server" Width="150px"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY"
                                                                    Selected_Text="--- Select ---" Enabled="False"></extddl:ExtendedDropDownList>
                                                            </td>
                                                            <td style="width: 10%; height: 31px" align="left">
                                                                Referring Doctor
                                                            </td>
                                                            <td style="width: 10%; height: 31px" align="left" colspan="2">
                                                                <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="100%" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="newGETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---">
                                                                </extddl:ExtendedDropDownList>
                                                            </td>
                                                            <%-- <td style="height: 31px" valign="top" align="left" width="10%">
                                                                </td>--%>
                                                            <td style="width: 20%; height: 31px" align="left">
                                                                Transport &nbsp;
                                                                <asp:CheckBox ID="chkTransportation" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkTransportation_CheckedChanged"
                                                                    TextAlign="Left"></asp:CheckBox>
                                                                <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="110px" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="GET_TRANSPORT_LIST" Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---"
                                                                    Visible="false"></extddl:ExtendedDropDownList>
                                                                &nbsp;&nbsp;
                                                                <%--<asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>--%>
                                                                <%-- <asp:TextBox ID="txtDoctorName" runat="server" Visible="false"></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" valign="top" align="left">
                                                                Procedure
                                                            </td>
                                                            <td style="text-align: left" align="left" width="10%" colspan="5">
                                                                <asp:ListBox ID="ddlTestNames" runat="server" Width="350px" Visible="false" CssClass="s"
                                                                    Rows="1" SelectionMode="Multiple"></asp:ListBox>
                                                                <div style="overflow: scroll; width: 100%; height: 200px" id="divProcedureCode" runat="server"
                                                                    visible="true">
                                                                    <asp:DataGrid ID="grdProcedureCode" runat="server" Width="95%" CssClass="GridTable"
                                                                        AutoGenerateColumns="false" PagerStyle-Mode="NumericPages" PageSize="3" OnItemDataBound="grdProcedureCode_ItemDataBound">
                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:TemplateColumn>
                                                                                <ItemStyle Width="5%" />
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="code" HeaderText="Patient ID" Visible="False" ItemStyle-Width="5%">
                                                                            </asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="description" HeaderText="Procedure" ItemStyle-Width="200px"
                                                                                ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:TemplateColumn HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <itemstyle width="5%" />
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No Show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--4--%>
                                                                            <asp:TemplateColumn HeaderText="Re-Schedule Date">
                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtReScheduleDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                        Width="70px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="Re-Schedule Time">
                                                                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlReSchHours" runat="server" Width="40px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="40px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:DropDownList ID="ddlReSchTime" runat="server" Width="40px">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:TemplateColumn HeaderText="Study No.">
                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtStudyNo" runat="server" Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--7--%>
                                                                            <asp:BoundColumn DataField="I_RESCHEDULE_ID" HeaderText="I_RESCHEDULE_ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="I_EVENT_PROC_ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:TemplateColumn HeaderText="Notes">
                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtProcNotes" runat="server" Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--10--%>
                                                                            <asp:TemplateColumn HeaderText="Notes" Visible="false">
                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtProcBillStatus" runat="server" Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </div>
                                                            </td>
                                                            <%--<td width="10%">
                                                            </td>
                                                        <td width="10%">
                                                            &nbsp; <asp:DropDownList ID="ddlTestNames" runat="server" Width="150px" Visible="false" >
                                                            </asp:DropDownList>--%>
                                                        </tr>
                                                        <%-- <tr>
                                                        <td style="width: 185px; height: 68px;">
                                                            Start Time</td>
                                                        <td colspan="2" style="height: 68px">
                                                            H
                                                            <asp:DropDownList ID="ddlHours" runat="server" Width="48px">
                                                            </asp:DropDownList>
                                                            M
                                                            <asp:DropDownList ID="ddlMinutes" runat="server" Width="48px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTime" runat="server" Width="48px">
                                                            </asp:DropDownList>
                                                        </td>
                                                   
                                                        <td style="height: 68px">
                                                            End Time</td>
                                                        <td colspan="2" style="height: 68px">
                                                            H
                                                            <asp:DropDownList ID="ddlEndHours" runat="server" Width="40px">
                                                            </asp:DropDownList>
                                                            M
                                                            <asp:DropDownList ID="ddlEndMinutes" runat="server" Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlEndTime" runat="server" Width="45px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                        <tr>
                                                            <td style="width: 185px" align="left">
                                                                Notes
                                                            </td>
                                                            <td style="text-align: left" align="left" colspan="5">
                                                                &nbsp;<asp:TextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 26px" align="left" colspan="6">
                                                                <asp:DropDownList ID="ddlHours" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlMinutes" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlTime" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlEndHours" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlEndMinutes" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlEndTime" runat="server" Width="10px" Visible="False">
                                                                </asp:DropDownList>
                                                                <%--<asp:TextBox id="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                &nbsp;
                                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="62px"
                                                                    Visible="false" CssClass="Buttons" Text="Update" OnClientClick="javascript:return FunctionValidationUpdate();">
                                                                </asp:Button>
                                                                &nbsp;
                                                                <asp:Button ID="btnDeleteEvent" OnClick="btnDeleteEvent_Click" runat="server" Width="62px"
                                                                    Visible="false" CssClass="Buttons" Text="Delete" OnClientClick="javascript:return FunctionValidationDelete();">
                                                                </asp:Button>&nbsp;
                                                                <asp:Button ID="btnDuplicateSaveClick" OnClick="btnSave_Click"  runat="server" Width="62px" 
                                                                    CssClass="Buttons" Text="Save"></asp:Button>
                                                                <%--<asp:Button ID="btnSaveTemp"  runat="server" Width="62px" onclick="btnSaveTemp_Click" Text="Save" CssClass="Buttons"></asp:Button>--%>
                                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="62px"
                                                                    CssClass="Buttons" Text="Cancel"></asp:Button>&nbsp;<%--<INPUT id="Button1" class="Buttons" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';javascript:parent.document.getElementById('frameeditexpanse').src = '';" type=button value="Cancel" />--%>&nbsp;
                                                                <asp:Button Style="display: none" ID="btnSave" OnClick="btnSave_Click" runat="server"
                                                                    Width="62px" CssClass="Buttons" Text="Save"></asp:Button>
                                                                <asp:Label ID="Label11" runat="server" Visible="False" Text="Study #" Font-Names="Verdana"
                                                                    Font-Size="12px"></asp:Label>
                                                                <asp:Label ID="lblTypetext" runat="server" Visible="False" Text="Type" Font-Names="Verdana"
                                                                    Font-Size="12px"></asp:Label>
                                                                <asp:DropDownList ID="ddlType" runat="server" Width="10px" Visible="false" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0"> --Select--</asp:ListItem>
                                                                    <asp:ListItem Value="TY000000000000000001">Visit</asp:ListItem>
                                                                    <asp:ListItem Value="TY000000000000000002">Treatment</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="TY000000000000000003">Test</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtStudyNumber" runat="server" Width="2px" Visible="false"></asp:TextBox>
                                                                <asp:HiddenField ID="txtPatientExistMsg" runat="server"></asp:HiddenField>
                                                                <asp:TextBox ID="txtPatientCompany" runat="server" Width="5px" Visible="False"></asp:TextBox>
                                                                <asp:FileUpload ID="flUpload" runat="server" Width="10px" Visible="false"></asp:FileUpload>
                                                                <asp:Button ID="btnLoadPageData" OnClick="btnLoadPageData_Click" runat="server" Width="62px"
                                                                    Visible="false" CssClass="Buttons" Text="Delete"></asp:Button>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <div style="border-right: silver 2px solid; border-top: silver 2px solid; z-index: 1500;
                                                        left: 428px; visibility: hidden; border-left: silver 2px solid; width: 300px;
                                                        border-bottom: silver 2px solid; position: absolute; top: 920px; height: 150px;
                                                        background-color: #dbe6fa; text-align: center" id="divDisplayMsg" runat="server">
                                                        <div style="float: left; width: 40%; font-family: Times New Roman; position: relative;
                                                            height: 20px; background-color: #8babe4; text-align: left">
                                                            Msg
                                                        </div>
                                                        <div style="float: left; width: 60%; position: relative; height: 20px; background-color: #8babe4;
                                                            text-align: right">
                                                            <a style="cursor: pointer" title="Close" onclick="CancelExistPatient();">X</a>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <div style="width: 231px; font-family: Times New Roman; top: 50px; text-align: center">
                                                            <span style="font-weight: bold; font-size: 12px" id="msgPatientExists" runat="server">
                                                            </span>
                                                        </div>
                                                        <br />
                                                        <div style="text-align: center">
                                                            <asp:Button ID="btnPEOk" OnClick="btnPEOk_Click" runat="server" CssClass="Buttons"
                                                                Text="Ok"></asp:Button>
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnPECancel" OnClick="btnPECancel_Click" runat="server" CssClass="Buttons"
                                                                Text="Cancel"></asp:Button>
                                                        </div>
                                                    </div>
                                                </table>
                                                <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
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
    <%--<div id="divid" style="position: absolute; width: 740px; height: 600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 740px;">
            <a  onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="740px"></iframe>
    </div>--%>
    <input type="hidden" id="txtGetDay" runat="server" />
        <%--<div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px;
                background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
                border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
                text-align: center;">
                <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
                    font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px;
                    top: 0px;">
                    Conformation
                </div>
                <br />
                <br />
                <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
                    <span id="Span2" runat="server"></span>
                </div>
                <br />
                <br />
                <div style="text-align: center;">
                    <asp:Button ID="Button1" class="Buttons" Style="width: 15%;" runat="server" Text="Ok"
                         />
                    <asp:Button ID="btnYes" runat="server" CssClass="Buttons" 
                        Text="Yes" Width="80px" />
                    <asp:Button ID="btnNo" runat="server" CssClass="Buttons"  Text="No"
                        Width="80px" />
                </div>
            </div>--%>
            
            <div id="div2" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px;
        background-color: White; visibility: hidden; border-right: #b4dd82 2px solid;
        border-top: #b4dd82 2px solid; border-left: #b4dd82 2px solid; border-bottom: #b4dd82 2px solid;
        text-align: center;">
        <div style="position: relative; width: 100%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #b4dd82; left: 0px;
            top: 0px;">
            MSG
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2" runat="server" style="font-size: 10pt; font-family: "></span>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
         <asp:Button ID="Button1" runat="server" OnClick="btnYes_Click" Text="Yes" Width="80px" />
            <asp:Button ID="Button2" runat="server" OnClick="btnNo_Click" Text="No" Width="80px" />
            <%--<asp:Button ID="btnYes" runat="server" Text="Yes" Width="80px" onclick=""/>
            <asp:Button ID="btnNo" runat="server" Text="No" Width="80px"  OnClick=""/>--%>
        </div>
        <asp:HiddenField ID="hdnPOMValue" runat="server" />
    </div>
</asp:Content>
