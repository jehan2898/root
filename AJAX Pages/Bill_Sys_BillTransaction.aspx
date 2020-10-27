
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_BillTransaction.aspx.cs" Inherits="Bill_Sys_BillTransaction" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
     <script type="text/javascript">
        var postBackElementID;
        var postBackElementClass;
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            function InitializeRequest(sender, args) {
                debugger;
                postBackElementID = args._postBackElement.id;
                var ele=document.getElementById(postBackElementID);
               
                postBackElementClass = ele.className;
                ele.disabled = true;
                ele.className = ''
            }
            function EndRequest(sender, args) {
                debugger;
                document.getElementById(postBackElementID).disabled = false;
                document.getElementById(postBackElementID).className = postBackElementClass
            }
        });
        
    </script>
    <script type="text/javascript" src="../validation.js"></script>
      <script type="text/javascript">
          function showOTPTinfoPopup(billnumber, caseid) {
              document.getElementById('divid4').style.zIndex = 1;
              document.getElementById('divid4').style.position = 'fixed';
              document.getElementById('divid4').style.left = '100px';
              document.getElementById('divid4').style.top = '30px';
              document.getElementById('divid4').style.visibility = 'visible';
              document.getElementById('frameeditexpanse1').src = '../Bill_Sys_PatientInformationOT-PT.aspx?billnumber=' + billnumber + '&caseid=' + caseid;
              return false;
          }
       </script>
    <script type="text/javascript">
        //         function() {
        //            var CaseValue=document.getElementById("<%= SessionCheck.ClientID%>").value;
        //            if(document.getElementById("<%= SessionCheck.ClientID%>").value!="")
        //            {
        //                if (window.XMLHttpRequest)
        //              {// code for IE7+, Firefox, Chrome, Opera, Safari
        //              xmlhttp=new XMLHttpRequest();
        //              }
        //            else
        //              {// code for IE6, IE5
        //              xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
        //              }
        //            xmlhttp.onreadystatechange=function()
        //              {
        //              if (xmlhttp.readyState==4)
        //                {
        //                    var SessionVar=xmlhttp.responseText;
        //                }
        //              }
        //            xmlhttp.open("GET","Bill_Sys_BillTransaction.aspx?CheckSession="+CaseValue,true);
        //            xmlhttp.send("");    
        //            }  
        //         }
        function checkList() {
            var list = document.getElementById("<%=lstDiagnosisCodes.ClientID %>");
            for (i = 0; i < list.length; i++) {
                if (list.options[i].selected == true) {
                    return true;
                }
            }
            alert('Select Diagnosis codes from list.');
            return false;
        }
      
     
    </script>
    <script type="text/javascript">
        function ShowDignosisPopup() {
            var url = "Dignosis_Code_Popup.aspx";
            DignosisPopup.SetContentUrl(url);
            DignosisPopup.Show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function fncParent(array) {
            var htmlSelect = document.getElementById('<%=lstDiagnosisCodes.ClientID%>');
            var lblDGCount = document.getElementById('<%=lblDiagnosisCodeCount.ClientID%>');
            var SeletedDGCodes = document.getElementById('<%=hndSeletedDGCodes.ClientID%>');
            SeletedDGCodes.value = array;
            var button = document.getElementById('<%=btnOK.ClientID%>');
            DignosisPopup.Hide();
            button.click();
            }
    </script>
     <script type="text/javascript">
         function DeleteDignosisCodes() {
             var button = document.getElementById('<%=btnRemoveDGCodes.ClientID%>');
            button.click();
        }
    </script>

    <script type="text/javascript" src="BillTransaction.js"></script>

    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtClaimInsurance" runat="server" Style="visibility: hidden;" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtNf2" runat="server" Style="visibility: hidden;" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtTransDetailID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtBillID" runat="server" Style="visibility: hidden;" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtCaseIDdummy" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:DropDownList ID="extddlDoctor" runat="server" AutoPostBack="true" Width="97%"
        OnSelectedIndexChanged="extddlDoctor_DropDownList_SelectedIndexChanged" Visible="false">
    </asp:DropDownList>
    <asp:TextBox ID="txtBillDate" runat="server" Width="150px" MaxLength="10" ReadOnly="true"
        onkeypress="return CheckForInteger(event,'/')" Visible="false"></asp:TextBox>
    <table>
        <tr>
            <td>
                <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
                    visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                    border-left: silver 1px solid; border-bottom: silver 1px solid;">
                    <div style="position: relative; text-align: right; background-color: #8babe4;">
                        <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
                    </div>
                    <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
                        visible="false"></iframe>
                </div>
            </td>
            <td>
                <div id="divid2" style="position: absolute; left: 100px; top: 100px; width: 500px;
                    height: 380px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
                    border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                    <div style="position: relative; text-align: right; background-color: #8babe4;">
                        <a onclick="CloseSource();" style="cursor: pointer;" title="Close">X</a>
                    </div>
                    <iframe id="iframeAddDiagnosis" src="" frameborder="0" height="380" width="500"></iframe>
                </div>
            </td>
        </tr>
    </table>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelUserMessage" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: middle;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                                        padding-top: 3px; height: 100%; vertical-align: top;" class="TDPart">
                                        <asp:GridView ID="grdPatientDeskList" runat="server" Width="100%" CssClass="mGrid"
                                            HeaderStyle-CssClass="GridViewHeader" GridLines="None" AlternatingRowStyle-BackColor="#EEEEEE"
                                            PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%"></asp:BoundField>
                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%"></asp:BoundField>
                                                <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%"></asp:BoundField>
                                                <asp:BoundField DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="25%">
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="3" class="ContentTable" style="width: 100%">
                                            <%--  <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 20px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" runat="server" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 50%" colspan="2">
                                                </td>
                                                <td style="width: 50%" colspan="2" align="right">
                                                    <asp:CheckBox ID="chkNf2" Text="NF2" runat="server" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnNf2" Text="Update" runat="server" OnClick="btnNf2_Click" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case #:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtCaseID" runat="server" BorderStyle="None" Style="border-top-style: none;
                                                        border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                        BorderColor="Transparent" ReadOnly="True" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseNo" runat="server" BorderStyle="None" Style="border-top-style: none;
                                                        border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                        BorderColor="Transparent" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Date:</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtBillDatedummy" Style="text-align: left;" runat="server" Width="150px"
                                                        MaxLength="10" ReadOnly="true" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="10">
                                                        <progresstemplate>
                                                            <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...</div>
                                                        </progresstemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="extddlSpeciality" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="width: 15%">
                                                    <asp:UpdatePanel ID="upVisitgrid" runat="server">
                                                        <ContentTemplate>
                                                            <div style="display: none">
                                                                <asp:Button ID="btnLoadProcedures" Text="Get Codes" runat="server" CssClass="Buttons"
                                                                    Visible="true" OnClick="btnLoadProcedures_Click" />
                                                                <asp:Button ID="btnclearDiaProc" Text="Clear" runat="server" CssClass="Buttons" Visible="true"
                                                                    OnClick="btnclearDiaProc_Click" />
                                                            </div>
                                                            <asp:Panel ID="dvcompletevisit" ScrollBars="Vertical" Style="height: 200px;" runat="server"
                                                                Visible="false">
                                                                <asp:HiddenField ID="hdnDocName" runat="server" />
                                                                <asp:HiddenField ID="hndPopFirstPostback" runat="server" />
                                                                <asp:HiddenField ID="hndJavaScriptDocID" runat="server" />
                                                                <asp:HiddenField ID="hdnVisitType" runat="server" />
                                                                <asp:DataGrid ID="grdCompleteVisit" runat="server" Width="96%" CssClass="mGrid" AutoGenerateColumns="false"
                                                                    OnItemDataBound="Item_Bound" OnItemCommand="grdCompleteVisit_ItemCommand">
                                                                    <ItemStyle CssClass="GridRow" Font-Bold="true" />
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                    <Columns>
                                                                        <%-- 0--%>
                                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="btnQuickBill" OnClick="btnQuickBill_Click" OnClientClick="ConfirmQuickBill()"
                                                                                    runat="server" Text="Quick Bill">
                                                                <img src="Images/Quick.jpg" style="border:none;"  height="20px" width ="20px" title = "Quick Bill"/></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectItem" runat="server" OnClick="chk_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%-- 1--%>
                                                                        <asp:BoundColumn DataField="VisitDate" HeaderText="Visit Date" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                        <%-- 2--%>
                                                                        <asp:BoundColumn DataField="DoctorName" HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 3--%>
                                                                        <asp:BoundColumn DataField="VisitType" HeaderText="Visit Type" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                        <%-- 4--%>
                                                                        <asp:BoundColumn DataField="CaseID" HeaderText="CaseID" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 5--%>
                                                                        <asp:BoundColumn DataField="PatientID" HeaderText="PatientID" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 6--%>
                                                                        <asp:BoundColumn DataField="SpecialityID" HeaderText="SpecialityID" Visible="false"
                                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 7--%>
                                                                        <asp:BoundColumn DataField="I_EventID" HeaderText="EventID" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 8--%>
                                                                        <asp:BoundColumn DataField="DoctorID" HeaderText="Doctor ID" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 9--%>
                                                                        <asp:BoundColumn DataField="IsRefferal" HeaderText="" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                        <%-- 10--%>
                                                                        <asp:TemplateColumn HeaderStyle-Width="0px" HeaderText="" HeaderStyle-CssClass="HeaderStyle">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="visibility: hidden;" ID="lnkSelectDoctor" runat="server" Text=""
                                                                                    Width="0px" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EventID")%>'
                                                                                    CommandName="Edit"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <%-- 11--%>
                                                                        <asp:TemplateColumn HeaderText="Include Limit" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkLimit" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%-- 12--%>
                                                                        <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added By Doctor" Visible="false">
                                                                        </asp:BoundColumn>
                                                                        <%-- 13--%>
                                                                        <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="Is Finalised" Visible="false"></asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="Added By" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                                                        <%-- 14--%>
                                                                        <asp:TemplateColumn HeaderText="Added By">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddedByDoctor" runat="server" Font-Size="12px" Font-Names="Arial"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%-- 15--%>
                                                                        <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name" Visible="false"></asp:BoundColumn>
                                                                        <%-- 16--%>
                                                                        <asp:TemplateColumn HeaderText="Procedures" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="LinkBtnCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Count")%>'
                                                                                    CommandName="Count"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    <div style="font-family: Arial; font-size: 14px; font-style: italic; color: Red">
                                                        <asp:Label runat="server" ID="lblLocationNote" Text="" />
                                                    </div>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    &nbsp;</td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlType" runat="server" Visible="false" Width="94%">
                                                        <asp:ListItem Selected="True" Value="0"> --Select--</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000001">Visit</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000002">Treatment</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000003">Test</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 20%; vertical-align: top;">
                                                    Diagnosis Code:
                                                    <br />
                                                    Total Count :
                                                    <asp:Label ID="lblDiagnosisCodeCount" runat="server"></asp:Label>
                                                </td>
                                                <td colspan="2" style="width: 70%; vertical-align: top;">
                                                    <asp:UpdatePanel ID="updatePanelDiagnosisCode" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%">
                                                                <tr style="width: 100%">
                                                                    <td width="70%">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ListBox ID="lstDiagnosisCodes" runat="server" Width="100%" SelectionMode="Multiple">
                                                                                    </asp:ListBox></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top" width="30%">
                                                                        <table width="100%">
                                                                            <tr style="width: 100%">
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkAddDiagnosis" runat="server" Text="Add Diagnosis" Style="text-align: right;
                                                                                        font-size: 12px; vertical-align: top;" OnClientClick="ShowDignosisPopup()"></asp:LinkButton>
                                                                                    <%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlDiagnosisCode"
                                                                                        TargetControlID="lnkAddDiagnosis" Drag="false" BackgroundCssClass="modalBackground"
                                                                                        CancelControlID="btnDiagnosisCodeClose">
                                                                                    </ajaxToolkit:ModalPopupExtender>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="width: 100%">
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px;
                                                                                        vertical-align: top;" OnClientClick="return checkList();" OnClick="lnkbtnRemoveDiag_Click"></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="width: 15%">
                                                    <asp:DataGrid ID="grdAllReports" runat="server" Width="99%" CssClass="GridTable"
                                                        AutoGenerateColumns="false" Visible="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="EventID" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="Visit Type"></asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
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
                                    <td style="width: 100%;" class="TDPart">
                                        <asp:UpdatePanel ID="UpdatePanelpnlShowNotes" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hndDoctorID" runat="server" />
                                                <asp:HiddenField ID="hndPopUpvalue" runat="server" />
                                                <asp:HiddenField ID="hdnDoctorId" runat="server" />
                                                <asp:HiddenField ID="hdnDateOfService" runat="server" />
                                                <asp:HiddenField ID="hndFinalised" runat="server" />
                                                <asp:HiddenField ID="hndIsAddedByDoctor" runat="server" />
                                                <%--<asp:HiddenField ID="hndSpecialityID" runat="server" />--%>
                                                <div align="right" style="vertical-align: top;">
                                                    <asp:LinkButton ID="Button1" Style="visibility: hidden;" runat="server" OnClick="Button1_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkAddGroupService" Style="visibility: hidden;" runat="server"
                                                        OnClick="lnkAddGroupService_Click"></asp:LinkButton>
                                                    <div style="display: none">
                                                        <asp:Button ID="dummybtnAddServices" Text="Add Services" runat="server" Visible="true" />
                                                        <asp:Button ID="dummybtnAddGroup" Text="Add Group" runat="server" Visible="true" />
                                                    </div>
                                                    <asp:Button ID="btnAddGroup" OnClick="btnAddGroup_Click" runat="server" Text="Add Group Services"
                                                        CssClass="Buttons" />
                                                    <ajaxToolkit:ModalPopupExtender ID="modalpopupaddgroup" runat="server" PopupControlID="pnlGroupService"
                                                        TargetControlID="dummybtnAddGroup" BehaviorID="BehModalpopupAddGroup" BackgroundCssClass="modalBackground">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <asp:Button ID="btnAddServices" runat="server" Text="Add Services" CssClass="Buttons"
                                                        OnClick="btnAddServices_Click" />
                                                    <ajaxToolkit:ModalPopupExtender ID="modalpopupAddservice" runat="server" PopupControlID="pnlShowNotes"
                                                        TargetControlID="dummybtnAddServices" BehaviorID="BehModalpopupAddservice" BackgroundCssClass="modalBackground">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Services" Width="110px" CssClass="Buttons"
                                                        OnClick="btnRemove_Click" Style="vertical-align: top; height: 21px;" />
                                                </div>
                                                <asp:Panel ID="pnlShowNotes" runat="server" BackColor="white" Style="display: none;
                                                    height: 400px; width: 50%;">
                                                    <asp:Label ID="lblDateOfService" runat="server" Text="Date Of Service" Font-Bold="False"></asp:Label>
                                                    <asp:TextBox ID="txtDateOfservice" runat="server" Width="240px" ReadOnly="false"
                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <a id="A1" href="#">
                                                        <input type="image" name="mgbtnDateofService" id="Image1" runat="server" src="Images/cal.gif"
                                                            border="0" /></a>

                                                   <table>
                                                        <tr>
                                                            <td class="ContentLabel" style="width: 5%; height: 18px;">
                                                                Code :
                                                            </td>
                                                            <td style="width: 25%; height: 18px;">
                                                                <asp:TextBox ID="txtProcCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td class="ContentLabel" style="width: 5%; height: 18px;">
                                                                Description :
                                                            </td>
                                                            <td style="width: 15%; height: 18px;">
                                                                <asp:TextBox ID="txtProcDesc" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 20%; height: 18px;">
                                                                <asp:Button ID="btnProcSearch" runat="server" Text="Search" CssClass="Buttons" onclick="btnProcSearch_Click" runat="server" />
                                                                <input type="button" value="Add" class="Buttons" onclick="onOk()" id="btnService" />
                                                                <asp:Button ID="btnclose" runat="server" Text="X" OnClientClick="$find('BehModalpopupAddservice').hide(); return false;"
                                                                    CssClass="Buttons" runat="server" />
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                    
                                                    <div style="overflow: scroll; height: 100%; width: 99%; background-color: Gray;">
                                                        <asp:DataGrid ID="grdProcedure" runat="server" AllowPaging="false" Width="99%" CssClass="GridTable"
                                                            AutoGenerateColumns="false">
                                                            <ItemStyle CssClass="GridRow" />
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkselect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False">
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID ID" Visible="False">
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" Visible="false"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                     <ItemTemplate>
                                                                        <asp:TextBox ID="txtModifier" runat="server" Width="105px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:DataGrid>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlGroupService" runat="server" BackColor="white" Style="display: none;">
                                                    <asp:Label ID="lblGroupServiceDate" runat="server" Text="Date Of Service" Font-Bold="False"></asp:Label>
                                                    <asp:TextBox ID="txtGroupDateofService" runat="server" Width="240px" ReadOnly="false"
                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <a id="trigger" href="#">
                                                        <input type="image" name="mgbtnDateofService" id="imgbtnDateofService" runat="server"
                                                            src="Images/cal.gif" border="0" /></a>
                                                    <input type="button" value="Add" class="Buttons" onclick="onGroupService()" id="btnGroupServices" />
                                                    <asp:Button ID="btnCloseGroup" runat="server" Text="X" OnClientClick="$find('BehModalpopupAddGroup').hide(); return false;"
                                                        CssClass="Buttons" />
                                                    <div style="overflow: scroll; height: 100%; width: 100%; background-color: Gray;">
                                                        <asp:DataGrid ID="grdGroupProcCodeService" runat="server" AllowPaging="false" Width="99%"
                                                            CssClass="GridTable" AutoGenerateColumns="false">
                                                            <ItemStyle CssClass="GridRow" />
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkselect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="GROUP_PROCEDURE_ID"
                                                                    Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_GROUP_NAME" HeaderText="Group Name"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="I_GROUP_AMOUNT_ID" HeaderText="Amount Id" Visible="false">
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" Visible="false"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtModifierGroup" runat="server" Width="105px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:DataGrid>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:Panel ID="pnlProcedureCodesPanel" ScrollBars="Vertical" Style="height: 200px;"
                                            runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <contenttemplate>
                                                    <asp:DataGrid ID="grdTransactionDetails" runat="server" OnSelectedIndexChanged="grdTransactionDetails_SelectedIndexChanged"
                                                        AutoGenerateColumns="False" Width="99%" CssClass="GridTable">
                                                        <PagerStyle Mode="NumericPages" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <Columns>
                                                        
                                                            <%--0 --%>
                                                            <asp:BoundColumn DataField="SZ_BILL_TXN_DETAIL_ID" HeaderText="Transaction Detail ID"
                                                                Visible="False"></asp:BoundColumn>
                                                            <%--1 --%>
                                                            <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Services" DataFormatString="{0:MM/dd/yyyy}"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:BoundColumn>
                                                            <%--2 --%>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedural Code ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <%--3 --%>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Procedure Code">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundColumn>
                                                            <%--4 --%>
                                                            <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Code Description">
                                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                            </asp:BoundColumn>
                                                            <%--5 --%>
                                                            <%-- <asp:TemplateColumn HeaderText="Price * Factor" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrice" runat="server" Style="text-align: right;" Font-Size="12px"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "FACTOR_AMOUNT") %>'> </asp:Label>
                                                                    &nbsp;*&nbsp;
                                                                    <asp:Label ID="lblFactor" runat="server" Style="text-align: right;" Font-Size="12px"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "FACTOR") %>'> </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>--%>
                                                              <%--5 --%>
                                                            <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount ($)" DataFormatString="{0:0.00}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundColumn>
                                                              <%--6 --%>
                                                             <asp:TemplateColumn HeaderText="Amount ($)" Visible="False">
                                                             
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmt" runat="server" Style="text-align: right;" Width="58px"
                                                                        Text='<%# Eval("FLT_AMOUNT") %>'>  </asp:TextBox>
                                                                        
                                                                </ItemTemplate>
                                                                 
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>

                                                            <%--7 --%>
                                                            <asp:TemplateColumn HeaderText="Unit" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtUnit" runat="server" Style="text-align: right;" Width="58px"
                                                                        Text="1"> </asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                            <%--8 --%>
                                                            <asp:BoundColumn DataField="I_UNIT" HeaderText="Unit" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundColumn>
                                                            <%--9 --%>
                                                            <%--   <asp:BoundColumn DataField="PROC_AMOUNT" HeaderText="Proc Amount" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                            </asp:BoundColumn>--%>
                                                            <%--10 --%>
                                                            <%-- <asp:BoundColumn DataField="DOCT_AMOUNT" HeaderText="Doct Amount" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                            </asp:BoundColumn>--%>
                                                            <%--9 --%>
                                                            <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="Type Code" Visible="False">
                                                                <HeaderStyle 
                                                                 HorizontalAlign="Right" />
                                                            </asp:BoundColumn>
                                                            <%--10 --%>
                                                          <%--  <asp:BoundColumn DataField="FLT_GROUP_AMOUNT" HeaderText="Total Charge Per Day ($)"DataFormatString="{0:0.00}">
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundColumn>--%>
                                                              <asp:BoundColumn DataField="FLT_GROUP_AMOUNT"    HeaderText="Total Charge Per Day ($)" >
                                                                <ItemStyle  HorizontalAlign="Right"   />
                                                            </asp:BoundColumn>
                                                            
                                                            <%--11 --%>
                                                            <asp:BoundColumn DataField="I_GROUP_AMOUNT_ID" HeaderText="Group Amount ID" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                            </asp:BoundColumn>
                                                            <%--12 --%>
                                                            <asp:BoundColumn DataField="SZ_MODIFIER_CODE" HeaderText="Modifier"></asp:BoundColumn>
                                                            <%--13 --%>
                                                            <asp:TemplateColumn HeaderText="Remove">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%--14 --%>
                                                            <asp:BoundColumn DataField="I_EventID" HeaderText="EventID" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <%--15 --%>
                                                                <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="SZ_VISIT_TYPE" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <%--16 --%>
                                                                <asp:BoundColumn DataField="BT_IS_LIMITE" HeaderText="BT_IS_LIMITE" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                
                                                                <%--17 --%>
                                                                  <asp:BoundColumn DataField="SZ_MODIFIER_CODE" HeaderText="SZ_MODIFIER_CODE" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                               
                                                               
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </contenttemplate>
                                                <triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button1" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="btnLoadProcedures" />--%>
                                                </triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" colspan="4" align="right">
                                        <asp:TextBox ID="txtAmount" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                            OnClick="btnSaveWithTransaction_Click" />
                                        <asp:Button ID="btnSaveTransaction" runat="server" Text="Save Transaction" Width="80px"
                                            CssClass="Buttons" Visible="false" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                            OnClick="btnUpdate_Click1" />
                                        <asp:Button ID="btnClearService" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                            OnClick="btnClearService_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdLatestBillTransaction" runat="server" OnSelectedIndexChanged="grdLatestBillTransaction_SelectedIndexChanged"
                                            OnItemCommand="grdLatestBillTransaction_ItemCommand" OnItemDataBound="grdLatestBillTransaction_ItemDataBound"
                                            AutoGenerateColumns="False" Width="99%">
                                            <ItemStyle CssClass="GridRow" />
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <%--0 --%>
                                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                                    <ItemStyle CssClass="grid-item-left" />
                                                </asp:ButtonColumn>
                                                <%--1 --%>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <%--2 --%>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <%-- 3 --%>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Specialty"></asp:BoundColumn>
                                                <%-- 4 --%>
                                                <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%-- 5 --%>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:MM/dd/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%-- 6 --%>
                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status"></asp:BoundColumn>
                                                <%-- 7 --%>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%-- 8 --%>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%-- 9 --%>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%-- 10 --%>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- 11 --%>
                                                <asp:BoundColumn DataField="BIT_WRITE_OFF_FLAG" HeaderText="WRITEOFFFLAG" Visible="False">
                                                </asp:BoundColumn>
                                                <%-- 12 --%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor" Visible="False"></asp:BoundColumn>
                                                <%-- 13 --%>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkInitialReport" runat="server" Text="Edit W.C. 4.0 " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Initial Report"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%-- 14 --%>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkProgressReport" runat="server" Text="Edit W.C. 4.2  " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Progress Report"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%-- 15 --%>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkMIReport" runat="server" Text="Edit W.C. 4.3 " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Doctor's Report Of MMI"> </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                 <%-- 16 --%>     <%--Add new column for OTPT report By kapil--%>
                                                <asp:TemplateColumn >
                                                    <ItemTemplate>                                                       
                                                        <a id="lnk_otpt" href="#" onclick='<%# "showOTPTinfoPopup(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\");" %>'>
                                                            Edit W.C. OT PT </a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%-- 17 --%> <%--change 16 to 17 By Kapil--%>
                                                <asp:TemplateColumn HeaderText="Generate bill">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill" OnClientClick='<%# "return checkExistingBills(" +Eval("SZ_BILL_COUNT") + ",\"" + Eval("SZ_CASE_TYPE") +"\",\""+ Eval("SZ_BILL_NUMBER") +"\",\""+ Eval("SPECIALITY") +"\");" %>'> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%-- 18 --%>     <%--change 17 to 18 By Kapil--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="False"></asp:BoundColumn>
                                                <%--19  --%>     <%--change 18 to 19 By Kapil--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible="False"></asp:BoundColumn>
                                                <%-- 20 --%>     <%--change 19 to 20 By Kapil--%>
                                                <asp:BoundColumn DataField="PG_ID" HeaderText="SpecilaityID" Visible="False"></asp:BoundColumn>
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
    <asp:Panel ID="pnlPDFWorkerComp" runat="server" Style="width: 250px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;" Height="0px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;"
            class="TDPart">
            <tr>
                <td align="right" valign="top">
                    <a onclick="ClosePDFWorkerComp();" style="cursor: pointer;" title="Close">X</a>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;" class="ContentLabel">
                    <asp:RadioButtonList ID="rdbListPDFType" runat="server">
                        <asp:ListItem Value="1" Text="Doctor's Initial Report" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Doctor's Progress Report"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Doctor's Report Of MMI"></asp:ListItem>
                        <asp:ListItem Value="4" Text="OTPT Report"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Psychologist Report"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:Button ID="btnGenerateWCPDF" runat="server" Text="Generate PDF" OnClick="btnGenerateWCPDF_Click"
                        CssClass="Buttons" />
                    <asp:HiddenField ID="hdnWCPDFBillNumber" runat="server" />
                    <asp:HiddenField ID="hdnSpeciality" runat="server" />
                    <asp:HiddenField ID="hdnQuick" runat="server" Value="false" />
                    <asp:hiddenfield id="hdnValue" runat="server"  />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPDFWorkerCompAdd" runat="server" Style="width: 250px; height: 0px;
        left: 450px; top: 400px; background-color: white; border-color: ThreeDFace; border-width: 1px;
        border-style: solid; position: absolute;" Height="0px" Visible="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;"
            class="TDPart">
            <tr>
                <td align="right" valign="top">
                    <a onclick="ClosePDFWorkerComp();" style="cursor: pointer;" title="Close">X</a>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;" class="ContentLabel">
                    <asp:RadioButtonList ID="rdbListPDFType1" runat="server">
                        <asp:ListItem Value="1" Text="Doctor's Initial Report" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Doctor's Progress Report"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Doctor's Report Of MMI"></asp:ListItem>
                        <asp:ListItem Value="4" Text="OTPT Report"></asp:ListItem>
                         <asp:ListItem Value="5" Text="Psychologist Report"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:Button ID="btnGenerateWCPDFAdd" runat="server" Text="Generate PDF" OnClick="btnGenerateWCPDFAdd_Click"
                        CssClass="Buttons" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" Value="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDiagnosisCode" runat="server" BackColor="white" Style="display: none;" DefaultButton="btnSeacrh">
        <asp:UpdatePanel ID="updatepanel123" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; height: 100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnDiagnosisCodeClose" CssClass="Buttons" runat="server" Text="X"
                                OnClientClick="CloseDiagnosisCodePopup();" />
                            <%--      <a onclick="CloseDiagnosisCodePopup();" style="cursor: pointer;" title="Close">X</a>--%>
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
                        <td style="width: 100%" valign="top">
                            <div style="height: 200px; background-color: Gray; overflow-y: scroll;">
                                <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="99%" CssClass="GridTable"
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div id="divid4" style="position: absolute; width: 90%; height: 90%; background-color: White;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; background-color: #B5DF82; width: 100%; text-align: center">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="txtUpdate4" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                            OnClick="txtUpdatepopup_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <iframe id="frameeditexpanse1" src="" frameborder="0" height="96%" width="100%"></iframe>
    </div>
    <div style="visibility: hidden;">
        <asp:Button ID="btnRemoveDGCodes" Text="X" BackColor="#B5DF82" BorderStyle="None"
            runat="server" OnClick="btnRemoveDGCodes_Click" />
    </div>
    <dx:ASPxPopupControl ID="DGCODEPOPUP" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DignosisPopup"
        HeaderText="Diagnosis Codes" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
        HeaderStyle-BackColor="#000000" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="900px" PopupHorizontalOffset="0" PopupVerticalOffset="0"
          AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="540px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <div style="visibility: hidden;">
        <asp:Button ID="Button2" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="btnOK_Click" />
    </div>
    <input type="hidden" runat="server" id="hndSeletedDGCodes" />
    <input type="hidden" runat="server" id="SessionCheck" />
   <asp:HiddenField ID="txtNewNF2" runat="server" />
</asp:Content>
