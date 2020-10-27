<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AddAppointment.aspx.cs"
    Inherits="Bill_Sys_AddAppointment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">    
    function DisplayControl()
    {
        document.getElementById('contentSearch').style.visibility='visible';
        document.getElementById('griddiv').style.visibility='hidden';   
        document.getElementById('searchbuttondiv').style.visibility='hidden';   
        document.getElementById('txtPatientFirstName').value='';  
        document.getElementById('txtPatientLastName').value='';
    }
    function HideControl()
    {
        document.getElementById('contentSearch').style.visibility='hidden';
        document.getElementById('griddiv').style.visibility='hidden';   
        document.getElementById('searchbuttondiv').style.visibility='visible';   
    }
   
    
    function SaveExistPatient()
        {
            document.getElementById('btnOK').click(); 
            document.getElementById('divid2').style.visibility='hidden';
        }
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';  
        }
        
         function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '100px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
        }
    
    
    </script>

    <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />

    <script>
    
//        function checkDate(year,month,day,hours,min)
//       {
//       //   alert('year : ' + year + ' month ' + month + ' day ' + day + ' hours' + hours + '  min' + min);
//          var myDate=new Date(year,month-1,day,hours,min,0);
//       //   myDate.setFullYear(year,month-1,day,hours,min,0);

//          // get Today's date from server.
//          var objYear = document.getElementById('txtTodayYear').value;
//          var objMonth = document.getElementById('txtTodayMonth').value;
//          var objDay = document.getElementById('txtTodayDay').value;
//          var objHour = document.getElementById('txtTodayYear').value;
//          var objMin = document.getElementById('txtTodayYear').value;

//          var today = new Date(objYear,objMonth,objDay,objHour,objMin,0);
//          
//          //alert('myDate ' + myDate + 'today' + today);
//          if (myDate>=today)
//          {
//                return true;
//          }
//          else
//          {
//                return false;
//          }
//       }
//    
    
//        function ValidateAllField(objBillingCompanyCheck)
//        {

//            var temp = (formValidator('form1','txtPatientFName,txtPatientLName,extddlMedicalOffice,extddlDoctor,ddlType,ddlTestNames,ddlHours,ddlEndHours'));
//            alert(temp);
//            if(temp == 'false')
//            {
//                alert(temp);
//            }
//            else
//            {
//                alert(temp);
//            }



//              var objEnteredYear = document.getElementById('txtEnteredYear').value;
//              var objEnteredMonth = document.getElementById('txtEnteredMonth').value;
//              var objEnteredDay = document.getElementById('txtEnteredDay').value;
//          
//               if(objBillingCompanyCheck=='RF')
//                {
//                
//                    var temp = (formValidator('form1','txtPatientFName,txtPatientLName,extddlMedicalOffice,extddlDoctor,ddlType,ddlTestNames,ddlHours,ddlEndHours'));
//                    alert(temp);
//                    if(temp  == 'false')
//                    {
//                           return false; 
//                    }
//                    else
//                    {
//                       
//                        if(!checkDate(objEnteredYear,objEnteredMonth,objEnteredDay,0,0)) 
//                           {
//                                if(confirm('Do you want to add visit for previous date ?'))
//                                {
//                                    return true;
//                                }
//                                else
//                                {
//                                    return false;
//                                }
//                           }
//                           else
//                           {
//                                return true;
//                           }
//                    }
//                }
////                else
////                {
////                    var temp1 = formValidator('form1','txtPatientFName,txtPatientLName,extddlDoctor,ddlType,ddlTestNames,ddlHours,ddlEndHours');
////                    if(temp1 == 'false')
////                    {
////                        return false;
////                    }
////                    else
////                    {
////                      if(!checkDate(objEnteredYear,objEnteredMonth,objEnteredDay,0,0)) 
////                       {
////                            if(confirm('Do you want to add visit for previous date ?'))
////                            {
////                                return true;
////                            }
////                            else
////                            {
////                                return false;
////                            }
////                       }
////                       else
////                       {
////                            return true;
////                       }
////                    }
////                }
//          } 


        function ValidateAllField(objBillingCompanyCheck)
        {
            var szDateFlag = document.getElementById('txtPreviousDateFlag').value;

            if(objBillingCompanyCheck == 'RF')
            {
                if(formValidator('form1','txtPatientFName,txtPatientLName,extddlMedicalOffice,extddlDoctor,ddlType,ddlHours,ddlEndHours,extddlCaseType') == false)
                {
                    return false;
                }
                else
                {
                    if(szDateFlag == 'future date')
                    {
                        return true;
                    }
                    else
                    {
                       if(confirm('Do you want add visit on previous date ?'))
                       {
                            return true;
                       }
                       else
                       {
                            return false;
                       }
                    }
                }
            }
            else
            {
                //if(formValidator('form1','txtPatientFName,txtPatientLName,txtPatientPhone,extddlDoctor,ddlType,ddlHours,ddlEndHours') == false)
                if(formValidator('form1','txtPatientFName,txtPatientLName,extddlDoctor,ddlType,ddlHours,ddlEndHours') == false)
                {
                    return false;
                }
                else
                {
                    if(szDateFlag == 'future date')
                    {
                        return true;
                    }
                    else
                    {
                       if(confirm('Do you want add visit on previous date ?'))
                       {
                            return true;
                       }
                       else
                       {
                            return false;
                       }
                    }
                }
            }
        }

         
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:auto; width:100%; float:left;  ">
            <table width="50%">
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height:auto; vertical-align: top;">
                                        <tr>
                                            <td style="width: 100%; height:auto; float:left;" class="TDPart">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr id="tdSerach" runat="server">
                                                                <td colspan="6">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="100%" class="ContentTable">
                                                                                <tr>
                                                                                    <td class="ContentLabel" width="100%" colspan="6" style="text-align: left">
                                                                                        <div id="searchbuttondiv" style="visibility: visible; float: left; position: relative;
                                                                                            left: 0px; top: 0px;">
                                                                                            <input id="btnClickSearch" runat="server" type="button" value="Search" class="Buttons"
                                                                                                onclick="DisplayControl()" />
                                                                                            <asp:Button ID="btnAddPatient" runat="server" Text="Add Patient" class="Buttons"
                                                                                                OnClick="btnAddPatient_Click" />
                                                                                        </div>
                                                                                        <div id="contentSearch" style="visibility: hidden; float: left; position: relative;
                                                                                            left: 0px; top: 0px;">
                                                                                            <asp:Label ID="lblFirstname" runat="server">First Name :</asp:Label>
                                                                                            <asp:TextBox ID="txtPatientFirstName" runat="server" Width="120px"></asp:TextBox>
                                                                                            <asp:Label ID="lblLastname" runat="server">Last Name :</asp:Label>
                                                                                            <asp:TextBox ID="txtPatientLastName" runat="server" Width="120px"></asp:TextBox>
                                                                                            <asp:Button ID="btnSearhPatientList" runat="server" Text="Submit" CssClass="Buttons"
                                                                                                OnClick="btnSearhPatientList_Click" />
                                                                                            <input id="btnPatientCancel" type="button" value="Cancel" class="Buttons" onclick="HideControl()" />
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnSearhPatientList" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="100%" class="ContentTable">
                                                                                <tr>
                                                                                    <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                                                        <div id="griddiv" style="float: left; position: relative; left: 0px; top: 0px;">
                                                                                            <asp:DataGrid ID="grdPatientList" runat="server" Width="100%" CssClass="GridTable"
                                                                                                AutoGenerateColumns="false" AllowPaging="true" PageSize="3" PagerStyle-Mode="NumericPages"
                                                                                                OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged" OnPageIndexChanged="grdPatientList_PageIndexChanged">
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
                                                                                                    <asp:BoundColumn DataField="SZ_PATIENT_STATE_ID" HeaderText="State" Visible="False"></asp:BoundColumn>
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
                                                                                                    <asp:BoundColumn DataField="CHART NUMBER" HeaderText="CHART NUMBER" Visible="False">
                                                                                                    </asp:BoundColumn>
                                                                                                    
                                                                                                </Columns>
                                                                                                <PagerStyle Mode="NumericPages" />
                                                                                            </asp:DataGrid>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ContentLabel" colspan="6" style="height: 493px">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="100%">
                                                                                <%--<tr>
                                                        <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                         Add Patient 
                                                        </td>
                                                    </tr>--%>
                                                                                <tr>
                                                                                    <td class="ContentLabel" style="text-align: left; height: 20px;" colspan="6">
                                                                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                                                        <div id="ErrorDiv" style="color: red;" visible="true">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 185px">
                                                                                        <asp:Label ID="lblChartNumber" runat="serveR" Text="Chart #"></asp:Label></td>
                                                                                    <td style="width: 201px" align="right">
                                                                                        <asp:TextBox ID="txtRefChartNumber" runat="server" Style="float: right;" CssClass="cinput"
                                                                                            onkeypress="return CheckForInteger(event,'/')" ReadOnly="true" ></asp:TextBox></td>
                                                                                    <td colspan="2">
                                                                                        </td>
                                                                                    <td width="10%">
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 10%">
                                                                                        First name</td>
                                                                                    <td style="width: 201px">
                                                                                        <asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                                                    <td style="width: 10%">
                                                                                        Middle
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <asp:TextBox ID="txtMI" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        Last name
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                   
                                                                                    <td style="width: 10%">
                                                                                        Address
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPatientAddress" runat="server" ReadOnly="true" Visible="true"></asp:TextBox></td>
                                                                                    <td width="10%">
                                                                                        City
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        <asp:TextBox ID="txtPatientCity" runat="server" ReadOnly="true" Visible="True"></asp:TextBox></td>
                                                                                         <td style="width :10%">
                                                                                       State
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtState" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                                        <cc1:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                                                                    </cc1:ExtendedDropDownList></td>
                                                                                   
                                                                                </tr>
                                                                                <tr>
                                                                                 <td style="width: 185px">
                                                                                        <%--Phone--%>
                                                                                    </td>
                                                                                    <td style="width: 201px">
                                                                                       <%-- <asp:TextBox ID="txtPatientPhone" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>--%></td>
                                                                                   
                                                                                        <%--Birthdate--%>
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        <asp:TextBox ID="txtBirthdate" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                                        <%--<asp:ImageButton ID="imgbtnBirthdate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <cc1:CalendarExtender ID="calBirthdate" runat="server" PopupButtonID="imgbtnBirthdate"
                                                                TargetControlID="txtBirthdate" >
                                                            </cc1:CalendarExtender>--%>
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        <%--Age&nbsp;--%>
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        <asp:TextBox ID="txtPatientAge" runat="server" ReadOnly="true" Visible="false"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 185px">
                                                                                        <%--SS #--%>
                                                                                        Phone
                                                                                        </td>
                                                                                    <td style="width: 201px">
                                                                                        <asp:TextBox ID="txtPatientPhone" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtSocialSecurityNumber" runat="server" ReadOnly="true" Visible="false"></asp:TextBox></td>
                                                                                    <td style="width: 20%">
                                                                                        Insurance</td>
                                                                                    <td width="10%">
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="--- Select ---"
                                                                                            Width="150px" Enabled="False" />
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                        Case Type</td>
                                                                                    <td width="10%">
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---"
                                                                                            Width="150px" Enabled="False"></extddl:ExtendedDropDownList>
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" Width="150px" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                            Flag_ID="txtCompanyID.Text.ToString();" Visible="false" Enabled="False" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 185px; height: 32px;">
                                                                                        <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 201px; height: 32px;">
                                                                                        <extddl:ExtendedDropDownList ID="extddlMedicalOffice" Visible="false" runat="server"
                                                                                            Width="150px" Selected_Text="--- Select ---" Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST"
                                                                                            Connection_Key="Connection_String" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlMedicalOffice_extendDropDown_SelectedIndexChanged">
                                                                                        </extddl:ExtendedDropDownList>
                                                                                        <extddl:ExtendedDropDownList ID="extddlReferenceFacility" runat="server" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY"
                                                                                            Selected_Text="--- Select ---" Width="150px" Enabled="False" />
                                                                                    </td>
                                                                                    <td style="width: 10%; height: 32px;">
                                                                                        Referring Doctor
                                                                                    </td>
                                                                                    <td style="width: 10%; height: 32px;">
                                                                                        <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST">
                                                                                        </extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td width="10%" style="height: 32px">
                                                                                        
                                                                                        <asp:Label ID="lblTypetext" runat="server" Text="Type" Visible="false"></asp:Label>
                                                                                        <asp:Label ID="Label1" runat="server" Text="Study #" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td width="10%" style="height: 32px" align="left">
                                                                                        <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" TextAlign="Left" AutoPostBack="true" OnCheckedChanged="chkTransportation_CheckedChanged1" />
                                                                                        
                                             <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                                 <ContentTemplate>
                                                    <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="150px" Connection_Key="Connection_String" Visible="false"
                                                    Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---" Flag_Key_Value="GET_TRANSPORT_LIST" Flag_ID="txtCompanyID.Text.ToString();">
                                                    </extddl:ExtendedDropDownList>
                                                </ContentTemplate>
                                                <Triggers >
                                                    <asp:AsyncPostBackTrigger ControlID="chkTransportation" EventName="OnCheckedChanged" />
                                                </Triggers> 
                                             </asp:UpdatePanel> 
                                                
                                                                                                  <asp:DropDownList ID="ddlType" Visible="false" runat="server" Width="150px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="0"> --Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="TY000000000000000001">Visit</asp:ListItem>
                                                                                            <asp:ListItem Value="TY000000000000000002">Treatment</asp:ListItem>
                                                                                            <asp:ListItem Value="TY000000000000000003" Selected="True">Test</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txtStudyNumber" runat="server" Visible="false"></asp:TextBox>
                                                                                        <%--<asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>--%>
                                                                                        <%-- <asp:TextBox ID="txtDoctorName" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 185px;">
                                                                                        <%--Referral Sheet-INI Report--%></td>
                                                                                    <td colspan="2" >
                                                                                        <asp:FileUpload ID="flUpload" runat="server" visible="false"/>
                                                                                    </td>
                                                                                    <td style="width: 10%;">
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                    </td>
                                                                                    <td width="10%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20%" valign="top">
                                                                                        Procedure</td>
                                                                                    <td width="10%" colspan="5" style="text-align: left;">
                                                                                        <asp:ListBox ID="ddlTestNames" runat="server" Width="350px" SelectionMode="Multiple">
                                                                                        </asp:ListBox></td>
                                                                                    <%--<td width="10%">
                                                            </td>
                                                        <td width="10%">
                                                            &nbsp; <asp:DropDownList ID="ddlTestNames" runat="server" Width="150px" Visible="false" >
                                                            </asp:DropDownList>--%>
                                                                                    </td> </td>
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
                                                                                    <td style="width: 185px">
                                                                                        Notes
                                                                                    </td>
                                                                                    <td colspan="5" style="text-align: left;">
                                                                                        &nbsp;<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="584px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6" align="left" style="height: 26px">
                                                                                        <asp:DropDownList ID="ddlHours" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlMinutes" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlTime" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlEndHours" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlEndMinutes" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlEndTime" runat="server" Visible="False" Width="10px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txtTodayYear" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtTodayMonth" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtTodayDay" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtTodayHour" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtTodayMin" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtEnteredYear" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtEnteredMonth" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtEnteredDay" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtEnteredHour" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtEnteredMin" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtPreviousDateFlag" runat="server" Style="visibility: hidden" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                                        &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                                                            Width="62px" CssClass="Buttons" Visible="false"></asp:Button><asp:Button ID="btnSave"
                                                                                                runat="server" Text="Save" OnClick="btnSave_Click" Width="62px" CssClass="Buttons">
                                                                                            </asp:Button>
                                                                                        <input id="Button1" type="button" value="Cancel" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';javascript:parent.document.getElementById('frameeditexpanse').src = '';"
                                                                                            class="Buttons" />&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        
        
         <div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="msgPatientExists"  runat="server"></span><br />
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
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
        <asp:Button ID="btnOK" runat="server" Style="visibility: hidden;" CssClass="Buttons"
            Text="OK" OnClick="btnOK_Click" />
   
        
        
    
    </form>
</body>
</html>
