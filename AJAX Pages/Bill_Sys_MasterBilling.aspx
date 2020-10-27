<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true"
 title="Green Your Bills - AC Billing Report" CodeFile="~/AJAX Pages/Bill_Sys_MasterBilling.aspx.cs" inherits="Bill_Sys_MasterBilling" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    
    <style type="text/css">
    .hiddencol
    {
        display:none;
    }
</style>
    <script type="text/javascript">

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdMasterBilling.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }


            }
        }
        function CancelExistPatient1() {
            document.getElementById('div1').style.height = '0px';
            document.getElementById('div1').style.visibility = 'hidden';
            return false;
        }
        function openExistsPage1() {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute';
            document.getElementById('div1').style.left = '360px';
            document.getElementById('div1').style.top = '100px';
            document.getElementById('div1').style.visibility = 'visible';

            return false;
        }
        function showReceiveDocumentPopup() {
            debugger;
            document.getElementById('<%=divid.ClientID%>').style.zIndex = 1;
            document.getElementById('<%=divid.ClientID%>').style.position = 'absolute';
            document.getElementById('<%=divid.ClientID%>').style.left = '300px';
            document.getElementById('<%=divid.ClientID%>').style.top = '100px';
            document.getElementById('<%=divid.ClientID%>').style.visibility = 'visible';
            document.getElementById('<%=frameeditexpanse.ClientID%>').src = 'Bill_Sys_ReceivedReportPopupPage.aspx?AcBilling=true';
            return false;
        }


        //        function Validatedelete() {
        //            var f = document.getElementById("<%= grdMasterBilling.ClientID %>");
        //            if (f != null) {
        //                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        //                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
        //                        if (f.getElementsByTagName("input").item(i).checked != false) {
        //                            return true;
        //                        }
        //                    }
        //                }
        //            }

        //            alert('Please select patient.');
        //            return false;

        //        }

        function Validatedelete() {
            var f = document.getElementById("<%= grdMasterBilling.ClientID %>");

            var flag = "0";
            if (f != null) {
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {

                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            flag = "1";
                            break;
                        }
                    }
                }
            }


            if (flag == "1") {


                var TypeID = "";




                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    var varFlag = "0";
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            varFlag = "1";

                        }
                    }
                    if (varFlag == "1") {

                        if (f.getElementsByTagName("input").item(i + 1).type == "hidden") {

                            if (TypeID == "") {
                                TypeID = f.getElementsByTagName("input").item(i + 1).value;

                            }
                            else {

                                if (TypeID != f.getElementsByTagName("input").item(i + 1).value) {
                                    alert('Please select same case type.');
                                    return false;
                                }

                            }
                        }
                    }
                }

                return true;
            }
            else {
                alert('Please select patient.');
                return false;
            }

        }

    </script>
<asp:UpdatePanel ID="update_pnl1" runat="server" ChildrenAsTriggers="true">
<ContentTemplate>   
<table style="vertical-align: middle; margin:3px" align="center"  border="0" width="95%">
<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="update_pnl1" runat="server" DisplayAfter="0">
    <ProgressTemplate>
        <asp:Image ID="img2" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....."></asp:Image>
    </ProgressTemplate>
</asp:UpdateProgress>
         <tbody>
            <tr>
                <td>
                    <asp:updatepanel id="UpdatePanel10" runat="server">
                        <contenttemplate>
                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                        </contenttemplate>
                    </asp:updatepanel>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-size:11px">
                    Specialty: 
                    <asp:DropDownList id="ddlSpeciality" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged" >
                        <asp:ListItem Text="--Select--" Value="NA"></asp:ListItem>
                        <asp:ListItem Text="AC" Value="AC"></asp:ListItem>
                        <asp:ListItem Text="SYN" Value="SYN"></asp:ListItem>
                        <asp:ListItem Text="CH" Value="CH"></asp:ListItem>
                        <asp:ListItem Text="PT" Value="PT"></asp:ListItem>
                        <asp:ListItem Text="WB" Value="WB"></asp:ListItem>
                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:Label id="lblCaseType" runat="server"  text="Case Type"></asp:Label>
                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="40%" AutoPost_back="true" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"  OnextendDropDown_SelectedIndexChanged="extddlCaseType_SelectedIndexChanged"
                                                       ></extddl:ExtendedDropDownList >
                    
                </td>
            </tr>
          
            
            <tr>
                <td>
                    <table style="vertical-align: middle; width: 100%" id="tblGridParameters" visible="false"> 
                        <tbody>
                            <tr>
                                <td style="vertical-align: middle; width: 30%" align="left">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="Search:" Font-Size="Small"></asp:Label><gridsearch:XGridSearchTextBox
                                        ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                    </gridsearch:XGridSearchTextBox>
                                </td>
                                <td style="vertical-align: middle; width: 30%" align="left">
                                </td>
                                <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                    Record Count:<%= this.grdMasterBilling.RecordCount%>| Page Count:
                                    <gridpagination:XGridPaginationDropDown ID="con1" runat="server">
                                    </gridpagination:XGridPaginationDropDown>
                                     
                                    
                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                        Text="Export TO Excel">
                                    <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                       <xgrid:XGridViewControl 
                            EnableViewState="true"
                            ID="grdMasterBilling" 
                            runat="server" 
                            CssClass="mGrid"  Width="100%"
                            AutoGenerateColumns="false"
                            AllowSorting="true" 
                            PagerStyle-CssClass="pgr" 
                            PageRowCount="10" 
                            XGridKey="MasterBilling"
                            AllowPaging="true" 
                            ExportToExcelColumnNames="Case No,Patient Name,Provider,Doctor Name,Insurance Company,Claim No,Accident Date,Visit Date,Procedure Codes,CaseType"
                            ShowExcelTableBorder="true" 
                            ExportToExcelFields="Case_No,sz_patient_name,Provider,Doctor_Name,Insurance_Company,Claim_No,Accident_date,Visit_date,Proc_Codes,SZ_CASE_TYPE_NAME"
                            AlternatingRowStyle-BackColor="#EEEEEE" 
                            HeaderStyle-CssClass="GridViewHeader"
                            EnableRowClick="false" 
                            MouseOverColor="0, 153, 153" 
                            OnRowCommand="grdMasterBilling_RowCommand" 
                           DataKeyNames="Specialty, CaseID, SZ_COMPANY_ID, EventID,BT_FINALIZE">
                            
                            
                        <Columns>
                         <%--0--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="5%" 
                                SortExpression="(SELECT SZ_CASE_NO FROM MST_CASE_MASTER WHERE SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID)"
                                headertext="Case#" DataField="Case_No" />
                          <%--1--%>      
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="15%" 
                                SortExpression="(SELECT ISNULL(SZ_PATIENT_FIRST_NAME,'')  + ' ' + ISNULL(SZ_PATIENT_LAST_NAME,'') FROM MST_PATIENT WHERE SZ_PATIENT_ID=TXN_CALENDAR_EVENT.SZ_PATIENT_ID)"
                                headertext="Patient Name" DataField="sz_patient_name" />
                                
                         <%--2--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="15%" 
                                SortExpression="(SELECT SZ_OFFICE FROM MST_OFFICE WHERE SZ_OFFICE_ID IN (CASE (SELECT BT_REFERRING_FACILITY FROM MST_BILLING_COMPANY WHERE SZ_COMPANY_ID=@SZ_COMPANY_ID) 
						WHEN 0 THEN (SELECT SZ_OFFICE_ID FROM MST_OFFICE WHERE SZ_OFFICE_ID IN (SELECT MST_DOCTOR.SZ_OFFICE_ID FROM MST_DOCTOR WHERE MST_DOCTOR.SZ_DOCTOR_ID=TXN_CALENDAR_EVENT.SZ_DOCTOR_ID))
						WHEN 1 THEN (SELECT SZ_OFFICE_ID FROM MST_OFFICE WHERE SZ_OFFICE_ID IN (SELECT MST_BILLING_DOCTOR.SZ_OFFICE_ID FROM MST_BILLING_DOCTOR WHERE MST_BILLING_DOCTOR.SZ_DOCTOR_ID=TXN_CALENDAR_EVENT.SZ_DOCTOR_ID)) END))"
                                headertext="Provider" DataField="Provider" />
                         <%--3--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="15%" 
                                SortExpression="(SELECT (CASE(select BT_REFERRING_FACILITY from MST_BILLING_COMPANY where SZ_COMPANY_ID=@SZ_COMPANY_ID) 
								WHEN 1 then (select SZ_DOCTOR_NAME from MST_BILLING_DOCTOR where SZ_DOCTOR_ID=TXN_CALENDAR_EVENT.SZ_DOCTOR_ID AND SZ_COMPANY_ID=@SZ_COMPANY_ID)
								ELSE (select sz_doctor_name from MST_DOCTOR where SZ_DOCTOR_ID=TXN_CALENDAR_EVENT.SZ_DOCTOR_ID AND SZ_COMPANY_ID=@SZ_COMPANY_ID)
							 END))"
                                headertext="Doctor Name" DataField="Doctor_Name" />
                                
                         <%--4--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="15%" 
                                SortExpression="(SELECT SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID = (SELECT SZ_INSURANCE_ID FROM MST_CASE_MASTER WHERE SZ_CASE_ID = TXN_CALENDAR_EVENT.SZ_CASE_ID))"
                                headertext="Insurance Name" DataField="Insurance_Company" />
                         <%--5--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="Right"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="10%" 
                                SortExpression="(SELECT SZ_CLAIM_NUMBER FROM MST_CASE_MASTER WHERE MST_CASE_MASTER.SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID)"
                                headertext="Claim Number" DataField="Claim_No" />
                         <%--6--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="Right"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="10%" 
                                SortExpression="(SELECT CONVERT(NVARCHAR(20),DT_DATE_OF_ACCIDENT,101) FROM MST_CASE_MASTER WHERE SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID)"
                                headertext="Date of Accident" DataField="Accident_date" />
                         <%--7--%>
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="Right"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="10%" 
                                SortExpression="CONVERT(NVARCHAR(20),txn_calendar_event.DT_EVENT_DATE,101)"
                                headertext="Date of Visit" DataField="Visit_date" />
                           <%--8--%> 
                         <asp:TemplateField  ItemStyle-HorizontalAlign="center" HeaderText="Edit <br/>Reading doctor <br/> Diagnosis code" >
                                <itemtemplate>
                                    <asp:LinkButton ID="lnkEdit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SZ_PATIENT_TREATMENT_ID")+","+DataBinder.Eval(Container.DataItem,"DoctorID")+","+DataBinder.Eval(Container.DataItem,"CaseID")+","+DataBinder.Eval(Container.DataItem,"SZ_PROCEDURE_GROUP_ID")%>' CommandName="Editt" runat="server" Text="Edit" />
                                </itemtemplate>
                         </asp:TemplateField>
                          <%--9--%>                                                  
                         <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="10%" 
                                SortExpression="MST_BILL_PROC_TYPE.SZ_TYPE_CODE"
                                headertext="Procedure Codes" DataField="Proc_Codes" />
                                
                        
                            <%--10--%>    
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center" ItemStyle-Width="2%">
                                <headertemplate>
                                  <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                              </headertemplate>
                                <itemtemplate>
                                 <asp:CheckBox ID="chkSelect" runat="server" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <%--11--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="CaseID" headertext="CaseID"  />
                            <%--12--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="PatientID" headertext="PatientID"/>      
                            <%--13--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="ChartNo" headertext="ChartNo"/>      
                            <%--14--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="DoctorID" headertext="DoctorID"/>
                            <%--15--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="Description" headertext="Description"/>                                  
                            <%--16--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="Status" headertext="Status"/>    
                            <%--17--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="SZ_CODE_ID" headertext="sz_code_id"/>                                                                               
                            <%--18--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="EventID" headertext="Event"/>    
                            <%--19--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="SZ_COMPANY_ID" headertext="CompanyID"/> 
                            <%--20--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="SZ_PATIENT_TREATMENT_ID" headertext="PatientTreatmentID"/>    
                            <%--21--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="SZ_PROCEDURE_GROUP_ID" headertext="ProcedureGroupID"/> 
                            <%--22--%>
                            <asp:BoundField visible="true" headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="SZ_INSURANCE_ADDRESS" headertext="InsuranceAdd"/> 
                            <%--23--%>
                             <asp:TemplateField SortExpression="(SELECT SZ_CASE_NO FROM MST_CASE_MASTER WHERE SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID)" ItemStyle-HorizontalAlign="center"  HeaderText="Documents" >
                            <itemtemplate>
                                <asp:LinkButton ID="lnkDocManager1" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CaseID")%>' CommandName="DocManager" runat="server" Text="Documents" />
                            </itemtemplate>
                         </asp:TemplateField>
                        <%--24--%>
                            <asp:BoundField visible="false" DataField="SZ_PROCEDURE_GROUP_ID" headertext="ProcedureGroupID"/> 
                            <%--25--%>
                            <asp:BoundField headerstyle-cssclass="hiddencol" itemstyle-cssclass="hiddencol" DataField="EventID" headertext="Event"/>
                            <%--26--%>
                            <asp:BoundField visible="false" DataField="Specialty" headertext="Speciality_Name"/>  
                              <%--27--%>
                           <%-- <asp:BoundField 
                                HeaderStyle-HorizontalAlign="left" 
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-VerticalAlign="Top"
                                ItemStyle-Width="10%" 
                                SortExpression=" (select SZ_CASE_TYPE_NAME from MST_CASE_TYPE	 where SZ_CASE_TYPE_ID =(select SZ_CASE_TYPE_ID from MST_CASE_MASTER where SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID))"
                                headertext="CaseType" DataField="SZ_CASE_TYPE_NAME" />  --%>

                                <asp:TemplateField SortExpression="(select SZ_CASE_TYPE_NAME from MST_CASE_TYPE	 where SZ_CASE_TYPE_ID =(select SZ_CASE_TYPE_ID from MST_CASE_MASTER where SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID))"
                                ItemStyle-HorizontalAlign="center" HeaderText="Case Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblCaseType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SZ_CASE_TYPE_NAME")%>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnCaseType" Value='<%# DataBinder.Eval(Container.DataItem,"SZ_ABBRIVATION_ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--28--%>
                            <asp:BoundField  DataField="BT_FINALIZE" headertext="Finalize"  SortExpression="BT_FINALIZE"/>
                            <%--29--%>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center" ItemStyle-Width="2%" >
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpWC" runat="server" Width="80px">
                                        <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Doctor's Initial Report</asp:ListItem>
                                        <asp:ListItem Value="2">Doctor's Progress Report</asp:ListItem>
                                        <asp:ListItem Value="3">Doctor's Report Of MMI</asp:ListItem>
                                        <asp:ListItem Value="4">OTPT Report</asp:ListItem>
                                        <asp:ListItem Value="5">Psychologist Report</asp:ListItem>
                                        <asp:ListItem Value="6">Dental Claim Report</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </xgrid:XGridViewControl>
                                       
          
                </td>
            </tr>
            
    
           
            <tr>
                <td  style="text-align:center;">
                <div id="divid" runat="server" style="position: absolute; width: 600px; height:600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position:absolute; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('<%=divid.ClientID %>').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" runat="server" frameborder="0"  width="600px" height="600px"></iframe>
    </div>
    <div id="div1" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient1();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="popupmsg"  runat="server"></span><br /><br /><br />
            <input type="button" class="Buttons" value="Cancel" id="btnCancel1" onclick="CancelExistPatient1();"
                style="width: 80px;" />
            </div>
            <br />
         <div style="text-align: center;">
            
        </div>
     </div>

                    <asp:Button ID="Btn_Patient" Text="Create Bills Per Patient" runat="server" OnClick="Btn_Patient_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btn_Selected" Text="Create Selected Bills" runat="server" OnClick="Btn_Selected_Click" />
                    
                </td>
            </tr>
        </tbody>
    </table>
    
    </ContentTemplate>
    <Triggers>
             <asp:PostBackTrigger ControlID="Btn_Patient" />
             <asp:PostBackTrigger ControlID="Btn_Selected" /> 
          
     </Triggers> 

</asp:UpdatePanel>
 
 
  <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtFromDate" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtToDate" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtReadingDocID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtPatientID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCaseNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtRefCompanyID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtBillDate" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtSpeciality" runat="server" Visible="false"></asp:TextBox> 
                      <asp:TextBox ID="txtCaseType" runat="server" Visible="false"></asp:TextBox>
                      <asp:HiddenField ID="hdnWCPDFBillNumber" runat="server" />
</asp:Content>
