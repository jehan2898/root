<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="~/Bill_Sys_PatientVisitSummary.aspx.cs" Inherits="Bill_Sys_PatientVisitSummary" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">

       function ShowConfirmation() {

           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdCaseMaster');
           var count = 0;
           var countVisitType = 0;
            var idstatus=0;
           if (grdProc.rows.length > 0) {
               var count = 0;
               for (var i = 1; i < grdProc.rows.length; i++) {
                   var cell = grdProc.rows[i].cells[0];
                   for (j = 0; j < cell.childNodes.length; j++) {
                       if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                           if (cell.childNodes[j].checked) {
                               count = count + 1;
                           }
                       }
                   }
               }
           }
           
           if (grdProc.rows.length > 0) 
           {
                               
              

                 
               


                if (count > 0) 
                {
                    
                }
                else 
                {
                    alert('Please select patient');
                    return false;
                }
            }
            else {
                return false;
            }
        }
        
        function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdCaseMaster.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
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
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <b>Patient Visit Stats Report </b>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="left" style="vertical-align: top;">
                                                        <asp:Button ID="btnViewReport" runat="server" Text="View Report" Width="80px" OnClick="btnViewReport_Click"
                                                            CssClass="Buttons" />
                                                        <%--<asp:LinkButton ID="Button1" style="visibility:hidden;" runat="server" OnClick="Button1_Click"></asp:LinkButton>
                                                        <a id="hlnkShowSearch" style="cursor:pointer;height:240px; vertical-align:top;"  class="Buttons" runat="server" title ="Quick Search" >Quick Search</a>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseIDSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="TextBox1" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" class="SectionDevider" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:DataGrid Width="100%" ID="grdCaseMaster" CssClass="GridTable" runat="server"
                                                        AutoGenerateColumns="False">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" onclick="javascript:SelectAll(this.checked);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case no." ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="true">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Case Id" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient name" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_CREATED_DATE" HeaderText="Date Open" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Patient Phone" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
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
</asp:Content>
