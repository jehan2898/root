<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="Bill_Sys_HardDelete.aspx.cs" Inherits="Bill_Sys_HardDelete" %>


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
                                    <td class="TDPart" style="width: 100%; text-align:right; height:20px;">
                                    <asp:Button ID="btnUnDelete" runat="server" CssClass="Buttons" Text="Un-Delete" OnClick="btnUnDelete_Click" />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdCaseMaster" Width="100%" runat="Server" CssClass="GridTable"
                                             AutoGenerateColumns="False" 
                                            AllowPaging="True" OnItemDataBound="grdCaseMaster_ItemDataBound" OnItemCommand="grdCaseMaster_ItemCommand" OnPageIndexChanged="grdCaseMaster_PageIndexChanged">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:TemplateColumn HeaderText="" ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                       <%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 
                                             <%--1--%>
                                                <asp:TemplateColumn HeaderText="Case #" Visible="False">
                                                   
                                                          <HeaderTemplate>
                                                <asp:LinkButton ID="lnlCase2" runat="server" CommandName="CaseSearch" CommandArgument="sz_Id" Font-Bold="true"
                                                                        Font-Size="12px" >Case #</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                   
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                               <%--2--%> 
                                               
                                                <asp:TemplateColumn HeaderText="Case #">
                                                 <HeaderTemplate>
                                                <asp:LinkButton ID="lnlCase" runat="server" CommandName="CaseSearch" CommandArgument="sz_Id" Font-Bold="true"
                                                                        Font-Size="12px" >Case #</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--3--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="False"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:TemplateColumn HeaderText="Chart No." Visible="False">
                                                 <HeaderTemplate>
                                                <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="ChartNoSearch" CommandArgument="SZ_CHART_NO" Font-Bold="true"
                                                                        Font-Size="12px" >Chart No.</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                       <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                          
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%--5--%>
                                                <asp:TemplateColumn HeaderText="Patient" Visible="False">
                                                     <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatient" runat="server" CommandName="PatientSearch" CommandArgument="SZ_PATIENT_NAME" Font-Bold="true"
                                                                        Font-Size="12px" >Patient</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_UpgradePatient.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_blank">
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--6--%>
                                                <asp:TemplateColumn HeaderText="Patient Name">
                                                     <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatient2" runat="server" CommandName="PatientSearch" CommandArgument="SZ_PATIENT_NAME" Font-Bold="true"
                                                                        Font-Size="12px" >Patient Name</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                      
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
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
                                                <asp:TemplateColumn HeaderText="Case Type" >
                                                     <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlCaseType" runat="server" CommandName="CASETYPESearch" CommandArgument="SZ_CASE_TYPE_NAME" Font-Bold="true"
                                                                        Font-Size="12px" >Case Type</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                      
                                                            <%#DataBinder.Eval(Container, "DataItem.SZ_CASE_TYPE_NAME")%>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Date Of Accident" >
                                                     <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOA" runat="server" CommandName="DOASearch" CommandArgument="DT_DATE_OF_ACCIDENT" Font-Bold="true"
                                                                        Font-Size="12px" >Date Of Accident</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                      
                                                            <%#DataBinder.Eval(Container, "DataItem.DT_DATE_OF_ACCIDENT")%>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
                                                <%--12--%>
                                                <asp:TemplateColumn HeaderText="Date Open" >
                                                     <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOO" runat="server" CommandName="DOOSearch" CommandArgument="DT_CREATED_DATE" Font-Bold="true"
                                                                        Font-Size="12px" >Date Open</asp:LinkButton>
                                                                         </HeaderTemplate>
                                                    <ItemTemplate>
                                                      
                                                            <%#DataBinder.Eval(Container, "DataItem.DT_CREATED_DATE")%>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
                                                <%--13--%>
                                                <asp:BoundColumn DataField="SZ_CLAIM_AMOUNT" HeaderText="Claim Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--14--%>
                                                <asp:BoundColumn DataField="SZ_PAID_AMOUNT" HeaderText="Paid Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--15--%>
                                                <asp:BoundColumn DataField="SZ_BALANCE" HeaderText="Balance" Visible="False"></asp:BoundColumn>
                                                <%--16--%>
                                                <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="Appointment"></asp:BoundColumn>
                                                <%--17--%>
                                                <asp:TemplateColumn HeaderText="Diagnosis Code" Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_AssociateDignosisCode.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_self" shape="rect">Update (<%# DataBinder.Eval(Container, "DataItem.TOTAL_DIAGNOSIS_CODE_COUNT")%>)</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--18--%>
                                                <asp:TemplateColumn HeaderText="Visit" Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientVisits.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>&companyId=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>"
                                                            target="_self" shape="rect">Visit</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--19--%>
                                                <asp:TemplateColumn HeaderText="Treatment" Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Treatments.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>&companyId=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>"
                                                            target="_self" shape="rect">Treatment</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--20--%>
                                                <asp:TemplateColumn HeaderText="Test" Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Tests.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>&companyId=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>"
                                                            target="_self" shape="rect">Test</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--21--%>
                                                <asp:BoundColumn DataField="SZ_CASE_STATUS_ID" HeaderText="STATUS ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--22--%>
                                                <asp:BoundColumn DataField="SZ_STATUS_NAME" HeaderText="Status" Visible="False"></asp:BoundColumn>
                                                <%--23--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="ATTORNEY ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--24--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Attorney" Visible="False">
                                                </asp:BoundColumn>
                                                <%--25--%>
                                                <asp:TemplateColumn HeaderText="Documents" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--26--%>
                                                <asp:TemplateColumn HeaderText="Templates" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Template" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Template Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--27--%>
                                                <asp:TemplateColumn HeaderText="In Schedule">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCalendarEvent" runat="server" Text="Calender Event" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>'
                                                            CommandName="Calender Event">
																		                <img src="Images/cal.gif" style="border:none;" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--28--%>
                                                <asp:TemplateColumn HeaderText="Out Schedule">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOutScheduleCalendarEvent" runat="server" Text="Calender Event"
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Out Calender Event">
																		                <img src="Images/cal.gif" style="border:none;" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--29--%>
                                                <asp:TemplateColumn HeaderText="Bills" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBillTransaction" runat="server" Text="Add | " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Bill Transaction"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkViewBills" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="View Bills"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--30--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--31--%>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" Visible="False"></asp:BoundColumn>
                                                 <%--32--%>
                                                <asp:TemplateColumn HeaderText="Schedule">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOutScheduleCalendarEvent1" runat="server" Text="Calender Event"
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Out Calender Event">
																		                <img src="Images/cal.gif" style="border:none;" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                 <%--33--%>
                                                <asp:BoundColumn DataField="SZ_COMPANY_NAME" HeaderText="SZ_COMPANY_NAME" Visible="False">
                                                </asp:BoundColumn>
                                                 <%--34--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient" Visible="False"></asp:BoundColumn>
                                                <%--35--%>
                                                <asp:BoundColumn DataField="BT_DELETE" HeaderText="Delete" Visible="False"></asp:BoundColumn>
                                                 <%--36--%>
                                                <asp:TemplateColumn HeaderText="Delete">
                                                <ItemTemplate>
                                                <asp:CheckBox ID="chkHardDelete" runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        
                         <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>  
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
