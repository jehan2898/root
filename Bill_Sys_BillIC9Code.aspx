<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BillIC9Code.aspx.cs"
    Inherits="Bill_Sys_BillIC9Code" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>
    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
		
			
			function Amountvalidate()
			{
			   var status=formValidator('frmIC9Code','txtBillNo,extddlIC9Code,txtUnit,txtAmount,txtWriteOff,txtDescription');
		      if (status!=false)
		      {
			   
			    if (document.getElementById('txtAmount').value > 0 )//&& document.getElementById('txtAmount')!= '' && document.getElementById('txtAmount')!= '0.00')
			    {
			        return  true;
			    }
			    else
			    {
			           
			           document.getElementById('ErrorDiv').innerHTML='Enter the amount greater than 0';							
						document.getElementById('txtAmount').focus();
			         return  false;
			    }
			  }
			  else
			  {
			    
			    return  false;
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

    function CalculateAmount()
    {
        var txtAmount = document.getElementById('txtAmount');
        var txtUnit = document.getElementById('txtUnit');
       var tempAmt =  document.getElementById('txtTempAmt');
        if(txtAmount.value!="")
        {
            txtAmount.value = tempAmt.value * txtUnit.value;
        }        
    }    
    </script>

</head>
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmIC9Code" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td width="9%" height="18">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu">Home | Logout</span></div>
                    </td>
                    <td width="8%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="9%" class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            Height="24px"></cc2:WebCustomControl1>
                    </td>
                    <td width="8%" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="35px" bgcolor="#000000">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">Data saved successfully...</span></td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="19%" scope="col" style="height: 29px">
                                    <div align="left">
                                       <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu">
                                            <asp:LinkButton ID="lnkShowPaidBills" CssClass="sub-menu" runat="server">Paid Cases</asp:LinkButton>
                                            |
                                            <asp:LinkButton ID="lnkUnpaidCases" CssClass="sub-menu" runat="server">Un-paid Cases</asp:LinkButton>
                                            |
                                            <asp:LinkButton ID="lnkLitigationDesk" CssClass="sub-menu" runat="server">Litigation Desk</asp:LinkButton>
                                            |
                                            <asp:LinkButton ID="lnkWriteOffDesk" runat="server" CssClass="sub-menu">Write-Off desk</asp:LinkButton>
                                        </span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                    <td class="top-menu" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" height="409">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="6" class="usercontrol">
                                    <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                                </td>
                            </tr>
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Bill IC9 Code</div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
                                    <div align="left">
                                        <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td colspan="6" height="30">
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                  <div class="lbl">  Bill Number</div> </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="250px" MaxLength="50" ReadOnly="True"
                                                        Style="border-top-style: none; border-right-style: none; border-left-style: none;
                                                        border-bottom-style: none" BorderColor="Transparent"></asp:TextBox></td>
                                                <td class="tablecellLabel">
                                                   <div class="lbl"> IC9 Code</div> </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <extddl:ExtendedDropDownList ID="extddlIC9Code" runat="server" Width="255px" AutoPost_back="True"
                                                        Connection_Key="Connection_String" Flag_Key_Value="GET_IC9CODE_LIST" OnextendDropDown_SelectedIndexChanged="extddlIC9Code_extendDropDown_SelectedIndexChanged"
                                                        Procedure_Name="SP_MST_IC9_CODE" Selected_Text="---Select---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                   <div class="lbl"> IC9 Code Amount</div> 
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <input type="text" id="txtTempAmt" runat="server" style="width: 250px;" onkeypress="return clickButton1(event,'/')" />
                                                </td>
                                                <td class="tablecellLabel">
                                                   <div class="lbl"> Unit</div> 
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtUnit" runat="server" Width="250px" MaxLength="50" onkeypress="return clickButton1(event,'/')"
                                                        onchange="CalculateAmount();"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                  <div class="lbl">  Amount</div> </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtAmount" runat="server" Text="" Width="250px" Columns="3" onkeypress="return clickButton1(event,'/')"></asp:TextBox></td>
                                                <td class="tablecellLabel">
                                                   <div class="lbl"> Write Off</div> </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtWriteOff" runat="server" Text="" Width="252px" onkeypress="return clickButton1(event,'/')"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                   <div class="lbl"> Description</div> </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtDescription" runat="server" Text="" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>&nbsp;
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click" cssclass="btn-gray"/>
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" cssclass="btn-gray"/>
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click" cssclass="btn-gray"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </th>
                            </tr>
                            <tr>
                                <th scope="col">
                                    <div align="left">
                                    </div>
                                    <div align="left" class="band">
                                        IC9 Code List</div>
                                </th>
                            </tr>
                            <tr>
                                <th valign="top" scope="col">
                                    <asp:DataGrid ID="grdIC9Code" runat="server" ShowFooter="True" OnPageIndexChanged="grdIC9Code_PageIndexChanged"
                                        OnSelectedIndexChanged="grdIC9Code_SelectedIndexChanged">
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle />
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="SZ_BILL_IC9_CODE_ID" HeaderText="IC9 CODE ID" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_IC9_ID" HeaderText="IC9 ID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_BILL_ID" HeaderText="BILL ID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="I_UNIT" HeaderText="UNIT" ItemStyle-HorizontalAlign="Right">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right"
                                                DataFormatString="{0:0.00}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="WRITE OFF" ItemStyle-HorizontalAlign="Right"
                                                DataFormatString="{0:0.00}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ACTUAL_AMOUNT" HeaderText="ACTUAL AMOUNT" Visible="False">
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle />
                                    </asp:DataGrid>
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
