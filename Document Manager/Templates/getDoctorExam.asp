<%
response.write("ID=")
response.write request.querystring("Id") & "    "
response.write("CaseID=")
response.write request.querystring("Case_Id")
%>