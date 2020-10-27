<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMaster.aspx.cs" Inherits="UserMaster" %>

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
       
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 883px;
        }
       
        .style7
        {
            width: 5px;
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
                <td class="style7" >
                </td>
              </tr>
              <tr>
                 <td class="style7" >
                 </td>
                 <td > 
              
                 <table  style="width: 100%">
                   <tr>
                      <td style="width: 100%">

                         <table style="border: 1px solid;" width="100%">
                           <tr>
                             <td colspan="3" >
                             </td>
                            </tr>
                            <tr>
                              <td style="width: 10%">
                                <dxe:ASPxLabel ID="Label1" runat="server" Text="User Name" Font-Size="13px" Font-Names="Arial"></dxe:ASPxLabel>
                                   </td>
                                   <td style="width: 20%">
                                       <dxe:ASPxTextBox ID= "txtUserName" runat="server" MaxLength="50" ></dxe:ASPxTextBox>
                                   </td>
                                   <td style="width: 10%" align="left">
                                        <dxe:ASPxLabel ID="label3" runat="server" Text="User role" Font-Size="13px" Font-Names="Arial"></dxe:ASPxLabel>
                                   </td>
                                   <td style="width: 20%">
                                        <dxe:ASPxComboBox ID="cmbUserRole" runat="server" style="margin-left: 0px" Height="16px" Width="275px">                  </dxe:ASPxComboBox>
                                      </td>
                                     </tr>
                                     <tr>
                                       <td style="width: 10%; height: 22px;" >
                                           <dxe:ASPxLabel ID="Label4" runat="server"  Text="Password" Font-size="13px" Font-Names="Arial">
                                          </dxe:ASPxLabel>
                                       </td>
                                       <td style="width: 20%; height: 22px;">
                                           <dxe:ASPxTextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="40">
                                           </dxe:ASPxTextBox>
                                       </td>
                                       <td align="left" style="width: 10%; height: 22px;">
                                                    <dxe:ASPxLabel ID="Label6" runat="server" Text="Confirm Password" Font-Size="13px" Font-Names="Arial"></dxe:ASPxLabel>
                                                </td>

                                        <td style="width: 20%; height: 22px;">
                                            <dxe:ASPxTextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="40">
                                            </dxe:ASPxTextBox>
                                        </td>
                                     </tr>
                                     <tr>
                                      <td style="width: 10%" >
                                          <dxe:ASPxLabel ID="label7" Text="Email ID" runat="server">
                                          </dxe:ASPxLabel>
                                      </td>
                                      <td style="width: 20%" >
                                         <dxe:ASPxTextBox ID="txtEmailID" runat="server">
                                         </dxe:ASPxTextBox>
                                      </td>
                                      <td style="width: 10%" align="left" runat="server" >
                                                    <dxe:ASPxLabel ID="lblReferringOffice" runat="server" Text="Referring Office" Font-Size="13px" Visible="false" Font-Names="Arial"></dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblRefferingProvider" runat="server" Text="Reffering Provider" Visible="false" Font-Size="13px"></dxe:ASPxLabel>
                                                </td>
                                       <td style="width: 20%">
                                                    <dxe:ASPxComboBox ID="cmbProviderList" runat ="server" Width= "90%" Visible="false"></dxe:ASPxComboBox>
                                                    <dxe:ASPxComboBox ID="cmbOfficeList" runat="server" Width="90%"  Visible="false"
                                                        ></dxe:ASPxComboBox>
                                                    <dxe:ASPxTextBox ID="txtReffProvSearch" runat="server"  CssClass="textboxCSS" Visible="False"></dxe:ASPxTextBox>
                                                    <dxe:ASPxTextBox ID="btnSearchRP" runat="server" Text="Search" Width="80px"  CssClass="Buttons" Visible="False"></dxe:ASPxTextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            &nbsp;
                                            </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td style="width: 20%">
                                                </td>                                       
                                        <td align="left" style="width: 10%">
                                             <dxe:ASPxLabel ID="lblProvider" runat="server" Text="Provider" Visible="false" Font-Size="13px"></dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 20%">
                                               <dxe:ASPxComboBox ID="cmbProvider" runat="server" Width="95%" Visible="false" 
                                                   Height="16px" > </dxe:ASPxComboBox>
                                               <dx:ASPxGridView ID="grdReffProvider" runat="server" 
                                                    Width="61%" CssClass="GridTable" AutoGenerateColumns="false" 
                                                   AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" 
                                                   Visible="false">
                                                    <Columns>
                                                          <dx:GridViewDataColumn>
                                                                <HeaderCaptionTemplate>
                                                                      <dxe:ASPxCheckBox ID="chkSelectAll" runat="server"  />                                                            
                                                                </HeaderCaptionTemplate>
                                                                <DataItemTemplate>
                                                                      <dxe:ASPxCheckBox ID="chkSelete" runat="server" />
                                                                </DataItemTemplate>                                                          
                                                          </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Reffering Provider"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="REFF_PROVIDER_ID" Visible="false"></dx:GridViewDataColumn>                                                                                                                 
                                                      </Columns>
                                               </dx:ASPxGridView>  
                                       </td>
                                  </tr>
                                  <tr>
                                  <td colspan="4">
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td style="width: 14%" valign="top" align="left">
                                                    <dxe:ASPxLabel ID="lblDoctorlst" runat="server" Text="List Of Doctors" Visible="false"
                                                        Font-Names="Arial" Font-Size="12px"></dxe:ASPxLabel>
                                                </td>
                                                <td align="right" style="width: 60%">
                                                    <dxe:ASPxListBox ID="LstBxDoctorList" runat="server" Width="100%" Height="150px" Visible="false"
                                                        SelectionMode="Single"></dxe:ASPxListBox>
                                                </td>
                                                <td align="right" style="width: 13%">
                                                </td>
                                                <td align="right" style="width: 13%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 14%" valign="top" align="right">
                                                    <dx:ASPxCheckBox ID="chkAllowandshow" runat="server" Visible="false" ></dx:ASPxCheckBox>&nbsp;
                                                </td>
                                                <td align="left" style="width: 60%">
                                                    <dxe:ASPxLabel  ID="lblvalidateshow" runat="server" Text="Validate And Show Previous Visits Results"
                                                        Font-Size="13px" Font-Names="Arial" Visible="false"></dxe:ASPxLabel>
                                                </td>
                                                <td align="right" style="width: 13%">
                                                </td>
                                                <td align="right" style="width: 13%">
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td class="ContentLabel" colspan="4">
                                                     <table class="style1">
                                                         <tr>
                                                             <td>
                                                    <dxe:ASPxTextBox ID="txtDoctorID" runat="server" Visible="false" Width="10px"></dxe:ASPxTextBox>
                                                             </td>
                                                             <td>
                                                    <dxe:ASPxTextBox ID="txtCurUserID" runat="server" Visible="false" Width="10px"></dxe:ASPxTextBox>
                                                             </td>
                                                             <td>
                                                    <dxe:ASPxTextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                                             </td>
                                                             <td class="style2">
                                                    <dxe:ASPxTextBox ID="txtIS_PROVIDER" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                                    
                                                             </td>
                                                             <td>
                                                    
                                                    <dxe:ASPxButton ID="btnSave" runat="server" Text="Add" Width="105px"/>
                                                             </td>
                                                             <td>
                                                    <dxe:ASPxButton ID="btnUpdate" runat="server" Text="Update" Width="105px" />
                                                             </td>
                                                             <td>
                                                    <dxe:ASPxButton ID="btnClear" runat="server" Text="Clear" Width="105px"/>
                                                             </td>
                                                             <td>
                                                    <dxe:ASPxButton ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" 
                                                        Width="105px" />
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
                                <td  style="border: 1px solid;">
                                    <table  style="width: 100%">
                                     <tr>
                                        <td  style="width: 100%">
                                        <div style="height: 215px; width: 100%;  overflow: scroll;" >
                                         <dx:ASPxGridView ID="grdUser" runat="server"  Width="100%" AllowPaging="true" 
                                            PageSize="10" PagerStyle-Mode="NumericPages">
                                            <Columns>
                                                <dx:GridViewCommandColumn  ShowSelectButton="true"  ></dx:GridViewCommandColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_USER_ID" Caption="User ID" Visible="False"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_USER_NAME" Caption="User Name"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_USER_ROLE_ID" Caption="User Role ID" Visible="False"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_USER_ROLE" Caption="Role"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_PROVIDER_ID" Caption="Provider ID" Visible="false"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_EMAIL" Caption="Email"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_PASSWORD" Caption="Password" Visible="false"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_OFFICE_ID" Caption="OFFICE ID" Visible="false"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_REFFERING_PROVIDER_ID" Caption="REFF_PROVIDER_ID" Visible="false"></dx:GridViewDataColumn>
                                               <%-- <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True"  Visible="false"/>--%>
                                               <dx:GridViewDataColumn>
                                                <HeaderCaptionTemplate>
                                                     Delete                                                           
                                                </HeaderCaptionTemplate>
                                                        <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="chkDelete" runat="server" />
                                                        </DataItemTemplate>  
                                                
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn>
                                                 <HeaderCaptionTemplate>
                                                         Send To No Diagnosys Page                                                       
                                                </HeaderCaptionTemplate>
                                                        <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="chkDiagnosys" runat="server" />
                                                        </DataItemTemplate>  
                                                
                                                </dx:GridViewDataColumn>
                                                
                                              <dx:GridViewDataColumn FieldName="BT_DIAGNOSYS_PAGE" Caption="Email" Visible="false"></dx:GridViewDataColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                          </div>
                                          </td>
                                          </tr>
                                          </table>
                                    </td>
                                </tr>               
                             </table>
                              </td>
                        <td >
                        </td>
                         </tr>
                    <tr>
                        <td class="style7">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                     </table>
                    </td>
                </tr>
            </table>
   
    </form>
</body>
</html>
