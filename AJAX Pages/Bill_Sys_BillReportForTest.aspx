<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_BillReportForTest.aspx.cs" Inherits="Bill_Sys_BillReportForTest" Title="Green Your Bills - Bill Report" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">       
     
     function UpdateStatus()
           {  
           var bfFlag = false;	
           var msg = "";
           var flag = false;
              //if(document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value == 'NA')
               
               if(document.getElementById("<%=extddlBillStatus.ClientID %>").value == 'NA'){
                    msg = 'Please select Bill Status .. !';
                    flag = true;
               }
               else
               {
 
               }   
              
               var f= document.getElementById('<%= grdAllReports.ClientID%>');
		       
		
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
//                  alert(f.getElementsByTagName("input").length);
//				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') !=-1)
//		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    bfFlag = true;
			                }			                
			            }
			      //  }			
		        }
		        if(bfFlag == false)
		        {
		            alert(msg + ' Please select Bill no..!');
		            flag = false;
		            return false;
		        }
		        else
		        {
		            if(flag == true)
		            {
		                alert(msg);
		                return false;
		            }
		           else
		           {
		           return true;
		           }
		        }    
           }

    function Validate(ival)
       {
            
            var f= document.getElementById("<%= grdAllReports.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            return true;
			        }
			    }			
		    }
		    alert('Please select record.');
		    return false;
       }
        function openURL(url)
        {
            if(url == "")
            {
                alert("Files not found!");
            }
            else
            {             
                 var bname = navigator.appName; 
		   //check type of the browser. if browser is Firefox or chrome, load plugin otherwise load activex control.
		        if(bname == "Netscape")
		        {
                    var url1 = url;
                    window.open(url1, "","top=10,left=100,menubar=no,toolbar=no,location=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=1,titlebar=no;");
                }
                else
                {
                    window.location.href =url;
                }
                 __doPostBack('btn_CreatePacket','My Argument');
            }
        }
        
    function CloseviewBills()
       {
            document.getElementById("<%= pnlviewBills.ClientID %>").style.height='0px';
            document.getElementById("<%= pnlviewBills.ClientID %>").style.visibility = 'hidden';  
       }
     function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdAllReports.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
     function ascii_value(c){
             c = c . charAt (0);
             var i;
             for (i = 0; i < 256; ++ i)
             {
                  var h = i . toString (16);
                  if (h . length == 1)
                    h = "0" + h;
                   h = "%" + h;
                  h = unescape (h);
                  if (h == c)
                    break;
             }
             return i;
        }
    function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
            
            
         }
             
         function openErrorMessage()
        {
            document.getElementById('div3').style.zIndex = 1;
            document.getElementById('div3').style.position = 'absolute'; 
            document.getElementById('div3').style.left= '360px'; 
            document.getElementById('div3').style.top= '250px'; 
            document.getElementById('div3').style.visibility='visible';           
            return false;            
        }
        
        function CancelErrorMassage()
        {
         document.getElementById('div3').style.visibility='hidden';           
        }
        //Function For POM Conformation
        function POMConformation()
        {           
           if (Validate())
           {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute'; 
            document.getElementById('div1').style.left= '360px'; 
            document.getElementById('div1').style.top= '250px'; 
            document.getElementById('div1').style.visibility='visible';  
            return false;
            }
            else
            {
                return false;
            }
        }
        
        
         function Validate()
       {
            
            var f= document.getElementById("<%= grdAllReports.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            return true;
			        }
			    }			
		    }
		    alert('Please select record.');
		    return false;
       }
                
        function YesMassage()
        {         
        //document.getElementById("<%= hdnPOMValue.ClientID%>").value="Yes";           
        document.getElementById('div1').style.visibility='hidden';           
        }
        function NoMassage()
        {        
        //document.getElementById("<%= hdnPOMValue.ClientID%>").value="No";         
        document.getElementById('div1').style.visibility='hidden'; 
        }
        function CheckValidation()
        {                        
        alert("Please Select Record");
        }
        //End Of Function
        
        
        function DisabledAll()
       {
           // alert('all');
            var f= document.getElementById("<%= grdAllReports.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
	    	    {		
		            
		            f.getElementsByTagName("input").item(i).disabled=true;
		        
		            //if(f.getElementsByTagName("input").item(i).disabled==false)
		            //{
                      //  f.getElementsByTagName("input").item(i).checked=ival;
                    //}
                   // alert('call');
                }
             }
     }
			    
			    	
		   
    </script>
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
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
                        <td class="LeftCenter" style="height: 322px">
                        </td>
                        <td class="Center" valign="top" style="height: 322px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; height: 25px; width: 25%;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                     <b> Bill Report &nbsp;</b>                                                      
                                                    </td>
                                                    <td style="text-align: left; height: 25px; width: 10%;" >
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label></td>
                                                    <td style="text-align: left; height: 25px; width: 10%;">
                                                    <asp:Label ID="lblMessage" runat="server" Style="color: red" Visible="false"> </asp:Label>&nbsp;</td>
                                                    <td style="text-align: left; height: 25px; width: 55%;" >
                                                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                                                    <contenttemplate>
                                                  
&nbsp; &nbsp; <asp:Image id="Image1" runat="server" ImageUrl="~/Images/loading_transp.gif" Visible="False" AlternateText="[image]"></asp:Image><%--<B><asp:Label id="Label15" runat="server" Width="186px" visible="False" Text="Processing, Please wait..." Font-Bold="True"></asp:Label>&nbsp;</B> --%>
                                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                <td class="TDPart">
                                <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                   <tr>
                                                                                                           
                                    <td style="width: 31%" align="right">
                                        Bill Status<extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Connection_Key="Connection_String"
                                            Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL" Procedure_Name="SP_MST_BILL_STATUS"
                                            Selected_Text="---Select---" Width="170px" /></td> 
                                            <td style="width: 13%"  align="center">
                                        <asp:Button ID="btnUpdateStatus" runat="server" CssClass="Buttons" OnClick="btnUpdateStatus_Click"
                                            Text="Update Status" Width="117px" /></td> 
                                            <td style="width: 5%"  align="right">
                                        &nbsp;<asp:UpdatePanel id="UpdatePanel2" runat="server"><contenttemplate>
                                        <asp:Button ID="btn_CreatePacket" runat="server" CssClass="Buttons" OnClick="btn_CreatePacket_Click"
                                            Text="Create Packet" Width="118px" />
                                            <asp:Timer id="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="False" Interval="10000">
                                            </asp:Timer>&nbsp; &nbsp; </contenttemplate>
                                        </asp:UpdatePanel></td> 
                                        <td style="width: 10%"  align="right">
                                     <asp:Button ID="btnPrintPOM" runat="server" CssClass="Buttons" Text="Print POM"  OnClick="btnPrintPOM_Click" /></td> 
                                     <td style="width: 11%"  align="right">
                                    <asp:Button ID="btnPrintEnvelop" runat="server" CssClass="Buttons" Text="Print Envelop" OnClick="btnPrintEnvelop_Click" Width="114px" /> </td> 
                                    <td style="width: 11%"  align="right">
                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" Width="115px" />
                                    </td> 
                                    </tr>
                                    </table> 
                                    </td>
                                    </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                     <div style="overflow: scroll; height: 600px">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages"
                                            OnPageIndexChanged="grdAllReports_PageIndexChanged"   OnItemCommand="grdAllReports_ItemCommand" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <%--0--%>
                                             <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                            <%--1--%>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                              <%--2--%>
                                                 <asp:TemplateColumn HeaderText="Bill No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillNo" runat="server" CommandName="Bill No" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--3--%>
                                                  <asp:TemplateColumn HeaderText="Bill Date" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillDate" runat="server" CommandName="Bill Date" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--4--%>
                                                   <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" ></asp:BoundColumn>
                                                
                                                <%--5--%>
                                                   <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="false"></asp:BoundColumn>
                                                   <%--6--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                    <%--7--%>
                                                 <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="Chart No" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                <%--9--%>
                                                <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--10--%>
                                                <asp:TemplateColumn HeaderText="Reffering Office" Visible="false" >
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlRefferingOffice" runat="server" CommandName="Reffering Office" CommandArgument="SZ_OFFICE"
                                                            Font-Bold="true" Font-Size="12px">Reffering Office</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Insurance Company" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceCompany" runat="server" CommandName="Insurance Company" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:TemplateColumn HeaderText="Insurance Claim No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceClaimNo" runat="server" CommandName="Insurance Claim No" CommandArgument="SZ_CLAIM_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Insurance Claim No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CLAIM_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:TemplateColumn HeaderText="Speciality" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="Speciality" CommandArgument="SZ_PROCEDURE_GROUP"
                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--14--%>
                                               <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                               <%--15--%>
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <%--16--%>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <%--17--%>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <%--18--%>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 
                                                 <%--   Repeated Fields  --%>
                                                 <%--19--%>
                                                  <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No" Visible="False"></asp:BoundColumn>
                                                  <%--20--%>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" Visible="False"></asp:BoundColumn>
                                                 <%--21--%>
                                                <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No" Visible="False"></asp:BoundColumn>
                                                 <%--22--%>                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" Visible="False"></asp:BoundColumn>
                                                 <%--23--%>                                                
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="False"></asp:BoundColumn>
                                                 <%--24--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="False"></asp:BoundColumn>
                                                 <%--25--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No" Visible="False"></asp:BoundColumn>
                                                 <%--26--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" Visible="False"></asp:BoundColumn>
                                                 
                                                 <%--   End Repeated Fields  --%>
                                                 <%--27--%>
                                                 
                                                 <asp:TemplateColumn HeaderText="View bill">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                    CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                 <%--28--%>
                                                 
                                             <asp:BoundColumn DataField="SZ_COMPANY_NAME" HeaderText="SZ_COMPANY_NAME" Visible="false"></asp:BoundColumn>
                                                 <%--29--%>
                                             
                                                 <asp:BoundColumn DataField="WC_ADDRESS" HeaderText="WC_ADDRESS" Visible="false"></asp:BoundColumn>
                                                <%-- <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                                 
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                        <tr>
                                        <td>
                                        <asp:Label ID="lblTotal" Text="Total" runat="server"></asp:Label>
                                        <asp:Label ID="lblTotalVal"  runat="server"></asp:Label>&nbsp;
                                        </td> 
                                        </tr>
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <asp:TemplateColumn>
                                                 <ItemTemplate>
                                                 <asp:CheckBox ID="chkSelect" runat="server" />
                                                 </ItemTemplate>
                                                 </asp:TemplateColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" ></asp:BoundColumn>
                                                  <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"> </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <%--<asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                                  
                                   <%--          <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Received Documents"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Missing Documents"></asp:BoundColumn>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid><asp:DataGrid ID="grdForExcel" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"> </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>                                                                                                  
                                                 <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 <%--<asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                   <%--          <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Received Documents"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Missing Documents"></asp:BoundColumn>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid></table>
                                        </td>
                                        
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        <asp:TextBox ID="txtSort" runat="server" Visible="false"></asp:TextBox> 
                                        <asp:TextBox ID="txtBillStatusdate" runat="server" onkeypress="return clickButton1(event,'/')"
                                            Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnAppointdate" runat="server" ImageUrl="~/Images/cal.gif"
                                            Visible="false" /> 
                             <asp:HiddenField ID="hdnPOMValue" runat="server" />                             
                            <ajaxToolkit:CalendarExtender ID="calAppointdate" runat="server" PopupButtonID="imgbtnAppointdate"
                                                        TargetControlID="txtBillStatusdate" >
                                                    </ajaxToolkit:CalendarExtender>
                                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
     <asp:Panel ID="pnlviewBills" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right" valign="top">
                   <table width="100%">
                        <tr>
                            <td width="80%" align="left">
                                List of Bills
                            </td>
                            <td width="20%" align="right">
                                <a onclick="CloseviewBills();" style="cursor: pointer;" title="Close">X</a>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 150px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdViewBills" runat="server" Width="97%" CssClass="GridTable"
                            AutoGenerateColumns="false" >
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                <asp:BoundColumn DataField="VERSION" HeaderText="Version" ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="File Path"> 
                                    <ItemTemplate>
                                        <a href="<%# DataBinder.Eval(Container,"DataItem.PATH")%>"
                                            target="_blank"><%# DataBinder.Eval(Container, "DataItem.FILE_NAME")%></a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CREATION_DATE" HeaderText="Date Created" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
    <div id="div3" style="position: absolute; left: 50%; top: 920px; width: 30%;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px; top: 0px;">
            Msg
            
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4; left: 0px; top: 0px;">
            <a onclick="CancelErrorMassage();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />        
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span1"  runat="server"></span></div>
     <br />
        <br />
        <div style="text-align: center;">
        <%--<asp:Button ID="Button1"  class="Buttons" style="width: 15%;" runat="server" Text="Ok" OnClick="btnOK_Click" />--%>
            <input type="button" class="Buttons" value="Ok" id="Button2" onclick="CancelErrorMassage();"
                style="width: 15%;" />
        </div>
    </div>
      
        </ContentTemplate>
    </asp:UpdatePanel>
        <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px; top: 0px;">
            MSG
            
        </div>
        <%--<div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4; left: 0px; top: 0px;">
            <a onclick="CancelErrorMassage();" style="cursor: pointer;" title="Close">X</a>
        </div>--%>
        <br />
        <br />        
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2"  runat="server"></span></div>
     <br />
        <br />
        <div style="text-align: center;">
        <asp:Button ID="Button1"  class="Buttons" style="width: 15%;" runt="server" Text="Ok" OnClick="btnOK_Click" />
          <asp:Button ID="btnYes" runat="server" CssClass="Buttons" OnClick="btnYes_Click"
                                            Text="Yes" Width="80px" />
         <asp:Button ID="btnNo" runat="server" CssClass="Buttons" OnClick="btnNo_Click"
                                            Text="No" Width="80px" /> 
           <%-- <input type="button" class="Buttons" value="Yes" id="Button1" onclick="YesMassage();"
                style="width: 15%;" />
                <input type="button" class="Buttons" value="NO" id="Button3" onclick="NoMassage();"
                style="width: 15%;" />--%>
        </div>
    </div>
</asp:Content>
