<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="Bill_Sys_Notification_Report.aspx.cs" Inherits="Bill_Sys_Notification_Report"  Title="Untitled Page" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <script type="text/javascript">
      function showPateintFrame(objCaseID,objCompanyID)
        {
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '400px'; 
            document.getElementById('divfrmPatient').style.top= '250px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="PatientViewFrame.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"";
            return false;   
        }
        
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
             }
             
     function OpenDocManager(CaseNo,CaseId,cmpid)
    {
        window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    </script>
    
    <div style="width: 100%;">
        <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" class="txt2" align="left" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 50%;
                        border: 1px solid #B5DF82;">
                        <tr>
                            <td style="width: 100%; height: 0px;" align="left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                    <tr>
                                        <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" colspan="3">
                                            <b class="txt3">Search Parameters</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1" style="text-align:left; width:30%;border:0px solid" align="left">
                                            Location
                                        </td>
                                        <td>
                                            <extddl:ExtendedDropDownList ID="ExtdropdwnLocation" runat="server" AutoPost_back="false" 
                                             Connection_Key="Connection_String" Width="213px" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Flag_Key_Value="LOCATION_LIST" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1" style="text-align:left; width:30%;border:0px solid" align="left">
                                            Group Name</td>
                                        <td class="lbl" style="width: 70%; height: 19px">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <extddl:ExtendedDropDownList ID="extddlGroup" runat="server" OnextendDropDown_SelectedIndexChanged="OnextendDropDown_SelectedIndexChanged"
                                                        Width="213px" Selected_Text="---Select---" Procedure_Name="SP_MST_USER_GROUP"
                                                        Flag_Key_Value="GET_USER_GROUP" Connection_Key="Connection_String" AutoPost_back="TRUE">
                                                    </extddl:ExtendedDropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                    <td class="td-widget-bc-search-desc-ch1" style="text-align:left; width:30%;border:0px solid" align="left">                                       
                                        Node Description
                                        </td>
                                         <td class="lbl" style="width: 70%; height: 19px">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="lstNodeDescription" runat="server" Width="100%"  SelectionMode="Multiple"></asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 319px" colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_OnClick" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>                                         
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" style="width: 413px">
                    <b class="txt3">Missing Document</b>
                </td>
            </tr>
            <tr>
                <td style="width: 1017px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="vertical-align: middle; width: 100%;">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                            Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>
                                        </td>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                <ProgressTemplate>
                                                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                        runat="Server">
                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                            Height="25px" Width="24px"></asp:Image>
                                                        Loading...</div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                            Record Count:<%= this.grdMissingDocument.RecordCount%>
                                            | Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel" runat="server" Text="Export TO Excel" OnClick="lnkExportToExcel_OnClick">
                                    <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <xgrid:XGridViewControl ID="grdMissingDocument" runat="server" Height="148px" Width="1002px"
                                CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="MissingDoc"
                                ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                HeaderStyle-CssClass="GridViewHeader" ExportToExcelColumnNames="Case#,Patient Name,Document Missing in,OfficeName,DoctorName"
                                ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_NODE_DESCRIPTION,OFFICE_ADDRESS,DOCTOR_NAME" AlternatingRowStyle-BackColor="#EEEEEE"
                                AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_MissingDocument" PageRowCount="50"
                                PagerStyle-CssClass="pgr" DataKeyNames="" AllowSorting="true">
                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                <Columns>
                                <%-- 0 --%>
                                <asp:TemplateField HeaderText="Case #" ItemStyle-Width="80px" SortExpression="SZ_CASE_NO">
                                   <itemtemplate>
                                 <a href="../Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>                                                                        
                                   </itemtemplate>
                                </asp:TemplateField>
                                   <%-- <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case#" >
                                        <headerstyle horizontalalign="Left"></headerstyle>
                                        <itemstyle horizontalalign="Left"></itemstyle>
                                    </asp:BoundField>--%>
                                <%-- 1 --%>
                                    <asp:TemplateField HeaderText="Patient Name" Visible="true" SortExpression="SZ_PATIENT_LAST_NAME" >
                                     <itemtemplate>                                              
                                               <a id="lnkframePatient" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%></a>
                                     </itemtemplate>
                                     </asp:TemplateField>
                                <%-- 2 --%>
                                     <asp:BoundField DataField="SZ_LOCATION_NAME" HeaderText="Location" SortExpression="sz_location_name" NullDisplayText="" visible="false"/>
                                   <%-- <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" SortExpression="SZ_PATIENT_FIRST_NAME">
                                        <headerstyle horizontalalign="Left"></headerstyle>
                                        <itemstyle horizontalalign="Left"></itemstyle>
                                    </asp:BoundField>--%>
                                <%-- 3 --%>
                                    <asp:BoundField DataField="SZ_NODE_DESCRIPTION" HeaderText="Document Missing in" SortExpression="sz_nodes_description">
                                        <headerstyle horizontalalign="Left"></headerstyle>
                                        <itemstyle horizontalalign="Left"></itemstyle>
                                    </asp:BoundField>
                                <%-- 4 --%>
                                    <asp:BoundField DataField="OFFICE_ADDRESS" HeaderText="Provider Name" visible="false" SortExpression="(SELECT SZ_OFFICE_ADDRESS FROM MST_OFFICE WHERE SZ_OFFICE_ID=(SELECT top 1 SZ_OFFICE_ID FROM TXN_PATIENT_OFFICE WHERE SZ_PATIENT_ID=MST_PATIENT.SZ_PATIENT_ID))">                             
                                    </asp:BoundField>
                                <%-- 5 --%>
                                    <asp:BoundField DataField="DOCTOR_NAME" HeaderText="Referring Doctor" visible="false">                             
                                    </asp:BoundField>
                                <%-- 6 --%>
                                     <asp:TemplateField HeaderText="Docs">
                                          <itemtemplate>
                                          <img alt="" onclick="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');"  src="Images/grid-doc-mng.gif" style="border:none;cursor:pointer;"/>
                                          </itemtemplate>
                                         <itemstyle horizontalalign="Left"></itemstyle>
                                     </asp:TemplateField>
                                <%-- 7 --%>
                                     <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id"  visible="false">
                                    </asp:BoundField>
                                <%-- 8 --%>
                                    <asp:BoundField DataField="SZ_COMPANY_ID" HeaderText="Company Id" visible="false">                             
                                    </asp:BoundField>
                                </Columns>
                            </xgrid:XGridViewControl>
                            
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtNodeDescription" runat="server" Visible="false"></asp:TextBox>
                     <asp:TextBox ID="txtGroupName" runat="server" Visible="false"></asp:TextBox>
                     <asp:TextBox ID="txtLocation" runat="server" Visible="false"></asp:TextBox>
                     <asp:TextBox ID="txtUserId" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 280px; background-color: #B5DF82" id="divfrmPatient" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
                <iframe id="frmpatient" src="" frameborder="0" width="500" height="380"></iframe>
            </div>
</asp:Content>
