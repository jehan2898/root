<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_PaidBills.aspx.cs" Inherits="Bill_Sys_PaidBills" ValidateRequest="false"
    Title="Green Your Bills - Paid/Unpaid Bills" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .BackColorTab
        {
            color:black;
            background-color:white;
            fore-color:black;
            
        }
    </style>

   <script type="text/javascript">
        function OpenReport(obj)
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpeciality').click();
           
            
        }
        
         function showUploadFilePopup()
       {
      
           var flag= false;
           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports');
           if(grdProc.rows.length>0)
            {           
                for (var i=1; i<grdProc.rows.length; i++)
                {
                    var cell = grdProc.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {  
                            if (cell.childNodes[j].type =="checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report")
                            {
                                if(cell.childNodes[j].checked)
                                {
                                   flag = true; 
                                   break;
                                }
                            }
                    }
                }
                if(flag==true)
                {
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '10px';
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left ='200px';
	            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height='0px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';  
                //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                    MA.length = 0;
                }
                else
                {
                    alert("Select procedure code ..");
                }   
            }
       }
        
       function CloseUploadFilePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       // 21 April 2010 show ReceiveReport popup -- sachin
       function showReceiveDocumentPopup()
       {
            
            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left= '300px'; 
            document.getElementById('divid').style.top= '100px';              
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src ='Bill_Sys_ReceivedDocumentPopupPage.aspx';  
            return false;
          
       }
         function ShowDiv()
       {
            document.getElementById('divDashBoard').style.position = 'absolute'; 
            document.getElementById('divDashBoard').style.left= '200px'; 
            document.getElementById('divDashBoard').style.top= '120px'; 
            document.getElementById('divDashBoard').style.visibility='visible'; 
            return false;
       }
       
       
        function showProcPopup()
       {
            
            document.getElementById('divid1').style.zIndex = 1;
            document.getElementById('divid1').style.position = 'absolute';
            document.getElementById('divid1').style.left= '300px'; 
            document.getElementById('divid1').style.top= '100px';              
            document.getElementById('divid1').style.visibility='visible'; 
            document.getElementById('frameeditProc').src ='Bill_Sys_EditRProcPopupPage.aspx';  
            return false;
          
       }
       
       function ViewDocumentPopup(caseid,Eventprocid,speciality)
       {
         
            
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left= '300px'; 
            document.getElementById('divid2').style.top= '100px';              
            document.getElementById('divid2').style.visibility='visible'; 
            document.getElementById('frm').src ='Bill_Sys_ViewDocuments.aspx?CaseID='+caseid+'&Type=YES&EProcid='+Eventprocid+'&spc='+speciality;
            return false;
          
       }
 
       
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 0%;
        background-color: white;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 80%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: white;"
                    border="0">
                    <tr>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 99%" runat="server" id="tdPaidUnpaid">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; height: 25px;" colspan="4">
                                                    <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server">Dash board</a>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px;">
                                                 <asp:Label ID="lblBillAmount" runat="server" Text="BillAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                                <asp:Label ID= "lblBillAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblPaidAmount" runat="server" Text="PaidAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                                <asp:Label ID ="lblPaidAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblBalance" runat="server" Text="Balance" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                                <asp:Label ID ="lblBalancevalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                                           
                                            
                                             <asp:Button ID="Button1" runat="server" style="float:left;" CssClass="Buttons" Text="Received Document" OnClick="btnnext_Click" Width="" Visible="False"   />
                                        <asp:Button ID="Button2" runat="server" CssClass="Buttons" Text="Export To Excel" style="float:right;"
                                            OnClick="btnExportToExcel_Click" Visible="false" />
                                        <asp:Button ID="Button3" runat="server" Text="Export To Excel" OnClick="btnSpeciality_Click" Visible="false"/>
                                            </td>
                                            
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 40%; text-align: left">
                                                    <%-- Search:
                                                <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                    AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                </td>
                                                <%-- <td style="width: auto; text-align: right;">
                                                Record Count:
                                                <%= this.grdPaidBillSearch.RecordCount%>
                                                |
                                            </td>--%>
                                                <%--<td style="vertical-align: middle; width: auto; text-align: right">
                                                Page Count:
                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                </gridpagination:XGridPaginationDropDown>
                                                <asp:LinkButton ID="lnkExportToExcel" runat="server"
                                                    Text="Export TO Excel"><%--OnClick="lnkExportTOExcel_onclick"
                                                <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                            </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <contenttemplate>
                                              <XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>
                                                         <%--   <XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                               <ajaxToolkit:TabContainer BorderWidth="1" BorderColor="#808080" ID="tabcontainerPatientVisit" runat="Server" ActiveTabIndex="1" OnActiveTabChanged="tab_changed" AutoPostBack="true" CssClass="BackColorTab">
                                                 <ajaxToolkit:TabPanel runat="server" ID="tabPanelScheduledVisit" Height="120px" >
                                                   <HeaderTemplate>
                                                   <div style="height: 15px;width: 150px; text-align: center; color:White" class="Buttons" runat="server" id="dvpaid">
                                                     Paid Bills
                                                   </div>
                                                   </HeaderTemplate>
                                                  <ContentTemplate>
                                                 <table runat="server" cellspacing="0" cellpadding="0" border="0" width="100%" >
                                                   <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;">
                                                       <table id="Table4" runat="server" cellspacing="0" cellpadding="0" visible="true"
                                                            style="margin:1px; float:left; position:relative; border: solid 1px #B5DF82; width:100%; border-collapse:collapse;">
                                                        <tr style="height:2.5em">
                                                          <td colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:Left;  vertical-align:middle;">
                                                            <table border="0" id="Table8" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                                             <tr>
                                                                <td style="padding-left:3px; width:98%; text-align:center; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                                                color: #000000;	text-decoration: none; ">
                                                               Summary
                                                                </td>
                                        
                                                             </tr>
                                                  </table>                               
                                            </td>
                                        </tr>
                           <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1; color:black">
                                <asp:Label ID="lblBillAmount" runat="server" Text="Billed" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                                        
                            </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                <asp:Label ID= "lblBillAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                            </td>
                           </tr>
                           <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;color:black">
                                  <asp:Label ID="lblPaidAmount" runat="server" Text="Paid"  Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                              </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                  <asp:Label ID ="lblPaidAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                      
                           <%-- <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePanel1">
                                <ProgressTemplate>
                         
                                        <asp:Image ID="img1" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                            
                           
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                             </td>  
                         </tr>
                         <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;color:black">
                                <asp:Label ID="lblBalance" runat="server" Text="Balance" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                                      
                            </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                    <asp:Label ID ="lblBalancevalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                                            
                                                <asp:Button ID="Button1" runat="server" style="float:left;" CssClass="Buttons" Text="Received Document" OnClick="btnnext_Click" Width="" Visible="False"   />
                                     <%--   <asp:Button ID="Button2" runat="server" CssClass="Buttons" Text="Export To Excel" style="float:right;"
                                            OnClick="btnExportToExcel_Click" Visible="false" />--%>
                                                <asp:Button ID="Button3" runat="server" Text="Export To Excel" OnClick="btnSpeciality_Click" Visible="false"/>
                            </td>
                                            
                            </tr>
                            <tr>
                                <td style="color:Black">
                                   Search:
                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                    AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                </td>
                                                                            <%--<td visible="true">
                                                                                 Record Count:
                                                <%= this.grdPaidBillSearch.RecordCount%>
                                                |
                                                                            </td>--%>
                               <td style="width:20px">
                                <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePanel1">
                                    <ProgressTemplate>
         
                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                 <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                     Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                            
           
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                               </td>                             
                               <td style="vertical-align: middle; width: auto; text-align: right ;color:Black">
                                   Record Count:
                                    <%= this.grdPaidBillSearch.RecordCount%>
                                    |
                                    Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" runat="server"
                                            Text="Export TO Excel" OnClick="lnkExportTOExcel_onclick"><%--OnClick="lnkExportTOExcel_onclick"--%> 
                                        <img src="Images/Excel.jpg" 
                                             style="border:none;"  
                                             height="15px" 
                                             width ="15px" 
                                             title = "Export TO Excel"/>
                                        </asp:LinkButton>
                                </td>
                                                                            
                                             </tr>
                                             </table>
                                                                    <%--Xgrid for paid--%>
                                             <table width="100%">
                                               <tr>
                                                 <td>
                                                    <xgrid:XGridViewControl Width="1000px" 
                                                            ID="grdPaidBillSearch" 
                                                            runat="server" 
                                                            CssClass="mGrid" 
                                                            AutoGenerateColumns="false"
                                                            AllowSorting="true" 
                                                            PagerStyle-CssClass="pgr" 
                                                            PageRowCount="50" 
                                                            XGridKey="PaidBill"
                                                            AllowPaging="true" 
                                                            ExportToExcelColumnNames="BILL NUMBER,CASE NO,CHART NO,PATIENT NAME,INSURANCE NAME,BILL DATE,BILL AMOUNT,PAID AMOUNT,BALANCE"
                                                            ShowExcelTableBorder="true" 
                                                            ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_CHART_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_BILL_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE"
                                                            AlternatingRowStyle-BackColor="#EEEEEE" 
                                                            HeaderStyle-CssClass="GridViewHeader"
                                                            ContextMenuID="ContextMenu1" 
                                                            EnableRowClick="true" 
                                                            MouseOverColor="0, 153, 153" OnRowCommand="grdPaidBillSearch_RowCommand"
                                                            DataKeyNames="SZ_BILL_NUMBER" GridLines="None">
                                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                          <%--0--%>
                                                        <asp:TemplateField HeaderText="Treatments" Visible="false">
                                                            <itemtemplate>
                                                                <asp:CheckBox ID="ChkBulkPayment1" runat="server" />
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>
                                                         <%--1--%>
                                                        <asp:TemplateField HeaderText="Bill No." Visible="true" SortExpression="convert(int,SUBSTRING(sz_bill_number,3,LEN(sz_bill_number)))">
                                                            <itemtemplate>
                                                             <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                               Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                               <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>
                                                               <a href="Bill_Sys_BillTransaction.aspx?Type=Search&BillNo=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER") %>&caseid=<%# Eval("SZ_CASE_ID") %>&caseno=<%# Eval("SZ_CASE_NO") %>"><%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%></a>
                                                          <%--   <asp:LinkButton 
                                                                    ID="lblBillno"
                                                                    runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")+","+Eval("SZ_CASE_ID")+","+Eval("SZ_CASE_NO")%>'
                                                                    CommandName="Edit"> 
                                                             </asp:LinkButton>--%>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="left" width="60px" />
                                                        </asp:TemplateField>
                                                        <%--2--%>
                                                          <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="40px" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)"
                                                            headertext="Case #"
                                                            DataField="SZ_CASE_NO" />
                                                      <%-- <asp:TemplateField HeaderText="Case #" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <asp:LinkButton 
                                                              ID="lblcaseno"
                                                              runat="server" 
                                                             Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                               CommandArgument="SZ_CASE_NO"
                                                              CommandName="CASENUMBER"> 
                                                             </asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="left" width="30px" />
                                                        </asp:TemplateField>--%>
                                                        <%--3--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            visible="false" ItemStyle-Width="100%" SortExpression="I_EVENT_ID" headertext="Case #"
                                                            DataField="SZ_CASE_NO" />
                                                        <%--4--%>  
                                                           <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="30px" 
                                                            SortExpression="mst_patient.sz_chart_no "
                                                            headertext="Chart No"
                                                            DataField="SZ_CHART_NO" />
                                                     <%--   <asp:TemplateField HeaderText="Chart No" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <asp:LinkButton 
                                                              ID="lnlChartNo"
                                                              runat="server" 
                                                             Text=' <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>'
                                                               CommandArgument="SZ_CHART_NO"
                                                              CommandName="CHART_NO"> 
                                                             </asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>--%>
                                                        <%--5--%>     
                                                                
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="120px" 
                                                            SortExpression="mst_patient.sz_patient_first_name + ' '+ mst_patient.sz_patient_last_name" 
                                                            headertext="Patient Name"
                                                            DataField="SZ_PATIENT_NAME" />
                                                        
                                                        <%--6--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="190px" 
                                                            visible="true" 
                                                            SortExpression="mst_insurance_company.sz_insurance_name" 
                                                            headertext="Insurance Name" 
                                                            DataField="SZ_INSURANCE_NAME" />
                                                        
                                                        <%--7--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="CaseID" 
                                                            DataField="SZ_CASE_ID" />
                                                        
                                                        <%--8--%>
                                                         <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="Bill Id" 
                                                            DataField="SZ_BILL_NUMBER" />
                                                        <%--9--%>      
                                                          <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="80px" 
                                                            visible="true" 
                                                            headertext="Bill Date" 
                                                            DataField="DT_BILL_DATE"
                                                            SortExpression="dt_bill_date" 
                                                            DataFormatString="{0:MM/dd/yyyy}" />
                                                        <%--<asp:TemplateField  CommandName="BILLDATE"  CommandArgument="DT_BILL_DATE" HeaderText="Bill Date" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                                > 
                                                            
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="60px" />
                                                        </asp:TemplateField>--%>
                                                        
                                                        <%--10--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="BT_STATUS" 
                                                            DataField="BT_STATUS" />
                                                        
                                                        <%--11--%>     
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="Write Off" 
                                                           DataField="FLT_WRITE_OFF" 
                                                           DataFormatString="{0:0.00}" />
                                                         
                                                         <%--12--%>     
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="Paid" 
                                                           DataField="BIT_PAID" />
                                                         
                                                         <%--13--%>  
                                                          <asp:TemplateField HeaderText="Bill Amount" Visible="true"  SortExpression="flt_bill_amount"   >
                                                           <itemtemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                         <%--14--%>  
                                                         <asp:TemplateField HeaderText="Paid Amount" Visible="true" SortExpression="ISNULL((SELECT SUM(flt_check_amount) FROM txn_payment_transactions WHERE sz_bill_id=txn_bill_transactions.sz_bill_number),0)" >
                                                           <itemtemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                          <%--15--%>  
                                                         <asp:TemplateField HeaderText="Balance Amount" Visible="true" SortExpression="txn_bill_transactions.flt_balance">
                                                           <itemtemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                          <%--16--%>  
                                                            <asp:TemplateField 
                                                            HeaderText="Make Payment" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                          <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>  
                                                        <%--17--%>  
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                         <%--18--%>   
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                          <%--19--%>   
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                               <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                        <%--20--%>   
                                                           <asp:TemplateField 
                                                           HeaderText="Generate bill" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                        <%--21--%>   
                                                           <asp:TemplateField 
                                                           HeaderText="Delete" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <asp:CheckBox ID="ChkDelete" runat="server" />
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                         <%--22--%>       
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="COMMENT" 
                                                           DataField="I_PAYMENT_STATE" />
                                                           <%--23--%>     
                                                        <asp:TemplateField visible="false" runat="server">
                                                            <itemtemplate> 
                                                             <itemstyle width="0px" height="0px" />
                                                             
                                                            <tr>
                                                   <%-- <td colspan="100%">--%>
                                                            <td colspan="30%">
                                                     
                                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                                                                        Width="500px" CellPadding="4" ForeColor="#333333" 
                                                                        GridLines="None" visible="true">
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SZ_BILL_ID" HeaderText="Bill No" ItemStyle-Width="25px"></asp:BoundField>
                                                                            <asp:BoundField DataField="SZ_CHECK_NUMBER" ItemStyle-Width="15px" DataFormatString="{0:C}" HeaderText="Check No">
                                                                             <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DT_PAYMENT_DATE" HeaderText="Posted Date" ItemStyle-Width="25px" DataFormatString="{0:MM/dd/yyyy}">
                                                                              <itemstyle horizontalalign="center"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <asp:BoundField DataField="DT_CHECK_DATE" HeaderText="Check Date" ItemStyle-Width="25px" DataFormatString="{0:MM/dd/yyyy}">
                                                                              <itemstyle horizontalalign="center"></itemstyle>
                                                                             </asp:BoundField>
                                                                         
                                                                            <asp:BoundField DataField="FLT_CHECK_AMOUNT" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Check Amount ($)" >
                                                                             <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                                                                                             
                                                                        </Columns>
                                                                  </asp:GridView>
                                                        
                                                        
                                                             
                                                            </td>
                                                           </tr>  
                                                          
                                                          </itemtemplate>
                                                        </asp:TemplateField> 
                                                      </Columns>
                                                    </xgrid:XGridViewControl>
                                                    </td>
                                                                    </tr>
                                                                    </table>
                                                                    <%--End of Xgrid for paid--%>
                                                                    <%--paid grid--%>
                                                                    <asp:DataGrid ID="grdBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                            Width="1000px" CssClass="GridTable" AutoGenerateColumns="false" Visible="false"
                                            AllowPaging="true" PagerStyle-Mode="NumericPages" OnItemDataBound="grdBillSearch_ItemDataBound" OnPageIndexChanged="grdBillSearch_PageIndexChanged" PageSize="50">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--1--%>
                                                <asp:TemplateColumn HeaderText="Bill Number">
                                                <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillno1" runat="server" CommandName="BILLNUMBER" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill Number</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--2--%>
                                                <asp:TemplateColumn HeaderText ="Case #">
                                                      <HeaderTemplate>
                                                        <asp:LinkButton ID="lblcaseno" runat="server" CommandName="CASENUMBER" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                              <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible = "false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="CHART_NO" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No.</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Patient Name" Visible = "true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PATIENT_NAME" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText = "Insurance Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceName" runat ="server" CommandName="InsuranceName" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container, "DataItem.SZ_INSURANCE_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField = "SZ_CASE_ID" HeaderText = "CaseID" Visible = "False"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False"></asp:BoundColumn>
                                                <%--5--%>
                                               <asp:TemplateColumn HeaderText="Bill Date">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOA" runat="server" CommandName="BILLDATE" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
                                               
                                             
                                               <%-- <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date"
                                                    DataFormatString="{0:dd MMM yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off"
                                                    DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundColumn>
                                                <%--8--%>
                                                
                                                 <asp:TemplateColumn HeaderText = "Bill Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillamount" runat ="server" CommandName="BILLAMOUNT" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                               <%-- <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--9--%>
                                                
                                                  <asp:TemplateColumn HeaderText = "Paid Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblPaidAmount" runat ="server" CommandName="PAIDAMOUNT" CommandArgument="PAID_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Paid Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               <%-- <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--10--%>
                                                
                                                <asp:TemplateColumn HeaderText = "Balance">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBalance" runat ="server" CommandName="BALANCE" CommandArgument="FLT_BALANCE"
                                                            Font-Bold="true" Font-Size="12px">Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%--<asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Make Payment" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--14--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--15--%>
                                                <asp:TemplateColumn HeaderText="Generate bill" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--16--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        <%--end of paid grid--%>
                                                                    </ContentTemplate>
                                                              </ajaxToolkit:TabPanel>
                                                       
                                                  
                                                             <ajaxToolkit:TabPanel Width="10px" runat="server" ID="tabPanel1" Height="60%" TabIndex="1">
                                                                    <HeaderTemplate>
                                                                        <div style="width: 150px; text-align: center; color:White" class="Buttons"><%--lbl--%>
                                                                            Unpaid Bill
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                    <table width="100%">
                                                                    <tr>
                                                                     <td class="ContentLabel" style="text-align: left; height: 25px;">
                                                                          <table id="Table5"  runat="server" cellspacing="0" cellpadding="0" border="0" width="100%" >
                                                         
                                                  <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px;">
                                                   <table id="Table6" runat="server" cellspacing="0" cellpadding="0" visible="true"
                        style="margin:1px; float:left; position:relative; border: solid 1px #B5DF82; width:100%; border-collapse:collapse;">
                        <tr style="height:2.5em">
                            <td colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:Left;  vertical-align:middle;">
                                <table border="0" id="Table7" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="padding-left:3px; width:98%; text-align:center; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                       Summary
                                        </td>
                                        
                                    </tr>
                                </table>                               
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;color:black">
                                <asp:Label ID="lblBillAmount1" runat="server" Text="Billed" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                                        
                            </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                <asp:Label ID= "lblBillAmountvalue1" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;color:black">
                                  <asp:Label ID="lblPaidAmount1" runat="server" Text="Paid"  Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                                       
                              </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                  <asp:Label ID ="lblPaidAmountvalue1" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                      
                           
                                    </td>  
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                          <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;color:black">
                                <asp:Label ID="lblBalance1" runat="server" Text="Balance" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                                      
                            </td>
                         <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;color:black">
                                    <asp:Label ID ="lblBalancevalue1" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                       
                       
                        
                    </table>
                                            
                                            
                                            
                                                <asp:Button ID="Button11" runat="server" style="float:left;" CssClass="Buttons" Text="Received Document" OnClick="btnnext_Click" Width="" Visible="False"   />
                                     <%--   <asp:Button ID="Button2" runat="server" CssClass="Buttons" Text="Export To Excel" style="float:right;"
                                            OnClick="btnExportToExcel_Click" Visible="false" />--%>
                                                <asp:Button ID="Button31" runat="server" Text="Export To Excel" OnClick="btnSpeciality_Click" Visible="false"/>
                                            </td>
                                            
                                            </tr>
                                          
                                             </table>
                                                                     
                                          
                                                                    </td>
                                                                    </tr>
                                                                      <tr>
                                                     <td style="color:Black">
                                                       Search:
                                                        <gridsearch:XGridSearchTextBox ID="txtUnpaidSrch" runat="server" CssClass="search-input"
                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                     
                                                                            <%--<td visible="true">
                                                                                 Record Count:
                                                <%= this.grdPaidBillSearch.RecordCount%>
                                                |
                                                                            </td>--%>
                                                    </td>
                                                    <td style="width:30px">
                                                         <asp:UpdateProgress ID="UpdateProgress123" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePanel1">
                                <ProgressTemplate>
                         
                                         <div id="Div2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                             runat="Server">
                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                             Height="25px" Width="24px"></asp:Image>
                                             Loading...</div>
                                            
                           
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                                    </td>
                                                    <td align="right" style="color:Black">
                                                      Record Count:
                                                <%= this.grdUnpaidBill.RecordCount%>
                                                |
                                                        Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="conUnpaid" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                        <asp:LinkButton ID="lnkExportToExcelUnpaid" runat="server"
                                                            Text="Export TO Excel" OnClick="lnkExportToExcelUnpaid_onclick"><%--OnClick="lnkExportTOExcel_onclick"--%> 
                                                        <img src="Images/Excel.jpg" 
                                                             style="border:none;"  
                                                             height="15px" 
                                                             width ="15px" 
                                                             title = "Export TO Excel"/>
                                                        </asp:LinkButton>
                                                     
                                                            </td>                
                                                   </tr>
                                                                    <tr>
                                                                  <td>
                                                                  </table>
                                                                   <xgrid:XGridViewControl Width="1000px" 
                                                            ID="grdUnpaidBill" 
                                                            runat="server" 
                                                            CssClass="mGrid" 
                                                            AutoGenerateColumns="false"
                                                            AllowSorting="true" 
                                                            PagerStyle-CssClass="pgr" 
                                                            PageRowCount="50" 
                                                            XGridKey="UnpaidBill"
                                                            AllowPaging="true" 
                                                            ExportToExcelColumnNames="BILL NUMBER,CASE NO,CHART NO,PATIENT NAME,INSURANCE NAME,BILL DATE,BILL AMOUNT,PAID AMOUNT,BALANCE"
                                                            ShowExcelTableBorder="true" 
                                                            ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_CHART_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_BILL_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE"
                                                            AlternatingRowStyle-BackColor="#EEEEEE" 
                                                            HeaderStyle-CssClass="GridViewHeader"
                                                            ContextMenuID="ContextMenu1" 
                                                            EnableRowClick="true" 
                                                            MouseOverColor="0, 153, 153" OnRowCommand="grdUnpaidBill_RowCommand"
                                                            DataKeyNames="SZ_BILL_NUMBER" GridLines="None">
                                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                          <%--0--%>
                                                        <asp:TemplateField HeaderText="Treatments" Visible="false">
                                                            <itemtemplate>
                                                                <asp:CheckBox ID="ChkBulkPayment2" runat="server" />
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>
                                                         <%--1--%>
                                                        <asp:TemplateField HeaderText="Bill No." Visible="true" SortExpression="convert(int,SUBSTRING(sz_bill_number,3,LEN(sz_bill_number)))">
                                                            <itemtemplate>
                                                             <asp:LinkButton ID="lnkUnP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                               Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ></asp:LinkButton>  
                                                               <asp:LinkButton ID="lnkUnM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>
                                                               <a href="Bill_Sys_BillTransaction.aspx?Type=Search&BillNo=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER") %>&caseid=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&caseno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>"><%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%></a>
                                                          <%--   <asp:LinkButton 
                                                                    ID="lblBillno"
                                                                    runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")+","+Eval("SZ_CASE_ID")+","+Eval("SZ_CASE_NO")%>'
                                                                    CommandName="Edit"> 
                                                             </asp:LinkButton>--%>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="left" width="60px" />
                                                        </asp:TemplateField>
                                                        <%--2--%>
                                                          <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="30px" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)"
                                                            headertext="Case #"
                                                            DataField="SZ_CASE_NO" />
                                                      <%-- <asp:TemplateField HeaderText="Case #" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <asp:LinkButton 
                                                              ID="lblcaseno"
                                                              runat="server" 
                                                             Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                               CommandArgument="SZ_CASE_NO"
                                                              CommandName="CASENUMBER"> 
                                                             </asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="left" width="30px" />
                                                        </asp:TemplateField>--%>
                                                        <%--3--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            visible="false" ItemStyle-Width="100%" SortExpression="I_EVENT_ID" headertext="Case #"
                                                            DataField="SZ_CASE_NO" />
                                                        <%--4--%>  
                                                           <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="30px" 
                                                            SortExpression="mst_patient.sz_chart_no "
                                                            headertext="Chart No"
                                                            DataField="SZ_CHART_NO" />
                                                     <%--   <asp:TemplateField HeaderText="Chart No" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <asp:LinkButton 
                                                              ID="lnlChartNo"
                                                              runat="server" 
                                                             Text=' <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>'
                                                               CommandArgument="SZ_CHART_NO"
                                                              CommandName="CHART_NO"> 
                                                             </asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>--%>
                                                        <%--5--%>     
                                                                
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="120px" 
                                                            SortExpression="mst_patient.sz_patient_first_name + ' '+ mst_patient.sz_patient_last_name" 
                                                            headertext="Patient Name"
                                                            DataField="SZ_PATIENT_NAME" />
                                                        
                                                        <%--6--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            ItemStyle-Width="190px" 
                                                            visible="true" 
                                                            SortExpression="mst_insurance_company.sz_insurance_name" 
                                                            headertext="Insurance Name" 
                                                            DataField="SZ_INSURANCE_NAME" />
                                                        
                                                        <%--7--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="CaseID" 
                                                            DataField="SZ_CASE_ID" />
                                                        
                                                        <%--8--%>
                                                         <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="Bill Id" 
                                                            DataField="SZ_BILL_NUMBER" />
                                                        <%--9--%>      
                                                          <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="80px" 
                                                            visible="true" 
                                                            headertext="Bill Date" 
                                                            SortExpression="dt_bill_date" 
                                                            DataField="DT_BILL_DATE"
                                                            DataFormatString="{0:MM/dd/yyyy}" />
                                                        <%--<asp:TemplateField  CommandName="BILLDATE"  CommandArgument="DT_BILL_DATE" HeaderText="Bill Date" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                             <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                                > 
                                                            
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Center" width="60px" />
                                                        </asp:TemplateField>--%>
                                                        
                                                        <%--10--%>       
                                                        <asp:BoundField 
                                                            HeaderStyle-HorizontalAlign="left" 
                                                            ItemStyle-HorizontalAlign="left"
                                                            visible="false" 
                                                            headertext="BT_STATUS" 
                                                            DataField="BT_STATUS" />
                                                        
                                                        <%--11--%>     
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="Write Off" 
                                                           DataField="FLT_WRITE_OFF" 
                                                           DataFormatString="{0:0.00}" />
                                                         
                                                         <%--12--%>     
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="Paid" 
                                                           DataField="BIT_PAID" />
                                                         
                                                         <%--13--%>  
                                                          <asp:TemplateField HeaderText="Bill Amount" Visible="true"  SortExpression="flt_bill_amount">
                                                           <itemtemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                         <%--14--%>  
                                                         <asp:TemplateField HeaderText="Paid Amount" Visible="true"  SortExpression="ISNULL((SELECT SUM(flt_check_amount) FROM txn_payment_transactions WHERE sz_bill_id=txn_bill_transactions.sz_bill_number),0)" >
                                                           <itemtemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                          <%--15--%>  
                                                         <asp:TemplateField HeaderText="Balance Amount" Visible="true" SortExpression="txn_bill_transactions.flt_balance">
                                                           <itemtemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:C}")%>
                                                           </itemtemplate>
                                                           <itemstyle horizontalalign="right" width="30px" />
                                                         </asp:TemplateField>
                                                          <%--16--%>  
                                                            <asp:TemplateField 
                                                            HeaderText="Make Payment" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                          <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField>  
                                                        <%--17--%>  
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                         <%--18--%>   
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                          <%--19--%>   
                                                           <asp:TemplateField 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                               <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                        <%--20--%>   
                                                           <asp:TemplateField 
                                                           HeaderText="Generate bill" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                        <%--21--%>   
                                                           <asp:TemplateField 
                                                           HeaderText="Delete" 
                                                            Visible="false" 
                                                            SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                            <itemtemplate>
                                                              <asp:CheckBox ID="ChkDelete1" runat="server" />
                                                            </itemtemplate>
                                                             <itemstyle horizontalalign="Center" width="30px" />
                                                        </asp:TemplateField> 
                                                         <%--22--%>       
                                                         <asp:BoundField 
                                                           HeaderStyle-HorizontalAlign="left" 
                                                           ItemStyle-HorizontalAlign="left"
                                                           visible="false" 
                                                           headertext="COMMENT" 
                                                           DataField="I_PAYMENT_STATE" />
                                                           <%--23--%>     
                                                        <asp:TemplateField visible="false">
                                                            <itemtemplate> 
                                                             <itemstyle horizontalalign="Center" width="0px" height="0px" />
                                                            <tr>
                                                   <%-- <td colspan="100%">--%>
                                                            <td colspan="100%">
                                                       <%-- <div id="div<%# Eval("SZ_CASE_ID") %>" style="display: none; position: relative;left: 25px;">--%>
                                                       <%-- <div id="divsa64" style="display:none; position: relative;left: 25px;">--%>
                                                  
                                                             <div id="div1" >
                                                                    <asp:GridView ID="GridViewUnpaid" runat="server" AutoGenerateColumns="false" 
                                                                        Width="500px" CellPadding="4" ForeColor="#333333" 
                                                                        GridLines="None" visible="true">
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SZ_BILL_ID" HeaderText="Bill No" ItemStyle-Width="25px"></asp:BoundField>
                                                                            <asp:BoundField DataField="SZ_CHECK_NUMBER" ItemStyle-Width="15px" DataFormatString="{0:C}" HeaderText="Check No">
                                                                             <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DT_PAYMENT_DATE" HeaderText="Posted Date" ItemStyle-Width="25px" DataFormatString="{0:MM/dd/yyyy}">
                                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <asp:BoundField DataField="DT_CHECK_DATE" HeaderText="Check Date" ItemStyle-Width="25px" DataFormatString="{0:MM/dd/yyyy}">
                                                                              <itemstyle horizontalalign="center"></itemstyle>
                                                                             </asp:BoundField>
                                                                         
                                                                            <asp:BoundField DataField="FLT_CHECK_AMOUNT" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Check Amount ($)" >
                                                                             <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                                                                                             
                                                                        </Columns>
                                                                  </asp:GridView>
                                                             </div>
                                                        
                                                             
                                                            </td>
                                                           </tr>   
                                                          </itemtemplate>
                                                        </asp:TemplateField> 
                                                      </Columns>
                                                    </xgrid:XGridViewControl>
                                                                  </td>
                                                                   
                                                                   <%-- <td>
                                                                     <asp:DataGrid ID="grdUnpaidBill" runat="server" 
                                            Width="1000px" CssClass="GridTable" AutoGenerateColumns="False" Visible="False"
                                            AllowPaging="True"  PageSize="50">
                                            <Columns>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Bill Number">
                                                <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillno" runat="server" CommandName="BILLNUMBER" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill Number</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText ="Case #">
                                                      <HeaderTemplate>
                                                        <asp:LinkButton ID="lblcaseno" runat="server" CommandName="CASENUMBER" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                              <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible = "False"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Chart No">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="CHART_NO" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No.</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Patient Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PATIENT_NAME" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText = "Insurance Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceName" runat ="server" CommandName="InsuranceName" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container, "DataItem.SZ_INSURANCE_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField = "SZ_CASE_ID" HeaderText = "CaseID" Visible = "False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False"></asp:BoundColumn>
                                               <asp:TemplateColumn HeaderText="Bill Date">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOA" runat="server" CommandName="BILLDATE" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off"
                                                    DataFormatString="{0:0.00}" Visible="False">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundColumn>
                                                
                                                 <asp:TemplateColumn HeaderText = "Bill Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillamount" runat ="server" CommandName="BILLAMOUNT" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                  <asp:TemplateColumn HeaderText = "Paid Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblPaidAmount" runat ="server" CommandName="PAIDAMOUNT" CommandArgument="PAID_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Paid Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText = "Balance">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBalance" runat ="server" CommandName="BALANCE" CommandArgument="FLT_BALANCE"
                                                            Font-Bold="true" Font-Size="12px">Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Make Payment" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Generate bill" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                                                    </td>--%>
                                                                   
                                                                   
                                                                    
                                                                    </ContentTemplate>
                                                              </ajaxToolkit:TabPanel>
                                                       </ajaxToolkit:TabContainer>
                                                       </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" align="right">
                                        <asp:Button ID="btnnext" runat="server" Style="float: left;" CssClass="Buttons" Text="Received Document"
                                            OnClick="btnnext_Click" Width="125px" Visible="False" />
                                        <asp:Button ID="btnSpeciality" runat="server" Text="Export To Excel" OnClick="btnSpeciality_Click"
                                            Width="0px" Height="0px" Visible="false" />
                                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                            Style="float: right;" OnClick="btnExportToExcel_Click" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                        <%-- <asp:TextBox ID="txtCompanyId" runat="server" Visible="False"></asp:TextBox>--%>
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%">
                                        <%--<asp:Label ID ="lblTotal" runat="server" Text="Total" Font-Bold="True" ></asp:Label>--%>
                                        <%-- <asp:Label ID="lblBillAmount" runat="server" Text="BillAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                        <asp:Label ID= "lblBillAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblPaidAmount" runat="server" Text="PaidAmount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                        <asp:Label ID ="lblPaidAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblBalance" runat="server" Text="Balance" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp;
                                        <asp:Label ID ="lblBalancevalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>--%>
                                        <asp:DataGrid ID="grdCaseMaster" Width="100%" runat="Server" CssClass="GridTable"
                                            OnItemCommand="grdCaseMaster_ItemCommand" AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--1--%>
                                                <asp:TemplateColumn HeaderText="Case #" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--2--%>
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--3--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:TemplateColumn HeaderText="Patient" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_blank">
                                                            <%-- <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>--%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--5--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="false">
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="PROVIDER ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="SZ_PROVIDER_NAME" HeaderText="Provider" Visible="False">
                                                </asp:BoundColumn>
                                                <%--9--%>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_ID" HeaderText="INSURANCE ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                <%--11--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ID" HeaderText="OFFICE ID" Visible="False"></asp:BoundColumn>
                                                <%--12--%>
                                                <%--13--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundColumn>
                                                <%--14--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="DOCTOR ID" Visible="False"></asp:BoundColumn>
                                                <%--15--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundColumn>
                                                <%--16--%>
                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}">
                                                </asp:BoundColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="SZ_CLAIM_AMOUNT" HeaderText="Claim Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--18--%>
                                                <asp:BoundColumn DataField="SZ_PAID_AMOUNT" HeaderText="Paid Amount" Visible="False">
                                                </asp:BoundColumn>
                                                <%--19--%>
                                                <asp:BoundColumn DataField="SZ_BALANCE" HeaderText="Balance" Visible="False"></asp:BoundColumn>
                                                <%--20--%>
                                                <asp:BoundColumn DataField="SZ_APPOINTMENT" HeaderText="Today's Appointment" Visible="false">
                                                </asp:BoundColumn>
                                                <%--21--%>
                                                <asp:TemplateColumn HeaderText="Diagnosis Code" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_AssociateDignosisCode.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                            target="_self" shape="rect">Update </a>
                                                        <%--(<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>)--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--22--%>
                                                <asp:TemplateColumn HeaderText="Visit" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientVisits.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Visit</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--23--%>
                                                <asp:TemplateColumn HeaderText="Treatment" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Treatments.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Treatment</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--24--%>
                                                <asp:TemplateColumn HeaderText="Test" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_Tests.aspx?PatientID=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>"
                                                            target="_self" shape="rect">Test</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--25--%>
                                                <asp:BoundColumn DataField="SZ_CASE_STATUS_ID" HeaderText="STATUS ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--26--%>
                                                <asp:BoundColumn DataField="SZ_STATUS_NAME" HeaderText="Status" Visible="false"></asp:BoundColumn>
                                                <%--27--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="ATTORNEY ID" Visible="False">
                                                </asp:BoundColumn>
                                                <%--28--%>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Attorney" Visible="false">
                                                </asp:BoundColumn>
                                                <%--29--%>
                                                <asp:TemplateColumn HeaderText="Documents" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--30--%>
                                                <asp:TemplateColumn HeaderText="Templates" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Template" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Template Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--31--%>
                                                <asp:TemplateColumn HeaderText="Schedule" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCalendarEvent" runat="server" Text="Calender Event" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>'
                                                            CommandName="Calender Event">
																		 <img src="Images/cal.gif" style="border:none;" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--32--%>
                                                <asp:TemplateColumn HeaderText="Bills" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBillTransaction" runat="server" Text="Add | " CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Bill Transaction"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkViewBills" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="View Bills"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--33--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--34--%>
                                                <asp:TemplateColumn HeaderText="Desk">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Patient Desk"
                                                            ToolTip="Patient Desk">
																		                
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--35--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                        <%--Search Bill--%>
                                        <asp:DataGrid ID="grdBillSearch1" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" Visible="false"
                                            AllowPaging="true" PagerStyle-Mode="NumericPages" OnItemDataBound="grdBillSearch_ItemDataBound"
                                            OnPageIndexChanged="grdBillSearch_PageIndexChanged" PageSize="50">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--1--%>
                                                <asp:TemplateColumn HeaderText="Bill Number">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillno" runat="server" CommandName="BILLNUMBER" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill Number</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--2--%>
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblcaseno" runat="server" CommandName="CASENUMBER" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="CHART_NO" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No.</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PATIENT_NAME" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Insurance Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceName" runat="server" CommandName="InsuranceName"
                                                            CommandArgument="SZ_INSURANCE_NAME" Font-Bold="true" Font-Size="12px">Insurance Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container, "DataItem.SZ_INSURANCE_NAME") %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="False"></asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False"></asp:BoundColumn>
                                                <%--5--%>
                                                <asp:TemplateColumn HeaderText="Bill Date">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlDOA" runat="server" CommandName="BILLDATE" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date"
                                                    DataFormatString="{0:dd MMM yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:0.00}"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundColumn>
                                                <%--8--%>
                                                <asp:TemplateColumn HeaderText="Bill Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBillamount" runat="server" CommandName="BILLAMOUNT" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--9--%>
                                                <asp:TemplateColumn HeaderText="Paid Amount">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblPaidAmount" runat="server" CommandName="PAIDAMOUNT" CommandArgument="PAID_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Paid Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--10--%>
                                                <asp:TemplateColumn HeaderText="Balance">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblBalance" runat="server" CommandName="BALANCE" CommandArgument="FLT_BALANCE"
                                                            Font-Bold="true" Font-Size="12px">Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:N2}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--<asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance"
                                                    DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>--%>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Make Payment" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                            CommandName="Make Payment"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.0</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.2</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--14--%>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                            target="_blank">Edit W.C. 4.3</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--15--%>
                                                <asp:TemplateColumn HeaderText="Generate bill" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--16--%>
                                                <asp:TemplateColumn HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        <%--End--%>
                                        <%--Grid used for Export to excel--%>
                                        <asp:DataGrid ID="grdEEBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="false"
                                            PagerStyle-Mode="NumericPages" OnItemDataBound="grdBillSearch_ItemDataBound"
                                            OnPageIndexChanged="grdBillSearch_PageIndexChanged" PageSize="50" Visible="false">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:dd MMM yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:0.00}"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        <%--End--%>
                                        <div style="overflow: scroll; height: 200px" visible="false" id="dvAllReport" runat="server">
                                            <asp:DataGrid ID="grdAllReports" runat="server" AutoGenerateColumns="false" CssClass="GridTable"
                                                OnPageIndexChanged="grdAllReports_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                                Width="100%" Visible="false" OnItemCommand="grdAllReports_ItemCommand">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--0--%>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--1--%>
                                                    <asp:TemplateColumn HeaderText="CHART NO">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkChartNoSearch" runat="server" CommandName="CHART_NO" CommandArgument="CHART_NO"
                                                                Font-Bold="true" Font-Size="12px">Chart #</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CHART_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--2--%>
                                                    <asp:TemplateColumn HeaderText="PATIENT NAME">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkCaseNoSearch" runat="server" CommandName="CASE_NO" CommandArgument="CASENO"
                                                                Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CASE_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--3--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                    <%--4--%>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--5--%>
                                                    <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="PATIENT_NAME" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--6--%>
                                                    <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="DT_DATE_OF_SERVICE" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--7--%>
                                                    <asp:TemplateColumn HeaderText="PATIENT NAME">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PATIENT_NAME"
                                                                CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkWorkAreaPage" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>'
                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="workarea"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--8--%>
                                                    <asp:TemplateColumn HeaderText="Date Of Service">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkDateOfServiceSearch" runat="server" CommandName="DT_EVENT_DATE"
                                                                CommandArgument="DT_EVENT_DATE" Font-Bold="true" Font-Size="12px">Date Of Service</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAppointment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>'
                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>'
                                                                CommandName="appointment"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--9--%>
                                                    <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure code"></asp:BoundColumn>
                                                    <%--10--%>
                                                    <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                    <%--11--%>
                                                    <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                    <%--12--%>
                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="CompanyID" Visible="false"></asp:BoundColumn>
                                                    <%--13--%>
                                                    <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="EventProcID" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--14--%>
                                                    <asp:BoundColumn DataField="SZ_PROC_CODE" HeaderText="Pro Code" Visible="false"></asp:BoundColumn>
                                                    <%--15--%>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                    </asp:BoundColumn>
                                                    <%--16--%>
                                                    <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No." Visible="false"></asp:BoundColumn>
                                                    <%--17--%>
                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Case No." Visible="false"></asp:BoundColumn>
                                                    <%--18--%>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                        Visible="false"></asp:BoundColumn>
                                                    <%--19--%>
                                                    <asp:BoundColumn DataField="sz_procedure_group" HeaderText="sz_procedure_group" Visible="false">
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                        </div>
                                        <br />
                                        <asp:DataGrid ID="grdExportToExcel" runat="server" PageSize="50" AllowPaging="true"
                                            AutoGenerateColumns="false" CssClass="GridTable" Visible="False">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Service"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%; height: auto; float: left;">
                                        <div id="divid" style="position: absolute; width: 600px; height: 0px; background-color: #DBE6FA;
                                            visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                            border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                                <a onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;"
                                                    style="cursor: pointer;" title="Close">X</a>
                                            </div>
                                            <iframe id="frameeditexpanse" src="" frameborder="0" height="0px" width="600px"></iframe>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%-- <td class="RightCenter" style="width: 10px; height: 0%;">
                        </td>--%>
                    </tr>
                    <%-- <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <div id="divDashBoard" visible="false" style="position: absolute; width: 800px; height: 0px;
        background-color: Yellow; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <%--background-color: #DBE6FA;--%>
        <div style="position: relative; text-align: right; background-color: Yellow;">
            <%--background-color: #8babe4;--%>
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="1" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 0; float: left; position: relative;" visible="false">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 0%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftCenter" style="height: 0%">
                            </td>
                            <td style="width: 97%" class="TDPart">
                                <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 99%; height: 0px; float: left; position: relative; left: 0px; top: 0px;
                                    vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Specialty</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%; height: 10px;" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Today's Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%">
                                            Weekly &nbsp;Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; vertical-align: top; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Bill Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%;" valign="top">
                                            Desk</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have&nbsp;
                                            <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Information</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%;" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Report Section</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative; left: 0px; top: 0px;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Procedure Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 0px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Visits</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblVisits" runat="server" Visible="true"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul style="list-style-type: disc; padding-left: 60px;">
                                                            <li><a id="hlnkTotalVisit" href="#" runat="server">
                                                                <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit</li><li>
                                                                    <a id="hlnkBilledVisit" href="#" runat="server">
                                                                        <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                </li>
                                                            <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                            </li>
                                                        </ul>
                                                        <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                            PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                            PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                            PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 33%; height: 0px; float: left; position: relative; left: 0px; top: 0px;
                                    vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Patient Visit Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server" Height="0px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" visible="false">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false" Visible="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <%-- <asp:BoundoClumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server" Height="0px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 0px"
            visible="false">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" AutoGenerateColumns="false"
                        Visible="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server" Height="0px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" visible="false">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" AutoGenerateColumns="false"
                        Visible="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
</asp:Content>
