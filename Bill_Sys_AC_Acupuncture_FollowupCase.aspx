<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AC_Acupuncture_FollowupCase.aspx.cs" Inherits="Bill_Sys_AC_Acupuncture_FollowupCase"
    Title="Acupuncture Followup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register 
        Src="~/UserControl/ErrorMessageControl.ascx" 
        TagName="MessageControl"
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
     <script language = "Javascript" type="text/javascript">
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
                                        <td align="right" valign="middle" visible ="false">
                                            <asp:Label ID="lblPatientFirstName" runat="server" CssClass="ContentLabel" Font-Bold="False" Text="PATIENT'S NAME :"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_FIRST_NAME" runat="server" Width="172px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lbl_doa" runat="server" CssClass="ContentLabel" Font-Bold="False" Text="DOA" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        </td>
                                        <td align="left" valign="middle">
                                         <td align="center" valign="middle">
                                            <asp:Label ID="lblPatientLastName" runat="server" CssClass="ContentLabel" Font-Bold="False" Text="LAST NAME" Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_LAST_NAME" runat="server" Width="172px"   BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                                <td>
                                    <table>
                                    <td align="center" valign="middle">
                                            <asp:Label ID="lblSNO" runat="server" CssClass="ContentLabel" Font-Bold="true" Text="S.NO"></asp:Label>
                                        </td>
                                        <td align="center" valign="middle" style="width: 100px">
                                            <asp:Label ID="LblDate" runat="server" CssClass="ContentLabel" Font-Bold="true" Text="DATE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:Label ID="lblOne" runat="server" CssClass="ContentLabel" Font-Bold="True" Text="1"></asp:Label>
                                        <td align="center" valign="top" style="width:100px;" >
                                            <asp:TextBox ID="TXT_CURRENT_DATE" runat="server" Width="85px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    
                                    </table>
                                
                                </td>
                                        
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0">
                                    <tr>
                                        <td align="center">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td align="center" valign="middle" style="width: 300px">
                                                        <asp:CheckBox ID="CHK_TREATMENT_CODE_97810" CssClass="ContentLabel" runat="server" Text="97810 - Follow Up" Visible="false"/>
                                                        <table style="vertical-align: middle" border="0" width="800px">
                                                         <tbody>
                                                            <tr>
                                                                <asp:Panel runat="server" ID="pnlSrch" Visible="false">
                                                                    <td  text-align: left" valign="top">
                                                                        Search:
                                                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                                            AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                                             Record Count:
                                                                        <%= this.grdProcedure.RecordCount%>
                                                                        |
                                                                        Page Count:
                                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                        </gridpagination:XGridPaginationDropDown>
                                                                        <asp:LinkButton ID="lnkExportToExcel"  runat="server" Visible="false"
                                                                            Text="Export TO Excel"> <%-- OnClick="lnkExportTOExcel_onclick"--%>
                                                                        <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel" visible="false"/></asp:LinkButton>
                                                                        
                                                                    </td>
                                                                    
                                                                  
                                                                    <%--<td style="vertical-align: middle; text-align: right">
                                                                        Page Count:
                                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                        </gridpagination:XGridPaginationDropDown>
                                                                        <asp:LinkButton ID="lnkExportToExcel"  runat="server"
                                                                            Text="Export TO Excel">--%> <%-- OnClick="lnkExportTOExcel_onclick"--%>
                                                                      <%--  <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel" visible="false"/></asp:LinkButton>
                                                                    </td>--%>
                                                                </asp:Panel>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                Procedures
                                                <xgrid:XGridViewControl 
                                                        ID="grdProcedure" 
                                                        runat="server" 
                                                        CssClass="mGrid"  Width="350px"
                                                        AutoGenerateColumns="false"
                                                        AllowSorting="true" 
                                                        PagerStyle-CssClass="pgr" 
                                                        PageRowCount="10" 
                                                        XGridKey="DoctorId"
                                                        AllowPaging="true" 
                                                        ExportToExcelColumnNames=""
                                                        ShowExcelTableBorder="true" 
                                                        ExportToExcelFields=""
                                                        AlternatingRowStyle-BackColor="#EEEEEE" 
                                                        HeaderStyle-CssClass="GridViewHeader"
                                                        ContextMenuID="ContextMenu1" 
                                                        EnableRowClick="false" 
                                                        MouseOverColor="0, 153, 153"
                                                        DataKeyNames="">
                                                        
                                                    <Columns>
                                                    
                                                     <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkProcId" runat="server"/>
                                                            </ItemTemplate>
                                                         </asp:TemplateField>
                                                        
                                                       <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="100%" 
                                                            SortExpression=""
                                                            headertext="Code" DataField="code" />
                                                        
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="40px" 
                                                            visible="true" 
                                                            SortExpression="" 
                                                            headertext="TypeCode"
                                                            DataField="SZ_TYPE_CODE_ID"  />
                                                      
                                                    </Columns>
                                                   </xgrid:XGridViewControl>
                                                                </td>
                                                                <td valign="top" style="width:200px">
                                                                    &nbsp 
                                                                </td>
                                                                <td valign ="top">
                                                                    Doctor's Notes
                                                                    <asp:TextBox ID="txtDocNote" runat="server" TextMode="MultiLine"  Width="300px" 
                                                                        Height="170px" onFocus="return taCount(this,'myCounter')" onKeyPress="return taLimit(this)" 
                                                                        onKeyUp="return taCount(this,'myCounter')"></asp:TextBox>
                                                                          You have <b><SPAN id="myCounter">4000</SPAN></b> characters remaining for your comments.
                                                                </td>
                                                                  <td valign="top">
                                                                  Complaints
                                            <xgrid:XGridViewControl 
                                                        ID="gv_Complaints" 
                                                        runat="server" 
                                                        CssClass="mGrid"  Width="350px"
                                                        AutoGenerateColumns="false"
                                                        AllowSorting="true" 
                                                        PagerStyle-CssClass="pgr" 
                                                        PageRowCount="10" 
                                                        XGridKey="Get_Complaint_Info"
                                                        AllowPaging="true" 
                                                        ExportToExcelColumnNames=""
                                                        ShowExcelTableBorder="true" 
                                                        ExportToExcelFields=""
                                                        AlternatingRowStyle-BackColor="#EEEEEE" 
                                                        HeaderStyle-CssClass="GridViewHeader"
                                                        ContextMenuID="ContextMenu1" 
                                                        EnableRowClick="false" 
                                                        MouseOverColor="0, 153, 153"
                                                        DataKeyNames="Complaint_Id">
                                                        
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkComplaintId" runat="server"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                       <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="100%" 
                                                            SortExpression=""
                                                            headertext="Complaint" DataField="sz_complaint" />
                                                            
                                                           <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="100%" 
                                                            SortExpression=""
                                                            visible="false"
                                                            headertext="Complaint Id" DataField="Complaint_Id" />
                                                    </Columns>
                                             </xgrid:XGridViewControl>
                                            <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="Buttons" Visible="false"  OnClick="btnSave_OnClick"/>
                                        </td>   
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                   
                                                    </td>
                                                    
                                                    <td align="left" valign="middle">
                                                        <asp:CheckBox ID="CHK_TREATMENT_CODE_97813" CssClass="ContentLabel" runat="server" Text="97813 - Follow Up" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="middle" style="width: 202px" visible="false">
                                                        <asp:CheckBox ID="CHK_TREATMENT_CODE_97811" CssClass="ContentLabel" runat="server" Text="97811 - Follow Up" Visible="false" />
                                                    </td>
                                                    <td align="left" valign="middle" visible="false">
                                                        <asp:CheckBox ID="CHK_TREATMENT_CODE_97814" CssClass="ContentLabel"  runat="server" Text="97814 - Follow Up" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                           
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:TextBox ID="txtCompanyID" runat="server" style="display: none;"></asp:TextBox>
                                            <asp:Button ID="css_btnSave" runat="server" Text="SAVE TREATMENTS" OnClick="css_btnSave_Click"
                                                CssClass="Buttons" />&nbsp;
                                            <asp:Button ID="css_btnSign" runat="server" Text="OBTAIN SIGNATURE" OnClick="css_btnSign_Click"
                                                CssClass="Buttons" />&nbsp;
                                            <asp:Button ID="css_btnFinalize" runat="server" Text="FINALIZE TREATMENT" OnClick="css_btnFinalize_Click"
                                                CssClass="Buttons" />&nbsp;
                                            
                                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtDoctorId" runat="Server" style="display: none;" Width="10px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="center" >
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
</asp:Content>
