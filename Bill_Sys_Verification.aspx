<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Verification.aspx.cs"
    Inherits="Bill_Sys_Verification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <style>
	 	.blocktitle_ql{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:0px;
		}

		.blocktitle_lm{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:0px;		
		}
				
		.blocktitle{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:10px;
		}
		
		.blocktitle_adv{
			width:100%;
			height: 20px;
			text-align:left;
			background-color:#CCCCCC;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
			padding-top:2px;
		}
		
		.block{
			width:100%; 
			height: 200px;
			vertical-align:middle;
		}
		
		DIV#m_content_adv1{
			position:absolute;
			left:1000px;
			top:105px;
			width:270px;
		}

		DIV#m_content_adv2{
			position:absolute;
			left:1000px;
			top:290px;
			width:270px;
		}

		.m_content{
			position:absolute;
			left:225px;
			top:100px;
			border:1px solid;
			width:inherit;
		}
		
		.div_messages{
			font:Arial, Helvetica, sans-serif;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
			width:100%;
			height:20px;
			padding-top:10px;
			text-align:center;
			/*visibility:hidden*/
		}
		
		.div_blockcontent{
			width:100%; 
			vertical-align:middle; 
			background-color:#ffffff;

			/*padding-right:10%;*/
		}
		
		.div_blockcontent_field_label{
		
		}
		
		DIV#div_content_adv{
			border-color:#8ba1ca;
			background-color:#FFFFFF;
			height:200px;
			border:1px;
		}

		TABLE#content_table{
			border-color:#8ba1ca;
			text-align:left;
			font:Arial, Helvetica, sans-serif;
			font-family:Arial, Helvetica, sans-serif;
			font-size:13px;
			font-weight:normal;
			height: 100%; 
			width: 100%;
		}
		INPUT#selectbox{
			width:inherit;
		}
		DIV#div_contect_field_buttons{
			position:inherit;
			border:0px;
			height:40px;
			text-align:center;
			background-color:#F0F0F0;
			padding-top:5px;
		}
		.btnsignin{
			background-color:#CCCCCC;
			color:#000000;
			font-size:12px;
			font-weight:bold;
			height:22px;
			border:1px solid;
			text-align:center;
			padding-bottom:4px;
		}
		
		.lcss_maintable{
			width:100%;
			height:2%;
			cellpadding:0; 
			cellspacing:0; 
			border: 1px solid #D8D8D8;
		}
	</style>
</head>
<body>
    <form id="femDenial" runat="server">
        <div align="center">
            <table width="100%" id="content_table" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="3">
                        Verification Request
                    </td>
                </tr>
                <tr>
                    <td width="83%" scope="col" style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="3" scope="col" style="height: 233px; width: 310px;">
                                    <div id="div_contect_field_buttons">
                                        <table>
                                            <tr>
                                                <td colspan="2" width="100%" align="left">
                                                    Notes
                                                </td>
                                                <td colspan="2" width="100%" align="left">
                                                    <asp:TextBox ID="txtNotes" runat="server" Height="57px" Width="182px" MaxLength="50"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" width="100%" align="center">
                                                    <extddl:ExtendedDropDownList ID="extddlNoteType" runat="server" Visible="false" Flag_Key_Value="LIST"
                                                        Selected_Text="---Select---" Procedure_Name="SP_MST_NOTES_TYPE" Connection_Key="Connection_String"
                                                        Width="225px"></extddl:ExtendedDropDownList>
                                                    <asp:TextBox ID="txtNoteDesc" runat="server" Visible="False" Height="0px" Width="0px"
                                                        MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtNoteCode" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnOK" runat="server" Text="Send Request" Width="100px" OnClick="btnOK_Click"
                                                        CssClass="Buttons" />
                                                    <input id="Button1" type="button" value="Cancel" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';"
                                                        class="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
