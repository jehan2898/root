<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatePatient.aspx.cs" Inherits="CreatePatient" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
    <title></title>
    <style type="text/css">
        .style3
        {
        }
        .lbl
        {
            width: 153px;
        }
        .cinput
        {
            margin-left: 1px;
        }
        .style4
        {
            width: 100%;
        }
        .style5
        {
            width: 134px;
        }
        .style6
        {
            width: 236px;
        }
        .style7
        {
            width: 226px;
        }
        .style8
        {
            width: 129px;
        }
        .style10
        {
            width: 38%;
            height: 87px;
        }
        .style11
        {
            height: 87px;
        }
        .style12
        {
            height: 22px;
            width: 20%;
        }
        .style13
        {
            width: 20%;
        }
        .style14
        {
            height: 18px;
            width: 20%;
        }
        .style15
        {
            width: 63px;
        }
        .style17
        {
            width: 1px;
        }
        .style18
        {
            width: 27%;
        }
        .style19
        {
            width: 77%;
        }
        .style20
        {
            height: 19px;
        }
        .style21
        {
            width: 52%;
        }
        .style22
        {
            height: 19px;
            width: 38%;
        }
        .style23
        {
            width: 38%;
        }
        .style26
        {
            width: 1042px;
        }
        </style>
</head>
<body>
<form id="form1" runat="server">
    <table id="First" border="0" cellpadding="0" cellspacing="0" width="100%" style="border: 1px solid;">
      <tr>
        <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; 
            height: 100%; vertical-align: top;">
            
            <table id="MainBodyTable" cellpadding="0" cellspacing="0" width="100%" align="center">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                           
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 25px; font-weight: bold;"
                            colspan="4">
                            <table width="100%">
                                <tr>
                                    <td width="50%" style="text-align:left";>
                                         <span style="color: Purple;"> &nbsp;</span></td>
                                    <td style="text-align: right;">
                                        <span style="color: Red;">* -&nbsp; Mandatory fields.</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                     <td width="100%" scope="col"  style="height: auto;">
                     <table width="100%">
                                <tr>
                                    <td align="center" valign="top" scope="col" style="height: auto;">
                                        <div align="left">
                                            
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" 
                                                   style="border: 1px solid;">
                                                
                                                <tr>
                                                    <td style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                         <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; PERSONAL INFORMATION</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                        <div class="lbl">
                                                            <dxe:ASPxLabel ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></dxe:ASPxLabel>
                                                            <div id="ErrorDiv" style="color: red" visible="true">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td >
                                                        
                                                            <dxe:ASPxLabel ID="lblPchartno" Text="Prev. Chart No. : " 
                                                                        runat="server" class="lbl"  Visible="False" 
                                                                        ForeColor="white" BackColor="red" ></dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="lblPreChartNo"  
                                                                       runat="server" Visible="False" 
                                                                       BackColor="Red" class="lbl" 
                                                                       ForeColor="White" 
                                                                       Font-Bold="true" ></dxe:ASPxLabel>
                                                        
                                                    </td>
                                                </tr>
                                              <tr>
                                               <td height="26" class="style3" rowspan="2" >
                                        </div>
                                                        <table class="style4">
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                            <dxe:ASPxLabel ID="lblChart0" Text="Chart No." runat="server" >
                                                            </dxe:ASPxLabel>
                                                                                                     
                                                                </td>
                                                                <td class="style6">
                                                     <dxe:ASPxTextBox ID="txtChartNo" runat="server" Height="16px" Width="180px">
                                                     </dxe:ASPxTextBox>
                                                                </td>
                                                                <td width="13%" class="ContentLabel" 
                                                                    style="font-family: calibri; font-size: medium;">
                                                       <dxe:ASPxLabel ID="lblLocation" Text="Location" runat="server" class="lbl"></dxe:ASPxLabel>
                                                                    <dxe:ASPxLabel ID="lextddlLocation" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                                   
                                                                </td>
                                                                <td class="style7">
                                                       <dx:ASPxComboBox ID="cmbLocation" Width="180px"
                                                            runat="server" Selected_Text="--- Select ---"  Visible="true" >
                                                       </dx:ASPxComboBox>
                                                   
                                                                </td>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    First Name <span style="color: Red;">
                                                                    <dxe:ASPxLabel ID="ltxtPatientFName" runat="server" 
                                                                                   ForeColor="#FF8000" Text="*" 
                                                                                   Visible="True">
                                                                    </dxe:ASPxLabel></span></td>
                                                                <td class="style6">
                                                                    <dxe:ASPxTextBox ID="txtPatientFName" runat="server" MaxLength="50" 
                                                                        Width="180px">
                                                                    </dxe:ASPxTextBox> </td>
                                                                <td width="13%" class="ContentLabel" 
                                                                    style="font-family: calibri; font-size: medium;">
                                                                    Middle Name <span style="color: Red;">
                                                                         <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                                   ForeColor="#FF8000" Text="*" 
                                                                                   Visible="True">
                                                                          </dxe:ASPxLabel></span></td>
                                                                <td class="style7">
                                                                    <dxe:ASPxTextBox  ID="txtMiddleName" runat="server"  MaxLength="50" 
                                                                        Width="180px">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="style8" style="font-family: calibri; font-size: medium">
                                                                    Last Name <span style="color: Red;">
                                                                         <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                                   ForeColor="#FF8000" Text="*" 
                                                                                   Visible="True">
                                                                          </dxe:ASPxLabel></span>
                                                               </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtLastName" runat="server" MaxLength="50" Width="180px">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    Date Of Birth</td>
                                                                <td class="style6">
                                                                     <dxe:ASPxDateEdit runat="server" Width="180px" >
                                                                     </dxe:ASPxDateEdit>

                                                                </td>
                                                                <td style="font-family: calibri; font-size: medium;">
                                                                    SSN#<dxe:ASPxLabel ID="ltxtSocialSecurityNumber" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style7">
                                                                     <dxe:ASPxTextBox ID="txtSocialSecurityNumber" MaxLength="20" runat="server" 
                                                                         Width="180px"> 
                                                                     </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="style8" style="font-family: calibri; font-size: medium">
                                                                    Gender<dxe:ASPxLabel ID="lddlSex" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxComboBox ID="ddlSex" runat="server" Width="180px">        
                                                                    <Items>
                                                                        <dx:ListEditItem Value="Male" Text="Male" />
                                                                        <dx:ListEditItem Value="Female" Text="Female" />
                                                                    </Items>                                                             
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    Address<dxe:ASPxLabel ID="ltxtPatientAddress" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                     </td>
                                                                <td class="style6">
                                                                     <dxe:ASPxTextBox  Width="180px" ID="txtPatientAddress" MaxLength="50" runat="server">
                                                                     </dxe:ASPxTextBox>
                                                                     </td>
                                                                <td style="font-family: calibri; font-size: medium;">
                                                                    State<dxe:ASPxLabel ID="ltxtState" runat="server" 
                                                                        ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style7">
                                                                     <dxe:ASPxComboBox ID="cmbState" runat="server" Width="180px" MaxLength="50">
                                                                     </dxe:ASPxComboBox> 
                                                                </td>
                                                                <td class="style8" style="font-family: calibri; font-size: medium">
                                                                    City<dxe:ASPxLabel ID="lextddlPatientState" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                     <dxe:ASPxComboBox ID="cmbCity" runat="server" Width="180px" MaxLength="50">
                                                                     </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    Zip</td>
                                                                <td class="style6">
                                                                    <dxe:ASPxTextBox  Width="180px" ID="txtZip" MaxLength="50" runat="server">
                                                                    </dxe:ASPxTextBox></td>
                                                                <td style="font-family: calibri; font-size: medium;">
                                                                    Home Phone</td>
                                                                <td class="style7">
                                                                    <dxe:ASPxTextBox Width="180px" ID="txtHomePhone" MaxLength="50" runat="server">
                                                                    </dxe:ASPxTextBox></td>
                                                                <td class="style8" style="font-family: calibri; font-size: medium">
                                                                    Work</td>
                                                                <td>
                                                                   <dxe:ASPxTextBox  Width="180px" ID="txtWork" MaxLength="50" runat="server">
                                                                   </dxe:ASPxTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    Extn.<dxe:ASPxLabel ID="ltxtExtn" runat="server" 
                                                                        ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style6">
                                                                    <dxe:ASPxTextBox Width="180px" ID="txtExtn" MaxLength="50" runat="server">
                                                                    </dxe:ASPxTextBox>
                                                                    </td>
                                                                <td style="font-family: calibri; font-size: medium;">
                                                                      Email
                                                                       <dxe:ASPxLabel ID="ltxtEmail" runat="server" 
                                                                        ForeColor="#FF8000" Text="*" Visible="False">
                                                                      </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style7">
                                                                     <dxe:ASPxTextBox Width="180px" ID="txtEmail" MaxLength="50" runat="server">
                                                                     </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="style8" style="font-family: calibri; font-size: medium">
                                                                    Attorney</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5" style="font-family: calibri; font-size: medium;">
                                                                    Case Type<dxe:ASPxLabel ID="ltxtCaseType" runat="server" 
                                                                        ForeColor="#FF8000" Text="*" Visible="False">
                                                                     </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style6">
                                                                    <dxe:ASPxComboBox ID="cmbCaseType" runat="server" Width="180px" MaxLength="50">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                 <div class="lbl">
                                                                    <dx:ASPxCheckBox ID="chkWrongPhone" runat="server" Text="Wrong Phone" Visible="false" />
                                                                    <dxe:ASPxLabel ID="lchkWrongPhones" runat="server" ForeColor="#FF8000" Text="*"
                                                                                   Visible="False">
                                                                    </dxe:ASPxLabel>
                                                                                    &nbsp; &nbsp;
                                                                    <dx:ASPxCheckBox ID="chkTransportation" runat="server" Text="Transport"
                                                                                    Visible="false"></dx:ASPxCheckBox>
                                                                    <dxe:ASPxLabel ID="lchkTransportation" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>
                                                                 </div>
                                                                   
                                                                </td>
                                                                <td class="style7">
                                                                    &nbsp;</td>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                            <td >
                                                            <div >
                                                            </div>
                                                            </td>
                                                             <td rowspan="1" >
                                                            </td>
                                                            <td colspan="4" rowspan="1">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="25%">
                                                                    <dxe:ASPxComboBox ID="cmbProvider" Width="53px" runat="server"  
                                                                                      Selected_Text="--- Select ---"
                                                                                      Visible="false" Height="19px" />
                                                                    <dxe:ASPxComboBox ID="cmbCaseStatus" Width="50px" runat="server"  
                                                                                      Selected_Text="--- Select ---"
                                                                                      Visible="false" Height="23px" />
                                                                    <dxe:ASPxComboBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False" 
                                                                                      Width="51px" Height="20px" />
                                                                    
                                                                    <dxe:ASPxTextBox ID="txtJobTitle" runat="server" Visible="False" Width="10px">
                                                                    </dxe:ASPxTextBox>
                                                                    <dxe:ASPxTextBox ID="txtWorkActivites" runat="server" Visible="False" Width="10px">
                                                                    </dxe:ASPxTextBox>
                                                                    <dxe:ASPxTextBox Visible="false" ID="txtPatientStreet" runat="server" Width="10px">
                                                                    </dxe:ASPxTextBox>
                                                                 </td>
                                                                <td width="13%" >
                                                                    <div>
                                                                        <dxe:ASPxTextBox Width="10px" ID="txtDateOfInjury" runat="server" 
                                                                                         MaxLength="10" Visible="False">
                                                                        </dxe:ASPxTextBox>
                                                                        <dxe:ASPxTextBox ID="txtCarrierCaseNo" runat="server" 
                                                                                         Visible="False" Width="10px">
                                                                        </dxe:ASPxTextBox>
                                                                        <dxe:ASPxTextBox ID="txtPatientAge" runat="server" 
                                                                                         MaxLength="10" Visible="False" 
                                                                                         Width="10px">
                                                                       </dxe:ASPxTextBox>
                                                                       </div>
                                                                </td>
                                                                <td width="25%">
                                                                    <span>
                                                                        <dx:ASPxCheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code"
                                                                                         Visible="False" Width="200px" />
                                                                     </span>
                                                                </td>
                                                                <td width="5%" >
                                                                    <div style="width: 133px" >
                                                                        &nbsp;</div>
                                                                </td>
                                                                <td width="32%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                </td>
                            </tr>
                                                    </table>  
                                                </td>
                </table>
            </td>   
             
      </tr>   
       <tr>
          <td>
             <table width="100%">
                 <tr>
                    <td >
                               <table>
                               <tr >                               
                               <td width="50%" valign="top" style="border: 1px solid;">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="4" style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                         <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; ACCIDENT INFORMATION</span>
                                                    </td>
                                            </tr>


                                      
                                             <td class="style5" >                                                 
                                                      &nbsp;</td>
                                             <td class="style18">
                                                  &nbsp;</td>
                                               <td>                                                  
                                                        &nbsp;</td>
                                               <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                             <td class="style5" style="font-family: calibri; font-size: medium" >                                                 
                                                      Date
                                                  <dxe:ASPxLabel ID="ltxtDateofAccident" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                                                
                                             </td>
                                             <td class="style18">
                                                  <dxe:ASPxDateEdit runat="server" Width="140px" 
                                                        ID="ASPxDateEdit1" >
                                                  </dxe:ASPxDateEdit>
                                                                
                                               </td>
                                               <td style="font-family: calibri; font-size: medium">                                                  
                                                        Plate #                                                
                                                    <dxe:ASPxLabel ID="ltxtPlatenumber" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                    </dxe:ASPxLabel>
                                               </td>
                                               <td>
                                                    <dxe:ASPxTextBox ID="txtPlatenumber" MaxLength="20" runat="server" 
                                                                     Width="140px" >
                                                    </dxe:ASPxTextBox>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td class="style5" style="font-family: calibri; font-size: medium">
                                                    Report #                                                   
                                                  <dxe:ASPxLabel ID="ltxtPolicyReport" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                  </dxe:ASPxLabel>
                                               </td>
                                               <td >
                                                  <dxe:ASPxTextBox ID="txtPolicyReport" runat="server" MaxLength="20" 
                                                                    Width="140px" >
                                                  </dxe:ASPxTextBox>
                                               </td>
                                               <td width="1%" style="font-family: calibri; font-size: medium">
                                                 
                                                       Address
                                                                    <dxe:ASPxLabel ID="ltxtAccidentAddress" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>
                                               
                                                            </td>
                                                            <td width="30%">
                                                                <dxe:ASPxTextBox Width="140px" MaxLength="50" ID="txtAccidentAddress"                                                                     runat="server" >
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium" >
                                                                  City
                                                                <dxe:ASPxLabel ID="ltxtAccidentCity" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>  
                                                            </td>
                                                            <td >
                                                                <dx:ASPxComboBox ID="txtAccidentCity" MaxLength="50" runat="server" 
                                                                    Width="140px" >
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                            <td style="font-family: calibri; font-size: medium" >                                                               
                                                                    State
                                                                <dxe:ASPxLabel ID="lextddlAccidentState" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td >
                                                                <dxe:ASPxTextBox ID="txtAccidentState" runat="server" Width="160px" 
                                                                    Visible="false"></dxe:ASPxTextBox>
                                                                <dxe:ASPxComboBox ID="cmbAccidentState" runat="server" Width="140px" 
                                                                    Selected_Text="--- Select ---">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium" >                                                        
                                                                    Hospital name
                                                                    <dxe:ASPxLabel ID="ltxtHospitalName" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>    
                                                              
                                                            </td>
                                                            <td >
                                                                <dxe:ASPxTextBox ID="txtHospitalName" runat="server" CssClass="cinput" 
                                                                    MaxLength="50" Width="140px">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td style="font-family: calibri; font-size: medium" >                                                               
                                                                    Hospital Address
                                                                <span style="color: #ff0000">
                                                                  <dxe:ASPxLabel ID="ltxtHospitalAddress" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>   
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <span style="color: #ff0000">
                                                                  <dxe:ASPxTextBox ID="txtHospitalAddress" runat="server" CssClass="cinput" 
                                                                    MaxLength="50" Width="140px"  ></dxe:ASPxTextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium" >                                                                
                                                                    Date of admission
                                                            </td>
                                                            <td >
                                                                <dxe:ASPxDateEdit runat="server" Width="140px" 
                                                                    ID="ASPxDateEdit2" >
                                                                </dxe:ASPxDateEdit>                                                              
                                                            </td>
                                                            <td width="20%">
                                                            </td>
                                                            <td width="30%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium">                                                               
                                                                    Describe Injury
                                                                    <dxe:ASPxLabel ID="ltxtDescribeInjury" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>                                                                  
                                                            </td>
                                                            <td colspan="3" width="80%" class="style19">
                                                                   <dxe:ASPxTextBox Width="90%" ID="txtDescribeInjury" runat="server" MaxLength="250" >
                                                                   </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium">
                                                                    Additional patients
                                                                    <dxe:ASPxLabel ID="ltxtListOfPatient" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>
                                                            </td>
                                                            <td colspan="3" width="80%" class="style19" >
                                                                <dxe:ASPxTextBox Width="90%" ID="txtListOfPatient" runat="server" MaxLength="250">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5" style="font-family: calibri; font-size: medium" >                                                               
                                                                    Patient Type
                                                                    <dxe:ASPxLabel ID="lrdolstPatientType" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                    </dxe:ASPxLabel>                                                                
                                                            </td>
                                                            <td colspan = "3" class = "style19">
                                                                <dx:ASPxRadioButtonList ID="rdolstPatientType" runat="server" 
                                                                                         RepeatDirection="Horizontal" Width="361px" >

                                                                            <Items>
                                                                            <dx:ListEditItem Value="0" Text="Driver"  />
                                                                            <dx:ListEditItem Value="1" Text="Passenger" />
                                                                            <dx:ListEditItem Value="2" Text="Pedestrian"  />
                                                                            </Items>                                                                                       
                                                                                                                                       
                                                               </dx:ASPxRadioButtonList>    
                                                               
                                                                <dxe:ASPxTextBox ID="txtPatientType" runat="server" Visible = "false" Width="2%"></dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                         
                                                    </table>
                               </td>
                                  <td class="style17">
                                    </td> 

                                    <td width="50%" valign="top" style="border: 1px solid;">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="5" style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                                    <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; INSURANCE INFORMATION</span>
                                                </td>
                                            </tr>
                                          <tr> 
                                        <td class="style23">
                                              &nbsp;</td>
                                        <td colspan="2">                                          
                                                &nbsp;</td>                                       
                                        <td>   
                                            &nbsp;</td>                                       
                                         <td>
                                                &nbsp;</td>                                     
                                       </tr>
                                       <tr> 
                                        <td class="style22">
                                              Name
                                                    <dxe:ASPxLabel ID="lextddlInsuranceCompany" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                    </dxe:ASPxLabel>      
                                       </td>
                                       <td colspan="3">
                                         <div class="lbl">
                                                                    <dxe:ASPxComboBox ID="extddlInsuranceCompany" Width="200px" runat="server"
                                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                        Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                        OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged" />
                                                                     <asp:Label ID="Label2" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    <a href="#" id="A2" onclick="showInsurancePanel()" style="text-decoration: none;">
                                                                        <img id="imgbtnAddInsuranceCompany" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company and Address Details" /></a>
                                                                    <a href="#" id="A3" onclick="confirmstatus();" style="text-decoration: none;"  >
                                                                         <img id="img3" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address" /></a>
                                                                    
                                                                        <%--<asp:LinkButton ID="lnkSearch" runat="server" Text="Search" OnClientClick="ShowInsSearchPanel()" ></asp:LinkButton>--%>
                                                                        <a href="#" onclick="ShowInsSearchPanel()" >Search</a>
                                                                    
                                                                        
                                                                <div>
                                                                    
                                                                        
                                                                </div>
                                                            </td>                                          
                                        <td class="style20">                                          
                                                &nbsp;</td>                                       
                                       </tr>
                                       <tr> 
                                        <td class="style23">
                                         Insurance Code                                       
                                            <dxe:ASPxLabel ID="ASPxLabel4" Width="16%" runat="server" ForeColor="#FF8000" 
                                                Text="*" Visible="False"></dxe:ASPxLabel>    
                                         &nbsp;
                                        </td>
                                        <td colspan="2">                                          
                                                <dxe:ASPxComboBox ID="ASPxComboBox1" Width="200px" runat="server"  
                                                    Selected_Text="--- Select ---" AutoPost_back="True"  />
                                           </td>                                       
                                        <td>   
                                            &nbsp;</td>                                       
                                         <td>
                                                &nbsp;</td>                                     
                                       </tr>
                                       <tr>
                                          <td class="style10">
                                                 Ins. Address
                                          <dxe:ASPxLabel ID="llstInsuranceCompanyAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                  </dxe:ASPxLabel>
                                          </td>
                                           <td colspan="4" class="style11">
                                                  <dxe:ASPxListBox Width="85%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server">
                                                  </dxe:ASPxListBox>                                                    
                                                        &nbsp;</td>
                                         </tr>
                                         <tr>
                                            <td class="style23">
                                                  Address
                                            </td>
                                              <td class="style21">
                                                  <dxe:ASPxTextBox Width="150px" ID="txtInsuranceAddress" runat="server" 
                                                                    ReadOnly="True">
                                                  </dxe:ASPxTextBox>
                                               </td>
                                                   <dxe:ASPxLabel ID="ltxtInsuranceAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                                   <td width="20%" colspan="2">                                                                
                                                       City
                                                   </td>
                                                    <td width="30%">
                                                        <dxe:ASPxComboBox ID="txtInsuranceCity" runat="server"  ReadOnly="True"
                                                                         Width="150px">
                                                         </dxe:ASPxComboBox>
                                                     </td>
                                                          <dxe:ASPxLabel ID="ltxtInsuranceCity" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                          </dxe:ASPxLabel>
                                           </tr>
                                           <tr>
                                           <td class="style23">
                                               State
                                                <dxe:ASPxLabel ID="lextddlInsuranceState" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>    
                                           </td>    
                                           <td class="style21">
                                                <dxe:ASPxTextBox ID="txtInsuranceState" runat="server" ReadOnly="True"
                                                                 Width="150px" Visible="false"></dxe:ASPxTextBox>
                                                <dxe:ASPxComboBox ID="extddlInsuranceState" runat="server" Selected_Text="--- Select ---"                                                   
                                                                  Width="150px" Enabled="false"></dxe:ASPxComboBox>
                                            </td>     
                                             <td width="20%" colspan="2">
                                                    Zip
                                                    <dxe:ASPxLabel ID="lbltxtInsuranceZip" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                             </td>
                                             <td width="30%">
                                               <dxe:ASPxTextBox Width="150px" ID="txtInsuranceZip" runat="server"  
                                                     ReadOnly="True"></dxe:ASPxTextBox>
                                               </td>                                                                                                                                    
                                           
                                           </tr>
                                           <tr>
                                                <td class="style23">                                                               
                                                  Claim/File #
                                                            <dxe:ASPxLabel ID="ltxtClaimNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                        </dxe:ASPxLabel>
                                                  </td>
                                                  <td class="style21">
                                                        <dxe:ASPxTextBox ID="txtClaimNumber" runat="server"  Width="150px" ></dxe:ASPxTextBox>
                                                   </td> 
                                                    <td width="20%" colspan="2">
                                                             policy Number
                                                            <dxe:ASPxLabel ID="lblWcbNumber" CssClass="lbl" runat="server"></dxe:ASPxLabel>
                                                    </td>
                                                    </td>                                                           
                                                         <td width="30%">
                                                              <dxe:ASPxTextBox ID="txtWCBNumber" runat="server" Width="150px" 
                                                                    Visible="False">
                                                              </dxe:ASPxTextBox>                                                                    
                                                              <dxe:ASPxLabel ID="ltxtWCBNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>       
                                                               <dxe:ASPxTextBox ID="txtPolicyNumber" runat="server" Width="150px" ></dxe:ASPxTextBox>
                                                              <dxe:ASPxLabel ID="ltxtPolicyNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                                          </td>
                                                </tr>
                                                 <tr>
                                                    <td class="style23">
                                                            Policy&nbsp;Holder
                                                            <dxe:ASPxLabel ID="ltxtPolicyHolder" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel> 
                                                            
                                                    </td>
                                                    <td style="width: 30%" colspan="4">
                                                        <dxe:ASPxTextBox D="txtPolicyHolder" Width="500px" runat="server" ></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                    </table>
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
                            <table width="100%">
                                <tr>
                                    <td width="50%" style="border: 1px solid;">
                                        <table width="100%">
                                            <tr>
                                            <td style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                                 <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; EMPLOYER INFORMATION</span>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart" align="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="style12">
                                                                    &nbsp;</td>
                                                            <td style="width: 30%; height: 22px;">
                                                                &nbsp;</td>
                                                            <td width="20%" style="height: 22px">
                                                                    &nbsp;</td>
                                                            <td style="height: 22px; width: 30%;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style12">
                                                                    Name
                                                                 <dxe:ASPxLabel ID="ltxtEmployerName" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>       
                                                            </td>
                                                            <td style="width: 30%; height: 22px;">
                                                                <dxe:ASPxTextBox Width="180px" ID="txtEmployerName" MaxLength="50" 
                                                                    runat="server" >
                                                                </dxe:ASPxTextBox>
                                                                </td>
                                                            <td width="20%" style="height: 22px">
                                                                    Address
                                                                 <dxe:ASPxLabel ID="ltxtEmployerAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                 </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="height: 22px; width: 30%;">
                                                                <dxe:ASPxTextBox ID="txtEmployerAddress" runat="server" MaxLength="150" 
                                                                    Width="180px" >
                                                                </dxe:ASPxTextBox>                                                                    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style13">
                                                                    City
                                                                <dxe:ASPxLabel ID="ltxtEmployerCity" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>       
                                                            </td>
                                                            <td style="width: 30%">
                                                                <dxe:ASPxComboBox  ID="txtEmployerCity" MaxLength="50" runat="server" 
                                                                    Width="180px" >
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td width="20%">                                                               
                                                                    State
                                                                 <dxe:ASPxLabel ID="ltxtEmployerState" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>       
                                                            </td>
                                                            <td style="width: 30%">
                                                                <dxe:ASPxTextBox ID="txtEmployerState" runat="server" CssClass="cinput" 
                                                                    Width="180px" Visible="false">
                                                                </dxe:ASPxTextBox>
                                                                <dxe:ASPxComboBox ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"                                                                    
                                                                    Width="180px"></dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style13">
                                                                    Zip
                                                               <dxe:ASPxLabel ID="ltxtEmployerZip" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                               </dxe:ASPxLabel>   
                                                            </td>
                                                            <td style="width: 30%">
                                                               <dxe:ASPxTextBox ID="txtEmployerZip" MaxLength="50" runat="server" 
                                                                    Width="180px" >
                                                               </dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="20%">
                                                                    Phone
                                                               <dxe:ASPxLabel ID="ltxtEmployerPhone" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                               </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 30%">
                                                               <dxe:ASPxTextBox ID="txtEmployerPhone" MaxLength="20" runat="server" 
                                                                    Width="180px" >
                                                               </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style13">
                                                                    &nbsp;
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style14">
                                                            </td>
                                                            <td colspan="3" width="80%" style="height: 18px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style13">
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="1px">
                                    </td>
                                    <td width="50%" valign="top" style="border: 1px solid;">
                                        <table width="100%">
                                            <tr>
                                               <td style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                                    <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; ADJUSTER INFORMATION</span>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart" align="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%">
                                                                    &nbsp;</td>
                                                            <td colspan="2" class="style15">
                                                                &nbsp;</td>
                                                            <td colspan="2">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                    Name
                                                               <dxe:ASPxLabel ID="lextddlAdjuster" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                               </dxe:ASPxLabel>      
                                                            </td>
                                                            <td colspan="2" class="style15">
                                                               <dxe:ASPxComboBox ID="cmbAdjuster" Width="180px" runat="server" />
                                                                <a href="#" id="hlnlShowAdjuster" onclick="showAdjusterPanel()" style="text-decoration: none;">
                                                                        <img id="imgShowAdjuster" src="Images/actionEdit.gif" style="border-style: none;" /></a>&nbsp;<a href="#" id="hlnlShowAdjuster" onclick="showAdjusterPanel()" style="text-decoration: none;">
                                                                        </a></div>
                                                            &nbsp;</td>
                                                            <td colspan="2">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                    &nbsp;Phone
                                                                <dxe:ASPxLabel ID="ltxtAdjusterPhone" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <dxe:ASPxTextBox ID="txtAdjusterPhone" runat="server" ReadOnly="true" 
                                                                    Width="180px"></dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="20%" colspan="2">
                                                                    Extension
                                                                 <dxe:ASPxLabel ID="ltxtAdjusterExtension" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                 </dxe:ASPxLabel>
                                                            </td>
                                                            <td width="30%">
                                                                <dxe:ASPxTextBox ID="txtAdjusterExtension" runat="server" ReadOnly="true" 
                                                                    Width="180px">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td width="20%">
                                                                    Email
                                                                <dxe:ASPxLabel ID="Label1" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>      
                                                            </td>
                                                            <td style="width: 30%">
                                                                <dxe:ASPxTextBox Width="180px" ID="TextBox1" runat="server" ReadOnly="true" >
                                                                </dxe:ASPxTextBox>
                                                           </td>
                                                            <td width="20%" colspan="2">
                                                                    Fax
                                                                <dxe:ASPxLabel ID="ltxtfax" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td width="30%">
                                                                <dxe:ASPxTextBox ID="txtfax" runat="server" ReadOnly="true" CssClass="cinput" 
                                                                    Width="180px">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            </tr>
                                                             <tr>
                                                            <td width="20%"> 
                                                                    &nbsp;<dxe:ASPxLabel ID="ltxtInsuranceStreet" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False">
                                                                </dxe:ASPxLabel>     
                                                            &nbsp;<dxe:ASPxTextBox Visible="false" Width="20px" ID="txtInsuranceStreet" runat="server"
                                                                                 ReadOnly="True"></dxe:ASPxTextBox>
                                                            </td>
                                                            <td colspan="4" width="80%">
                                                            </td>
                                                        </tr>
                                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                              
                   </table>
                       </td>
                        
                     </tr>
                     <tr>
                         <td >
                              <table width="100%">
                                <tr>
                                    <td class="ContentLabel">
                                        &nbsp;</td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxTextBox ID="txtAccidentID" runat="server" Visible="False" Width="120px"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxTextBox ID="txtCaseID" runat="server" Visible="False" Width="120px"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxTextBox ID="txtCompanyID" runat="server" Visible="False" Width="120px"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="style26">
                                        <dxe:ASPxTextBox  ID="txtUserId" runat="server" Visible="False" Width="120px"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxButton ID="btnSave" runat="server"  Text="Add" Width="120px"/>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxButton  ID="btnUpdate" runat="server"  Text="Update" Width="120px"  visible="false"/>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxButton ID="btnClear" runat="server"  Text="Clear" Width="120px"/>
                                    </td>
                                    <td class="ContentLabel">
                                        <dxe:ASPxTextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                     </tr>
         </table>
      
    </form>
</body>
</html>
