<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="Bill_Sys_Diagnosys.aspx.cs" Inherits="Bill_Sys_Diagnosys" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">  
  
<head id="Head1" runat="server">
    <script type="text/javascript">
    function LoadPage()
    {
     window.resizeTo(700,400)
     window.moveTo(screen.availWidth/4,screen.availHeight/4)
    }
    
    function OnSave()
     {            
          alert('1')
          window.open('../TM/RTFEditter.aspx?hidden='','AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
     }
    
    </script>
    <script type="text/javascript" src="validation.js"></script>    
    <script type="text/javascript" src="BillTransaction.js"></script>    
</head> 
      <body onload="LoadPage()" >
        <form id="FORM1" method="post" name="FORM1"    runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
     <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_theme">
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                <HeaderTemplate>
                                                    <div style="width: 200px; height: 20px;" class="lbl">
                                                        Associate Diagnosis Code</div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; height: 100%">      
                 <tr>
                        <td align="left">
                          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                            <asp:Label ID="lblMSG" runat="server"  Font-Bold="True" CssClass="Labels"
                                Font-Size="Small" ForeColor="Red" ></asp:Label>
                                </ContentTemplate> 
                                </asp:UpdatePanel> 
                        </td>
                    </tr>            
                    <tr>
                        <td align="right">.
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" CssClass="Buttons" OnClick="btnClose_Click" />
                                    </ContentTemplate> 
                         </asp:UpdatePanel>                       
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <table border="0" class="ContentTable" style="width: 100%">
                                <tr runat="server" id="trDoctorType">
                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                        Diagnosis Type:</td>
                                    <td style="width: 35%; height: 18px;">
                                        <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                            Flag_Key_Value="DIAGNOSIS_TYPE_LIST"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                        Code :
                                    </td>
                                    <td style="width: 35%; height: 18px;">
                                        <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                        Description :
                                    </td>
                                    <td style="width: 35%; height: 18px;">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                            OnClick="btnSeacrh_Click" />
                                        <asp:Button ID="btnOK" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                            OnClick="btnOK_Click" /> 
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                            OnClick="btnUpdate_Click"   Visible="false"/>    
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                            <div style="height: 200px; background-color: White; overflow-y: scroll;">
                                <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="99%" CssClass="GridTable"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                                    OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged">                                    
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                    </Columns>                                    
                                </asp:DataGrid>
                            </div>
                            </ContentTemplate> 
                            </asp:UpdatePanel> 
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                             <asp:TextBox ID="TextBox1" runat="server" Width="10px" Visible="False"></asp:TextBox>
                             <asp:TextBox ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                             <asp:TextBox ID="txtSpeciality" runat="server" Width="10px" Visible="False"></asp:TextBox>
                             <asp:TextBox ID="txtUserId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                             <asp:TextBox ID="txtDoctorId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                             
                        </td>
                    </tr>
                    <tr>
                                                                                <td colspan="2">
                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable"
                                                                                        runat="server" AutoGenerateColumns="False" >
                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" ></asp:BoundColumn>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                                    </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                    <tr>
                                                                                <td colspan="2">
                                                                                    <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                        <tr runat="server" id="Tr1">
                                                                                            <td width="100%" runat="server" id="Td1">
                                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                                    <asp:DataGrid Width="90%" ID="grdNormalDgCode" CssClass="GridTable" runat="server"
                                                                                                        AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged">
                                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateColumn>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" Visible="False"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION">
                                                                                                            </asp:BoundColumn>
                                                                                                            
                                                                                                        </Columns>
                                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                                                    </asp:DataGrid>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                    <tr>
                                                                                <td colspan="2">
                                                                                    Total Count : <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                     
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="DataGrid1" CssClass="GridTable"
                                                                                        runat="server" AutoGenerateColumns="False" >
                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" ></asp:BoundColumn>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                                    </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                     
                </table>   
                  </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
                                                <HeaderTemplate>
                                                    <div style="width: 200px; height: 20px;" class="lbl">
                                                        De-associate Diagnosis Code</div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                      <table width="100%">
                                                        <tr>
                                                            <td width="100%" scope="col">
                                                                <div class="blocktitle">
                                                                    <div class="div_blockcontent">
                                                                        <table width="100%" border="0">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server"
                                                                                        CssClass="GridTable" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged">
                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                        <Columns>
                                                                                            <asp:TemplateColumn>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" ></asp:BoundColumn>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                                        <PagerStyle Mode="NumericPages" />
                                                                                    </asp:DataGrid>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="104px"
                                                                                        CssClass="Buttons" OnClick="btnDeassociateDiagCode_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>      
        </form>
      </body>
</html>

