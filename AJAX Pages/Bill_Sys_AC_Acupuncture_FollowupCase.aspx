<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AC_Acupuncture_FollowupCase.aspx.cs" Inherits="Bill_Sys_AC_Acupuncture_FollowupCase"
    Title="Acupuncture Followup" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script language="Javascript" type="text/javascript">
    
            function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '380px'; 
            document.getElementById('divid2').style.top= '350px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
        }
       function HideDiv()
        {
            document.getElementById('divid2').style.visibility='hidden';
        }
    
        var maxL=4000;
        var bName = navigator.appName;
      
        function taLimit(taObj) {
            if (taObj.value.length==maxL) 
          
            return false;
            return true;
        }

        function taCount(taObj,Cnt) {
	        objCnt=createObject(Cnt);
	        objVal=taObj.value;
	        if (objVal.length>maxL) objVal=objVal.substring(0,maxL);
	        if (objCnt) {
		        if(bName == "Netscape"){
			        objCnt.textContent=maxL-objVal.length;}
		        else{objCnt.innerText=maxL-objVal.length;}
	        }
	        return true;
        }
        
        function createObject(objId) {
	        if (document.getElementById) return document.getElementById(objId);
	        else if (document.layers) return eval("document." + objId);
	        else if (document.all) return eval("document.all." + objId);
	        else return eval("document." + objId);
        }
        
    </script>

    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="ACUPUNCTURE FOLLOW-UP" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%" border="0">
                                    <tr>
                                        <td align="right" valign="middle" visible="false">
                                            <asp:Label ID="lblPatientFirstName" runat="server" CssClass="ContentLabel" Font-Bold="False"
                                                Text="PATIENT'S NAME :"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_FIRST_NAME" runat="server" Width="172px" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lbl_doa" runat="server" CssClass="ContentLabel" Font-Bold="False"
                                                Text="DOA" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lblPatientLastName" runat="server" CssClass="ContentLabel" Font-Bold="False"
                                                Text="LAST NAME" Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_LAST_NAME" runat="server" Width="172px" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lblSNO" runat="server" CssClass="ContentLabel" Font-Bold="true" Text="S.NO"></asp:Label>
                                        </td>
                                        <td align="center" valign="middle" style="width: 100px">
                                            <asp:Label ID="LblDate" runat="server" CssClass="ContentLabel" Font-Bold="true" Text="DATE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:Label ID="lblOne" runat="server" CssClass="ContentLabel" Font-Bold="True" Text="1"></asp:Label></td>
                                        <td align="center" valign="top" style="width: 100px;">
                                            <asp:TextBox ID="TXT_CURRENT_DATE" runat="server" Width="85px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="50%" border="0">
                                    <tr>
                                        <td align="center">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <table width="50%" border="0">
                                                        <tr>
                                                            <td align="center" style="width: 50%; float: left;" valign="middle">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 40%;
                                                                    height: 50%; border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                                                                style="width: 100%; float: left;">
                                                                                <tr>
                                                                                    <td align="left" bgcolor="#b5df82" class="txt2" colspan="2" height="28" valign="middle">
                                                                                        <b class="txt3">Accupuncture Points</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 50%; height: 19px">
                                                                                        Accupunture Points</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" style="width: 50%; height: 24px">
                                                                                        <asp:DropDownList ID="drpAccupuncturePoints" runat="server" OnSelectedIndexChanged="extddlacpoints_extendDropDown_SelectedIndexChanged"
                                                                                            AutoPostBack="true">
                                                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="LUNG HAND TAIYIN.gif">LUNG HAND TAIYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="LARGE INTESTINE HAND YANGMING.gif">LARGE INTESTINE HAND YANGMING</asp:ListItem>
                                                                                            <asp:ListItem Value="STOMACH FOOT YANGMING.gif">STOMACH FOOT YANGMING</asp:ListItem>
                                                                                            <asp:ListItem Value="SPLEEN FOOT TAIYIN.gif">SPLEEN FOOT TAIYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="HEART HAND SHAOYIN.gif">HEART HAND SHAOYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="SMALL INTESTINE HAND TAIYANG.gif">SMALL INTESTINE HAND TAIYANG</asp:ListItem>
                                                                                            <asp:ListItem Value="URINARY BLADDER FOOT TAIYANG.gif">URINARY BLADDER FOOT TAIYANG</asp:ListItem>
                                                                                            <asp:ListItem Value="KIDNEY FOOT SHAOYIN.gif">KIDNEY FOOT SHAOYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="PERICARDIUM HAND JUEYIN.gif">PERICARDIUM HAND JUEYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="SAN JIAO HAND SHAOYANG.gif">SAN JIAO HAND SHAOYANG</asp:ListItem>
                                                                                            <asp:ListItem Value="GALLBLADDER FOOT SHAOYANG.gif">GALLBLADDER FOOT SHAOYANG</asp:ListItem>
                                                                                            <asp:ListItem Value="LIVER FOOT JUEYIN.gif">LIVER FOOT JUEYIN</asp:ListItem>
                                                                                            <asp:ListItem Value="REN CONCEPTION.gif">REN CONCEPTION(CV=REN)</asp:ListItem>
                                                                                            <asp:ListItem Value="DU GOVERNING.gif">DU GOVERNING</asp:ListItem>
                                                                                            <asp:ListItem Value="EAR ACUPUNTURE CHART.gif">EAR ACUPUNTURE CHART</asp:ListItem>
                                                                                        </asp:DropDownList>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="left" valign="middle" style="width: 50%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle" style="width: 1035px">
                                                                <asp:CheckBox ID="CHK_TREATMENT_CODE_97810" CssClass="ContentLabel" runat="server"
                                                                    Text="97810 - Follow Up" Visible="false" />
                                                                <table style="vertical-align: middle" border="0" width="800px">
                                                                    <tbody>
                                                                        <tr>
                                                                            <asp:Panel runat="server" ID="pnlSrch" Visible="false">
                                                                                <td style="text-align: left" valign="top">
                                                                                    Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                                                    Record Count:
                                                                                    <%= this.grdProcedure.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel" runat="server" Visible="false" Text="Export TO Excel"> <%-- OnClick="lnkExportTOExcel_onclick"--%>
                                                                        <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel" visible="false"/></asp:LinkButton>
                                                                                </td>
                                                                            </asp:Panel>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                Procedures
                                                                                <xgrid:XGridViewControl ID="grdProcedure" runat="server" CssClass="mGrid" Width="350px"
                                                                                    AutoGenerateColumns="false" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="10"
                                                                                    XGridKey="DoctorId" AllowPaging="true" ExportToExcelColumnNames="" ShowExcelTableBorder="true"
                                                                                    ExportToExcelFields="" AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-CssClass="GridViewHeader"
                                                                                    ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                                                    DataKeyNames="">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                                                            <itemtemplate>
                                                                <asp:CheckBox ID="ChkProcId" runat="server"/>
                                                            </itemtemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            ItemStyle-Width="100%" SortExpression="" headertext="Code" DataField="code" />
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            ItemStyle-Width="40px" visible="true" SortExpression="" headertext="TypeCode"
                                                                                            DataField="SZ_TYPE_CODE_ID" />
                                                                                    </Columns>
                                                                                </xgrid:XGridViewControl>
                                                                            </td>
                                                                            <td valign="top" style="width: 200px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td valign="top">
                                                                                Doctor's Notes
                                                                                <asp:TextBox ID="txtDocNote" runat="server" TextMode="MultiLine" Width="300px" Height="170px"
                                                                                    onFocus="return taCount(this,'myCounter')" onKeyPress="return taLimit(this)"
                                                                                    onKeyUp="return taCount(this,'myCounter')"></asp:TextBox>
                                                                                You have <b><span id="myCounter">4000</span></b> characters remaining for your comments.
                                                                            </td>
                                                                            <td valign="top">
                                                                                <div style="overflow: scroll; height: 300px">
                                                                                    Complaints
                                                                                    <xgrid:XGridViewControl ID="gv_Complaints" runat="server" CssClass="mGrid" Width="350px"
                                                                                        AutoGenerateColumns="false" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="500"
                                                                                        XGridKey="Get_Complaint_Info" AllowPaging="true" ExportToExcelColumnNames=""
                                                                                        ShowExcelTableBorder="true" ExportToExcelFields="" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                                        HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                                        MouseOverColor="0, 153, 153" DataKeyNames="Complaint_Id">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                                <itemtemplate>
                                                                <asp:CheckBox ID="ChkComplaintId" runat="server"/>
                                                            </itemtemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                ItemStyle-Width="100%" SortExpression="" headertext="Complaint" DataField="sz_complaint" />
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                ItemStyle-Width="100%" SortExpression="" visible="false" headertext="Complaint Id"
                                                                                                DataField="Complaint_Id" />
                                                                                        </Columns>
                                                                                    </xgrid:XGridViewControl>
                                                                                </div>
                                                                                <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="Buttons" Visible="false"
                                                                                    OnClick="btnSave_OnClick" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                <asp:CheckBox ID="CHK_TREATMENT_CODE_97813" CssClass="ContentLabel" runat="server"
                                                                    Text="97813 - Follow Up" Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle" style="width: 1035px" visible="false">
                                                                <asp:CheckBox ID="CHK_TREATMENT_CODE_97811" CssClass="ContentLabel" runat="server"
                                                                    Text="97811 - Follow Up" Visible="false" />
                                                            </td>
                                                            <td align="left" valign="middle" visible="false">
                                                                <asp:CheckBox ID="CHK_TREATMENT_CODE_97814" CssClass="ContentLabel" runat="server"
                                                                    Text="97814 - Follow Up" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:TextBox ID="txtCompanyID" runat="server" Style="display: none;"></asp:TextBox>
                                            <asp:Button ID="css_btnSave" runat="server" Text="Create Visit/Save Session" OnClick="css_btnSave_Click"
                                                CssClass="Buttons" />&nbsp;
                                            <asp:Button ID="css_btnSign" runat="server" Text="PATIENT SIGNATURE" OnClick="css_btnSign_Click"
                                                CssClass="Buttons" />&nbsp;<asp:Button ID="btn_Doctor" runat="server" OnClick="btn_Doctor_Click"
                                                    Text="DOCTOR SIGNATURE" CssClass="Buttons" />
                                            <asp:Button ID="css_btnFinalize" runat="server" Text="FINALIZE TREATMENT" OnClick="css_btnFinalize_Click"
                                                CssClass="Buttons" />&nbsp;
                                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtDoctorId" runat="Server" Style="display: none;" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtDoctorNotes" runat="Server" Style="display: none;" Width="10px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <gridpagination:XGridPaginationDropDown ID="con1" runat="server" Visible="false">
                                            </gridpagination:XGridPaginationDropDown>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det"
                DropShadow="false" PopupControlID="pnlSaveDescription" BehaviorID="modal" PopupDragHandleControlID="pnlSaveDescriptionHeader">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none; width: 80%; height: 100%; background-color: #dbe6fa;"
                ScrollBars="Both" ID="pnlSaveDescription" runat="server">
                <table width="80%" border="0">
                    <tr>
                        <td>
                            <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #8babe4;">
                                <asp:Button ID="btnClose" runat="server" Height="19px" Width="50px" CssClass="Buttons"
                                    Text="X" OnClientClick="$find('modal').hide(); return false;"></asp:Button>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="drpAccupuncturePointsNew" runat="server" OnSelectedIndexChanged="extddlacpoints_extendDropDownNew_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="LUNG HAND TAIYIN.gif">LUNG HAND TAIYIN</asp:ListItem>
                                <asp:ListItem Value="LARGE INTESTINE HAND YANGMING.gif">LARGE INTESTINE HAND YANGMING</asp:ListItem>
                                <asp:ListItem Value="STOMACH FOOT YANGMING.gif">STOMACH FOOT YANGMING</asp:ListItem>
                                <asp:ListItem Value="SPLEEN FOOT TAIYIN.gif">SPLEEN FOOT TAIYIN</asp:ListItem>
                                <asp:ListItem Value="HEART HAND SHAOYIN.gif">HEART HAND SHAOYIN</asp:ListItem>
                                <asp:ListItem Value="SMALL INTESTINE HAND TAIYANG.gif">SMALL INTESTINE HAND TAIYANG</asp:ListItem>
                                <asp:ListItem Value="URINARY BLADDER FOOT TAIYANG.gif">URINARY BLADDER FOOT TAIYANG</asp:ListItem>
                                <asp:ListItem Value="KIDNEY FOOT SHAOYIN.gif">KIDNEY FOOT SHAOYIN</asp:ListItem>
                                <asp:ListItem Value="PERICARDIUM HAND JUEYIN.gif">PERICARDIUM HAND JUEYIN</asp:ListItem>
                                <asp:ListItem Value="SAN JIAO HAND SHAOYANG.gif">SAN JIAO HAND SHAOYANG</asp:ListItem>
                                <asp:ListItem Value="GALLBLADDER FOOT SHAOYANG.gif">GALLBLADDER FOOT SHAOYANG</asp:ListItem>
                                <asp:ListItem Value="LIVER FOOT JUEYIN.gif">LIVER FOOT JUEYIN</asp:ListItem>
                                <asp:ListItem Value="REN CONCEPTION.gif">REN CONCEPTION(CV=REN)</asp:ListItem>
                                <asp:ListItem Value="DU GOVERNING.gif">DU GOVERNING</asp:ListItem>
                                <asp:ListItem Value="EAR ACUPUNTURE CHART.gif">EAR ACUPUNTURE CHART</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="50%" border="0">
                                <tr>
                                    <td align="center" style="width: 50%; float: left;" valign="middle">
                                        <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 50%;
                                            height: 50%; border: 1px solid #B5DF82;">
                                            <tr>
                                                <td style="width: 50%; height: 0px;" align="center">
                                                    <table border="0" cellpadding="0" cellspacing="0" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                                        style="width: 50%; float: left;">
                                                        <tr>
                                                            <td align="center" style="width: 50%; height: 24px">
                                                                <asp:ImageMap Visible="false" ID="LUNG" ImageUrl="~/images/LUNG HAND TAIYIN.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:CircleHotSpot AlternateText="LU2" HotSpotMode="PostBack" PostBackValue="LU2"
                                                                        Radius="10" X="120" Y="90" />
                                                                    <asp:CircleHotSpot AlternateText="LU1(LUMu)" HotSpotMode="PostBack" PostBackValue="LU1(LU Mu)"
                                                                        Radius="10" X="130" Y="100" />
                                                                    <asp:CircleHotSpot AlternateText="LU3" HotSpotMode="PostBack" PostBackValue="LU3"
                                                                        Radius="10" X="210" Y="210" />
                                                                    <asp:CircleHotSpot AlternateText="LU4" HotSpotMode="PostBack" PostBackValue="LU4"
                                                                        Radius="10" X="210" Y="235" />
                                                                    <asp:CircleHotSpot AlternateText="LU5" HotSpotMode="PostBack" PostBackValue="LU5"
                                                                        Radius="10" X="225" Y="335" />
                                                                    <asp:CircleHotSpot AlternateText="LU6" HotSpotMode="PostBack" PostBackValue="LU6"
                                                                        Radius="10" X="240" Y="420" />
                                                                    <asp:CircleHotSpot AlternateText="LU7" HotSpotMode="PostBack" PostBackValue="LU7"
                                                                        Radius="7" X="270" Y="510" />
                                                                    <asp:CircleHotSpot AlternateText="LU8" HotSpotMode="PostBack" PostBackValue="LU8"
                                                                        Radius="7" X="260" Y="520" />
                                                                    <asp:CircleHotSpot AlternateText="LU9" HotSpotMode="PostBack" PostBackValue="LU9"
                                                                        Radius="7" X="270" Y="530" />
                                                                    <asp:CircleHotSpot AlternateText="LU10" HotSpotMode="PostBack" PostBackValue="LU10"
                                                                        Radius="7" X="300" Y="550" />
                                                                    <asp:CircleHotSpot AlternateText="LU11" HotSpotMode="PostBack" PostBackValue="LU11"
                                                                        Radius="7" X="320" Y="600" />
                                                                    <asp:CircleHotSpot AlternateText="LU11" HotSpotMode="PostBack" PostBackValue="LU11"
                                                                        Radius="8" X="70" Y="550" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="LARGE_INTESTINE" ImageUrl="~/images/LARGE INTESTINE HAND YANGMING.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:CircleHotSpot AlternateText="LI16" HotSpotMode="PostBack" PostBackValue="LI16"
                                                                        Radius="10" X="310" Y="60" />
                                                                    <asp:CircleHotSpot AlternateText="LI20" HotSpotMode="PostBack" PostBackValue="LI20"
                                                                        Radius="7" X="120" Y="230" />
                                                                    <asp:CircleHotSpot AlternateText="LI19" HotSpotMode="PostBack" PostBackValue="LI19"
                                                                        Radius="10" X="120" Y="240" />
                                                                    <asp:CircleHotSpot AlternateText="LI18" HotSpotMode="PostBack" PostBackValue="LI18"
                                                                        Radius="10" X="160" Y="280" />
                                                                    <asp:CircleHotSpot AlternateText="LI17" HotSpotMode="PostBack" PostBackValue="LI17"
                                                                        Radius="10" X="160" Y="305" />
                                                                    <asp:CircleHotSpot AlternateText="LI15" HotSpotMode="PostBack" PostBackValue="LI15"
                                                                        Radius="10" X="250" Y="325" />
                                                                    <asp:CircleHotSpot AlternateText="LI14" HotSpotMode="PostBack" PostBackValue="LI14"
                                                                        Radius="10" X="270" Y="380" />
                                                                    <asp:CircleHotSpot AlternateText="LI13" HotSpotMode="PostBack" PostBackValue="LI13"
                                                                        Radius="10" X="270" Y="440" />
                                                                    <asp:CircleHotSpot AlternateText="LI12" HotSpotMode="PostBack" PostBackValue="LI12"
                                                                        Radius="10" X="290" Y="460" />
                                                                    <asp:CircleHotSpot AlternateText="LI11" HotSpotMode="PostBack" PostBackValue="LI11"
                                                                        Radius="10" X="270" Y="460" />
                                                                    <asp:CircleHotSpot AlternateText="LI10" HotSpotMode="PostBack" PostBackValue="LI10"
                                                                        Radius="7" X="260" Y="464" />
                                                                    <asp:CircleHotSpot AlternateText="LI9" HotSpotMode="PostBack" PostBackValue="LI9"
                                                                        Radius="7" X="255" Y="464" />
                                                                    <asp:CircleHotSpot AlternateText="LI8" HotSpotMode="PostBack" PostBackValue="LI8"
                                                                        Radius="7" X="250" Y="471" />
                                                                    <asp:CircleHotSpot AlternateText="LI7" HotSpotMode="PostBack" PostBackValue="LI7"
                                                                        Radius="7" X="230" Y="481" />
                                                                    <asp:CircleHotSpot AlternateText="LI6" HotSpotMode="PostBack" PostBackValue="LI6"
                                                                        Radius="7" X="215" Y="485" />
                                                                    <asp:CircleHotSpot AlternateText="LI5" HotSpotMode="PostBack" PostBackValue="LI5"
                                                                        Radius="7" X="182" Y="492" />
                                                                    <asp:CircleHotSpot AlternateText="LI4" HotSpotMode="PostBack" PostBackValue="LI4"
                                                                        Radius="7" X="162" Y="492" />
                                                                    <asp:CircleHotSpot AlternateText="LI3" HotSpotMode="PostBack" PostBackValue="LI3"
                                                                        Radius="7" X="142" Y="494" />
                                                                    <asp:CircleHotSpot AlternateText="LI2" HotSpotMode="PostBack" PostBackValue="LI2"
                                                                        Radius="7" X="131" Y="496" />
                                                                    <asp:CircleHotSpot AlternateText="LI1" HotSpotMode="PostBack" PostBackValue="LI1"
                                                                        Radius="7" X="95" Y="480" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="STOMACH" ImageUrl="~/images/STOMACH FOOT YANGMING.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="102" Top="40" Right="116" Bottom="52" AlternateText="ST8"
                                                                        HotSpotMode="PostBack" PostBackValue="ST8" />
                                                                    <asp:RectangleHotSpot Left="99" Top="94" Right="111" Bottom="106" AlternateText="ST7"
                                                                        HotSpotMode="PostBack" PostBackValue="ST7" />
                                                                    <asp:RectangleHotSpot Left="96" Top="119" Right="113" Bottom="136" AlternateText="ST6"
                                                                        HotSpotMode="PostBack" PostBackValue="ST6" />
                                                                    <asp:RectangleHotSpot Left="112" Top="133" Right="129" Bottom="150" AlternateText="ST5"
                                                                        HotSpotMode="PostBack" PostBackValue="ST5" />
                                                                    <asp:RectangleHotSpot Left="225" Top="28" Right="242" Bottom="41" AlternateText="ST8"
                                                                        HotSpotMode="PostBack" PostBackValue="ST8" />
                                                                    <asp:RectangleHotSpot Left="211" Top="93" Right="229" Bottom="109" AlternateText="ST7"
                                                                        HotSpotMode="PostBack" PostBackValue="ST7" />
                                                                    <asp:RectangleHotSpot Left="241" Top="87" Right="257" Bottom="201" AlternateText="ST1"
                                                                        HotSpotMode="PostBack" PostBackValue="ST1" />
                                                                    <asp:RectangleHotSpot Left="240" Top="98" Right="256" Bottom="109" AlternateText="ST2"
                                                                        HotSpotMode="PostBack" PostBackValue="ST2" />
                                                                    <asp:RectangleHotSpot Left="239" Top="109" Right="257" Bottom="121" AlternateText="ST3"
                                                                        HotSpotMode="PostBack" PostBackValue="ST3" />
                                                                    <asp:RectangleHotSpot Left="239" Top="128" Right="257" Bottom="142" AlternateText="ST4"
                                                                        HotSpotMode="PostBack" PostBackValue="ST4" />
                                                                    <asp:RectangleHotSpot Left="221" Top="128" Right="239" Bottom="142" AlternateText="ST6"
                                                                        HotSpotMode="PostBack" PostBackValue="ST6" />
                                                                    <asp:RectangleHotSpot Left="228" Top="140" Right="246" Bottom="155" AlternateText="ST5"
                                                                        HotSpotMode="PostBack" PostBackValue="ST5" />
                                                                    <asp:RectangleHotSpot Left="245" Top="173" Right="263" Bottom="188" AlternateText="ST9"
                                                                        HotSpotMode="PostBack" PostBackValue="ST9" />
                                                                    <asp:RectangleHotSpot Left="243" Top="189" Right="261" Bottom="204" AlternateText="ST10"
                                                                        HotSpotMode="PostBack" PostBackValue="ST10" />
                                                                    <asp:RectangleHotSpot Left="243" Top="203" Right="261" Bottom="218" AlternateText="ST11"
                                                                        HotSpotMode="PostBack" PostBackValue="ST11" />
                                                                    <asp:RectangleHotSpot Left="219" Top="201" Right="237" Bottom="218" AlternateText="ST12"
                                                                        HotSpotMode="PostBack" PostBackValue="ST12" />
                                                                    <asp:RectangleHotSpot Left="219" Top="212" Right="237" Bottom="227" AlternateText="ST13"
                                                                        HotSpotMode="PostBack" PostBackValue="ST13" />
                                                                    <asp:RectangleHotSpot Left="218" Top="224" Right="236" Bottom="239" AlternateText="ST14"
                                                                        HotSpotMode="PostBack" PostBackValue="ST14" />
                                                                    <asp:RectangleHotSpot Left="213" Top="237" Right="231" Bottom="252" AlternateText="ST15"
                                                                        HotSpotMode="PostBack" PostBackValue="ST15" />
                                                                    <asp:RectangleHotSpot Left="211" Top="251" Right="229" Bottom="267" AlternateText="ST16"
                                                                        HotSpotMode="PostBack" PostBackValue="ST16" />
                                                                    <asp:RectangleHotSpot Left="205" Top="269" Right="223" Bottom="286" AlternateText="ST17"
                                                                        HotSpotMode="PostBack" PostBackValue="ST17" />
                                                                    <asp:RectangleHotSpot Left="204" Top="290" Right="224" Bottom="307" AlternateText="ST18"
                                                                        HotSpotMode="PostBack" PostBackValue="ST18" />
                                                                    <asp:RectangleHotSpot Left="237" Top="243" Right="257" Bottom="360" AlternateText="ST19"
                                                                        HotSpotMode="PostBack" PostBackValue="ST19" />
                                                                    <asp:RectangleHotSpot Left="237" Top="360" Right="257" Bottom="377" AlternateText="ST20"
                                                                        HotSpotMode="PostBack" PostBackValue="ST20" />
                                                                    <asp:RectangleHotSpot Left="237" Top="374" Right="257" Bottom="391" AlternateText="ST21"
                                                                        HotSpotMode="PostBack" PostBackValue="ST21" />
                                                                    <asp:RectangleHotSpot Left="237" Top="374" Right="257" Bottom="391" AlternateText="ST22"
                                                                        HotSpotMode="PostBack" PostBackValue="ST22" />
                                                                    <asp:RectangleHotSpot Left="236" Top="401" Right="256" Bottom="417" AlternateText="ST23"
                                                                        HotSpotMode="PostBack" PostBackValue="ST23" />
                                                                    <asp:RectangleHotSpot Left="236" Top="415" Right="256" Bottom="431" AlternateText="ST24"
                                                                        HotSpotMode="PostBack" PostBackValue="ST24" />
                                                                    <asp:RectangleHotSpot Left="233" Top="428" Right="253" Bottom="404" AlternateText="ST25(LIMu)"
                                                                        HotSpotMode="PostBack" PostBackValue="ST25(LIMu)" />
                                                                    <asp:RectangleHotSpot Left="235" Top="447" Right="255" Bottom="463" AlternateText="ST26"
                                                                        HotSpotMode="PostBack" PostBackValue="ST26" />
                                                                    <asp:RectangleHotSpot Left="232" Top="468" Right="252" Bottom="484" AlternateText="ST27"
                                                                        HotSpotMode="PostBack" PostBackValue="ST27" />
                                                                    <asp:RectangleHotSpot Left="234" Top="487" Right="254" Bottom="503" AlternateText="ST28"
                                                                        HotSpotMode="PostBack" PostBackValue="ST28" />
                                                                    <asp:RectangleHotSpot Left="234" Top="506" Right="254" Bottom="522" AlternateText="ST29"
                                                                        HotSpotMode="PostBack" PostBackValue="ST29" />
                                                                    <asp:RectangleHotSpot Left="238" Top="526" Right="258" Bottom="542" AlternateText="ST30"
                                                                        HotSpotMode="PostBack" PostBackValue="ST30" />
                                                                    <asp:RectangleHotSpot Left="581" Top="145" Right="601" Bottom="161" AlternateText="ST31"
                                                                        HotSpotMode="PostBack" PostBackValue="ST31" />
                                                                    <asp:RectangleHotSpot Left="588" Top="277" Right="608" Bottom="293" AlternateText="ST32"
                                                                        HotSpotMode="PostBack" PostBackValue="ST32" />
                                                                    <asp:RectangleHotSpot Left="586" Top="350" Right="606" Bottom="366" AlternateText="ST33"
                                                                        HotSpotMode="PostBack" PostBackValue="ST33" />
                                                                    <asp:RectangleHotSpot Left="590" Top="365" Right="610" Bottom="382" AlternateText="ST34"
                                                                        HotSpotMode="PostBack" PostBackValue="ST34" />
                                                                    <asp:RectangleHotSpot Left="586" Top="446" Right="606" Bottom="463" AlternateText="ST35"
                                                                        HotSpotMode="PostBack" PostBackValue="ST35" />
                                                                    <asp:RectangleHotSpot Left="585" Top="510" Right="605" Bottom="527" AlternateText="ST36"
                                                                        HotSpotMode="PostBack" PostBackValue="ST36" />
                                                                    <asp:RectangleHotSpot Left="583" Top="578" Right="603" Bottom="595" AlternateText="ST37"
                                                                        HotSpotMode="PostBack" PostBackValue="ST37" />
                                                                    <asp:RectangleHotSpot Left="584" Top="624" Right="599" Bottom="641" AlternateText="ST38"
                                                                        HotSpotMode="PostBack" PostBackValue="ST38" />
                                                                    <asp:RectangleHotSpot Left="583" Top="646" Right="601" Bottom="663" AlternateText="ST39"
                                                                        HotSpotMode="PostBack" PostBackValue="ST39" />
                                                                    <asp:RectangleHotSpot Left="570" Top="624" Right="585" Bottom="641" AlternateText="ST40"
                                                                        HotSpotMode="PostBack" PostBackValue="ST40" />
                                                                    <asp:RectangleHotSpot Left="597" Top="791" Right="615" Bottom="808" AlternateText="ST41"
                                                                        HotSpotMode="PostBack" PostBackValue="ST41" />
                                                                    <asp:RectangleHotSpot Left="595" Top="835" Right="613" Bottom="852" AlternateText="ST42"
                                                                        HotSpotMode="PostBack" PostBackValue="ST42" />
                                                                    <asp:RectangleHotSpot Left="587" Top="883" Right="605" Bottom="900" AlternateText="ST43"
                                                                        HotSpotMode="PostBack" PostBackValue="ST43" />
                                                                    <asp:RectangleHotSpot Left="585" Top="907" Right="603" Bottom="924" AlternateText="ST44"
                                                                        HotSpotMode="PostBack" PostBackValue="ST44" />
                                                                    <asp:RectangleHotSpot Left="581" Top="930" Right="600" Bottom="942" AlternateText="ST45"
                                                                        HotSpotMode="PostBack" PostBackValue="ST45" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="SPLEEN" ImageUrl="~/images/SPLEEN FOOT TAIYIN.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="137" Top="131" Right="154" Bottom="148" AlternateText="CV24"
                                                                        HotSpotMode="PostBack" PostBackValue="CV24" />
                                                                    <asp:RectangleHotSpot Left="138" Top="154" Right="155" Bottom="171" AlternateText="CV23"
                                                                        HotSpotMode="PostBack" PostBackValue="CV23" />
                                                                    <asp:RectangleHotSpot Left="139" Top="198" Right="156" Bottom="215" AlternateText="CV22"
                                                                        HotSpotMode="PostBack" PostBackValue="CV22" />
                                                                    <asp:RectangleHotSpot Left="140" Top="209" Right="157" Bottom="226" AlternateText="CV21"
                                                                        HotSpotMode="PostBack" PostBackValue="CV21" />
                                                                    <asp:RectangleHotSpot Left="137" Top="220" Right="154" Bottom="397" AlternateText="CV20"
                                                                        HotSpotMode="PostBack" PostBackValue="CV20" />
                                                                    <asp:RectangleHotSpot Left="139" Top="239" Right="156" Bottom="256" AlternateText="CV19"
                                                                        HotSpotMode="PostBack" PostBackValue="CV19" />
                                                                    <asp:RectangleHotSpot Left="140" Top="252" Right="157" Bottom="269" AlternateText="CV18"
                                                                        HotSpotMode="PostBack" PostBackValue="CV18" />
                                                                    <asp:RectangleHotSpot Left="138" Top="266" Right="155" Bottom="283" AlternateText="CV17"
                                                                        HotSpotMode="PostBack" PostBackValue="CV17" />
                                                                    <asp:RectangleHotSpot Left="138" Top="282" Right="155" Bottom="299" AlternateText="CV16"
                                                                        HotSpotMode="PostBack" PostBackValue="CV16" />
                                                                    <asp:RectangleHotSpot Left="139" Top="312" Right="156" Bottom="329" AlternateText="CV15"
                                                                        HotSpotMode="PostBack" PostBackValue="CV15" />
                                                                    <asp:RectangleHotSpot Left="139" Top="327" Right="156" Bottom="344" AlternateText="CV14"
                                                                        HotSpotMode="PostBack" PostBackValue="CV14" />
                                                                    <asp:RectangleHotSpot Left="140" Top="346" Right="157" Bottom="263" AlternateText="CV13"
                                                                        HotSpotMode="PostBack" PostBackValue="CV13" />
                                                                    <asp:RectangleHotSpot Left="137" Top="362" Right="154" Bottom="379" AlternateText="CV12"
                                                                        HotSpotMode="PostBack" PostBackValue="CV12" />
                                                                    <asp:RectangleHotSpot Left="139" Top="377" Right="156" Bottom="394" AlternateText="CV11"
                                                                        HotSpotMode="PostBack" PostBackValue="CV11" />
                                                                    <asp:RectangleHotSpot Left="141" Top="390" Right="158" Bottom="407" AlternateText="CV10"
                                                                        HotSpotMode="PostBack" PostBackValue="CV10" />
                                                                    <asp:RectangleHotSpot Left="137" Top="404" Right="154" Bottom="421" AlternateText="CV9"
                                                                        HotSpotMode="PostBack" PostBackValue="CV9" />
                                                                    <asp:RectangleHotSpot Left="138" Top="417" Right="155" Bottom="434" AlternateText="CV8"
                                                                        HotSpotMode="PostBack" PostBackValue="CV8" />
                                                                    <asp:RectangleHotSpot Left="139" Top="430" Right="156" Bottom="447" AlternateText="CV7"
                                                                        HotSpotMode="PostBack" PostBackValue="CV7" />
                                                                    <asp:RectangleHotSpot Left="138" Top="440" Right="155" Bottom="457" AlternateText="CV6"
                                                                        HotSpotMode="PostBack" PostBackValue="CV6" />
                                                                    <asp:RectangleHotSpot Left="139" Top="450" Right="156" Bottom="467" AlternateText="CV5"
                                                                        HotSpotMode="PostBack" PostBackValue="CV5" />
                                                                    <asp:RectangleHotSpot Left="141" Top="465" Right="158" Bottom="482" AlternateText="CV4"
                                                                        HotSpotMode="PostBack" PostBackValue="CV4" />
                                                                    <asp:RectangleHotSpot Left="140" Top="479" Right="157" Bottom="496" AlternateText="CV3"
                                                                        HotSpotMode="PostBack" PostBackValue="CV3" />
                                                                    <asp:RectangleHotSpot Left="141" Top="496" Right="158" Bottom="513" AlternateText="CV2"
                                                                        HotSpotMode="PostBack" PostBackValue="CV2" />
                                                                    <asp:RectangleHotSpot Left="313" Top="541" Right="330" Bottom="558" AlternateText="SP1"
                                                                        HotSpotMode="PostBack" PostBackValue="SP1" />
                                                                    <asp:RectangleHotSpot Left="347" Top="549" Right="364" Bottom="566" AlternateText="SP2"
                                                                        HotSpotMode="PostBack" PostBackValue="SP2" />
                                                                    <asp:RectangleHotSpot Left="368" Top="546" Right="385" Bottom="563" AlternateText="SP3"
                                                                        HotSpotMode="PostBack" PostBackValue="SP3" />
                                                                    <asp:RectangleHotSpot Left="385" Top="540" Right="402" Bottom="557" AlternateText="SP4"
                                                                        HotSpotMode="PostBack" PostBackValue="SP4" />
                                                                    <asp:RectangleHotSpot Left="419" Top="491" Right="436" Bottom="508" AlternateText="SP5"
                                                                        HotSpotMode="PostBack" PostBackValue="SP5" />
                                                                    <asp:RectangleHotSpot Left="439" Top="436" Right="456" Bottom="453" AlternateText="SP6"
                                                                        HotSpotMode="PostBack" PostBackValue="SP6" />
                                                                    <asp:RectangleHotSpot Left="438" Top="387" Right="455" Bottom="404" AlternateText="SP7"
                                                                        HotSpotMode="PostBack" PostBackValue="SP7" />
                                                                    <asp:RectangleHotSpot Left="438" Top="327" Right="455" Bottom="344" AlternateText="SP8"
                                                                        HotSpotMode="PostBack" PostBackValue="SP8" />
                                                                    <asp:RectangleHotSpot Left="439" Top="283" Right="456" Bottom="300" AlternateText="SP9"
                                                                        HotSpotMode="PostBack" PostBackValue="SP9" />
                                                                    <asp:RectangleHotSpot Left="414" Top="210" Right="431" Bottom="227" AlternateText="SP10"
                                                                        HotSpotMode="PostBack" PostBackValue="SP10" />
                                                                    <asp:RectangleHotSpot Left="395" Top="140" Right="412" Bottom="157" AlternateText="SP11"
                                                                        HotSpotMode="PostBack" PostBackValue="SP11" />
                                                                    <asp:RectangleHotSpot Left="80" Top="493" Right="97" Bottom="510" AlternateText="SP12"
                                                                        HotSpotMode="PostBack" PostBackValue="SP12" />
                                                                    <asp:RectangleHotSpot Left="74" Top="484" Right="91" Bottom="501" AlternateText="SP13"
                                                                        HotSpotMode="PostBack" PostBackValue="SP13" />
                                                                    <asp:RectangleHotSpot Left="75" Top="439" Right="92" Bottom="456" AlternateText="SP14"
                                                                        HotSpotMode="PostBack" PostBackValue="SP14" />
                                                                    <asp:RectangleHotSpot Left="75" Top="416" Right="92" Bottom="433" AlternateText="SP15"
                                                                        HotSpotMode="PostBack" PostBackValue="SP15" />
                                                                    <asp:RectangleHotSpot Left="74" Top="379" Right="91" Bottom="396" AlternateText="SP16"
                                                                        HotSpotMode="PostBack" PostBackValue="SP16" />
                                                                    <asp:RectangleHotSpot Left="56" Top="270" Right="73" Bottom="287" AlternateText="SP17"
                                                                        HotSpotMode="PostBack" PostBackValue="SP17" />
                                                                    <asp:RectangleHotSpot Left="59" Top="244" Right="76" Bottom="261" AlternateText="SP18"
                                                                        HotSpotMode="PostBack" PostBackValue="SP18" />
                                                                    <asp:RectangleHotSpot Left="68" Top="225" Right="85" Bottom="242" AlternateText="SP19"
                                                                        HotSpotMode="PostBack" PostBackValue="SP19" />
                                                                    <asp:RectangleHotSpot Left="75" Top="207" Right="92" Bottom="224" AlternateText="SP20"
                                                                        HotSpotMode="PostBack" PostBackValue="SP20" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="HEART" ImageUrl="~/images/HEART HAND SHAOYIN.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="134" Top="443" Right="151" Bottom="464" AlternateText="HT1"
                                                                        HotSpotMode="PostBack" PostBackValue="HT1" />
                                                                    <asp:RectangleHotSpot Left="65" Top="435" Right="82" Bottom="456" AlternateText="HT2"
                                                                        HotSpotMode="PostBack" PostBackValue="HT2" />
                                                                    <asp:RectangleHotSpot Left="23" Top="432" Right="40" Bottom="453" AlternateText="HT3"
                                                                        HotSpotMode="PostBack" PostBackValue="HT3" />
                                                                    <asp:RectangleHotSpot Left="282" Top="344" Right="301" Bottom="358" AlternateText="HT4"
                                                                        HotSpotMode="PostBack" PostBackValue="HT4" />
                                                                    <asp:RectangleHotSpot Left="284" Top="352" Right="303" Bottom="366" AlternateText="HT5"
                                                                        HotSpotMode="PostBack" PostBackValue="HT5" />
                                                                    <asp:RectangleHotSpot Left="286" Top="362" Right="305" Bottom="376" AlternateText="HT6"
                                                                        HotSpotMode="PostBack" PostBackValue="HT6" />
                                                                    <asp:RectangleHotSpot Left="299" Top="369" Right="316" Bottom="383" AlternateText="HT7"
                                                                        HotSpotMode="PostBack" PostBackValue="HT7" />
                                                                    <asp:RectangleHotSpot Left="309" Top="407" Right="326" Bottom="428" AlternateText="HT8"
                                                                        HotSpotMode="PostBack" PostBackValue="HT8" />
                                                                    <asp:RectangleHotSpot Left="308" Top="427" Right="325" Bottom="448" AlternateText="HT9"
                                                                        HotSpotMode="PostBack" PostBackValue="HT9" />
                                                                    <asp:RectangleHotSpot Left="215" Top="175" Right="234" Bottom="194" AlternateText="HT2"
                                                                        HotSpotMode="PostBack" PostBackValue="HT2" />
                                                                    <asp:RectangleHotSpot Left="229" Top="216" Right="248" Bottom="235" AlternateText="HT3"
                                                                        HotSpotMode="PostBack" PostBackValue="HT3" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="SMALL_INTESTINE" ImageUrl="~/images/SMALL INTESTINE HAND TAIYANG.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="234" Top="590" Right="251" Bottom="607" AlternateText="SI1"
                                                                        HotSpotMode="PostBack" PostBackValue="SI1" />
                                                                    <asp:RectangleHotSpot Left="225" Top="551" Right="242" Bottom="568" AlternateText="SI2"
                                                                        HotSpotMode="PostBack" PostBackValue="SI2" />
                                                                    <asp:RectangleHotSpot Left="222" Top="539" Right="239" Bottom="556" AlternateText="SI3"
                                                                        HotSpotMode="PostBack" PostBackValue="SI3" />
                                                                    <asp:RectangleHotSpot Left="216" Top="517" Right="233" Bottom="534" AlternateText="SI4"
                                                                        HotSpotMode="PostBack" PostBackValue="SI4" />
                                                                    <asp:RectangleHotSpot Left="214" Top="496" Right="231" Bottom="511" AlternateText="SI5"
                                                                        HotSpotMode="PostBack" PostBackValue="SI5" />
                                                                    <asp:RectangleHotSpot Left="211" Top="483" Right="228" Bottom="498" AlternateText="SI6"
                                                                        HotSpotMode="PostBack" PostBackValue="SI6" />
                                                                    <asp:RectangleHotSpot Left="190" Top="444" Right="207" Bottom="462" AlternateText="SI7"
                                                                        HotSpotMode="PostBack" PostBackValue="SI7" />
                                                                    <asp:RectangleHotSpot Left="167" Top="365" Right="184" Bottom="383" AlternateText="SI8"
                                                                        HotSpotMode="PostBack" PostBackValue="SI8" />
                                                                    <asp:RectangleHotSpot Left="142" Top="221" Right="156" Bottom="239" AlternateText="SI9"
                                                                        HotSpotMode="PostBack" PostBackValue="SI9" />
                                                                    <asp:RectangleHotSpot Left="141" Top="195" Right="158" Bottom="213" AlternateText="SI10"
                                                                        HotSpotMode="PostBack" PostBackValue="SI10" />
                                                                    <asp:RectangleHotSpot Left="106" Top="222" Right="123" Bottom="240" AlternateText="SI11"
                                                                        HotSpotMode="PostBack" PostBackValue="SI11" />
                                                                    <asp:RectangleHotSpot Left="114" Top="175" Right="131" Bottom="193" AlternateText="SI12"
                                                                        HotSpotMode="PostBack" PostBackValue="SI12" />
                                                                    <asp:RectangleHotSpot Left="96" Top="181" Right="113" Bottom="199" AlternateText="SI13"
                                                                        HotSpotMode="PostBack" PostBackValue="SI13" />
                                                                    <asp:RectangleHotSpot Left="80" Top="164" Right="97" Bottom="182" AlternateText="SI14"
                                                                        HotSpotMode="PostBack" PostBackValue="SI14" />
                                                                    <asp:RectangleHotSpot Left="69" Top="151" Right="86" Bottom="169" AlternateText="SI15"
                                                                        HotSpotMode="PostBack" PostBackValue="SI15" />
                                                                    <asp:RectangleHotSpot Left="274" Top="142" Right="291" Bottom="160" AlternateText="SI15"
                                                                        HotSpotMode="PostBack" PostBackValue="SI15" />
                                                                    <asp:RectangleHotSpot Left="303" Top="116" Right="320" Bottom="134" AlternateText="SI16"
                                                                        HotSpotMode="PostBack" PostBackValue="SI16" />
                                                                    <asp:RectangleHotSpot Left="321" Top="101" Right="338" Bottom="119" AlternateText="SI17"
                                                                        HotSpotMode="PostBack" PostBackValue="SI17" />
                                                                    <asp:RectangleHotSpot Left="348" Top="81" Right="365" Bottom="99" AlternateText="SI18"
                                                                        HotSpotMode="PostBack" PostBackValue="SI18" />
                                                                    <asp:RectangleHotSpot Left="323" Top="71" Right="340" Bottom="89" AlternateText="SI19"
                                                                        HotSpotMode="PostBack" PostBackValue="SI19" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="URINARY" ImageUrl="~/images/URINARY BLADDER FOOT TAIYANG.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="702" Top="397" Right="717" Bottom="415" AlternateText="UB58"
                                                                        HotSpotMode="PostBack" PostBackValue="UB58" />
                                                                    <asp:RectangleHotSpot Left="703" Top="451" Right="718" Bottom="469" AlternateText="UB59"
                                                                        HotSpotMode="PostBack" PostBackValue="UB59" />
                                                                    <asp:RectangleHotSpot Left="702" Top="485" Right="717" Bottom="503" AlternateText="UB60"
                                                                        HotSpotMode="PostBack" PostBackValue="UB60" />
                                                                    <asp:RectangleHotSpot Left="704" Top="510" Right="719" Bottom="528" AlternateText="UB61"
                                                                        HotSpotMode="PostBack" PostBackValue="UB61" />
                                                                    <asp:RectangleHotSpot Left="718" Top="504" Right="733" Bottom="522" AlternateText="UB62"
                                                                        HotSpotMode="PostBack" PostBackValue="UB62" />
                                                                    <asp:RectangleHotSpot Left="730" Top="511" Right="745" Bottom="529" AlternateText="UB63"
                                                                        HotSpotMode="PostBack" PostBackValue="UB63" />
                                                                    <asp:RectangleHotSpot Left="741" Top="521" Right="756" Bottom="539" AlternateText="UB64"
                                                                        HotSpotMode="PostBack" PostBackValue="UB64" />
                                                                    <asp:RectangleHotSpot Left="760" Top="528" Right="775" Bottom="546" AlternateText="UB65"
                                                                        HotSpotMode="PostBack" PostBackValue="UB65" />
                                                                    <asp:RectangleHotSpot Left="775" Top="533" Right="790" Bottom="551" AlternateText="UB66"
                                                                        HotSpotMode="PostBack" PostBackValue="UB66" />
                                                                    <asp:RectangleHotSpot Left="787" Top="533" Right="804" Bottom="552" AlternateText="UB67"
                                                                        HotSpotMode="PostBack" PostBackValue="UB67" />
                                                                    <asp:RectangleHotSpot Left="486" Top="104" Right="501" Bottom="122" AlternateText="UB35"
                                                                        HotSpotMode="PostBack" PostBackValue="UB35" />
                                                                    <asp:RectangleHotSpot Left="550" Top="168" Right="556" Bottom="186" AlternateText="UB36(50)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB36(50)" />
                                                                    <asp:RectangleHotSpot Left="552" Top="256" Right="567" Bottom="274" AlternateText="UB37(51)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB37(51)" />
                                                                    <asp:RectangleHotSpot Left="567" Top="363" Right="582" Bottom="381" AlternateText="UB38(52)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB38(52)" />
                                                                    <asp:RectangleHotSpot Left="566" Top="376" Right="581" Bottom="394" AlternateText="UB39(53)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB39(53)" />
                                                                    <asp:RectangleHotSpot Left="543" Top="373" Right="558" Bottom="391" AlternateText="UB40(54)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB40(54)" />
                                                                    <asp:RectangleHotSpot Left="542" Top="402" Right="557" Bottom="420" AlternateText="UB55"
                                                                        HotSpotMode="PostBack" PostBackValue="UB55" />
                                                                    <asp:RectangleHotSpot Left="542" Top="450" Right="557" Bottom="468" AlternateText="UB56"
                                                                        HotSpotMode="PostBack" PostBackValue="UB56" />
                                                                    <asp:RectangleHotSpot Left="539" Top="490" Right="554" Bottom="508" AlternateText="UB57"
                                                                        HotSpotMode="PostBack" PostBackValue="UB57" />
                                                                    <asp:RectangleHotSpot Left="551" Top="501" Right="566" Bottom="519" AlternateText="UB58"
                                                                        HotSpotMode="PostBack" PostBackValue="UB58" />
                                                                    <asp:RectangleHotSpot Left="541" Top="562" Right="556" Bottom="580" AlternateText="UB59"
                                                                        HotSpotMode="PostBack" PostBackValue="UB59" />
                                                                    <asp:RectangleHotSpot Left="535" Top="606" Right="550" Bottom="624" AlternateText="UB60"
                                                                        HotSpotMode="PostBack" PostBackValue="UB60" />
                                                                    <asp:RectangleHotSpot Left="328" Top="76" Right="343" Bottom="93" AlternateText="UB1"
                                                                        HotSpotMode="PostBack" PostBackValue="UB1" />
                                                                    <asp:RectangleHotSpot Left="328" Top="65" Right="343" Bottom="82" AlternateText="UB2"
                                                                        HotSpotMode="PostBack" PostBackValue="UB2" />
                                                                    <asp:RectangleHotSpot Left="323" Top="18" Right="338" Bottom="35" AlternateText="UB3"
                                                                        HotSpotMode="PostBack" PostBackValue="UB3" />
                                                                    <asp:RectangleHotSpot Left="336" Top="16" Right="351" Bottom="33" AlternateText="UB4"
                                                                        HotSpotMode="PostBack" PostBackValue="UB4" />
                                                                    <asp:RectangleHotSpot Left="334" Top="6" Right="349" Bottom="23" AlternateText="UB5"
                                                                        HotSpotMode="PostBack" PostBackValue="UB5" />
                                                                    <asp:RectangleHotSpot Left="148" Top="4" Right="163" Bottom="21" AlternateText="UB7"
                                                                        HotSpotMode="PostBack" PostBackValue="UB7" />
                                                                    <asp:RectangleHotSpot Left="147" Top="24" Right="162" Bottom="41" AlternateText="UB8"
                                                                        HotSpotMode="PostBack" PostBackValue="UB8" />
                                                                    <asp:RectangleHotSpot Left="149" Top="69" Right="164" Bottom="86" AlternateText="UB9"
                                                                        HotSpotMode="PostBack" PostBackValue="UB9" />
                                                                    <asp:RectangleHotSpot Left="147" Top="109" Right="162" Bottom="126" AlternateText="UB10"
                                                                        HotSpotMode="PostBack" PostBackValue="UB10" />
                                                                    <asp:RectangleHotSpot Left="327" Top="453" Right="342" Bottom="470" AlternateText="UB2"
                                                                        HotSpotMode="PostBack" PostBackValue="UB2" />
                                                                    <asp:RectangleHotSpot Left="323" Top="408" Right="338" Bottom="425" AlternateText="UB3"
                                                                        HotSpotMode="PostBack" PostBackValue="UB3" />
                                                                    <asp:RectangleHotSpot Left="337" Top="410" Right="352" Bottom="427" AlternateText="UB4"
                                                                        HotSpotMode="PostBack" PostBackValue="UB4" />
                                                                    <asp:RectangleHotSpot Left="338" Top="397" Right="353" Bottom="414" AlternateText="UB5"
                                                                        HotSpotMode="PostBack" PostBackValue="UB5" />
                                                                    <asp:RectangleHotSpot Left="338" Top="376" Right="353" Bottom="393" AlternateText="UB6"
                                                                        HotSpotMode="PostBack" PostBackValue="UB6" />
                                                                    <asp:RectangleHotSpot Left="341" Top="357" Right="356" Bottom="374" AlternateText="UB7"
                                                                        HotSpotMode="PostBack" PostBackValue="UB7" />
                                                                    <asp:RectangleHotSpot Left="150" Top="173" Right="165" Bottom="190" AlternateText="UB11"
                                                                        HotSpotMode="PostBack" PostBackValue="UB11" />
                                                                    <asp:RectangleHotSpot Left="168" Top="189" Right="183" Bottom="208" AlternateText="UB41(36)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB41(36)" />
                                                                    <asp:RectangleHotSpot Left="169" Top="202" Right="184" Bottom="221" AlternateText="UB42(37)LU"
                                                                        HotSpotMode="PostBack" PostBackValue="UB42(37)LU" />
                                                                    <asp:RectangleHotSpot Left="169" Top="217" Right="184" Bottom="236" AlternateText="UB43(38)PC"
                                                                        HotSpotMode="PostBack" PostBackValue="UB43(38)PC" />
                                                                    <asp:RectangleHotSpot Left="170" Top="231" Right="185" Bottom="250" AlternateText="UB44(39)HT"
                                                                        HotSpotMode="PostBack" PostBackValue="UB44(39)HT" />
                                                                    <asp:RectangleHotSpot Left="168" Top="249" Right="183" Bottom="268" AlternateText="UB45(40)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB45(40)" />
                                                                    <asp:RectangleHotSpot Left="168" Top="265" Right="183" Bottom="284" AlternateText="UB46(41)Diaphragm"
                                                                        HotSpotMode="PostBack" PostBackValue="UB46(41)Diaphragm" />
                                                                    <asp:RectangleHotSpot Left="169" Top="299" Right="185" Bottom="318" AlternateText="UB47(42)LV"
                                                                        HotSpotMode="PostBack" PostBackValue="UB47(42)LV" />
                                                                    <asp:RectangleHotSpot Left="169" Top="316" Right="185" Bottom="335" AlternateText="UB48(43)GB"
                                                                        HotSpotMode="PostBack" PostBackValue="UB48(43)GB" />
                                                                    <asp:RectangleHotSpot Left="169" Top="331" Right="185" Bottom="350" AlternateText="UB49(44)SP"
                                                                        HotSpotMode="PostBack" PostBackValue="UB49(44)SP" />
                                                                    <asp:RectangleHotSpot Left="170" Top="348" Right="186" Bottom="367" AlternateText="UB50(45)ST"
                                                                        HotSpotMode="PostBack" PostBackValue="UB50(45)ST" />
                                                                    <asp:RectangleHotSpot Left="171" Top="363" Right="187" Bottom="382" AlternateText="UB51(46)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB51(46)" />
                                                                    <asp:RectangleHotSpot Left="170" Top="376" Right="186" Bottom="395" AlternateText="UB52(47)"
                                                                        HotSpotMode="PostBack" PostBackValue="UB52(47)" />
                                                                    <asp:RectangleHotSpot Left="146" Top="442" Right="162" Bottom="461" AlternateText="UB31"
                                                                        HotSpotMode="PostBack" PostBackValue="UB31" />
                                                                    <asp:RectangleHotSpot Left="144" Top="456" Right="160" Bottom="475" AlternateText="UB32"
                                                                        HotSpotMode="PostBack" PostBackValue="UB32" />
                                                                    <asp:RectangleHotSpot Left="144" Top="456" Right="160" Bottom="475" AlternateText="UB32"
                                                                        HotSpotMode="PostBack" PostBackValue="UB32" />
                                                                    <asp:RectangleHotSpot Left="142" Top="466" Right="158" Bottom="485" AlternateText="UB33"
                                                                        HotSpotMode="PostBack" PostBackValue="UB33" />
                                                                    <asp:RectangleHotSpot Left="143" Top="476" Right="155" Bottom="494" AlternateText="UB34"
                                                                        HotSpotMode="PostBack" PostBackValue="UB34" />
                                                                    <asp:RectangleHotSpot Left="139" Top="496" Right="155" Bottom="514" AlternateText="UB35"
                                                                        HotSpotMode="PostBack" PostBackValue="UB35" />
                                                                    <asp:RectangleHotSpot Left="173" Top="555" Right="189" Bottom="574" AlternateText="UB36"
                                                                        HotSpotMode="PostBack" PostBackValue="UB36" />
                                                                    <asp:RectangleHotSpot Left="171" Top="454" Right="187" Bottom="473" AlternateText="UB53(48)UB"
                                                                        HotSpotMode="PostBack" PostBackValue="UB53(48)UB" />
                                                                    <asp:RectangleHotSpot Left="150" Top="479" Right="166" Bottom="497" AlternateText="UB30"
                                                                        HotSpotMode="PostBack" PostBackValue="UB30" />
                                                                    <asp:RectangleHotSpot Left="169" Top="478" Right="185" Bottom="497" AlternateText="UB54"
                                                                        HotSpotMode="PostBack" PostBackValue="UB54" />
                                                                    <asp:RectangleHotSpot Left="149" Top="186" Right="165" Bottom="204" AlternateText="UB12"
                                                                        HotSpotMode="PostBack" PostBackValue="UB12" />
                                                                    <asp:RectangleHotSpot Left="149" Top="201" Right="165" Bottom="219" AlternateText="UB13"
                                                                        HotSpotMode="PostBack" PostBackValue="UB13" />
                                                                    <asp:RectangleHotSpot Left="151" Top="215" Right="167" Bottom="233" AlternateText="UB14"
                                                                        HotSpotMode="PostBack" PostBackValue="UB14" />
                                                                    <asp:RectangleHotSpot Left="150" Top="229" Right="166" Bottom="247" AlternateText="UB15"
                                                                        HotSpotMode="PostBack" PostBackValue="UB15" />
                                                                    <asp:RectangleHotSpot Left="150" Top="244" Right="166" Bottom="262" AlternateText="UB16"
                                                                        HotSpotMode="PostBack" PostBackValue="UB16" />
                                                                    <asp:RectangleHotSpot Left="151" Top="260" Right="166" Bottom="278" AlternateText="UB17"
                                                                        HotSpotMode="PostBack" PostBackValue="UB17" />
                                                                    <asp:RectangleHotSpot Left="152" Top="292" Right="168" Bottom="310" AlternateText="UB18"
                                                                        HotSpotMode="PostBack" PostBackValue="UB18" />
                                                                    <asp:RectangleHotSpot Left="150" Top="308" Right="166" Bottom="326" AlternateText="UB19"
                                                                        HotSpotMode="PostBack" PostBackValue="UB19" />
                                                                    <asp:RectangleHotSpot Left="151" Top="323" Right="167" Bottom="341" AlternateText="UB20"
                                                                        HotSpotMode="PostBack" PostBackValue="UB20" />
                                                                    <asp:RectangleHotSpot Left="150" Top="339" Right="166" Bottom="357" AlternateText="UB21"
                                                                        HotSpotMode="PostBack" PostBackValue="UB21" />
                                                                    <asp:RectangleHotSpot Left="150" Top="355" Right="166" Bottom="373" AlternateText="UB22"
                                                                        HotSpotMode="PostBack" PostBackValue="UB22" />
                                                                    <asp:RectangleHotSpot Left="149" Top="371" Right="165" Bottom="389" AlternateText="UB23"
                                                                        HotSpotMode="PostBack" PostBackValue="UB23" />
                                                                    <asp:RectangleHotSpot Left="151" Top="385" Right="167" Bottom="403" AlternateText="UB24"
                                                                        HotSpotMode="PostBack" PostBackValue="UB24" />
                                                                    <asp:RectangleHotSpot Left="151" Top="407" Right="167" Bottom="425" AlternateText="UB25"
                                                                        HotSpotMode="PostBack" PostBackValue="UB25" />
                                                                    <asp:RectangleHotSpot Left="151" Top="424" Right="167" Bottom="442" AlternateText="UB26"
                                                                        HotSpotMode="PostBack" PostBackValue="UB26" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="KIDNEY" ImageUrl="~/images/KIDNEY FOOT SHAOYIN.gif"
                                                                    runat="server" OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="155" Top="70" Right="172" Bottom="88" AlternateText="KD27"
                                                                        HotSpotMode="PostBack" PostBackValue="KD27" />
                                                                    <asp:RectangleHotSpot Left="150" Top="86" Right="171" Bottom="104" AlternateText="KD26"
                                                                        HotSpotMode="PostBack" PostBackValue="KD26" />
                                                                    <asp:RectangleHotSpot Left="148" Top="108" Right="169" Bottom="126" AlternateText="KD25"
                                                                        HotSpotMode="PostBack" PostBackValue="KD25" />
                                                                    <asp:RectangleHotSpot Left="147" Top="134" Right="168" Bottom="152" AlternateText="KD24"
                                                                        HotSpotMode="PostBack" PostBackValue="KD24" />
                                                                    <asp:RectangleHotSpot Left="144" Top="154" Right="165" Bottom="172" AlternateText="KD23"
                                                                        HotSpotMode="PostBack" PostBackValue="KD23" />
                                                                    <asp:RectangleHotSpot Left="145" Top="180" Right="166" Bottom="198" AlternateText="KD22"
                                                                        HotSpotMode="PostBack" PostBackValue="KD22" />
                                                                    <asp:RectangleHotSpot Left="177" Top="245" Right="198" Bottom="263" AlternateText="KD21"
                                                                        HotSpotMode="PostBack" PostBackValue="KD21" />
                                                                    <asp:RectangleHotSpot Left="174" Top="265" Right="195" Bottom="283" AlternateText="KD20"
                                                                        HotSpotMode="PostBack" PostBackValue="KD20" />
                                                                    <asp:RectangleHotSpot Left="175" Top="284" Right="196" Bottom="302" AlternateText="KD19"
                                                                        HotSpotMode="PostBack" PostBackValue="KD19" />
                                                                    <asp:RectangleHotSpot Left="175" Top="301" Right="196" Bottom="319" AlternateText="KD18"
                                                                        HotSpotMode="PostBack" PostBackValue="KD18" />
                                                                    <asp:RectangleHotSpot Left="175" Top="322" Right="196" Bottom="340" AlternateText="KD17"
                                                                        HotSpotMode="PostBack" PostBackValue="KD17" />
                                                                    <asp:RectangleHotSpot Left="173" Top="360" Right="194" Bottom="378" AlternateText="KD16"
                                                                        HotSpotMode="PostBack" PostBackValue="KD16" />
                                                                    <asp:RectangleHotSpot Left="177" Top="379" Right="198" Bottom="397" AlternateText="KD15"
                                                                        HotSpotMode="PostBack" PostBackValue="KD15" />
                                                                    <asp:RectangleHotSpot Left="179" Top="394" Right="200" Bottom="412" AlternateText="KD14"
                                                                        HotSpotMode="PostBack" PostBackValue="KD14" />
                                                                    <asp:RectangleHotSpot Left="177" Top="409" Right="198" Bottom="427" AlternateText="KD13"
                                                                        HotSpotMode="PostBack" PostBackValue="KD13" />
                                                                    <asp:RectangleHotSpot Left="178" Top="422" Right="199" Bottom="440" AlternateText="KD12"
                                                                        HotSpotMode="PostBack" PostBackValue="KD12" />
                                                                    <asp:RectangleHotSpot Left="178" Top="442" Right="199" Bottom="460" AlternateText="KD11"
                                                                        HotSpotMode="PostBack" PostBackValue="KD11" />
                                                                    <asp:RectangleHotSpot Left="562" Top="98" Right="583" Bottom="116" AlternateText="KD10"
                                                                        HotSpotMode="PostBack" PostBackValue="KD10" />
                                                                    <asp:RectangleHotSpot Left="559" Top="260" Right="580" Bottom="278" AlternateText="KD9"
                                                                        HotSpotMode="PostBack" PostBackValue="KD9" />
                                                                    <asp:RectangleHotSpot Left="539" Top="308" Right="560" Bottom="326" AlternateText="KD8"
                                                                        HotSpotMode="PostBack" PostBackValue="KD8" />
                                                                    <asp:RectangleHotSpot Left="554" Top="308" Right="575" Bottom="326" AlternateText="KD7"
                                                                        HotSpotMode="PostBack" PostBackValue="KD7" />
                                                                    <asp:RectangleHotSpot Left="537" Top="356" Right="555" Bottom="374" AlternateText="KD6"
                                                                        HotSpotMode="PostBack" PostBackValue="KD6" />
                                                                    <asp:RectangleHotSpot Left="558" Top="345" Right="574" Bottom="361" AlternateText="KD3"
                                                                        HotSpotMode="PostBack" PostBackValue="KD3" />
                                                                    <asp:RectangleHotSpot Left="511" Top="391" Right="527" Bottom="407" AlternateText="KD2"
                                                                        HotSpotMode="PostBack" PostBackValue="KD2" />
                                                                    <asp:RectangleHotSpot Left="568" Top="356" Right="584" Bottom="372" AlternateText="KD4"
                                                                        HotSpotMode="PostBack" PostBackValue="KD4" />
                                                                    <asp:RectangleHotSpot Left="557" Top="369" Right="573" Bottom="385" AlternateText="KD5"
                                                                        HotSpotMode="PostBack" PostBackValue="KD5" />
                                                                    <asp:RectangleHotSpot Left="746" Top="546" Right="762" Bottom="562" AlternateText="KD1"
                                                                        HotSpotMode="PostBack" PostBackValue="KD1" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="PERICARDIUM" ImageUrl="~/images/PERICARDIUM HAND JUEYIN.gif"
                                                                    OnClick="Image_Click" runat="server">
                                                                    <asp:RectangleHotSpot Left="77" Top="212" Right="97" Bottom="229" AlternateText="PC1"
                                                                        HotSpotMode="PostBack" PostBackValue="PC1" />
                                                                    <asp:RectangleHotSpot Left="151" Top="244" Right="171" Bottom="261" AlternateText="PC2"
                                                                        HotSpotMode="PostBack" PostBackValue="PC2" />
                                                                    <asp:RectangleHotSpot Left="162" Top="414" Right="182" Bottom="431" AlternateText="PC3"
                                                                        HotSpotMode="PostBack" PostBackValue="PC3" />
                                                                    <asp:RectangleHotSpot Left="203" Top="535" Right="223" Bottom="552" AlternateText="PC4"
                                                                        HotSpotMode="PostBack" PostBackValue="PC4" />
                                                                    <asp:RectangleHotSpot Left="212" Top="565" Right="232" Bottom="582" AlternateText="PC5"
                                                                        HotSpotMode="PostBack" PostBackValue="PC5" />
                                                                    <asp:RectangleHotSpot Left="213" Top="585" Right="233" Bottom="602" AlternateText="PC6"
                                                                        HotSpotMode="PostBack" PostBackValue="PC6" />
                                                                    <asp:RectangleHotSpot Left="222" Top="618" Right="242" Bottom="635" AlternateText="PC7"
                                                                        HotSpotMode="PostBack" PostBackValue="PC7" />
                                                                    <asp:RectangleHotSpot Left="249" Top="687" Right="269" Bottom="704" AlternateText="PC8"
                                                                        HotSpotMode="PostBack" PostBackValue="PC8" />
                                                                    <asp:RectangleHotSpot Left="255" Top="792" Right="275" Bottom="809" AlternateText="PC9"
                                                                        HotSpotMode="PostBack" PostBackValue="PC9" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="SAN" ImageUrl="~/images/SAN JIAO HAND SHAOYANG.gif"
                                                                    OnClick="Image_Click" runat="server">
                                                                    <asp:RectangleHotSpot Left="179" Top="70" Right="198" Bottom="90" AlternateText="TH20"
                                                                        HotSpotMode="PostBack" PostBackValue="TH20" />
                                                                    <asp:RectangleHotSpot Left="199" Top="87" Right="214" Bottom="104" AlternateText="TH19"
                                                                        HotSpotMode="PostBack" PostBackValue="TH19" />
                                                                    <asp:RectangleHotSpot Left="199" Top="100" Right="214" Bottom="117" AlternateText="TH18"
                                                                        HotSpotMode="PostBack" PostBackValue="TH18" />
                                                                    <asp:RectangleHotSpot Left="188" Top="114" Right="203" Bottom="131" AlternateText="TH17"
                                                                        HotSpotMode="PostBack" PostBackValue="TH17" />
                                                                    <asp:RectangleHotSpot Left="204" Top="122" Right="219" Bottom="239" AlternateText="TH16"
                                                                        HotSpotMode="PostBack" PostBackValue="TH16" />
                                                                    <asp:RectangleHotSpot Left="173" Top="187" Right="188" Bottom="204" AlternateText="TH15"
                                                                        HotSpotMode="PostBack" PostBackValue="TH15" />
                                                                    <asp:RectangleHotSpot Left="132" Top="227" Right="149" Bottom="244" AlternateText="TH14"
                                                                        HotSpotMode="PostBack" PostBackValue="TH14" />
                                                                    <asp:RectangleHotSpot Left="118" Top="275" Right="135" Bottom="292" AlternateText="TH13"
                                                                        HotSpotMode="PostBack" PostBackValue="TH13" />
                                                                    <asp:RectangleHotSpot Left="112" Top="324" Right="129" Bottom="341" AlternateText="TH12"
                                                                        HotSpotMode="PostBack" PostBackValue="TH12" />
                                                                    <asp:RectangleHotSpot Left="108" Top="370" Right="125" Bottom="387" AlternateText="TH11"
                                                                        HotSpotMode="PostBack" PostBackValue="TH11" />
                                                                    <asp:RectangleHotSpot Left="106" Top="385" Right="123" Bottom="402" AlternateText="TH10"
                                                                        HotSpotMode="PostBack" PostBackValue="TH10" />
                                                                    <asp:RectangleHotSpot Left="86" Top="449" Right="103" Bottom="466" AlternateText="TH9"
                                                                        HotSpotMode="PostBack" PostBackValue="TH9" />
                                                                    <asp:RectangleHotSpot Left="78" Top="491" Right="95" Bottom="508" AlternateText="TH8"
                                                                        HotSpotMode="PostBack" PostBackValue="TH8" />
                                                                    <asp:RectangleHotSpot Left="91" Top="507" Right="108" Bottom="521" AlternateText="TH7"
                                                                        HotSpotMode="PostBack" PostBackValue="TH7" />
                                                                    <asp:RectangleHotSpot Left="78" Top="505" Right="95" Bottom="519" AlternateText="TH6"
                                                                        HotSpotMode="PostBack" PostBackValue="TH6" />
                                                                    <asp:RectangleHotSpot Left="78" Top="516" Right="97" Bottom="533" AlternateText="TH5"
                                                                        HotSpotMode="PostBack" PostBackValue="TH5" />
                                                                    <asp:RectangleHotSpot Left="74" Top="535" Right="93" Bottom="552" AlternateText="TH4"
                                                                        HotSpotMode="PostBack" PostBackValue="TH4" />
                                                                    <asp:RectangleHotSpot Left="70" Top="576" Right="89" Bottom="593" AlternateText="TH3"
                                                                        HotSpotMode="PostBack" PostBackValue="TH3" />
                                                                    <asp:RectangleHotSpot Left="66" Top="592" Right="85" Bottom="609" AlternateText="TH2"
                                                                        HotSpotMode="PostBack" PostBackValue="TH2" />
                                                                    <asp:RectangleHotSpot Left="54" Top="635" Right="73" Bottom="652" AlternateText="TH1"
                                                                        HotSpotMode="PostBack" PostBackValue="TH1" />
                                                                    <asp:RectangleHotSpot Left="261" Top="462" Right="280" Bottom="479" AlternateText="TH23"
                                                                        HotSpotMode="PostBack" PostBackValue="TH23" />
                                                                    <asp:RectangleHotSpot Left="284" Top="478" Right="303" Bottom="495" AlternateText="TH22"
                                                                        HotSpotMode="PostBack" PostBackValue="TH22" />
                                                                    <asp:RectangleHotSpot Left="291" Top="490" Right="308" Bottom="504" AlternateText="TH21"
                                                                        HotSpotMode="PostBack" PostBackValue="TH21" />
                                                                    <asp:RectangleHotSpot Left="298" Top="465" Right="317" Bottom="482" AlternateText="TH20"
                                                                        HotSpotMode="PostBack" PostBackValue="TH20" />
                                                                    <asp:RectangleHotSpot Left="310" Top="488" Right="329" Bottom="506" AlternateText="TH19"
                                                                        HotSpotMode="PostBack" PostBackValue="TH19" />
                                                                    <asp:RectangleHotSpot Left="308" Top="501" Right="325" Bottom="517" AlternateText="TH18"
                                                                        HotSpotMode="PostBack" PostBackValue="TH18" />
                                                                    <asp:RectangleHotSpot Left="300" Top="312" Right="317" Bottom="528" AlternateText="TH17"
                                                                        HotSpotMode="PostBack" PostBackValue="TH17" />
                                                                    <asp:RectangleHotSpot Left="314" Top="516" Right="331" Bottom="535" AlternateText="TH16"
                                                                        HotSpotMode="PostBack" PostBackValue="TH16" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="GALLBLADDER" ImageUrl="~/images/GALLBLADDER FOOT SHAOYANG.gif"
                                                                    OnClick="Image_Click" runat="server">
                                                                    <asp:RectangleHotSpot Left="121" Top="62" Right="138" Bottom="77" AlternateText="GB17"
                                                                        HotSpotMode="PostBack" PostBackValue="GB17" />
                                                                    <asp:RectangleHotSpot Left="163" Top="58" Right="180" Bottom="75" AlternateText="GB16"
                                                                        HotSpotMode="PostBack" PostBackValue="GB16" />
                                                                    <asp:RectangleHotSpot Left="202" Top="72" Right="219" Bottom="89" AlternateText="GB15"
                                                                        HotSpotMode="PostBack" PostBackValue="GB15" />
                                                                    <asp:RectangleHotSpot Left="224" Top="116" Right="241" Bottom="133" AlternateText="GB14"
                                                                        HotSpotMode="PostBack" PostBackValue="GB14" />
                                                                    <asp:RectangleHotSpot Left="183" Top="73" Right="200" Bottom="90" AlternateText="GB13"
                                                                        HotSpotMode="PostBack" PostBackValue="GB13" />
                                                                    <asp:RectangleHotSpot Left="159" Top="113" Right="176" Bottom="130" AlternateText="GB5"
                                                                        HotSpotMode="PostBack" PostBackValue="GB5" />
                                                                    <asp:RectangleHotSpot Left="183" Top="73" Right="200" Bottom="90" AlternateText="GB13"
                                                                        HotSpotMode="PostBack" PostBackValue="GB13" />
                                                                    <asp:RectangleHotSpot Left="87" Top="76" Right="104" Bottom="93" AlternateText="GB18"
                                                                        HotSpotMode="PostBack" PostBackValue="GB18" />
                                                                    <asp:RectangleHotSpot Left="167" Top="92" Right="184" Bottom="109" AlternateText="GB4"
                                                                        HotSpotMode="PostBack" PostBackValue="GB4" />
                                                                    <asp:RectangleHotSpot Left="110" Top="106" Right="127" Bottom="123" AlternateText="GB8"
                                                                        HotSpotMode="PostBack" PostBackValue="GB8" />
                                                                    <asp:RectangleHotSpot Left="92" Top="115" Right="109" Bottom="132" AlternateText="GB9"
                                                                        HotSpotMode="PostBack" PostBackValue="GB9" />
                                                                    <asp:RectangleHotSpot Left="83" Top="149" Right="100" Bottom="166" AlternateText="GB11"
                                                                        HotSpotMode="PostBack" PostBackValue="GB11" />
                                                                    <asp:RectangleHotSpot Left="59" Top="158" Right="76" Bottom="175" AlternateText="GB10"
                                                                        HotSpotMode="PostBack" PostBackValue="GB10" />
                                                                    <asp:RectangleHotSpot Left="90" Top="171" Right="107" Bottom="188" AlternateText="GB19"
                                                                        HotSpotMode="PostBack" PostBackValue="GB19" />
                                                                    <asp:RectangleHotSpot Left="99" Top="195" Right="116" Bottom="212" AlternateText="GB12"
                                                                        HotSpotMode="PostBack" PostBackValue="GB12" />
                                                                    <asp:RectangleHotSpot Left="83" Top="211" Right="100" Bottom="228" AlternateText="GB20"
                                                                        HotSpotMode="PostBack" PostBackValue="GB20" />
                                                                    <asp:RectangleHotSpot Left="153" Top="128" Right="170" Bottom="145" AlternateText="GB6"
                                                                        HotSpotMode="PostBack" PostBackValue="GB6" />
                                                                    <asp:RectangleHotSpot Left="141" Top="142" Right="158" Bottom="159" AlternateText="GB7"
                                                                        HotSpotMode="PostBack" PostBackValue="GB7" />
                                                                    <asp:RectangleHotSpot Left="164" Top="158" Right="181" Bottom="175" AlternateText="GB3"
                                                                        HotSpotMode="PostBack" PostBackValue="GB3" />
                                                                    <asp:RectangleHotSpot Left="196" Top="162" Right="213" Bottom="179" AlternateText="GB1"
                                                                        HotSpotMode="PostBack" PostBackValue="GB1" />
                                                                    <asp:RectangleHotSpot Left="153" Top="192" Right="170" Bottom="209" AlternateText="GB2"
                                                                        HotSpotMode="PostBack" PostBackValue="GB2" />
                                                                    <asp:RectangleHotSpot Left="111" Top="349" Right="128" Bottom="366" AlternateText="GB21"
                                                                        HotSpotMode="PostBack" PostBackValue="GB21" />
                                                                    <asp:RectangleHotSpot Left="150" Top="493" Right="167" Bottom="510" AlternateText="GB22"
                                                                        HotSpotMode="PostBack" PostBackValue="GB22" />
                                                                    <asp:RectangleHotSpot Left="182" Top="505" Right="199" Bottom="522" AlternateText="GB23"
                                                                        HotSpotMode="PostBack" PostBackValue="GB23" />
                                                                    <asp:RectangleHotSpot Left="271" Top="582" Right="288" Bottom="599" AlternateText="GB24"
                                                                        HotSpotMode="PostBack" PostBackValue="GB24" />
                                                                    <asp:RectangleHotSpot Left="211" Top="694" Right="228" Bottom="711" AlternateText="GB25"
                                                                        HotSpotMode="PostBack" PostBackValue="GB25" />
                                                                    <asp:RectangleHotSpot Left="238" Top="726" Right="255" Bottom="743" AlternateText="GB26"
                                                                        HotSpotMode="PostBack" PostBackValue="GB26" />
                                                                    <asp:RectangleHotSpot Left="268" Top="807" Right="285" Bottom="824" AlternateText="GB27"
                                                                        HotSpotMode="PostBack" PostBackValue="GB27" />
                                                                    <asp:RectangleHotSpot Left="271" Top="827" Right="288" Bottom="844" AlternateText="GB28"
                                                                        HotSpotMode="PostBack" PostBackValue="GB28" />
                                                                    <asp:RectangleHotSpot Left="235" Top="893" Right="252" Bottom="910" AlternateText="GB29"
                                                                        HotSpotMode="PostBack" PostBackValue="GB29" />
                                                                    <asp:RectangleHotSpot Left="112" Top="928" Right="129" Bottom="945" AlternateText="GB30"
                                                                        HotSpotMode="PostBack" PostBackValue="GB30" />
                                                                    <asp:RectangleHotSpot Left="613" Top="116" Right="630" Bottom="133" AlternateText="GB30"
                                                                        HotSpotMode="PostBack" PostBackValue="GB30" />
                                                                    <asp:RectangleHotSpot Left="651" Top="280" Right="668" Bottom="297" AlternateText="GB31"
                                                                        HotSpotMode="PostBack" PostBackValue="GB31" />
                                                                    <asp:RectangleHotSpot Left="648" Top="305" Right="665" Bottom="322" AlternateText="GB32"
                                                                        HotSpotMode="PostBack" PostBackValue="GB32" />
                                                                    <asp:RectangleHotSpot Left="632" Top="405" Right="649" Bottom="422" AlternateText="GB33"
                                                                        HotSpotMode="PostBack" PostBackValue="GB33" />
                                                                    <asp:RectangleHotSpot Left="640" Top="445" Right="657" Bottom="462" AlternateText="GB34"
                                                                        HotSpotMode="PostBack" PostBackValue="GB34" />
                                                                    <asp:RectangleHotSpot Left="624" Top="573" Right="641" Bottom="590" AlternateText="GB35"
                                                                        HotSpotMode="PostBack" PostBackValue="GB35" />
                                                                    <asp:RectangleHotSpot Left="639" Top="573" Right="653" Bottom="590" AlternateText="GB36"
                                                                        HotSpotMode="PostBack" PostBackValue="GB36" />
                                                                    <asp:RectangleHotSpot Left="633" Top="609" Right="650" Bottom="627" AlternateText="GB37"
                                                                        HotSpotMode="PostBack" PostBackValue="GB37" />
                                                                    <asp:RectangleHotSpot Left="639" Top="624" Right="656" Bottom="642" AlternateText="GB38"
                                                                        HotSpotMode="PostBack" PostBackValue="GB38" />
                                                                    <asp:RectangleHotSpot Left="631" Top="643" Right="648" Bottom="661" AlternateText="GB39"
                                                                        HotSpotMode="PostBack" PostBackValue="GB39" />
                                                                    <asp:RectangleHotSpot Left="644" Top="697" Right="661" Bottom="715" AlternateText="GB40"
                                                                        HotSpotMode="PostBack" PostBackValue="GB40" />
                                                                    <asp:RectangleHotSpot Left="697" Top="732" Right="714" Bottom="750" AlternateText="GB41"
                                                                        HotSpotMode="PostBack" PostBackValue="GB41" />
                                                                    <asp:RectangleHotSpot Left="710" Top="739" Right="727" Bottom="757" AlternateText="GB42"
                                                                        HotSpotMode="PostBack" PostBackValue="GB42" />
                                                                    <asp:RectangleHotSpot Left="726" Top="741" Right="743" Bottom="759" AlternateText="GB43"
                                                                        HotSpotMode="PostBack" PostBackValue="GB43" />
                                                                    <asp:RectangleHotSpot Left="751" Top="749" Right="768" Bottom="767" AlternateText="GB44"
                                                                        HotSpotMode="PostBack" PostBackValue="GB44" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="LIVER" ImageUrl="~/images/LIVER FOOT JUEYIN.gif"
                                                                    OnClick="Image_Click" runat="server">
                                                                    <asp:RectangleHotSpot Left="177" Top="181" Right="196" Bottom="198" AlternateText="LV14"
                                                                        HotSpotMode="PostBack" PostBackValue="LV14" />
                                                                    <asp:RectangleHotSpot Left="125" Top="261" Right="144" Bottom="278" AlternateText="LV13"
                                                                        HotSpotMode="PostBack" PostBackValue="LV13" />
                                                                    <asp:RectangleHotSpot Left="212" Top="393" Right="231" Bottom="410" AlternateText="LV12"
                                                                        HotSpotMode="PostBack" PostBackValue="LV12" />
                                                                    <asp:RectangleHotSpot Left="211" Top="422" Right="230" Bottom="439" AlternateText="LV11"
                                                                        HotSpotMode="PostBack" PostBackValue="LV11" />
                                                                    <asp:RectangleHotSpot Left="212" Top="454" Right="231" Bottom="462" AlternateText="LV10"
                                                                        HotSpotMode="PostBack" PostBackValue="LV10" />
                                                                    <asp:RectangleHotSpot Left="223" Top="564" Right="242" Bottom="581" AlternateText="LV9"
                                                                        HotSpotMode="PostBack" PostBackValue="LV9" />
                                                                    <asp:RectangleHotSpot Left="220" Top="622" Right="239" Bottom="639" AlternateText="LV8"
                                                                        HotSpotMode="PostBack" PostBackValue="LV8" />
                                                                    <asp:RectangleHotSpot Left="224" Top="651" Right="243" Bottom="668" AlternateText="LV7"
                                                                        HotSpotMode="PostBack" PostBackValue="LV7" />
                                                                    <asp:RectangleHotSpot Left="261" Top="739" Right="280" Bottom="756" AlternateText="LV6"
                                                                        HotSpotMode="PostBack" PostBackValue="LV6" />
                                                                    <asp:RectangleHotSpot Left="262" Top="766" Right="281" Bottom="783" AlternateText="LV5"
                                                                        HotSpotMode="PostBack" PostBackValue="LV5" />
                                                                    <asp:RectangleHotSpot Left="277" Top="844" Right="296" Bottom="861" AlternateText="LV4"
                                                                        HotSpotMode="PostBack" PostBackValue="LV4" />
                                                                    <asp:RectangleHotSpot Left="327" Top="881" Right="345" Bottom="895" AlternateText="LV3"
                                                                        HotSpotMode="PostBack" PostBackValue="LV3" />
                                                                    <asp:RectangleHotSpot Left="342" Top="888" Right="360" Bottom="903" AlternateText="LV2"
                                                                        HotSpotMode="PostBack" PostBackValue="LV2" />
                                                                    <asp:RectangleHotSpot Left="67" Top="919" Right="86" Bottom="931" AlternateText="LV2"
                                                                        HotSpotMode="PostBack" PostBackValue="LV2" />
                                                                    <asp:RectangleHotSpot Left="73" Top="903" Right="92" Bottom="920" AlternateText="LV3"
                                                                        HotSpotMode="PostBack" PostBackValue="LV3" />
                                                                    <asp:RectangleHotSpot Left="86" Top="847" Right="105" Bottom="864" AlternateText="LV4"
                                                                        HotSpotMode="PostBack" PostBackValue="LV4" />
                                                                    <asp:RectangleHotSpot Left="99" Top="760" Right="118" Bottom="777" AlternateText="LV5"
                                                                        HotSpotMode="PostBack" PostBackValue="LV5" />
                                                                    <asp:RectangleHotSpot Left="105" Top="734" Right="124" Bottom="751" AlternateText="LV6"
                                                                        HotSpotMode="PostBack" PostBackValue="LV6" />
                                                                    <asp:RectangleHotSpot Left="133" Top="648" Right="152" Bottom="665" AlternateText="LV7"
                                                                        HotSpotMode="PostBack" PostBackValue="LV7" />
                                                                    <asp:RectangleHotSpot Left="142" Top="621" Right="161" Bottom="638" AlternateText="LV8"
                                                                        HotSpotMode="PostBack" PostBackValue="LV8" />
                                                                    <asp:RectangleHotSpot Left="154" Top="563" Right="173" Bottom="580" AlternateText="LV9"
                                                                        HotSpotMode="PostBack" PostBackValue="LV9" />
                                                                    <asp:RectangleHotSpot Left="178" Top="445" Right="197" Bottom="462" AlternateText="LV10"
                                                                        HotSpotMode="PostBack" PostBackValue="LV10" />
                                                                    <asp:RectangleHotSpot Left="177" Top="422" Right="196" Bottom="439" AlternateText="LV11"
                                                                        HotSpotMode="PostBack" PostBackValue="LV11" />
                                                                    <asp:RectangleHotSpot Left="179" Top="392" Right="198" Bottom="409" AlternateText="LV12"
                                                                        HotSpotMode="PostBack" PostBackValue="LV12" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="REN" ImageUrl="~/images/REN CONCEPTION.gif" runat="server"
                                                                    OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="142" Top="137" Right="161" Bottom="155" AlternateText="CV24"
                                                                        HotSpotMode="PostBack" PostBackValue="CV24" />
                                                                    <asp:RectangleHotSpot Left="140" Top="160" Right="159" Bottom="178" AlternateText="CV23"
                                                                        HotSpotMode="PostBack" PostBackValue="CV23" />
                                                                    <asp:RectangleHotSpot Left="140" Top="206" Right="159" Bottom="221" AlternateText="CV22"
                                                                        HotSpotMode="PostBack" PostBackValue="CV22" />
                                                                    <asp:RectangleHotSpot Left="139" Top="217" Right="157" Bottom="233" AlternateText="CV21"
                                                                        HotSpotMode="PostBack" PostBackValue="CV21" />
                                                                    <asp:RectangleHotSpot Left="141" Top="229" Right="159" Bottom="245" AlternateText="CV20"
                                                                        HotSpotMode="PostBack" PostBackValue="CV20" />
                                                                    <asp:RectangleHotSpot Left="139" Top="246" Right="157" Bottom="262" AlternateText="CV19"
                                                                        HotSpotMode="PostBack" PostBackValue="CV19" />
                                                                    <asp:RectangleHotSpot Left="141" Top="260" Right="159" Bottom="273" AlternateText="CV18"
                                                                        HotSpotMode="PostBack" PostBackValue="CV18" />
                                                                    <asp:RectangleHotSpot Left="140" Top="275" Right="159" Bottom="290" AlternateText="CV17"
                                                                        HotSpotMode="PostBack" PostBackValue="CV17" />
                                                                    <asp:RectangleHotSpot Left="140" Top="292" Right="159" Bottom="307" AlternateText="CV16"
                                                                        HotSpotMode="PostBack" PostBackValue="CV16" />
                                                                    <asp:RectangleHotSpot Left="140" Top="321" Right="159" Bottom="336" AlternateText="CV15"
                                                                        HotSpotMode="PostBack" PostBackValue="CV15" />
                                                                    <asp:RectangleHotSpot Left="140" Top="337" Right="159" Bottom="352" AlternateText="CV14"
                                                                        HotSpotMode="PostBack" PostBackValue="CV14" />
                                                                    <asp:RectangleHotSpot Left="140" Top="355" Right="159" Bottom="370" AlternateText="CV13"
                                                                        HotSpotMode="PostBack" PostBackValue="CV13" />
                                                                    <asp:RectangleHotSpot Left="139" Top="371" Right="158" Bottom="386" AlternateText="CV12"
                                                                        HotSpotMode="PostBack" PostBackValue="CV12" />
                                                                    <asp:RectangleHotSpot Left="139" Top="385" Right="158" Bottom="400" AlternateText="CV11"
                                                                        HotSpotMode="PostBack" PostBackValue="CV11" />
                                                                    <asp:RectangleHotSpot Left="139" Top="399" Right="159" Bottom="414" AlternateText="CV10"
                                                                        HotSpotMode="PostBack" PostBackValue="CV10" />
                                                                    <asp:RectangleHotSpot Left="140" Top="413" Right="159" Bottom="428" AlternateText="CV9"
                                                                        HotSpotMode="PostBack" PostBackValue="CV9" />
                                                                    <asp:RectangleHotSpot Left="140" Top="425" Right="159" Bottom="440" AlternateText="CV8"
                                                                        HotSpotMode="PostBack" PostBackValue="CV8" />
                                                                    <asp:RectangleHotSpot Left="140" Top="440" Right="159" Bottom="455" AlternateText="CV7"
                                                                        HotSpotMode="PostBack" PostBackValue="CV7" />
                                                                    <asp:RectangleHotSpot Left="140" Top="451" Right="159" Bottom="462" AlternateText="CV6"
                                                                        HotSpotMode="PostBack" PostBackValue="CV6" />
                                                                    <asp:RectangleHotSpot Left="141" Top="459" Right="160" Bottom="472" AlternateText="CV5"
                                                                        HotSpotMode="PostBack" PostBackValue="CV5" />
                                                                    <asp:RectangleHotSpot Left="140" Top="474" Right="159" Bottom="488" AlternateText="CV4"
                                                                        HotSpotMode="PostBack" PostBackValue="CV4" />
                                                                    <asp:RectangleHotSpot Left="140" Top="487" Right="159" Bottom="501" AlternateText="CV3"
                                                                        HotSpotMode="PostBack" PostBackValue="CV3" />
                                                                    <asp:RectangleHotSpot Left="139" Top="502" Right="158" Bottom="516" AlternateText="CV2"
                                                                        HotSpotMode="PostBack" PostBackValue="CV2" />
                                                                    <asp:RectangleHotSpot Left="297" Top="592" Right="318" Bottom="609" AlternateText="CV1"
                                                                        HotSpotMode="PostBack" PostBackValue="CV1" />
                                                                    <asp:RectangleHotSpot Left="286" Top="81" Right="307" Bottom="96" AlternateText="CV1"
                                                                        HotSpotMode="PostBack" PostBackValue="CV1" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="DU" ImageUrl="~/images/DU GOVERNING.gif" runat="server"
                                                                    OnClick="Image_Click">
                                                                    <asp:RectangleHotSpot Left="168" Top="43" Right="185" Bottom="60" AlternateText="DU14"
                                                                        HotSpotMode="PostBack" PostBackValue="DU14" />
                                                                    <asp:RectangleHotSpot Left="167" Top="60" Right="184" Bottom="77" AlternateText="DU13"
                                                                        HotSpotMode="PostBack" PostBackValue="DU13" />
                                                                    <asp:RectangleHotSpot Left="166" Top="99" Right="183" Bottom="116" AlternateText="DU12"
                                                                        HotSpotMode="PostBack" PostBackValue="DU12" />
                                                                    <asp:RectangleHotSpot Left="166" Top="147" Right="183" Bottom="164" AlternateText="DU11"
                                                                        HotSpotMode="PostBack" PostBackValue="DU11" />
                                                                    <asp:RectangleHotSpot Left="166" Top="167" Right="183" Bottom="184" AlternateText="DU10"
                                                                        HotSpotMode="PostBack" PostBackValue="DU10" />
                                                                    <asp:RectangleHotSpot Left="167" Top="188" Right="184" Bottom="205" AlternateText="DU9"
                                                                        HotSpotMode="PostBack" PostBackValue="DU9" />
                                                                    <asp:RectangleHotSpot Left="165" Top="224" Right="182" Bottom="241" AlternateText="DU8"
                                                                        HotSpotMode="PostBack" PostBackValue="DU8" />
                                                                    <asp:RectangleHotSpot Left="169" Top="246" Right="186" Bottom="263" AlternateText="DU7"
                                                                        HotSpotMode="PostBack" PostBackValue="DU7" />
                                                                    <asp:RectangleHotSpot Left="165" Top="269" Right="182" Bottom="286" AlternateText="DU6"
                                                                        HotSpotMode="PostBack" PostBackValue="DU6" />
                                                                    <asp:RectangleHotSpot Left="168" Top="325" Right="185" Bottom="342" AlternateText="DU5"
                                                                        HotSpotMode="PostBack" PostBackValue="DU5" />
                                                                    <asp:RectangleHotSpot Left="167" Top="344" Right="184" Bottom="361" AlternateText="DU4"
                                                                        HotSpotMode="PostBack" PostBackValue="DU4" />
                                                                    <asp:RectangleHotSpot Left="167" Top="376" Right="184" Bottom="393" AlternateText="DU3"
                                                                        HotSpotMode="PostBack" PostBackValue="DU3" />
                                                                    <asp:RectangleHotSpot Left="166" Top="428" Right="183" Bottom="445" AlternateText="DU2"
                                                                        HotSpotMode="PostBack" PostBackValue="DU2" />
                                                                    <asp:RectangleHotSpot Left="167" Top="487" Right="184" Bottom="504" AlternateText="DU1"
                                                                        HotSpotMode="PostBack" PostBackValue="DU1" />
                                                                    <asp:RectangleHotSpot Left="542" Top="34" Right="559" Bottom="51" AlternateText="DU20"
                                                                        HotSpotMode="PostBack" PostBackValue="DU20" />
                                                                    <asp:RectangleHotSpot Left="544" Top="60" Right="561" Bottom="77" AlternateText="DU19"
                                                                        HotSpotMode="PostBack" PostBackValue="DU19" />
                                                                    <asp:RectangleHotSpot Left="541" Top="90" Right="558" Bottom="107" AlternateText="DU18"
                                                                        HotSpotMode="PostBack" PostBackValue="DU18" />
                                                                    <asp:RectangleHotSpot Left="542" Top="113" Right="559" Bottom="130" AlternateText="DU17"
                                                                        HotSpotMode="PostBack" PostBackValue="DU17" />
                                                                    <asp:RectangleHotSpot Left="541" Top="143" Right="558" Bottom="160" AlternateText="DU16"
                                                                        HotSpotMode="PostBack" PostBackValue="DU16" />
                                                                    <asp:RectangleHotSpot Left="542" Top="159" Right="559" Bottom="176" AlternateText="DU15"
                                                                        HotSpotMode="PostBack" PostBackValue="DU15" />
                                                                    <asp:RectangleHotSpot Left="541" Top="362" Right="558" Bottom="379" AlternateText="DU19"
                                                                        HotSpotMode="PostBack" PostBackValue="DU19" />
                                                                    <asp:RectangleHotSpot Left="541" Top="385" Right="558" Bottom="402" AlternateText="DU20"
                                                                        HotSpotMode="PostBack" PostBackValue="DU20" />
                                                                    <asp:RectangleHotSpot Left="540" Top="411" Right="557" Bottom="428" AlternateText="DU21"
                                                                        HotSpotMode="PostBack" PostBackValue="DU21" />
                                                                    <asp:RectangleHotSpot Left="540" Top="435" Right="557" Bottom="452" AlternateText="DU22"
                                                                        HotSpotMode="PostBack" PostBackValue="DU22" />
                                                                    <asp:RectangleHotSpot Left="541" Top="451" Right="558" Bottom="468" AlternateText="DU23"
                                                                        HotSpotMode="PostBack" PostBackValue="DU23" />
                                                                    <asp:RectangleHotSpot Left="540" Top="468" Right="556" Bottom="480" AlternateText="DU24"
                                                                        HotSpotMode="PostBack" PostBackValue="DU24" />
                                                                </asp:ImageMap>
                                                                <asp:ImageMap Visible="false" ID="EAR" ImageUrl="~/images/EAR ACUPUNTURE CHART.gif"
                                                                    OnClick="Image_Click" runat="server">
                                                                    <asp:RectangleHotSpot Left="298" Top="44" Right="316" Bottom="60" AlternateText="ear Apex"
                                                                        HotSpotMode="PostBack" PostBackValue="ear Apex" />
                                                                    <asp:RectangleHotSpot Left="344" Top="49" Right="361" Bottom="67" AlternateText="tonsil"
                                                                        HotSpotMode="PostBack" PostBackValue="tonsil" />
                                                                    <asp:RectangleHotSpot Left="241" Top="86" Right="252" Bottom="97" AlternateText="common cold"
                                                                        HotSpotMode="PostBack" PostBackValue="common cold" />
                                                                    <asp:RectangleHotSpot Left="175" Top="101" Right="186" Bottom="112" AlternateText="Hemorrhoids"
                                                                        HotSpotMode="PostBack" PostBackValue="Hemorrhoids" />
                                                                    <asp:RectangleHotSpot Left="236" Top="101" Right="247" Bottom="113" AlternateText="Proximal Segment Rectum"
                                                                        HotSpotMode="PostBack" PostBackValue="Proximal Segment Rectum" />
                                                                    <asp:RectangleHotSpot Left="263" Top="109" Right="274" Bottom="120" AlternateText="Lower Blood Pressure"
                                                                        HotSpotMode="PostBack" PostBackValue="Lower Blood Pressure" />
                                                                    <asp:RectangleHotSpot Left="315" Top="114" Right="326" Bottom="127" AlternateText="Heel"
                                                                        HotSpotMode="PostBack" PostBackValue="Heel" />
                                                                    <asp:RectangleHotSpot Left="387" Top="127" Right="398" Bottom="140" AlternateText="Toes"
                                                                        HotSpotMode="PostBack" PostBackValue="Toes" />
                                                                    <asp:RectangleHotSpot Left="423" Top="150" Right="435" Bottom="163" AlternateText="Appendix"
                                                                        HotSpotMode="PostBack" PostBackValue="Appendix" />
                                                                    <asp:RectangleHotSpot Left="215" Top="112" Right="227" Bottom="125" AlternateText="Externel Genitalia"
                                                                        HotSpotMode="PostBack" PostBackValue="Externel Genitalia" />
                                                                    <asp:RectangleHotSpot Left="197" Top="131" Right="210" Bottom="144" AlternateText="Urethra"
                                                                        HotSpotMode="PostBack" PostBackValue="Urethra" />
                                                                    <asp:RectangleHotSpot Left="231" Top="139" Right="244" Bottom="153" AlternateText="Uterus"
                                                                        HotSpotMode="PostBack" PostBackValue="Uterus" />
                                                                    <asp:RectangleHotSpot Left="336" Top="139" Right="350" Bottom="154" AlternateText="Ankle"
                                                                        HotSpotMode="PostBack" PostBackValue="Ankle" />
                                                                    <asp:RectangleHotSpot Left="504" Top="176" Right="518" Bottom="191" AlternateText="Liver Yang"
                                                                        HotSpotMode="PostBack" PostBackValue="Liver Yang" />
                                                                    <asp:RectangleHotSpot Left="181" Top="155" Right="196" Bottom="169" AlternateText="Distal Segment Rectum"
                                                                        HotSpotMode="PostBack" PostBackValue="Distal Segment Rectum" />
                                                                    <asp:RectangleHotSpot Left="159" Top="155" Right="174" Bottom="168" AlternateText="Sympathetic"
                                                                        HotSpotMode="PostBack" PostBackValue="Sympathetic" />
                                                                    <asp:RectangleHotSpot Left="115" Top="163" Right="130" Bottom="176" AlternateText="Externel Genitalia"
                                                                        HotSpotMode="PostBack" PostBackValue="Externel Genitalia" />
                                                                    <asp:RectangleHotSpot Left="234" Top="165" Right="250" Bottom="179" AlternateText="Adnexa"
                                                                        HotSpotMode="PostBack" PostBackValue="Adnexa" />
                                                                    <asp:RectangleHotSpot Left="272" Top="175" Right="287" Bottom="189" AlternateText="Wheezing"
                                                                        HotSpotMode="PostBack" PostBackValue="Wheezing" />
                                                                    <asp:RectangleHotSpot Left="319" Top="170" Right="334" Bottom="186" AlternateText="Neurogate"
                                                                        HotSpotMode="PostBack" PostBackValue="Neurogate" />
                                                                    <asp:RectangleHotSpot Left="377" Top="177" Right="392" Bottom="193" AlternateText="Knee Joint"
                                                                        HotSpotMode="PostBack" PostBackValue="Knee Joint" />
                                                                    <asp:RectangleHotSpot Left="444" Top="180" Right="459" Bottom="196" AlternateText="Fingers"
                                                                        HotSpotMode="PostBack" PostBackValue="Fingers" />
                                                                    <asp:RectangleHotSpot Left="345" Top="181" Right="360" Bottom="197" AlternateText="Gastrocnemius"
                                                                        HotSpotMode="PostBack" PostBackValue="Gastrocnemius" />
                                                                    <asp:RectangleHotSpot Left="460" Top="188" Right="474" Bottom="204" AlternateText="Lesser Occipital Nerve"
                                                                        HotSpotMode="PostBack" PostBackValue="Lesser Occipital Nerve" />
                                                                    <asp:RectangleHotSpot Left="198" Top="201" Right="211" Bottom="217" AlternateText="Sciatic Nerve"
                                                                        HotSpotMode="PostBack" PostBackValue="Sciatic Nerve" />
                                                                    <asp:RectangleHotSpot Left="298" Top="205" Right="311" Bottom="221" AlternateText="Hepatitis"
                                                                        HotSpotMode="PostBack" PostBackValue="Hepatitis" />
                                                                    <asp:RectangleHotSpot Left="280" Top="226" Right="293" Bottom="242" AlternateText="Hip Joint"
                                                                        HotSpotMode="PostBack" PostBackValue="Hip Joint" />
                                                                    <asp:RectangleHotSpot Left="325" Top="234" Right="338" Bottom="250" AlternateText="Pelvic Cavity"
                                                                        HotSpotMode="PostBack" PostBackValue="Pelvic Cavity" />
                                                                    <asp:RectangleHotSpot Left="160" Top="224" Right="173" Bottom="240" AlternateText="Prostate"
                                                                        HotSpotMode="PostBack" PostBackValue="Prostate" />
                                                                    <asp:RectangleHotSpot Left="104" Top="252" Right="117" Bottom="268" AlternateText="Urethra"
                                                                        HotSpotMode="PostBack" PostBackValue="Urethra" />
                                                                    <asp:RectangleHotSpot Left="196" Top="261" Right="209" Bottom="277" AlternateText="Bladder"
                                                                        HotSpotMode="PostBack" PostBackValue="Bladder" />
                                                                    <asp:RectangleHotSpot Left="288" Top="264" Right="301" Bottom="280" AlternateText="Buttocks"
                                                                        HotSpotMode="PostBack" PostBackValue="Buttocks" />
                                                                    <asp:RectangleHotSpot Left="352" Top="274" Right="365" Bottom="290" AlternateText="Hol"
                                                                        HotSpotMode="PostBack" PostBackValue="Hol" />
                                                                    <asp:RectangleHotSpot Left="380" Top="229" Right="393" Bottom="245" AlternateText="Hip Joint"
                                                                        HotSpotMode="PostBack" PostBackValue="Hip Joint" />
                                                                    <asp:RectangleHotSpot Left="410" Top="246" Right="423" Bottom="262" AlternateText="ear Apex"
                                                                        HotSpotMode="PostBack" PostBackValue="ear Apex" />
                                                                    <asp:RectangleHotSpot Left="387" Top="285" Right="400" Bottom="301" AlternateText="Abdomen"
                                                                        HotSpotMode="PostBack" PostBackValue="Abdomen" />
                                                                    <asp:RectangleHotSpot Left="351" Top="239" Right="364" Bottom="255" AlternateText="Popliteal Fossa"
                                                                        HotSpotMode="PostBack" PostBackValue="Popliteal Fossa" />
                                                                    <asp:RectangleHotSpot Left="437" Top="261" Right="450" Bottom="275" AlternateText="Lower Abdomen"
                                                                        HotSpotMode="PostBack" PostBackValue="Lower Abdomen" />
                                                                    <asp:RectangleHotSpot Left="463" Top="274" Right="476" Bottom="287" AlternateText="Wrist"
                                                                        HotSpotMode="PostBack" PostBackValue="Wrist" />
                                                                    <asp:RectangleHotSpot Left="453" Top="263" Right="466" Bottom="276" AlternateText="Allergy"
                                                                        HotSpotMode="PostBack" PostBackValue="Allergy" />
                                                                    <asp:RectangleHotSpot Left="533" Top="290" Right="546" Bottom="303" AlternateText="Helix"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix" />
                                                                    <asp:RectangleHotSpot Left="530" Top="306" Right="543" Bottom="319" AlternateText="Liver Yang"
                                                                        HotSpotMode="PostBack" PostBackValue="Liver Yang" />
                                                                    <asp:RectangleHotSpot Left="169" Top="288" Right="182" Bottom="301" AlternateText="Blood Base"
                                                                        HotSpotMode="PostBack" PostBackValue="Blood Base" />
                                                                    <asp:RectangleHotSpot Left="240" Top="299" Right="253" Bottom="312" AlternateText="Ureter"
                                                                        HotSpotMode="PostBack" PostBackValue="Ureter" />
                                                                    <asp:RectangleHotSpot Left="114" Top="312" Right="127" Bottom="325" AlternateText="Anus"
                                                                        HotSpotMode="PostBack" PostBackValue="Anus" />
                                                                    <asp:RectangleHotSpot Left="201" Top="304" Right="214" Bottom="319" AlternateText="Colon"
                                                                        HotSpotMode="PostBack" PostBackValue="Colon" />
                                                                    <asp:RectangleHotSpot Left="279" Top="314" Right="292" Bottom="329" AlternateText="Kidney"
                                                                        HotSpotMode="PostBack" PostBackValue="Kidney" />
                                                                    <asp:RectangleHotSpot Left="417" Top="340" Right="430" Bottom="355" AlternateText="Lumbar Vertebrae"
                                                                        HotSpotMode="PostBack" PostBackValue="Lumbar Vertebrae" />
                                                                    <asp:RectangleHotSpot Left="330" Top="335" Right="343" Bottom="350" AlternateText="Pancreas/Gall Bladder"
                                                                        HotSpotMode="PostBack" PostBackValue="Pancreas/Gall Bladder" />
                                                                    <asp:RectangleHotSpot Left="203" Top="341" Right="216" Bottom="356" AlternateText="Large Intestine"
                                                                        HotSpotMode="PostBack" PostBackValue="Large Intestine" />
                                                                    <asp:RectangleHotSpot Left="379" Top="342" Right="392" Bottom="357" AlternateText="Lumbago"
                                                                        HotSpotMode="PostBack" PostBackValue="Lumbago" />
                                                                    <asp:RectangleHotSpot Left="244" Top="337" Right="257" Bottom="352" AlternateText="Colon"
                                                                        HotSpotMode="PostBack" PostBackValue="Colon" />
                                                                    <asp:RectangleHotSpot Left="275" Top="352" Right="288" Bottom="367" AlternateText="Ascites"
                                                                        HotSpotMode="PostBack" PostBackValue="Ascites" />
                                                                    <asp:RectangleHotSpot Left="310" Top="361" Right="323" Bottom="376" AlternateText="Pancreas"
                                                                        HotSpotMode="PostBack" PostBackValue="Pancreas" />
                                                                    <asp:RectangleHotSpot Left="487" Top="363" Right="500" Bottom="378" AlternateText="Elbow"
                                                                        HotSpotMode="PostBack" PostBackValue="Elbow" />
                                                                    <asp:RectangleHotSpot Left="367" Top="373" Right="380" Bottom="388" AlternateText="Liver"
                                                                        HotSpotMode="PostBack" PostBackValue="Liver" />
                                                                    <asp:RectangleHotSpot Left="432" Top="392" Right="445" Bottom="407" AlternateText="Abdomen"
                                                                        HotSpotMode="PostBack" PostBackValue="Abdomen" />
                                                                    <asp:RectangleHotSpot Left="472" Top="359" Right="485" Bottom="374" AlternateText="Abdominal Wall"
                                                                        HotSpotMode="PostBack" PostBackValue="Abdominal Wall" />
                                                                    <asp:RectangleHotSpot Left="539" Top="408" Right="552" Bottom="423" AlternateText="Helix"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix" />
                                                                    <asp:RectangleHotSpot Left="134" Top="360" Right="146" Bottom="374" AlternateText="Distal Segment Rectum"
                                                                        HotSpotMode="PostBack" PostBackValue="Distal Segment Rectum" />
                                                                    <asp:RectangleHotSpot Left="245" Top="373" Right="259" Bottom="387" AlternateText="Appendix"
                                                                        HotSpotMode="PostBack" PostBackValue="Appendix" />
                                                                    <asp:RectangleHotSpot Left="276" Top="382" Right="289" Bottom="395" AlternateText="Small Intestine"
                                                                        HotSpotMode="PostBack" PostBackValue="Small Intestine" />
                                                                    <asp:RectangleHotSpot Left="308" Top="390" Right="322" Bottom="404" AlternateText="Duodenum"
                                                                        HotSpotMode="PostBack" PostBackValue="Duodenum" />
                                                                    <asp:RectangleHotSpot Left="342" Top="403" Right="355" Bottom="418" AlternateText="Protapse"
                                                                        HotSpotMode="PostBack" PostBackValue="Protapse" />
                                                                    <asp:RectangleHotSpot Left="491" Top="409" Right="505" Bottom="425" AlternateText="Appendix #2"
                                                                        HotSpotMode="PostBack" PostBackValue="Appendix #2" />
                                                                    <asp:RectangleHotSpot Left="112" Top="404" Right="125" Bottom="420" AlternateText="External Ear"
                                                                        HotSpotMode="PostBack" PostBackValue="External Ear" />
                                                                    <asp:RectangleHotSpot Left="242" Top="414" Right="255" Bottom="430" AlternateText="Diaphragm"
                                                                        HotSpotMode="PostBack" PostBackValue="Diaphragm" />
                                                                    <asp:RectangleHotSpot Left="256" Top="414" Right="267" Bottom="427" AlternateText="Mid Ear"
                                                                        HotSpotMode="PostBack" PostBackValue="Mid Ear" />
                                                                    <asp:RectangleHotSpot Left="285" Top="405" Right="297" Bottom="419" AlternateText="Nervous Dysfunction"
                                                                        HotSpotMode="PostBack" PostBackValue="Nervous Dysfunction" />
                                                                    <asp:RectangleHotSpot Left="336" Top="428" Right="350" Bottom="442" AlternateText="Stomach"
                                                                        HotSpotMode="PostBack" PostBackValue="Stomach" />
                                                                    <asp:RectangleHotSpot Left="538" Top="454" Right="552" Bottom="468" AlternateText="Tonsil #2"
                                                                        HotSpotMode="PostBack" PostBackValue="Tonsil #2" />
                                                                    <asp:RectangleHotSpot Left="131" Top="436" Right="144" Bottom="449" AlternateText="Heart"
                                                                        HotSpotMode="PostBack" PostBackValue="Heart" />
                                                                    <asp:RectangleHotSpot Left="250" Top="433" Right="264" Bottom="447" AlternateText="Branch"
                                                                        HotSpotMode="PostBack" PostBackValue="Branch" />
                                                                    <asp:RectangleHotSpot Left="377" Top="444" Right="391" Bottom="459" AlternateText="Relax Muscles"
                                                                        HotSpotMode="PostBack" PostBackValue="Relax Muscles" />
                                                                    <asp:RectangleHotSpot Left="441" Top="454" Right="454" Bottom="469" AlternateText="Thorax"
                                                                        HotSpotMode="PostBack" PostBackValue="Thorax" />
                                                                    <asp:RectangleHotSpot Left="493" Top="451" Right="506" Bottom="466" AlternateText="Shoulder"
                                                                        HotSpotMode="PostBack" PostBackValue="Shoulder" />
                                                                    <asp:RectangleHotSpot Left="258" Top="460" Right="271" Bottom="475" AlternateText="Esophagus"
                                                                        HotSpotMode="PostBack" PostBackValue="Esophagus" />
                                                                    <asp:RectangleHotSpot Left="311" Top="463" Right="326" Bottom="478" AlternateText="Pylorus"
                                                                        HotSpotMode="PostBack" PostBackValue="Pylorus" />
                                                                    <asp:RectangleHotSpot Left="200" Top="481" Right="215" Bottom="496" AlternateText="Lower Abdomen"
                                                                        HotSpotMode="PostBack" PostBackValue="Lower Abdomen" />
                                                                    <asp:RectangleHotSpot Left="170" Top="479" Right="182" Bottom="493" AlternateText="Thyroid #4"
                                                                        HotSpotMode="PostBack" PostBackValue="Thyroid #4" />
                                                                    <asp:RectangleHotSpot Left="227" Top="484" Right="239" Bottom="497" AlternateText="Mouth"
                                                                        HotSpotMode="PostBack" PostBackValue="Mouth" />
                                                                    <asp:RectangleHotSpot Left="287" Top="480" Right="299" Bottom="493" AlternateText="New Eye"
                                                                        HotSpotMode="PostBack" PostBackValue="New Eye" />
                                                                    <asp:RectangleHotSpot Left="484" Top="484" Right="496" Bottom="496" AlternateText="Axilla"
                                                                        HotSpotMode="PostBack" PostBackValue="Axilla" />
                                                                    <asp:RectangleHotSpot Left="128" Top="497" Right="140" Bottom="509" AlternateText="Thirst"
                                                                        HotSpotMode="PostBack" PostBackValue="Thirst" />
                                                                    <asp:RectangleHotSpot Left="163" Top="494" Right="175" Bottom="506" AlternateText="Throat"
                                                                        HotSpotMode="PostBack" PostBackValue="Throat" />
                                                                    <asp:RectangleHotSpot Left="264" Top="509" Right="279" Bottom="524" AlternateText="Bronchi"
                                                                        HotSpotMode="PostBack" PostBackValue="Bronchi" />
                                                                    <asp:RectangleHotSpot Left="303" Top="500" Right="318" Bottom="515" AlternateText="Upper Lung"
                                                                        HotSpotMode="PostBack" PostBackValue="Upper Lung" />
                                                                    <asp:RectangleHotSpot Left="336" Top="506" Right="351" Bottom="521" AlternateText="Lateral Lung"
                                                                        HotSpotMode="PostBack" PostBackValue="Lateral Lung" />
                                                                    <asp:RectangleHotSpot Left="375" Top="511" Right="390" Bottom="526" AlternateText="Spieen"
                                                                        HotSpotMode="PostBack" PostBackValue="Spieen" />
                                                                    <asp:RectangleHotSpot Left="479" Top="508" Right="494" Bottom="523" AlternateText="Shoulder Pain"
                                                                        HotSpotMode="PostBack" PostBackValue="Shoulder Pain" />
                                                                    <asp:RectangleHotSpot Left="480" Top="522" Right="495" Bottom="537" AlternateText="Chest Wall"
                                                                        HotSpotMode="PostBack" PostBackValue="Chest Wall" />
                                                                    <asp:RectangleHotSpot Left="169" Top="521" Right="184" Bottom="536" AlternateText="Clear Nose/Eyes"
                                                                        HotSpotMode="PostBack" PostBackValue="Clear Nose/Eyes" />
                                                                    <asp:RectangleHotSpot Left="236" Top="533" Right="251" Bottom="548" AlternateText="Trachea"
                                                                        HotSpotMode="PostBack" PostBackValue="Trachea" />
                                                                    <asp:RectangleHotSpot Left="180" Top="494" Right="195" Bottom="509" AlternateText="Tragus Apex"
                                                                        HotSpotMode="PostBack" PostBackValue="Tragus Apex" />
                                                                    <asp:RectangleHotSpot Left="303" Top="531" Right="318" Bottom="546" AlternateText="Heart"
                                                                        HotSpotMode="PostBack" PostBackValue="Heart" />
                                                                    <asp:RectangleHotSpot Left="129" Top="541" Right="144" Bottom="556" AlternateText="External Nose"
                                                                        HotSpotMode="PostBack" PostBackValue="External Nose" />
                                                                    <asp:RectangleHotSpot Left="519" Top="548" Right="534" Bottom="563" AlternateText="Helix #3"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix #3" />
                                                                    <asp:RectangleHotSpot Left="477" Top="544" Right="492" Bottom="559" AlternateText="Shoulder Joint"
                                                                        HotSpotMode="PostBack" PostBackValue="Shoulder Joint" />
                                                                    <asp:RectangleHotSpot Left="369" Top="561" Right="384" Bottom="576" AlternateText="Blood"
                                                                        HotSpotMode="PostBack" PostBackValue="Blood" />
                                                                    <asp:RectangleHotSpot Left="335" Top="560" Right="350" Bottom="575" AlternateText="Lateral Lung"
                                                                        HotSpotMode="PostBack" PostBackValue="Lateral Lung" />
                                                                    <asp:RectangleHotSpot Left="301" Top="565" Right="316" Bottom="580" AlternateText="Lower Lung"
                                                                        HotSpotMode="PostBack" PostBackValue="Lower Lung" />
                                                                    <asp:RectangleHotSpot Left="264" Top="557" Right="279" Bottom="572" AlternateText="Bronchi"
                                                                        HotSpotMode="PostBack" PostBackValue="Bronchi" />
                                                                    <asp:RectangleHotSpot Left="224" Top="573" Right="239" Bottom="588" AlternateText="Triple Burner"
                                                                        HotSpotMode="PostBack" PostBackValue="Triple Burner" />
                                                                    <asp:RectangleHotSpot Left="172" Top="545" Right="183" Bottom="558" AlternateText="Mid-Tragus"
                                                                        HotSpotMode="PostBack" PostBackValue="Mid-Tragus" />
                                                                    <asp:RectangleHotSpot Left="159" Top="588" Right="170" Bottom="601" AlternateText="Inner Nose"
                                                                        HotSpotMode="PostBack" PostBackValue="Inner Nose" />
                                                                    <asp:RectangleHotSpot Left="129" Top="589" Right="142" Bottom="602" AlternateText="Hunger"
                                                                        HotSpotMode="PostBack" PostBackValue="Hunger" />
                                                                    <asp:RectangleHotSpot Left="179" Top="589" Right="193" Bottom="603" AlternateText="Adrenal"
                                                                        HotSpotMode="PostBack" PostBackValue="Adrenal" />
                                                                    <asp:RectangleHotSpot Left="191" Top="588" Right="204" Bottom="601" AlternateText="Upper Abdomen"
                                                                        HotSpotMode="PostBack" PostBackValue="Upper Abdomen" />
                                                                    <asp:RectangleHotSpot Left="208" Top="624" Right="223" Bottom="639" AlternateText="Bronchiectasis"
                                                                        HotSpotMode="PostBack" PostBackValue="Bronchiectasis" />
                                                                    <asp:RectangleHotSpot Left="270" Top="626" Right="285" Bottom="641" AlternateText="Parotid Gland"
                                                                        HotSpotMode="PostBack" PostBackValue="Parotid Gland" />
                                                                    <asp:RectangleHotSpot Left="287" Top="619" Right="302" Bottom="634" AlternateText="Brain"
                                                                        HotSpotMode="PostBack" PostBackValue="Brain" />
                                                                    <asp:RectangleHotSpot Left="310" Top="615" Right="325" Bottom="630" AlternateText="Vertigo"
                                                                        HotSpotMode="PostBack" PostBackValue="Vertigo" />
                                                                    <asp:RectangleHotSpot Left="335" Top="613" Right="350" Bottom="628" AlternateText="Brain Stem"
                                                                        HotSpotMode="PostBack" PostBackValue="Brain Stem" />
                                                                    <asp:RectangleHotSpot Left="357" Top="613" Right="372" Bottom="628" AlternateText="Thyroid #2"
                                                                        HotSpotMode="PostBack" PostBackValue="Thyroid #2" />
                                                                    <asp:RectangleHotSpot Left="387" Top="611" Right="402" Bottom="626" AlternateText="Neck"
                                                                        HotSpotMode="PostBack" PostBackValue="Neck" />
                                                                    <asp:RectangleHotSpot Left="444" Top="626" Right="459" Bottom="641" AlternateText="Clavicle"
                                                                        HotSpotMode="PostBack" PostBackValue="Clavicle" />
                                                                    <asp:RectangleHotSpot Left="478" Top="651" Right="493" Bottom="666" AlternateText="Tonsil #3"
                                                                        HotSpotMode="PostBack" PostBackValue="Tonsil #3" />
                                                                    <asp:RectangleHotSpot Left="412" Top="660" Right="427" Bottom="675" AlternateText="Thyroid #1"
                                                                        HotSpotMode="PostBack" PostBackValue="Thyroid #1" />
                                                                    <asp:RectangleHotSpot Left="378" Top="671" Right="393" Bottom="686" AlternateText="Appendix #3"
                                                                        HotSpotMode="PostBack" PostBackValue="Appendix #3" />
                                                                    <asp:RectangleHotSpot Left="350" Top="634" Right="365" Bottom="649" AlternateText="Throat and Teeth"
                                                                        HotSpotMode="PostBack" PostBackValue="Throat and Teeth" />
                                                                    <asp:RectangleHotSpot Left="322" Top="641" Right="337" Bottom="656" AlternateText="Occiput"
                                                                        HotSpotMode="PostBack" PostBackValue="Occiput" />
                                                                    <asp:RectangleHotSpot Left="335" Top="629" Right="350" Bottom="644" AlternateText="Toothache"
                                                                        HotSpotMode="PostBack" PostBackValue="Toothache" />
                                                                    <asp:RectangleHotSpot Left="284" Top="631" Right="298" Bottom="641" AlternateText="Testicles"
                                                                        HotSpotMode="PostBack" PostBackValue="Testicles" />
                                                                    <asp:RectangleHotSpot Left="126" Top="652" Right="139" Bottom="667" AlternateText="Hypertension"
                                                                        HotSpotMode="PostBack" PostBackValue="Hypertension" />
                                                                    <asp:RectangleHotSpot Left="186" Top="646" Right="199" Bottom="661" AlternateText="Harmone"
                                                                        HotSpotMode="PostBack" PostBackValue="Harmone" />
                                                                    <asp:RectangleHotSpot Left="284" Top="640" Right="297" Bottom="655" AlternateText="ear Apex"
                                                                        HotSpotMode="PostBack" PostBackValue="ear Apex" />
                                                                    <asp:RectangleHotSpot Left="300" Top="642" Right="313" Bottom="657" AlternateText="Excitation"
                                                                        HotSpotMode="PostBack" PostBackValue="Excitation" />
                                                                    <asp:RectangleHotSpot Left="314" Top="652" Right="327" Bottom="663" AlternateText="Nerve"
                                                                        HotSpotMode="PostBack" PostBackValue="Nerve" />
                                                                    <asp:RectangleHotSpot Left="250" Top="657" Right="263" Bottom="670" AlternateText="Subcortex"
                                                                        HotSpotMode="PostBack" PostBackValue="Subcortex" />
                                                                    <asp:RectangleHotSpot Left="223" Top="674" Right="236" Bottom="687" AlternateText="Ovaries"
                                                                        HotSpotMode="PostBack" PostBackValue="Ovaries" />
                                                                    <asp:RectangleHotSpot Left="152" Top="665" Right="165" Bottom="678" AlternateText="ear Apex"
                                                                        HotSpotMode="PostBack" PostBackValue="ear Apex" />
                                                                    <asp:RectangleHotSpot Left="468" Top="671" Right="481" Bottom="684" AlternateText="Helix #4"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix #4" />
                                                                    <asp:RectangleHotSpot Left="409" Top="691" Right="422" Bottom="704" AlternateText="Nephritis"
                                                                        HotSpotMode="PostBack" PostBackValue="Nephritis" />
                                                                    <asp:RectangleHotSpot Left="316" Top="679" Right="329" Bottom="692" AlternateText="Lung"
                                                                        HotSpotMode="PostBack" PostBackValue="Lung" />
                                                                    <asp:RectangleHotSpot Left="295" Top="675" Right="308" Bottom="688" AlternateText="Pituitary"
                                                                        HotSpotMode="PostBack" PostBackValue="Pituitary" />
                                                                    <asp:RectangleHotSpot Left="285" Top="666" Right="298" Bottom="679" AlternateText="Temple"
                                                                        HotSpotMode="PostBack" PostBackValue="Temple" />
                                                                    <asp:RectangleHotSpot Left="142" Top="684" Right="155" Bottom="697" AlternateText="Vision #1"
                                                                        HotSpotMode="PostBack" PostBackValue="Vision #1" />
                                                                    <asp:RectangleHotSpot Left="184" Top="681" Right="197" Bottom="694" AlternateText="Thyroid #3"
                                                                        HotSpotMode="PostBack" PostBackValue="Thyroid #3" />
                                                                    <asp:RectangleHotSpot Left="247" Top="694" Right="260" Bottom="707" AlternateText="Forehead"
                                                                        HotSpotMode="PostBack" PostBackValue="Forehead" />
                                                                    <asp:RectangleHotSpot Left="271" Top="687" Right="284" Bottom="700" AlternateText="Emphysema"
                                                                        HotSpotMode="PostBack" PostBackValue="Emphysema" />
                                                                    <asp:RectangleHotSpot Left="215" Top="702" Right="228" Bottom="715" AlternateText="Vision #2"
                                                                        HotSpotMode="PostBack" PostBackValue="Vision #2" />
                                                                    <asp:RectangleHotSpot Left="320" Top="692" Right="333" Bottom="705" AlternateText="Vertex"
                                                                        HotSpotMode="PostBack" PostBackValue="Vertex" />
                                                                    <asp:RectangleHotSpot Left="360" Top="710" Right="373" Bottom="723" AlternateText="Mandible"
                                                                        HotSpotMode="PostBack" PostBackValue="Mandible" />
                                                                    <asp:RectangleHotSpot Left="176" Top="727" Right="189" Bottom="740" AlternateText="Raise Blood Pressure"
                                                                        HotSpotMode="PostBack" PostBackValue="Raise Blood Pressure" />
                                                                    <asp:RectangleHotSpot Left="221" Top="728" Right="234" Bottom="741" AlternateText="LOwer Palate"
                                                                        HotSpotMode="PostBack" PostBackValue="LOwer Palate" />
                                                                    <asp:RectangleHotSpot Left="255" Top="747" Right="268" Bottom="760" AlternateText="Tongue"
                                                                        HotSpotMode="PostBack" PostBackValue="Tongue" />
                                                                    <asp:RectangleHotSpot Left="363" Top="754" Right="376" Bottom="767" AlternateText="Maxilla"
                                                                        HotSpotMode="PostBack" PostBackValue="Maxilla" />
                                                                    <asp:RectangleHotSpot Left="296" Top="764" Right="309" Bottom="777" AlternateText="Upper Palate"
                                                                        HotSpotMode="PostBack" PostBackValue="Upper Palate" />
                                                                    <asp:RectangleHotSpot Left="382" Top="789" Right="395" Bottom="802" AlternateText="Helix #5"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix #5" />
                                                                    <asp:RectangleHotSpot Left="346" Top="813" Right="359" Bottom="826" AlternateText="Inner Ear"
                                                                        HotSpotMode="PostBack" PostBackValue="Inner Ear" />
                                                                    <asp:RectangleHotSpot Left="259" Top="813" Right="272" Bottom="826" AlternateText="Eye"
                                                                        HotSpotMode="PostBack" PostBackValue="Eye" />
                                                                    <asp:RectangleHotSpot Left="168" Top="776" Right="181" Bottom="789" AlternateText="Neurasthenia"
                                                                        HotSpotMode="PostBack" PostBackValue="Neurasthenia" />
                                                                    <asp:RectangleHotSpot Left="261" Top="877" Right="274" Bottom="890" AlternateText="Tonsil #4"
                                                                        HotSpotMode="PostBack" PostBackValue="Tonsil #4" />
                                                                    <asp:RectangleHotSpot Left="261" Top="905" Right="274" Bottom="918" AlternateText="Helix #6"
                                                                        HotSpotMode="PostBack" PostBackValue="Helix #6" />
                                                                    <asp:RectangleHotSpot Left="184" Top="752" Right="197" Bottom="765" AlternateText="Tooth Extraction Anesthetic"
                                                                        HotSpotMode="PostBack" PostBackValue="Tooth Extraction Anesthetic" />
                                                                    <asp:RectangleHotSpot Left="181" Top="816" Right="194" Bottom="829" AlternateText="Tooth Extraction Anesthetic"
                                                                        HotSpotMode="PostBack" PostBackValue="Tooth Extraction Anesthetic" />
                                                                    <asp:RectangleHotSpot Left="515" Top="738" Right="531" Bottom="753" AlternateText="Superior Rool Of Ear"
                                                                        HotSpotMode="PostBack" PostBackValue="Superior Rool Of Ear" />
                                                                    <asp:RectangleHotSpot Left="580" Top="754" Right="596" Bottom="769" AlternateText="Spinal Cord"
                                                                        HotSpotMode="PostBack" PostBackValue="Spinal Cord" />
                                                                    <asp:RectangleHotSpot Left="577" Top="767" Right="593" Bottom="782" AlternateText="Lower Back"
                                                                        HotSpotMode="PostBack" PostBackValue="Lower Back" />
                                                                    <asp:RectangleHotSpot Left="581" Top="836" Right="597" Bottom="851" AlternateText="Middle Back"
                                                                        HotSpotMode="PostBack" PostBackValue="Middle Back" />
                                                                    <asp:RectangleHotSpot Left="526" Top="843" Right="542" Bottom="858" AlternateText="Vagus Rool"
                                                                        HotSpotMode="PostBack" PostBackValue="Vagus Rool" />
                                                                    <asp:RectangleHotSpot Left="561" Top="889" Right="577" Bottom="904" AlternateText="Upper Back"
                                                                        HotSpotMode="PostBack" PostBackValue="Upper Back" />
                                                                    <asp:RectangleHotSpot Left="530" Top="895" Right="546" Bottom="910" AlternateText="Yang Linking"
                                                                        HotSpotMode="PostBack" PostBackValue="Yang Linking" />
                                                                    <asp:RectangleHotSpot Left="517" Top="923" Right="533" Bottom="938" AlternateText="Interior Rool Of Ear"
                                                                        HotSpotMode="PostBack" PostBackValue="Interior Rool Of Ear" />
                                                                    <asp:RectangleHotSpot Left="560" Top="922" Right="576" Bottom="937" AlternateText="Spinal Cord *2"
                                                                        HotSpotMode="PostBack" PostBackValue="Spinal Cord *2" />
                                                                </asp:ImageMap>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left" valign="middle" style="width: 50%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                  <triggers>
       <asp:AsyncPostBackTrigger ControlID="drpAccupuncturePointsNew" EventName="OnSelectedIndexChanged"></asp:AsyncPostBackTrigger>
       </triggers>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>     
    </asp:UpdatePanel>
    <div id="divid2" style="position: absolute; left: 0px; top: 0px; width: 300px; height: 150px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 100%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
        </div>
        <br />
        <br />
        <div style="top: 20px; width: 231px; font-family: Times New Roman; text-align: center;
            font-size: medium;">
            Do You Want To Create New Visit?
        </div>
        <br />
        <br />
        <br />
        <div style="text-align: center; vertical-align: bottom;">
            <asp:Button ID="btnClient" class="Buttons" Style="width: 80px;" runat="server" Text="Yes"
                OnClick="btnOK_Click" />
            <asp:Button ID="btnCancelExist" class="Buttons" Style="width: 80px;" runat="server"
                Text="No" OnClick="CancelVisit" />
        </div>
    </div>
</asp:Content>
