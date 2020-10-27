<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" CodeFile="VerificationReason.aspx.cs" Inherits="AJAX_Pages_VerificationReason" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <contenttemplate>
                <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
            <table width="100%">
                <tr>
                    <td align="center">

                        <dx:ASPxLabel ID="lblmsg" CssClass="message-text" runat="server" Visible="false">
                        </dx:ASPxLabel>

                    </td>
                </tr>


            </table>

                                    <table width="100%" border="1" cellpadding="0" >
                                   <tr style="width: 100%">
                                            <td align="center">
                                                    Verification
                                            </td>
   
                                                     <td width="70%" class="registration-form-ibox" >
                                                                <dxe:ASPxMemo ID="memo_descver" runat="server" Width="500px" Height="100px">
                                                                </dxe:ASPxMemo>
                              
                                                      </td>
               
   
                                    </tr>
                                   </table>
                                 <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100%; vertical-align: top;">
                                                        <table style="width: 100%;">
                                                              <tr>
                           
                                                                <td align="right" colspan="2"  >
                                                                    <dx:ASPxButton runat="server" Text="Save" ID="btnadd" onclick="btnadd_Click">
                                                                    </dx:ASPxButton></td>
                                                                    <td align="left" colspan="2" >
                                                                    <dx:ASPxButton runat="server" Text="Reset" ID="btnupdate" Enabled="false"
                                                                        onclick="btnupdate_Click" >
                                                                    </dx:ASPxButton>
                                                                </td>
                            
                                                            </tr>


                                                         </table>
                                                        <table>
                                                            <tr>
                                                            <td>
                                                            
                                                                     <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2" displayafter="10">
                                                                        <progresstemplate>
                                                                            <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                               left: 600px" runat="Server">
                                                                                      <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                             Loading...</div>
                                                                         </progresstemplate>
                                                                      </asp:updateprogress>
                                                            
                                                            
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td></td>
                                                            </tr>


                                                            <tr>
                                                            <td></td>
                                                            </tr>
                                                            <tr>
                                                            <td></td>
                                                            </tr>
                                                             <tr>
                                                            <td></td>
                                                            </tr>
                                                        </table>

                                                <table style="width: 100%">
                                 
                                                                    <tr>
                                                                        <td style="width: 100%">
                                                                            <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;" >
                                                                                <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="i_reason_id" AutoGenerateColumns="false"
                                                                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                                                    Settings-VerticalScrollableHeight="330" 
                                                                                    onrowcommand="grdVisits_RowCommand">
                                                                                    <columns>
                                                                                                  
                                                                                     <dx:GridViewDataColumn Caption="" Settings-AllowSort="False" Width="25px">
                                                    
                                                                                            <DataItemTemplate>
                                                                                                <asp:linkbutton id="lnkAddVisit" runat="server" text='Select' CommandName="select">
                                                                                                </asp:linkbutton>
                                                                                            </DataItemTemplate>
                                                                                      </dx:GridViewDataColumn>
                                                    
                                                                                       <dx:GridViewDataColumn FieldName="sz_reason" Caption="Verification Reason" HeaderStyle-HorizontalAlign="Center"
                                                                                            Visible="true" HeaderStyle-Font-Bold="true" >
                                                                                      </dx:GridViewDataColumn>                                                                                    
                                                  
                                                                                        <%--1--%>
                                                                                        <dx:GridViewDataColumn FieldName="i_reason_id" Caption="ireasonid" HeaderStyle-HorizontalAlign="Center"
                                                                                            Visible="false" HeaderStyle-Font-Bold="true" >
                                                                                        </dx:GridViewDataColumn>
                                                                                        <%--2--%>
                                                                                          <dx:GridViewDataColumn FieldName="sz_company_id" Caption="company id" HeaderStyle-HorizontalAlign="Center"
                                                                                            Visible="false" HeaderStyle-Font-Bold="true">
                                                                                          </dx:GridViewDataColumn>
                                                                                        <%--3--%>
                                                                                         <dx:GridViewDataColumn FieldName="sz_created" Caption="sz_created " 
                                                                                          HeaderStyle-HorizontalAlign="Center"
                                                                                            Visible="false" HeaderStyle-Font-Bold="true">
                                                                                        </dx:GridViewDataColumn>
                                                                                        <%--4--%>
                                                                                        <dx:GridViewDataColumn HeaderStyle-HorizontalAlign="Center"
                                                                                         FieldName="dt_created" Caption="dt_created"   Visible="false" HeaderStyle-Font-Bold="true" >
                                                                                        </dx:GridViewDataColumn>

                                                                                          <%--5--%>
                                                                                        <dx:GridViewDataColumn FieldName="sz_modified" Caption="sz_modified" HeaderStyle-HorizontalAlign="Center"
                                                                                            Visible="false" HeaderStyle-Font-Bold="true">
                                                                                        </dx:GridViewDataColumn>
                                                                                          <%--6--%>
                                                                                        <dx:GridViewDataColumn FieldName="dt_modified" Caption="dt_modified" HeaderStyle-HorizontalAlign="Center" Visible="false"
                                                                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                                                         </dx:GridViewDataColumn>
                                                     
                                                                                         <dx:GridViewDataColumn Caption="" Settings-AllowSort="False" Width="25px">
                                                                                             <DataItemTemplate>
                                                                                                <asp:linkbutton id="lnkAddVisit" runat="server" text='Delete' commandname="delete" CommandArgument="delete">
                                                                                                </asp:linkbutton>
                                                                                             </DataItemTemplate>
                                                                                         </dx:GridViewDataColumn>

                                                                                </columns>
                                                                                    <settingsbehavior allowfocusedrow="True" allowsort="False" />
                                                                                    <settingspager pagesize="20"></settingspager>
                                                                                    <settings verticalscrollableheight="330"></settings>
                                                                                    <settingscustomizationwindow height="330px"></settingscustomizationwindow>
                                                                                    <styles>
                                                                                    <FocusedRow CssClass="dxgvFocusedGroupRow">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                    <SelectedRow CssClass="dxgvFocusedGroupRow">
                                                                                    </SelectedRow>
                                                                                </styles>
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                     </td>
                                                                 </tr>
                                                      </table>
                                            </td>
                                        </tr>
                                    </table>
                                        <dxe:aspxtextbox CssClass="inputBox" runat="server" ID="txt_reasonid" Width="195px"  visible="false"
                                            Height="30px">
                                        </dxe:aspxtextbox>
                                        <dxe:aspxtextbox CssClass="inputBox" runat="server" ID="txt_companyid" Width="195px"  visible="false"
                                            Height="30px">
                                        </dxe:aspxtextbox>
        </contenttemplate>
    </asp:updatepanel>
</asp:Content>