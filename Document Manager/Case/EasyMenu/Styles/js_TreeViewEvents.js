// JScript File

function ob_OnNodeDrop(src, dst, copy)
{    
    // add client side code here	
    //alert("Node with id:" + src + " was " + (!copy ? "moved" : "copied") + " to node with id:" + dst);
    	   
	if(ob_ev("OnNodeDrop"))
	{
		if(document.getElementById(dst).parentNode.parentNode.firstChild.firstChild.className == "ob_t8") {
			dst = "root";
		} 
	    if(typeof ob_post == "object")
	    {
	        ob_post.AddParam("src", src);
	        ob_post.AddParam("dst", dst);
	         //Change "TreeEvents.aspx" with the name of your server-side processing file
	        ob_post.post("vb_CaseInformation.aspx", "OnNodeDrop");
	    }
	    else
	    {
	        alert("Please add obout_AJAXPage control to your page to use the server-side events");
	    }
	}
	        var sURL = unescape(window.location.pathname);  
            alert('Moved Successfully');      
            window.location.href = sURL;    
}



function ob_OnNodeEdit(id, text, prevText)
{    
    // add client side code here
	//alert("OnNodeEdit on " + id + "\n  text: " + text + "\n prevText: " + prevText);
		
	if(ob_ev("OnNodeEdit"))
	{
		if(document.getElementById(id).parentNode.parentNode.firstChild.firstChild.className == "ob_t8") {
			id = "root";
		}
	    if(typeof ob_post == "object")
	    {
	        ob_post.AddParam("id", id);
	        ob_post.AddParam("text", text);
	        ob_post.AddParam("prevText", prevText);
	        //Change "TreeEvents.aspx" with the name of your server-side processing file
	        ob_post.post("vb_CaseInformation.aspx", "OnNodeEdit");
	    }
	    else
	    {
	        alert("Please add obout_AJAXPage control to your page to use the server-side events");
	    } 
	} 
}

function ob_OnAddNode(parentId,childId, textOrHTML, expanded, image, subTreeURL)
{    
	// add client side code here
	/*alert("OnAddNode:\n  parentId: " + (parentId || "none")
			+ "\n  childId: " + (childId || "none")
			+ "\n  textOrHTML: " + (textOrHTML || "none")
			+ "\n  expanded: " + (expanded || "false")
			+ "\n  image: " + (image || "none")
			+ "\n  subTreeURL: " + (subTreeURL || "none"));*/
     
	if(ob_ev("OnAddNode"))
	{
	    debugger;
		if(document.getElementById(parentId).parentNode.parentNode.firstChild.firstChild.className == "ob_t8") {
			parentId = "root";
		}
	    if(typeof ob_post == "object")
	    {
	        ob_post.AddParam("parentId", parentId);
	        ob_post.AddParam("childId", childId);
	        ob_post.AddParam("textOrHTML", textOrHTML);
	        ob_post.AddParam("expanded", expanded ? expanded : 0);
	        ob_post.AddParam("image", image ? image : "");
	        ob_post.AddParam("subTreeURL", subTreeURL ? subTreeURL : "");
	        
	         ob_post.AddParam("CaseID", document.getElementById("Text1").value);
	        //Change "TreeEvents.aspx" with the name of your server-side processing file
	        ob_post.post("vb_CaseInformation.aspx", "OnAddNode",fixClientNodeId);
	       
	    } 		
	    else
	    {
	        alert("Please add obout_AJAXPage control to your page to use the server-side events");
	    } 
	}
	
	
	if(ob_ev("upLoadFile"))
	{
	   
	    if(document.getElementById(parentId).parentNode.parentNode.firstChild.firstChild.className == "ob_t8")
	     {
			    parentId = "root";
		 }
	     if(typeof ob_post == "object")
	     {
	            ob_post.AddParam("parentId", parentId);
	            ob_post.AddParam("childId", childId);
	            ob_post.AddParam("textOrHTML", textOrHTML);
	            ob_post.AddParam("expanded", expanded ? expanded : 0);
	            ob_post.AddParam("image", image ? image : "");
	            ob_post.AddParam("subTreeURL", subTreeURL ? subTreeURL : "");
    	        
	             ob_post.AddParam("CaseID", document.getElementById("Text1").value);
	            //Change "TreeEvents.aspx" with the name of your server-side processing file
	            ob_post.post("vb_CaseInformation.aspx", "OnAddNode",fixClientNodeId);
	           
	      } 		
	      else
	      {
	            alert("Please add obout_AJAXPage control to your page to use the server-side events");
	      }
	      
	 }
	 //var sURL = unescape(window.location.pathname);
     //window.location.href = sURL;

}

function fixClientNodeId(newID)
{
	var oldID = tree_selected_id ;
	if ( newID.length > 30 )
	{
		// Exception
		//alert(newID);
	}
	if ( newID + "" != "-1")
	{
		document.getElementById( oldID ).id = newID;
		
	}
	
}



function ob_OnRemoveNode(id)
{    
     // add client side code here
	 //alert("OnRemoveNode on " + id);
	 	 
	 if(ob_ev("OnRemoveNode"))
	 {		
	    if(typeof ob_post == "object")
	    {			
	        ob_post.AddParam("id", id);
	       
	        //Change "TreeEvents.aspx" with the name of your server-side processing file
	        ob_post.post("vb_CaseInformation.aspx", "OnRemoveNode");
	    }
	    else
	    {
	        alert("Please add obout_AJAXPage control to your page to use the server-side events");
	    } 
	 }
}


function ob_OnNodeSelect(id)
 {  

    document.getElementById("selectedID").value=id;
     var Flag=1;
                
            var list = document.getElementById('hfAllId').value.split('_');
           
            for(i=0;i<list.length;i++)
            {   
               
                if(list[i]==id)
                {
                    Flag=0 ;
                      
                   
                }
            }
                  
    
    
//    if(document.getElementById(id).firstChild.nodeValue==null)
//    {   
//        alert("1");         
//       //document.getElementById("hidnFileName").value =document.getElementById(id).firstChild.lastChild.nodeValue;
//       document.getElementById("selectedID").value=id;
//       document.getElementById("miSearch").style.display='none';
//	   document.getElementById("miScanDocument").style.display='none';
//       document.getElementById("miAddNode").style.display='none';
//       document.getElementById("miAddFile").style.display='none'; 
//       document.getElementById("miEmailFile").style.display='block';
//       document.getElementById("miFax").style.display='block'; 
//       document.getElementById("miPrint").style.display='block'; 
//       document.getElementById("miCopytoCase").style.display='none';
//       
//       document.getElementById("miDeleteNode").style.display='block';
//       document.getElementById("miCopy").style.display='none';
//       document.getElementById("miPaste").style.display='none';
//       document.getElementById("miScanDocument").style.display='none';
//       //document.getElementById('miDeleteNode').style.display='none';     
//        //        document.getElementById("miPaste").style.display='none'; 
//          if(Flag==1)
//       {
//            document.getElementById('miDeleteNode').style.display='block';
//            
//        }  else
//        {
//             document.getElementById("miDeleteNode").style.display='none';
//            
//        }
//        
//    }
    if(true)
    {
        
        var isRightMB;
        if ("which" in window.event)  // Gecko (Firefox), WebKit (Safari/Chrome) & Opera
            isRightMB = window.event.which == 3;
        else if ("button" in window.event)  // IE, Opera 
            isRightMB = window.event.button == 2;
        if (isRightMB) {
            window.event.preventDefault();
        }
        //alert("1");         
       //document.getElementById("hidnFileName").value =document.getElementById(id).firstChild.lastChild.nodeValue;
       document.getElementById("selectedID").value=id;
       document.getElementById("miSearch").style.display='block';
	   document.getElementById("miScanDocument").style.display='block';
       document.getElementById("miAddNode").style.display='block';
       document.getElementById("miAddFile").style.display='block'; 
       document.getElementById("miEmailFile").style.display='none';
       document.getElementById("miFax").style.display='none'; 
       document.getElementById("miPrint").style.display='block'; 
       document.getElementById("miCopytoCase").style.display='none';
       
       document.getElementById("miDeleteNode").style.display='block';
       document.getElementById("miCopy").style.display='block';
       document.getElementById("miPaste").style.display='block';
       document.getElementById("miScanDocument").style.display='block';
       document.getElementById("miEdit").style.display='none';
       document.getElementById("menuItem7").style.display='block';
       //document.getElementById('miDeleteNode').style.display='none';     
        //        document.getElementById("miPaste").style.display='none'; 
     var  hdnRe = document.getElementById('hdnRemerge');
      

       if (hdnRe.value != 'undefined') {
           document.getElementById("miMerge").style.display = 'block';


       } else {
           document.getElementById("miMerge").style.display = 'none';
       }
      
          if(Flag==1)
       {
            document.getElementById('miDeleteNode').style.display='block';
            
        }  else
        {
             document.getElementById("miDeleteNode").style.display='none';
            
        }
        
    }
    else
    {
        //alert("2");
         
        //        document.getElementById("miPaste").style.display='none'; 
       document.getElementById("miSearch").style.display='block';
       document.getElementById("miScanDocument").style.display='block';
       document.getElementById("miAddNode").style.display='block';
       document.getElementById("miAddFile").style.display='block'; 
       //document.getElementById("miSearch").style.display='block';
       document.getElementById("miPrint").style.display='block'; 
       document.getElementById("miEmailFile").style.display='none';
       document.getElementById("miFax").style.display='none'; 
       if(Flag==1)
       {
            document.getElementById('miDeleteNode').style.display='block';
           
        }  else
        {
             document.getElementById("miDeleteNode").style.display='none';
           
        }
       document.getElementById("miCopytoCase").style.display='none';
       //document.getElementById("miDeleteNode").style.display='block';
       document.getElementById("miCopy").style.display='none';
       document.getElementById("miPaste").style.display='none';
    }
    document.getElementById("hidnSelected").value = document.getElementById(id).firstChild.nodeValue; 
    document.getElementById("selectedID").value=id;
  //    if(document.getElementById('lblSession').value == 'Admin' || document.getElementById('lblSession').value =='Super Admin') 
//    {
//     
//     document.getElementById('miDeleteNode').style.display='block'; 
//    }
//    else
//    {
//       document.getElementById('miDeleteNode').style.display='none'; 
//    }
   
      
 }

