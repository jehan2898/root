<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Doctor_Notes.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Doctor_Notes" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<script type="text/javascript">
function windowclose() {
    //    alert('Hi');

    opener.location.reload(true);
    self.close();
//    
//    window.close();   
}
</script>--%>

    <title></title>
</head>
<body style="width: 425px">
    <form id="form1" runat="server">
    <div>
        <td class="td-widget-bc-search-desc-ch">
            Specialty:
        </td>
        <tr>
            <td>
        <%--<extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" 
            Width="103%" Selected_Text="---Select---"
                    Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP" 
                    Connection_Key="Connection_String" 
            onextenddropdown_selectedindexchanged="extddlSpeciality_extendDropDown_SelectedIndexChanged" AutoPost_back="true"></extddl:ExtendedDropDownList>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblmsg" runat="server" Font-Bold="False" ForeColor="#3333FF" 
            Text="Your changes made to server successfully." Visible="False"></asp:Label>
        <dxe:ASPxComboBox ID="DDLDiagnosis" runat="server" IncrementalFilteringMode="StartsWith"
                                                                            
            EnableSynchronization="False" Width="600px" ClientInstanceName="tbspecility" 
                                                                      
            TextField="description" ValueField="code" EnableIncrementalFiltering="True" 
            ValueType="System.String" 
            onselectedindexchanged="DDLDiagnosis_SelectedIndexChanged" 
            AutoPostBack="True">
                                                                        </dxe:ASPxComboBox>
            </td>
           
        </tr>
    </div>
     <td class="td-widget-bc-search-desc-ch">
            Future:
        </td>


        <div style="height: 150px; background-color: Gray; overflow-y: scroll; width: 600px;">
        <dx:ASPxGridView ID="grdDoctorNotes" ClientInstanceName="grdDiagnosis" runat="server"
          Width="130%" KeyFieldName="description" AutoGenerateColumns="False" >
          <Columns>
          <dx:GridViewDataColumn Caption="chk" VisibleIndex="0" Width="30px">
           <HeaderTemplate>
           </HeaderTemplate>
          <DataItemTemplate>
          <asp:CheckBox ID="chkall" runat="server" />
          </DataItemTemplate>
          <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
          </dx:GridViewDataColumn>
           <%--<dx:GridViewDataTextColumn FieldName="sz_Doctor_Notes" Caption="Doctor Notes">--%>
           <dx:GridViewDataTextColumn FieldName="sz_mandatory_description" Caption="Doctor Notes">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="i_txn_id" Caption="Mandatory Id" Visible="false">
            </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager Visible="False" PageSize="25">
            </SettingsPager>
            </dx:ASPxGridView>
            </div> 
          <center>
         <table width="600px">
         <tr>
         <td align="center">
         
         <dx:ASPxButton ID="btnSave"  runat="server" Width="79px" 
        Text="Save" onclick="btnSave_Click"></dx:ASPxButton>
       </td>
       &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp;&nbsp; 
        &nbsp; &nbsp; &nbsp;
        <%-- <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Width="80px" 
        Text="Close" OnClick ="btnclose_Click" ></dx:ASPxButton>--%>
        <%--onclick="btnclose_Click"--%>
       <%-- <dx:ASPxButton ID="ASPxButton2" runat="server" Width="80px" 
        Text="Close"  onclick="javascript: window.close();"></dx:ASPxButton>
--%>
  <%--  <asp:Button ID="button1" runat="server" OnClientClick="javascript: window.close();"> </asp:Button>--%>

         <%-- </td>--%>
          </tr>
        </table>
        </center>
                         


       
            
    </form>
</body>
</html>
