<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_ReferalOffice.aspx.cs" Inherits="Bill_Sys_ReferalOffice" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
  <script language="javascript" type="text/javascript" >
    
        function ascii_value(c)
        {
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
    
        function clickButton1(e,charis)
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
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
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
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                
                                                
                                                <tr>
                       <td class="ContentLabel" style="width: 15%">
                    Office Name</td>
                     <td class="ContentLabel" style="width: 35%"><asp:TextBox ID="txtOfficeName" runat="server"></asp:TextBox></td>
                       <td class="ContentLabel" style="width: 15%">
                    Office type</td>
               <td class="ContentLabel" style="width: 35%">
                    <cc1:ExtendedDropDownList ID="extddlOfficeType" width="150px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_OFFICE_TYPE" Flag_Key_Value="GET_OFFICE_TYPE_LIST" Selected_Text="--- Select ---" />
                </td>
            </tr>
            <tr>
                <td class="ContentLabel" style="width: 15%">Address </td>
                <td class="ContentLabel" style="width: 35%"><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
           
                 <td class="ContentLabel" style="width: 15%">City </td>
                <td class="ContentLabel" style="width: 35%"><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="ContentLabel" style="width: 15%">
                    State</td>
            <td class="ContentLabel" style="width: 35%"><asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
            
                <td class="ContentLabel" style="width: 15%">Zip</td>
               <td class="ContentLabel" style="width: 35%"><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td>
            </tr>
            
            
            
                                          
                                         
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">                                               
                                               
                                                   <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>

                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" CssClass="Buttons" />
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                        Width="80px" CssClass="Buttons" />
                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" CssClass="Buttons" />
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
                                    <td style="width: 100%" class="TDPart">
                                    
                                     <asp:DataGrid ID="grdReferalOfficeList" runat="server" 
               
                        OnDeleteCommand="grdReferalOfficeList_DeleteCommand"
                        OnPageIndexChanged="grdReferalOfficeList_PageIndexChanged" OnSelectedIndexChanged="grdReferalOfficeList_SelectedIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                       
                       <ItemStyle CssClass="GridRow"/>
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="SZ_REFERAL_OFFICE_ID" HeaderText="Referal Office ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_OFFICE_TYPE_ID" HeaderText="Office Type ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_OFFICE_TYPE" HeaderText="Office Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ADDRESS" HeaderText="Address"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_CITY" HeaderText="City" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_STATE" HeaderText="State" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ZIP" HeaderText="Zip" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID"  Visible="false" ></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                        </Columns>
                       <HeaderStyle CssClass="GridHeader"/>
                    </asp:DataGrid>
                                        
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
</asp:Content>


