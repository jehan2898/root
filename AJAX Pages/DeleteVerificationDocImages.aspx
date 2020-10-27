<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteVerificationDocImages.aspx.cs" Inherits="AJAX_Pages_DeleteVerificationDocImages" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
<script type="text/javascript">
    function OpenDocumentManager(Path) {
        window.open(Path, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    
 </script>

<script type="text/javascript">
    
    function CheckDelete() 
    {
        
        var f = document.getElementById("<%=grdDelDocImg.ClientID%>");
        var bfFlag = false;
        for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
            if (f.getElementsByTagName("input").item(i).name.indexOf('chkDel') != -1) 
            {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                {

                    if (f.getElementsByTagName("input").item(i).checked != false)
                     {
                        bfFlag = true;

                     }

                }
            }
        }

        if (bfFlag == false) 
        {
            alert('Please select the record for delete.');
            return false;
        }
        else {
            if (confirm("Are you sure want to Delete?") == true) {

                return true;
            }
            else {
                return false;
            }
        }

     

      

    }

        
    </script>
   
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <div>
    <asp:updatepanel id="UpdatePanel2" runat="server">
                        <ContentTemplate>
        <table id="tb2" runat="server" width="100%">
        <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                </td>
                <td valign="top" align="center">
                    <table width="100%" border="0">
                        <tr>
                            <td style="width: 78%">
                                &nbsp;
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxGridView ID="grdDelDocImg" runat="server" AutoGenerateColumns="False" 

                        Settings-VerticalScrollableHeight="300" 
                        SettingsCustomizationWindow-Height="300" SettingsPager-PageSize="50" 
                        Width="100%">
                        <Columns>
                        <dx:GridViewDataColumn Caption="Bill #" FieldName="SZ_BILL_NUMBER" 
                                HeaderStyle-HorizontalAlign="Center" VisibleIndex="0" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataColumn>

                             <dx:GridViewDataColumn Caption="File Name" FieldName="Filename" 
                                HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" Width="230px">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataColumn>

                            
                            <dx:GridViewDataColumn Caption="File Path" Settings-AllowSort="False" >
                                <HeaderTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="File Path"></asp:Label>
                                </HeaderTemplate>
                                <settings allowsort="False" />
                                <dataitemtemplate>
                                   <a id="lnkframefile"  runat="server"  href="#" onclick='<%# "OpenDocumentManager(" + "\""+ Eval("File_Path") +"\");" %>' >'<%# DataBinder.Eval(Container, "DataItem.Filename")%>'</a>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </dx:GridViewDataColumn>
                            

                            <dx:GridViewDataTextColumn Caption="Type" FieldName="VerifyTYPE" 
                                VisibleIndex="4" Width="100px">
                                <propertiestextedit enablefocusedstyle="False">
                                </propertiestextedit>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataColumn Caption="ImageID" FieldName="ImageID" 
                                HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" Width="0px" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Delete" Settings-AllowSort="False" Width="50px">
                                <HeaderTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="Delete"></asp:Label>
                                </HeaderTemplate>
                                <settings allowsort="False" />
                                <dataitemtemplate>
                                    <asp:CheckBox ID="chkDel" runat="server" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </dx:GridViewDataColumn>

                             <dx:GridViewDataColumn Caption="Physical_File_Path" FieldName="Physical_File_Path" 
                                HeaderStyle-HorizontalAlign="Center"  Width="0px" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataColumn>

                        </Columns>
                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="false"  
                            ShowGroupPanel="false" />
                        <SettingsBehavior AllowFocusedRow="false" />
                        <SettingsBehavior AllowSelectByRowClick="false" />
                        <SettingsPager Position="Bottom" />
                        <settingscustomizationwindow height="100px" />
                    </dx:ASPxGridView>
                    <asp:TextBox ID="txtCompanyName" runat="server" visible="false">
                    </asp:TextBox>
                    <asp:TextBox ID="txtBillNo" runat="server" visible="false">
                    </asp:TextBox>
                    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false" Width="10px"></asp:TextBox>
                    <%--<asp:updatepanel id="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <asp:UpdateProgress ID="UpdateProgress123" runat="server" 
                        AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="10">
                        <progresstemplate>
                            <div ID="DivStatus123" runat="Server" style="vertical-align: bottom; position: absolute; top: 100px;
                                        left: 450px">
                                <asp:Image ID="img123" runat="server" AlternateText="Loading....." 
                                    Height="25px" ImageUrl="~/Images/rotation.gif" Width="24px" />
                                Loading...
                            </div>
                        </progresstemplate>
                    </asp:UpdateProgress>
                    <center>
                    </center>
                    <%--</ContentTemplate>
                    </asp:updatepanel>--%>
                </td>
            </tr>
            <tr>
            <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                </td>
            <td style="text-align:center">
            <asp:Button Style="width: 80px" ID="btnDelete" Text="Delete" runat="Server" 
                    align="Center"  onclick="btnDelete_Click"
                                     />
            </td></tr>
            <tr><td><asp:HiddenField ID="hdncaseid" runat="server" /><asp:HiddenField ID="hdnConfirm" runat="server" /></td></tr>
        </table>
                            
        </ContentTemplate>
                    </asp:updatepanel>
    </div>
       <div style="visibility:hidden">
            <asp:Button ID="btnConfirm"  runat="server" />

        </div>
    </form>
</body>
</html>
