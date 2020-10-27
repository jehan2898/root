<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewTicket.aspx.cs" Inherits="NewTicket" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/ticketing.css" type="text/css" />
    <script language="javascript" type="text/javascript">
        var lastType = null;
        function OnTypeChanged() {
            if (cSubTypes.InCallback())
                lastType = cIssueType.GetValue().toString();
            else {
                cSubTypes.PerformCallback(cIssueType.GetValue().toString());
            }
        }

        function OnEndCallback(s, e) {
            if (lastType) {
                cSubTypes.PerformCallback(lastType);
                lastType = null;
            }
        }

        /***********************************************************************************************/
        var fieldSeparator = "|";
        function FileUploadStart() {
            var i = document.getElementById('hdDirToken');
            alert(i);
            alert(i.value);
            document.getElementById("uploadedListFiles").innerHTML = "";
        }

        function FileUploaded(s, e) {
            if (e.isValid) {
                var linkFile = document.createElement("a");
                var indexSeparator = e.callbackData.indexOf(fieldSeparator);
                var fileName = e.callbackData.substring(0, indexSeparator);
                var pictureUrl = e.callbackData.substring(indexSeparator + fieldSeparator.length);
                var date = new Date();
                var imgSrc = "UploadedDocuments/" + pictureUrl + "?dx=" + date.getTime();
                linkFile.innerHTML = fileName;
                linkFile.setAttribute("href", imgSrc);
                linkFile.setAttribute("target", "_blank");
                var container = document.getElementById("uploadedListFiles");
                container.appendChild(linkFile);
                container.appendChild(document.createElement("br"));
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <table style="width:100%;padding-left:0px;height:30px;" border="0">
            <tr>
                <td style="background-color:#CDCAB9;font-family:Calibri;font-size:20px;font-weight:normal;font-style:italic;">
                    Create new ticket
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdDirToken" runat="server" />
        <dx:ASPxCallbackPanel runat="server" 
            meta:resourcekey="ASPxCallbackPanelResource1">
            <PanelCollection>
                <dx:PanelContent meta:resourcekey="PanelContentResource1">
                    <table width="100%" border="0">
                        <tr>
                            <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                                <dx:ASPxLabel runat="server" id="lblErrorMessage" ForeColor="Red" 
                                    meta:resourcekey="lblErrorMessageResource1"></dx:ASPxLabel>
                                <dx:ASPxLabel runat="server" id="lblMessage" ForeColor="Green" 
                                    meta:resourcekey="lblMessageResource1" ></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                                <dx:ASPxLabel runat="server" id="lblTicketNumber" ForeColor="DarkBlue" 
                                    meta:resourcekey="lblMessageResource1" ></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                    
                    <table id="new-ticket-form" width="400px" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Type</label>
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxComboBox runat="server" 
                                    CssClass="inputBox" 
                                    ID="cIssueType"
                                    ClientInstanceName = "cIssueType"
                                    Width="195px" 
                                    ItemStyle-HoverStyle-BackColor="#F6F6F6"
                                    EnableSynchronization="False" meta:resourcekey="cIssueTypeResource1">
                                    <Items>
                                        <dx:ListEditItem Text="Select a type" Value="not_selected" Selected="true" 
                                            meta:resourcekey="ListEditItemResource1" />
                                        <dx:ListEditItem Text="I am experiencing a problem with the website" 
                                            Value="website_problem" meta:resourcekey="ListEditItemResource2" />
                                        <dx:ListEditItem Text="I need support to make changes through the backend" 
                                            Value="backend_support" meta:resourcekey="ListEditItemResource3" />
                                        <dx:ListEditItem Text="I want to import data into my account" 
                                            Value="data_import" meta:resourcekey="ListEditItemResource4" />
                                        <dx:ListEditItem Text="I want to request a new feature" Value="feature_request" 
                                            meta:resourcekey="ListEditItemResource5" />
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTypeChanged(); }" />
                                    <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6"></HoverStyle>
                                    </ItemStyle>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Elaborate</label>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxComboBox runat="server" 
                                    CssClass="inputBox"
                                    ValueField = "s_value"
                                    TextField = "s_text"
                                    ID="cSubTypes"
                                    ClientInstanceName = "cSubTypes"
                                    Width="195px"
                                    OnCallback="changeSubType" meta:resourcekey="cSubTypesResource1">
                                    <ClientSideEvents EndCallback=" OnEndCallback"/>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Priority</label>
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxComboBox runat="server" ID="ddlPriority" Width="195px" CssClass="inputBox" EnableSynchronization="False"
                                    ValueType="System.String">
                                    <ItemStyle>
                                        <HoverStyle BackColor="#F6F6F6">
                                        </HoverStyle>
                                    </ItemStyle>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Raised By</label>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxTextBox 
                                    Enabled="false"
                                    CssClass="inputBox" 
                                    runat="server" 
                                    ID="tRaisedBy" 
                                    Height="30px"
                                    Width="195px" meta:resourcekey="tRaisedByResource1">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Email (Default)</label>
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxTextBox 
                                    CssClass="inputBox" 
                                    runat="server" 
                                    Height="30px"
                                    ID="tDefaultEmail" Width="195px" meta:resourcekey="tDefaultEmailResource1">
                                </dx:ASPxTextBox>
                                <div class="text-note">
                                    (This is the default email address configured with your account. Change it to receive ticket updates on a different email address)
                                </div>
                                <asp:RegularExpressionValidator 
                                    ID="emailValidator" 
                                    runat="server" 
                                    Display="Dynamic"
                                    CssClass="invalid"
                                    ErrorMessage="Email address is not valid"
                                    ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                    ControlToValidate="tDefaultEmail">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Email (CC)</label>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxTextBox 
                                    CssClass="inputBox" 
                                    runat="server" 
                                    ID="tEmailCC" 
                                    Height="30px"
                                    Width="195px" meta:resourcekey="tEmailCCResource1">
                                </dx:ASPxTextBox>
                                <div class="text-note">
                                    (A comma (,) separated list of email addresses. <%=sFrequentEmailsText %>)
                                </div>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator1" 
                                    runat="server" 
                                    Display="Dynamic"
                                    CssClass="invalid"
                                    ErrorMessage="List of email addresses is not valid"
                                    ValidationExpression="^(\w([-_+.']?\w+)+@(\w(-*\w+)+\.)+[a-zA-Z]{2,4}[,])*\w([-_+.']?\w+)+@(\w(-*\w+)+\.)+[a-zA-Z]{2,4}$"
                                    ControlToValidate="tEmailCC">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Subject</label>
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxTextBox 
                                    CssClass="inputBox" 
                                    runat="server" 
                                    Height="30px"
                                    ID="tSubject"
                                    Width="195px" meta:resourcekey="tSubjectResource1">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Describe the problem</label>
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxMemo
                                    CssClass="inputBox" 
                                    runat="server"
                                    ID="tDescription" 
                                    Height="30px"
                                    Rows="20"
                                    Width="195px" meta:resourcekey="tDescriptionResource1">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="new-ticket-form-lable-td">
                                <label>Callback Phone</label>
                            </td>
                            <td class="form-ibox">
                                <dx:ASPxTextBox 
                                    CssClass="inputBox" 
                                    runat="server" 
                                    ID="tCallBack" 
                                    Height="30px"
                                    Width="195px" meta:resourcekey="tCallBackResource1">
                                </dx:ASPxTextBox>
                                <div class="text-note" >
                                    (Leave a number where we can reach you to know more about the issue. Your contact details will not be shared outside Green Bills)
                                </div>
                            </td>
                        </tr>

                            <tr>
                            <td class="new-ticket-form-lable-td">
                                <label> Upload File :</label>
                            </td>
                            <td class="form-ibox" align="left">
                                 <asp:FileUpload ID="fuFirst" runat="server"  />                         
                            </td>
                        </tr>

                         <tr>
                            <td class="new-ticket-form-lable-td">
                                <label> Upload File :</label>
                            </td>
                            <td class="form-ibox" align="left">
                                 <asp:FileUpload ID="fuSecond" runat="server" />
                            </td>
                        </tr>

                         <tr>
                            <td class="new-ticket-form-lable-td">
                                <label> Upload File :</label>
                            </td>
                            <td class="form-ibox" align="left">
                                 <asp:FileUpload ID="fuThird" runat="server" />
                            </td>
                        </tr>
                         
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;padding-left:50%;">
                                <dx:ASPxButton runat="server" Text="Create" ID="btnCreate" 
                                    OnClick="btnCreate_Click" meta:resourcekey="btnCreateResource1">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </form>
</body>
</html>