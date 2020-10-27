<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" MasterPageFile="~/MasterPage.master" 
CodeFile="NinetyDaysBillingReport.aspx.cs" Inherits="AJAX_Pages_NinetyDaysBillingReport" AsyncTimeout="10000" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
     
      
      function chkLitigate()
      {
        var f= document.getElementById("<%=grdNinetyDaysReport.ClientID%>");
        var bfFlag = false;	
        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
        {		
		  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
            {
                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
	            {						
	                if(f.getElementsByTagName("input").item(i).checked != false) 
	                {
	                    bfFlag = true;
	                }
	            }
	        }
        }
        if(bfFlag == false)
        {
            alert('Select record to litigate');
            return false;
        }
	  }
	 
	  function chkForSearch()
	  {
	    var objSelDoc1 = document.getElementById("<%=lstMenusToRole.ClientID%>");
	    var objDays = document.getElementById("<%=txtDays.ClientID%>");
	    
	    if (objSelDoc1.options.length==0)
	    {
	        alert('Select atleast 1 office name');
	        return false;
	    }
	    else if(objDays.value=="")
	    {
	        alert('Enter days to search');
	        return false;
	    }
	    else
	    {
	        return true;
	    }
	  }
	  function chkLitigateAll()
	  {
	  
	    var objSelDoc2 = document.getElementById("<%=lstMenusToRole.ClientID%>");
	    if (objSelDoc2.options.length==0)
	    {
	        alert('Select atleast 1 office name');
	        return false;
	    }
	  }
        
       function MoveRight()
        {
        
            var sel = document.getElementById("<%=lstMenu.ClientID%>");
            var objSelDoc = document.getElementById("<%=lstMenusToRole.ClientID%>");
            var selNodeInlist = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>");
            var seltextNode=document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>");
            var listLength = sel.options.length;
            //selNodeInlist.value="";
           
            for (var i = listLength-1; i >= 0; i--) 
            {
                if (sel.options[i].selected) 
                {
               
                   var no = new Option();
                   no.value = sel.options[i].value;
                   no.text = sel.options[i].text;
                   
                   flag = 0;
                   for(j=0;j<objSelDoc.options.length;j++)
                   {
                        if(no.text == objSelDoc.options[j].text)
                        {
                            flag = 1;
                        }
                   }  
                   if(flag == 0)
                   {
                       objSelDoc[objSelDoc.options.length] = no; 
                       selNodeInlist.value = selNodeInlist.value + no.value+"," ;
                       seltextNode.value=seltextNode.value+no.text+";";
                   }
                   sel.remove(i);
                }
            }
        }
        
       function MoveLeft()
        {
       
            var sel = document.getElementById("<%=lstMenusToRole.ClientID%>");
            var objSel = document.getElementById("<%=lstMenu.ClientID%>");
            var listLength = sel.options.length;
           
            var arrText = new Array();
            var arrValue = new Array();
            var count = 0;
            for (var i = 0; i < listLength; i++) 
            {
                if(!sel.options[i].selected)
                {
                  var no = new Option();
                  arrText[count] = sel.options[i].text;
                  arrValue[count] = sel.options[i].value;
                  count++;
                }
                else
                {
                    document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value.replace(sel.options[i].value +",", "");
                    document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value.replace(sel.options[i].text +";", "");
                    var no = new Option();
                    no.text = sel.options[i].text;  //lstmenurole=sel
                    no.value = sel.options[i].value;
                    objSel[objSel.options.length] = no;
                }
            }
            sel.length = 0;
            for(index = 0; index < arrText.length; index++)
            {
                var no = new Option();
                no.value = arrValue[index];
                no.text = arrText[index];
                sel[index] = no;
            }
        }
       function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdNinetyDaysReport.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		        {	
		            if(f.getElementsByTagName("input").item(i).disabled==false)
		            {
                        f.getElementsByTagName("input").item(i).checked=ival;
                    }
		       
			    }	
		    }
       }
       
        
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
</asp:ScriptManager>
    <table width="100%" style="background-color:white;" >
   <tr>
    <td>
     <b>Select OFFICE</b>

    </td>
   </tr>
   <tr>
    <td>
       <table>
        <tr>
             <td style="width: 357px; height: 300px;">
                     <asp:ListBox ID="lstMenu" runat="server" Width="490px" SelectionMode="Multiple" Height="300px" ></asp:ListBox>
                   </td>
                   <td class="ContentLabel" style="width: 57px; text-align:center; height: 300px;">
                    <input type="button" onclick="MoveRight();" value=">>" style="font-size: 12px;color: #000099;font-family: arial;background-color: #8babe4;border: 1px solid #000099;	padding-top:1px;padding-bottom:1px;padding-left:5px;padding-right:5px;"/>
       
                    <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                        padding-left: 5px; font-size: 12px; padding-bottom: 1px; border-left: #000099 1px solid;
                        color: #000099; padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial;
                        background-color: #8babe4" id="Button1" class="Buttons" title="Move Left"
                        onclick="MoveLeft();" type="button" value="<<" />
                    <asp:Button ID="btnMoveLeft" runat="server" Text=">>" visible="false"/><%--OnClick="btnMoveLeft_Click"--%> 
                    <asp:Button ID="btnMoveRight" runat="server"  Text="<<" visible="false"/><%--OnClick="btnMoveRight_Click"--%>
                     <%--<input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenu,this.form.ctl00_ContentPlaceHolder1_lstMenusToRole);" value=">>" />
                           <input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenusToRole,this.form.ctl00_ContentPlaceHolder1_lstMenu);" value="<<" />--%>
                   </td>
                   <td style="width:386px; height: 300px;">
                        <asp:ListBox ID="lstMenusToRole" runat="server" Width="490px" EnableViewState="true" SelectionMode="Multiple" Height="300px">
                        </asp:ListBox>
                   </td>
                   <asp:HiddenField runat="server" ID="hfselectedNodeinListbox"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hfselectedNodeTextinListbox"></asp:HiddenField>

        </tr>
        <tr>
        <td valign="bottom">
           <asp:TextBox ID="txtDays" runat="server" Text="90"></asp:TextBox>
                        <div style="position:absolute; top:410px; left:370px" style="width:400px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <asp:CheckBox ID="chkBalance" runat="server" Text="Balance not zero"/>
                         <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnSearch_Click" />
                         <asp:Button id="btnLitigate" onclick="btnLitigate_onclick" runat="server" Text="Litigate"></asp:Button>
                         <asp:Button id="btnLitigateAll"  runat="server" Text="Litigate All" onclick="btnLitigateAll_onclick"></asp:Button>
                        
                          </ContentTemplate>
                          
                          
                        </asp:UpdatePanel> 
                        </div>
                        <asp:UpdateProgress id="UpdateProgress121" AssociatedUpdatePanelID="UpdatePanel2"  runat="server"  DisplayAfter="0">
                         <ProgressTemplate>

                            <div  id="Div11" style="text-align: right; vertical-align: bottom; " class="PageUpdateProgress"
                                 runat="Server">
                             <asp:Image ID="img21" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                 Height="25px" Width="24px"></asp:Image>
                                 Loading...</div>
                         </ProgressTemplate>
                        </asp:UpdateProgress>
                       
                        <asp:TextBox ID="txtchkBalance" runat="server" Visible="false"></asp:TextBox>
                        <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
        </td>
        </tr>
       </table> 
    </td>
   </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
        <table width="100%">
        <tr>
        <td>
        
                   <table style="VERTICAL-ALIGN: middle; WIDTH: 100%" border="0" id="tblSrch" visible="false">
                     <tbody>
                       <tr>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 35%" align="left">
                            <asp:Label id="Label2" runat="server" Text="Search:" Font-Size="Small" Font-Bold="True"></asp:Label> 
                            <gridsearch:XGridSearchTextBox id="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                            </gridsearch:XGridSearchTextBox> 
                        </td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 40%" align="left">
                         <asp:UpdateProgress id="UpdateProgress12" AssociatedUpdatePanelID="UpdatePanel1"  runat="server"  DisplayAfter="0">
                         <ProgressTemplate>

                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                 runat="Server">
                             <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                 Height="25px" Width="24px"></asp:Image>
                                 Loading...</div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                             
                        </td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 100%; TEXT-ALIGN: center" align="right" >
                       
                        <asp:LinkButton id="lnkshow" onclick="lnkshow_onclick" runat="server" Text="Show Details" />
                             Record Count:<%= this.grdNinetyDaysReport.RecordCount%> | Page Count: 
                            <gridpagination:XGridPaginationDropDown id="con" runat="server">
                            </gridpagination:XGridPaginationDropDown>
                                 <asp:LinkButton id="lnkExportToExcel" onclick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>
                            </asp:LinkButton> 
                        </td>
                        </tr>
                        <tr>
                            
                        </tr>
                     </tbody>
                   </table>
                   </td>
                   </tr>
                   <tr>
                   <td style="width:100%";>
                      
                        <xgrid:XGridViewControl 
                             id="grdNinetyDaysReport" 
                             runat="server" Width="100%" 
                             CssClass="mGrid" DataKeyNames="Bill_no" 
                             AllowSorting="true" PagerStyle-CssClass="pgr" 
                             PageRowCount="50" XGridKey="NinetyReport" 
                             GridLines="None" AllowPaging="true" 
                             AlternatingRowStyle-BackColor="#EEEEEE" 
                           ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" 
                              EnableRowClick="false" 
                             ShowExcelTableBorder="true" ExcelFileNamePrefix="BillSearch" 
                             MouseOverColor="0, 153, 153" AutoGenerateColumns="false" 
                             ExportToExcelColumnNames="Bill#,Case#,Patient Name,Insurance Name,Insurance Address,Patient Street,Patient City,Patient State,Patient Zip,Patient Phone,Date of accident,Policy no,Claim No,Case Status,Attorney,Attorney Name,Attorney Address,Attorney City,Attorney State,Attorney Zip,Attorney Fax,SSN No,Date of birth,Policy Holder,Specialty,Visit Date,Bill Date,Bill Status,Bill Amount,Paid,Balance" 
                             ExportToExcelFields="Bill_no,Case_no,Patient_Name,Ins_Name,Ins_Address,Patient_Street,Patient_City,Patient_State,Patient_Zip,Patient_Phone,Date_of_accident,Policy_no,Claim_no,Case_Status,Attorney,Attorney_Name,Attorney_Address,Attorney_City,Attorney_State,Attorney_Zip,Attorney_Fax,SSN_No,Date_of_birth,Policy_Holder,SPECIALITY,PROC_DATE,DT_BILL_DATE,SZ_BILL_STATUS_NAME,Bill,Paid,Outstanding"> 
                             <%--OnRowCommand="grdNinetyDaysReport_RowCommand  ,DataKeyNames="SZ_BILL_NUMBER,SZ_BILL_STATUS_CODE,SZ_CASE_ID,FLT_BILL_AMOUNT,FLT_BALANCE,PROC_DATE,BT_TRANSFER,SZ_CASE_NO,SZ_ABBRIVATION_ID,SZ_BILL_STATUS_NAME""--%>
                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                          <Columns>
                           <%--0--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                              visible="false" headertext="Case ID" DataField="SZ_CASE_ID" />
                           <%--1--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                              SortExpression="convert(int,SUBSTRING(sz_bill_number,3,LEN(sz_bill_number)))" headertext="Bill#" DataField="Bill_no" />
                            <%--CAST(MCM.SZ_CASE_NO as int)"--%>
                           <%--2--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="MST_CASE_MASTER.SZ_CASE_NO" headertext="Case#" DataField="Case_no" />
                           <%--3--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="MST_PATIENT.SZ_PATIENT_LAST_NAME + ' ' + MST_PATIENT.SZ_PATIENT_FIRST_NAME " headertext="Patient Name" DataField="Patient_Name" />
                           <%--4--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_INSURANCE_NAME,'')" headertext="Insurance Name" DataField="Ins_Name" />
                           <%--5--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(MST_INSURANCE_ADDRESS.SZ_INSURANCE_ADDRESS,'')" headertext="Insurance Address" DataField="Ins_Address" />
                           <%--6--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_PATIENT_STREET,'')" headertext="Patient Street" DataField="Patient_Street" />
                           <%--7--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_PATIENT_CITY,'') " headertext="Patient City" DataField="Patient_City" />
                           <%--8--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull((Select SZ_STATE_CODE FROM MST_STATE WHERE I_STATE_ID=SZ_PATIENT_STATE_ID),'')" headertext="Patient State" DataField="Patient_State" />
                           <%--9--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_PATIENT_ZIP,'')" headertext="Patient Zip" DataField="Patient_Zip" />
                           <%--10--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_PATIENT_PHONE,'')" headertext="Patient Phone" DataField="Patient_Phone" />
                           <%--11--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="convert(nvarchar(10),DT_DATE_OF_ACCIDENT,106)" headertext="Date Of Accident" DataField="Date_of_accident" />
                           <%--12--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_POLICY_NUMBER,'')" headertext="Policy No" DataField="Policy_no" />
                           <%--13--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_CLAIM_NUMBER,'')" headertext="Claim No" DataField="Claim_no" />
                           <%--14--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_STATUS_NAME,'') " headertext="Case Status" DataField="Case_Status" />
                           <%--15--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_FIRST_NAME,'')" headertext="Attorney" DataField="Attorney" />
                           <%--16--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_LAST_NAME,'') " headertext="Attorney Name" DataField="Attorney_Name" />
                           <%--17--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_ADDRESS,'')" headertext="Attorney Address" DataField="Attorney_Address" />
                           <%--18--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_CITY,'')" headertext="Attorney City" DataField="Attorney_City" />
                           <%--19--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull((Select SZ_STATE_CODE FROM MST_STATE WHERE I_STATE_ID=SZ_ATTORNEY_STATE_ID),'')" headertext="Attorney State" DataField="Attorney_State" />
                           <%--20--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_ZIP,'')" headertext="Attorney Zip" DataField="Attorney_Zip" />
                           <%--21--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_ATTORNEY_FAX,'')" headertext="Attorney Fax" DataField="Attorney_Fax" />
                           <%--22--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_SOCIAL_SECURITY_NO,'')" headertext="SSN No" DataField="SSN_No" />
                           <%--23--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(convert(nvarchar(10),DT_DOB,106),'')" headertext="Date of birth" DataField="Date_of_birth" />
                           <%--24--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                            SortExpression="isnull(SZ_POLICY_HOLDER,'')" headertext="Policy Holder" DataField="Policy_Holder" />
                           <%--25--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" 
                            SortExpression=" (select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP 
		                    where SZ_PROCEDURE_GROUP_ID =
		                    (select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=
		                    (select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER )) )"
                            headertext="Specialty" DataField="SPECIALITY" />
                           <%--26--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                             SortExpression = "	convert(nvarchar(10),TXN_BILL_TRANSACTIONS.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TXN_BILL_TRANSACTIONS.DT_LAST_VISIT_DATE,101)"
                             headertext="Visit Date" DataField="PROC_DATE" />
                           <%--27--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                             SortExpression ="isnull(convert(nvarchar(11),TXN_BILL_TRANSACTIONS.DT_BILL_DATE ,106),'')"
                             headertext="Bill Date" DataField="DT_BILL_DATE" />
                           <%--28--%>
                           <asp:TemplateField HeaderText="Bill Status" SortExpression="MST_BILL_STATUS.SZ_BILL_STATUS_NAME">
                                <itemtemplate>
                                    <asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>'></asp:Label>
                                </itemtemplate>
                           </asp:TemplateField>
                           <%--29 --%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                             SortExpression="TBT.FLT_WRITE_OFF" headertext="Write Off" DataFormatString="{0:C}"
                             DataField="FLT_WRITE_OFF" visible="false" />
                           <%--30 --%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                             headertext="Bill Amount" SortExpression="cast(TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT as numeric(13,2))"
                             DataFormatString="{0:C}" DataField="Bill" />
                           <%--31--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                             headertext="Paid" DataFormatString="{0:C}" DataField="Paid"  SortExpression=" cast(TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT as numeric(13,2)) - cast(TXN_BILL_TRANSACTIONS.FLT_BALANCE as numeric(13,2))"/>
                           <%--32--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                             headertext="Outstanding" DataFormatString="{0:C}" SortExpression="cast(TXN_BILL_TRANSACTIONS.FLT_BALANCE as numeric(13,2))"
                             DataField="Outstanding" />
                           <%--33--%>
                           <asp:TemplateField HeaderText="">
                                <headertemplate>
                                  <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                </headertemplate>
                                <itemtemplate>
                                 <asp:CheckBox ID="ChkDelete" runat="server" />
                                </itemtemplate>
                           </asp:TemplateField>
                           <%--34--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                visible="false" headertext="bill status code" DataField="SZ_BILL_STATUS_CODE" />
                           <%--35--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                headertext="Bill Amount" DataField="FLT_BILL_AMOUNT" visible="false" />
                           <%--36--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                headertext="Balance" visible="false" DataField="FLT_BALANCE" />
                            <%--37--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                headertext="Balance" visible="false" DataField="BT_TRANSFER" />
                            <%--38--%>
                           <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                headertext="CaseType" visible="false" DataField="SZ_ABBRIVATION_ID" />
                           </Columns>
                        </xgrid:XGridViewControl> 
                       
                       <div style="width:40%">
                        <asp:GridView ID="grdSearchTotal" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333">
                        <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true"/>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                               
                                <asp:BoundField  ItemStyle-HorizontalAlign="Right" DataField="Bill_Amount" DataFormatString="{0:C}"  HeaderText="Total Bill ($)">
                                 
                                </asp:BoundField>
                                 <asp:BoundField  DataField="Paid_Amount"  DataFormatString="{0:C}"  ItemStyle-HorizontalAlign="Right"    HeaderText="Total Paid($)">
                                
                                </asp:BoundField>
                                 <asp:BoundField DataField="Outstanding_Amount" DataFormatString="{0:C}" HeaderText="Total Outstanding($)"   ItemStyle-HorizontalAlign="Right">
                                
                                </asp:BoundField>
                            </Columns>
                          </asp:GridView>
                       </div>
                       <table>
                        <tr>
                        <td style="visibility:hidden;">
                            <asp:TextBox ID="showcolumn" runat="server" ></asp:TextBox>
                                <asp:TextBox ID="ShowColumnTest" runat="server" ></asp:TextBox>
                                <asp:HiddenField ID="hdlshow" runat="server" />
                        
                    </td>
                        </tr>
                        </table>
                   
        </td>
        </tr>
        </table>
         </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlId="btnSearch" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
              </asp:UpdatePanel>

    
    <asp:TextBox ID="txtCompId" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtOffice" runat="server" Visible="false"></asp:TextBox>
            


</asp:Content>