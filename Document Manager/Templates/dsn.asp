<%
Sub Session_OnStart
   session.timeout = 60
End sub
%>
<%
Set dataconn = Server.CreateObject ("ADODB.Connection")
strConnectionString ="PROVIDER=SQLOLEDB;DATA SOURCE=LAWALLIES-MAIN\SQLEXPRESS;UID=sa;PWD=neuralit123;DATABASE=StahlDB"
dataconn.Open strConnectionString
%>