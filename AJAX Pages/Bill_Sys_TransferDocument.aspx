<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_TransferDocument.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_TransferDocument"
    EnableEventValidation="false" %>

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

           });
           
      
        function OnCheckBoxCheckChanged(evt) 
        {          
            
           var src = window.event != window.undefined ? window.event.srcElement : evt.target;           
           var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");  
           
           var ddl = document.getElementById("<%=ddlSpeciality.ClientID%>");
           value = ddl.options[ddl.selectedIndex].value;
           if(isChkBoxClick && (value == "0"))
           {
                src.checked = false;
                alert('Select Specialty.');            
           }
           else
           {     
                   
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
               // CheckUncheckParents(src, src.checked);  
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
         function order()
         {
            document.getElementById("<%=hfselectedNode.ClientID%>").value = "";
            var listbox=document.getElementById("<%=lbSelectedDocs.ClientID%>");
            var list = document.getElementById("<%=hfselectedNode.ClientID%>");
            var count = 0;
            for(i = 0; i < listbox.options.length; i++)
            {
               list.value = list.value + listbox.options[i].text+'~'+listbox.options[i].value +',';
            }
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
                  
                   var ddl = document.getElementById("<%=ddlSpeciality.ClientID%>");
                   
                   var Text = ddl.options[ddl.selectedIndex].text; 
                   
                   opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;
                
                    var listbox=document.getElementById("<%=lbSelectedDocs.ClientID%>");
                   
                    var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
                     
                    var arrText = new Array();
                    var arrValue = new Array();
                    var count = 0;
                     //alert(document.getElementById("<%=hfselectedNode.ClientID%>").value);
                     
                        
                     var IsAll = document.getElementById("<%=hfIsAll.ClientID%>").value;
                       
                    for(i = 0; i < listbox.options.length; i++)
                    {   
                      
                        if(IsAll=="0")
                        {
                         
                            if (listbox.options[i].value != opt.value)
                            {
                                arrText[count] = listbox.options[i].text;
                                arrValue[count] = listbox.options[i].value;
                                count++;
                                
                            }
                            else
                            {
                            //alert("replace : " + path+'~'+opt.value +',');
                            
                            
                                document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value.replace(path+'~'+opt.value +',','');
                            }  
                        } else
                        {   
                             var nodid1 =listbox.options[i].value;
                             var nodid2 =opt.value;
                             
                              var nod1= nodid1.split('~')[1];
                              var nod2=nodid2.split('~')[1];
                              
                          
                           
                               if (nod1!= nod2)
                                {
                                    arrText[count] = listbox.options[i].text;
                                    arrValue[count] = listbox.options[i].value;
                                    count++;
                                    
                                }
                                else
                                {
                                //alert("replace : " + path+'~'+opt.value +',');
                          
                                    
                                
                                    document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value.replace(path+'~'+opt.value +',','');
                                }  
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
                    //alert(document.getElementById("<%=hfselectedNode.ClientID%>").value);
                    //alert(document.getElementById("<%=hfselectedNode.ClientID%>").value);
                    
         } 
           
         function AddToNodeList(node)
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
               var ddl = document.getElementById("<%=ddlSpeciality.ClientID%>");
               var Text = ddl.options[ddl.selectedIndex].text; 
               opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;
              
               if(!document.getElementById("<%=hfselectedNode.ClientID%>").value.match(path +"~" +opt.value + ","))
               {
                 
                 var objSelDoc=document.getElementById("<%=lbSelectedDocs.ClientID%>");
                 var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
                 var no = new Option();
                 no.value = opt.value;
                 no.text = path;
                 objSelDoc[objSelDoc.options.length] = no; 
                 document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value + path +"~" +opt.value + ",";
               }       
              // alert(document.getElementById("<%=hfselectedNode.ClientID%>").value); 
         } 
               
         function CheckUncheckChildren(childContainer, check) {
             alert('CheckUncheckChildren');        
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            alert(childChkBoxCount);     
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

        function btnUp_onclick() 
        {
            var sel = document.getElementById("<%=lbSelectedDocs.ClientID%>")
            var listLength = sel.options.length;
            for (var i = 0; i < listLength; i++) 
            {
                if (((i+1)< listLength) && sel.options[i+1].selected) 
                {
                   var text = sel.options[i].text;
                   var value = sel.options[i].value;
                   document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value.replace(text +"~"+value+"," +sel.options[i+1].text+"~"+sel.options[i+1].value+",",sel.options[i+1].text+"~"+sel.options[i+1].value +"," +text +"~"+value+",");
                   sel.options[i].text = sel.options[i+1].text;
                   sel.options[i].value = sel.options[i+1].value;
                   sel.options[i+1].text = text;
                   sel.options[i+1].value = value;
                   sel.options[i+1].selected = false;
                   sel.options[i].selected = true;
                }
            }
        }

        function btnDown_onclick() 
        {
            var sel = document.getElementById("<%=lbSelectedDocs.ClientID%>")
            var listLength = sel.options.length;
            for (var i = listLength-1; i >= 0; i--) 
            {
                if (((i-1)>= 0) && sel.options[i-1].selected) 
                {
                   var text = sel.options[i-1].text;
                   var value = sel.options[i-1].value;
                   document.getElementById("<%=hfselectedNode.ClientID%>").value = document.getElementById("<%=hfselectedNode.ClientID%>").value.replace(text +"~"+value+"," +sel.options[i].text+"~"+sel.options[i].value+",",sel.options[i].text+"~"+sel.options[i].value +"," +text +"~"+value+",");
                   sel.options[i-1].text = sel.options[i].text;
                   sel.options[i-1].value = sel.options[i].value;
                   sel.options[i].text = text;
                   sel.options[i].value = value;
                   sel.options[i-1].selected = false;
                   sel.options[i].selected = true;
                }
            }
        }
        function MoveRight()
        {
            var sel = document.getElementById("<%=lbAllSelectedDocuments.ClientID%>");
            var objSelDoc = document.getElementById("<%=lbselect.ClientID%>");
            var selNodeInlist = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>");
            var listLength = sel.options.length;
            for (var i = 0; i < listLength; i++) 
            {
                if (sel.options[i].selected) 
                {
                   var no = new Option();
                   no.value = sel.options[i].value;
                   no.text = sel.options[i].text;
                   flag = 0;
                   for(j=0;j<objSelDoc.options.length;j++)
                   {
                        if(no.text == objSelDoc.options[j].text)
                        {
                            flag = 1;
                        }
                   }  
                   if(flag == 0)
                   {
                       objSelDoc[objSelDoc.options.length] = no; 
                       //alert(selNodeInlist.value);
                       selNodeInlist.value = selNodeInlist.value + no.value +",";
                   }
                   sel.remove(i);
                }
            }
        }
        function MoveLeft()
        {
            var sel = document.getElementById("<%=lbselect.ClientID%>");
            var objSel = document.getElementById("<%=lbAllSelectedDocuments.ClientID%>");
            var listLength = sel.options.length;
            var arrText = new Array();
            var arrValue = new Array();
            var count = 0;
            for (var i = 0; i < listLength; i++) 
            {
               // alert(sel.options[i].selected);
                if(!sel.options[i].selected)
                {
                  var no = new Option();
                  arrText[count] = sel.options[i].text;
                  arrValue[count] = sel.options[i].value;
                  count++;
                }
                else
                {
                    document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value.replace(sel.options[i].value +",", "");
                    var no = new Option();
                    no.text = sel.options[i].text;
                    no.value = sel.options[i].value;
                    objSel[objSel.options.length] = no;
                }
            }
            sel.length = 0;
            for(index = 0; index < arrText.length; index++)
            {
                var no = new Option();
                no.value = arrValue[index];
                no.text = arrText[index];
                sel[index] = no;
                
            }
        }
         
    </script>

    <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
        <ContentTemplate>
            <asp:Wizard ID="Wizard1" runat="server" Width="100%" DisplaySideBar="False" FinishCompleteButtonText="Save"
                Font-Size="Small" ActiveStepIndex="0" OnFinishButtonClick="Wizard1_FinishButtonClick"
                OnNextButtonClick="Wizard1_NextButtonClick">
                <StartNextButtonStyle CssClass="Buttons"></StartNextButtonStyle>
                <FinishCompleteButtonStyle CssClass="Buttons"></FinishCompleteButtonStyle>
                <StepNextButtonStyle CssClass="buttons"></StepNextButtonStyle>
                <FinishPreviousButtonStyle CssClass="Buttons"></FinishPreviousButtonStyle>
                <WizardSteps>
                    <asp:WizardStep runat="server" StepType="Start" Title="1:">
                        <table style="width: 100%">
                            <tbody>
                                <tr style="text-align: left">
                                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                                        width: 100%; padding-top: 3px; height: 100%" class="TDPart" colspan="5">
                                        <asp:Label runat="server" Text="Missing Documents" CssClass="Labels" Font-Bold="True"
                                            Font-Size="Small" ForeColor="Black" Width="205px" ID="Label1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" class="TDPart" colspan="5">
                                        Specialty
                                        <asp:DropDownList runat="server" AutoPostBack="True" Width="143px" ID="ddlSpeciality"
                                            OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:Button runat="server" Text="AssignTOAll" CssClass="Buttons" ID="btnAssignAll"
                                            Visible="False" OnClick="btnAssignAll_Click"></asp:Button>
                                            
                                            &nbsp;
                                        <asp:Button runat="server" Text="RemoveAll" CssClass="Buttons" ID="btnRemove"
                                            Visible="False" OnClick="btnRemove_Click"  ></asp:Button>
                                            <asp:HiddenField runat="server" ID="hfIsAll"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" class="TDPart" colspan="5">
                                        <div style="width: 400px; font-family: Times New Roman; top: 150px; text-align: center">
                                            <span runat="server" id="msg"></span>
                                            <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel112" DisplayAfter="10"
                                                ID="UpdateProgress2">
                                                <ProgressTemplate>
                                                    <div id="DivStatus2" class="PageUpdateProgress" runat="Server">
                                                        <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading.....">
                                                        </asp:Image>
                                                        Loading....
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td style="width: 30%; text-align: left" class="TDPart" align="left">
                                        <asp:Panel runat="server" ScrollBars="Both" Height="450px" Width="100%" ID="contentPanel">
                                            <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                                                ID="tvwmenu" OnTreeNodePopulate="Node_Populate">
                                                <Nodes>
                                                    <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                                        Value="0"></asp:TreeNode>
                                                </Nodes>
                                            </asp:TreeView>
                                        </asp:Panel>
                                        <asp:HiddenField runat="server" ID="hfselectedNode"></asp:HiddenField>
                                        <asp:HiddenField runat="server" ID="hfOrder"></asp:HiddenField>
                                        &nbsp;&nbsp;
                                        <asp:TextBox runat="server" Width="10px" ID="txtCompanyID" Visible="False"></asp:TextBox>
                                        <asp:TextBox runat="server" Width="10px" ID="txtUserID" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 5%; text-align: left" class="TDPart">
                                        &nbsp;
                                    </td>
                                    <td style="width: 30%" class="TDPart" align="center">
                                        <asp:Panel runat="server" ScrollBars="Horizontal" Height="450px" Width="250px" ID="Panel3"
                                            allign="center">
                                            <asp:ListBox runat="server" Height="430px" ID="lbSelectedDocs"></asp:ListBox>
                                        </asp:Panel>
                                        &nbsp;
                                        <asp:Button runat="server" Text="Assign" CssClass="Buttons" ID="btnAssign" OnClick="btnAssign_Click">
                                        </asp:Button>
                                    </td>
                                    <td style="width: 5%" class="TDPart" align="center">
                                        <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                                            padding-left: 5px; font-size: 12px; background-image: url(Images/up_arrow.gif);
                                            padding-bottom: 1px; border-left: #000099 1px solid; width: 22px; color: #000099;
                                            padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial; background-color: transparent"
                                            id="btnUp" class="Buttons" onclick="return btnUp_onclick()" type="button" /><br />
                                        <br />
                                        <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                                            padding-left: 5px; font-size: 12px; background-image: url(Images/down_arrow.GIF);
                                            padding-bottom: 1px; border-left: #000099 1px solid; width: 23px; color: #000099;
                                            padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial; background-color: transparent"
                                            id="btnDown" class="Buttons" onclick="return btnDown_onclick()" type="button"
                                            size="20" />
                                    </td>
                                    <td style="width: 30%" class="TDPart" align="center">
                                        <asp:Panel runat="server" ScrollBars="Horizontal" Height="450px" Width="250px" ID="Panel4"
                                            allign="center">
                                            <asp:ListBox runat="server" Height="430px" ID="lbAllAssignedDoc"></asp:ListBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" StepType="Finish" Title="2:">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td class="TDPart">
                                        <asp:Label runat="server" Text="Select folder to transfer all the documents in the folder."
                                            CssClass="Labels" Font-Bold="True" Font-Size="Small" ForeColor="Black" ID="Label2"></asp:Label>
                                    </td>
                                    <td class="TDPart">
                                        &nbsp;</td>
                                    <td class="TDPart">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="TDPart">
                                        <UserMessage:MessageControl runat="server" ID="MessageControl1"></UserMessage:MessageControl>
                                        &nbsp;&nbsp;<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel112"
                                            DisplayAfter="10" ID="UpdateProgress3">
                                            <ProgressTemplate>
                                                <div id="DivStatus3" class="PageUpdateProgress" runat="Server">
                                                    <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                                    <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Please wait....">
                                                    </asp:Image>
                                                    Please wait....
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%" class="TDPart" align="center">
                                        <asp:Panel runat="server" Height="450px" Width="100%" ID="Panel1">
                                            <asp:ListBox runat="server" Height="447px" Width="100%" ID="lbAllSelectedDocuments">
                                            </asp:ListBox>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 10%" class="TDPart" align="center">
                                        <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                                            padding-left: 5px; font-size: 12px; padding-bottom: 1px; border-left: #000099 1px solid;
                                            color: #000099; padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial;
                                            background-color: #8babe4" id="btnMoveRight" class="Buttons" title="Move Right"
                                            onclick="MoveRight();" type="button" value=">>" />
                                        <br />
                                        <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                                            padding-left: 5px; font-size: 12px; padding-bottom: 1px; border-left: #000099 1px solid;
                                            color: #000099; padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial;
                                            background-color: #8babe4" id="btnMoveLeft" class="Buttons" title="Move Left"
                                            onclick="MoveLeft();" type="button" value="<<" />
                                    </td>
                                    <td style="width: 45%" class="TDPart" align="center">
                                        <asp:Panel runat="server" Height="450px" Width="100%" ID="Panel2">
                                            <asp:ListBox runat="server" Height="447px" Width="100%" ID="lbselect"></asp:ListBox>
                                        </asp:Panel>
                                        <asp:HiddenField runat="server" ID="hfselectedNodeinListbox"></asp:HiddenField>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" align="center" colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
