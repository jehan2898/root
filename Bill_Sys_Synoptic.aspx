<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Synoptic.aspx.cs" Inherits="Bill_Sys_Synoptic" Title="Synoptic" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register  Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
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
                          
                            <asp:Label ID="lblHeading" runat="server" Text="SYNAPTIC FOLLOW-UP" Style="font-weight: bold;" class="td-widget-bc-search-desc-ch"></asp:Label>
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
                                            <asp:Label ID="lblPatientFirstName" runat="server" CssClass="ContentLabel" Font-Bold="true" Text="PATIENT'S NAME :" ></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_FIRST_NAME" runat="server" Width="172px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lbl_doa" runat="server" CssClass="ContentLabel" Font-Bold="False" Text="DOA" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox> 
                                        </td>
                                         
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
                                    <tr>
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
                                            </td> 
                                        <td align="center" valign="top" style="width:100px;" >
                                            <asp:TextBox ID="TXT_CURRENT_DATE" runat="server" Width="85px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    
                                    </table>
                                
                                </td>
                                        
                        </tr>
                        <tr>
                            <td>
                             <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 70%;height: 50%; border: 1px solid #B5DF82;">
                              <tr>
                                <td style="width: 40%; height: 0px;" align="center">
                                <table border="0" cellpadding="0" cellspacing="0" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                    style="width: 100%">
                                    <tr>
                                        <td align="left" bgcolor="#b5df82" class="txt2" colspan="2" height="28" valign="middle">
                                            <b class="txt3">Save Parameters</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch" style="width:50%; height: 19px;">
                                            Treatment Time</td>
                                        <td class="td-widget-bc-search-desc-ch" style="width:50%;height: 19px">
                                            Area</td>                                        
                                    </tr>
                                    <tr>
                                        <td align="center" style="width:50%;height: 24px;">
                                            <asp:DropDownList ID="DrpTreatmentTime" runat="server">
                                            <asp:ListItem Value="0">15 min</asp:ListItem>
                                            <asp:ListItem Value="1">20 min</asp:ListItem>
                                            <asp:ListItem Value="2">25 min</asp:ListItem>
                                            <asp:ListItem Value="3">30 min</asp:ListItem>
                                            <asp:ListItem Value="4">35 min</asp:ListItem>
                                            <asp:ListItem Value="5">40 min</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                        <td align="center" style="width:50%; height: 24px">
                                        <extddl:ExtendedDropDownList ID="extddlArea" runat="server" Width="88%" Selected_Text="---Select---"
                                        Procedure_Name="SP_GET_SPECIALITY_BY_DOCTOR" Flag_Key_Value="GET_CMPLAINT_LIST" Connection_Key="Connection_String">
                                            </extddl:ExtendedDropDownList>
                                            &nbsp;</td>                                        
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1" style="width:50%;height: 20px;">
                                            LOP Before</td>
                                        <td class="td-widget-bc-search-desc-ch1" style="width:50%;height: 20px">
                                            LOP After</td>                                        
                                    </tr>
                                    <tr>
                                        <td align="center" style="width:50%;">
                                        <asp:TextBox ID="Slider2" runat="server"   style="right:0px" Text="0" />
                                        <asp:Label ID="Slider2_BoundControl" runat="server" style="text-align:right" />
                                        <ajaxToolkit:SliderExtender ID="SliderLOPBefore" runat="server"
                                        TargetControlID="Slider2"
                                        Minimum="1"
                                        Maximum="10"
                                        BoundControlID="Slider2_BoundControl"
                                        Steps="10" />
                                            &nbsp;</td>
                                        <td class="td-widget-bc-search-desc-ch3" align="center" style="width:50%;">
                                            &nbsp;
                                         <asp:TextBox ID="Slider1" runat="server"   style="right:0px" Text="0" />
                                         <asp:Label ID="Slider1_BoundControl" runat="server" style="text-align:right" />
                                         <ajaxToolkit:SliderExtender ID="SliderLOPAfter" runat="server"
                                        TargetControlID="Slider1"
                                        Minimum="1"
                                        Maximum="10"
                                        BoundControlID="Slider1_BoundControl"
                                        Steps="10" />
                                          </td>                                        
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch" align="center" style="width:50%;">
                                            Intensity</td>
                                        <td align="center" class="td-widget-bc-search-desc-ch" style="width:50%;">
                                            Bias&nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="center" style="width:50%;height: 19px">
                                         <asp:TextBox ID="Slider4" runat="server"   style="right:0px" Text="0" />
                                         <asp:Label ID="Slider4_BoundControl" runat="server" style="text-align:right" />
                                         <ajaxToolkit:SliderExtender ID="SliderIntensity" runat="server"
                                        TargetControlID="Slider4"
                                        Minimum="1"
                                        Maximum="10"
                                        BoundControlID="Slider4_BoundControl"
                                        Steps="10" />
                                        </td>
                                        <td align="center" class="td-widget-bc-search-desc-ch3" style="width:50%;height: 19px">
                                            <asp:ListBox ID="Listbias" runat="server">
                                                <asp:ListItem Selected="True">0.1</asp:ListItem>
                                                <asp:ListItem>0.2</asp:ListItem>
                                                <asp:ListItem>0.3</asp:ListItem>
                                                <asp:ListItem>0.4</asp:ListItem>
                                                <asp:ListItem>0.5</asp:ListItem>
                                                <asp:ListItem>0.6</asp:ListItem>
                                                <asp:ListItem>0.7</asp:ListItem>
                                                <asp:ListItem>0.8</asp:ListItem>
                                                <asp:ListItem>0.9</asp:ListItem>
                                                <asp:ListItem>1.0</asp:ListItem>
                                                <asp:ListItem>1.1</asp:ListItem>
                                                <asp:ListItem>1.2</asp:ListItem>
                                                <asp:ListItem>1.3</asp:ListItem>
                                                <asp:ListItem>1.4</asp:ListItem>
                                                <asp:ListItem>1.5</asp:ListItem>
                                                <asp:ListItem>1.6</asp:ListItem>
                                                <asp:ListItem>1.7</asp:ListItem>
                                                <asp:ListItem>1.8</asp:ListItem>
                                                <asp:ListItem>1.9</asp:ListItem>
                                                <asp:ListItem>2.0</asp:ListItem>
                                                <asp:ListItem>2.1</asp:ListItem>
                                                <asp:ListItem>2.2</asp:ListItem>
                                                <asp:ListItem>2.3</asp:ListItem>
                                                <asp:ListItem>2.4</asp:ListItem>
                                                <asp:ListItem>2.5</asp:ListItem>
                                                <asp:ListItem>2.6</asp:ListItem>
                                                <asp:ListItem>2.7</asp:ListItem>
                                                <asp:ListItem>2.8</asp:ListItem>
                                                <asp:ListItem>2.9</asp:ListItem>
                                                <asp:ListItem>3.0</asp:ListItem>
                                                <asp:ListItem>3.1</asp:ListItem>
                                                <asp:ListItem>3.2</asp:ListItem>
                                                <asp:ListItem>3.3</asp:ListItem>
                                                <asp:ListItem>3.4</asp:ListItem>
                                                <asp:ListItem>3.5</asp:ListItem>
                                                <asp:ListItem>3.6</asp:ListItem>
                                                <asp:ListItem>3.7</asp:ListItem>
                                                <asp:ListItem>3.8</asp:ListItem>
                                                <asp:ListItem>3.9</asp:ListItem>
                                                <asp:ListItem>4.0</asp:ListItem>
                                                <asp:ListItem>4.1</asp:ListItem>
                                                <asp:ListItem>4.2</asp:ListItem>
                                                <asp:ListItem>4.3</asp:ListItem>
                                                <asp:ListItem>4.4</asp:ListItem>
                                                <asp:ListItem>4.5</asp:ListItem>
                                                <asp:ListItem>4.6</asp:ListItem>
                                                <asp:ListItem>4.7</asp:ListItem>
                                                <asp:ListItem>4.8</asp:ListItem>
                                                <asp:ListItem>4.9</asp:ListItem>
                                                <asp:ListItem>5.0</asp:ListItem>
                                                <asp:ListItem>5.1</asp:ListItem>
                                                <asp:ListItem>5.2</asp:ListItem>
                                                <asp:ListItem>5.3</asp:ListItem>
                                                <asp:ListItem>5.4</asp:ListItem>
                                                <asp:ListItem>5.5</asp:ListItem>
                                                <asp:ListItem>5.6</asp:ListItem>
                                                <asp:ListItem>5.7</asp:ListItem>
                                                <asp:ListItem>5.8</asp:ListItem>
                                                <asp:ListItem>5.9</asp:ListItem>
                                                <asp:ListItem>6.0</asp:ListItem>
                                                <asp:ListItem>6.1</asp:ListItem>
                                                <asp:ListItem>6.2</asp:ListItem>
                                                <asp:ListItem>6.3</asp:ListItem>
                                                <asp:ListItem>6.4</asp:ListItem>
                                                <asp:ListItem>6.5</asp:ListItem>
                                                <asp:ListItem>6.6</asp:ListItem>
                                                <asp:ListItem>6.7</asp:ListItem>
                                                <asp:ListItem>6.8</asp:ListItem>
                                                <asp:ListItem>6.9</asp:ListItem>
                                                <asp:ListItem>7.0</asp:ListItem>
                                                <asp:ListItem>7.1</asp:ListItem>
                                                <asp:ListItem>7.2</asp:ListItem>
                                                <asp:ListItem>7.3</asp:ListItem>
                                                <asp:ListItem>7.4</asp:ListItem>
                                                <asp:ListItem>7.5</asp:ListItem>
                                                <asp:ListItem>7.6</asp:ListItem>
                                                <asp:ListItem>7.7</asp:ListItem>
                                                <asp:ListItem>7.8</asp:ListItem>
                                                <asp:ListItem>7.9</asp:ListItem>
                                                <asp:ListItem>8.0</asp:ListItem>
                                                <asp:ListItem>8.1</asp:ListItem>
                                                <asp:ListItem>8.2</asp:ListItem>
                                                <asp:ListItem>8.3</asp:ListItem>
                                                <asp:ListItem>8.4</asp:ListItem>
                                                <asp:ListItem>8.5</asp:ListItem>
                                                <asp:ListItem>8.6</asp:ListItem>
                                                <asp:ListItem>8.7</asp:ListItem>
                                                <asp:ListItem>8.8</asp:ListItem>
                                                <asp:ListItem>8.9</asp:ListItem>
                                                <asp:ListItem>9.0</asp:ListItem>
                                                <asp:ListItem>9.1</asp:ListItem>
                                                <asp:ListItem>9.2</asp:ListItem>
                                                <asp:ListItem>9.3</asp:ListItem>
                                                <asp:ListItem>9.4</asp:ListItem>
                                                <asp:ListItem>9.5</asp:ListItem>
                                                <asp:ListItem>9.6</asp:ListItem>
                                                <asp:ListItem>9.7</asp:ListItem>
                                                <asp:ListItem>9.8</asp:ListItem>
                                                <asp:ListItem>9.9</asp:ListItem>
                                            </asp:ListBox>
                                            &nbsp;
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch" style="width:50%;">
                                            Doctor Notes</td>
                                        <td class="td-widget-bc-search-desc-ch" style="width:50%;">
                                        </td>                                       
                                    </tr>
                                    <tr>
                                        <td align="center" class="td-widget-bc-search-desc-ch" style="width:50%;">
                                                                     <asp:DropDownList ID="DrpDoctorNotes" runat="server">
                                                                    <asp:ListItem Value="0">Patients Reacts Very Well To Treatment</asp:ListItem>
                                                                    <asp:ListItem Value="1">Patient Request No  More Treatment</asp:ListItem>
                                                                    <asp:ListItem Value="2">Patient Request More Treatment</asp:ListItem>                                                                     
                                                                    </asp:DropDownList></td>
                                        <td class="td-widget-bc-search-desc-ch" style="width:50%;">
                                        </td>                                        
                                    </tr>                                 
                                </table>
                                  </td>
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
                                                    <td align="left" valign="middle" style="width: 300px">
                                                        <asp:CheckBox ID="CHK_TREATMENT_CODE_97810" CssClass="ContentLabel" runat="server" Text="97810 - Follow Up" Visible="false"/>
                                                        <table style="vertical-align: middle" border="0" width="800px">
                                                         <tbody>
                                                            <tr>
                                                                <asp:Panel runat="server" ID="pnlSrch" Visible="false">
                                                                    <td  style="text-align:left" valign="top">
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
                                                                </asp:Panel>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="height: 167px">
                                                                <asp:Label ID="Label1" runat="server" Text="Procedures" Style="font-weight: bold;" class="td-widget-bc-search-desc-ch"></asp:Label>
                                                                
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
                                                                <td valign="top" style="width:200px; height: 167px;">
                                                                <asp:Label ID="Label3" runat="server" Text="Doctor Notes" Style="font-weight: bold;" class="td-widget-bc-search-desc-ch"></asp:Label>
                                                                    
                                                                    <br />
                                                                    <asp:TextBox ID="txtDoctorNotes" runat="server" TextMode="MultiLine" Height="96px" Width="275px"></asp:TextBox></td>
                                                                  <td valign="top" style="height: 167px">
                                                                  <asp:Label ID="Label2" runat="server" Text="Complaints" Style="font-weight: bold;" class="td-widget-bc-search-desc-ch"></asp:Label>
                                                                  
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
                                                 <asp:Button ID="btnDiagnosysCodes" runat="server" Text="DIAGNOSYS CODES" OnClick="OnClick_btnDagnosys_Click"
                                                CssClass="Buttons"  Enabled="false"/>&nbsp;
                                            <asp:Button ID="css_btnFinalize" runat="server" Text="FINALIZE TREATMENT" OnClick="css_btnFinalize_Click"
                                                CssClass="Buttons" />&nbsp;
                                            
                                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                            <asp:TextBox ID="txtDoctorId" runat="Server" style="display: none;" Width="10px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                        <gridpagination:XGridPaginationDropDown ID="con1" runat="server" Visible="false">
                                        </gridpagination:XGridPaginationDropDown>
                                        </td>
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
