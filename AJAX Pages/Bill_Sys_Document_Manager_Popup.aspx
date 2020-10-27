 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Document_Manager_Popup.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Document_Manager_Popup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .selector
        {
            -moz-border-radius: 6x;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            border: 1px solid white;
        }
    </style>
    <script type="text/javascript" src="../Document Manager/Case/EasyMenu/Styles/ListBoxEvents.js"></script>
    <script type="text/javascript" src="../Document Manager/Case/EasyMenu/Styles/js_TreeViewFunctions.js"></script>
    <script type="text/javascript">

        function VisibleFrame() {
            document.getElementById('divmyiframe').style.display = 'block';
        }
        function callforMerge() {
            document.getElementById('hdnmerge').value = 'true';
            var f = document.getElementById('ASPxRoundPanel1_grddocumnetmanager');

            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkall') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }


        }


        function UploadDoc() {
            document.getElementById('hdnmerge').value = 'true';
            var f = document.getElementById('ASPxRoundPanel1_grddocumnetmanager');
            var cnt = 0;

            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkall') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                            cnt++;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            } else {

                if (cnt > 1) {

                    alert('Please select only one node.');
                    return false;
                } else {
                    OpenUploadPanel();
                    return false;
                }
            }


        }

        function OpenUploadPanel() {

            document.getElementById("Uploaddiv").style.position = "absolute";

            document.getElementById("Uploaddiv").style.left = "300px";

            document.getElementById("Uploaddiv").style.top = "150px";

            document.getElementById("Uploaddiv").style.visibility = "visible";
            document.getElementById("Uploaddiv").style.zIndex = '1';

        }
        function Close_UploadPanel() {
            document.getElementById("Uploaddiv").style.visibility = "hidden";

        }

        function chkgotxt() {
            if (document.getElementById("txtCaseSearch").value == "") {
                alert("Please Enter the Case No!!!");
                return false;
            }
        }
        
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="33%" style="height: 28px">
                                <b>Patient Name : </b>
                                <asp:label id="lblPatientName" runat="server"></asp:label>
                            </td>
                            <td align="right" style="height: 28px">
                                Billing Company :
                                <%=((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME%>
                            </td>
                            <td width="10px" style="height: 28px">
                            </td>
                            <td style="height: 28px">
                                As :
                                <%=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME%>
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp;&nbsp;
                    <UserMessage:MessageControl ID="MessageControl1" runat="server" />
                </td>
            </tr>
        </table>
        
        <div style="float: left; width: 28%">
            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Merge" Width="100%">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                     <asp:button id="btnUpload" runat="server" text="Upload Files" />
                                    <asp:button id="btndelete" runat="server" text="Delete Files" onclick="btndelete_Click" />
                                    <asp:button id="btnAdd" runat="server" text="Pdf Merge" onclick="BtnMerge_OnClick" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="grddocumnetmanager" ClientInstanceName="grddocumnetmanager"
                                        runat="server" AutoGenerateColumns="False" KeyFieldName="NodeID" Width="100%">
                                        <columns>
                                                <dx:GridViewDataColumn Caption="chk" VisibleIndex="0" Width="50px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" Visible="false" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" />
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="CaseID" Caption="CaseID"
                                                    Visible="False">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID"
                                                    Visible="False">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="NodeName" Caption="NodeName"
                                                    Width="100px" VisibleIndex="2">
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="NodeID" Caption="NodeID"
                                                    Visible="False">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataButtonEditColumn Caption="File Name"
                                                    Width="307px" VisibleIndex="3">
                                                    <DataItemTemplate>
                                                        <a target="imageframe" href="<%#Eval("Link")%>" onclick="VisibleFrame();">
                                                            <%#Eval("File_Name")%>
                                                        </a>
                                                    </DataItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataButtonEditColumn>
                                                <dx:GridViewDataColumn FieldName="File_Path" Caption="File Path"
                                                    Width="240px" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Link" Caption="Link"
                                                    Width="240px" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataColumn FieldName="ImageId" Caption="ImageId"
                                                    Visible="False">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </dx:GridViewDataColumn>

                                                   
                                            </columns>
                                        <settingspager visible="False" pagesize="1000">
                                            </settingspager>
                                        <settingsbehavior allowselectbyrowclick="True" allowfocusedrow="True" />
                                        <settings showverticalscrollbar="True" showhorizontalscrollbar="True" verticalscrollableheight="1000" />
                                        <styles>
                                                <FocusedRow BackColor="#99CCFF">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                                <Header BackColor="#99CCFF">
                                                </Header>
                                            </styles>
                                        <settingscustomizationwindow height="600px" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </div>
        <div style="float: right; width: 70%; height: 100%;">
            <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="View">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">
                        <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" Height="1054px" ClientInstanceName="CallbackPanel">
                            <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server">
                                    <div id="divmyiframe">
                                        <iframe name="imageframe" id="myiframe" runat="server" frameborder="0" style="height: 1000px;
                                            width: 890px;"></iframe>
                                    </div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </div>
        <%--  <table style="width: 100%; background-color: White" border="1">
                <tr>
                    <td style="width: 40%;" valign="top">
                    </td>
                    <td style="width: 60%;" valign="top">
                    </td>
                </tr>
            </table>--%>
        <asp:textbox id="txtCompanyID" runat="server" visible="false">
        </asp:textbox>
        <asp:textbox id="txtcaseid" runat="server" visible="false">
        </asp:textbox>
        <asp:hiddenfield id="hdnmerge" runat="server" />
        <%--     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>--%>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMerge" runat="server" TargetControlID="lbnTest"
            DropShadow="false" PopupControlID="pnlMerge" BehaviorID="modal1" PopupDragHandleControlID="Div2">
        </ajaxToolkit:ModalPopupExtender>
        <asp:panel style="display: none; width: 510px; height: 420px; background-color: white;
            border: 1px solid #B5DF82;" id="pnlMerge" runat="server" cssclass="selector">
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <div style="left: 0px; width: 510px; position: absolute; top: 0px; height: 18px;
                                            background-color: #B5DF82; text-align: left" id="Div2" class="selector">
                                            <b>Merge</b>
                                            <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;
                                                border: 1px solid #B5DF82;" class="selector">
                                                <asp:button id="btnmergeclose" runat="server" height="19px" width="50px" class="GridHeader1"
                                                    text="X" onclientclick="$find('modal1').hide(); return false;"></asp:button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>--%>
                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                        <%--<asp:UpdateProgress ID="UpdatePanel19" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel11">
                                                        <ProgressTemplate>
                                                            <div id="Div3" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img46" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...</div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="button" name="btnmoveup" id="btnmoveup" onclick="listbox_move('lstPDF', 'up');"
                                            runat="server" value="Up" style="width: 50px" class="box" />
                                        &nbsp;
                                        <input type="button" name="btnmovedown" id="btnmovedown" onclick="listbox_move('lstPDF', 'down');"
                                            runat="server" value="Down" style="width: 50px" class="box" />
                                        &nbsp; &nbsp;
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="ver11blue" style="width: 395px; height: 119px;" valign="top">
                                        Select the order in which you want to merge the pdf: &nbsp;
                                        <asp:listbox id="lstPDF" runat="server" width="500px" height="200px"></asp:listbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <%--Please Enter Merge Filename &nbsp;--%>
                                        <asp:textbox id="txtmergerfilename" runat="server" width="91%" visible="false">
                                        </asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <center>
                                            <input name="btnSelectOrder" type="button" class="box" id="btnSelectOrder" onclick="setorder()"
                                                value="Start Merging" runat="server" />
                                            <%--  <asp:Button ID="btnSelectOrder" runat="server" Text="Start Merging" OnClick="BtnMergeDoc_OnClick" />--%>
                                            <%--<dx:ASPxButton ID="btnSelectOrder" Native="true" runat="server" Text="Start Merging"
                                                    OnClick="BtnMergeDoc_OnClick">
                                                    <ClientSideEvents Click="function(s, e) {LoadingPanel.Show();}" />
                                                </dx:ASPxButton>--%>
                                            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="box" OnClick="btnCancel_Click" />--%>
                                        </center>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True" b>
            </dx:ASPxLoadingPanel>
        </asp:panel>
        <div style="display: none">
            <asp:linkbutton id="lbnTest" runat="server" text="View Job Details" font-names="Verdana">
            </asp:linkbutton>
        </div>
        <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
    <div id="div1" style="visibility: hidden">
        <asp:button id="btnDone" runat="server" cssclass="box" text="Button" visible="true"
            onclick="btnDone_Click" />
        <asp:button id="btnCheck" runat="server" text="Check" visible="true" />
        <input type="hidden" id="hidnFile" name="hidnFile" runat="server" class="box" value="" />
        <input type="hidden" id="hidnOrderFiles" name="hidnOrderFiles" runat="server" class="box"
            value="" />
    </div>


       <div id="Uploaddiv" style="position: absolute; width: 500px; height: 200px; background-color: white; visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 500px">
            <a onclick="document.getElementById('Uploaddiv').style.zIndex = '-1'; document.getElementById('Uploaddiv').style.visibility='hidden';"
                style="cursor: pointer;" title="Close">X</a>
        </div>&nbsp;&nbsp;&nbsp;&nbsp;
       <table cellpadding="1" cellspacing="0" align="center" style="width:100%; vertical-align:middle; font-size:small; ">
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="width:70%; color:Blue">
                        <asp:Label ID="Msglbl" runat="server" Font-Size="Small" Text="Select a File to Upload"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td style="width:30%;" align="center">
                        Upload File:
                    </td>
                    <td style="width:70%" >
                        <asp:FileUpload  ID="ReportUpload" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>                    
                    <td style="width:30%">
                        &nbsp;                    
                    </td>
                    <td style="width:70%" align="center">
                        <table cellpadding="0" cellspacing="0" style="width:100%">
                            <tr>
                                <td style="width:50%;" align="right">
                                    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" />&nbsp;
                                    &nbsp;&nbsp;  
                                </td>
                                <td style="width:50%;" >
                                    <input type="button" value="Close" onclick="Close_UploadPanel();" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
               </tr>
               <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
        </table>
    </div>      
    </form>
</body>
</html>