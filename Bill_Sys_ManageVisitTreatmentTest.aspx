<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ManageVisitTreatmentTest.aspx.cs"
    Inherits="Bill_Sys_ManageVisitTreatmentTest" MasterPageFile="~/MasterPage.master"  %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript" src="validation.js"></script>

     <script type="text/javascript">
    function CloseWindow()
    {
        window.close();
    }
    
    </script>
<div runat="server">

            <script type="text/javascript">
                var iCount = <%=getProcedureCount()%>;
                var arAmounts=new Array(iCount);
                var drAmounts=new Array(iCount);
                
                <%
                    string szAmount="",szDRAmounts="";
                    DataTable obj = getProcedureTable();
                    int i=0;
                    if( obj != null)
                    {
                        foreach (DataRow row in obj.Rows)
                        {
                              szAmount = "" + row["Amount"];
                              szDRAmounts = "" + row["dr_Amount"];
                              Response.Write("arAmounts[" + i + "] =" + "'" + szAmount + "';");
                              Response.Write("drAmounts[" + i + "] =" + "'" + szDRAmounts + "';");
                              ++i;
                        }
                    }
                %>    


function RemoveNode(Obj)
{
  //  alert(Obj);
    var mySelect = document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_lstProcedures");
    for(i = 0; i < mySelect.options.length; i++) 
    { 
        if(mySelect.options[i].selected) 
        { 
            if(mySelect.options[i].value == Obj)
                mySelect.options[i].selected = false;
        }
    }
    getMultipleSelection();
}
                
 function getMultipleSelection()
 {  var selected = new Array(); 
 
    var mySelect = document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_lstProcedures");
    var pnnerHTML = new String(); 
    pnnerHTML="<table width='100%' style='width:100%;border-right: teal 1pt solid; border-top: teal 1pt solid; border-left: teal 1pt solid; border-bottom: teal 1pt solid'><tr><td>Procedure code/description</td><td>Procedure amount</td><td>KOEL factor</td><td>Doctor’s amount</td><td>[-] – Remove row</td></tr>"
 // pnnerHTML="<table style='border-right: teal 1pt solid; border-top: teal 1pt solid; border-left: teal 1pt solid;' align='center'><tr><td>Procedure code/description</td><td>Procedure amount</td><td>KOEL factor</td><td>Doctor’s amount</td><td>[-] – Remove row</td></tr>"
    for(i = 0; i < mySelect.options.length; i++) 
    { 
        if(mySelect.options[i].selected) 
        { 
//            var txtPA = document.getElementById("tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_txtProcedureAmount");
//            var txtDA = document.getElementById("tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_txtDoctorPrice");
//            txtPA.value = arAmounts[i];
//            txtDA.value = drAmounts[i];
            
            //var objProcCodeDescription=mySelect.options[i].text;
            //tr" + mySelect.options[i].value + "
            //name='tr" + mySelect.options[i].value + "'
            pnnerHTML=pnnerHTML.concat("<tr name='tr" + mySelect.options[i].value + "'><td>" + mySelect.options[i].text + "</td><td>" + arAmounts[i] + "</td><td>" + "1" + "</td><td>" + "<input type='text' width='120px' name='txtDoctorAmount' value='" + drAmounts[i] + "'  />" + "</td><td>" + "<input type='button' value='Remove' onclick=\"RemoveNode('" + mySelect.options[i].value + "');\" />" + "</td></tr>"); 
            //alert(pnnerHTML);
        } 
    } 
    pnnerHTML=pnnerHTML.concat("</table>"); 
    document.getElementById('divPrices').innerHTML = pnnerHTML;

    
}
                function processAmount(){
                
                   
                
                    var lst = document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_lstProcedures");
                  
                    var txtPA = document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_txtProcedureAmount");
                    var txtDA = document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatmentPrice_txtDoctorPrice");
                    txtPA.value = arAmounts[lst.selectedIndex];
                    txtDA.value = drAmounts[lst.selectedIndex];
                  
                }
                
            </script>

        </div>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
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
                                                <td class="TDPart" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                 <tr>
                        <td style="width: 100%" class="SectionDevider">
                        </td>
                    </tr>
                                            <tr>  
                                                  
                    <tr>
                    <td class="TDPart" width="100%">
                    <asp:Label ID="lblHeaderText" runat="server"></asp:Label>
                    </td>
                    </tr>
                     <tr>
                        <td style="width: 100%" class="SectionDevider">
                        </td>
                    </tr>
                     
                
                  
                    <tr>
                        <td style="width: 100%" class="TDPart">
                            <ajaxToolkit:TabContainer ID="tabcontainerPatientTreatment" runat="Server" ActiveTabIndex="1" CssClass="ajax__tab_theme">
                                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlViewTreatment" TabIndex="0">
                                                                <HeaderTemplate>
                                                                    <div style="width: 250px; text-align: center;" class="lbl">
                                                                        <%=szTabCaptions[0]%>
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <div>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td width="95%">
                                                                                    <span style="padding: 1px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">
                                                                                        <asp:Label ID="lblDoctorNameViewTreatment" runat="server"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td width="95%">
                                                                                    <div style="padding: 1px; overflow-y: scroll; width: 90%; height: 400px;">
                                                                                        <asp:DataGrid ID="grdTreatmentTestVisit" Width="100%" AutoGenerateColumns="false"  CssClass="GridTable" runat="server" OnItemCommand="grdTreatments_ItemCommand">
                                                                                        <ItemStyle CssClass="GridRow" />
                                                                                           <HeaderStyle CssClass="GridHeader" />
                                                                                            <Columns>
                                                                                                <asp:TemplateColumn>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkAssign" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="Type Code ID" Visible="False">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Type Code"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Type Description"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_ID" HeaderText="Type ID" Visible="False"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Type" Visible="False"></asp:BoundColumn>
                                                                                                <asp:TemplateColumn HeaderText="Billable/Non-Billable">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnlMakeBillable" runat="server" Text="Make Billable" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_TYPE_CODE_ID")%>'
                                                                                                            CommandName="MakeBillable"></asp:LinkButton>
                                                                                                        <asp:LinkButton ID="lnlMakeNonBillable" runat="server" Text="Make Non-Billable" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_TYPE_CODE_ID")%>'
                                                                                                            CommandName="MakeNonBillable"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:BoundColumn DataField="BT_Bilable" HeaderText="Billable/NonBillable" Visible="False">
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="center">
                                                                                    <span class="tablecellControl" style="width: 100%">
                                                                                        <asp:Button ID="btnAssign" runat="server" Text="OK" Width="80px" Height="20px" Font-Size="11px"
                                                                                            CssClass="Buttons" OnClick="btnAssign_Click" />
                                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" Height="20px"
                                                                                            Font-Size="11px" CssClass="Buttons" />
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="left">
                                                                                    <asp:DataGrid ID="grdTreatments" runat="server" Width="100%"  CssClass="GridTable" AutoGenerateColumns="False">
                                                                                     <ItemStyle CssClass="GridRow" />
                                                                                            <HeaderStyle CssClass="GridHeader" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="SZ_TREATMENT_NAME" HeaderText="Treatment">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_TREATMENT_DESCRIPTION" HeaderText="Description">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE" HeaderText="Code">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_TEXT" HeaderText="Description">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT" HeaderText="Procedure charge">
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_AMOUNT" HeaderText="Doctor's charge">
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundColumn>
                                                                                        </Columns>
                                                                                    </asp:DataGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAddTreatmentPrice" TabIndex="1" HeaderText="Anywya">
                                                                <HeaderTemplate>
                                                                    <div style="width: 250px; text-align: center;" class="lbl">
                                                                        <%=szTabCaptions[1]%>
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <div>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td width="95%">
                                                                                    <span style="padding: 1px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">
                                                                                        <asp:Label ID="lblDoctorHeaderPrice" runat="server"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel" style="height: 22px">
                                                                                    <div class="lbl">
                                                                                        Treatment</div>
                                                                                </td>
                                                                                <td width="95%" style="height: 22px">
                                                                                    <span class="tablecellControl" style="padding: 1px;">
                                                                                        <asp:DropDownList ID="ddlTestNames" runat="server" Width="100%" OnSelectedIndexChanged="ddlTestNames_SelectedIndexChanged"
                                                                                            AutoPostBack="True">
                                                                                        </asp:DropDownList>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    <div class="lbl">
                                                                                        Procedures</div>
                                                                                </td>
                                                                                <td width="95%">
                                                                                    <span class="tablecellControl" style="padding: 1px;">
                                                                                        <asp:ListBox onchange="getMultipleSelection();" ID="lstProcedures" runat="server"
                                                                                            CssClass="text-box" Width="100%" Height="150px" SelectionMode="Multiple"></asp:ListBox>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trHideRow" runat="server" visible="false">
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    <div class="lbl">
                                                                                        KOEL</div>
                                                                                </td>
                                                                                <td width="100%">
                                                                                    <span class="tablecellControl" style="padding: 1px;">
                                                                                        <asp:TextBox ReadOnly="True" ID="txtProviderKOEL" runat="server" CssClass="text-box"
                                                                                            Width="10%"></asp:TextBox>
                                                                                    </span><span class="lbl" style="width: 130px; float: left; position: relative; padding: 1px;">
                                                                                        Procedure Price (US $) </span><span class="lbl" style="float: left; position: relative;
                                                                                            padding: 1px;">
                                                                                            <asp:TextBox ID="txtProcedureAmount" runat="server" CssClass="text-box" Width="10%"
                                                                                                ReadOnly="True"></asp:TextBox>
                                                                                        </span><span class="lbl" style="width: 130px; float: left; position: relative; padding: 1px;">
                                                                                            Doctor's price </span><span class="lbl" style="float: left; position: relative;">
                                                                                                <asp:TextBox ID="txtDoctorPrice" runat="server" CssClass="text-box" Width="10%"></asp:TextBox>
                                                                                            </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            <td width="5%" class="tablecellLabel">
                                                                                &nbsp;
                                                                            </td>
                                                                                <td colspan="2" align="center" width="95%" >
                                                                                    <div id="divPrices" >
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="center">
                                                                                    <span class="tablecellControl" style="width: 100%">
                                                                                        <asp:Button ID="btnSaveTreatmentPrices" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                                                            Font-Size="11px" OnClick="btnSaveTreatmentPrices_Click" />
                                                                                        <asp:Button ID="btnCloseFromTPrices" runat="server" Text="Close" Width="80px" CssClass="Buttons"
                                                                                            Font-Size="11px" />
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="left">
                                                                                    <asp:DataGrid ID="grdTreatmentPrices" runat="server" AutoGenerateColumns="False"
                                                                                        Visible="False" Width="100%"  CssClass="GridTable">
                                                                                         <ItemStyle CssClass="GridRow" />
                                                                                            <HeaderStyle CssClass="GridHeader" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="SZ_TREATMENT_NAME" HeaderText="Treatment">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_TREATMENT_DESCRIPTION" HeaderText="Description">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE" HeaderText="Code">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_TEXT" HeaderText="Description">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT" HeaderText="Procedure charge">
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_AMOUNT" HeaderText="Doctor's charge">
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                            </asp:BoundColumn>
                                                                                        </Columns>
                                                                                    </asp:DataGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociateProcedureCode" TabIndex="2">
                                                                <HeaderTemplate>
                                                                    <div style="width: 250px; text-align: center;" class="lbl">
                                                                        <%=szTabCaptions[2]%>
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <div>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                </td>
                                                                                <td width="95%">
                                                                                    <span style="padding: 1px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">
                                                                                        <asp:Label ID="lblAssociateDoctorHeader" runat="server"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="left">
                                                                                    <asp:DropDownList ID="ddlTestList" runat="server" Width="100%" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlTestList_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="left">
                                                                                    <div style="overflow-y: scroll; height: 500px;">
                                                                                        <asp:DataGrid ID="grdDoctorAssociateProcedureCode"  runat="server"  Width="100%"  CssClass="GridTable"  AutoGenerateColumns="False">
                                                                                           <ItemStyle CssClass="GridRow" />
                                                                                            <HeaderStyle CssClass="GridHeader" />
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="SZ_DOCT_PROC_AMOUNT_ID" HeaderText="PROC_AMOUNT_ID" Visible="False">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Proc ID" Visible="False">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Code">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="P_FLT_AMOUNT" HeaderText="Procedure charge" DataFormatString="{0:0.00}">
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Doctor's charge" DataFormatString="{0:0.00}">
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                        </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="RightCenter" style="width: 10px; height: 100%;">
             <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtDoctorID" runat="server" Width="10px" Visible="false"></asp:TextBox>
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
    </td> </tr> </table>
</asp:Content>



