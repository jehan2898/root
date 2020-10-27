<%
'-------------------------%%%%%%%%%%%fax module%%%%%%%%%%%%%%%%%%
if Request("savevar")<>"" then

  sendDoc=Request("HtmString")
  sendDoc = "<HTML><BODY>" & sendDoc & "</BODY></HTML>"
  
   
  Set fso = Server.CreateObject("Scripting.FileSystemObject")
  path = "c:\"&Request("filename")&".htm"

' open the file
	'ForReading = 1, ForWriting = 2, ForAppending = 3
	set file = fso.CreateTextFile(path,TRUE)

	' write the info to the file
	file.write(sendDoc) & vbcrlf
	' close and clean up
	file.close
	set file = nothing
	set fso = nothing
 
Response.Write "Click <a href='javascript:history.back()'>here</a> to Go Back</h2></center>"
Response.End	

end if
%>
