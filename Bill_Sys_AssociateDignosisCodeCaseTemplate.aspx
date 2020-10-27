<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDignosisCodeCaseTemplate.aspx.cs" Inherits="Bill_Sys_AssociateDignosisCodeCaseTemplate"   %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">  
  
<head id="Head1" runat="server">

<style type="text/css">
.ajax__tab_theme .ajax__tab_header 
{
    font-family:verdana,tahoma,helvetica;
    font-size:11px;
    
}
</style>
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
    <script type="text/javascript" src="Ajax Pages/BillTransaction.js"></script>    
</head> 
      <body onload="LoadPage()" >
        <form id="FORM1" method="post" name="FORM1"    runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
  
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 80%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <HeaderStyle CssClass="GridHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <ItemStyle CssClass="GridRow" BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" DataFormatString="{0:MM/dd/yyyy}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>                                                                                                    
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditItemStyle BackColor="#2461BF" />
                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle BackColor="White" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" style="color:Red;"></asp:Label>
                                        <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 19px;" class="SectionDevider" align="right"><asp:Button ID="btnTemplateManager" runat="server" Text="TemplateManager" Width="132px"
                                                                                        CssClass="Buttons" OnClick="btnTemplateManager_Click" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_theme">
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                <HeaderTemplate>
                                                    <div style="width: 200px; height: 20px;font-weight:bold;font-size:small;" class="lbl" >
                                                        Associate Diagnosis Code</div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100%" scope="col">
                                                                <div class="blocktitle">
                                                                    <div class="div_blockcontent">
                                                                        <table width="100%" border="0">
                                                                            
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 80%">
                                                                                        <tr runat="server" id="trDoctorType">
                                                                                            <td class="ContentLabel" runat="server" >
                                                                                                Diagnosis Type &nbsp;&nbsp;</td>
                                                                                            <td  class="ContentLabel" runat="server">
                                                                                                <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                                                                    Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <td class="ContentLabel" runat="server" >
                                                                                                Code &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td runat="server"  >
                                                                                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ContentLabel" runat="server" >
                                                                                                Description &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td  class="ContentLabel" runat="server">
                                                                                                <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        
                                                                                        <tr>
                                                                                            <td colspan="6" align="left">
                                                                                                <asp:Button ID="btnOK" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnOK_Click" Visible="False" />
                                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                                                                        OnClick="btnCancel_Click" Visible="False"/>
                                                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                                <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnSeacrh_Click" />
                                                                                                &nbsp;<asp:Button
                                                                                        ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons" OnClick="btnAssign_Click" /></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                          
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp; Search Result&nbsp; &nbsp;&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table>
                                                                                        <tr>
                                                                                           <td style="font-size: 14px; vertical-align: top; width: 35%; font-family: arial;
                                                                                                        text-align: left">
                                                                                                <asp:Label ID="lblSpeciality" runat="server" Text="Speciality" Visible="False"></asp:Label>
                                                                                            </td>
                                                                                            <td id="Td3" runat="server" class="ContentLabel">
                                                                                                <extddl:extendeddropdownlist id="extddlSpeciality" runat="server" connection_key="Connection_String"
                                                                                                    procedure_name="SP_MST_PROCEDURE_GROUP" flag_key_value="GET_PROCEDURE_GROUP_LIST"
                                                                                                    selected_text="---Select---" width="140px" OldText="" StausText="False">
                                                                                                </extddl:extendeddropdownlist>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                        <tr runat="server" id="Tr1">
                                                                                            <td width="100%" runat="server" id="Td1">
                                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                                    <asp:DataGrid Width="90%" ID="grdNormalDgCode" CssClass="GridTable" runat="server"
                                                                                                        AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                        <ItemStyle CssClass="GridRow" BackColor="#EFF3FB" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateColumn>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                                                                                                Visible="False"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION">
                                                                                                            </asp:BoundColumn>
                                                                                                            
                                                                                                        </Columns>
                                                                                                        <HeaderStyle CssClass="GridHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                        <EditItemStyle BackColor="#2461BF" />
                                                                                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                                        <AlternatingItemStyle BackColor="White" />
                                                                                                    </asp:DataGrid>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td colspan="2">
                                                                                    Total Count : <asp:Label ID="lblDiagnosisCount" Font-Bold="True" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable"
                                                                                        runat="server" AutoGenerateColumns="False"  Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                        <ItemStyle CssClass="GridRow" BackColor="#EFF3FB" />
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
                                                                                        <HeaderStyle CssClass="GridHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                        <EditItemStyle BackColor="#2461BF" />
                                                                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                        <AlternatingItemStyle BackColor="White" />
                                                                                    </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            
                                                                            <tr>
                                                                                <td class="tablecellLabel" colspan="2" rowspan="3">
                                                                                    &nbsp; &nbsp;<asp:TextBox Width="100%" ID="txtSearchText" runat="server" Visible="False"></asp:TextBox>
                                                                                    <asp:CheckBox ID="chkDiagnosisCode" runat="server" Text="Diagnosis Code" Visible="False"/>
                                                                                    <asp:CheckBox ID="chkDiagnosisCodeDescription" runat="server" Text="Diagnosis Code Description" Visible="False"/>
                                                                                    <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                                        OnClick="btnSearch_Click" Visible="False"></asp:Button>
                                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                    &nbsp; &nbsp;
                                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                                                                        ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                         <asp:TextBox ID="txtUserId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                         <asp:TextBox ID="txtDoctorId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                         <asp:TextBox ID="txtSpeciality" runat="server" Width="10px" Visible="False"></asp:TextBox>
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
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
                                                <HeaderTemplate>
                                                    <div style="width: 250px; height: 20px;font-weight:bold;font-size:small;" class="lbl" >
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
                                                                                        CssClass="GridTable" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                        <ItemStyle CssClass="GridRow" BackColor="#EFF3FB" />
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
                                                                                        <HeaderStyle CssClass="GridHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                        <EditItemStyle BackColor="#2461BF" />
                                                                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                        <AlternatingItemStyle BackColor="White" />
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
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
  </form> 
</body> 
</html>
