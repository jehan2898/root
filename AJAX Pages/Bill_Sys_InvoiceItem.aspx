<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_InvoiceItem.aspx.cs" Inherits="Bill_Sys_InvoiceItem" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
    
       function Validate(ival)
       {
            var check=0;
            var f= document.getElementById("<%= grdInvoiceItem.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            check=1;
			            break;
			        }
			    }			
		    }
		    if(check==1)
		    {
		       var val= ConfirmDelete();
		       
		       if(val==true)
		       {
		         return true;
		       }
		       else
		       {
		         return false;
		       }
		    }
		    else
		    {
		         alert('Please select record.');
		            return false;
		    }
		    
		   
       }
        function ConfirmDelete()
        { 
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
      
        }
       
         function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdInvoiceItem.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
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
         
         function Validation() 
         {
//                alert(formValidator('frmInvoiceitem','txtItemName,txtItemPrice'));
                
                var status = formValidator('frmInvoiceitem','txtItemName,txtItemPrice');
                if (status!=false)
                {
//                       alert("call");
                       
                        var price = document.getElementById("<%= txtItemPrice.ClientID %>").value;
//                       alert(price);
                        var check = price.split('.');
                        if (check.length > 2)
                        {
                            alert ("Enter Valid Price");
                            return false;
                            
                        }                
                        else
                        {
                             return true;
                        }      
                }else
                {
                    return false;
                }
         }
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop" style="width: 10px">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 468px; width: 10px;">
                        </td>
                        <td class="Center" valign="top" style="height: 468px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%; height: 17px;" class="TDPart">
                                        <table border="0" cellpadding="0" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 73px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <UserMessage:MessageControl ID="usrMessage" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%;">
                                                    Description
                                                </td>
                                                <td class="ContentLabel" style="width: 35%;">
                                                    <asp:TextBox ID="txtItemName" runat="server" CssClass="textboxCSS" MaxLength="250"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%;">
                                                    Price
                                                </td>
                                                <td class="ContentLabel" style="width: 35%;">
                                                    &nbsp;<asp:TextBox ID="txtItemPrice" runat="server" CssClass="textboxCSS" MaxLength="50"
                                                         onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <br />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="1px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtCreatedUserID" runat="server" Width="1px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtInvoiceId" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCreatedDate" runat="server" Width="1px" Visible="false"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="overflow: scroll; height: 300px; width: 100%;">
                                            <asp:DataGrid ID="grdInvoiceItem" runat="server"  
                                                 OnPageIndexChanged="grdInvoiceItem_PageIndexChanged" OnSelectedIndexChanged="grdInvoiceItem_SelectedIndexChanged"
                                                  Width="98%" CssClass="GridTable" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages" >
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:ButtonColumn CommandName="Select" Text="Select">
                                                        <ItemStyle CssClass="grid-item-left" />
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="I_INVOICE_ITEM_ID" HeaderText="InvoiceItemId" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FLT_PRICE" HeaderText="Price" DataFormatString="{0:000.00}"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server"  onclick="javascript:SelectAll(this.checked);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <PagerStyle Mode="NumericPages" />
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 468px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom" style="width: 10px">
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
</asp:Content>
