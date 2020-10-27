<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ThirtyDaysUnbilledVisits.aspx.cs" Inherits="Bill_Sys_ThirtyDaysUnbilledVisits" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
       
       
       function showDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height='180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.top = '300px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.left ='620px';
	   }
        
       function CloseDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'hidden'; 
             
       }
       
       function setDiv(DoctorID,EventID)
       {
            
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
            document.getElementById("frameeditexpanse").src="Ajax Pages/Bill_Sys_PopUpBillTransactionAllVisit.aspx?F=L&DID=" + DoctorID+ "&EID=" + EventID + '&P_CASE_ID='+p_szCaseID + '&P_PATIENT_ID='+p_szPatientID+'&P_CASE_NO='+p_szCaseNO+'&PROC_GROUP_ID='+document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality').value;;
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
                                                   
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b>30 Days Un-billed Visit Report </b>
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                   <asp:Label CssClass="message-text" id="lblMsg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    From Date&nbsp; &nbsp;</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Doctor Name
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                    
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Specialty
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                   
                                                         <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                            Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="65%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                
                                                Case Type:
                                                </td>
                                                
                                                
                                                <td style="width: 35%; height: 18px;">
                                                                        
                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                            OldText="" StausText="False" />
                                                                    </td>
                                                <%--<td style="width: 35%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 23px">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" /> &nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" OnItemCommand="grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="CASE ID" Visible="false"></asp:BoundColumn>
                                                <%--1--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false"></asp:BoundColumn>
                                                 <%--2--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false"></asp:BoundColumn>
                                                 <%--3--%>
                                                <asp:BoundColumn DataField="Speciality_ID" HeaderText="Speciality_ID" Visible="false"></asp:BoundColumn>
                                                 <%--4--%>
                                                 <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnlOpenCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>' CommandName="Open Case"> </asp:LinkButton>
                                                    
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Document Manager" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>' CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                               
                                                 <%--5--%>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                  <%--6--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="true">
                                                                            </asp:BoundColumn>
                                                 <%--7--%>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                 <%--8--%>
                                                 <asp:BoundColumn DataField="SZ_VISIT" HeaderText="Visit Type"></asp:BoundColumn>
                                                  <%--9--%>
                                                <asp:BoundColumn  DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                 <%--10--%>
                                                <asp:BoundColumn  DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                                                <%--11--%>
                                                <asp:BoundColumn  DataField="NO_OF_DAYS" HeaderText="No Of Days"></asp:BoundColumn>
                                                 <%--12--%>
                                                <asp:TemplateColumn HeaderText="Diagnosis Code">
                                                     <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAddDiagCode" runat="server" Text="Add" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>'
                                                            CommandName="Add Diagnosis Code" Visible="false"> </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDisplayDiagCode" runat="server" Text="" CommandName="Display Diag Code" Visible="false">Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</asp:LinkButton>
                                                        <a href="#" onclick="javascript:OpenPopUPDisplayDiagCode('<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>')">Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                 <%--13--%>
                                                  <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case No" Visible="false"></asp:BoundColumn>
                                                 <%--14--%>
                                                <asp:TemplateColumn HeaderText="Add Bills">
                                                     <ItemTemplate>
                                                        <a href="#" onclick="javascript:OpenPopUPBillTransaction('<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>','<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>')">Add Bills</a>
                                                        <asp:LinkButton ID="lnkAddGroupService" runat="server" Text="Add" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>'
                                                            CommandName="Group Service" Visible="false"> </asp:LinkButton> 
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                 <%--15--%>
                                                <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="I_EVENT_ID" Visible="false"></asp:BoundColumn>
                                                <%--16--%>
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
                    <a onclick="CloseDisplayDiagnosisCodePopup();" style="cursor: pointer;" title="Close">X</a>
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
                                <asp:BoundColumn DataField="CODE" HeaderText="DIAGNOSIS CODE"  Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Diagnosis Codes"> </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <%--Add Diagosis code--%>
    
     <asp:Panel ID="pnlDiagnosisCode" runat="server" Style="width: 450px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right">
                    <a onclick="CloseDiagnosisCodePopup();" style="cursor: pointer;" title="Close">X</a>
                </td>
            </tr>
            <tr>
                <td style="width: 102%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%" valign="top">
                        <tr runat="server" id="trDoctorType">
                            <td class="ContentLabel" style="width: 15%; height: 18px;">
                                Diagnosis Type:</td>
                            <td style="width: 35%; height: 18px;">
                                <cc1:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px" Connection_Key="Connection_String"
                                    Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---" Flag_Key_Value="DIAGNOSIS_TYPE_LIST">
                                </cc1:ExtendedDropDownList>
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
                                <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                    OnClick="btnSeacrh_Click" />
                                <asp:Button ID="btnOK" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                    OnClick="btnOK_Click" />&nbsp;
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"  cssclass="Buttons" OnClick="btnCancel_Click"/>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 200px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="100%" CssClass="GridTable"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                            OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False">
                                </asp:BoundColumn>
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
    <%--        <a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden';"
                style="cursor: pointer;" title="Close">X</a>--%>
                  <asp:Button ID="btnlClosePopUP" runat="server" OnClick="btnlClosePopUP_Click" Text="X" CssClass="Buttons" />
                
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="500px" width="700px"></iframe>
    </div>
    
    <div id="divDisplayDiagCode" style="position: absolute; width: 400px; height: 300px; background-color: #DBE6FA;visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 400px">
            <a onclick="document.getElementById('divDisplayDiagCode').style.zIndex = '-1'; document.getElementById('divDisplayDiagCode').style.visibility='hidden';" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="ifrmDisplayDiagCode" src="" frameborder="0" height="250px" width="400px"></iframe>
    </div>
</div>
</asp:Content>
