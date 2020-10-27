<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_PaymentTransactions.aspx.cs" Inherits="Bill_Sys_PaymentTransactions" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();
    </script>

    <script language="javascript" type="text/javascript">
            	
            function checkAmount()
            {
               // float ChequeAmount =parseFloat(document.getElementById('txtChequeAmount').value);
               // float balance = parseFloat(document.getElementById('txtBalance').value);
               //alert(ChequeAmount + ' ' + balance);
                if (parseFloat(document.getElementById('txtChequeAmount').value) > parseFloat(document.getElementById('txtBalance').value))
                {
                   //document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                    document.getElementById('txtChequeAmount').focus();
                    document.getElementById('tdLitti_Write').style.visibility = 'hidden'; 
                    //return false;
                }
               else if (parseFloat(document.getElementById('txtChequeAmount').value) == parseFloat(document.getElementById('txtBalance').value))
                {
                      //document.getElementById('ErrorDiv').innerHTML='';
                    document.getElementById('tdLitti_Write').style.visibility = 'hidden'; 
                    //return false;
                }
                else
                {
               // document.getElementById('ErrorDiv').innerHTML='';
                    document.getElementById('tdLitti_Write').style.visibility = 'visible'; 
                }
            }
            function check()
            {
                formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');
            } 
            
            function ooValidate()
            {             
  
                   var plz =  document.getElementById('_ctl0_ContentPlaceHolder1_rngDate').style.visibility ;
                 
                     if(plz=="visible")
                     {
                   
                        return false;
                     }
                       
                  var status=formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');
                 
                
                  if (status != false)
                  {
                    var chequeamt = document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value;
                //alert(chequeamt);
                    var balance = document.getElementById('_ctl0_ContentPlaceHolder1_txtBalance');
                //alert(balance);
                       
                           
                      
                          var radio= document.getElementById('_ctl0_ContentPlaceHolder1_rdbList_2');
               if(radio.checked)
               {
                          

                          
                if(balance != "" && balance != null)
                {        
                    //alert(document.getElementById('_ctl0_ContentPlaceHolder1_txtBalance').value);
                    //alert(document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                    if(document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value == "")
                    {
                       
                             
                        if (status != false)
                        {
                           
                            if (parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value) >parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtBalance').value))
                            {
                                if(confirm('Entered check amount is greater than bill amount , Do you want to proceed?'))
                                {           
                                    return true;
                                }
                                else
                                {
                                   // document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                                   document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').focus();
                                    return false;
                                }
                            }
                            else
                            {
                                if (document.getElementById('tdLitti_Write').style.visibility == 'hidden')
                                {
                                    test();

                                }
                                else
                                {
                                   // document.getElementById('ErrorDiv').innerHTML='Please select  Litigation,Write off or Cancel';

                                    return  false;
                                }
                            }
                            
                        }
                        else
                        {
                            return  false;
                        }
                    }
                    else
                    {
                        var status=formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');

                        var chk1 = parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtBalance').value);
                        var chk2 = parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                        var chk3 = chk1 + chk2;
                        //alert(chk3);
                        if (status!=false)
                        {
                            if(parseFloat(chk3) >= parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value))
                            {
                                //alert("1");
                            }
                            else
                            {
                                //alert("2");
                                if (parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value) > parseFloat(chk3))
                                {
                                    if(confirm('Entered check amount is greater than bill  amount, Do you want to proceed?'))
                                    { 
                                    
                                        return true;
                                    }
                                    else
                                    {
                                        //document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                                      document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').focus();
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (document.getElementById('tdLitti_Write').style.visibility == 'hidden')
                                    {
                                        test();
                                    }
                                    else
                                    {
                                       // document.getElementById('ErrorDiv').innerHTML='Please select  Litigation,Write off or Cancel';

                                        return  false;
                                    }
                                }
                            }    
                        }
                        else
                        {
                            return  false;
                        }
                    }
                }
                else
                {
                    var chequeamt1 = document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value;
                    //alert(chequeamt1);
                    var postbalance = (document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value);
                    //alert(postbalance);
                    var hdcheckamount1 = (document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                    //alert(hdcheckamount1);
                    
                    
                    
                    if(postbalance != "")
                    {
                        
                        if(document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value == "")
                        {
                            //alert(postbalance);
                            var status=formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');
                    
                            if (status!=false)
                            {
                               
                                if (parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value) > parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value))
                                {          
                                    if(confirm('Entered check amount is greater than bill amount, Do you want to proceed?'))
                                    {    
                                        return true;
                                    }
                                    else
                                    {
                                       //document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                                        document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').focus();
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (document.getElementById('tdLitti_Write').style.visibility == 'hidden')
                                    {
                                        test();

                                    }
                                    else
                                    {
                                        //document.getElementById('ErrorDiv').innerHTML='Please select  Litigation,Write off or Cancel';

                                        return  false;
                                    }
                                }
                                
                            }
                            else
                            {
                                return  false;
                            }
                        }
                        else
                        {
                            var chk1 = parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value);
                            var chk2 = parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                            var chk3 = chk1 + chk2;
                            //alert(chk3);
                            var hdcheckamount1 = (document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                            //alert(hdcheckamount1);
                            var status=formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');
                        
                            if (status!=false)
                            {
                                
                                if(parseFloat(chk3) >= parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value))
                                {
                                    //alert("1");
                                }
                                else
                                {
                                    //alert("2");
                                    if (parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value) > parseFloat(chk3))
                                    {
                                        if(confirm('Entered check amount is greater than bill amount, Do you want to proceed?'))
                                        {    
                                            return true;
                                        }
                                        else
                                        {
                                            //document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                                          document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').focus();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (document.getElementById('tdLitti_Write').style.visibility == 'hidden')
                                        {
                                            test();
                                        }
                                        else
                                        {
                                           //document.getElementById('ErrorDiv').innerHTML='Please select  Litigation,Write off or Cancel';

                                            return  false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return  false;
                            }
                        }
                    }
                 }
                 }
                 
                 }else
                 {
                    return  false;
                 }
                
            }
            
            function ooValidateupdate()
            {
                var chequeamt = document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value;
                alert(chequeamt);
                var postbalance = (document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value);
                alert(postbalance);
                var hdcheckamount = (document.getElementById('_ctl0_ContentPlaceHolder1_txthdcheckamount').value);
                alert(hdcheckamount);
                
                var chk1 = parseInt(document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value);
                var chk2 = parseInt(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value);
                var chk3 = chk1 + chk2;
                alert(chk3);
                if(postbalance != "")
                {
                    var status=formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');
                    
                    if (status!=false)
                    {
                        //int checkbalance = parseINT(document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value 
                        //alert(checkbalance);
                        //if(parseINT(postbalance) >= 0)
                        //{
                            //alert("1");
                            //return false;
                        //}
                        //else
                        //{
                            //alert("2");
                            if (parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').value) > parseFloat(document.getElementById('_ctl0_ContentPlaceHolder1_txtPostBalance').value))
                            {
                                if(confirm('Entered check amount is greater than bill amount, Do you want to proceed?'))
                                {    
                                    return true;
                                }
                                else
                                {
                                    //document.getElementById('ErrorDiv').innerHTML='Check amount should be equal or less than bill amount';
                                    document.getElementById('_ctl0_ContentPlaceHolder1_txtChequeAmount').focus();
                                    return false;
                                }
                            }
                            else
                            {
                                if (document.getElementById('tdLitti_Write').style.visibility == 'hidden')
                                {
                                    test();

                                }
                                else
                                {
////                                    document.getElementById('ErrorDiv').innerHTML='Please select  Litigation,Write off or Cancel';

                                    return  false;
                                }
                            }
                         //}

                    }
                    else
                    {
                        return  false;
                    }
                }
                
            }
            
         function ascii_value(c){
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

        function CheckForInteger(e,charis)
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
         function test()
            {
                if(formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType')==undefined)
                {    
                    if (confirm('Click ok to write off, Click cancel to continue adding more bills'))
                      {
                          document.getElementById('hiddenconfirmBox').value=1; 
                      }
                      else
                      {
                         document.getElementById('hiddenconfirmBox').value=0;
                      }
                 }
                 else
                 {      
                        return false;
                 }
            }
            
            function AddComment()
            {
                var comment = document.getElementById('txtComment');
                if (comment.value=='')
                {
                
                    alert('Please enter the Comment First ... !');
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            
            
            function showUploadFilePopup()
       {
      
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '200px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left ='350px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       
       
       
        function FromDateValidation()
      {
         var year1="";
         year1=document.getElementById("<%=txtChequeDate.ClientID%>").value;                   
         if(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_')
         {
             return true;   
         }
         
         
         if((year1.charAt(6)=='_' && year1.charAt(7)=='_') || (year1.charAt(8)=='_' && year1.charAt(9)=='_') || (year1.charAt(6)=='0' && year1.charAt(7)=='0') || (year1.charAt(6)=='0') || (year1.charAt(9)=='_'))
         {
                     document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = 'Date is Invalid';
         
            document.getElementById("<%=txtChequeDate.ClientID%>").focus();
            return false; 
         }
         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
         {
            document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = '';
            return true;
         }
        
      }
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <%--<tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td colspan="4" style="width: 100%" class="TDPart">
                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                      
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left;">
                                                    <b>Payment Details</b>
                                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 100%; text-align:left; height: 18px;">
                                                     <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                        <div id="ErrorDivServer" runat="server" style="color: red" visible="true">
                                            &nbsp;</div>
                                            </td>
                                                <td style="width: 100%; text-align: right; height: 18px;">
                                        
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" class="SectionDevider">
                                                    <asp:DataGrid ID="grdPaymentTransaction" runat="server" OnDeleteCommand="grdPaymentTransaction_DeleteCommand"
                                                        OnPageIndexChanged="grdPaymentTransaction_PageIndexChanged" OnSelectedIndexChanged="grdPaymentTransaction_SelectedIndexChanged"
                                                        OnItemCommand="grdPaymentTransaction_ItemCommand" CssClass="GridTable" AutoGenerateColumns="false"
                                                        Width="100%">
                                                        <FooterStyle />
                                                        <SelectedItemStyle />
                                                        <PagerStyle />
                                                        <AlternatingItemStyle />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                            <asp:BoundColumn DataField="SZ_PAYMENT_ID" HeaderText="Payment ID" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CHECK_NUMBER" HeaderText="Check Number"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_PAYMENT_DATE" HeaderText="Posted Date" DataFormatString="{0:MM/dd/yyyy}">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Check Date" DataFormatString="{0:MM/dd/yyyy}">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="FLT_CHECK_AMOUNT" HeaderText="Check Amount" DataFormatString="{0:0.00}"
                                                                ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_COMMENT" HeaderText="Comment" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="I_PAYMENT_TYPE" HeaderText="Payment Type" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Check">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan"
                                                                        Text="Scan" OnClick="lnkscan_Click"></asp:LinkButton>/<asp:LinkButton ID="lnkuplaod"
                                                                            runat="server" CausesValidation="false" CommandName="Upload" Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="SZ_PAYMENT_DOCUMENT_LINK" HeaderText="View" Visible="True">
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
                                                    <%--<input type="hidden"id ="hiddentxtchkamt" runat = "server" value="" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left;">
                                                    <b>Payment Transaction</b>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 9px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>&nbsp;<div
                                                        id="Div1" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill No :</td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblCaseNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Visit Date :</td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblVisitDate" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Posted Date :
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblPosteddate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Check Number :</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtChequeNumber" runat="server" Width="80%"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Check Date :
                                                </td>
                                                <td style="width: 35%">
                                                   <asp:TextBox ID="txtChequeDate" runat="server" Width="150px" MaxLength="10" ></asp:TextBox> 
                                                       <asp:ImageButton ID="imgbtnChequeDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                  <%--<asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>--%>
                                                    <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtChequeDate"
                                                        PopupButtonID="imgbtnChequeDate" />  
                                                        
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtChequeDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtChequeDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                        
                                                   <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtChequeDate" type="Date" minimumvalue="01/01/1901" maximumvalue="12/31/2099" errormessage="Enter a valid date " />
                                                       
                                                   <%-- <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtChequeDate"
                                                        ErrorMessage="Enter valid date" MaximumValue="12/01/2099" MinimumValue="01/01/1901"
                                                        Type="Date"></asp:RangeValidator>--%></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 38px;">
                                                    Check Amount&nbsp; :</td>
                                                <td style="width: 35%; height: 38px;">
                                                    <asp:TextBox ID="txtChequeAmount" runat="server" Width="80%" MaxLength="50" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox></td>
                                                <asp:Label ID="lblPrevAmount" runat="server" Visible="false"></asp:Label><td class="ContentLabel"
                                                    style="width: 15%; height: 38px;">
                                                    Balance&nbsp;:</td>
                                                <td style="width: 35%; height: 38px;">
                                                    <asp:TextBox ID="txtPostBalance" runat="server" Width="80%" MaxLength="50" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" ForeColor="Black" Font-Bold="true"
                                                        Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtBalance" runat="server" Width="150px" MaxLength="50" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" ForeColor="Black"></asp:TextBox></td>
                                            </tr>
                                            <tr id="tdComment">
                                                <td class="ContentLabel" style="width: 15%" valign="top">
                                                    Comment &nbsp;:</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtComment" runat="server" MaxLength="255" Width="80%" TextMode="MultiLine"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; text-align: left;">
                                                    <asp:RadioButtonList ID="rdbList" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Litigation"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Write-Off"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="Save" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr id="tdLitti_Write" runat="server" visible="false">
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:Button ID="btnLitigation" runat="server" CssClass="Buttons" Text="Litigation"
                                                        Width="80px" OnClick="btnLitigation_Click" /><asp:Button ID="btnWriteoff" runat="server"
                                                            CssClass="Buttons" Text="Write off" Width="80px" OnClick="btnWriteoff_Click" /><asp:Button
                                                                ID="btnCancel" runat="server" CssClass="Buttons" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                            <tr id="tdAddUpdate" runat="server">
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="Buttons" OnClick="btnSave_Click"
                                                        Text="Add" Width="80px"  CausesValidation ="true" />
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="Buttons" OnClick="btnUpdate_Click"
                                                        Text="Update" Width="80px" CausesValidation="true"  />
                                                    <asp:Button ID="Button1" runat="server" CssClass="Buttons" Text="Cancel" Width="80px"
                                                        OnClick="btnCancel_Click" /><asp:Button ID="btnClear" runat="server" CssClass="Buttons"
                                                            OnClick="btnClear_Click" Text="Clear" Width="80px" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                &nbsp;&nbsp;
                            </table>
                            <asp:TextBox ID="txtPaymentType" runat="server" Width="150px" Text="3" MaxLength="50"
                                Visible="false"></asp:TextBox><asp:TextBox ID="txtCompanyID" runat="server" Visible="false"
                                    Width="10px"></asp:TextBox><asp:TextBox ID="txtBillNo" runat="server" Visible="false"
                                        Width="10px"></asp:TextBox><asp:TextBox ID="txtUserId" runat="server" Visible="false"
                                            Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtBillStatusId" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            
                            <input type="hidden" id="hiddenconfirmBox" name="hiddenconfirmBox" />
                            <asp:TextBox ID="txthdcheckamount" runat="server" Width="0%"></asp:TextBox></td>
                        <td class="RightCenter" style="height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
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
                                    OnClick="btnUploadFile_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
