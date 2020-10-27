<%
'-------------------------%%%%%%%%%%%fax module%%%%%%%%%%%%%%%%%%
if Request("phno")<>"" then
  Dim sendDoc
  Dim phoneNo
  Dim faxsent
  
  phoneno=Request("phno")
  sendDoc=Request("HtmString")
  sendDoc = "<HTML><BODY>" & sendDoc & "</BODY></HTML>"
  
   
  Set faxSender = Server.CreateObject("FaxUtilities.FaxSenderTrace")
  faxsent = faxSender.SendHTMLFax(CStr(phoneNo), cstr(senddoc), "d:\PrintTemp")
	Response.Write "<center><h2>"
  If faxsent = 0 Then
    Response.Write ("Your fax has been submitted to the Fax queue.<br>")
    InsNotes="Insert into Notes values ('Fax Sent to "&Request("phno")&"','G',0,'"&Cid&"','"&Now()&"','"&Session("User_Id")&"')"
  DataConn.Execute InsNotes
  ElseIf faxsent = 2 Then
    Response.Write("An error occurred while connecting to the fax service.<br>")
  ElseIf faxsent = 4 Then
    Response.Write("Phone number or File name cannot be empty. Please try again.<br>")
  ElseIf faxsent = 6 Then
    Response.Write("The temp folder specified cannot be found.<br>")
  ElseIf faxsent = 7 Then
    Response.Write("An error occurred creating the temporary file for faxing.<br>")
  ElseIf faxsent = -1 Then
    Response.Write("An unknown error occurred while connecting to fax server. Your fax could not be sent.  Please try again or contact the administrator.<br>")
  ElseIf faxsent = -2 Then
    Response.Write("An unknown error occurred while sending fax. Your fax could not be sent.  Please try again or contact the administrator.<br>")
  End If
  set faxSender = nothing
  
  
Response.Write "Click <a href='javascript:history.back()'>here</a> to Go Back</h2></center>"
Response.End	

end if
%>
