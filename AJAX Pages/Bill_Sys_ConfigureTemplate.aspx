<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ConfigureTemplate.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ConfigureTemplate"
    Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
    function test()
    {
       // alert('');
        document.getElementById("<%= btntest.ClientID %>").click();
       // alert('');
    }
      function RemoveFromList(node)
         {
            document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value = "";
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
        function OnCheckBoxCheckChanged(evt) 
        {          
           var src = window.event != window.undefined ? window.event.srcElement : evt.target;        
           var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick)
            {

                     var parentTable = GetParentByTagName("table", src);
                    var nxtSibling = parentTable.nextSibling;

                    if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt       sibling is not null & is an element node 
                    {         
                        if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                        {           
                            //check or uncheck children at all levels 
                            if(src.checked)
                            {
                                if(document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value == "")
                                {
                                    AddToNodeList(src);
                                }
                                else
                                {
                                     alert("Select only one node.")
                                    src.checked = false;
                                }
                            }
                            else
                            {
                                RemoveFromList(src);
                            }
                            //CheckUncheckChildren(parentTable.nextSibling, src.checked);           
                        } 
                        else
                         {
                            if(src.checked)
                            {
                               if(document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value == "")
                                {
                                    AddToNodeList(src);
                                }
                                else
                                {
                                    alert("Select only one node.")
                                    src.checked = false;
                                }
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
                             if(document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value == "")
                                {
                                    AddToNodeList(src);
                                }
                                else
                                {
                                     alert("Select only one node.")
                                    src.checked = false;
                                }
                        }
                        else
                        {
                            RemoveFromList(src);
                        }
                    }          

            }      
        }  
         function AddToNodeList(node)
         {
               var elm1 = node.nextSibling;
               var opt = document.createElement("option");
               //alert(elm1.title)
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
               
//               opt.text = path;
//               //alert("Path : " + path)
//               var ddl = document.getElementById("<%=lstTemplates.ClientID%>");
//               //alert(ddl)
//               opt.value = value;
              
               document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value = value;
               //alert(document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value )
               //ddl[ddl.options.length] = opt; 
               
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
                   var flag = 0;  
                   childChkBoxes[i].checked = check;  
                   if(check)
                    {
                        if(flag == 0)
                        {
                            AddToNodeList(childChkBoxes[i]);                        
                        }
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

     function OpenFile()
     {
            var listbox=document.getElementById("<%=lstTemplates.ClientID%>");
            var count = 0;
            for (var i = 0; i < listbox.options.length;i++)
            {
                if(listbox.options[i].selected)
                {
                     document.getElementById("ctl00_ContentPlaceHolder1_nfSelectedNodeId").value = listbox.options[i].value;             
                    count = count + 1;
                }
            }
            if(count == 0)
            {
                alert('select one template from list.');
                return false;
            }
            else
            {
                return true;
            }
     }
     function CheckTemplateName()
     {
       
            var textbox=document.getElementById("<%=txtTemplateName.ClientID%>");
            
           var listbox=document.getElementById("<%=lstTemplates.ClientID%>");
           var count = 0;
           var flag = 0;
           var sel = document.getElementById("<%=lstTemplates.ClientID%>")
           var listLength = sel.options.length;
           for (var i = 0; i < listbox.options.length;i++) 
           {
                if(listbox.options[i].selected)
                {
                    
                  //  document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value = listbox.options[i].value;
                }
                else
                {
                   
                }
           }
           if(flag == 1)
           {
                alert('Template name already exist.');
                return false;
           }
           else
           {
                var fileupload=document.getElementById("<%=FileUploadControl.ClientID%>");
                var flag="";
                if(fileupload.value == "")
                {
                   flag = "select file to upload."
                }
                if (textbox.value == "")
                {
                    flag = flag +' Enter Template Name.';
                }
                if(flag == "")
                {
                    if(document.getElementById("ctl00_ContentPlaceHolder1_hfNodeType").value == '')
                    {
                        alert('select node from document manager.')
                        return false;
                    }
                    else
                    {
                         return true;
                    }
                   
                }
                else
                {
                    alert(flag);
                    return false;
                }
           }
     }
    </script>

    <table class="ContentTable" style="height: 100%; width: 100%;">
        <tr>
            <td style="height: 5%; width: 100%;" class="TDPart" align="left">
                <asp:Label runat="server" ID="lblPageName" Text="Template Manager" CssClass="ContentLabel"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblError" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 5%; width: 100%;" class="TDPart" align="center">
                <asp:UpdatePanel ID="UpdatePanelUserMessage" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 90%; width: 100%;" class="TDPart" align="left">
                <table>
                    <asp:UpdatePanel ID="updatePanel" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td style="height: 100%; width: 50%;" align="center">
                                    <table>
                                        <tr>
                                            <td style="height: 100%; width: 100%;" align="left">
                                                Template Name &nbsp;<asp:TextBox runat="server" ID="txtTemplateName" Width="181px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr valign="top" align="left">
                                            <td style="height: 100%; width: 100%;" align="left">
                                                Upload Template
                                                <asp:FileUpload ID="FileUploadControl" runat="server" Height="27px" Width="267px"></asp:FileUpload>
                                                <asp:Button ID="btnupload" runat="server" Height="25px" OnClick="btnupload_Click"
                                                    Text="Upload" Width="60px" CausesValidation="False" CssClass="Buttons" />&nbsp;<br />
                                                <asp:Label runat="server" ID="lblFileName" ForeColor="red" Text=""></asp:Label>
                                                <asp:Label ID="lblFilseName" runat="server" CssClass="label" ForeColor="Blue" Height="12px"
                                                    Width="164px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%; width: 100%;" align="center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 100%; width: 100%;" valign="top">
                                <td style="height: 100%; width: 40%;" align="center">
                                    Templates
                                    <br />
                                    <asp:ListBox ID="lstTemplates" runat="server" Height="300px" Width="250px" ></asp:ListBox>
                                    
                                    <br />
                                    <asp:Button runat="server" Text="Delete Template" OnClientClick="return OpenFile();"
                                        ID="btnDeleteTemplate" OnClick="btnDeleteTemplate_click" />
                                </td>
                                <td>
                                    <asp:Panel runat="server" ScrollBars="Both" Height="450px" Width="100%" ID="contentPanel">
                                        <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                                            ID="tvwmenu" OnTreeNodePopulate="Node_Populate">
                                            <Nodes>
                                                <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                                    Value="0"></asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                                    </asp:Panel>
                                </td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="hfNodeType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </table>
            </td>
        </tr>
        <tr style="height: 100%; width: 100%;">
            <td style="height: 21%; width: 30%;" align="center">
                <%--<asp:Button runat="server" Text="Open Template" OnClientClick="return OpenFile();"
                    ID="btnOpenTemplate" />--%>
            </td>
            <td style="height: 21%; width: 70%;" align="center">
                &nbsp;
            </td>
            <td>
                <input type="hidden" id="hfNodeType" runat="server" />
                <input type="hidden" id="nfSelectedNodeId" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btntest" runat="server" OnClick="btntest_click" Style="visibility:hidden;" />
    <asp:TextBox ID="txtnodevalue" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
