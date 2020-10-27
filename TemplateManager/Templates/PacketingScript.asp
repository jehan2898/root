<%
Public Function ReplaceAll(HtmString, rs)

  Dim Bal_Amount
  Dim AMOUNT_INTEREST
  Dim ATTORNEY_FEE
  Dim TOTAL_BILL

On Error Resume Next
 HtmString = Replace(HtmString, "[CaseCoordinator]", Trim(UCase(rs("CaseCoordinator"))), 1, -1,vbTextCompare)
 HtmString = Replace(HtmString,"Hearing_Date",Trim(UCase(CheckValidDate(ConvertToDate(rs("hearing_date"))))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "hearing_time", Trim(UCase(rs("hearing_time"))), 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "COURT_PART", Trim(UCase(rs("COURT_PART"))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_ROOM", Trim(UCase(rs("COURT_ROOM"))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_VENUE", Trim(UCase(rs("COURT_VENUE"))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_BASIS", Trim(UCase(rs("COURT_BASIS"))), 1, -1, vbTextCompare)
'  HtmString = Replace(HtmString, "COURT_ADDRESS", Trim(UCase(rs("COURT_ADDRESS"))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_ADDRESS_NEW", Trim(UCase(rs("COURT_ADDRESS"))), 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "NOWDT", FormatDateTime(Now(), 2), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TOMORROWDT", FormatDateTime(DateAdd("d", Now, 1), 2), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "CASE_ID", rs("CASE_ID"), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_NAME_AND_INJURED_PARTY", rs("PROVIDER_NAME") & " <BR> A/A/O " & Trim(ucase(rs("INJURED_PARTY"))), 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "INJUREDPARTY_NAME", Trim(UCase(rs("INJUREDPARTY_NAME"))), 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_FIRSTNAME", Trim(UCase(rs("INJUREDPARTY_FIRSTNAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_LASTNAME", Trim(UCase(rs("INJUREDPARTY_LASTNAME"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_ADDRESS", Trim(UCase(rs("INJUREDPARTY_ADDRESS"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_CITY", Trim(UCase(rs("INJUREDPARTY_CITY"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_STATE", Trim(UCase(rs("INJUREDPARTY_STATE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_ZIP", Trim(UCase(rs("INJUREDPARTY_ZIP"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_MISC", Trim(UCase(rs("INJUREDPARTY_MISC"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_PHONE", Trim(UCase(rs("INJUREDPARTY_PHONE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INJUREDPARTY_TYPE", Trim(UCase(rs("INJUREDPARTY_TYPE"))) & " " , 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "INSUREDPARTY_NAME", Trim(UCase(rs("INSUREDPARTY_NAME"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_FIRSTNAME", Trim(UCase(rs("INSUREDPARTY_FIRSTNAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_LASTNAME", Trim(UCase(rs("INSUREDPARTY_LASTNAME"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_ADDRESS", Trim(UCase(rs("INSUREDPARTY_ADDRESS"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_CITY", Trim(UCase(rs("INSUREDPARTY_CITY"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_STATE", Trim(UCase(rs("INSUREDPARTY_STATE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_ZIP", Trim(UCase(rs("INSUREDPARTY_ZIP"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_MISC", Trim(UCase(rs("INSUREDPARTY_MISC"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_PHONE", Trim(UCase(rs("INSUREDPARTY_PHONE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSUREDPARTY_TYPE", Trim(UCase(rs("INSUREDPARTY_TYPE"))) & " " , 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "ACCIDENT_DATE", Trim(UCase(rs("ACCIDENT_DATE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ACCIDENT_ADDRESS", Trim(UCase(rs("ACCIDENT_ADDRESS"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ACCIDENT_CITY", Trim(UCase(rs("ACCIDENT_CITY"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ACCIDENT_STATE", Trim(UCase(rs("ACCIDENT_STATE"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ACCIDENT_ZIP", Trim(UCase(rs("ACCIDENT_ZIP"))) & " " , 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "POLICY_NUMBER", Trim(UCase(rs("POLICY_NUMBER"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INS_CLAIM_NUMBER", Trim(UCase(rs("INS_CLAIM_NUMBER"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INDEXORAAA_NUMBER", Trim(UCase(rs("INDEXORAAA_NUMBER"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "STATUS", Trim(UCase(rs("STATUS"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INITIAL_STATUS", Trim(UCase(rs("INITIAL_STATUS"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "MEMOFIELD", Trim(rs("MEMO")) & " " , 1, -1, vbTextCompare)

 
  HtmString = Replace(HtmString, "Attorney_FileNumber", Trim(UCase(rs("Attorney_FileNumber"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_ID", Trim(UCase(rs("Attorney_FileNumber"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Date_Summons_Served", Trim(UCase(rs("Date_Summons_Served"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Date_Answer_Received", Trim(UCase(rs("Date_Answer_Received"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Motion_Date", Trim(UCase(rs("Motion_Date"))) & " " , 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Trial_Date", Trim(UCase(rs("Trial_Date"))) & " " , 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "INSURANCECOMPANY_NAME", Trim(UCase(rs("INSURANCECOMPANY_NAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_TYPE", Trim(rs("INSURANCECOMPANY_TYPE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_ADDRESS", Trim(rs("INSURANCECOMPANY_LOCAL_ADDRESS")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "InsuranceCompany_Local_City", Trim(UCase(rs("InsuranceCompany_Local_City"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_STATE", Trim(UCase(rs("INSURANCECOMPANY_LOCAL_STATE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_ZIP", Trim(UCase(rs("INSURANCECOMPANY_LOCAL_ZIP"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_PHONE", Trim(rs("INSURANCECOMPANY_LOCAL_PHONE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_FAX", Trim(rs("INSURANCECOMPANY_LOCAL_FAX")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_ADDRESS", Trim(rs("INSURANCECOMPANY_PERM_ADDRESS")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_CITY", Trim(UCase(rs("INSURANCECOMPANY_PERM_CITY"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_STATE", Trim(UCase(rs("INSURANCECOMPANY_PERM_STATE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_ZIP", Trim(UCase(rs("INSURANCECOMPANY_PERM_ZIP"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_PHONE", Trim(rs("INSURANCECOMPANY_PERM_PHONE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_FAX", Trim(rs("INSURANCECOMPANY_PERM_FAX")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_CONTACT", Trim(UCase(rs("INSURANCECOMPANY_CONTACT"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "INSURANCECOMPANY_EMAIL", Trim(UCase(rs("INSURANCECOMPANY_EMAIL"))) & " ", 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "PROVIDER_NAME", Trim(UCase(rs("PROVIDER_NAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_TYPE", Trim(rs("PROVIDER_TYPE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_ADDRESS", Trim(rs("PROVIDER_LOCAL_ADDRESS")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_CITY", Trim(UCase(rs("PROVIDER_LOCAL_CITY"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_STATE", Trim(UCase(rs("PROVIDER_LOCAL_STATE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_ZIP", Trim(UCase(rs("PROVIDER_LOCAL_ZIP"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_PHONE", Trim(rs("PROVIDER_LOCAL_PHONE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_LOCAL_FAX", Trim(rs("PROVIDER_LOCAL_FAX")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_ADDRESS", Trim(rs("PROVIDER_PERM_ADDRESS")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_CITY", Trim(UCase(rs("PROVIDER_PERM_CITY"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_STATE", Trim(UCase(rs("PROVIDER_PERM_STATE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_ZIP", Trim(UCase(rs("PROVIDER_PERM_ZIP"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_PHONE", Trim(rs("PROVIDER_PERM_PHONE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_PERM_FAX", Trim(rs("PROVIDER_PERM_FAX")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_CONTACT", Trim(UCase(rs("PROVIDER_CONTACT"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PROVIDER_EMAIL", Trim(UCase(rs("PROVIDER_EMAIL"))) & " ", 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "DEFENDANT_ID", Trim(UCase(rs("DEFENDANT_ID"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_NAME", Trim(UCase(rs("DEFENDANT_NAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_ADDRESS", Trim(UCase(rs("DEFENDANT_ADDRESS"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_CITY", Trim(UCase(rs("DEFENDANT_CITY"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_STATE", Trim(UCase(rs("DEFENDANT_STATE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_ZIP", Trim(UCase(rs("DEFENDANT_ZIP"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_PHONE", Trim(UCase(rs("DEFENDANT_PHONE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_FAX", Trim(UCase(rs("DEFENDANT_FAX"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DEFENDANT_EMAIL", Trim(UCase(rs("DEFENDANT_EMAIL"))) & " ", 1, -1, vbTextCompare)


  HtmString = Replace(HtmString, "ADJUSTER_NAME", Trim(UCase(rs("ADJUSTER_FIRSTNAME") & " " & rs("ADJUSTER_LASTNAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ADJUSTER_PHONE", Trim(UCase(rs("ADJUSTER_PHONE"))) & " ", 1, -1, vbTextCompare) 
  HtmString = Replace(HtmString, "ADJUSTER_FAX", Trim(UCase(rs("ADJUSTER_FAX"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "ADJUSTER_EMAIL", Trim(UCase(rs("ADJUSTER_EMAIL"))) & " ", 1, -1, vbTextCompare)


  HtmString = Replace(HtmString, "Attorney_FirstName", Trim(UCase(rs("Attorney_FirstName"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_LastName", Trim(UCase(rs("Attorney_LastName"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_Name", Trim(UCase(rs("Attorney_FirstName") & " " & rs("Attorney_LastName"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_Address", Trim(UCase(rs("Attorney_Address"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_City", Trim(UCase(rs("Attorney_City"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_State", Trim(UCase(rs("Attorney_State"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_Zip", Trim(rs("Attorney_Zip")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_Phone", Trim(UCase(rs("Attorney_Phone"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Attorney_Fax", Trim(rs("Attorney_Fax")) & " ", 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "COURT_NAME", Trim(UCase(rs("COURT_NAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_VENUE", Trim(UCase(rs("COURT_VENUE"))) & " ", 1, -1, vbTextCompare)
'  HtmString = Replace(HtmString, "COURT_ADDRESS", Trim(UCase(rs("COURT_ADDRESS") & " ")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_ADDRESS", Trim(UCase(rs("COURT_ADDRESS"))), 1, -1, vbTextCompare)
  'HtmString = Replace(HtmString, "COURT_ADDRESS", Trim(UCase(rs("COURT_ADDRESS"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_BASIS", Trim(rs("COURT_BASIS")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "COURT_MISC", Trim(UCase(rs("COURT_MISC"))) & " ", 1, -1, vbTextCompare)


  HtmString = Replace(HtmString, "SETTLEMENT_AMOUNT", FormatNumber(rs("Settlement_amount"), 2) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLEMENT_INT", FormatNumber(rs("SETTLEMENT_INT"), 2)  & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLEMENT_AF", FormatNumber(rs("Settlement_AF"), 2) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLEMENT_FF", FormatNumber(rs("SETTLEMENT_FF"), 2)  & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLEMENT_TOTAL", FormatNumber(rs("SETTLEMENT_TOTAL"), 2)  & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLEMENT_DATE", FORMATDATETIME(rs("SETTLEMENT_DATE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLED_WITH_NAME", Trim(UCase(rs("SETTLED_WITH_NAME"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLED_WITH_PHONE", Trim(UCase(rs("SETTLED_WITH_PHONE"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "SETTLED_WITH_FAX", Trim(UCase(rs("SETTLED_WITH_FAX"))) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TRANSACTIONS_AMOUNT", FormatNumber(rs("Transactions_amount"), 2) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TRANSACTIONS_FEE", FormatNumber(rs("TRANSACTIONS_FEE"), 2)  & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TRANSACTIONS_DATE", FORMATDATETIME(rs("TRANSACTIONS_DATE")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TRANSACTIONS_DESCRIPTION", rs("TRANSACTIONS_DESCRIPTION")& " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "TRANSACTIONS_TYPE", rs("TRANSACTIONS_TYPE")& " ", 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "CLAIM_AMOUNT", FormatNumber(rs("CLAIM_AMOUNT"), 2) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "Balance", FormatNumber(rs("CLAIM_AMOUNT")-rs("paid_AMOUNT"), 2) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "PAID_AMOUNT", FormatNumber(rs("PAID_AMOUNT"), 2)  & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DATEOFSERVICE_START", FORMATDATETIME(rs("DATEOFSERVICE_START")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DATEOFSERVICE_END", FORMATDATETIME(rs("DATEOFSERVICE_END")) & " ", 1, -1, vbTextCompare)
  HtmString = Replace(HtmString, "DATE_BILLSENT", rs("DATE_BILLSENT") & " ", 1, -1, vbTextCompare)

  HtmString = Replace(HtmString, "HEADER_LOGO", "<IMG SRC='test.JPG' height='250' width='600'>")
  HtmString = Replace(HtmString, "SCANNED_SIGNATURE", "<IMG SRC='sign.gif'>")

  
  HtmString = Replace(HtmString, "User_Id", "System")
  
  
   stRANS = "select * from TBLTRANSACTIONS where case_id='"&rs("CASE_ID")&"'"
   set rstRANS=DataConn.Execute(stRANS)
  
  Dim p, r, n
  p = rs("CLAIM_AMOUNT")
  r = 1.02
  n = DateDiff("d", rs("DATE_BILLSENT"), Now()) / 365
  
 AMOUNT_INTEREST = (p * (r ^ n)) - p
  
  ATTORNEY_FEE = 20 / 100 * (rs("Claim_Amount") + AMOUNT_INTEREST)
  at_fee = "80.00"
	if cdbl(ATTORNEY_FEE) < cdbl(at_fee) then
		ATTORNEY_FEE = cdbl(at_fee) 
	end if

  TOTAL_BILL = Bal_Amount + AMOUNT_INTEREST + ATTORNEY_FEE 
  
  HtmString = Replace(HtmString, "AMOUNT_INTEREST", FormatCurrency(AMOUNT_INTEREST, 2), 1, -1, 1)
  HtmString = Replace(HtmString, "ATTORNEY_FEE", FormatCurrency(ATTORNEY_FEE, 2), 1, -1, 1)
  HtmString = Replace(HtmString, "TOTAL_BILL", FormatCurrency(TOTAL_BILL, 2), 1, -1, 1)
  ReplaceAll = HtmString
  Err.Clear

End Function

 Public Function CheckValidDate(ByVal szDate)
        Dim sznewdate
        
            sznewdate = szDate.Substring(6)
            If (sznewdate.Equals("1900")) Then
                sznewdate = ""
            Else
                sznewdate = szDate
            End If
      
        
        Return sznewdate
    End Function
	  Private  Function ConvertToDate(ByVal dteDate) 
        If IsDate(dteDate) = True Then
            Dim dteDay, dteMonth, dteYear
            dteDay = Day(dteDate)
            dteMonth = Month(dteDate)
            dteYear = Year(dteDate)
            Return Right(CStr(dteMonth + 100), 2) & "/" & Right(CStr(dteDay + 100), 2) & "/" & dteYear
        Else
        End If
        
    End Function

%>
