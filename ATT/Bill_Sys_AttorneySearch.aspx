<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AttorneySearch.aspx.cs"
    Inherits="ATT_Bill_Sys_AttorneySearch" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, 
PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GreenYourBills.com - Search</title>
</head>
<link rel="Stylesheet" href="Css/Search.css" type="text/css" />
<link rel="Stylesheet" href="Css/intake-sheet-ff.css" type="text/css" />
<link rel="Stylesheet" href="Css/main-ch.css" type="text/css" />
<link rel="Stylesheet" href="Css/main-ff.css" type="text/css" />
<link rel="Stylesheet" href="Css/main-ie.css" type="text/css" />
<link rel="Stylesheet" href="Css/mainmaster.css" type="text/css"" />
<link rel="Stylesheet" href="Css/menu.ch.ui.css" type="text/css"" />
<link rel="Stylesheet" href="Css/ModelPopup.css" type="text/css" />
<link rel="Stylesheet" href="Css/style.css" type="text/css" />
<body>

    <script type="text/javascript">
     function OpenDocumentManager(CaseNo,CaseId,cmpid)
    {    
       window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    
     function popup_Shown() 
         {
            PatientInfoPop.Show();
           
        }
         function popup_ShownDesk() 
         {
            PatientDeskPop.Show();
           
        }
        function showPateintFrame(objCaseID,objflag,objCompanyID)
        {
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '100px'; 
            document.getElementById('divfrmPatient').style.top= '200px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="../CaseDetailsPopup.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"&flag="+objflag+"";
            return false;   
        }
        function ClosePatientFramePopup()
        {
            document.getElementById('divfrmPatient').style.visibility='hidden';
            document.getElementById('divfrmPatient').style.top='-10000px';
            document.getElementById('divfrmPatient').style.left='-10000px';
        }
    
    </script>

    <form id="form1" runat="server">
        <div style="margin-left: 300px; width: 800px; padding-top: 10px; padding-bottom: 0px;
            font-family: Arial; font-size: 12px; text-align: right;">
            <span style="font-weight: bold;">Welcome,
                <dxe:ASPxLabel ID="lblUser" runat="server" Text="">
                </dxe:ASPxLabel>
            </span><span>&nbsp; </span><span><a href="../Bill_Sys_Logout.aspx">Logout</a> </span>
        </div>
        <div style="background-color: #E9E9E9; width: 950px; height: 750px; margin-top: 10px;
            margin-left: 150px;">
            <div id="title" style="padding-left: 40px; padding-top: 10px;">
                <p style="font-family: Arial; font-size: 30px; color: #46466A">
                    GreenYourBills.com
                </p>
            </div>
            <table style="width: 95%; background-color: White; margin-left: 23px; height: 680px;
                border: 2px;">
                <tr>
                    <td style="vertical-align: top; width: 100%;">
                        <!-- Table left to hold all the input field controls -->
                        <table style="width: 100%; border: 0px solid;" id="tblsearchfile" runat="server">
                            <tr>
                                <td style="border-bottom: 2px solid #BABABA;">
                                    <span style="text-align: left; color: #FF820C; font-family: Arial; font-size: 22px;">
                                        Search File</span>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 0px solid; padding-left: 0px;" cellpadding="0"
                            cellspacing="0" id="tblsearchparam" runat="server">
                            <tr>
                                <td style="font-family: Arial; font-size: 13px; text-decoration: none; color: #026CA8;
                                    border: 2px; width: 50%; font-weight: bold;" colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;">
                                    File #
                                </td>
                                <td style="padding-top: 0px; margin-top: 0px; padding-left: 15px;">
                                    <dxe:ASPxTextBox ID="txtCaseNo" runat="server" CssClass="txtbox-diplay">
                                    </dxe:ASPxTextBox>
                                    <%-- <input type="textbox" id="txtName" style="width: 220px; border: 1px solid #BABABA;
                                        height: 20px;" />--%>
                                </td>
                                <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;">
                                    Patient Name
                                </td>
                                <td style="padding-top: 0px; margin-top: 0px; padding-left: 15px;">
                                    <dxe:ASPxTextBox ID="txtPatientName" runat="server" CssClass="txtbox-diplay">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;
                                    padding-top: 20px;">
                                    Facility<span style="color: Red;">*</span>
                                </td>
                                <td style="padding-top: 20px; margin-top: 0px; padding-left: 15px;">
                                    <dx:ASPxComboBox runat="server" ID="ddlCompanyName" DropDownStyle="DropDownList"
                                        ClientInstanceName="ddlCompanyName" IncrementalFilteringMode="StartsWith" TextField="SZ_COMPANY_NAME"
                                        ValueField="SZ_COMPANY_ID" EnableSynchronization="False" CssClass="txtbox-diplay">
                                    </dx:ASPxComboBox>
                                    <%-- <select name="origin_city" id="origin_state" size="1" style="width: 220px; border: 1px solid #BABABA;
                                        height: 20px;">
                                        <option value="select city">--- Select Facility --- </option>
                                    </select>--%>
                                </td>
                                <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="font-style: italic; font-size: 11px; font-family: Arial; vertical-align: top;"
                                    colspan="4" align="center">
                                    <%-- <img src="Images/submit.gif" id="btnSubmit" onclick="btnSubmit_Click" />--%>
                                    <dx:ASPxButton ID="btnSubmit" runat="server" EnableTheming="False" Text="Submit"
                                        OnClick="btnSubmit_Click"  AutoPostBack="False"
                                        ImagePosition="Right" Width="132px" Height="35px" ForeColor="#FFFFFF">
                                        <Image Width="128px" Height="30px" Url="Images/submit.gif" UrlPressed="Images/submit.gif" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                            cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100%; border-bottom: 0px solid #BABABA; padding-left: 0px;" align="center">
                                    <dx:ASPxGridView ID="grdPatientSearch" runat="server" Width="99%" SettingsBehavior-AllowSort="false"
                                        SettingsPager-PageSize="20" ClientInstanceName="grdPatientSearch" KeyFieldName="Case_Id"
                                        OnPageIndexChanged="grid_PageIndexChanged">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="File #" FieldName="Case#">
                                                <%--  <DataItemTemplate>
                                                    <a href="Bill_Sys_ATT_WorkArea.aspx?Caseid=<%# Eval("Case_Id") %> &cmp=<%# Eval("SZ_COMPANY_ID") %>&pid=<%# Eval("SZ_PATIENT_ID") %> ">
                                                        <%# Eval("Case#")%>
                                                    </a>
                                                </DataItemTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="Case_Id" Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn Caption="Patient Name" FieldName="Patient_Name">
                                                <%--  <DataItemTemplate>
                                                    <a href="Bill_Sys_Att_patientDesk.aspx?Caseid=<%# Eval("Case_Id") %> &cmp=<%# Eval("SZ_COMPANY_ID") %>&pid=<%# Eval("SZ_PATIENT_ID") %> ">
                                                        <%# Eval("Patient_Name")%>
                                                    </a>
                                                </DataItemTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="Date of Accident"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_COMPANY_NAME" Caption="Facility" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="SZ_PATIENT_ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataHyperLinkColumn Caption="Documents" Visible="true" FieldName="(None)">
                                                <DataItemTemplate>
                                                    <a href="javascript:void(0);" onclick="javascript:OpenDocumentManager('<%# DataBinder.Eval(Container, "DataItem.Case#")%> ','<%# DataBinder.Eval(Container, "DataItem.Case_Id")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');">
                                                        Show</a>
                                                    <%-- <img alt="" onclick="javascript:OpenDocumentManager('<%# DataBinder.Eval(Container, "DataItem.Case#")%> ','<%# DataBinder.Eval(Container, "DataItem.Case_Id")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');"
                                                        style="border: none; cursor: pointer;" title="Show" />--%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataHyperLinkColumn>
                                            <dx:GridViewDataHyperLinkColumn Caption="Demographics" Visible="true" FieldName="(None)">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkPatientInfo" runat="server" Text="Show" OnClick="lnkPatientInfo_Click"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataHyperLinkColumn>
                                            <dx:GridViewDataHyperLinkColumn Caption=" Patient Desk" Visible="true" FieldName="(None)">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkPatientDesk" runat="server" Text="Show" OnClick="lnkPatientDesk_Click"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataHyperLinkColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <Styles>
                                            <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                            </FocusedRow>
                                            <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                            </AlternatingRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                            cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100%; border-bottom: 0px solid #BABABA; padding-left: 0px;" align="center">
                                    <dx:ASPxGridView ID="grdpatientsearchintegrator" runat="server" Width="99%" SettingsBehavior-AllowSort="false"
                                        SettingsPager-PageSize="20" ClientInstanceName="grdpatientsearchintegrator" KeyFieldName="Case Id">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Case Id" Caption="Case Id" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Case #" Caption="Case No" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Patient" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="bill_no" Caption="Bill #" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="sz_company_name" Caption="Facility" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Specialty_Id" Caption="Speciality Id" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Specialty" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataHyperLinkColumn Caption="Documents" Visible="true">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataHyperLinkColumn>
                                            <dx:GridViewDataColumn FieldName="CompanyId" Caption="CompanyId" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="center" />
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <Styles>
                                            <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                            </FocusedRow>
                                            <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                            </AlternatingRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%--<td style="vertical-align: top; width: 1%;">
                        <!-- Table right to hold display text USP -->
                        &nbsp;
                    </td>--%>
                </tr>
            </table>
        </div>
        <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            LoadContentViaCallback="OnFirstShow" ClientInstanceName="PatientInfoPop" HeaderText="Patient Info"
            HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
            EnableViewState="True" Width="500px" Height="200px">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">
                    <panelcollection>
                        <dx:PanelContent ID="PanelContent12" runat="server">
               <div style="vertical-align: middle; width:800px; height:500px; overflow:scroll;">
                    <table width="50%">
                        <tr>
                            <td style="width: 50%;">
                                <table style="width: 800px;">
                                    <tr>
                                        <td style="width: 800px;">
                                            <asp:DataList ID="DtlPatientDetails" runat="server" BorderWidth="0px" BorderStyle="None"
                                                CssClass="TDPart" BorderColor="#DEBA84" Width="100%">
                                                <ItemTemplate>
                                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 98%; border: #8babe4 1px solid #B5DF82; background-color:White;">
                                                        <tr>
                                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                <b>Case#</b>
                                                            </td>
                                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                                <b>Chart No</b>
                                                            </td>
                                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                <b>Patient Name</b>
                                                            </td>
                                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                <b>Insurance Name</b>
                                                            </td>
                                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                <b>Accident Date</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_ID")%>
                                                            </td>
                                                            <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CHART_NO")%>
                                                            </td>
                                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                                            </td>
                                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                                            </td>
                                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:MM/dd/yyyy}")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" CssClass="TDPart"
                                                BorderColor="#DEBA84" Width="100%">
                                                <ItemTemplate>
                                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82; background-color:White;">
                                                        <tr>
                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                                    border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 490px;">
                                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 490px;">
                                                                            <!-- outer table to hold 2 child tables -->
                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                <tr>
                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    First Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Middle Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <asp:Label ID="lblViewMiddleName" runat="server" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Last Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    D.O.B
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_DOB") %>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Gender
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <asp:Label runat="server" ID="lblViewGender" CssClass="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    City
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    State
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label runat="server" ID="lblViewPatientState" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Home Phone
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Work
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    ZIP
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server"
                                                                                                        Text="Wrong Phone" TextAlign="Left" />
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Email
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Extn.
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Attorney
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label ID="lblViewAttorney" runat="server" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Case Type
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label ID="lblViewCasetype" runat="server" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Case Status
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label runat="server" ID="lblViewCaseStatus" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    SSN
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Style="font-weight: bold;"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <asp:Label runat="server" ID="lblVLocation1" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:CheckBox ID="chkPatientTransport" Visible="False" Enabled="False" runat="server"
                                                                                                        Text="Transport" TextAlign="Left"></asp:CheckBox></td>
                                                                                                <td>
                                                                                                    &nbsp;</td>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="td-widget-lf-top-holder-division-ch" valign="top">
                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                                    border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                            &nbsp;<b class="txt3">Insurance Information</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 490px;">
                                                                            <!-- outer table to hold 2 child tables -->
                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                <tr>
                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Policy Holder
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Ins. Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <asp:Label ID="lblViewInsuranceAddress" runat="server" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    City
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    State
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    ZIP
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Phone
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                    FAX
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                    Contact Person
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Claim File#
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Policy #
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewPolicyNumber" runat="server" TargetControlID="lnkViewPolicyNumber" 
                                                                                                                    PopupControlID="pnlPolicyNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                                    border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                            &nbsp;<b class="txt3">Accident Information</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 490px;">
                                                                            <!-- outer table to hold 2 child tables -->
                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                <tr>
                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Accident Date
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Plate Number
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Report Number
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentReportNumber" runat="server" TargetControlID="lnkViewAccidentReportNumber" 
                                                                                                                PopupControlID="pnlReportNO" OffsetX="-300" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    City
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    State
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label runat="server" ID="lblViewAccidentState" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Hospital Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Hospital Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Date Of Admission
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Additional Patient
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Describe Injury
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Patient Type
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label runat="server" ID="lblPatientType" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="td-widget-lf-top-holder-division-ch" valign="top">
                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                                    border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                            &nbsp;<b class="txt3">Employer Information</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 490px;">
                                                                            <!-- outer table to hold 2 child tables -->
                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                <tr>
                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Address
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    City
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    State
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <asp:Label runat="server" ID="lblViewEmployerState" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    ZIP
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Phone
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Date Of First Treatment
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    <asp:Label ID="lblView" runat="server" Text="Chart No." class="lbl" Font-Bold="true"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    <asp:Label ID="lblViewChartNo" runat="server" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                                    border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                            &nbsp;<b class="txt3">Adjuster Information</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 490px;">
                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                <tr>
                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Name
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp; <a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;"
                                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                                        <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                                        <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%>
                                                                                                    </a>
                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
                                                                                                                    PopupControlID="pnlAdjuster" OffsetX="100" OffsetY="-300" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Phone
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Extension
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    FAX
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                    Email
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                    &nbsp;
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                        visibility: hidden; border-left: silver 1px solid; width: 700px; border-bottom: silver 1px solid;
                        position: absolute; top: 682px; height: 150px; background-color: white" id="divfrmPatient">
                        <div style="position: relative; background-color: #B5DF82; text-align: right">
                            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                        </div>
                        <iframe id="frmpatient" src="" frameborder="0" width="700" height="150"></iframe>
                    </div>
                </div>
               </dx:PanelContent>
               </panelcollection>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popup_Shown" />
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="PatientDeskPop" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="PatientDeskPop" HeaderText="Patient Desk" HeaderStyle-HorizontalAlign="Left"
            AllowDragging="True" EnableAnimation="False" EnableViewState="true" Width="400px"
            Height="400px">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <panelcollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                           <table width="80%">
        <tr>
            <td>
                <table width="100%">
                 
                    <tr>
                        <td>
                            <asp:DataList ID="DtlPatientDesk" runat="server" BorderWidth="0px" BorderStyle="None"
                                CssClass="TDPart" BorderColor="#DEBA84" Width="100%">
                                <ItemTemplate>
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82;">
                                        <tr>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Case#</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                <b>Name</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Insurance Company</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Attorney Company</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Accident Date</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_ID")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:MM/dd/yyyy}")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divtab" style="width: 800px; overflow: scroll;">
                    <table width="60%">
                        <tr>
                            <td>
                                <dx:ASPxPageControl ID="tabVistInformation" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                                    Height="400" Width="100%">
                                    <TabPages>
                                        <dx:TabPage Name="tabpnlOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdOne" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportOne" runat="server" GridViewID="grdOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwo" runat="server" GridViewID="grdTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl3" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThree" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThree" runat="server" GridViewID="grdThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFour" runat="server" GridViewID="grdFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl5" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFive" runat="server" GridViewID="grdFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSix" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSix" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSix_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSix" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSix" runat="server" GridViewID="grdSix">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSeven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl7" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSeven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSeven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSeven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSeven" runat="server" GridViewID="grdSeven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEight" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl8" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEight" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEight_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEight" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEight" runat="server" GridViewID="grdEight">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlNine" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl9" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportNine" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportNine_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdNine" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportNine" runat="server" GridViewID="grdNine">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl10" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTen" runat="server" GridViewID="grdTen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEleven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl11" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEleven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEleven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEleven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEleven" runat="server" GridViewID="grdEleven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwelve" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl12" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwelve" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwelve_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwelve" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwelve" runat="server" GridViewID="grdTwelve">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl13" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirteen" runat="server" GridViewID="grdThirteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFourteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl14" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFourteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFourteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFourteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFourteen" runat="server" GridViewID="grdFourteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFifteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl15" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFifteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFifteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFifteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFifteen" runat="server" GridViewID="grdFifteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSixteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl16" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSixteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSixteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSixteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSixteen" runat="server" GridViewID="grdSixteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSeventeen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl17" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSeventeen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSeventeen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSeventeen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSeventeen" runat="server" GridViewID="grdSeventeen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEighteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl18" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEighteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEighteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEighteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEighteen" runat="server" GridViewID="grdEighteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlNineteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl19" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportNineteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportNineteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdNineteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportNineteen" runat="server" GridViewID="grdNineteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwenty" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl20" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwenty" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwenty_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwenty" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwenty" runat="server" GridViewID="grdTwenty">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl21" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyOne" runat="server" GridViewID="grdTwentyOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl22" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyTwo" runat="server" GridViewID="grdTwentyTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl23" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyThree" runat="server" GridViewID="grdTwentyThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl24" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyFour" runat="server" GridViewID="grdTwentyFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl25" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyFive" runat="server" GridViewID="grdTwentyFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentySix" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl26" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentySix" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentySix_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentySix" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentySix" runat="server" GridViewID="grdTwentySix">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentySeven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl27" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentySeven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentySeven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentySeven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentySeven" runat="server" GridViewID="grdTwentySeven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyEight" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl28" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyEight" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyEight_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyEight" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyEight" runat="server" GridViewID="grdTwentyEight">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyNine" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl29" runat="server">
                                                    <table border="0px" width="10%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyNine" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyNine_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyNine" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyNine" runat="server" GridViewID="grdTwentyNine">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirty" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl30" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirty" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirty_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirty" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirty" runat="server" GridViewID="grdThirty">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl31" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyOne" runat="server" GridViewID="grdThirtyOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl32" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyTwo" runat="server" GridViewID="grdThirtyTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl33" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyThree" runat="server" GridViewID="grdThirtyThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl34" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyFour" runat="server" GridViewID="grdThirtyFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl35" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyFive" runat="server" GridViewID="grdThirtyFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
                        </dx:PanelContent>
                    </panelcollection>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popup_ShownDesk" />
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCompanyId" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPatientId" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtuserid" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtbillno" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtlawfirmid" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcaseidforlawfirm" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcompanyidlf" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtSpecialty" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtpatientnamelf" runat="server" Width="10px" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcompanidlaw" runat="server" Width="10px" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
