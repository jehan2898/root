<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_sys_workingDays.aspx.cs" Inherits="Bill_sys_workingDays" Title="Green Your Bills - Patient Absentee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptWorkDays" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
<script type="text/javascript" language="javascript">
    function Clear()
       {
           document.getElementById("<%=txtWorkDays.ClientID %>").value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_extddlSpeciality').value = 'NA';
       }
       function CheckValidate() 
       {
        
            var spciality = document.getElementById('ctl00_ContentPlaceHolder1_extddlSpeciality').value;
            var day = document.getElementById("<%=txtWorkDays.ClientID %>").value;
           // alert(spciality + '' + day);
            if (day != "" && spciality != "NA")
            {
               //alert("true");
                return true;
            }
            else 
            {
              // alert("false");
              alert("Please enter specialty and  work day");
                return false;
            }
       }
  </script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<table>
              <tr >
                 <td>
                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
               </td>
            </tr>
            <tr>
                <td>
                
                     
                     <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;"  onKeyPress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                     
                        <tr>
                             <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                <b class="txt3">Search Parameters</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                       <table>
                        <tr>
                        <td  class="td-widget-bc-search-desc-ch1">
                             Specialty
                            </td>
                            
                          <td class="td-widget-bc-search-desc-ch1">
                            Case Status
                            </td>
                         </tr>
                        <tr>
                        <td class="td-widget-bc-search-desc-ch3" valign="top">
                                       <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                       Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                       Selected_Text="---Select---" Width="100%" CssClass="search-input" ></extddl:ExtendedDropDownList>
                               </td>
                               <td class="td-widget-bc-search-desc-ch3">
                                <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="100%" Selected_Text="OPEN"
                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String" CssClass="search-input" >
                                </extddl:ExtendedDropDownList>
                              </td>
                        </tr>
                        <tr>
                            <td class="td-widget-bc-search-desc-ch1">
                             WorkDays
                            </td>
                        </tr>
                        <tr>
                            <td class="td-widget-bc-search-desc-ch3">
                                <asp:TextBox ID="txtWorkDays" runat="server" Width="100%" CssClass="search-input"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="txtWorkDays" ValidChars="1234567890" />
                              </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" onClientClick=" return CheckValidate()" runat="server" Width="80px" Text="Search"></asp:Button>
            
             <input type="button" id="btnClear" onclick="Clear();" style="width:80px" value="Clear" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
            
            <table width="100%"> 
                <tr>
                    <td height="28" align="left"  bgcolor="#B5DF82" class="txt2" style="width: 100%">
                        <b class="txt3">Work Days</b>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                            DisplayAfter="10">
                        <ProgressTemplate>
                     <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                         runat="Server">
                         <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                             Height="25px" Width="24px"></asp:Image>
                         Loading...</div>
                 </ProgressTemplate>
             </asp:UpdateProgress>
             </td>
        </tr>
        <tr>
            <td>
                  <table style="vertical-align: middle; width: 100%;">
                    <tbody> 
                        <tr>
                           <td style="vertical-align: middle; width: 30%" align="left">
                               Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                   CssClass="search-input">
                               </gridsearch:XGridSearchTextBox>
                           </td>
                           <td style="width: 60%" align="right">
                             Record Count:
                             <%= this.grdWorkDayList.RecordCount%>
                             | Page Count:
                             <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                             </gridpagination:XGridPaginationDropDown>
                             <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                   <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                  
                            </td>                                                                                                                                   
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
       </table>
      <table width="100%">
        <tr>
           <td >
                 <xgrid:XGridViewControl ID="grdWorkDayList" runat="server" Width="100%" CssClass="mGrid"
                     DataKeyNames="SZ_CASE_ID" MouseOverColor="0, 153, 153"
                     EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                     AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,DT_ACCIDENT_DATE,SZ_CASE_TYPE,SZ_PATIENT_PHONE,DT_LAST_VISIT_DATE"
                     ShowExcelTableBorder="true" ExportToExcelColumnNames="Case #,Patient Name,Accident Date,CaseType,PhoneNo,Last Visit Date"
                     AllowPaging="true" XGridKey="WorkDays" PageRowCount="50" PagerStyle-CssClass="pgr"
                     AllowSorting="true" AutoGenerateColumns="false">
                     <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Case#" DataField="SZ_CASE_NO"  SortExpression="convert(int,MAX(SZ_CASE_NO))" />
                         <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Patient Name" DataField="SZ_PATIENT_NAME" SortExpression ="(max(mp.SZ_PATIENT_FIRST_NAME))" />
                         <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Accident Date" DataField="DT_ACCIDENT_DATE"   SortExpression ="isnull(max(mst.DT_DATE_OF_ACCIDENT),'')" />
                          <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Case Type" DataField="SZ_CASE_TYPE"  SortExpression ="(select SZ_CASE_TYPE_NAME  from MST_CASE_TYPE where SZ_CASE_TYPE_ID  = max(mst.SZ_CASE_TYPE_ID) )" />
                          <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Phone No" DataField="SZ_PATIENT_PHONE"  SortExpression ="isnull(max(mp.SZ_PATIENT_PHONE) ,'')" />
                          <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                         headertext="Last Visit Date" DataField="DT_LAST_VISIT_DATE" SortExpression ="isnull(max(t.dt_event_date),'')" />
                     </Columns>
                </xgrid:XGridViewControl>
         </td>
      </tr>
   </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtSpeciality" runat="Server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtStatus" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtWorkDay" runat="server" Visible ="false"></asp:TextBox>
   </asp:Content>

