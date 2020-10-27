<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ReffPaidBills.aspx.cs" Inherits="Bill_Sys_ReffPaidBills" Title="Untitled Page" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    

    <script language="javascript" type="text/javascript">
    
     function openURL(url)
        {
            if(url == "")
            {
                alert("There is no bill created for the selected record!");
            }
            else
            {
                var url1 = url;
                window.open(url1, "","top=10,left=100,menubar=no,toolbar=no,location=no,resizable=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=0,titlebar=no;");
            }
        }
        
     function isRecordsselected()
       {
       
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdAllReports");	
            var IsCheckd=false;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" && f.getElementsByTagName("input").item(i).checked==true)		
			    {						
			            IsCheckd=true;
			     
			    }			
		    }
		    if (IsCheckd==true){return true;} else { alert('Please select records'); return false;}
		    
       }
    
    function showReceiveDocumentPopup()
       {
            
            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left= '300px'; 
            document.getElementById('divid').style.top= '100px';              
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src ='/Bill_Sys_ReceivedReportPopupPage.aspx';  
            return false;
          
       }
        function ValidateGridCheckBox(objPatientID)
        {
            var index = 3;
            var patientId = objPatientID;
            var cnt = 0;
            chkbox=document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports' + '__ctl' + index + '_chkSelect');
            //alert('begin');
            while(chkbox != null)
            {
                if(chkbox.checked == true)
                {
                    cnt = cnt + 1;
                    //alert ("count:" + cnt);
                    index = index + 1;
                    chkbox=document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports' + '__ctl' + index + '_chkSelect');
                    //document.getElementById('_ctl0_ContentPlaceHolder1_hndJavaScriptDocID').value = objDocID;
                    //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hndJavaScriptDocID').value);
                }
                else
                {
                    //alert("else:" + index);
                    index = index + 1;
                    chkbox=document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports' + '__ctl' + index + '_chkSelect');

                }
            }
            
            if(cnt > 1)
            {
                var previouspatientID = document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value;
                
                if(previouspatientID == patientId)
                {
                    //alert('more');
                    return true;
                }
                else
                {
                    //alert('Select same patient only');
                    return false;
                }
                //document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value
            }
            else if(cnt == 0)
            {
                var i_chkbox;
                var i_Index = 2;
                i_chkbox=document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports' + '__ctl' + i_Index + '_chkSelect');
                while(i_chkbox != null)
                {
                    i_chkbox.checked = false;
                    i_Index = i_Index + 1;
                    chkbox=document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports' + '__ctl' + i_Index + '_chkSelect');
                }
                //alert("Select CheckBox");
                //__doPostBack('_ctl0_ContentPlaceHolder1_grdCompleteVisit __ctl2_chkSelectItem','');
                return true;
            }
            else if(cnt == 1)
            {
                //alert('First select');
                if(document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value == '')
                {
                    document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value = objPatientID;
                    //alert(document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value);
                    return true;
                }
                else if(document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value != '')
                {
                    if(objPatientID == document.getElementById('_ctl0_ContentPlaceHolder1_HndPatientID').value)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            
        }
        
        
        function SelectAll(ival)
       {
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdAllReports");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
       
        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '100px'; 
            document.getElementById('divid2').style.visibility='visible';  
                     
            return false;            
        }
        
        function CancelExistPatient()
        {
            //document.getElementById('divid2').style.zIndex = 1;
            //document.getElementById('divid2').style.position = 'absolute'; 
            //document.getElementById('divid2').style.left= '360px'; 
            //document.getElementById('divid2').style.top= '100px';
            document.getElementById('divid2').style.height = '0px'; 
            document.getElementById('divid2').style.visibility='hidden';  
                    
            return false;            
        }
       
        function openExistsPage1()
        {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute'; 
            document.getElementById('div1').style.left= '360px'; 
            document.getElementById('div1').style.top= '100px'; 
            document.getElementById('div1').style.visibility='visible'; 
                     
            return false;            
        }
        
        function CancelExistPatient1()
        {
            //document.getElementById('divid2').style.zIndex = 1;
            //document.getElementById('divid2').style.position = 'absolute'; 
            //document.getElementById('divid2').style.left= '360px'; 
            //document.getElementById('divid2').style.top= '100px';
            document.getElementById('div1').style.height = '0px'; 
            document.getElementById('div1').style.visibility='hidden';   
                  
            return false;            
        }
       
       function ShowDiv()
       {
            document.getElementById('divDashBoard').style.position = 'absolute'; 
            document.getElementById('divDashBoard').style.left= '200px'; 
            document.getElementById('divDashBoard').style.top= '120px'; 
            document.getElementById('divDashBoard').style.visibility='visible'; 
            document.getElementById("<%=  extddlCaseType.ClientID%>").style.visibility='hidden'; 
            return false;
       }
       
    function Close()
    {
    document.getElementById('divDashBoard').style.visibility='hidden';
    document.getElementById("<%=  extddlCaseType.ClientID%>").style.visibility='visible';
    }
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td  colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    
                    
                    
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                
                                <tr>
                                    <td style="width: 90%" class="TDPart" align="left">
                                        <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server">Dash board</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90%" align="center">
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr><td style="width: 90%" class="TDPart" align="left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 31%">
                                    Case Type
                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="66%" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                 OldText="" StausText="False" /></td>
                                        &nbsp;<td style="width: 10%">
                                       <asp:Button ID="btnSearch" CssClass="Buttons" Text="Search"
                                            runat="server"   OnClick="btnSearch_Click" /></td>
                                     <td style="width: 10%"> <asp:Button ID="btnRevert" CssClass="Buttons" Text="Revert"
                                            runat="server" OnClick="btnRevert_Click" Width="68px"  /></td>
                                    <td style="width: 19%"><asp:Button ID="btnPerPatient" CssClass="Buttons" Text="Create Bill Per Patient"
                                            runat="server" OnClick="btnPerPatient_Click"  Width="93%"/></td>
                                        <td style="width: 17%"> <asp:Button ID="btnSelectedBill" CssClass="Buttons" Text="Create Selected Bill" runat="server"
                                            OnClick="btnSelectedBill_Click" Width="93%"/></td>
                                        
                                   <td style="width: 14%"> <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                            OnClick="btnExportToExcel_Click" Width="96%"/>
                                       
                                    </td>
                                </tr>
                                </table> 
                                </td></tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtRefCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtBillDate" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtReadingDocID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <%--<asp:HiddenField ID="HndPatientID" runat="server" />
                                        <asp:HiddenField ID="hndJavascriptsPatientID" runat="server" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="TDPart">
                                        <asp:DataGrid ID="grdEEBillSearch" runat="Server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" Visible="false">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:BoundColumn DataField="CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Service"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle CssClass="GridRow" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                        <div style="overflow: scroll; height: 600px">
                                        <asp:DataGrid ID="grdAllReports" runat="server" AutoGenerateColumns="false" CssClass="GridTable"
                                            Width="100%" Visible="true" OnItemCommand="grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--1--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                <%--2--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false">
                                                </asp:BoundColumn>
                                                <%--3--%>
                                                
                                               <%-- <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart #"></asp:BoundColumn>--%>
                                                <asp:TemplateColumn HeaderText="Chart #">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkChartNumber" runat="server" CommandName="ChartNumberSort" CommandArgument="CHART_NO" Font-Bold="true" Font-Size="12px">Chart #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--4--%>
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkCaseNoSearch" runat="server" CommandName="CaseNoSort" CommandArgument="CASENO" Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--5--%>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Service" Visible="false">
                                                </asp:BoundColumn>
                                                <%--6--%>
                                                <asp:TemplateColumn HeaderText="Edit <br/>Reading doctor <br/> Diagnosis code ">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" 
                                                            CommandName="Edit" ToolTip="Edit" Text="Edit" >
                                                        </asp:LinkButton>
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--7--%>
                                                <asp:TemplateColumn HeaderText="Patient name">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PatientNameSort" CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientName" runat="server" 
                                                            CommandName="PatientClick" ToolTip="Edit" Text='<%# DataBinder.Eval(Container, "DataItem.PATIENT_NAME")%>' >
                                                        </asp:LinkButton>
                                                           
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               <%--8--%>
                                               <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type Name"></asp:BoundColumn>
                                                <%--9--%>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <%--11--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                                 <%--12--%>
                                                <asp:TemplateColumn HeaderText="Date Of Service">
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="lnkDateOfServiceSearch" runat="server" CommandName="DateOfServiceSort" CommandArgument="DATE_SORT" Font-Bold="true" Font-Size="12px">Date Of Service</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.DT_DATE_OF_SERVICE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                <%--14--%>
                                                <asp:BoundColumn DataField="SZ_CODE_ID" HeaderText="Code ID" Visible="false"></asp:BoundColumn>
                                                <%--15--%>
                                                <asp:BoundColumn DataField="SZ_EVENT_ID" HeaderText="EVENT ID" Visible="false"></asp:BoundColumn>
                                                <%--16--%>
                                                <asp:BoundColumn DataField="DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="CASE_NO" HeaderText="CASE NO" Visible="false"></asp:BoundColumn>
                                                <%--18--%>
                                                <asp:BoundColumn DataField="Company_ID" HeaderText="Company ID" Visible="false"></asp:BoundColumn>
                                                <%--19--%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="SZ_PATIENT_TREATMENT_ID" Visible="false"></asp:BoundColumn>
                                                <%--20--%>
                                                <asp:BoundColumn DataField="Insurance_Company" HeaderText="Insurance Company" Visible="false"></asp:BoundColumn>
                                                <%--21--%>  
                                                <asp:BoundColumn DataField="Insurance_Address" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                <%--22--%>
                                                <asp:BoundColumn DataField="CLAIM_NO" HeaderText="CLAIM NO" Visible = "false"></asp:BoundColumn>
                                                <%--23--%>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Speciality" Visible = "false"></asp:BoundColumn>
                                                   
                                                  <%--24--%>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="false"></asp:BoundColumn>
                                                						<%--25--%>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                <%--26--%>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        Report
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href="<%# DataBinder.Eval(Container, "DataItem.REPORT PATH")%>" target="_blank">
                                                        <img src="Images/grid-doc-mng.gif" style="border:none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                	<%--27--%>
                                                <asp:BoundColumn DataField="SZ_STUDY_NUMBER" HeaderText="Study Number"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                            <td style="width: 99%; height:auto; float:left;"  class="TDPart">
     
                                              <div id="divid" style="position: absolute; width: 600px; height: 600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="600px"></iframe>
    </div>
                                            </td>
                                        </tr>
                                <tr>
                                    <td class="TDPart" align="right">
                                         <asp:TextBox ID="txtSort" runat="server" Visible="False"></asp:TextBox>
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
    
     <div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        
         
            <span id="msgPatientExists"  runat="server"></span><br /><br /><br />
            <%--<asp:Button ID="btnOK" runat ="server" OnClick="btnOK_Click" Text = "OK" CssClass="Buttons" />--%>
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
            
        <br />
        <%--div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />--%>
                
                <div style="text-align: center;">
            
        </div>
        </div>
    
    <div id="div1" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient1();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="popupmsg"  runat="server"></span><br /><br /><br />
            <%--<asp:Button ID="btnOK1" runat ="server" OnClick="btnOK1_Click" Text = "OK" CssClass="Buttons" />--%>
            <input type="button" class="Buttons" value="Cancel" id="btnCancel1" onclick="CancelExistPatient1();"
                style="width: 80px;" />
            </div>
        <br />
        <%--div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />--%>
                
                <div style="text-align: center;">
            
        </div>
        </div>
    
    
    <div id="divDashBoard" visible="false" style="position: absolute; width: 800px; height: 475px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="Close()" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 430; float: left; position: relative;">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td style="width: 97%" class="TDPart">
                            <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Speciality</td>
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
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
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
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
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
                                    style="width: 32%; height: 195px; vertical-align: top; float: left; position: relative;"
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
                                    height: 195px; float: left; position: relative;" visible="false">
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
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
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
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
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
                                    style="width: 32%; height: 195px; float: left; position: relative; left: 0px;
                                    top: 0px;" visible="false">
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
                                    height: 195px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
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
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
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
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
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
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
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
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
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
