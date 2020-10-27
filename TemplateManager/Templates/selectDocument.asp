<!--#include file = "dsn.asp"-->
<%
	Dim szSelectedDoc
	szSelectedDoc = request("selid")
	If szSelectedDoc <> "" Then
		Dim szQuery,szQueryHTMPath,szHTMFile,szDocFile
		szSelectedDoc = mid(szSelectedDoc,InStr(1,szSelectedDoc,"-")+1)
		szQuery = "select [filename],lower(right([filename],4)) [ext] from tbldocimages where imageid = '" & szSelectedDoc & "'"
		'szQueryHTMPath = "select parametervalue from tblApplicationSettings where parametername='TemplateDocsPath'"
		
		Dim rs
		set rs=Server.CreateObject("ADODB.RecordSet")

		'rs.open szQueryHTMPath,DataConn
		'szHTMPath = rs("parametervalue")
		'response.write("HTM PATH : " & szHTMPath)
		
		'rs.close()
		Dim szExt
		rs.open szQuery,DataConn
		szDocFile = rs("filename")
		szExt = rs("ext")
		
		'szHTMFile = Trim(Mid(szDocFile,1,Len(szDocFile)-4) & ".htm")
		szHTMFile = Trim(Mid(szDocFile,1,Len(szDocFile)-4))

		If szExt <> ".doc" then
			response.write("Only Microsoft Word Documents can be edited.")
		Else
			Response.Redirect "http://192.168.100.3/ZwerlingCM/Templates/richeditter.asp?Type=S&Case_Id=" & request("Case_ID")& "&seldoc=" & szHTMFile
		End if
	End if
%>