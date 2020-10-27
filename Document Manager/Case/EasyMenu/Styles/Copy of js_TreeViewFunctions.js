
// JScript File

	
		// function used for adding file node - it is triggered when the users clicks on the Add button
		
		function upLoadFile()
		{		
		  if(typeof tree_selected_id == "undefined")
           {
              alert("Please select the parent of the new node!");
              return;
           }
           if(document.getElementById("fileUpload").value=="")
           {
              alert("Please enter the text of the node");
              return;
           }
           
           document.getElementById("HiddenField1").value = tree_selected_id;
           
           document.getElementById("Button3").click();
                    
            
//           if(typeof tree_selected_id != "undefined" && document.getElementById("fileUpload").value)
//           {    
//                ob_t2_Add(tree_selected_id, parseInt(Math.random() * 100).toString(), "FILE",1, "page.gif", null);
//                document.getElementById("fileUpload").value = "";               
//           }
           var sURL = unescape(window.location.pathname);
          alert('File successfully uploaded');
          window.location.href = sURL;
		}
				
			
        // function used for adding a node - it is triggered when the users clicks on the Add button
        function addNode()
        { 
        
       
           if(typeof tree_selected_id == "undefined")
           {
              alert("Please select the parent of the new node!");
              return;
           }
           if(document.getElementById("sNewNodeText").value=="")
           {
              alert("Please enter the text of the node");
              return;
           }
          
           //document.getElementById("sNewNodeText").focus();
                
           if(typeof tree_selected_id != "undefined" && document.getElementById("sNewNodeText").value)
           {    
                // if a node was selected (this will be the parent of the newly created node)
                // and if the id and text of the node were entered, then the node will be created  
                ob_t2_Add(tree_selected_id,"", document.getElementById("sNewNodeText").value,1, "Folder.gif", null);
                 //ob_t2_Add(tree_selected_id, parseInt(Math.random() * 100).toString(), document.getElementById("sNewNodeText").value,1, "Folder.gif", null);
               document.getElementById("sNewNodeText").value = "";
                             
           }
           
           var sURL = unescape(window.location.pathname);
             alert('Node Added Successfully');        
            window.location.href = sURL;
           
        }
        
        // function that will remove the selected node
        function removeSelectedNode()
        {
            if(typeof tree_selected_id != "undefined")
            {
                ob_t2_Remove(tree_selected_id);
                 //alert(tree_selected_id);
            }            
        }      
        
        function showhideNode()
        { 
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='block';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('sNewNodeText').focus() ; 
        }
        function showhideFile()
        { 
			document.getElementById('miSearch').style.display='none';
			document.getElementById('divmyiframe').style.display='none';
			document.getElementById('divAddNode').style.display='none';
			document.getElementById('divAddFile').style.display='block';
			document.getElementById('divCopyToCase').style.display='none';
			document.getElementById('fileUpload').focus() ;            
        }
        
        // Function for Sending selected file name through query string to another page.
        function SendMail()
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
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';        
                   
            frames['myiframe'].location.href='../Case/vb_SendFax.aspx?TID='+ tree_selected_id +"" ;
        
        }
        
        function SearchOCRData()
        {
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='none';
            document.getElementById('divmyiframe').style.display='block';  
            //document.getElementById('miSearch').style.display='block';       
             
            frames['myiframe'].location.href='vb_Search.aspx';
        }
        
        // Function for selected file for Printing name through query string to another page.
        function SendPrint()
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
        
        // Function for selected file or folder copyed.
        function CopyToCaseSelectedNode()
        {
            document.getElementById('miSearch').style.display='none';
            document.getElementById('divmyiframe').style.display='none';
            document.getElementById('divAddNode').style.display='none';
            document.getElementById('divAddFile').style.display='none';
            document.getElementById('divCopyToCase').style.display='block';
           
            document.getElementById("SourceID").value=tree_selected_id;
           
           
        }  
          // Function for selected file or folder Copyed. 
        function CopySelectedNode()
        { 
        
            document.getElementById("SourceID").value=tree_selected_id;
            document.getElementById('miPaste').disabled=false;
        }      
             
         
         // Function for selected file or folder Pasted. 
        function PasteSelectedNode()
        {            
            document.getElementById("btnPasteNode").click();
            document.getElementById('miPaste').disabled=true;
        }  
		//Function for edit selected file
		function EditSelectedNode()
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
      
