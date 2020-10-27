<%
'%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%send mail%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


if Request("mailvar")<>"" then
  Dim sendDocmail
  Dim mail2var
  'Dim faxsent
  
  mail2var=Request("mailvar")
  sendDocmail=Request("HtmString")
  
set o_mailer = Server.CreateObject("CDONTS.NewMail")
'o_mailer.AttachURL "sign.jpg", "sign.jpg"
o_mailer.From = "smurchison@shaplaw.com"
o_mailer.To = ""&mail2var
o_mailer.Subject = "Re:"&Now()
o_mailer.BodyFormat = 0 
o_mailer.MailFormat = 0 
o_mailer.Body = ""&sendDocmail
o_mailer.Send
set o_mailer = nothing


Response.Write "Click <a href='javascript:history.back()'>here</a> to Go Back</h2></center>"
Response.End	


END IF
'%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%>