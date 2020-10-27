<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IntakeSheetProvider.aspx.cs" Inherits="AJAX_Pages_IntakeSheetProvider" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <link rel="Stylesheet" href="../Css/admin.css" type="text/css" />
    
   <script type="text/javascript">
       function ShowDocumetLink(IntakeSheetProviderId,IntakeSheetProviderId1)
       {
           
           debugger;        
           var url = "ShowDocumentIntake.aspx?i_id=" + IntakeSheetProviderId + "";
           ShowDocumentIntake.SetContentUrl(url);
           ShowDocumentIntake.Show();
           return false;
       } 
       </script>
       <script type="text/javascript">

        function Clear()
        {

            document.getElementById('ctl00_ContentPlaceHolder1_extddlOfficeState').value = "NA";
           
            document.getElementById("<%=txtEmail.ClientID%>").value = '';
            document.getElementById("<%=txtOfficeCode.ClientID%>").value = '';

            if (document.getElementById("<%=txtPrefix.ClientID%>") != null) {
                document.getElementById("<%=txtPrefix.ClientID%>").value = '';
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState') != null) {
                document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState').value = "NA";
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_extddlLocation') != null) {
                document.getElementById('ctl00_ContentPlaceHolder1_extddlLocation').value = "NA";
            }

            document.getElementById("<%=btnSave.ClientID%>").disabled = false;
            document.getElementById("<%=btnUpdate.ClientID%>").disabled = true;
            
        }
        function validate()
        {
                       
        if (document.getElementById("<%=txtEmail.ClientID%>").value == '') 
        {
            alert('Please Enter e-mail');
            return false;
        }
        else
        {
            //if (document.getElementById('ctl00_ContentPlaceHolder1_objextddlOfficeState').text = "---Select---")
            //{
            //    alert('Please Select State');
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }
    </script>
    <div style="width:100%;">
    <table width="100%" style="background-color: white">
       <tr> 
            <td >
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <contenttemplate>
                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
       <tr>
        <td valign="top" style="width: 100%;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div style="width:100%;vertical-align:top;">
                    <table id="page-title" style="width: 100%;">
                        <tr style="width: 100%;">
                            <td>Intake Sheet Provider
                            </td>
                        </tr>
                    </table>
                    <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%; vertical-align: top;">
                                <table style="width: 100%;">
                                    <tr style="width: 100%;">
                                        <td class="manage-member-lable-td">
                                            <label>
                                                Name
                                            </label>
                                        </td>
                                        <td class="manage-member-lable-td">
                                            <label>
                                                Address</label>
                                        </td>
                                        <td class="manage-member-lable-td">
                                            <label>
                                                City</label>
                                        </td>
                                        <td class="manage-member-lable-td">
                                            <label>
                                                </label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="50"  Width="295px" CssClass="inputBox"></asp:TextBox>
                                        </td>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="50"  Width="295px" CssClass="inputBox"></asp:TextBox>
                                        </td>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="50"  Width="295px" CssClass="inputBox"></asp:TextBox>
                                        </td>
                                        <td class="registration-form-ibox">
                                            
                                            
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td class="manage-member-lable-td">
                                            <label>
                                                State</label>
                                        </td>
                                        <td class="manage-member-lable-td">
                                            <label>
                                                Zip</label>
                                        </td>
                                        <td class="manage-member-lable-td">
                                            <label>
                                                Phone</label>
                                        </td>
                                            
                                        <td class="manage-member-lable-td">
                                            <label>
                                                </label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td class="registration-form-ibox">
                                            <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Width="295px" Selected_Text="--- Select ---"
                                                Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String" height="21px"></cc1:ExtendedDropDownList>
                                        </td>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtZip" runat="server" MaxLength="50"  Width="295px" CssClass="inputBox"></asp:TextBox>
                                        </td>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" Width="295px" CssClass="inputBox"></asp:TextBox>
                                        </td>
                                            
                                        <td class="registration-form-ibox">
                                            
                                           
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td class="manage-member-lable-td">
                                            <label>
                                                Email</label>
                                        </td>
                                       
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"  Width="295px" CssClass="inputBox"></asp:TextBox>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="a"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="test@domain.com"
                                                ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="registration-form-ibox">
                                            
                                        </td>
                                        <td class="registration-form-ibox">
                                           
                                            &nbsp;&nbsp;
                                           
                                        </td>
                                            
                                        <td class="registration-form-ibox">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td class="manage-member-lable-td">
                                            <label>
                                                Prefix</label>
                                        </td>--%>
                                        <%--<td class="manage-member-lable-td">
                                            <label>
                                                Code</label>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtPrefix" runat="server" Width="295px" MaxLength="2" CssClass="inputBox" Visible="false"></asp:TextBox>
                                        </td>
                                        <td class="registration-form-ibox">
                                            <asp:TextBox ID="txtOfficeCode" runat="server" MaxLength="2" Width="295px" CssClass="inputBox" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td style="height: 19px" align="center"></td>
                                        <td style="height: 19px" align="center">
                                               
                                        </td>
                                        <td style="height: 19px" align="center"></td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td align="center" colspan="3">
                                            <asp:Button  ID="btnSave" runat="server" Height="30px" BorderStyle="None" BackColor="#555555"
                        ForeColor="White" Text="Save" ValidationGroup="a" CausesValidation="true" Width="100px" OnClick="btnSave_Click"></asp:Button>
                                            <asp:Button  ID="btnUpdate" runat="server" Height="30px" BorderStyle="None" BackColor="#555555"
                        ForeColor="White" Text="Update" Width="100px" OnClick="btnUpdate_Click"></asp:Button>
                                            <input type="button" runat="server" id="btnClear1" onclick="Clear();" style="height:30px;background-color:#555555;color:white;width:100px;border-style:none;" value="Clear" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 130%">
                                    <table style="vertical-align: middle; width: 100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#336699"
                                                    colspan="6" height="28">
                                                    <asp:Label ID="Label1" runat="server" Text="Office" Font-Bold="True" Font-Size="Small" ForeColor="White"></asp:Label>
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtBillingOfficeFlag" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
       <tr>
            <dx:ASPxGridView ID="grdIntakeSheet" runat="server" keyfieldname="i_id" AutoGenerateColumns="false" Width="100%" SettingsPager-PageSize="50">
            <columns>
                
            <dx:GridViewDataColumn Caption="Delete"  Settings-AllowSort="False" Width="40px" >
            <HeaderTemplate>
            <asp:Label ID="Label4" runat="server" Text="Delete"></asp:Label>
            </HeaderTemplate>
            <DataItemTemplate>
            <asp:CheckBox ID="chkdel" runat="server"></asp:CheckBox> 
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Left"  />
            </dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="Id"   Settings-AllowSort="False" Width="50px">
			<HeaderTemplate>
			<asp:Label ID="Label2" runat="server" Text="Id"></asp:Label>
			</HeaderTemplate>
            <DataItemTemplate>
            <asp:LinkButton ID="lnkid" runat="server" OnClick="lnkid_Click" Text='<%# DataBinder.Eval(Container,"DataItem.i_id")%>' CommandArgument='<%# DataBinder.Eval(Container,"DataItem.i_id") %>' > </asp:LinkButton>
			</DataItemTemplate> 
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

                <dx:GridViewDataColumn Caption="Name"  FieldName="sz_name"  Settings-AllowSort="False" Width="150px">
			<HeaderTemplate>
			<asp:Label ID="Label10" runat="server" Text="Name"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="Address"  FieldName="sz_address"  Settings-AllowSort="False" Width="150px">
			<HeaderTemplate>
			<asp:Label ID="Label3" runat="server" Text="Address"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="City"  FieldName="sz_city"  Settings-AllowSort="False" Width="100px">
			<HeaderTemplate>
			<asp:Label ID="Label6" runat="server" Text="City"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

             <dx:GridViewDataColumn Caption="State"  FieldName="sz_state_name"  Settings-AllowSort="False" Width="100px">
			<HeaderTemplate>
			<asp:Label ID="Label7" runat="server" Text="State"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="Zip"  FieldName="sz_zip"  Settings-AllowSort="False" Width="80px">
			<HeaderTemplate>
			<asp:Label ID="Label8" runat="server" Text="Zip"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="Phone"  FieldName="sz_phone"  Settings-AllowSort="False" Width="100px">
			<HeaderTemplate>
			<asp:Label ID="Label9" runat="server" Text="Phone"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>
                
            <dx:GridViewDataColumn Caption="Email"  FieldName="sz_email"  Settings-AllowSort="False" Width="150px">
			<HeaderTemplate>
			<asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
			</HeaderTemplate>
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>
               
             <dx:GridViewDataColumn Caption="Set Document"   Settings-AllowSort="False" Width="50px">
			<HeaderTemplate>
			<asp:Label ID="Label11" runat="server" Text="Set Document"></asp:Label>
			</HeaderTemplate>
            <DataItemTemplate>
           
                <a href="javascript:void(0);" onclick="ShowDocumetLink('<%# DataBinder.Eval(Container,"DataItem.i_id")%>','<%# DataBinder.Eval(Container,"DataItem.i_id")%>')">
                    View  </a>
			</DataItemTemplate> 
			<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
			</dx:GridViewDataColumn>

			</columns>
            <settings showverticalscrollbar="false" showfilterrow="false" showgrouppanel="false" />
            <settingsbehavior allowfocusedrow="false" />
            <settingsbehavior allowselectbyrowclick="false" />
            <settingspager position="Bottom" />
            </dx:ASPxGridView>
   
        </tr>
    </table>

    <dx:ASPxPopupControl ID="ShowDocumentIntake" runat="server"    
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="ShowDocumentIntake" HeaderText="Document Intake Sheet" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="900px" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="550px">
        <contentstyle>     
            <Paddings PaddingBottom="5px" />
        </contentstyle>
    </dx:ASPxPopupControl>
        <asp:HiddenField ID="hdnCaseId" runat="server" />
        <asp:HiddenField ID="hdnCompanyId" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnId" runat="server" />
    </div>

</asp:Content>


