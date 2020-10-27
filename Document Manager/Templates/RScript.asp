<%
Public Function ReplaceAll(HtmString, rs)
 
  strConnectionString ="PROVIDER=SQLOLEDB;DATA SOURCE=LAWALLIES-MAIN\SQLEXPRESS;UID=sa;PWD=neuralit123;DATABASE=StahlDB"
  sqlfield= "exec GETFIELD"
  Set rsfield = CreateObject("ADODB.RecordSet")
  rsfield.open sqlfield, strConnectionString
  

  On Error Resume Next
  HtmString = Replace(HtmString, "nowdt", FormatDateTime(Now(), 2), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "tomorrowdt", FormatDateTime(DateAdd("d", Now, 1), 2), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "USER_ID", SESSION("UID"), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "USER_ID", SESSION("USER_ID"), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "STL_BARCODE_VALUE", "<img src='http://www.bcgen.com/demo/linear-dbgs.aspx?D=SL@" & rs("ID") & "@" & rs("CaseId") & "@1'/>", 1, -1, vbTextCompare)
  'HtmString = Replace(HtmString, "STL_BARCODE_VALUE", rs("caseid"), 1, -1, vbTextCompare)
  
  While Not rsfield.eof
	
	a = rsfield("FIELDNAME")
	b = rsfield("fieldval")
	If b = "money" Then
	HtmString = Replace(HtmString, rsfield("FIELDNAME"), formatcurrency(rs(a),2), 1, -1, vbTextCompare)
	else
	HtmString = Replace(HtmString, rsfield("FIELDNAME"), RTrim(LTrim(rs(a))), 1, -1, vbTextCompare)
	End if

  rsfield.MoveNext
  wend
  ReplaceAll = HtmString
  'Err.Clear
End Function

%>