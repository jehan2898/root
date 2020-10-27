
<%
'-------------------------%%%%%%%%%%%fax module%%%%%%%%%%%%%%%%%%
if Request("savevar")<>"" then

	sendDoc=Request("HtmString")
	sendDoc = "<HTML><BODY>" & sendDoc & "</BODY></HTML>"

	dim rsSaveDocPath
	set rsSaveDocPath=Server.CreateObject("ADODB.RecordSet")
	rsSaveDocPath.open "select parametervalue from tblApplicationSettings where parametername='SavedDocsPath'",DataConn
	Set fsoSaveDoc = Server.CreateObject("Scripting.FileSystemObject")
	Set fsoSaveHTM = Server.CreateObject("Scripting.FileSystemObject")

	szSavePath = rsSaveDocPath("parametervalue") & Request ("filename")&".doc"
	szSavePathHTM = rsSaveDocPath("parametervalue") & Request ("filename")&".htm"
	
	' ************************************************
	' Doc files should be saved under document manager
	' ************************************************
	dim szCaseIdShort 
	
	szCaseIdShort = Session("CaseIDForDocs")
	
	dim szCompanyName 
	szCompanyName = Session("CompanyName")
	
	dim szCompanyID
	szCompanyID = Session("CompanyID")
	
	
	
	
	
	if(szCaseIdShort = "") then
	
	else
	    dim szShort
	    ' szShort = Mid(szCaseIdShort, instr(1,szCaseIdShort,"-",vbTextCompare)+1)
	    
	    szShort = szCaseIdShort 

        Set cmd = Server.CreateObject("ADODB.Command")
        Set cmd.ActiveConnection = DataConn
        cmd.CommandText = "SP_CREATE_SAVEDOC_NODES"
        cmd.CommandType = adCmdStoredProc
        cmd.Parameters.Refresh
        cmd.Parameters("@p_szCaseID") = szShort ' "CA00013"
        cmd.Parameters("@SZ_COMPANY_ID") = szCompanyID    '"CO00023"
        
        
        cmd.Execute
        
    '    Response.Write(" Company Name : " & szCompanyName & " -:- Company ID : " & szCompanyID & " -:- Case ID : " & szShort)
	'    Response.End() 
        
        dim rsUploadDocPath
        
        set rsUploadDocPath=Server.CreateObject("ADODB.RecordSet")
        rsUploadDocPath.open "select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'",DataConn
        
        Set fs = Server.createObject("Scripting.FileSystemObject")
        dim szFolder
        szFolder = rsUploadDocPath("ParameterValue") & szCompanyName & "/" & szShort
       ' Response.Write(szFolder)
       ' Response.End()
        if not fs.FolderExists(szFolder) then
            fs.createfolder(szFolder)
        end if
        szFolder = szFolder & "/" & "Saved Letters"
        if not fs.FolderExists(szFolder) then
            fs.createfolder(szFolder)
        end if
        
        set fs = nothing
        
        
        set cmd = Server.CreateObject("ADODB.Command")
        set cmd.ActiveConnection = DataConn
        cmd.CommandText = "SP_SAVEDOC_IN_MGR"
        cmd.CommandType = adCmdStoredProc
        cmd.Parameters.Refresh
        
        cmd.Parameters("@p_szCaseID") = szShort
   
        
     
        cmd.Parameters("@p_szFileName") = Request("filename") & ".doc"
        cmd.Parameters("@SZ_COMPANY_ID") = szCompanyID
       
        cmd.Execute
        
		Dim szRTFFile
        szRTFFile= szFolder & "\" & Request ("filename")&".htm"
		szFolder = szFolder & "\" & Request ("filename")&".doc"
		
        set flSave = fsoSaveDoc.CreateTextFile(szFolder,TRUE)
        flSave.write(sendDoc) & vbcrlf
        flSave.close

		dim fsCopy
		set fsCopy=Server.CreateObject("Scripting.FileSystemObject")
		fsCopy.CopyFile szFolder,szRTFFile
		set fsCopy=nothing
	end if

' open the file
	'ForReading = 1, ForWriting = 2, ForAppending = 3
	
	set flHTM = fsoSaveHTM.CreateTextFile(szSavePathHTM,TRUE)
	flHTM.write(sendDoc) & vbcrlf
	flHTM.close
	set flSave = nothing
	set fsoSaveDoc = nothing
	set fsoSaveHTM = nothing
 
	Response.Write "<h3>Your template was successfully modified and saved to the server.</h3>"
	Response.End
end if
%>
