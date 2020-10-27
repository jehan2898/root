<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_MissingDocumentSettings.aspx.cs" Inherits="Bill_Sys_MissingDocumentSettings" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
    <%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="usrMessage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="MasterScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<script type="text/javascript" src="validation.js"></script>--%>
    <%--<script type="text/javascript" src="ListBox.js"></script>--%>

    <script type="text/javascript">
      Sys.Application.add_load(function() 
       {     
//            var objSelDoc=document.getElementById("<%=lbSelectedDocs.ClientID%>");
//            
//            if(document.getElementById("<%=hfselectedNode.ClientID%>").value!="")
//            {
//            alert();
//            var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
//            for(i=0;i<list.length - 1;i++)
//            {
//                 var no = new Option();
//                 no.value = list[i].split('~')[1];
//                 no.text = list[i].split('~')[0];  
//                 objSelDoc[i] = no; 
//            }
//            }
               
       });
      
        function OnCheckBoxCheckChanged(evt) 
        {           
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;           
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");  
//             
//            value = ddl.options[ddl.selectedIndex].value;
//            if(isChkBoxClick && value == "0")
//           {
//                src.checked = false;
//                alert('Select User Role.');            
//           }
//           else
//           {   
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
               // CheckUncheckParents(src, src.checked);  
                         
            }      
//            }     
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
        
        
        function  AddMailId()
      {
      
        if (document.getElementById("<%= RegularExpressionValidator1.ClientID%>").style.visibility == 'hidden')
                {                
                
        if(document.getElementById("<%=txtEmailID.ClientID %>").value == "")
             {
        			alert('Please enter EmailId in Text Box');			
        			document.getElementById("<%=txtEmailID.ClientID %>").focus();			
        			return false;		
              }		
        else if(document.getElementById("<%=txtEmailID.ClientID %>").value != "")		
         {			
        var vStatus = "0";			
        var vLen = document.getElementById("<%=lbSelectedEmailIds.ClientID %>").length;			
        var i;			
        
        if(vLen != 0)			
          {			   
        for(i=0;i< vLen;i++)			   
            {			
          	if(document.getElementById("<%=txtEmailID.ClientID %>").value.toLowerCase() == document.getElementById("<%=lbSelectedEmailIds.ClientID %>").options[i].value.toLowerCase())				
              {				    
        alert("EmailId already exists in ListBox.");				    
        vStatus = "1";										    
        return false;				
              }								   
            }					
         }
        if(vStatus != "1")			
            {			   
        var optn = document.createElement("OPTION");                           
        optn.text = document.getElementById("<%=txtEmailID.ClientID %>").value;                           
        optn.value = document.getElementById("<%=txtEmailID.ClientID %>").value;                           
        document.getElementById("<%=lbSelectedEmailIds.ClientID %>").options.add(optn); 
        document.getElementById("<%=hfEmailId.ClientID %>").value=document.getElementById("<%=hfEmailId.ClientID %>").value + document.getElementById("<%=txtEmailID.ClientID %>").value + ",";                      
        document.getElementById("<%=txtEmailID.ClientID %>").value = "";                                         
        return false;			
           }			
         } 
        }       
    }
        
        
        function RemoveMailId()
        {    
        if(document.getElementById("<%=lbSelectedEmailIds.ClientID %>").length <=0)  
          {      
           alert("No Email Id available to remove.")  
           document.getElementById("<%=lbSelectedEmailIds.ClientID %>").focus(); 
           return false; 
            } 
          else if(document.getElementById("<%=lbSelectedEmailIds.ClientID %>").selectedIndex < 0) 
            {  
              alert("Please Select Email Id to Remove.");  
              document.getElementById("<%=lbSelectedEmailIds.ClientID %>").focus();   
              return false;   
            } 
          else 
            {   
            document.getElementById("<%=lbSelectedEmailIds.ClientID %>").options[document.getElementById("<%=lbSelectedEmailIds.ClientID %>").selectedIndex] = null;                                        
            document.getElementById("<%=hfEmailId.ClientID %>").value="";           
            var list=document.getElementById("<%=lbSelectedEmailIds.ClientID %>");
            var items=list.getElementsByTagName("option");
            
              var i=0;
            for(i=0;i<items.length;i++)
            {
            document.getElementById("<%=hfEmailId.ClientID %>").value=document.getElementById("<%=hfEmailId.ClientID %>").value + document.getElementById("<%=lbSelectedEmailIds.ClientID %>").options[i].value + ",";             
            
            }
            
             return false;   
            }
         } 
    function  Validation()
    {
      alert("Please Enter No Of Days");
    }
    function  Validation1()
    {
      alert("Please Enter EmailId In ListBox");
    }
    
    
    function CheckForInteger(e,charis)
        {
                  if (e.charCode < 48 || e.charCode > 57)
                         {      
                         
                                return false;
                         } 

            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          

                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
            }   
         }
    </script>

    <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
        <ContentTemplate>
<asp:Wizard id="Wizard1" runat="server" Width="100%" Font-Size="Small" DisplaySideBar="False" FinishCompleteButtonText="Save" ActiveStepIndex="0" OnFinishButtonClick="Wizard1_FinishButtonClick" OnNextButtonClick="Wizard1_NextButtonClick">
<StartNextButtonStyle CssClass="Buttons"></StartNextButtonStyle>

<FinishCompleteButtonStyle CssClass="Buttons"></FinishCompleteButtonStyle>

<StepNextButtonStyle CssClass="buttons"></StepNextButtonStyle>

<FinishPreviousButtonStyle CssClass="Buttons"></FinishPreviousButtonStyle>
<WizardSteps>
<asp:WizardStep runat="server" StepType="Start" Title="1:">
<TABLE style="WIDTH: 100%"><TBODY><TR style="TEXT-ALIGN: left"><TD style="TABLE-LAYOUT: auto; HEIGHT: 16px; TEXT-ALIGN: left" class="TDPart" align=left colSpan=4><asp:Label runat="server" Text="Assign missing documents" CssClass="Labels" Font-Bold="True" Font-Size="Small" ForeColor="Black" Width="205px" ID="Label1"></asp:Label>
 </TD></TR><TR><TD style="HEIGHT: 16px; TEXT-ALIGN: left" class="TDPart" colSpan=4><UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
 </TD></TR><TR align=left><TD style="WIDTH: 30%; HEIGHT: 523px; TEXT-ALIGN: left" class="TDPart" align=left><asp:Panel runat="server" ScrollBars="Both" Height="450px" Width="80%" ID="contentPanel">
                                <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="400px" Width="322px" ID="tvwmenu" OnClick="AddToList();" OnTreeNodePopulate="Node_Populate"><Nodes>
<asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager" Value="0"></asp:TreeNode>
</Nodes>
</asp:TreeView>


                            </asp:Panel>
                        
 <asp:HiddenField runat="server" ID="hfselectedNode"></asp:HiddenField>
 <asp:HiddenField runat="server" ID="hfselectedNodeinListbox"></asp:HiddenField>
 <asp:HiddenField runat="server" ID="hfOrder"></asp:HiddenField>
 &nbsp;&nbsp; <asp:TextBox runat="server" Width="10px" ID="txtCompanyID" Visible="False"></asp:TextBox>
 <asp:TextBox runat="server" Width="10px" ID="txtUserID" Visible="False"></asp:TextBox>
 </TD><TD style="WIDTH: 10%; TEXT-ALIGN: left" class="TDPart"><asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel112" DisplayAfter="10" ID="UpdateProgress2"><ProgressTemplate>
                                    <div id="DivStatus2" class="PageUpdateProgress" runat="Server">
                                        <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif"
                                            AlternateText="Loading....."></asp:Image>
                                        <br />
                                        Loading....
                                    </div>
                                
</ProgressTemplate>
</asp:UpdateProgress>
 </TD><TD style="WIDTH: 30%; HEIGHT: 523px; TEXT-ALIGN: right" class="TDPart"><asp:Panel runat="server" Height="450px" Width="80%" ID="Panel3">
                                <asp:ListBox runat="server" Height="447px" Width="322px" ID="lbSelectedDocs"></asp:ListBox>


                            </asp:Panel>
 <asp:Button runat="server" Text="Assign" CssClass="Buttons" ID="btnAssign" onclick="btnAssign_Click"></asp:Button>
 <BR /><BR /></TD></TR></TBODY></TABLE>
</asp:WizardStep>
<asp:WizardStep runat="server" StepType="Finish" Title="2:">
<TABLE style="WIDTH: 100%"><TBODY><TR style="TEXT-ALIGN: left"> <TD style="TABLE-LAYOUT: auto; HEIGHT: 16px; TEXT-ALIGN: left" class="TDPart" align=left colSpan=3><asp:Label runat="server" Text="Choose reminder / email settings" CssClass="Labels" Font-Bold="True" Font-Size="Small" ForeColor="Black" Width="222px" ID="lblHeader"></asp:Label>
 </TD></TR><TR> <TD style="HEIGHT: 16px; TEXT-ALIGN: left" class="TDPart" colSpan=3><UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
 </TD></TR><TR align=left> <TD style="WIDTH: 50%; HEIGHT: 523px; TEXT-ALIGN: left" class="TDPart" align=left><asp:Panel runat="server" Height="450px" Width="80%" ID="contentPanel1"><BR /><BR />&nbsp;&nbsp; Send an email notification every <asp:TextBox runat="server" Width="10%" ID="txtNoOfDays" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
 day(s) <BR /><BR /><BR /><BR />Email Id:-<BR /><asp:TextBox runat="server" Width="40%" ID="txtEmailID"></asp:TextBox>
 <input type="button" class="Buttons" value="+" Height="5px" Width="5px" ID="btnAddMailID"  onclick="AddMailId();"/>
  <input type="button" class="Buttons" value="~" Height="5px" Width="5px" ID="btnRemoveMailId"  onclick="RemoveMailId();"/> <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address..." 
                            Font-Size="Medium" ControlToValidate="txtEmailID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                
                          
<p></p><asp:ListBox runat="server" Height="60%" Width="40%" ID="lbSelectedEmailIds"></asp:ListBox>
</asp:Panel>
    <asp:HiddenField runat="server" ID="hfEmailId"></asp:HiddenField>
    
</TD></TR></TBODY></TABLE>
</asp:WizardStep>
</WizardSteps>
</asp:Wizard> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
