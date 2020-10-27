<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_DocumentType.aspx.cs" Inherits="Bill_Sys_DocumentType" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="MasterScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<script type="text/javascript" src="validation.js"></script>--%>
    <%--<script type="text/javascript" src="ListBox.js"></script>--%>

    <script type="text/javascript">
      Sys.Application.add_load(function() 
       {     
//            var objSelDoc=document.getElementById("<%=lbSelectedDocs.ClientID%>");
//            var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
//            for(i=0;i<list.length - 1;i++)5
//            {
//                 var no = new Option();
//                 no.value = list[i].split('~')[1];
//                 no.text = list[i].split('~')[0];  
//                 objSelDoc[i] = no; 
//            }
               
       });
      
        function OnCheckBoxCheckChanged(evt) 
        {           
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;           
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");  
             var ddl = document.getElementById("<%=ddUserRole.ClientID%>");
            value = ddl.options[ddl.selectedIndex].value;
            if(isChkBoxClick && value == "0")
           {
                src.checked = false;
                alert('Select User Role.');            
           }
           else
           {   
            if (isChkBoxClick) 
            {        
                var parentTable = GetParentByTagName("table", src); 
                var nxtSibling = parentTable.nextSibling;   
                
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node 
                {         
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children           
                    {           
                        //check or uncheck children at all levels   
                        if(src.checked)
                        {
                            AddToNodeList(src);
                        }
                        else
                        {
                            RemoveFromList(src);
                        }
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);           
                    } 
                    else
                     {
                        if(src.checked)
                        {
                            AddToNodeList(src);
                        }
                        else
                        {
                            RemoveFromList(src);
                        }
                     }          
                }
                else
                {
                    if(src.checked)
                    {
                        AddToNodeList(src);
                    }
                    else
                    {
                        RemoveFromList(src);
                    }
                }           
                 //check or uncheck parents at all levels           
                //CheckUncheckParents(src, src.checked);  
                         
            }      
            }     
        }   
         function GetParentByTagName(parentTagName, childElementObj) 
         {           
            var parent = childElementObj.parentNode;           
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) 
            {           
                parent = parent.parentNode;           
            }           
            return parent;           
         }  
         function RemoveFromList(node)
         {
                    var elm1 = node.nextSibling;
                   var opt = document.createElement("option");
                   var str = elm1.title.split("(");
                   var path = "";
                   var value = "";
                   for(j=0;j<str.length;j++)
                   {
                       if(j==0)
                       {
                            path = path + str[j].substring(0);
                       }
                       else
                       {
                             path = path + str[j].split(")")[1];
                             value = str[j].split(")")[0];
                       }
                   }
                   opt.text = path;
                   opt.value = value; 
                    var listbox=document.getElementById("<%=lbSelectedDocs.ClientID%>");
                    var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
                    var arrText = new Array();
                    var arrValue = new Array();
                    var count = 0;
                    for(i = 0; i < listbox.options.length; i++)
                    {
                        if (listbox.options[i].value != value)
                        {
                            arrText[count] = listbox.options[i].text;
                            arrValue[count] = listbox.options[i].value;
                            count++;
                        }
                        else
                        {
                            document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value.replace(path+'~'+value + ',','');
                        }                       
                    }
                    listbox.length = 0;
                    for(index = 0; index < arrText.length; index++)
                    {
                        var no = new Option();
                        no.value = arrValue[index];
                        no.text = arrText[index];  
                        listbox[index] = no;     
                    }
                    
         }   
         function AddToNodeList(node)
         {
                   var elm1 = node.nextSibling;
                   var opt = document.createElement("option");
                   var str = elm1.title.split("(");
                   var path = "";
                   var value = "";
                   //alert(str);
                   for(j=0;j<str.length;j++)
                   {
                       if(j==0)
                       {
                            path = path + str[j].substring(0);
                       }
                       else
                       {
                      // alert("str[j].substring(0) :"+str[j].split(")")[1]);
                             path = path + str[j].split(")")[1];
                             value = str[j].split(")")[0];
                             //alert(path);
                       }
                   }
                 opt.text = path;
                 opt.value = value;
                  
                 if(!document.getElementById("<%=hfselectedNode.ClientID%>").value.match(path +"~" +value + ","))
                 {
                     var objSelDoc=document.getElementById("<%=lbSelectedDocs.ClientID%>");
                     var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
                     var no = new Option();
                     no.value = value;
                     no.text = path;  
                     objSelDoc[objSelDoc.options.length] = no; 
                     document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value + path +"~" +value + ",";
                 }
                                           
         }       
         function CheckUncheckChildren(childContainer, check) 
         {           
            var childChkBoxes = childContainer.getElementsByTagName("input");                                    
            var childChkBoxCount = childChkBoxes.length;           
            for (var i = 0; i < childChkBoxCount; i++) 
            {           
                   var elm1 = childChkBoxes[i].nextSibling;
                   var opt = document.createElement("option");
                   var str = elm1.title.split("(");
                   var path = "";
                   var value = "";
                   for(j=0;j<str.length;j++)
                   {
                       if(j==0)
                       {
                            path = path + str[j].substring(0);
                       }
                       else
                       {
                             path = path + str[j].substring(5);
                             value = str[j].split(")")[0];
                       }
                   }
                   opt.text = path;
                   opt.value = value;                            
                   childChkBoxes[i].checked = check;  
                   if(check)
                    {
                        AddToNodeList(childChkBoxes[i]);
                    }
                    else
                    {
                        RemoveFromList(childChkBoxes[i]);
                    }        
            }           
        }           
        function CheckUncheckParents(srcChild, check) 
        {           
            var parentDiv = GetParentByTagName("div", srcChild);           
            var parentNodeTable = parentDiv.previousSibling; 
            if (parentNodeTable) 
            {          
                var checkUncheckSwitch; 
                if (check) //checkbox checked          
                {           
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);           
                    if (isAllSiblingsChecked)           
                        checkUncheckSwitch = true;           
                    else           
                        return; //do not need to check parent if any(one or more) child not checked           
                }           
                else //checkbox unchecked           
                {           
                    checkUncheckSwitch = false;           
                } 
                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");          
                if (inpElemsInParentTable.length > 0) 
                {           
                    var parentNodeChkBox = inpElemsInParentTable[0]; 
                    if(checkUncheckSwitch)
                    {
                        AddToNodeList(parentNodeChkBox);
                    }
                    else
                    {
                        RemoveFromList(parentNodeChkBox);
                    }           
                    parentNodeChkBox.checked = checkUncheckSwitch;           
                    //do the same recursively           
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);           
                }           
            }           
        }
        function AreAllSiblingsChecked(chkBox) 
        {           
            var parentDiv = GetParentByTagName("div", chkBox);           
            var childCount = parentDiv.childNodes.length;           
            for (var i = 0; i < childCount; i++) {           
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node           
                {           
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {           
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];           
                        //if any of sibling nodes are not checked, return false           
                        if (!prevChkBox.checked) {           
                            return false;           
                        }           
                    }           
                }           
            }           
            return true;           
        }         

    </script>

    <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
        <ContentTemplate>
            <table style="width: 100%">
                <tbody>
                    <tr style="text-align: left">
                        <td style="table-layout: auto; height: 16px; text-align: left" class="TDPart" align="left"
                            colspan="3">
                            <asp:Label ID="Label1" runat="server" Width="205px" Font-Bold="True" CssClass="Labels"
                                Font-Size="Small" ForeColor="Black" Text="Assign Documents to User Role"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="table-layout: auto; text-align: center" class="TDPart" colspan="3">
                            User Role :<asp:DropDownList ID="ddUserRole" runat="server" Width="21%" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px; text-align: left" class="TDPart" colspan="3">
                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 40%; height: 523px; text-align: left" class="TDPart" align="left">
                            <asp:Panel ID="contentPanel" runat="server" Height="450px" Width="322px" ScrollBars="Both">
                                <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="400px" Width="322px"
                                    ID="tvwmenu" OnClick="AddToList();" OnTreeNodePopulate="Node_Populate">
                                    <Nodes>
                                        <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                            Value="0"></asp:TreeNode>
                                    </Nodes>
                                </asp:TreeView>
                            </asp:Panel>
                            <asp:HiddenField ID="hfselectedNode" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfselectedNodeinListbox" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfOrder" runat="server"></asp:HiddenField>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                ID="txtUserID" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                        <td style="width: 20%; text-align: left" class="TDPart">
                            &nbsp;&nbsp;&nbsp;<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel112"
                                DisplayAfter="10" __designer:wfdid="w320">
                                <ProgressTemplate>
                                    <div id="DivStatus2" class="PageUpdateProgress" runat="Server">
                                        <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" __designer:wfdid="w321"
                                            AlternateText="Loading....."></asp:Image>
                                        <br />
                                        Loading....
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width: 40%; height: 523px; text-align: right" class="TDPart">
                            <asp:Panel ID="Panel3" runat="server" Height="450px" Width="322px">
                                <asp:ListBox runat="server" Height="447px" Width="322px" ID="lbSelectedDocs"></asp:ListBox>
                            </asp:Panel>
                            <asp:Button ID="btnAssign" OnClick="btnAssign_Click" runat="server" CssClass="Buttons"
                                Text="Assign"></asp:Button><br />
                            <br />
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
