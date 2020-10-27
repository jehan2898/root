<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_AssignMenuToRole.aspx.cs" Inherits="Bill_Sys_AssignMenuToRole" validateRequest="false" enableEventValidation="false" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script>
function selectAll(){
        
        var objList = document.frmmenutorole.lstMenusToRole;
    //    alert(objList);
        for(var i =0 ; i< objList.length ; i++){
            objList.options[i].selected = true;
        }
        document.Form1.hidDataChanged.value='0';
    }
    
   function selectDoc(){
        var objTempType=document.getElementById("lstMenu");
        var arrSelectedDoc = new Array();
        var arrTempType=new Array();
        var arrLookup = new Array();
        var objListDoc=document.getElementById("lstMenusToRole");
        var i;
        var arrSelectedDoc = new Array();
        var selTempType;
        var selIndex=0;
      
        for (i = 0; i < objListDoc.options.length; i++){
           arrLookup[objListDoc.options[i].text] = objListDoc.options[i].value;  
           arrSelectedDoc[i] = objListDoc.options[i].text;
        }
     
        if(objListDoc.value)
         {
            selTempType=objTempType.value;
            selIndex= objListDoc.selectedIndex;
         }
      
          var index = arrSelectedDoc.length;
          var temptypeLength = 0;
          for(i=0; i<objTempType.options.length; i++)
          {
              arrLookup[objTempType.options[i].text] = objTempType.options[i].value;
              if(objTempType.options[i].selected && objTempType.options[i].value != "")
              {
                var szAcceptedDoc= objTempType.options[i].value                
                arrSelectedDoc[index]=objTempType.options[i].text
                index++;
              }
              else
              {
                arrTempType[temptypeLength] = objTempType.options[i].text;  
                temptypeLength++;
              }
           }
           var szindex;
           objTempType.length = 0;
           objListDoc.length = 0;
           for(szindex = 0; szindex < arrTempType.length; szindex++)
           {
              var no = new Option();
              no.value = arrLookup[arrTempType[szindex]];
              no.text = arrTempType[szindex]; 
              objTempType[szindex] = no;
           }
           for(szindex = 0; szindex < arrSelectedDoc.length; szindex++)
           {
             var no = new Option();
             no.value = arrLookup[arrSelectedDoc[szindex]];
             no.text = arrSelectedDoc[szindex];  
             objListDoc[szindex] = no;
           }
    }
     function getDoc(){
        var objListDoc=document.getElementById("lstMenu")
        var objListSelectedDoc=document.getElementById("lstMenusToRole")
        for(i=0; i<objListDoc.options.length; i++)
        {
        //    alert("List Text value="+objListDoc.options[i].text)
            if(objListDoc.options[i].selected)
            {
                var szSelectedDoc=objListDoc.options[i].value
        //        alert("Selected Item="+szSelectedDoc)
                objListSelectedDoc.options[0].text=szSelectedDoc
            }
        }
    }  
    
    function SelectedMenus()
    {
    
         var ddllist1= window.document.getElementById("_ctl0_ContentPlaceHolder1_extCIddlRoleName");
         var itemName1= ddllist1.options[ddllist1.selectedIndex].value;
         if(itemName1 == "-1")
         {
            alert("Please select role ...!");
            return false;
         }
        
//         var ddllist= window.document.getElementById("_ctl0_ContentPlaceHolder1_extCIddlMainMenu");
//         var itemName= ddllist.options[ddllist.selectedIndex].value;
//         if(itemName == "-1")
//         {
//            alert("Please select main menu ...!");
//            return false;
//         }
         
        
            
            
            
         var objTest;
         var tbox;
         tbox = document.getElementById("_ctl0_ContentPlaceHolder1_lstMenusToRole");
         objTest=document.getElementById("_ctl0_ContentPlaceHolder1_lbTest");
      //   alert(objTest);
         objTest.length=0;
         var x;
         for(x=0;x<tbox.options.length;x++){
            var no = new Option();
            no.text=  tbox.options[x].text;
            no.value=  tbox.options[x].value;
            no.selected = true;
            objTest[x]=no;
         }
    }
    
            
            function move(fbox, tbox){
            var ddllist= window.document.getElementById("_ctl0_ContentPlaceHolder1_extCIddlMainMenu");
            var itemName= ddllist.options[ddllist.selectedIndex].text;

           
           
                     
            var arrFbox = new Array();
            var arrFboxValue = new Array();
            var arrTbox = new Array();
            var arrLookup = new Array();
            var arrTboxValue = new Array();
            var i;

            for (i = 0; i < tbox.options.length; i++){
                arrLookup[tbox.options[i].text] = tbox.options[i].value;   
                arrTbox[i] = tbox.options[i].text;
            }
            var fLength = 0;
            var tLength = arrTbox.length;
            
            for(k = 0; k < tbox.options.length; k++)
            {
                       arrTbox[k] =  tbox.options[k].text;
                       arrTboxValue[k] =  tbox.options[k].value;
            }
         
            for(i = 0; i < fbox.options.length; i++)
            {
                arrLookup[fbox.options[i].text] = fbox.options[i].value;  
                if (fbox.options[i].selected && fbox.options[i].value != "")
                {
                
                    // Check selected Item is already exist in MenuRole List.
                    
                    for(j = 0; j < tbox.options.length; j++)
                    {
                 //       alert("Value " + j + " : " + tbox.options[j].value);
                        if(tbox.options[j].value ==  fbox.options[i].value)
                        {
                            alert("Menu already selected ...!");
                            break;
                        }
                    }
                    if(j == tbox.options.length)
                    {
                        arrTbox[tLength] = itemName + " >> " + fbox.options[i].text; 
                        arrTboxValue[tLength] =  fbox.options[i].value; 
                        tLength++;
                    }
                    
                    
                    
                    
                    //////////
                    arrFbox[fLength] = fbox.options[i].text;  
                    arrFboxValue[fLength] = fbox.options[i].value;
                    fLength++;
                    
                }
                else {
                    arrFbox[fLength] = fbox.options[i].text; 
                    arrFboxValue[fLength] = fbox.options[i].value; 
                    fLength++;
                }
            }
            fbox.length = 0;
            tbox.length = 0;
            var index;

            for(index = 0; index < arrFbox.length; index++){
                var no = new Option();
                no.value = arrFboxValue[index];
                no.text = arrFbox[index];
                fbox[index] = no;
            }
            for(index = 0; index < arrTbox.length; index++){
                var no = new Option();
                no.value = arrTboxValue[index];
                no.text = arrTbox[index];  
                tbox[index] = no;     
            }
            
        }
        
        function Remove(listbox)
        {
            var arrText = new Array();
            var arrValue = new Array();
            var count = 0;
            for(i = 0; i < listbox.options.length; i++)
            {
                if (listbox.options[i].selected && listbox.options[i].value != "")
                {
              //      alert(listbox.options[i].text);
                }
                else
                {
                    arrText[count] = listbox.options[i].text;
                    arrValue[count] = listbox.options[i].value;
                    count++;
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
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                         <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="5" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                            <tr>
                                                <td class="ContentLabel" style="width: 10%; height: 22px;">
                                                    Role Name:</td>
                                                <td style="width: 43%; height: 22px;">
                                                    <%--<extddl:ExtendedDropDownList ID="extCIddlRoleName" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_USER_ROLE_LIST" OldText="" Procedure_Name="SP_MST_USER_ROLES"
                                                        Selected_Text="---Select---" StausText="False" Width="100%" OnextendDropDown_SelectedIndexChanged="extCIddlRoleName_extendDropDown_SelectedIndexChanged" AutoPost_back="True" />--%>
                                                        <asp:DropDownList ID="extCIddlRoleName" runat="server" Width="100%" OnSelectedIndexChanged="extCIddlRoleName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 5%; height: 22px;">
                                                    </td>
                                                <td colspan="2" style="height: 22px">
                                                </td>
                                            </tr>
                                         
                                            <tr>
                                                <td class="ContentLabel" style="width: 10%; height: 22px;">
                                                    Main Menu:</td>
                                                <td style="width: 43%; height: 19px;">
                                                    <%--<extddl:ExtendedDropDownList ID="extCIddlMainMenu" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_MST_MAIN_MENU_LIST" OldText="" Procedure_Name="SP_GET_MST_MAIN_MENU_LIST"
                                                        Selected_Text="---Select---" StausText="False" Width="100%" OnextendDropDown_SelectedIndexChanged="extCIddlMainMenu_extendDropDown_SelectedIndexChanged" AutoPost_back="True"  />--%>
                                                        
                                                        <asp:DropDownList ID="extCIddlMainMenu" runat="server" Width="100%" OnSelectedIndexChanged="extCIddlMainMenu_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        
                                                        </td>
                                              <td class="ContentLabel" style="width: 5%; height: 19px;">
                                                    </td>
                                                <td colspan="2" style="height: 19px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;" >
                                                </td>
                                                <td style="width: 43%;font-size: 12px;font-family: arial;" align="left">
                                                    <b>Select menu from</b></td>
                                                <td class="ContentLabel" style="width: 5%">
                                                </td>
                                                <td colspan="2" align="left" style="font-size: 12px;font-family: arial;">
                                                    <b>Selected menu</b></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 10%">
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:ListBox ID="lstMenu" runat="server" Width="100%" SelectionMode="Multiple" Height="355px" ></asp:ListBox></td>
                                                <td class="ContentLabel" style="width: 5%; text-align:center;">
                                                <input type="button" onclick="move(this.form._ctl0_ContentPlaceHolder1_lstMenu,this.form._ctl0_ContentPlaceHolder1_lstMenusToRole)" value=">>" style="font-size: 12px;color: #000099;font-family: arial;background-color: #8babe4;border: 1px solid #000099;	padding-top:1px;padding-bottom:1px;padding-left:5px;padding-right:5px;"/>
                                                <asp:Button ID="btnMoveLeft" runat="server" OnClick="btnMoveLeft_Click" Text=">>" visible="false"/>
                                                <asp:Button ID="btnMoveRight" runat="server" OnClick="btnMoveRight_Click" Text="<<" visible="false"/>
                                                 <%--<input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenu,this.form.ctl00_ContentPlaceHolder1_lstMenusToRole);" value=">>" />
                                                       <input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenusToRole,this.form.ctl00_ContentPlaceHolder1_lstMenu);" value="<<" />--%>
                                                </td>
                                                <td colspan="2">
                                                    <asp:ListBox ID="lstMenusToRole" runat="server" Width="100%" SelectionMode="Multiple" Height="354px">
            
                                                   
                                                    </asp:ListBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 10%">
                                                </td>
                                                <td style="width: 43%">
                                                </td>
                                                <td class="ContentLabel" style="width: 5%">
                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                             <tr>
                                                 <td class="ContentLabel" colspan="5">
                                                     <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                                     <asp:Button ID="saveSelectedDoc" Text="Save Assigned Menus" runat="server" CssClass="Buttons"
                                                         OnClick="saveSelectedDoc_Click"></asp:Button>
                                                          <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="Buttons" OnClick="btnRemove_Click"
                                                         visible="false"></asp:Button>
                                                         <input type="button" onclick="Remove(this.form._ctl0_ContentPlaceHolder1_lstMenusToRole);" value="Remove" style="font-size: 12px;color: #000099;font-family: arial;background-color: #8babe4;border: 1px solid #000099;	padding-top:1px;padding-bottom:1px;padding-left:5px;padding-right:5px;"/>
                                                         
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
    <input type="hidden" value="" name="hidDataChanged" id="Hidden1" />
    <asp:ListBox ID="lbTest" runat="server" Width="10%" SelectionMode="Multiple" style="visibility:hidden;">
    </asp:ListBox>
    
</asp:Content>
