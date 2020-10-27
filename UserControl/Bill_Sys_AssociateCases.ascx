<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateCases.ascx.cs" Inherits="UserControl_Bill_Sys_AssociateCases" %>

<div runat="server" id="divAssociatedCases" class="blocktitle_ql">
    <div align="left" class="blocktitle_adv">Associated Cases</div>
    <div class="div_blockcontent">
        <table width="100%">
		    <tr>
			    <td>
				    <ul style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">
                        <% getHTML();%>
                    </ul>
				</td>
			</tr>
		</table>
    </div>
</div>