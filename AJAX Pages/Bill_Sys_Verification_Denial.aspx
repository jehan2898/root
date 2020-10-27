<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Verification_Denial.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Verification_Denial" %>

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

       function checkdenial()
        {            
             var year1="";
             year1=document.getElementById("<%=txtSaveDate.ClientID %>").value; 
            if(document.getElementById("lbSelectedDenial").length <=0)
                {      
                    alert("Select denial Reason atleast one!")  
                    return false; 
                }
             else if(!(year1.charAt(0) !='_' && year1.charAt(1)!='_' && year1.charAt(2)=='/' && year1.charAt(3)!='_' && year1.charAt(4)!='_' && year1.charAt(5)=='/' && year1.charAt(6)!='_' && year1.charAt(7)!='_' && year1.charAt(8)!='_' && year1.charAt(9)!='_'))
              {
                  alert("Please select verification received date.");
                  return false;
              }
             else
              {
               return true;
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
         

        
        function  AddDenial()
      {
        var e = document.getElementById("extddenial");
        var user = e.options[e.selectedIndex].text;
      if (user == "---Select---") 
      {
        alert ("Please select Denial Reason!");
        return false;
      }
     
     //alert(user);
        var vlength = document.getElementById("<%=lbSelectedDenial.ClientID%>").length;
      
        
       //alert(vlength);
        var status = "0";
        
        var i;
        if (vlength != 0)
        {
            for (i =0; i < vlength;i++) 
            {
                if (document.getElementById("extddenial").value == document.getElementById("<%=lbSelectedDenial.ClientID %>").options[i].value)
                {
                    alert ("Denial reason already exists");
                    status = "1";
                    return false;
                }
            }
        }
        if (status != "1") 
        {
             //alert(e.options[e.selectedIndex].text);
             document.getElementById("<%=hfdenialReason.ClientID %>").value =  document.getElementById("<%=hfdenialReason.ClientID %>").value + e.options[e.selectedIndex].value + ",";
             var optn = document.createElement("OPTION");
        optn.text = e.options[e.selectedIndex].text;
        optn.value = e.options[e.selectedIndex].value;                           
        document.getElementById("<%=lbSelectedDenial.ClientID %>").options.add(optn); 
        return false;	
        }
      }  
      
      
      function RemoveDenial()
        {    
          
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
       
       
       function checkedDate()
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
        
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 1%">
             <tr>
                 <td colspan ="2">
                        <UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
                    
                    </td>  
                     </tr>
                <tr>
                    <td style="height: 16px">
                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Font-Bold="True" Text="Bill Number"></asp:Label></td>
                    <td style="height: 16px">
                        <asp:Label ID="txtViewBillNumber" runat="server" CssClass="lbl" Font-Bold="True"></asp:Label></td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <asp:DataGrid ID="grdVerificationDen" runat="server" AutoGenerateColumns="false"
                            Width="100%" CssClass="GridTable">
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                       <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number" ></asp:BoundColumn>
                                             <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" ></asp:BoundColumn>
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
                        <asp:Label ID="lblSaveDatevalue" runat="server" Text="" CssClass="lbl" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"
                            Visible="false">
                        </asp:TextBox><asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                        <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red" CssClass="lbl"></asp:Label>
                        <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtSaveDate"
                            PopupButtonID="imgSavebtnToDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Description" runat="server" Text="Description" Font-Bold="true" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSaveDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:TextBox ID="txtCompanyID" runat="server" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"
                            Visible="false" Width="150px">
                             
                        </asp:TextBox>
                        <asp:TextBox ID="txtTest" runat="server" Visible="false" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDenial" Text="Denial Reason" runat="Server" Font-Bold="true" CssClass="lbl"></asp:Label></td>
                    <td>
                        <extddl:ExtendedDropDownList ID="extddenial" Width="140px" runat="server" Connection_Key="Connection_String"
                            Procedure_Name="SP_MST_DENIAL" Flag_Key_Value="DENIAL_LIST" Selected_Text="--- Select ---"
                            CssClass="cinput" Visible="true" />
                        <input type="button" value="+" height="5px" width="5px" id="btnAddDenial" runat="server"
                            onclick="AddDenial();" />
                        <input type="button" value="~" height="5px" width="5px" id="btnRemoveDenial" runat="server"
                            onclick="RemoveDenial();" /></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:ListBox ID="lbSelectedDenial" Width="100%" runat="server"></asp:ListBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveDesc" runat="server" Text="Save" class="Buttons" OnClick="btnSaveDesc_Click" />
                        <asp:Button ID="btnCancelDesc" runat="server" Text="Cancel" class="Buttons" OnClick="btnCancelDesc_Click" />
                        <br />
                        <asp:HiddenField ID="hfdenialReason" runat="server" />
                        <asp:HiddenField ID="hfremovedenialreason" runat="server" />
                    </td>
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
