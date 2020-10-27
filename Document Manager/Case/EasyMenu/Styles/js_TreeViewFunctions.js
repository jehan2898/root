
// JScript File

	
		// function used for adding file node - it is triggered when the users clicks on the Add button
		
		function upLoadFile()
		{
		    //alert("upLoadFile()");
            if(typeof tree_selected_id == "undefined"){
                alert("Please select the parent of the new node!");
                return;
            }
            if(document.getElementById("fileUpload").value==""){
                alert("Please enter the text of the node");
                return;
            }
            //alert("tree_selected_id " + tree_selected_id);
            document.getElementById("HiddenField1").value = tree_selected_id;
			
            document.getElementById("Button3").click();
            //var sURL = unescape(window.location.pathname);
            // alert('File successfully uploaded');
            //sURL = document.getElementById('hidLA').value;
            //alert(sURL);
            //window.location.href = sURL;
		}				
			
        // function used for adding a node - it is triggered when the users clicks on the Add button
        
//            var flag = 1;
//            if(typeof tree_selected_id == "undefined"){
//                alert("Please select the parent of the new node!");
//                return;
//            }
//            if(document.getElementById("sNewNodeText").value==""){
//                alert("Please enter the text of the node");
//                return;
//            }
//           
//            for(i=0;i<arr.length;i++)
//            {
//            
//                if(arr[i][1] == tree_selected_id)
//                {
//                    var typechk = arr[i][2];
//                    if(typechk != null || typechk != "")
//                    {
//                        var chkaddnodebit = typechk.substring(0,1);
//                        if(chkaddnodebit == "0")
//                        {
//                            alert('You cannot add nodes manually to a system nodes in Document Management System');
//                            return;
//                        }
//                        else
//                        {
//                           flag = 2;
//                            break; 
//                        }
//                    }
//                    else if(typechk == null || typechk == "")
//                    {
//                        flag = 2;
//                        break;  
//                    } 
//                }
//            }
//            
//            if(flag == 2)
//            {
//            
                //document.getElementById("sNewNodeText").focus();
                function addNode()
                {
                    debugger;
                if(typeof tree_selected_id != "undefined" && document.getElementById("sNewNodeText").value)
                {    
                    
                    // if a node was selected (this will be the parent of the newly created node)
                    // and if the id and text of the node were entered, then the node will be created  
                    ob_t2_Add(tree_selected_id,"", document.getElementById("sNewNodeText").value,1, "Folder.gif", null);
                    //ob_t2_Add(tree_selected_id, parseInt(Math.random() * 100).toString(), document.getElementById("sNewNodeText").value,1, "Folder.gif", null);
                    document.getElementById("sNewNodeText").value = "";
                }

                //var sURL = unescape(window.location.pathname);
               // alert('Node Added Successfully');        
                //window.location.href = sURL;

                var sURL = unescape(window.location.pathname);
                sURL = document.getElementById('hidLA').value;
                window.location.href = sURL;
                }
//        }
        
        // function that will remove the selected node
                   
//         if(typeof tree_selected_id != "undefined")
//         {
//            for(i=0;i<arr.length;i++)
//            {
//                if(arr[i][1] == tree_selected_id)
//                {
//                    var typeCheck = arr[i][2]
//                    if( typeCheck != null || typeCheck != "")
//                    {
//                        var chkDelNodeBit = typeCheck.substring(1,2);
//                        if(chkDelNodeBit == '0')
//                        {
//                            alert('You cannot delete system nodes from the Document Management System');
//                            return;
//                        }
//                        else
//                        {
//                            var conobj = confirm('Do You Want to Delete Node');
//                            if(conobj == true)
//                            {
//                                ob_t2_Remove(tree_selected_id);
//                                var sURL = document.getElementById('hidLA').value;
//                                //alert(sURL)
//                                window.location.href = sURL;
//                                alert('Deleted successfully');
//                                break;
//                            }
//                        }
//                    }
//                    else if(typeCheck == null || typeCheck == "")
//                    {
                   function removeSelectedNode()
                   {                
                   if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {                  
                        var confirmobj = confirm('Do You Want to Delete Node');
                        if(confirmobj == true)
                        {
                            ob_t2_Remove(tree_selected_id);
                            var sURL = document.getElementById('hidLA').value;
                            //alert(sURL)
                            window.location.href = sURL;
                          //  alert('Deleted successfully');
//                            break;
                            //alert(tree_selected_id);
                        }
                      }
                    }
//                }
//             }
//          }    
//        }
        
        function showhideNode()
        { 
            if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='block';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('sNewNodeText').focus() ; 
            }
        }
        function showhideFile()
        { 
           if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
			document.getElementById('miSearch').style.display='none';
			document.getElementById('divmyiframe').style.display='none';
			document.getElementById('divAddNode').style.display='none';
			document.getElementById('divAddFile').style.display='block';
			document.getElementById('divCopyToCase').style.display='none';
			document.getElementById('fileUpload').focus() ;            
			}
        }
        
        // Function for Sending selected file name through query string to another page.
        function SendMail()
        {
            if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';      
         //var filename;        
         //filename=document.getElementById("HiddenField1").value;                     
         frames['myiframe'].location.href='../Case/vb_SendingMail.aspx?TID='+ tree_selected_id +"" ;
        }
        }
        
        // Function for Sending selected file name through query string to another page.
        function ViewFile(URL)
        {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';      
         //var filename;        
         //filename=document.getElementById("HiddenField1").value;                     
         // document.getElementById('myiframe').location.href=URL ;
         parent.frames['myiframe'].location=URL; 
         //document.getElementById("myiframe").src = URL;
        
        }
        
        // Function for Selected file for Faxing through query string to another page.
        function SendFax()
        {
            if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';        

            frames['myiframe'].location.href='../Case/vb_SendFax.aspx?TID='+ tree_selected_id +"" ;
                  }
        }
        
        function SearchOCRData()
        {
           if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';  
            //document.getElementById('miSearch').style.display='block';       
             
            frames['myiframe'].location.href='vb_Search.aspx';
            }
        }
        
        // Function for selected file for Printing name through query string to another page.
        function SendPrint()
        {  
           if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('miSearch').style.display='none';        
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
                       
            document.getElementById("printButton").click();
         
            var sURL = unescape(window.location.pathname);
            alert('Your file has been sent to printer');
            window.location.href = sURL;
                   } 
        }
        function GetWebScanUrl()
        {
          if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            var szNodeid = tree_selected_id;
            var szCaseId = document.getElementById('hCase').value;
            var szCompanyId = document.getElementById('hfCompanyId').value;
            var szCaseNo = document.getElementById('hfCaseNo').value;
            var szpatientName = document.getElementById('hfPatientName').value;
            var szCompanyName = document.getElementById('hfcompanyname').value;
            var szUrl = document.getElementById('hfUrl').value + "CaseId=" + szCaseId + "&NodeId=" + szNodeid + "&CompanyId=" + szCompanyId + "&Flag=DocMgr"+ "&CaseNo=" + szCaseNo+ "&PName=" + szpatientName + "&CompanyName=" +szCompanyName;
            //alert(szUrl);
            window.open(szUrl,"Scan_Document","channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550");
            }
        }

        //function for scan upload
        function GetWebScanUpload() {
            if (document.getElementById("hdnMenu").value == "True") {
                alert("Permission to perform this operation is permanently disabled for your account");
                return;
            }
            else {
                var szNodeid = tree_selected_id;
                var szCaseId = document.getElementById('hCase').value;
                var szCaseNo = document.getElementById('hfCaseNo').value;
                scanDocManager(szCaseId, szNodeid, szCaseNo, '3');
            }
        }

        // Function for selected file or folder copyed.
        function CopyToCaseSelectedNode()
        {
         if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='block';
            document.getElementById("SourceID").value=tree_selected_id;
            }
        }  
          // Function for selected file or folder Copyed. 
        function CopySelectedNode()
        { 
           if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById("SourceID").value=tree_selected_id;
            alert(tree_selected_id);
            document.getElementById('miPaste').disabled=false;
            }
        }
         // Function for selected file or folder Pasted. 
        function PasteSelectedNode()
        {            
         if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById("btnPasteNode").click();
            document.getElementById('miPaste').disabled=true;
            }
        }  
		//Function for edit selected file
		function EditSelectedNode()
		{
		     if(document.getElementById("hdnMenu").value == "True")
                   {
                        alert("Permission to perform this operation is permanently disabled for your account");
                        return;
                   }
                   else
                   {
            document.getElementById("btnEditNode").click();
            document.getElementById('miEdit').disabled=true;
            var szPath=document.getElementById('txtdocumentStr').value;
            //alert("szPath"+szPath)
            var szUserName =  document.getElementById('hUserName').value;    
            //parent.displayFrame.location.href = "http://192.168.1.140/LCJ/Templates/selectDocument.asp?selid=" + tree_selected_id + "&Case_ID=" + document.getElementById('hCase').value;
			//alert(window.location.href);
            window.open("http://"+szPath+"Templates/selectDocument.asp?selid=" + tree_selected_id + "&Case_ID=" + document.getElementById('hCase').value +"&URL="+szPath);
			window.location.href = window.location.href;
			}
		}

		function VisibleFrame()
	    {
	           document.getElementById('miSearch').style.display='none';            
               document.getElementById('divAddNode').style.display='none';
               document.getElementById('divAddFile').style.display='none';
               document.getElementById('divCopyToCase').style.display='none';
               document.getElementById('divmyiframe').style.display='block';               
	    }
	    
	    function showhideRename()
        { 
			document.getElementById('miSearch').style.display='none';
			document.getElementById('divAddNode').style.display='none';
			document.getElementById('divmyiframe').style.display='none';
			document.getElementById('divRenameNode').style.display='block';
			document.getElementById('divAddFile').style.display='none';
			document.getElementById('divCopyToCase').style.display='none';
			document.getElementById('txtRenameNode').focus() ;            
        }
        
        function renameNode()
        {
            if(typeof tree_selected_id == "undefined")
            {
                alert("Please select the parent of the new node!");
                return;
            }
            if(document.getElementById("txtRenameNode").value=="")
            {
                alert("Please enter the name of the node");
                return false;
            }
		}
		
		function selectMultipleNodes()
		{
	        var arrElements = document.getElementsByTagName("input");
	        var arrNodes = new Array();
	        var j=0;
            if(arrElements.length)
            {
                for(var i=0; i<arrElements.length; i++)
                {
                    if(arrElements[i].type == "checkbox" && ob_elementBelongsToTree(arrElements[i]))
                    {
                        if(arrElements[i].checked)
                        {
                            arrNodes[j]=arrElements[i].id
                            j = j + 1;
                        }
                    }
                }
            }                
            document.getElementById("nodeid").value = arrNodes.join(",");
                      
            document.getElementById('btnEmailSelectFiles').click();
		}
		
		function ob_t2_SelectCheckboxes(bType)
        {
            var arrElements = document.getElementsByTagName("input");
            if(arrElements.length)
            {
                for(var i=0; i<arrElements.length; i++)
                {
                    if(arrElements[i].type == "checkbox" && ob_elementBelongsToTree(arrElements[i]))
                    {
                        arrElements[i].checked = bType;
                    }
                }
            }
        }
		function Remerge()
		{
		    document.getElementById("btnREMergePDF").click();
		}
        function mergeNode()
		{
	        var arrElements = document.getElementsByTagName("input");
	        var arrNodes = new Array();
	        var j=0;
	        //alert(arrElements.length);
            if(arrElements.length)
            {
                for(var i=0; i<arrElements.length; i++)
                {
                    if(arrElements[i].type == "checkbox" && ob_elementBelongsToTree(arrElements[i]))
                    {
                        if(arrElements[i].checked)
                        {
                            arrNodes[j]=arrElements[i].id
                            j = j + 1;
                        }
                    }
                }
            }                
           
                      
            //document.getElementById('btnMergePDF').click();
            document.getElementById("hdnids").value = arrNodes.join(",");
            document.getElementById("btnMergePDF").click();
              
            //document.getElementById('divid4').style.zIndex = 1;
            //document.getElementById('divid4').style.position = 'absolute';
            //document.getElementById('divid4').style.left= '250px'; 
            //document.getElementById('divid4').style.top= '80px';       
            //document.getElementById('divid4').style.visibility='visible'; 
            
            //document.getElementById('frameeditexpanse1').src ='./SetOrder.aspx?NodeList='+arrNodes;
		}
		
        function DisplayFrame() {
            SelectNode();
            debugger;
            document.getElementById('divid4').style.zIndex = 1;
            document.getElementById('divid4').style.position = 'absolute';
            document.getElementById('divid4').style.left = '250px';
            document.getElementById('divid4').style.top = '80px';
            document.getElementById('divid4').style.visibility = 'visible';

        }
        function SelectNode() {
            var ids = document.getElementById("hdnids").value.split(",")
            var arrElements = document.getElementsByTagName("input");
            for (var i = 0; i < arrElements.length; i++) {
                if (ids.indexOf(arrElements[i].id) > -1)
                    arrElements[i].checked = true;

            }

        }

		function setorder()
		{
		    var listbox = document.getElementById('lstPDF');
		    var strOrder;
		    strOrder = '';
		    for(var i = 0; i < listbox.options.length; ++i) 
		    {           
		        if(i==0)
		            strOrder = listbox.options[i].value;
		        else
		            strOrder = strOrder + "," + listbox.options[i].value ;
		    }
		    var answer = prompt("Merged PDF will be saved under Saved Letters. Please enter filename");
            document.getElementById("hidnFile").value = answer;
            document.getElementById('hidnOrderFiles').value=strOrder;
            if(answer!=null)
            {
                document.getElementById('pnlProgress').style.visibility = 'visible';
                $("#btnCancel").attr("disabled", true);
                $("#btnSelectOrder").attr("disabled", true);
                document.getElementById("btnDone").click();

		    }
		}
		
		function chek(strName)
		{
		    //alert(strName);
		    if(confirm("'These PDF not found : '" + strName + "''. Do you want to continue?")==true)
		    {
		        document.getElementById('pnlProgress').style.visibility = 'visible';
		        $("#btnSelectOrder").attr("disabled", true);
		        $("#btnCancel").attr("disabled", true);
		        document.getElementById("btnCheck").click();
		        return true;
		    }else
		    {
		        return false;
		    }
		}