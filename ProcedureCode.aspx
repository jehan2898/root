<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcedureCode.aspx.cs" Inherits="ProcedureCode1" %>


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
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
            height: 22px;
            width: 326px;
        }
        .style7
        {
            width: 326px;
        }
        .style9
        {
            height: 22px;
            width: 196px;
        }
        .style10
        {
            width: 196px;
        }
        .style12
        {
            width: 146px;
        }
        .style13
        {
            width: 99px;
        }
        .style15
        {
            width: 19px;
        }
        .style17
        {
            width: 850px;
        }
        .style18
        {
            width: 695px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
     <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="CenterTop" colspan="2">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="ContentLabel" style="text-align: center;" colspan="3">                                                
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                            <div id="ErrorDiv" style="color: red" visible="true">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="Center" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                 <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td width="100%" style="border: 1px solid;">
                                        <table width="100%">
                                            <tr>
                                            <td style="height: 10px; background-color: #CCCCCC; font-family: Kalinga; font-size: large; color: #0000FF;">
                                                 <span style="color: #800080; font-size: medium; font-weight: bold;"> &nbsp; 
                                                 PARAMETERS                                    PARAMETERS</span></td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart" align="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="style15">
                                                                    &nbsp;</td>
                                                            <td class="style18">
                                                                &nbsp;</td>
                                                            <td class="style6">
                                                               
                                                                &nbsp;</td>
                                                            <td class="style9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                             <td class="style15">
                                                                 &nbsp;</td>
                                                             <td class="style18" style="font-family: calibri; font-size: medium">
                                                               Code
                                                             </td>
                                                             <td class="style7">
                                                                <dxe:ASPxTextBox ID="txtCode" runat="server" Width="220px">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                             <td class="style10" style="font-family: calibri; font-size: medium">
                                                                Specialty
                                                             </td>
                                                             <td>
                                                              <dxe:ASPxComboBox ID="cmbSpecialty" runat="server" Connection_Key="Connection_String" width="225px"
                                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST">
                                                             </dxe:ASPxComboBox>
                                                             
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                             <td class="style15">
                                                                 &nbsp;</td>
                                                             <td class="style18" style="font-family: calibri; font-size: medium">
                                                                Short Description
                                                             </td>
                                                             <td class="style7">
                                                                <dxe:ASPxTextBox ID="txtShortDescription" runat="server" Width="220px">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                             <td class="style10" style="font-family: calibri; font-size: medium">
                                                              Amount
                                                            </td>
                                                            <td>
                                                            
                                                               <dxe:ASPxTextBox ID="txtAmount" runat="server" Width="220px">
                                                               </dxe:ASPxTextBox>
                                                              <dxe:ASPxComboBox ID="ddlType" runat="server" CssClass="textboxCSS" Visible="false">
                                                                   <Items>
                                                                   <dx:ListEditItem Value="0" Text= "--Select--" />
                                                                   <dx:ListEditItem Value="TY000000000000000001" Text="Visits" />
                                                                   <dx:ListEditItem Value="TY000000000000000002" Text="Treatments" />
                                                                   <dx:ListEditItem Value="TY000000000000000003" Selected="True" Text="Test" />
                                                                   </Items>
                                                              </dxe:ASPxComboBox>
                                                            
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                                &nbsp;</td>
                                                            <td class="style18" style="font-family: calibri; font-size: medium">
                                                               Long Description
                                                           </td>
                                                           <td class="style7">
                                                             <dx:ASPxMemo ID="MemoLongDescription" runat="server" Height="70px"
                                                                           Width="220px">
                                                             </dx:ASPxMemo>
                                                            </td>
                                                           <td class="style10" style="font-family: calibri; font-size: medium">
                                                               Visit Type
                                                            </td>
                                                            <td>
                                                              <dx:ASPxRadioButtonList ID="rbVisitType" runat="server" 
                                                                                      RepeatDirection="Horizontal" Width="220px"  
                                                                    Border-BorderWidth="0">
                                                                            <Items>
                                                                            <dx:ListEditItem Value="0" Text="IE" />
                                                                            <dx:ListEditItem Value="1" Text="FU" />
                                                                            </Items>                                                                                                              

                                                                   <Border BorderWidth="0px"></Border>
                                                               </dx:ASPxRadioButtonList>                                                            
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                                &nbsp;</td>
                                                            <td class="style18" style="font-family: calibri; font-size: medium">
                                                            PAS code
                                                            </td>
                                                            <td class="style7">
                                                              <dxe:ASPxTextBox ID="txtPAScode" runat="server" Width="220px">
                                                              </dxe:ASPxTextBox>
                                                           </td>
                                                            <td class="style10" style="font-family: calibri; font-size: medium">
                                                               Revenue Code
                                                           </td>
                                                           <td>
                                                               <dxe:ASPxTextBox ID="txtREVcode" runat="server" Width="220px">
                                                               </dxe:ASPxTextBox>
                                                           </td>
                                                        </tr>
                                                        <tr>
                                                             <td class="style15">
                                                                 &nbsp;</td>
                                                             <td class="style18" style="font-family: calibri; font-size: medium">
                                                              RVU
                                                          </td>
                                                          <td class="style7">
                                                              <dx:ASPxMemo ID="MemoRVU" runat="server" Height="70px"
                                                                           Width="220px">
                                                              </dx:ASPxMemo>
                                                          </td>
                                                          <td class="style10" style="font-family: calibri; font-size: medium">
                                                               Modifier
                                                           </td>
                                                           <td>
                                                            <dxe:ASPxTextBox ID="txtModifier" runat="server" Width="220px"> 
                                                            </dxe:ASPxTextBox>
                                                           </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                             <td class="style15">
                                                                 &nbsp;</td>
                                                             <td class="style18">
                                                                 Modifier Description</td>
                                                          <td class="style7">
                                                             <dxe:ASPxTextBox ID="txtModifierDesc" runat="server" TextMode="MultiLine"  MaxLength="1500" Width="500px" Height="90px" Visible="false"></dxe:ASPxTextBox></td>
                                                          <td class="style10" style="font-family: calibri; font-size: medium">
                                                                 Add To preferred List
                                                          </td>
                                                          <td>
                                                               <dxe:ASPxCheckBox ID="cbPreferredList0" runat="server" Width="220px">
                                                               </dxe:ASPxCheckBox>
                                                          </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                             
                                                <td colspan="2">
                                                    <dx:ASPxGridView ID="grdAmount" runat="server" Width="100%" 
                                                        AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" 
                                                        Visible="false" >                                                      
                                                        <Columns>
                                                            <dx:GridViewDataColumn  Caption="SZ_PROCEDURE_AMOUNT_ID"
                                                                Visible="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn  Caption="SZ_CASE_TYPE_ID" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="Case Type"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn>   
                                                            <HeaderCaptionTemplate>                                                            
                                                                    <dxe:ASPxTextBox ID="txtAmount" runat="server"  MaxLength="10" Text='<%# DataBinder.Eval(Container.DataItem, "SZ_AMOUNT") %>'>
                                                                    </dxe:ASPxTextBox>
                                                            </HeaderCaptionTemplate>
                                                            </dx:GridViewDataColumn>
                                                           <dx:GridViewDataColumn Caption="SZ_PROCEDURE_CODE_ID" Visible="false"></dx:GridViewDataColumn>
                                                        </Columns>                                                       
                                                  </dx:ASPxGridView>
                                                  </td>
                                          
                                                             
                                                             </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                             <td class="style15">
                                                                   
                                                             </td>
                                                            <td  colspan="4">
                                                               
                                                                <table  style=" text-align:right;">
                                                                    <tr>
                                                                        <td>
                                                                             &nbsp;</td>
                                                                        <td>
                                                                             &nbsp;</td>
                                                                        <td>
                                                                             &nbsp;</td>
                                                                        <td class="style17">
                                                                             &nbsp;</td>
                                                                        <td>
                                                                             <dxe:ASPxButton ID="btnsearch" runat="server" Text="Search" Width="100px"
                                                                                 ></dxe:ASPxButton></td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btnAdd" Text="Add" runat="server" Width="100px" onclick="btnAdd_Click" 
                                                                                ></dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                             <dxe:ASPxButton ID="btnUpdate" runat="server" Text="Update" Width="100px" onclick="btnUpdate_Click" 
                                                                                 ></dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                          <dxe:ASPxButton ID="btnClear" runat="server" Text="Clear" Width="100px">
                                                                            </dxe:ASPxButton> 
                                                                        </td>
                                                                        </tr>
                                                                </table>
                                                            
                                                                  </tr>
                                                               </table>
                                                               
                                                            </td>
                                                             
                                                        </tr>
                                                        
                                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="1px">
                                    </td>
                                   
                                </tr>
                              
                            </table>
                        </td>
                        
                     </tr>
                               
                                <tr>
                                    <td  style="width: 100%; text-align:right; height: 44px;" colspan="2">
                                        <table class="style4">
                                            <tr>
                                                <td class="style12">
                                                   <dxe:ASPxButton ID="btnPreferredList" Text="Update Preferred List"  
                                                        runat="server" Width="159px"></dxe:ASPxButton>
                                                </td>
                                                <td class="style13">
                                                   <dxe:ASPxButton ID="ASPxButton1" Text="Update Modifier"  runat="server" 
                                                        Width="159px"></dxe:ASPxButton>    
                                                </td>
                                                <td>
                                                   <dxe:ASPxButton ID="ASPxButton2" Text="Delete"  runat="server" Width="159px"></dxe:ASPxButton>      
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td>
                                   <table>
                                       <td>
                                          Search
                                       </td>
                                       <td>
                                          <dxe:ASPxTextBox ID="txtSearch" runat="server" Width="180px" >
                                          
                                          </dxe:ASPxTextBox>
                                       </td>
                                   </table>
                                  </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2" >
                                        &nbsp;</td>
                               </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2" >
                                        <div style="overflow: scroll; height: 400px; width: 99%;">
                                            <dx:ASPxGridView ID="grdProcedure" runat="server"  Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PagerStyle-Mode="NumericPages" >                                                
                                                <Columns>
                                                    <dx:GridViewCommandColumn  ShowSelectButton="true"  ></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn Caption="PROCEDURE ID" Visible="False"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="Procedure Code"  Visible="true"></dx:GridViewDataColumn>                                                   
                                                   <dx:GridViewDataColumn Caption="description" visible="true"></dx:GridViewDataColumn>
                                                   <dx:GridViewDataColumn Caption="Amount" Visible="True"></dx:GridViewDataColumn>
                                                   <dx:GridViewDataColumn Caption="SZ_PROCEDURE_GROUP_ID" Visible="False"></dx:GridViewDataColumn>

                                                   <dx:GridViewDataColumn Caption="Speciality" Visible="true">                                  
                                                   </dx:GridViewDataColumn>
                                                   <dx:GridViewDataColumn Caption="Modifier" Visible="True"></dx:GridViewDataColumn> 
                                                   <dx:GridViewDataColumn Caption="Rev Code" Visible="True"></dx:GridViewDataColumn>
                                                   <dx:GridViewDataColumn Caption="Value Code" Visible="True"></dx:GridViewDataColumn>                                                                                                   
                                                    <dx:GridViewDataColumn Caption= "Add To Preferred List" Visible="true"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption= "Procedure Full Description" Visible="true"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="RVU" Visible="True"></dx:GridViewDataColumn> 
                                                    <dx:GridViewDataColumn Caption="VisitType" Visible="false" ></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="SZ_TYPE_CODE_ID" Visible="False"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="Type" Visible="False"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption="Room" Visible="False"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption ="Type" Visible="False"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn caption="Room" Visible="False"></dx:GridViewDataColumn>                                                  

                                                    <dx:GridViewDataColumn Caption="Speciality" Visible="false"></dx:GridViewDataColumn>
                                                   
                                                    <dx:GridViewDataColumn>
                                                            <HeaderCaptionTemplate>
                                                                <dxe:ASPxCheckBox ID="chkSelectAll" runat="server"  />                                                            
                                                            </HeaderCaptionTemplate>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="chkSelect" runat="server" />
                                                            </DataItemTemplate>                                                          
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                               
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="height: 100%;">
                        </td>
                    </tr>
                       <tr>
                        <td colspan="2" align="center">
                            <table class="style4">
                                <tr>
                                    <td>
                            <dxe:ASPxTextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtprocedureID" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtProcedureGroup" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtIndex" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtModifierLongDesc" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txthdnModifier" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txthdnCode" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtRoomId" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtVisitID" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txthdnDesc" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtRVU" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                    <td>
                            <dxe:ASPxTextBox ID="txtPreList" runat="server" Visible="False" Width="10px"></dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                     </tr>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                            &nbsp;</td>
                        <td class="CenterBottom">
                            &nbsp;</td>
                        <td class="RightBottom">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
