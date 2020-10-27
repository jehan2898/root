<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Adjuster.aspx.cs" Inherits="Billing_Sys_Adjuster" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
      <script type="text/javascript">
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
        
        
//        
//        function isLocalEmail(formID,ID)
//		{			
//			// alert('From isEmail Function');	
//			
//			if(formValidator('aspnetForm','txtAdjusterName') == true)
//		    {
//        			
//		            var ID = '_ctl0_ContentPlaceHolder1_' + ID;
//		            alert(ID);
//		            var EmailIdvalue = document.getElementById(ID).value;	
//		            alert(document.getElementById('ErrorDiv'));	
//		            var Form =  document.getElementById(formID);
//		            if(EmailIdvalue == "")
//		            {				
//		                document.getElementById(ID).style.backgroundColor = '';	
//		                document.getElementById('ErrorDiv').innerText='';
//			            return true;
//		            }
//		            if (EmailIdvalue.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
//		            {
//		                document.getElementById('ErrorDiv').innerText='';
//		                document.getElementById(ID).style.backgroundColor = '';	
//			            return true;
//		            }
//		            else
//		            {			
//			            document.getElementById('ErrorDiv').innerText='Enter valid email id ...!';
//			            document.getElementById(ID).value=""
//			            document.getElementById(ID).focus();
//			            document.getElementById(ID).style.backgroundColor = "#ffff99";	
//			            alert(document.getElementById('ErrorDiv').innerText);		
//			            return false;
//		            }
//			}			
//			else
//			   {alert('sandeep');return false;}
//		}

        
        
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
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="text-align: center; height: 25px;">
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Adjuster Name
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtAdjusterName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Phone Number
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Extension
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtExtension" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    FAX&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Email
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmail"
                                                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                        OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                        OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               <tr>
                                    <td class="TDPart" style="width: 100%; text-align:right; height: 44px;">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAdjuster" AutoGenerateColumns="false" runat="server" Width="100%"
                                            CssClass="GridTable" OnPageIndexChanged="grdAdjuster_PageIndexChanged" OnSelectedIndexChanged="grdAdjuster_SelectedIndexChanged">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_ADJUSTER_ID" HeaderText="Adjuster ID" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ADJUSTER_NAME" HeaderText="Adjuster Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PHONE" HeaderText="Phone"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_EXTENSION" HeaderText="Extension" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_FAX" HeaderText="FAX" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_EMAIL" HeaderText="Email"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
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
</asp:Content>
