<%
Sub Session_OnStart
   session.timeout = 60
End sub
%>
<%
Dim DataConn,oConn
Set DataConn = Server.CreateObject ("ADODB.Connection")
'v_DSN ="Provider=SQLOLEDB;Persist Security Info=False;Data Source =192.168.1.199;Initial Catalog = JM_CM_DB; User Id = sa; Password=Password123;"
v_DSN ="Provider=SQLOLEDB;Persist Security Info=True;Data Source=192.168.1.199;Initial Catalog = BILLING_SYSTEM; User Id=sa; Password=Password123;"

DataConn.Open v_DSN
'response.Write(Dataconn.State)

Set oConn = Server.CreateObject ("ADODB.Connection")
'strConnectionString ="Provider=SQLOLEDB;dsn=dsnMassood; uid=sa; pwd=Password123"
strConnectionString ="Provider=SQLOLEDB;Persist Security Info=True;Data Source=192.168.1.199;Initial Catalog=BILLING_SYSTEM; User Id=sa; Password=Password123;"
oConn.Open strConnectionString
%>