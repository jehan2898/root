<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_UnbilledVisits.aspx.cs" Inherits="Bill_Sys_UnbilledVisits" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
         
         
         function setDiv(DoctorID,EventID)
       {
            document.getElementById("divid").style.position = "absolute";
            document.getElementById("divid").style.left= "70px";
            document.getElementById("divid").style.top= "   10px";
            document.getElementById('divid').style.zIndex= '1'; 
            document.getElementById("divid").style.visibility="visible";
            document.getElementById("divid").style.width= "350px";
            document.getElementById("divid").style.height= "180px";
            document.getElementById("frameeditexpanse").style.width= "700px";
            document.getElementById("frameeditexpanse").style.height= "500px";
            document.getElementById("frameeditexpanse").src="Ajax Pages/Bill_Sys_PopUpBillTransactionAllVisit.aspx?F=U&DID=" + DoctorID+ "&EID=" + EventID;
        }
        
         function showDisplayDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.height='180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.top = '300px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.left ='620px';
       }
        
       function CloseDisplayDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDisplayDiagnosisCode').style.visibility = 'hidden';  
       }
       
       function OpenPopUPBillTransaction(p_szCaseID,p_szPatientID,p_szCaseNO,DoctorID,EventID)
        {
             
            document.getElementById("divid").style.position = "absolute";
            document.getElementById("divid").style.left= "70px";
            document.getElementById("divid").style.top= "10px";
            document.getElementById('divid').style.zIndex= '1'; 
            document.getElementById("divid").style.visibility="visible";
            document.getElementById("divid").style.width= "350px";
            document.getElementById("divid").style.height= "180px";
            document.getElementById("frameeditexpanse").style.width= "700px";
            document.getElementById("frameeditexpanse").style.height= "500px";
            document.getElementById("frameeditexpanse").src="Ajax Pages/Bill_Sys_PopUpBillTransactionAllVisit.aspx?F=U&DID=" + DoctorID+ "&EID=" + EventID + '&P_CASE_ID='+p_szCaseID + '&P_PATIENT_ID='+p_szPatientID+'&P_CASE_NO='+p_szCaseNO+'&PROC_GROUP_ID='+document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality').value;
        }
        
        function OpenPopUPDisplayDiagCode(p_szCaseID,p_szDoctorID)
        {
            document.getElementById("divDisplayDiagCode").style.position = "absolute";
            document.getElementById("divDisplayDiagCode").style.left= "300px";
            document.getElementById("divDisplayDiagCode").style.top= "200px";
            document.getElementById('divDisplayDiagCode').style.zIndex= '1'; 
            document.getElementById("divDisplayDiagCode").style.visibility="visible";
            document.getElementById("divDisplayDiagCode").style.width= "400px";
            document.getElementById("divDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").style.width= "400px";
            document.getElementById("ifrmDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").src="Bill_Sys_PopupShowDiagnosisCode.aspx?P_SZ_DOCTOR_ID=" + p_szDoctorID +  "&P_SZ_CASE_ID=" +p_szCaseID;
        }
        function closePopup()
        {
           document.getElementById('divid').style.zIndex = '-1'; 
           document.getElementById('divid').style.visibility='hidden';
        }
    </script>

    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
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
                                        <td style="width: 100%" class="TDPart">
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                        <b>Un-billed Visit Report </b>
                                                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Date&nbsp; &nbsp;</td>
                                                    <td style="width: 35%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        To Date&nbsp;
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxcontrol:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        Doctor Name
                                                    </td>
                                                    <td style="width: 35%; height: 18px;">
                                                        <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                        &nbsp;
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                        Specialty
                                                    </td>
                                                    <td style="width: 35%; height: 18px;">
                                                        <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                            Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                            Visible="False" Width="94px"></asp:Label></td>
                                                    <td style="width: 35%">
                                                        <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="65%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                            Visible="false" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                    <td style="width: 35%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td style="width: 15%">
                                                    </td>--%>
                                                    <td style="width: 85%" colspan="2">
                                                        <asp:RadioButtonList ID="btnRadio_Group" RepeatDirection="Horizontal" CssClass="lbl"
                                                            runat="server">
                                                            <asp:ListItem Selected="True" Text="By Doctor" Value="Doctor"></asp:ListItem>
                                                            <asp:ListItem Text="By Patient" Value="Patient"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                        Case Type:
                                                    </td>
                                                    <td style="width: 35%; height: 18px;" colspan="2">
                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                            OldText="" StausText="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4">
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <%--<asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSearch_Click"/>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <div style="width: 100%; text-align: left;" class="ContentLabel">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" style="width: 50%">
                                                            Total Count :
                                                            <asp:Label ID="lblCount" Font-Bold="true" Font-Size="10" runat="server"></asp:Label>
                                                            &nbsp; <a id="hlnkShowCount" href="#" runat="server" title="Total Count By Speciality"
                                                                class="lbl" visible="false">Total Count By Specialty</a>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                                OnClick="btnSearch_Click" />&nbsp;
                                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                                OnClick="btnExportToExcel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--<img src="Images/actionEdit.gif" style="border-style: none;" /> -->
                                                <ajaxcontrol:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowCount"
                                                    PopupControlID="pnlShowCount" Position="Center" OffsetX="100" OffsetY="10" />
                                                <%--<asp:Panel ID="pnlShowCount" runat="server" Style="background-color: white; border-color:SteelBlue;border-width:1px;border-style:solid;">--%>
                                                <asp:Panel ID="pnlShowCount" runat="server">
                                                    <asp:DataGrid ID="grdCount" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count"></asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" OnItemCommand="grdAllReports_ItemCommand">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%-- 0 --%>
                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case No" Visible="false"></asp:BoundColumn>
                                                    <%-- 1 --%>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%-- 2 --%>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkCaseSearch" runat="server" CommandName="CaseSearch" CommandArgument="cast(outtable.sz_case_id as integer)"
                                                                Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkCaseSearch" runat="server" CommandName="CaseID" CommandArgument="sz_case_id"
                                                                Font-Bold="true" Font-Size="12px"><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Document Manager" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%-- 3 --%>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PatientNameSearch"
                                                                CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>
                                                            <%-- <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%-- 4 --%>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkEventDateSearch" runat="server" CommandName="EventDateSearch"
                                                                CommandArgument="outtable.DT_EVENT_DATE" Font-Bold="true" Font-Size="12px">Date Of Visit</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container,"DataItem.DT_EVENT_DATE")%>
                                                            <%-- <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%-- 5 --%>
                                                    <asp:BoundColumn DataField="SZ_VISIT" HeaderText="Visit Type"></asp:BoundColumn>
                                                    <%-- 6 --%>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkDoctorNameSearch" runat="server" CommandName="DoctorNameSearch"
                                                                CommandArgument="DOCTOR_NAME" Font-Bold="true" Font-Size="12px">Doctor Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container,"DataItem.DOCTOR_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%-- 7 --%>
                                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="true">
                                                    </asp:BoundColumn>
                                                    <%-- 8 --%>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkSpecialitySearch" runat="server" CommandName="SpecialitySearch"
                                                                CommandArgument="Speciality" Font-Bold="true" Font-Size="12px">Specialty</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container,"DataItem.Speciality")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--  9--%>
                                                    <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--  10--%>
                                                    <asp:BoundColumn DataField="sz_case_no" HeaderText="Case No." Visible="false"></asp:BoundColumn>
                                                    <%-- 11--%>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="patient id" Visible="false"></asp:BoundColumn>
                                                    <%--  12 --%>
                                                    <asp:BoundColumn DataField="NO_OF_DAYS" HeaderText="No Of Days"></asp:BoundColumn>
                                                     <%--  13 --%>
                                                    <asp:BoundColumn DataField="SZ_INS" HeaderText="Insurance Company"></asp:BoundColumn>
                                                    <%-- 14--%>
                                                    <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Claim Number"></asp:BoundColumn>
                                                    <%-- 15 --%>
                                                    <asp:TemplateColumn HeaderText="Diagnosis Code">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAddDiagCode" runat="server" Text="Add" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                CommandName="Add Diagnosis Code" Visible="false"> </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDisplayDiagCode" runat="server" Text="" CommandName="Display Diag Code"
                                                                Visible="false">Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</asp:LinkButton>
                                                            <a href="#" onclick="javascript:OpenPopUPDisplayDiagCode('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>')">
                                                                Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                    <%-- 16 --%>
                                                    <asp:TemplateColumn HeaderText="Group Service">
                                                        <ItemTemplate>
                                                            <a href="#" onclick="javascript:OpenPopUPBillTransaction('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>','<%# DataBinder.Eval(Container,"DataItem.sz_case_no")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>','<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>')">
                                                                Add Bills</a>
                                                            <asp:LinkButton ID="lnkAddGroupService" runat="server" Text="Add" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                CommandName="Group Service" Visible="false"> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                    <%-- 17--%>
                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="I_EVENT_ID" Visible="false"></asp:BoundColumn>
                                                    <%-- 18--%>
                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="DT_EVENT_DATE" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%-- 19--%>
                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR_NAME" Visible="false"></asp:BoundColumn>
                                                <%-- 20--%>
                                                      <asp:BoundColumn DataField="Speciality" HeaderText="Speciality" Visible="false"></asp:BoundColumn>
                                                    <%-- 21--%>
                                                    <asp:BoundColumn DataField="DIAG COUNT" HeaderText="DIAG COUNT" Visible="false"></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
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
        <%--Display Diagnosis Code--%>
        <asp:Panel ID="pnlDisplayDiagnosisCode" runat="server" Style="width: 450px; height: 0px;
            background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
            visibility: hidden;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td align="right">
                        <a onclick="CloseDisplayDiagnosisCodePopup();" style="cursor: pointer;" title="Close">
                            X</a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 102%" valign="top">
                        <div style="height: 200px; overflow-y: scroll;">
                            <asp:DataGrid ID="grdDisplayDiagonosisCode" runat="server" Width="100%" CssClass="GridTable"
                                AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                                OnPageIndexChanged="grdDisplayDiagonosisCode_PageIndexChanged">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="CODE" HeaderText="DIAGNOSIS CODE" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Diagnosis Codes"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" />
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div id="divid" style="position: absolute; width: 700px; height: 500px; background-color: #DBE6FA;
            visibility: hidden;">
            <div style="position: relative; text-align: right; background-color: #8babe4; width: 700px">
                <%--<a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden';  "
                style="cursor: pointer;" title="Close">X</a>--%>
                <asp:Button ID="btnlClosePopUP" runat="server" OnClick="btnlClosePopUP_Click" Text="X"
                    CssClass="Buttons" />
            </div>
            <iframe id="frameeditexpanse" src="" frameborder="0" height="500px" width="700px"></iframe>
        </div>
        <div id="divDisplayDiagCode" style="position: absolute; width: 400px; height: 300px;
            background-color: #DBE6FA; visibility: hidden;">
            <div style="position: relative; text-align: right; background-color: #8babe4; width: 400px">
                <a onclick="document.getElementById('divDisplayDiagCode').style.zIndex = '-1'; document.getElementById('divDisplayDiagCode').style.visibility='hidden';"
                    style="cursor: pointer;" title="Close">X</a>
            </div>
            <iframe id="ifrmDisplayDiagCode" src="" frameborder="0" height="250px" width="400px">
            </iframe>
        </div>
    </div>
</asp:Content>
