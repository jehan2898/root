<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_MonthCalender.aspx.cs"
    Inherits="Bill_Sys_MonthCalender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Month Calender</title>

    <script language="javascript" type="text/javascript">
   
    function OpenDayViewCalender(p_szDate)
    {
        alert(document.getElementById("hdnSessionValue").value);
        document.getElementById("hdnSessionValue").value = p_szDate;
        
        document.getElementById("btnSetSession").click();
    }
    </script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="frmMonthCalender" runat="server">
        <div align="center">
            <table class="maintable">
                <tr>
                    <td class="bannerImg" colspan="3">
                        <img src="Images/page-bg.gif" width="100%" height="138px" /></td>
                </tr>
                <tr>
                    <td class="tableheader" colspan="3">
                        <span class="style6">Medical Billing System</span></td>
                </tr>
                <tr>
                    <td colspan="3" class="menucontrol">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            Height="24px"></cc2:WebCustomControl1>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellHeader" colspan="3">
                        Month View Calender
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="30px">
                        <div id="ErrorDiv" visible="true" style="color: Red;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table  width="100%" bgcolor="gray" >
                            <tr>
                                <td width="20%" align="right">
                                    <asp:Button ID="imgbtnPrevDate" runat="server" Text="Prev" OnClick="imgbtnPrevDate_Click" />
                                </td>
                                <td width="30%" align="right">
                                    <asp:DropDownList ID="ddlMonth" runat="Server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="30%" align="left">
                                    <asp:DropDownList ID="ddlYear" runat="Server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                        <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                        <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                        <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                        <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td id="tdMonthCalender" runat="server">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
       <asp:Button ID="btnSetSession" runat="server"  style="width:0px;height:0px;" OnClick="btnSetSession_Click" />
       <asp:HiddenField  ID="hdnSessionValue" runat="server" Value="" ></asp:HiddenField>
      
       
    </form>
</body>
</html>
