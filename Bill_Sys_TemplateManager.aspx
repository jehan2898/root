<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="Bill_Sys_TemplateManager.aspx.cs" Inherits="Bill_Sys_TemplateManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="validation.js"></script>
    
    <script language="javascript" type="text/javascript">
            	
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
         
         
      
    </script>

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
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="4" style="width: 100%; height: 400px">
                                 <tr>
                                    <td style="width: 100%;height: 40px;" class="TDPart">
                                         <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                            <Columns>
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
                                    <td style="width: 100%;text-align:left;height: 360px;"  class="TDPart" valign="top">
                                        <table width= "100%" cellpadding="0" cellspacing="4">
                                             
                                            <tr>
                                                <td align="right" style="width: 42%"> Select Template &nbsp;
                                                </td>
                                                <td width="50%" align="left">
                                                <extddl:ExtendedDropDownList ID="ddlTemplate" runat="server" AutoPost_back="false"  Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_TEMPLATE_LIST" Procedure_Name="SP_GET_LIST_MST_TEMPLATES" Selected_Text="---Select---"
                                                        Width="300px" />
                                                    <%--<asp:DropDownList ID="ddlTemplate" runat="server" Width="272px">
                                                        <asp:ListItem Text="--- Select ---" value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Funding" value="1"></asp:ListItem>
                                                        <asp:ListItem Text="LSC K Single Merged 6mTemp" value="2"></asp:ListItem>
                                                        <asp:ListItem Text="LSC K Single Merged 9m" value="3"></asp:ListItem>
                                                        <asp:ListItem Text="LSC Payoff Ltr Templ" value="4"></asp:ListItem>
                                                        <asp:ListItem Text="LSC Pltf Settled Funding K Merged" value="5"></asp:ListItem>
                                                        <asp:ListItem Text="LSC Pltf Settled Funding K temp" value="6"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI K AFR wPayoff Multiple Merged9m" value="7"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI K Mult wPayoff Merged 9m" value="8"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI K Single Merged 9m" value="9"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI K AFR 9m" value="10"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI K AFR Multiple 9m" value="11"></asp:ListItem>
                                                         <asp:ListItem Text="LSFI K Multiple 9m" value="12"></asp:ListItem>
                                                         <asp:ListItem Text="LSFI K Multiple Buyout 9m" value="13"></asp:ListItem>
                                                         <asp:ListItem Text="LSFI K Single 9m" value="14"></asp:ListItem>
                                                         <asp:ListItem Text="LSFI K Single Buyout 9m" value="15"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI Pltf Settled Funding K" value="16"></asp:ListItem>
                                                        <asp:ListItem Text="LSFI ShoreRd K Single 9m" value="17"></asp:ListItem>                                                         
                                                    </asp:DropDownList>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                  <%--  <asp:RadioButtonList ID="rdSelectOne" runat="server" RepeatDirection="Horizontal" Width="226px">
                                                        <asp:ListItem Value="PDF" Selected="True">PDF</asp:ListItem>
                                                        <asp:ListItem Value="MS-Word">MS-Word</asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtUserID" runat="server" Visible="false" Width="10px"></asp:TextBox><asp:TextBox
                                                        ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:Button ID="btnGeneratePDF" Text="Generate Document" runat="server" CssClass="Buttons" OnClick="btnGeneratePDF_Click"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                   
                                                    
                                                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                                      <%--<Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="IntervalTimer" EventName="Tick" />
                                                        </Triggers>--%>
                                                        <contenttemplate>
                                                        <img id="imgWait" runat="server" src="Images/bigrotation2.gif" visible="false"  />
                                                     <asp:LinkButton  id="lnkOpenDoc" runat="server" visible="false">Download</asp:LinkButton>
                                                        <%--<asp:Timer ID="IntervalTimer"  runat="server"  OnTick="IntervalTimer_Tick">
                                                        
                                                      </asp:Timer>--%>
                                                      
                                                        </contenttemplate>
                                                       
                                                    </asp:UpdatePanel>
                                                    
                                                      
                                                    </td>
                                            </tr>
                                        </table>
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
</asp:Content>
