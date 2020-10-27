<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BillingCompany.aspx.cs" Inherits="AJAX_Pages_BillingCompany" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" src="validation.js"></script>
    
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.min.js"></script>
 <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
      <script language="javascript" type="text/javascript">

          $(document).ready(function () {

              $("#ctl00_ContentPlaceHolder1_txtPhone").mask("999-999-9999");
              $("#ctl00_ContentPlaceHolder1_txtFax").mask("999-999-9999");
              
          });
          
    </script>
    <script language="javascript" type="text/javascript">
        function javascriptFunctionCallHere() {
          
            $("#ctl00_ContentPlaceHolder1_txtPhone").mask("999-999-9999");
            $("#ctl00_ContentPlaceHolder1_txtFax").mask("999-999-9999");
        }
        function Clear() {
            document.getElementById("<%=txtName.ClientID%>").value = '';
            document.getElementById("<%=txtAddress.ClientID %>").value = '';
            document.getElementById("<%=txtCity.ClientID %>").value = '';
            document.getElementById("<%=txtZip.ClientID %>").value = '';
            document.getElementById("<%=txtPhone.ClientID %>").value = '';
            document.getElementById("<%=txtFax.ClientID %>").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlOfficeState").value = 'NA';
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdCompany.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }
            }
        }

        function ValidateDelete() {
            var f = document.getElementById("<%= grdCompany.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        if (confirm("Are you sure to continue for delete?"))
                            return true;
                        return false;
                    }
                }
            }

            alert('Please select Company.');
            return false;
        }
        function Validate() {
            if (document.getElementById("<%=txtName.ClientID%>").value == '') {
                alert('Please select Company Name.');
                return false;
            } else {
                return true;
            }
        }
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <contenttemplate>
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White">
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
                                    <td class="ContentLabel" style="text-align: center;" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <contenttemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                        <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 50%;
                                            border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                    <b class="txt3">Parameters</b>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Name</td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Address
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                     <asp:TextBox ID="txtAddress" TextMode="MultiLine" Height="40px" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    City</td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    state
                                                </td>
                                            </tr>
                                            <tr>
                                              <td align="center">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                     <extddl:ExtendedDropDownList ID="extddlOfficeState" runat="server" Selected_Text="--- Select ---"
                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                        Width="95%"></extddl:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                             <td class="td-widget-bc-search-desc-ch">
                                                    Zip</td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Phone</td>
                                               
                                            </tr>

                                             <tr>
                                             <td align="center">
                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                             <td class="td-widget-bc-search-desc-ch">
                                                    Fax
                                                </td>
                                                 <td class="td-widget-bc-search-desc-ch">
                                                   
                                                </td>
                                            </tr>
                                            <tr><td align="center">
                                                     <asp:TextBox ID="txtFax" runat="server" CssClass="textboxCSS"  Width="90%"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                  <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="80px" />
                                                  <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="80px" />
                                                  <input type="button" value="Clear" onclick="Clear();"  style="width:80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        
                                        <table border="0" width="100%">
                                            <tr>
                                            <td align="right">
                                             <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Width="80px" />
                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow: scroll; height: 400px; width: 99%;">
                                                      <asp:DataGrid ID="grdCompany" runat="server" Width="100%" CssClass="mGrid" AutoGenerateColumns="false"
                                                      OnSelectedIndexChanged="grdCompany_SelectedIndexChanged"  >
                                                        
                                                         <AlternatingItemStyle BackColor="#EEEEEE" ></AlternatingItemStyle>
                                                        <Columns>        
                                                               <asp:ButtonColumn CommandName="Select" Text="Select"  ></asp:ButtonColumn>                                    
                                                        <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="false">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Address" HeaderText="Address"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="City" HeaderText="City"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="StateName" HeaderText="StateName"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="StateId" HeaderText="StateId" Visible="false">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="zip" HeaderText="zip"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Phone" HeaderText="Phone"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Fax" HeaderText="Fax"></asp:BoundColumn>                                                       
                                                            
                                                        <asp:TemplateColumn>
                                                        <HeaderTemplate  >
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                         <asp:CheckBox ID="ChkDelete" runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateColumn>    
                                                        </Columns>
                                                        <HeaderStyle BackColor="#b5df82" Font-Bold="true" />
                                                    </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                               
                            </table>
                        </td>
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
                </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
             
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtID" runat="server" Visible="False" Width="10px"></asp:TextBox>
               
            </td>
        </tr>
    </table>

    </div>
</asp:Content>

