<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_VerificationRequestPopup.aspx.cs"
    Inherits="Bill_Sys_VerificationRequestPopup" EnableEventValidation="false" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 <%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
        $(document).ready(function () {
            $('.scanlbl').click(function () {
                debugger;
                var data = $(this).attr('data-val');
                var dataArray = data.split(',');
                var caseid = $('[id$=hdnCaseId]').val();
                var billNo = $('[id$=hdnBillNo]').val();
                var TypeID = dataArray[0];
                var specialtyID = dataArray[1];             
                var I_TYPE_ID = dataArray[2];
                if (I_TYPE_ID == 'den') {
                    scanVerificationDenial(caseid, specialtyID, billNo, TypeID,'5');
                }
                else if (I_TYPE_ID == 'vr' || I_TYPE_ID == 'vs') {
                    scanBillVerification(caseid, specialtyID, TypeID, billNo,'5');
                }
            });
        });
    </script>

    <script type="text/javascript">
        function CheckedBillStatus()
        {
            var chkStatus = document.getElementById('extBillStatus').value;
            if(chkStatus == 'NA')
            {
                alert('Select Bill Status!!!');
                return false;
            }
            else
            {
                    var year1="";
                 year1=document.getElementById("txtSaveDate").value;                   
                // alert(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_'
                 if((year1.charAt(0)!='' && year1.charAt(1)!='' && year1.charAt(2)=='/' && year1.charAt(3)!='' && year1.charAt(4)!='' && year1.charAt(5) == '/' && year1.charAt(6)!='' && year1.charAt(7)!='' && year1.charAt(8)!='' && year1.charAt(9)!='' && year1.charAt(6)!='0'))
                 {
                     return true;   
                 }
                 else
                  {
                    alert("Select verification received date .");
                    return false;
                 }
            }
          
        }
        
        
        
        function CheckedDate()
        {
         //alert("");
            var year1="";
         year1=document.getElementById("txtSaveDate").value;                   
         if(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_')
         {
             return true;   
         }
         else
         {
            alert("Please select verification received date.");
            return false;
         }
        }
        
        
        
        
        
        function date_Required()
        {
       
            if(document.getElementById('txtSaveDate').value == '')
            {
                alert('Please Select Verification Date.');
                document.getElementById('txtChequeDate').focus();
                return false;
            }          
        }
        
        
        function CheckDenialSelect()
         {
                var vlength = document.getElementById("lbSelectedDenial").length;
                if (vlenght == 0)
                {
                    alert("Add denial reason");
                }
        }
       
        function confirm_bill_delete()
		 {
		        
		       var f= document.getElementById("grdVerificationReq");
		        var bfFlag = false;
		        var index = document.getElementById("hfindex").value;	
		        //alert(index);
		       
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				    
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') !=-1)
		            {
		               
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                   if (i == index)
			                   {
			                        if (confirm("Bille status has been changed POM received? do you want to allow changed bill status..")) {
			                            document.getElementById("hfconfirm").value = 'yes';
			                            
			                        }
			                        else {
			                            document.getElementById("hfconfirm").value = 'no';
			                                
			                        }
			                   }
			                   else 
			                   {
			                        document.getElementById("hfconfirm").value = 'delete';
			                   }
			                    bfFlag = true;
			                }
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		           return false;
		        }
		        else {
		        if (confirm("Are you sure want to Delete?")){
		            return true;
		            }
		            else{
		                return false;
		            }
		        }
		}
		
		
		function checkdenial()
        {
            //alert("call");
            if(document.getElementById("lbSelectedDenial").length <=0)  
                {      
                    alert("Select denial Reason atleast one!")  
                    return false; 
                }
           if(document.getElementById("txtsavedate1").value =="")  
                {      
                    alert("Select denial Date!")  
                    return false; 
                }
                
                 return true;
        }
		
	
    function  addDenial()
      {
     // alert("ok");
       
        var e = document.getElementById("extddenial");
       // alert(e);
        var user = e.options[e.selectedIndex].text;
        
       
      if (user == "---Select---") 
      {
        alert ("Please select Denial Reason!");
        return false;
      }
     
     //alert(user);
        var vlength = document.getElementById("lbSelectedDenial").length;
      
        
      // alert(vlength);
        var status = "0";
        
        var i;
        if (vlength != 0)
        {
            for (i =0; i < vlength;i++) 
            {
                if (e.options[e.selectedIndex].value == document.getElementById("lbSelectedDenial").options[i].value)
                {
                    alert ("Denial reason already exists");
                    status = "1";
                    return false;
                }
            }
        }
        
        if (status != "1") 
        {
         // alert("if");
         document.getElementById("hfdenialreason").value = document.getElementById("hfdenialreason").value +  e.options[e.selectedIndex].value + ",";      
         // alert("if");
         var optn = document.createElement("OPTION");
        optn.text = e.options[e.selectedIndex].text;
        optn.value = e.options[e.selectedIndex].value;                           
        document.getElementById("lbSelectedDenial").options.add(optn); 
        document.getElementById("lslListBox").options.add(optn);

        return false;	
        }
      } 
      
      function RemoveDenial()
        {    
           // alert("ok");
        if(document.getElementById("lbSelectedDenial").length <=0)  
          {      
           alert("No Denial reason available to remove.")  
           document.getElementById("lbSelectedDenial").focus(); 
           return false; 
            } 
          else if(document.getElementById("lbSelectedDenial").selectedIndex < 0) 
            {  
              alert("Please Select Denail reason to Remove.");  
              document.getElementById("lbSelectedDenial").focus();   
              return false;   
            } 
          else 
            {   
            var e = document.getElementById("lbSelectedDenial");
            var user = e.options[e.selectedIndex].value;
            //alert(user);
            document.getElementById("hfremovedenialreason").value = document.getElementById("hfremovedenialreason").value + e.options[e.selectedIndex].value + ",";
           document.getElementById("lbSelectedDenial").options[document.getElementById("lbSelectedDenial").selectedIndex] = null; 
            
            var list=document.getElementById("lbSelectedDenial");
            var items=list.getElementsByTagName("option");
            
             
            
             return false;   
            }
         } 
         
         
		function showUploadFilePopup()
       {
      
            document.getElementById("pnlUploadFile").style.height='100px';
            document.getElementById("pnlUploadFile").style.visibility = 'visible';
            document.getElementById("pnlUploadFile").style.position = "absolute";
	        document.getElementById("pnlUploadFile").style.top = '100px';
	        document.getElementById("pnlUploadFile").style.left ='200px';
	        document.getElementById("pnlUploadFile").style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById("pnlUploadFile").style.height='0px';
            document.getElementById("pnlUploadFile").style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       
        
        
        
        function FromDateValidation()
      {
         //alert("call");
         var year1="";
         year1=document.getElementById("txtSaveDate").value;                   
        // alert(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_'
         if((year1.charAt(0)!='' && year1.charAt(1)!='' && year1.charAt(2)=='/' && year1.charAt(3)!='' && year1.charAt(4)!='' && year1.charAt(5) == '/' && year1.charAt(6)!='' && year1.charAt(7)!='' && year1.charAt(8)!='' && year1.charAt(9)!='' && year1.charAt(6)!='0'))
         {
             return true;   
         }
         
         
         if((year1.charAt(6)=='_' && year1.charAt(7)=='_') || (year1.charAt(8)=='_' && year1.charAt(9)=='_') || (year1.charAt(6)=='0' && year1.charAt(7)=='0') || (year1.charAt(6)=='0') || (year1.charAt(9)=='_'))
         {
                     document.getElementById('lblValidator1').innerText = 'Date is Invalid';
         
            document.getElementById("txtSaveDate").focus();
            return false; 
         }
         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
         {
            document.getElementById('lblValidator1').innerText = '';
            return true;
         }
        
      }
		
    </script>

</head>
 
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div>
            <table width="100%">
                <tr>
                    <td class="lbl" valign="top">
                        Bill Number
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true"></asp:TextBox>
                    </td>
                    <td class="lbl" valign="top">
                        Visit Date
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtVisitDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan ="5">
                        <UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <%--<tr>
                                <td align="right" valign="top">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="height: auto" valign="top">
                                   <%-- <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                        Width="100%" OnItemCommand="grdVerificationReq_ItemCommand" CssClass="GridTable">
                                        <ItemStyle CssClass="GridRow" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <Columns>
                                       
                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                <ItemStyle CssClass="grid-item-left" />
                                            </asp:ButtonColumn>
                                            
                                            <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>
                                           
                                            <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                          
                                            <asp:BoundColumn DataField="DATE" HeaderText="Date"></asp:BoundColumn>
                                      >
                                            <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                         
                                            <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                          
                                            <asp:BoundColumn DataField ="SZ_DENIAL_REASONS" HeaderText="Denial reason"></asp:BoundColumn> 
                                       
                                           
                                            <asp:TemplateColumn HeaderText="Verification">
                                                    <ItemTemplate>
                                                        <asp:LinkButton  ID ="lnkscan" runat ="server" CausesValidation="false" CommandName="Scan" Text="Scan" OnClick="lnkscan_Click"></asp:LinkButton>/
                                                        <asp:LinkButton  ID ="lnkuplaod" runat ="server" CausesValidation="false" CommandName="Upload" Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                           
                                            <asp:BoundColumn DataField="I_TYPE_ID" Visible="false"></asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField ="sz_bill_status" Visible="true"></asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="DT_CREATED_DATE" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                       
                                    </asp:DataGrid>--%>
                                    
                                    <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                        Width="100%" OnItemCommand="grdVerificationReq_ItemCommand" CssClass="GridTable">
                                        <ItemStyle CssClass="GridRow" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <Columns>
                                       <%-- 0--%>
                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                <ItemStyle CssClass="grid-item-left" />
                                            </asp:ButtonColumn>
                                                                                   <%-- 1--%>
                                            <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>
                                                                                   <%-- 2--%>
                                            <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                                                                   <%-- 3--%>
                                            <asp:BoundColumn DataField="DATE" HeaderText="Date"></asp:BoundColumn>
                                                                                   <%-- 4--%>
                                            <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                                                                   <%-- 5--%>
                                            <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                             <%-- 6--%>
                                            <asp:BoundColumn DataField ="SZ_DENIAL_REASONS" HeaderText="Denial reason"></asp:BoundColumn> 
                                              <%-- 7--%>
                                             <asp:BoundColumn DataField ="sz_answer" HeaderText="Answer"></asp:BoundColumn> 
                                              <%--  8--%> 
                                            <asp:BoundColumn DataField ="sz_ans_user_name" HeaderText="User Name"></asp:BoundColumn> 
                                              <%--  9--%>
                                            <asp:BoundColumn DataField ="sz_date_of_answer" HeaderText="Answered Date"></asp:BoundColumn> 
                                             <%-- 10/7--%>
                                            <asp:TemplateColumn HeaderText="Verification">
                                                    <ItemTemplate>
                                                        <asp:LinkButton  ID ="lnkscan" runat ="server" CausesValidation="false" CommandName="Scan" Text="Scan" Visible="false" OnClick="lnkscan_Click"></asp:LinkButton>
                                                        <asp:LinkButton  ID ="lnkuplaod" runat ="server" CausesValidation="false" CommandName="Upload" Visible="false" Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                        <a id="caseDetailScan" href="#" runat="server" data-val='<%# Eval("TYPEID")+","+ Eval("SZ_SPECIALITY_ID")+","+ Eval("I_TYPE_ID") %>'
                                                    title="Scan/Upload" class="lbl scanlbl">Scan/Upload</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <%-- 11/8--%>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                             <%-- 12/9--%>
                                            <asp:BoundColumn DataField="I_TYPE_ID" Visible="false"></asp:BoundColumn>
                                             <%-- 13/10--%>
                                            <asp:BoundColumn DataField ="sz_bill_status" Visible="true"></asp:BoundColumn>
                                             <%-- 14/11--%>
                                            <asp:BoundColumn DataField="DT_CREATED_DATE" Visible="false"></asp:BoundColumn>
                                            
                                                <%-- 15--%>
                                            <asp:BoundColumn DataField="sz_answer_id" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                       
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                   
                    <td class="lbl" valign="top">
                       <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lbl" Visible="false"></asp:Label>
                    </td>
                    <td valign="top">
                    <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')" Visible="false">
                    </asp:TextBox><asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/>
                         <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtSaveDate" PopupButtonID="imgSavebtnToDate" />
                       <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                     <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator> 
                    
                    
                    <%--    <asp:TextBox ID="txtSaveDate1" runat="server"   onkeypress="return CheckForInteger(event,'.')" Visible="false"></asp:TextBox>
                  <asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/>
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSaveDate"
                     PopupButtonID="imgSavebtnToDate" />--%>
                        <asp:TextBox ID="txtVerificationDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true" Visible ="false"> </asp:TextBox>
                    </td>
                    <%--<td align="right" valign="top" colspan="2">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                    </td>--%>
                </tr>
                
                <tr valign="top">
                   
                    <td class="lbl" valign="top">
                       <asp:Label ID="lbldenialdate" runat="server" Text=" Denial Date" CssClass="lbl" Visible="false"></asp:Label>
                    </td>
                    <td valign="top">
                    <asp:TextBox ID="txtSaveDate1" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')" Visible="false">
                    </asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/>
                         <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSaveDate1" PopupButtonID="ImageButton1" />
                       <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSaveDate1" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                     <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtSaveDate1" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator> 
                    
                    
                    <%--    <asp:TextBox ID="txtSaveDate1" runat="server"   onkeypress="return CheckForInteger(event,'.')" Visible="false"></asp:TextBox>
                  <asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/>
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSaveDate"
                     PopupButtonID="imgSavebtnToDate" />--%>
                        <asp:TextBox ID="TextBox2" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true" Visible ="false"> </asp:TextBox>
                    </td>
                    <%--<td align="right" valign="top" colspan="2">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                    </td>--%>
                </tr>
                
                
                
                
                <tr>
                    <td valign="top" class="lbl">
                        Notes
                    </td>
                    <td colspan="1" valign="top">
                        <asp:TextBox ID="txtVerificationNotes" runat="server" TextMode="MultiLine" Height="80px"
                            Width="100%"></asp:TextBox>
                    </td>
                    <td  valign="top" class="lbl" >
                    &nbsp; &nbsp; &nbsp;<asp:Label ID="lblAns" runat="server" Text="Answer" ></asp:Label>
                    </td>
                     <td colspan="1" valign="top" align="right">
                        <asp:TextBox ID="txtVerificationAns" runat="server" TextMode="MultiLine" Height="80px"
                            Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="lbl">
                        Bill Status
                    </td>
                    <td colspan="3" valign="top">
                        <extddl:ExtendedDropDownList ID="extBillStatus" Width="90%" runat="server" Connection_Key="Connection_String"
                            Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_VERIFICATION_BILL_STATUS_NEW"
                            Selected_Text="--- Select ---" OnextendDropDown_SelectedIndexChanged ="extendDropDown_SelectedIndexChanged" AutoPost_back="true"/>
                       <asp:Label ID="lblBillStatus" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
              <tr>
                <td>
                    <asp:Label ID="lblDenial" Text="Denial Reason" runat='server' Visible="false"></asp:Label>
                </td>
                <td>
                    <extddl:ExtendedDropDownList ID="extddenial" Width="140px"
                     runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_DENIAL"
                       Flag_Key_Value="DENIAL_LIST" Selected_Text="--- Select ---" CssClass="cinput" Visible="false" />
                      
                        <input type="button" class="Buttons" value="+" Height="5px" Width="5px" ID="btnAddDenial"  onclick="addDenial();" runat="server" Visible="false"/>
                       <input type="button" class="Buttons" value="~" Height="5px" Width="5px" ID="btnRemoveDenial"  onclick="RemoveDenial();" runat="server" Visible="false"/>
                    
                </td>
                
              </tr>
              <tr>
              <td>
              <asp:Label ID="lblselect" Text="Selected reason" runat="server" Visible="false"></asp:Label>
              </td>
              <td colspan="2">
                       <asp:ListBox ID="lbSelectedDenial" runat="server" width="100%" Visible="false"> </asp:ListBox>
              </td>
                <td>
                    <asp:ListBox runat="server" Height="60%" Width="100%" ID="testlist" Visible="false"></asp:ListBox>
                   
                </td>
               
                 
              </tr>
                <tr>
                    <td colspan="2" width="350px" align="right">
                        <asp:Button ID="btnSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />
                        <asp:Button ID="btnSaveden" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />
                        <asp:Button ID="btnSavesent" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />
                        
                    </td>
                    <td colspan="2" width="350px" align="left">
                        <asp:Button ID="btnUpdate" Text="Update" runat="Server" CssClass="Buttons" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnUpdateden" Text="Update" runat="Server" CssClass="Buttons" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnUpdatesent" Text="Update" runat="Server" CssClass="Buttons" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" Text="Clear" runat="Server" CssClass="Buttons" OnClick="btnClear_Click" />
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="2%"></asp:TextBox>
                        <asp:Button ID ="btnConfirm" runat="server" Visible="false" OnClick="btnConfirm_Click" />
                         
                    </td>
                </tr>
                <tr>
                    <td.>
                        <asp:HiddenField runat="server" ID="hfdenialreason"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hfremovedenialreason" />
                        <asp:HiddenField runat="server" ID="hfconfirm" />
                        <asp:HiddenField runat ="server" ID ="hfindex" Value="no" />
                        <asp:HiddenField runat="server" ID="hfverificationId" />
                        <asp:HiddenField ID="hdnCaseId" runat="server" />
                        <asp:HiddenField ID="hdnBillNo" runat="server" />
                    </td.>
                    
                </tr>
            </table>
            
             <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
       
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
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons" OnClick="btnUploadFile_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         
    </asp:Panel>
        </div>
        
      
    </form>
</body>
</html>
