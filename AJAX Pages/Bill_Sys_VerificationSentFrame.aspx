<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_VerificationSentFrame.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_VerificationSentFrame" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
 
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
      

          
            function checkdate()
            {
               
                var year1="";
                year1=document.getElementById("<%=txtSaveDate.ClientID %>").value;
              if((year1.charAt(0) !='_' && year1.charAt(1)!='_' && year1.charAt(2)=='/' && year1.charAt(3)!='_' && year1.charAt(4)!='_' && year1.charAt(5)=='/' && year1.charAt(6)!='_' && year1.charAt(7)!='_' && year1.charAt(8)!='_' && year1.charAt(9)!='_'))
           {
               return true;   
            
           }
         else
         {
        
            alert("Please select verification received date.");
            return false;
            
            
         }
        
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
		
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table width="100%">
            <tr>
                    <td colspan="5">
                        <UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
                    </td>
                </tr>
                <tr>
                
                    <td class="lbl" valign="top">
                        Bill Number
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="lbl" valign="top">
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtVisitDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly="true" Visible="false"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <tr>
                                <td style="height: auto" valign="top">
                                    <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                        Width="100%" OnItemCommand="grdVerificationReq_ItemCommand" CssClass="GridTable">
                                        <ItemStyle CssClass="GridRow" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <Columns>
                                            <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID"></asp:BoundColumn>
                                            <%-- <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATE" HeaderText="Date"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="sz_bill_status" HeaderText="Bill Status"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DT_CREATED_DATE" HeaderText="Created Date"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Verification">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan"
                                                        Text="Scan" OnClick="lnkscan_Click"></asp:LinkButton>/
                                                    <asp:LinkButton ID="lnkuplaod" runat="server" CausesValidation="false" CommandName="Upload"
                                                        Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="lbl" valign="top">
                        <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lbl"></asp:Label></td>
                    <td valign="top">
                        <asp:Label ID="lblcurrntDateValue" runat="server" CssClass="lbl"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td class="lbl" valign="top">
                        <asp:Label ID="lblcurrentDate" runat="server" CssClass="lbl" Font-Bold="False" Text="Verification Date"></asp:Label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"
                            Visible="false">
                        </asp:TextBox><asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif"
                            Visible="false" />
                        <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtSaveDate"
                            PopupButtonID="imgSavebtnToDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDenial" Text="Description" runat='server'></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSaveDescription" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Button ID="btnSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnClear" Text="Clear" runat="Server" CssClass="Buttons" OnClick="btnClear_Click" />
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="0px"></asp:TextBox>
                        <asp:TextBox ID="txtTest" runat="server" Visible="False" Width="0px"></asp:TextBox></td>
                </tr>
                <tr>
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
        </div>
    </form>
</body>
</html>
