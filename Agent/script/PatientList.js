// JScript File

function ascii_value(c){
             c = c . charAt (0);
             var i;
             for (i = 0; i < 256; ++ i)
             {
                  var h = i . toString (16);
                  if (h . length == 1)
                    h = "0" + h;
                   h = "%" + h;
                  h = unescape (h);
                  if (h == c)
                    break;
             }
             return i;
        }
    function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
            
            
         }
         
           function showCheckinPopup(objCaseID,objPatientID)
       {
            var obj3 = "";
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '350px'; 
            document.getElementById('divid2').style.top= '200px'; 
            document.getElementById('divid2').style.visibility='visible'; 
            document.getElementById('iframeCheckIn').src="Bill_Sys_CheckinPopup.aspx?CaseID="+objCaseID+"&PatientID="+objPatientID+"&CompID="+obj3+"";
            return false;            
       }
        function showCheckoutPopup(objCaseID)
       {
            var obj3 = "";
            document.getElementById('divid3').style.zIndex = 1;
            document.getElementById('divid3').style.position = 'absolute'; 
            document.getElementById('divid3').style.left= '350px'; 
            document.getElementById('divid3').style.top= '200px'; 
            document.getElementById('divid3').style.visibility='visible'; 
            document.getElementById('iframeCheckOut').src="Bill_Sys_CheckoutPopup.aspx?CaseID="+objCaseID;
            return false;            
       }
       
       function CloseCheckoutPopup()
       {
            document.getElementById('divid3').style.visibility='hidden';
            document.getElementById('iframeCheckOut').src='Bill_Sys_SearchCase.aspx';
//         
       }
       function CloseCheckinPopup()
       {
            document.getElementById('divid2').style.visibility='hidden';
            document.getElementById('iframeCheckIn').src='Bill_Sys_SearchCase.aspx';
//              
       }
      


