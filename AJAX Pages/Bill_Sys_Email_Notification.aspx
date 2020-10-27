<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Email_Notification.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Email_Notification"
    Title="Green Bills EmailNotification" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
      Sys.Application.add_load(function() 
           {     

           });
           
 //prashant
 
 function Checkempty()
 {
    //alert('');
    var lawfirm = document.getElementById("<%= extddlLawFirm.ClientID %>");
    if (lawfirm.value != 'NA'){
    
        var email = document.getElementById("<%= txtEmailAddress.ClientID %>");
        if(email.value != ''){
        return true;
        }else{
        alert('you can not entered emaiid');
        return false;
        }
    
    }
    else{
        alert('You can not select law firm');
        return false;
     }
 
 }
    function OnCheckBoxCheckChangedlaw(evt) 
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
                            AddToNodeListlaw(src);
                        }
                        else
                        {
                            RemoveFromListlaw(src);
                        }
                        CheckUncheckChildrenlaw(parentTable.nextSibling, src.checked);           
                    } 
                    else
                     {
                        if(src.checked)
                        {
                            AddToNodeListlaw(src);
                        }
                        else
                        {
                            RemoveFromListlaw(src);
                        }
                     }          
                }
                else
                {
                    if(src.checked)
                    {
                        AddToNodeListlaw(src);
                    }
                    else
                    {
                        RemoveFromListlaw(src);
                    }
                }           
               // CheckUncheckParents(src, src.checked);  
            }      
               
        }  
        
        
           function CheckUncheckChildrenlaw(childContainer, check) 
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
                            AddToNodeListlaw(childChkBoxes[i]);                        
                        }
                    }
                    else
                    {
                        RemoveFromListlaw(childChkBoxes[i]);
                    }        
            }           
        }
        
        
        
            function AddToNodeListlaw(node)
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
                 opt.value =  value;
                 // var ddl1 = document.getElementById("<%=ExtendedGroup.ClientID%>");
                //var txt1 = document.getElementById("<%=txtNewGroup.ClientID%>");
                //if(ddl1!=null)
                //{
                  //   var ddl = document.getElementById("<%=ExtendedGroup.ClientID%>");
              //             
                // var Text = ddl.options[ddl.selectedIndex].text; 
                //opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;
                //}else
                //{
                  //   var Text =txt1.value ; 
                  
                //}
              
               if(!document.getElementById("<%=hfselectedNodelaw.ClientID%>").value.match(path +"~" +opt.value + ","))
               {
                 var objSelDoc=document.getElementById("<%=lbSelectedDocslaw.ClientID%>");
                 var list = document.getElementById("<%=hfselectedNodelaw.ClientID%>").value.split(',');
                 var no = new Option();
                 no.value = opt.value;
                 no.text = path;
                 objSelDoc[objSelDoc.options.length] = no; 
                 document.getElementById("<%=hfselectedNodelaw.ClientID%>").value = document.getElementById("<%=hfselectedNodelaw.ClientID%>").value + path +"~" +opt.value + ",";
                 //alert(document.getElementById("<%=hfselectedNodelaw.ClientID%>").value);
               }       
              // alert(document.getElementById("<%=hfselectedNode.ClientID%>").value); 
         }
         
         
         
           function RemoveFromListlaw(node)
         {
                 
                   var elm1 = node.nextSibling;
                   var opt = document.createElement("option");
                   var str = elm1.title.split("(");
                   var path = "";
                   var value = "";
                   //alert("str : " +str);
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
                
                   opt.value =  value;
                
                
                   
                    var listbox=document.getElementById("<%=lbSelectedDocslaw.ClientID%>");
                    
                    var list = document.getElementById("<%=hfselectedNodelaw.ClientID%>").value.split(',');
                    var arrText = new Array();
                    var arrValue = new Array();
                    var count = 0;
                     //alert(document.getElementById("<%=hfselectedNode.ClientID%>").value);
                    // alert(document.getElementById("<%=hfselectedNodelaw.ClientID%>").value);
                    for(i = 0; i < listbox.options.length; i++)
                    {
                        //alert(listbox.options[i].value);
                        if (listbox.options[i].value != opt.value)
                        {
                     //   alert(listbox.options[i].value);
                            arrText[count] = listbox.options[i].text;
                            arrValue[count] = listbox.options[i].value;
                            count++;
                            
                        }
                        else
                        {
                        //alert("replace : " + path+'~'+opt.value +',');
                            document.getElementById("<%=hfselectedNodelaw.ClientID%>").value = document.getElementById("<%=hfselectedNodelaw.ClientID%>").value.replace(path+'~'+opt.value +',','');
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
 
 
 //-----------------------------------------------------------------
 
 
 
 
 
        
        
        
        
        
        
        
        
        
        function OnCheckBoxCheckChanged(evt) 
        {      
              
           var src = window.event != window.undefined ? window.event.srcElement : evt.target;           
           var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");   
           var ddl =document.getElementById("ctl00_ContentPlaceHolder1_ExtendedGroup") //document.getElementById("<%=ExtendedGroup.ClientID%>");
          
             var txt = document.getElementById("<%=txtNewGroup.ClientID%>");
            var iFlag=1;
           if(ddl!=null)
           {
               var value = ddl.options[ddl.selectedIndex].value;
               if(isChkBoxClick && (value == "---Select---"))
               {
                    src.checked = false;
                    alert('Select Group Name.');
                    iFlag=0 ;           
               }else
               {
                iFlag=1;
               }
            }
            else if(txt!=null)
           {
               var value = txt.value;
               if(isChkBoxClick && (value == ""))
               {
                    src.checked = false;
                    iFlag=0;
                    alert('Add Group Name.');            
               }else
               {
                iFlag=1;
               }
            }
           if(iFlag==1)
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
                   //alert("str : " +str);
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
//                   var ddl = document.getElementById("<%=ExtendedGroup.ClientID%>");
//                   var Text = ddl.options[ddl.selectedIndex].text; 
//                   opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;

                var ddl1 = document.getElementById("<%=ExtendedGroup.ClientID%>");
                var txt1 = document.getElementById("<%=txtNewGroup.ClientID%>");
                if(ddl1!=null)
                {
                      var ddl = document.getElementById("<%=ExtendedGroup.ClientID%>");
                   var Text = ddl.options[ddl.selectedIndex].text; 
                   opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;
                }else
                {
                
                    var ddl = document.getElementById("<%=txtNewGroup.ClientID%>");
                   var Text = ddl.value; 
                   opt.value = ddl.value + "~" + value;
                }
                
                   
                    var listbox=document.getElementById("<%=lbSelectedDocs.ClientID%>");
                    var list = document.getElementById("<%=hfselectedNode.ClientID%>").value.split(',');
                    var arrText = new Array();
                    var arrValue = new Array();
                    var count = 0;
                     //alert(document.getElementById("<%=hfselectedNode.ClientID%>").value);
                    for(i = 0; i < listbox.options.length; i++)
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
                  var ddl1 = document.getElementById("<%=ExtendedGroup.ClientID%>");
                var txt1 = document.getElementById("<%=txtNewGroup.ClientID%>");
                if(ddl1!=null)
                {
                     var ddl = document.getElementById("<%=ExtendedGroup.ClientID%>");
               
               
                 var Text = ddl.options[ddl.selectedIndex].text; 
                opt.value = ddl.options[ddl.selectedIndex].value + "~" + value;
                }else
                {
                     var Text =txt1.value ; 
                    opt.value = txt1.value + "~" + value;
                }
              
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
     
         
    </script>

    <table width="100%" style="background-color:White">
        <tbody>
                    <tr>
                <td colspan="2" align="center">
                  <asp:UpdateProgress ID="UpdateProgresslaw" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                         DisplayAfter="10">
                         <ProgressTemplate>
                             <div id="Divlaw" class="PageUpdateProgress" runat="Server">
                                 <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                 <asp:Image ID="imglaw" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading.....">
                                 </asp:Image>
                                 Loading....
                             </div>
                         </ProgressTemplate>
                     </asp:UpdateProgress>
                     </td>
          </tr>
            <tr>
                <td valign="top" width="10%">
                    <extddl:ExtendedDropDownList ID="extddlNotification" runat="server" Width="250px"
                        Procedure_Name="sp_get_notification" Flag_Key_Value="get_notification" Connection_Key="Connection_String"
                        OnextendDropDown_SelectedIndexChanged="extddlNotification_extendDropDown_SelectedIndexChanged"
                        AutoPost_back="true"></extddl:ExtendedDropDownList>
                </td>
                <td width="90%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 200px; text-align: left" border="0">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; width: 200px; text-align: left">
                                            <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                width: 500px; border-bottom: #b5df82 1px solid ;background-color:White;" cellspacing="0" cellpadding="0"
                                                align="left" border="0" >
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 200px; height: 0px" align="center">
                                                            <table style="width: 500px" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                                                cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                <ContentTemplate>
                                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                                                      
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 500px"  valign="middle" align="left" bgcolor="#b5df82" >
                                                                            <b class="txt3">Email Notifications - </b>
                                                                            <asp:Label ID="lblheder" runat="server" Font-Size="X-Small"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 60%" valign="middle" align="center" bgcolor="#b5df82"
                                                                            >
                                                                            <asp:Label ID="lblmsgEmail" runat="server" Font-Size="Small" ForeColor="red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 40%" colspan="2">
                                                                            <asp:Label ID="lblLawFirm" runat="server" Font-Size="Small" Visible="False" Font-Bold="True"
                                                                                Font-Italic="False" Text="Law Firm"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top; width: 40%; height: 0px" align="center" colspan="2">
                                                                            <extddl:ExtendedDropDownList ID="extddlLawFirm" runat="server" Width="60%" AutoPost_back="true"
                                                                                OnextendDropDown_SelectedIndexChanged="extddlLawFirm_extendDropDown_SelectedIndexChanged"
                                                                                Connection_Key="Connection_String" Flag_Key_Value="GET_USER_LIST" Procedure_Name="SP_MST_LEGAL_LOGIN"
                                                                                Visible="false"></extddl:ExtendedDropDownList>
                                                                                
                                                                        </td>
                                                                    </tr>
                                                           
                                                                    <tr>
                                                                        <td style="width: 60%" >
                                                                            <asp:Label ID="lblEmailAddress" runat="server" Font-Size="Small" Visible="False"
                                                                                Font-Bold="True" Font-Italic="False" Text=" Email Address"></asp:Label></td>
                                                                    </tr>
                                                                     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="width: 60%";  align="center" colspan="2">
                                                                           
                                                                                    <asp:TextBox ID="txtEmailAddress" runat="server" Height="80px" Width="90%" MaxLength="4000"
                                                                                        TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                                               
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100%";  align="center" colspan="2">
                                                                            <asp:Label ID="lblNote" runat="server" Font-Size="Small" ForeColor="red" Visible="false"
                                                                                Text="(Comma separated email addresses)"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 60%">
                                                                            &nbsp;
                                                                            <asp:Label ID="lblInterval" style="width: 50%; font-size:1em; font-weight:bold;" runat ="server" Text="Interval (in Days):" Visible="false"  align="left"></asp:Label>    
                                                                            <asp:TextBox ID="txtIntervalProvider" style="width: 10%" runat="server" Visible="false"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                    </tr>                                                                    
                                                                   
                                                                    <!-- prashant 10 march 2011 -->
                                                                    <tr align="left" id="tbllawfirm" runat="Server">
                                                                          <td style="width: 30%; text-align: left" align="left">
                                                                              <asp:Panel ID="contentPanellaw" runat="server" Height="450px" Width="100%" ScrollBars="Both">
                                                                                  <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                                                                                      ID="tvwmenulaw" OnTreeNodePopulate="Node_Populatelaw">
                                                                                      <Nodes>
                                                                                          <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                                                                              Value="0"></asp:TreeNode>
                                                                                      </Nodes>
                                                                                  </asp:TreeView>
                                                                              </asp:Panel>
                                                                              <asp:HiddenField ID="hfselectedNodelaw" runat="server"></asp:HiddenField>
                                                                              <asp:HiddenField ID="hfOrderlaw" runat="server"></asp:HiddenField>
                                                                              &nbsp;&nbsp;
                                                                              <asp:TextBox ID="txtCompanyID1law" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                              <asp:TextBox ID="txtUserIDlaw" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                          </td>
                                                                          <td style="width: 30%"  align="center">
                                                                             <asp:Panel ID="Panel3law" runat="server" Height="450px" Width="250px" ScrollBars="Horizontal"
                                                                                 allign="center" BorderStyle="Ridge">
                                                                                 <asp:ListBox runat="server" Height="430px" ID="lbSelectedDocslaw" ></asp:ListBox>
                                                                             </asp:Panel>
                                                                              <%--<asp:Button ID="btnSavelaw" OnClick="btnSave_Clicklaw" runat="server" Text="Save"></asp:Button>--%>
                                                                          </td>
                                                                                                                                               
                                                                    </tr>
                                                                     <tr>
                                                                        <td style="width: 100%" align="center" colspan="2">
                                                                            <asp:Button ID="btnSaveEmail" OnClick="btnSaveEmail_Click" runat="server" Width="57px"
                                                                                Visible="false" Text="Save" OnClientClick="return Checkempty()"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                     </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="extddlLawFirm" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                    
                                                                    
                                                                    <!-- /////////////////////////// -->
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    <tr>
                                                                        <td id="tdMisDoc" runat="server">
                                                                            <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
                                                                                <ContentTemplate>
                                                                                    <table style="width: 100%; background-color:White;" id="tblMisDoc" runat="server" >
                                                                                        <tbody>
                                                                                            <tr style="text-align: left">
                                                                                                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                                                                                                    width: 100%; padding-top: 3px; height: 100%" colspan="5">
                                                                                                    <asp:Label ID="Label1" runat="server" Width="300px" Font-Size="Small" ForeColor="Black"
                                                                                                        Font-Bold="True" Text="Email Notification Missing Document" CssClass="Labels"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                                                                                                    width: 100%; padding-top: 3px; height: 100%" colspan="5">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 80%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 70%">
                                                                                                                            <asp:Label ID="lblGroup" runat="server" Text="Group Name"></asp:Label>
                                                                                                                            <%--  <asp:DropDownList ID="ddlSpeciality" runat="server" Width="143px" AutoPostBack="True"
                                                                                                        OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged"> </asp:DropDownList>--%>
                                                                                                                            <asp:TextBox ID="txtNewGroup" runat="server" Width="60%" Visible="false"></asp:TextBox>
                                                                                                                            <%--<extddl:ExtendedDropDownList ID="ExtendedGroup" runat="server" Width="80%"  
                                                                                                                            Connection_Key="Connection_String" Flag_Key_Value="GET_GROUP_LIST"  OnextendDropDown_SelectedIndexChanged="ExtendedGroup_extendDropDown_SelectedIndexChanged" AutoPost_back="true" Procedure_Name="SP_MST_GROUP_NAME">
                                                                                                                        </extddl:ExtendedDropDownList>--%>
                                                                                                                            <asp:DropDownList ID="ExtendedGroup" Width="70%" runat="server" AutoPostBack="True" AutoPost_back="true"
                                                                                                                                OnSelectedIndexChanged="ExtendedGroup_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 30%">
                                                                                                                            <asp:CheckBox ID="chkGroup" runat="server" AutoPostBack="true" Text="Add new group"
                                                                                                                                OnCheckedChanged="chkGroup_CheckedChanged"></asp:CheckBox>
                                                                                                                            
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td >
                                                                                                                            Interval :&nbsp;&nbsp;&nbsp;&nbsp;   
                                                                                                                            <asp:TextBox style="width: 10%" ID="txtInterval" runat="server" ></asp:TextBox> 
                                                                                                                            (in Days)
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="chkGroup"></asp:AsyncPostBackTrigger>
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="text-align: center" colspan="5">
                                                                                                    <div style="width: 400px; background-color:White; font-family: Times New Roman; top: 150px; text-align: center">
                                                                                                        <span id="msg" runat="server"></span>
                                                                                                        <UserMessage:MessageControl ID="MessageControl1" runat="server"></UserMessage:MessageControl>
                                                                                                      
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="left">
                                                                                                <td style="width: 30%; text-align: left" align="left">
                                                                                                    <asp:Panel ID="contentPanel" runat="server" Height="450px" Width="100%" ScrollBars="Both">
                                                                                                        <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                                                                                                            ID="tvwmenu" OnTreeNodePopulate="Node_Populate">
                                                                                                            <Nodes>
                                                                                                                <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                                                                                                    Value="0"></asp:TreeNode>
                                                                                                            </Nodes>
                                                                                                        </asp:TreeView>
                                                                                                    </asp:Panel>
                                                                                                    <asp:HiddenField ID="hfselectedNode" runat="server"></asp:HiddenField>
                                                                                                    <asp:HiddenField ID="hfOrder" runat="server"></asp:HiddenField>
                                                                                                    &nbsp;&nbsp;
                                                                                                    <asp:TextBox ID="txtCompanyID1" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 5%; text-align: left" >
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td style="width: 30%"  align="center">
                                                                                                    <asp:Panel ID="Panel3" runat="server" Height="450px" Width="250px" ScrollBars="Horizontal"
                                                                                                        allign="center" BorderStyle="Ridge">
                                                                                                        <asp:ListBox runat="server" Height="430px" ID="lbSelectedDocs" ></asp:ListBox>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save"></asp:Button>
                                                                                                </td>
                                                                                                <td style="width: 5%"  align="center">
                                                                                                </td>
                                                                                                <td style="width: 30%" align="center">
                                                                                                    <asp:Panel ID="Panel4" runat="server" Height="450px" Width="250px" ScrollBars="Horizontal"
                                                                                                        allign="center" >
                                                                                                        <%--  <asp:ListBox runat="server" Height="430px" ID="lbAllAssignedDoc">
                                            </asp:ListBox>--%>
                                                                                                        <asp:UpdatePanel ID="UpdatePanelEAdd" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:TextBox ID="txtMisDocEmailAddress" runat="server" Height="80px" Width="90%"
                                                                                                                    MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ExtendedGroup" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                        <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="red" Text="(Comma separated email addresses)"></asp:Label>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="extddlNotification"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>
     
</asp:Content>
