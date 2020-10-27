<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewPatientASpeciality.aspx.cs"
    Inherits="Bill_Sys_ViewPatientASpeciality" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script>
            function openPage(id) 
            {
                document.getElementById("divid").style.position = "absolute";
                document.getElementById("divid").style.left= "0px";
                document.getElementById("divid").style.top= "0px";
                document.getElementById("divid").style.visibility="visible";
                document.getElementById('divid').style.zIndex= '1'; 
                document.getElementById("divid").style.width= "700px";
                document.getElementById("divid").style.height= "500px";
                document.getElementById("frameeditexpanse").style.width= "700px";
                document.getElementById("frameeditexpanse").style.height= "500px";
                document.getElementById("frameeditexpanse").src="ViwScheduled.aspx?id=" + id
            }
            
            
       function SelectAndClosePopup()
       {
          var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td class="TDPart" width="100%">
                        <asp:DataGrid ID="grdListOfPatient" AutoGenerateColumns="false" runat="server" Width="100%"
                            CssClass="GridTable">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Case #">
                                    <ItemTemplate>
                                        <a href="#" onclick="javascript:openPage('<%#DataBinder.Eval(Container, "DataItem.I_EVENT_ID")%>');">
                                            <%#DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_EVENT_TIME" HeaderText="Event Time"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                <asp:BoundColumn DataField="STATUS" HeaderText="Visit Status"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
                <div style="visibility: hidden;">
                    <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                        OnClick="txtUpdate_Click" /></div>
            </table>
        </div>
        <div id="divid" style="position: absolute; width: 700px; height: 500px; background-color: #DBE6FA;
            visibility: hidden;">
            <div style="position: relative; text-align: right; background-color: #8babe4; width: 700px">
                <a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden'; var button = document.getElementById('<%=btnCls.ClientID%>');     button.click(); "
                    style="cursor: pointer;" title="Close">X</a>
            </div>
            <iframe id="frameeditexpanse" src="" frameborder="0" height="500px" width="700px"></iframe>
        </div>
    </form>
</body>
</html>
