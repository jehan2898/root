<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_StatusProceudure.aspx.cs" Inherits="Bill_Sys_StatusProceudure" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
 
 <script>
 
 
 
 
 
 
 function showReceiveDocumentPopup(caseid,Eventprocid,speciality)
       {
         
            
            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left= '300px'; 
            document.getElementById('divid').style.top= '100px';              
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src ='Bill_Sys_ViewDocuments.aspx?CaseID='+caseid+'&Type=YES&EProcid='+Eventprocid+'&spc='+speciality; 
            return false;
          
       }
 
    function showUploadFilePopup()
       {
       
           var flag= false;
           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdProcesureCode');
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
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '200px';
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left ='350px';
	                 document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.zIndex= '0';
	            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
	                //document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height='0px';
                   // document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';  
                //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                  //  MA.length = 0;
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
 
 



   function ChekOne()
       {
             var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdProcesureCode");	
          
            var k=0
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		   
		           
		        if(f.getElementsByTagName("input").item(i).type == "checkbox"  )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked==true)
			        {    k=1;
			            return ConfirmDelete();
			        
			        }
			    }	
			}		
		  
		    if(k==0)
		    {
		        alert('Select Record');
		        return false;
		    }
       }
       
        function Check()
       {
             var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdProcesureCode");	
            var k=0
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox"  )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked==true)
			        {    k=1;
			           //formValidator('frmPatient','_ctl0_ContentPlaceHolder1_ddlDoctor');
			           
			        
			        }
			    }			
		    }
		    if(k==0)
		    {
		        alert('Please select record');
		        return false;
		    }
		    else
		    {
		        if (document.getElementById("_ctl0_ContentPlaceHolder1_ddlDoctor").selectedIndex==0	)
		        {
		            document.getElementById("_ctl0_ContentPlaceHolder1_ddlDoctor").focus();
		            document.getElementById("_ctl0_ContentPlaceHolder1_ddlDoctor").style.backgroundColor = "#ffff99";
		            alert('Please select reading doctor');
		            return false;
		        }
		    }
       }
 
   function ConfirmDelete()
        {
             var msg = "Do you want to Revert?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
 </script>
 
 
 
   <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 80%;
                padding-top: 3px; height: 100%; vertical-align:top;">
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
                                         <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                            <Columns>
                                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                               
                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                
                                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                <a href="#" onclick="return openTypePage('a')">
                                                                <img src="Images/actionEdit.gif" style="border:none;"/>
                                                                </a> 
                                                                </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                               
                                                            </Columns>
                                  </asp:DataGrid>		
                                    </td> 
                                    </tr> 
                                      
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 100%" class="TDPart">
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
                                    <td style="width: 100%">
                                        <div style="overflow: scroll; height: 320px; width: 100%;">
                                                <asp:DataGrid Width="95%" ID="grdProcesureCode" CssClass="GridTable" runat="server" AutoGenerateColumns="False" OnItemCommand="grdProcesureCode_ItemCommand"  >
                                                  
                                                   <ItemStyle CssClass="GridRow"/>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="CodeID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="CodeID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_PROC_CODE" HeaderText="CodeID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Procedure Code"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="EVENT_DATE" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="I_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_TYPE_ID" HeaderText="Type" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="I_REPORT_RECEIVED" HeaderText="Report Status"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SPECIALITY" HeaderText="Speciality" Visible="False"></asp:BoundColumn>
                                                           <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                           <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                               <asp:LinkButton ID="LKBView" CommandName="View" runat="server">View Document </asp:LinkButton>
                                                            </ItemTemplate>
                                                           </asp:TemplateColumn>
                                                    </Columns>
                                                  <HeaderStyle CssClass="GridHeader"/>
                                                  
                                                </asp:DataGrid>
                                                
                                            </div>
                                             <a id="hlnkShowGroupProcCode" style="cursor: pointer; height: 260px; vertical-align:middle;"
                                                onclick="showUploadFilePopup();" class="Buttons" runat="server" title="Add Group Services">
                                                Received Document</a>
                                        <asp:Button id="btnReceivedDocument" runat="server" CssClass="Buttons" Text="Received Document" Visible="false" OnClick="btnReceivedDocument_Click"></asp:Button>
                                         <asp:Button ID="btnRevert" runat="server" Text="Revert" CssClass="Buttons" OnClick="btnRevert_Click1" Width="200px" /> 
                                        <%-- <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="150px" Connection_Key="Connection_String"
                                             Procedure_Name="SP_MST_READINGDOCTOR" Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();"/>--%>
                                             <asp:DropDownList ID="ddlDoctor" runat="server" Width="200px" ></asp:DropDownList>
                                           <asp:Button ID="btnDoctor" runat="server" Text="Assign Reading Doctor" CssClass="Buttons" Width="200px" OnClick="btnDoctor_Click"  />                                                
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                        

                                        
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
    
<div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    
    <div id="div1" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        </div>
        
     <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
       
         <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 98%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%">
                        <tr>
                            <td>
                                Upload Report :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuUploadReport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons" OnClick="btnUploadFile_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         
    </asp:Panel>
     <asp:TextBox ID="hdnCaseID" runat="server" style="visibility:hidden;" Width="10px"></asp:TextBox>
  
     <div id="divid" style="position: absolute; width: 600px; height: 400px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 600px;">
            <a  onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;window.parent.document.location.href='Bill_Sys_StatusProceudure.aspx'; " style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="400px" width="600px"></iframe>
    </div>
</asp:Content>
