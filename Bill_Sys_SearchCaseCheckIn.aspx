<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_SearchCaseCheckIn.aspx.cs" Inherits="Bill_Sys_SearchCaseCheckIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Src="UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
     function ascii_value(c){
             c = c . charAt (0);
             var i;
             for (i = 0; i < 256; ++ i)
             {
                  var h = i . toString (16);
                  if (h . length == 1)
                    h = "0" + h;
                   h = "%" + h;
                  h = unescape (h);
                  if (h == c)
                    break;
             }
             return i;
        }
    function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
            
            
         }

    </script>
    
     <script>
    
        function showCheckinPopup()
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.height='250px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.top = '250px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.left ='450px';
        }
        
        function CloseCheckinPopup()
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlCheckin').style.visibility = 'hidden';  
        }
        
        function checkValidate()
        {
            if((document.getElementById('_ctl0_ContentPlaceHolder1_extddlVisitType').value == 'NA'))
            {
                alert("All fields are mandatory.");
                return false;
            }
            return true;
        }
    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%;">
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
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <%--<tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case ID:</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtCaseID" runat="server" Width="100px" CssClass="text-box"></asp:TextBox></td>
                                                  <td class="ContentLabel" style="width: 15%">
                                                  </td>
                                                   <td style="width: 35%">
                                                   </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <div align="left" style="vertical-align:top;">
                                                     <asp:LinkButton ID="Button1" style="visibility:hidden;" runat="server" OnClick="Button1_Click"></asp:LinkButton>
                                                        <a id="hlnkShowSearch" style="cursor:pointer;height:240px; vertical-align:top;"  class="Buttons" runat="server" title ="Quick Search" >Quick Search</a>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case Type:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="98%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---" />
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case Status:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="98%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="CASESTATUS_LIST" Procedure_Name="SP_MST_CASE_STATUS" Selected_Text="---Select---" />
                                                    </div>
                                                </td>
                                                <%--<td class="ContentLabel" style="width: 15%;valign:top">
                                                    Provider:</td>
                                                <td style="width: 35%">
													<div>
														<asp:TextBox ID="txtProviderName" runat="server" Width="98%"></asp:TextBox>
													</div>
													<div>
														 <extddl:ExtendedDropDownList ID="extddlProvider" runat="server" Width="98%"
																Connection_Key="Connection_String" Flag_Key_Value="PROVIDER_LIST" Procedure_Name="SP_MST_PROVIDER"
																Selected_Text="---Select---" AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlProvider_extendDropDown_SelectedIndexChanged" />
													</div>
												</td>--%>
                                            </tr>
                                            <tr  >
                                                <td class="ContentLabel" style="width: 15%">
                                                    Patient:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="98%"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                        <extddl:ExtendedDropDownList ID="extddlPatient" Width="98%" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Selected_Text="--- Select ---"
                                                            AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged" />
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Insurance:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="98%"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="98%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="---Select---"
                                                            OnextendDropDown_SelectedIndexChanged="extddlInsurance_extendDropDown_SelectedIndexChanged"
                                                            AutoPost_back="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Date of Accident:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtDateofAccident" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtDateofAccident"
                                                            PopupButtonID="imgbtnDateofAccident" />
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Date of Birth:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtDateofBirth" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtDateofBirth"
                                                            PopupButtonID="imgbtnDateofBirth" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%" />
                                                <td class="ContentLabel" style="width: 35%" />
                                                <td class="ContentLabel" style="width: 15%" />
                                                <td class="ContentLabel" style="width: 35%" />
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%" bgcolor="">
                                                    Claim Number:</td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    SSN #:
                                                </td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:TextBox ID="txtSSNNo" runat="server" CssClass="text-box"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%" bgcolor="">
                                                    <asp:Label ID="lblOffice" runat="server" class="ContentLabel" Visible="false" Text="Office Name:"></asp:Label></td>
                                                <td style="width: 35%">
                                                    <div>
                                                         <extddl:ExtendedDropDownList ID="extddlOffice" Visible="false" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---" />
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    
                                                </td>
                                                <td style="width: 35%">
                                                    <div>
                                                       </div>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                
                                                    
                                                        
                                                        <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                                                         <asp:TextBox ID="txtCaseIDSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>                                              
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; height:20px; text-align:right;">
                                    <asp:Button ID="btnSoftDelete" CssClass="Buttons" runat="server" Text="Soft Delete" OnClick="btnSoftDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 303px;" class="TDPart">
                                        <asp:DataGrid ID="grdCaseMaster" Width="100%" runat="Server" CssClass="GridTable"
                                            OnItemCommand="grdCaseMaster_ItemCommand" AutoGenerateColumns="False" OnPageIndexChanged="grdCaseMaster_PageIndexChanged" PageSize="100" 
                                            AllowPaging="True" OnItemDataBound="grdCaseMaster_ItemDataBound">
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
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
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
                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
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
                                                <asp:CheckBox ID="chkSoftDelete" runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--37--%>
                                                <asp:TemplateColumn HeaderText="Desk">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Patient Desk" ToolTip="Patient Desk" >
																		                
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--38--%>
                                                <asp:TemplateColumn HeaderText="Check in">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCheckin" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Check in" ToolTip="Check in" Text="Check in">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
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
             <%--<div id="divid2" style="position: absolute;left:800px; top:500px; width: 500px; height: 380px; background-color: #DBE6FA;
                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                        <div style="position: relative; text-align: right; background-color: #8babe4;">
                            <a onclick="CloseSource();" style="cursor: pointer;" title="Close">X</a>
                        </div>
                        <iframe id="iframeQuickSearch" src="" frameborder="0" height="380" width="500"></iframe>
                    </div>
      --%>
      
        <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowSearch"
            PopupControlID="pnlShowSearch" Position= "Center" OffsetX="300" OffsetY="120" />
            <asp:Panel ID="pnlShowSearch" runat="server" Style="left:400px; top:600px; width:380px;height:150px;background-color: #DBE6FA;
                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                        border-left: silver 1px solid; border-bottom: silver 1px solid; vertical-align:middle; text-align:center;">
                <table style="text-align:center; vertical-align:middle;">
                
                    <tr>
                        <td><asp:Label id="lblChartSearch" runat="server" Text="Chart No."></asp:Label>
                        </td>
                        <td> <asp:TextBox ID="txtChartnoSearch" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label id="lblCaseIDSearch" runat="server" Text="Case ID"></asp:Label>
                        </td>
                        <td> <asp:TextBox ID="txtCIDSearch" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label id="lblPatientLNameSearch" runat="server" Text="Patient Last Name"></asp:Label>
                        </td>
                        <td> <asp:TextBox ID="txtLNameSearch" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td><asp:Label id="lblPatientFNameSearch" runat="server" Text="Patient First Name"></asp:Label>
                        </td>
                        <td> <asp:TextBox ID="txtFNameSearch" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                <table>
                     <tr>                        
                        <td align="center"> 
                             <asp:Button ID="btnQuickSearch" runat="server" Text="Search" OnClick="btnQuickSearch_Click"
                              Width="80px" CssClass="Buttons" />
                        </td>
                    </tr>
                </table>
                 
                 
                
             </asp:Panel>
                                

         <asp:Panel ID="pnlCheckin" runat="server" Style="width: 420px; height: 250px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        <table width="100%" class="TDPart">
            <tr>
               <td colspan="2" width="100%">
                   <table width="100%">
                       <tr>
                           <td align="center" width="80%">
                               <b>Check in Information</b>
                           </td>
                           <td align="right" width="20%">
                               <a onclick="CloseCheckinPopup();" style="cursor: pointer;" title="Close">X</a>
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
            <tr>
               <td colspan="2" width="100%">
                    <asp:Label ID="lblCheckinMsg" runat="server" ForeColor="red"></asp:Label>
               </td>
            </tr>
            <tr>
                <td width="30%" valign="top">
                    Doctor List : 
                </td>
                <td width="70%">
                    <asp:ListBox ID="lbDoctor" runat="server" SelectionMode="Multiple" Width="97%" Height="100px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    Date : 
                </td>
                <td width="70%">
                    <asp:TextBox ID="txtVisitDate" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    Visit Type : 
                </td>
                <td width="70%">
                    <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="97%"
                            Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                </td>
            </tr>
           
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnCheckinSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnCheckinSave_Click"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
