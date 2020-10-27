

<%
	Response.write("Hello")

	Set objMyForm = Server.CreateObject("CutePDF.Document")
	objMyForm.initialize("DEMO-SDK-84232865-00514228")

	if(objMyForm is nothing) then
		Response.write("<br/>objMyForm not initialized")
	else
		Response.write("<br/>objMyForm initialized")
		dim nReturn
		nReturn = objMyForm.openFile("http://localhost/BillingSystem/c4_21.pdf")
		Response.write("<br/>Returned " & nReturn)

		if nReturn = 1 then
			dim fieldName
			fieldName = objMyForm.getFieldName(15)
			objMyForm.setField fieldName,"Insurance Company name goes here"

			fieldName = objMyForm.getFieldName(10)
					
			'objMyForm.setField fieldName,"Employers name goes here Employers name goes here Employers name goes here Employers name goes here Employers name goes here"
						
			objMyForm.saveFile("C:/c4_2_1.pdf")
			Response.write("<br/>Field Name " & fieldName)
		end if
	end if
	'fieldName = objMyForm.getFieldName(nIndex) 
%>