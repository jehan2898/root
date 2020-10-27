<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_PaidBills.aspx.cs" Inherits="Bill_Sys_PaidBills" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        function OpenReport(obj)
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpeciality').click();
           
            
        }
        
         function showUploadFilePopup()
       {
      
           var flag= false;
           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports');
           if(grdProc.rows.length>0)
            {           
                for (var i=1; i<grdProc.rows.length; i++)
                {
                    var cell = grdProc.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {  
                            if (cell.childNodes[j].type =="checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report")
                            {
                                if(cell.childNodes[j].checked)
                                {
                                   flag = true; 
                                   break;
                                }
                            }
                    }
                }
                if(flag==true)
                {
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '10px';
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left ='200px';
	            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height='0px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';  
                //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                    MA.length = 0;
                }
                else
                {
                    alert("Select procedure code ..");
                }   
            }
       }
        
       function CloseUploadFilePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       // 21 April 2010 show ReceiveReport popup -- sachin
       function showReceiveDocumentPopup()
       {
            
            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left= '300px'; 
            document.getElementById('divid').style.top= '100px';              
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src ='Bill_Sys_ReceivedDocumentPopupPage.aspx';  
            return false;
          
       }
         function ShowDiv()
       {
            document.getElementById('divDashBoard').style.position = 'absolute'; 
            document.getElementById('divDashBoard').style.left= '200px'; 
            document.getElementById('divDashBoard').style.top= '120px'; 
            document.getElementById('divDashBoard').style.visibility='visible'; 
            return false;
       }
       
       
        function showProcPopup()
       {
            
            document.getElementById('divid1').style.zIndex = 1;
            document.getElementById('divid1').style.position = 'absolute';
            document.getElementById('divid1').style.left= '300px'; 
            document.getElementById('divid1').style.top= '100px';              
            document.getElementById('divid1').style.visibility='visible'; 
            document.getElementById('frameeditProc').src ='Bill_Sys_EditRProcPopupPage.aspx';  
            return false;
          
       }
       
        function ViewDocumentPopup(caseid,Eventprocid,speciality)
       {
         
            
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left= '300px'; 
            document.getElementById('divid2').style.top= '100px';              
            document.getElementById('divid2').style.visibility='visible'; 
           document.getElementById('frm').src ='Bill_Sys_ViewDocuments.aspx?CaseID='+caseid+'&Type=YES&EProcid='+Eventprocid+'&spc='+speciality;
            return false;
          
       }
 
       
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
                                    <td style="width: 99%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; height: 25px;" colspan="4">
                                                    <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server">Dash board</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="TDPart" align="right">
                               
                                       <asp:Button ID="btnnext" runat="server" style="float:left;" CssClass="Buttons" Text="Received Document" OnClick="btnnext_Click" Width="125px" Visible="False"   />
                                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel" style="float:right;"
                                            OnClick="btnExportToExcel_Click" />
                                        <asp:Button ID="btnSpeciality" runat="server" Text="Export To Excel" OnClick="btnSpeciality_Click"
                                            Width="0px" Height="0px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="false" ></asp:TextBox>
                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                         <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="TDPart">
                                        <%--<asp:Label ID ="lblTotal" runat="server" Text="Total" Font-Bold="True" ></asp:Label>--%>
                                        
                                        <asp:Label ID="lblBillAmount" runat="server" Text="BillAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                        <asp:Label ID= "lblBillAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblPaidAmount" runat="server" Text="PaidAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                        <asp:Label ID ="lblPaidAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblBalance" runat="server" Text="Balance" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                        <asp:Label ID ="lblBalancevalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                                        <asp:DataGrid ID="grdCaseMaster" Width="100%" runat="Server" CssClass="GridTable"
                                            OnItemCommand="grdCaseMaster_ItemCommand" AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--1--%>
                                                <asp:TemplateColumn HeaderText="Case #" Visible="false">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblLocationName1" runat="server"  Visible="false"  Text="Location" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--2--%>
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblLocationName" runat="server"  Visible="false"  Text="Location" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--3--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:TemplateColumn HeaderText="Patient" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_blank">
                                                           <%-- <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>--%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--5--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="false">
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="PROVIDER ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_NAME" HeaderText="Provider" Visible="False">
                                                </asp:BoundColumn>
                                                <%--9--%>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_ID" HeaderText="INSURANCE ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                <%--11--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ID" HeaderText="OFFICE ID" Visible="False"></asp:BoundColumn>
                                                <%--12--%>
                                                <%--13--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundColumn>
                                                <%--14--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="DOCTOR ID" Visible="False"></asp:BoundColumn>
                                                <%--15--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundColumn>
                                                <%--16--%>
                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}">
                                                </asp:BoundColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="SZ_CLAIM_AMOUNT" HeaderText="Claim Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--18--%>
                                                <asp:BoundColumn DataField="SZ_PAID_AMOUNT" HeaderText="Paid Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--19--%>
                                                <asp:BoundColumn DataField="SZ_BALANCE" HeaderText="Balance" Visible="False"></asp:BoundColumn>
                                                <%--20--%>
                                                <asp:BoundColumn DataField="SZ_APPOINTMENT" HeaderText="Today's Appointment" Visible="false">
                                                </asp:BoundColumn>
                                                <%--21--%>
                                                <asp:TemplateColumn HeaderText="Diagnosis Code" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_AssociateDignosisCode.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_self" shape="rect">Update </a>
                                                            <%--(<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>)--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--22--%>
                                                <asp:TemplateColumn HeaderText="Visit" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientVisits.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Visit</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--23--%>
                                                <asp:TemplateColumn HeaderText="Treatment" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Treatments.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Treatment</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--24--%>
                                                <asp:TemplateColumn HeaderText="Test" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Tests.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Test</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--25--%>
                                                <asp:BoundColumn DataField="SZ_CASE_STATUS_ID" HeaderText="STATUS ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--26--%>
                                                <asp:BoundColumn DataField="SZ_STATUS_NAME" HeaderText="Status" Visible="false"></asp:BoundColumn>
                                                <%--27--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="ATTORNEY ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--28--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Attorney" Visible="false">
                                                </asp:BoundColumn>
                                                <%--29--%>
                                                <asp:TemplateColumn HeaderText="Documents" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--30--%>
                                                <asp:TemplateColumn HeaderText="Templates" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Template" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Template Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--31--%>
                                                <asp:TemplateColumn HeaderText="Schedule" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCalendarEvent" runat="server" Text="Calender Event" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>'
                                                            CommandName="Calender Event">
																		 <img src="Images/cal.gif" style="border:none;" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--32--%>
                                                <asp:TemplateColumn HeaderText="Bills" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBillTransaction" runat="server" Text="Add | " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Bill Transaction"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkViewBills" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="View Bills"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--33--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--34--%>
                                                <asp:TemplateColumn HeaderText="Desk">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Patient Desk"
                                                            ToolTip="Patient Desk">
																		                
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--35--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                        
                                        <%--Search Bill--%>
                                        
                                        <asp:DataGrid ID="grdBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" Visible="false"
                                            AllowPaging="true" PagerStyle-Mode="NumericPages" OnItemDataBound="grdBillSearch_ItemDataBound" OnPageIndexChanged="grdBillSearch_PageIndexChanged" PageSize="50">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--1--%>
                                                <asp:TemplateColumn HeaderText="Bill Number">
                                                <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillno" runat="server" CommandName="BILLNUMBER" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill Number</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--2--%>
                                                <asp:TemplateColumn HeaderText ="Case #">
                                                      <HeaderTemplate>
                                                        <asp:LinkButton ID="lblcaseno" runat="server" CommandName="CASENUMBER" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                              <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible = "false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="CHART_NO" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No.</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Patient Name" Visible = "true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PATIENT_NAME" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText = "Insurance Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceName" runat ="server" CommandName="InsuranceName" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container, "DataItem.SZ_INSURANCE_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField = "SZ_CASE_ID" HeaderText = "CaseID" Visible = "False"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False"></asp:BoundColumn>
                                                <%--5--%>
                                               <asp:TemplateColumn HeaderText="Bill Date">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOA" runat="server" CommandName="BILLDATE" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
                                               
                                             
                                               <%-- <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date"
                                                    DataFormatString="{0:dd MMM yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off"
                                                    DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundColumn>
                                                <%--8--%>
                                                
                                                 <asp:TemplateColumn HeaderText = "Bill Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillamount" runat ="server" CommandName="BILLAMOUNT" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                               <%-- <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--9--%>
                                                
                                                  <asp:TemplateColumn HeaderText = "Paid Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblPaidAmount" runat ="server" CommandName="PAIDAMOUNT" CommandArgument="PAID_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Paid Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               <%-- <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--10--%>
                                                
                                                <asp:TemplateColumn HeaderText = "Balance">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBalance" runat ="server" CommandName="BALANCE" CommandArgument="FLT_BALANCE"
                                                            Font-Bold="true" Font-Size="12px">Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%--<asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Make Payment" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--14--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--15--%>
                                                <asp:TemplateColumn HeaderText="Generate bill" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--16--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        
                                        <%--End--%>
                                        
                                        <%--Grid used for Export to excel--%>
                                        <asp:DataGrid ID="grdEEBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" 
                                            AllowPaging="false" PagerStyle-Mode="NumericPages" OnItemDataBound="grdBillSearch_ItemDataBound"
                                            OnPageIndexChanged="grdBillSearch_PageIndexChanged" PageSize="50" Visible="false">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField ="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField ="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField ="SZ_INSURANCE_NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:dd MMM yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        
                                        <%--End--%> 
                                        <div style="overflow: scroll; height: 600px">
                                        <asp:DataGrid ID="grdAllReports" runat="server" AutoGenerateColumns="false"
                                            CssClass="GridTable" OnPageIndexChanged="grdAllReports_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                             Width="100%" Visible="false" OnItemCommand="grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                             
                                             <%--0--%>
                                            <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                             <%--1--%>
                                             <asp:TemplateColumn HeaderText="CHART NO">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkChartNoSearch" runat="server" CommandName="CHART_NO" CommandArgument="CHART_NO" Font-Bold="true" Font-Size="12px">Chart #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.CHART_NO")%>                                                                                                            
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                             <%--2--%>
                                             <asp:TemplateColumn HeaderText="PATIENT NAME">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkCaseNoSearch" runat="server" CommandName="CASE_NO" CommandArgument="CASENO" Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.CASE_NO")%>                                                                                                            
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              <%--3--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false">
                                                </asp:BoundColumn>
                                                <%--5--%>
                                              
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="PATIENT_NAME" Visible="false">
                                                </asp:BoundColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="DT_DATE_OF_SERVICE" Visible="false">
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                  <asp:TemplateColumn HeaderText="PATIENT NAME">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PATIENT_NAME" CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                            
                                                          <asp:LinkButton ID="lnkWorkAreaPage" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="workarea"></asp:LinkButton>                                                     
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            
                                                <%--8--%>
                                                <asp:TemplateColumn HeaderText="Date Of Service">
                                                  <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkDateOfServiceSearch" runat="server" CommandName="DT_EVENT_DATE" CommandArgument="DT_EVENT_DATE" Font-Bold="true" Font-Size="12px">Date Of Service</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAppointment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>'
                                                            CommandName="appointment"  ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--9--%>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure code"></asp:BoundColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <%--11--%>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                <%--12--%>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="CompanyID" Visible="false"></asp:BoundColumn>
                                                 <%--13--%>
                                                <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="EventProcID" Visible="false"></asp:BoundColumn>
                                                <%--14--%>
                                                <asp:BoundColumn DataField="SZ_PROC_CODE" HeaderText="Pro Code" Visible="false"></asp:BoundColumn>
                                                 <%--15--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false"></asp:BoundColumn>
                                                <%--16--%>
                                                 <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No." Visible="false"></asp:BoundColumn>
                                                 <%--17--%>
                                                 <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Case No." Visible="false"  ></asp:BoundColumn>
                                                 <%--18--%>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="false"  ></asp:BoundColumn>
                                                  <%--19--%>
                                                 <asp:BoundColumn DataField="sz_procedure_group" HeaderText="sz_procedure_group" Visible="false"  ></asp:BoundColumn>
                                                    <%--20--%>
                                                 <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="sz_procedure_group" Visible="false"  ></asp:BoundColumn>
                                                    <%--21--%>
                                                 <asp:TemplateColumn HeaderText="">
                                                    
                                                    <ItemTemplate>
                                                            
                                                          <asp:LinkButton ID="lnkEditProc" runat="server" Text='Edit Procedure Code'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_PROCEDURE_GROUP_ID")%>' CommandName="edit"></asp:LinkButton>                                                     
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                    <%--22--%>
                                                 <asp:TemplateColumn HeaderText="">
                                                    
                                                    <ItemTemplate>
                                                            
                                                          <asp:LinkButton ID="lnkView" runat="server" Text='View Document'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_PROCEDURE_GROUP_ID")%>' CommandName="view"></asp:LinkButton>                                                     
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 
                                            </Columns>
                                          
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </div>
                                        <br />
                                        <asp:DataGrid ID="grdExportToExcel" runat="server" PageSize="50" AllowPaging="true" AutoGenerateColumns="false"
                                            CssClass="GridTable" Visible="False">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Service"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                         <input type="button" runat="server" style="visibility:hidden" id="btnHdn" onclick="btnHdn_Click" />
                                        </td>
                                        
                                        
                                        
                                </tr>
                                
                                <tr>
                                            <td style="width: 99%; height:auto; float:left;"  class="TDPart">
     
                                              <div id="divid" style="position: absolute; width: 600px; height: 600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;  window.parent.document.location.href='Bill_Sys_PaidBills.aspx?Flag=report&Type=p&popup=done1'; " style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="600px"></iframe>
    </div>
    
         <div id="divid1" style="position: absolute; width: 400px; height: 600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('divid1').style.visibility='hidden';document.getElementById('divid1').style.zIndex = -1; window.parent.document.location.href='Bill_Sys_PaidBills.aspx?Flag=report&Type=p&popup=done1';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditProc" src="" frameborder="0" height="400px" width="600px"></iframe>
    </div>
    
     <div id="divid2" style="position: absolute; width: 600px; height: 350px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('divid2').style.visibility='hidden';document.getElementById('divid2').style.zIndex = -1; window.parent.document.location.href='Bill_Sys_PaidBills.aspx?Flag=report&Type=p&popup=done1' " style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frm" src="" frameborder="0" height="350px" width="600px"></iframe>
    </div>
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
    <div id="divDashBoard" visible="false" style="position: absolute; width: 800px; height: 475px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 430; float: left; position: relative;">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td style="width: 97%" class="TDPart">
                            <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Specialty</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    You have
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="width: 99%; height: 10px;" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Today's Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%">
                                            Weekly &nbsp;Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; vertical-align: top; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Bill Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%;" valign="top">
                                            Desk</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have&nbsp;
                                            <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Information</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%;" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Report Section</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative; left: 0px;
                                    top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Procedure Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 195px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Visits</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblVisits" runat="server" Visible="true"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul style="list-style-type: disc; padding-left: 60px;">
                                                            <li><a id="hlnkTotalVisit" href="#" runat="server">
                                                                <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit</li><li>
                                                                    <a id="hlnkBilledVisit" href="#" runat="server">
                                                                        <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                </li>
                                                            <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                            </li>
                                                        </ul>
                                                        <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                            PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                            PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                            PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Patient Visit Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
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
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
   
</asp:Content>
