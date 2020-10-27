<%

fpath=Server.MapPath("/JMCaseManager/iisdocs/amend-ltr.htm")


set fso=Server.CreateObject("Scripting.FileSystemObject")
Set TextStream=fso.OpenTextFile(fpath, 1)
HtmString=TextStream.ReadAll()
Set TextSream = Nothing
Set fso = nothing
errorMessage = "0"
HtmStringAll=""

Response.write len(HtmString)


Response.write HtmString

%>