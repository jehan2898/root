<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Intake_Document_CaseType.aspx.cs" Inherits="AJAX_Pages_Intake_Document_CaseType" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <link rel="Stylesheet" href="../Css/admin.css" type="text/css" />
    
    <script type="text/javascript">

        function CheckDelete() {

            var f = document.getElementById("<%=grdIntakeDocCaseTyp.ClientID%>");
        var bfFlag = false;
        for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
            if (f.getElementsByTagName("input").item(i).name.indexOf('chkdel') != -1) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        bfFlag = true;

                    }

                }
            }
        }

        if (bfFlag == false) {
            alert('Please select the record for delete.');
            return false;
        }
        else {
            if (confirm("Are you sure want to Delete?") == true) {

                return true;
            }
            else {
                return false;
            }
        }

    }


    </script>

     <script type="text/javascript">

         function checkValue() 
         {

             var Document = document.getElementById('ctl00_ContentPlaceHolder1_txtDocument');
            
             var CastType = document.getElementById('ctl00_ContentPlaceHolder1_ddlCaseType');


             if (Document.value == "")
             {
                 alert('Please enter Document name');
                 return false
             }

             if (CastType.value == "") {
                 alert('Please select case type');
                 return false
             }
               
         }

         function clearControls()
         {

             var Document = document.getElementById('ctl00_ContentPlaceHolder1_txtDocument');

             Document.value = "";

         }
    </script>
   
    <table width="100%" style="background-color: white">
        <tr> <td >
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td></tr>
        <tr>
          
            <td valign="top" style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div style="width:100%;vertical-align:top;"> 
                        <table id="page-title"  style="width: 100%;">
                            <tr style="width: 100%;">
                                <td > Intake Document Case Type
                                </td>
                                
                            </tr>
                        </table>
                        <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100%; vertical-align: top;">
                                    <table style="width: 50%;">
                                        <tr>
                                            <td class="manage-member-lable-td" align="center">
                                                <label >
                                                   Case Type
                                                </label>
                                            </td>
                                            <td class="manage-member-lable-td" align="center">
                                                <label>
                                                    Document</label>
                                            </td>
                                        </tr>
                                        <tr>
                                              <td class="registration-form-ibox">
                                                   
                                                <cc1:ExtendedDropDownList ID="ddlCaseType" runat="server" Width="220px" Selected_Text="---Select---" Maxlength="50" CssClass="te" 
                                                     Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                </cc1:ExtendedDropDownList>

                                            </td>
                                            <td class="registration-form-ibox">
                                                <asp:TextBox ID="txtDocument" runat="server" MaxLength="50" CssClass="textboxCSS" Width="220px"></asp:TextBox>
                                            </td>
                                        </tr>   

                                        <tr style="width: 100%;">
                                            <td style="height: 19px" align="center"></td>
                                            <td style="height: 19px" align="center">
                                               
                                            </td>
                                            <td style="height: 19px" align="center"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                               
                                                 <asp:Button  ID="btnSave" runat="server" Height="30px" BorderStyle="None" BackColor="#555555"
                            ForeColor="White" Text="Save" ValidationGroup="a" CausesValidation="true" Width="100px" OnClick="btnSave_Click"></asp:Button>
                                                   <asp:Button  ID="btnUpdate" runat="server" Height="30px" BorderStyle="None" BackColor="#555555"
                            ForeColor="White" Text="Update" ValidationGroup="a" CausesValidation="true" Width="100px" OnClick="btnUpdate_Click" ></asp:Button>
                                                <asp:Button  ID="btnDelete" runat="server" Height="30px" BorderStyle="None" BackColor="#555555"
                            ForeColor="White" Text="Delete" ValidationGroup="a" CausesValidation="true" Width="100px" OnClick="btnDelete_Click"  ></asp:Button>                                             
                                                 <input type="button" runat="server" id="btnClear1" onclick="clearControls();" style="height:30px;background-color:#555555;color:white;width:100px;border-style:none;" value="Clear" />
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
                                                        <asp:Label ID="Label1" runat="server" Text="Office" Font-Bold="True" Font-Size="Small"></asp:Label>
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
                                                <tr>
               <dx:ASPxGridView ID="grdIntakeDocCaseTyp" runat="server" keyfieldname="i_id" AutoGenerateColumns="false" Width="70%" SettingsPager-PageSize="50">
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
                <asp:LinkButton ID="lnkid" runat="server"  OnClick="lnkid_Click" Text='<%# DataBinder.Eval(Container,"DataItem.i_id")%>' CommandArgument='<%# DataBinder.Eval(Container,"DataItem.i_id") %>' > </asp:LinkButton>
				</DataItemTemplate> 
				<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
				</dx:GridViewDataColumn>

                 <dx:GridViewDataColumn Caption="Company"  FieldName="sz_company_name"  Settings-AllowSort="False" Width="150px">
				<HeaderTemplate>
				<asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
				</HeaderTemplate>
				<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
				</dx:GridViewDataColumn>

                <dx:GridViewDataColumn Caption="Case Type"  FieldName="sz_case_type_name"  Settings-AllowSort="False" Width="150px">
				<HeaderTemplate>
				<asp:Label ID="Label5" runat="server" Text="Case Type"></asp:Label>
				</HeaderTemplate>
				<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
				</dx:GridViewDataColumn>

                <dx:GridViewDataColumn Caption="Document"  FieldName="sz_name"  Settings-AllowSort="False" Width="150px">
				<HeaderTemplate>
				<asp:Label ID="Label10" runat="server" Text="Document"></asp:Label>
				</HeaderTemplate>
				<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
				</dx:GridViewDataColumn>

                <dx:GridViewDataColumn Caption="User "  FieldName="sz_user_name"  Settings-AllowSort="False" Width="150px">
				<HeaderTemplate>
				<asp:Label ID="Label11" runat="server" Text="Created By"></asp:Label>
				</HeaderTemplate>
				<HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
				</dx:GridViewDataColumn>
               
			    </columns>
                                        <settings showverticalscrollbar="false" showfilterrow="false" showgrouppanel="false" />
                                        <settingsbehavior allowfocusedrow="false" />
                                        <settingsbehavior allowselectbyrowclick="false" />
                                        <settingspager position="Bottom" />
              </dx:ASPxGridView>
   
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnCaseId" runat="server" />
                <asp:HiddenField ID="hdnCompanyId" runat="server" />
                <asp:HiddenField ID="hdnUserId" runat="server" />
               <asp:HiddenField ID="hdnId" runat="server" />
                <asp:TextBox ID="txtBillingOfficeFlag" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>

