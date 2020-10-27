<!--#include file = "dsn.asp"-->





<br>
<br>

<br>
<br>
<%
IF REQUEST("BTNSUB") <> "" THEN
SQL2 =  "SELECT * FROM TBLCASE WHERE CASEID = '"&REQUEST("CID")&"'"
SET RS2 = DATACONN.EXECUTE(SQL2)
%>
<CENTER> <img src="http://www.bcgen.com/demo/linear-dbgs.aspx?D=SL@<%=rs2("ID")%>@<%=rs2("CaseId")%>@<%=REQUEST("SELDOC")%>"/></CENTER>
<%
RESPONSE.END
END IF
%>

<form name="myfrm" action="getBarcodes.asp" method="post">
Enter Case Id : <input type="text" name="cid" value=""> <br>
<%
SQL = "SELECT * FROM tblDefaultDocTypes"
SET RS = DataConn.Execute(SQL)
%>

Select Document Type : <select name="seldoc">
                        <%DO WHILE NOT RS.EOF%>
                        <option value=<%=RS("DOCID")%>><%=RS("DOCTYPENAME")%></option>
                        <%
                        RS.MOVENEXT
                        LOOP
                        %>
                        </select> <br>
                        <INPUT TYPE="SUBMIT" NAME="BTNSUB" VALUE="PRINT BAR CODES">
                                               
</form>
