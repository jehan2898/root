<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HPJ1Form.aspx.cs" Inherits="AJAX_Pages_HPJ1Form" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.min.js"></script>  
    <link href="css/buttons.css" rel="stylesheet" type="text/css" />
    <link href="css/buttons-core.css" rel="stylesheet" type="text/css" />
    <link href="css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
        
   <%--<script language="javascript" type="text/javascript">

        jQuery(function ($) {

            $('[id$=txtPhyCompDate]').mask("99-99-9999");
            $('[id$=txtAllOthCompDate]').mask("99-99-9999");
        });

    </script>       --%> 
    
    <style type="text/css">
        .font {
            font-family: Verdana,Geneva, Tahoma, sans-serif;
        }

       
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <div>
        <asp:updatepanel id="UpdatePanel2" runat="server">
                        <ContentTemplate>
        <table style="width:100%; text-align:center">
            <tr>
               <td>
                   <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                     <contenttemplate>
                         <UserMessage:MessageControl runat="server" ID="usrMsg" />
                         
                                
                                
<%--                                    <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                        runat="Server">
                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        Loading...
                                    </div>--%>
                                
                            
                     </contenttemplate>
                  </asp:UpdatePanel>
               </td>
            </tr>
            <tr>
               <td style=" font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;"> <asp:Button ID="btnTopSave" runat="server"  CssClass="pure-button pure-button-primary" Text="Save" Width="100px" OnClick="btnTopSave_Click" />  
                   <asp:Button ID="btnTopCancle"  CssClass="pure-button pure-button-primary" runat="server" Text="Cancel"  Width="100px" OnClick="btnTopCancle_Click"/>
                   <asp:Button ID="btnTopPrint" runat="server"  CssClass="pure-button pure-button-primary" Text="Print" Width="100px" OnClick="btnTopPrint_Click" />
                </td>
            </tr>
            <tr>
                <td class="font" style="font-size: 14px">STATE OF NEW YORK - WORKER'S COMPENSATION BOARD</td>
                
            </tr>
            <tr>
                <td class="font" style="font-size: 14px">Bureau of Health Management</td>
                
            </tr>
            <tr>
                <td class="font" style="font-size: 14px">Office of Health Provider Administration</td>
            </tr>
            <tr>
                <td class="font" style="font-size: 14px">1-800-781-2362</td>
            </tr>
            
        </table>
        <table style="border: thin groove #000000; width:100%;  text-align:center" >
             <tr>
                <td style="font-style: italic;font-size: 14px" class="font">PROVIDER'S REQUEST FOR JUDGEMENT OF AWARD</td>
            </tr>
            <tr>
                <td style="font-style: italic;font-size: 14px" class="font">SECTION 54-b,Enforcement on Failure to Pay Award or Judgement</td>
            </tr>
         </table>
        <table  style="width:100%; text-align:left; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;" >
            <tr><td>Upon issance of an administrative award and/or arbitration decision you must wait at least 30 days before requesting consent for judgement.</td></tr>
            <tr><td>To Avoid the complications of filling unnecessary requests, waiting 60 days is recommended. The 60 day time period will allow for carriers billing/payment cycles.</td></tr>
            <tr><td>This form may be used by an authorized workers compensation provider whenever a carrier or self- insured employer has not paid for an award or decision(for awards/decision made on after march 13,2007).</td></tr>
            <tr><td>Section 54-b of workers&#39; Compensation Law provides that in the event an insurance carrier or self - insured employer defaults in the payment of an award made by the board, any party to an award may,with the Chair's consent (or the consent of the Chair's designee),</td></tr>
            <tr><td>file with the County Clerk for the country in which the injured occured or the county in which the carrier or self-insured employer has its principal place of business, a certified copy of the decision that awareded compensation.  </td></tr>
            <tr><td></td></tr>
          </table>
        <table style="width:100%;  text-align:center">
            <tr>
                <td style="font-family: Verdana, Geneva, Tahoma, sans-serif;" colspan="6"><u>Request for Consent and Certified Copy of Unpaid Award or Decision for Medical Care</u></td>
            </tr>
            <tr>
                <td style="text-align:left; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;" colspan="6"> I request consent for judgement and a certified copy of the unpaid award or decision for WCB dispute nymber(s): </td>
            </tr>
            <tr>
                <td style="text-align:center; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: medium;" colspan="6">ATTACH A COPY OF THE ORIGINAL AWARD(S) </td>
            </tr>
            <tr>
                <td style="border: thin groove #000000; height: 22px;"></td>
                <td style="border: thin groove #000000; height: 22px;"></td>
                <td style="border: thin groove #000000; height: 22px;"></td>
                <td style="border: thin groove #000000; height: 22px;"></td>
                <td style="border: thin groove #000000; height: 22px;"></td>
                <td style="border: thin groove #000000; height: 22px;"></td>
            </tr>
            
            </table>
        <table style="width:100%;  font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;">
                <tr>
                     <td colspan="6">Name and Address of&nbsp; Health Care Provider</td>
                     <td rowspan="5">
                       <table style="width:100%; font-weight: 100;">
                           <tr>
                               <td style="text-align:center">
                                    WCB Case Number</td>

                               <td style="text-align:center">
                                    WCB Authorization #</td>

                           </tr>
                           <tr>
                               <td >
                                    <asp:TextBox ID="txtWCBCaseNo" runat="server" Width="100%" ></asp:TextBox>
                               </td>

                               <td >
                                    <asp:TextBox ID="txtWCBAuthNo" runat="server" Width="100%"  ></asp:TextBox>
                               </td>

                           </tr>
                           <tr>
                               <td>Date of Accident Or Injury</td>
                               <td>Carrier Case Number</td>
                           </tr>
                           <tr>
                               <td>
                                    <asp:TextBox ID="txtAccidentDate" runat="server" Width="100%" ></asp:TextBox>
                               </td>
                               <td>
                                    <asp:TextBox ID="txtCarrCaseNo" runat="server" Width="100%" ></asp:TextBox>
                               </td>

                           </tr>
                            <tr>
                               <td>Carrier/Self-Insured Employer I.D Number</td>
                               <td>County in Which Injury Occured</td>
                           </tr>
                           <tr>
                               <td>
                                    <asp:TextBox ID="txtInsuredId" runat="server" Width="100%"  ></asp:TextBox>
                               </td>
                               <td>
                                    <asp:TextBox ID="txtInjuredOccured" runat="server" Width="100%"  ></asp:TextBox>
                               </td>

                           </tr>
                       </table>
                     </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right" ">Name1</td>
                     <td colspan="5">
                        <asp:TextBox ID="txtProviderName" runat="server" Width="100%" ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">2</td>
                     <td colspan="5">
                                    <asp:TextBox ID="txtEmptyProvider" runat="server" Width="100%"  Columns="1"></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right" ">Address</td>
                     <td colspan="5">
                                    <asp:TextBox ID="txtProvAddress" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">City</td>
                     <td>
                                    <asp:TextBox ID="txtProvCity" runat="server" ></asp:TextBox>
                                </td>
                     <td>State</td>
                     <td>
                                    <asp:TextBox ID="txtProvState" runat="server" ></asp:TextBox>
                                </td>
                     <td>Zip</td>
                     <td>
                                    <asp:TextBox ID="txtProvZip" runat="server" ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td colspan="6">Name and Address of Carrier/Self-Insured Employer</td>
                     <td rowspan="6">
                       <table style="width:100%; font-weight: 100;">
                           <tr>
                               <td colspan="2" style="text-align:center">
                                    Employer</td>

                           </tr>
                           <tr>
                               <td colspan="2">
                                    <asp:TextBox ID="txtEmp" runat="server" Width="100%" ></asp:TextBox>
                               </td>

                           </tr>
                           <tr>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr>
                               <td>
                                    &nbsp;</td>
                               <td>
                                    &nbsp;</td>

                           </tr>
                            <tr>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr>
                               <td>
                                    &nbsp;</td>
                               <td>
                                    &nbsp;</td>

                           </tr>
                           <tr>
                               <td>
                                    &nbsp;</td>
                               <td>
                                    &nbsp;</td>

                           </tr>
                       </table>
                     </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">Name1</td>
                     <td colspan="5">
                        <asp:TextBox ID="txtCarrName" runat="server" Width="100%"  ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">2</td>
                     <td colspan="5">
                                    <asp:TextBox ID="txtEmptyCarr" runat="server" Width="100%"  Columns="1"  ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">Address</td>
                     <td colspan="5">
                                    <asp:TextBox ID="txtCarrAddress" runat="server" Width="100%" TextMode="MultiLine"  ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;text-align:right"">City</td>
                     <td>
                                    <asp:TextBox ID="txtCarrCity" runat="server" ></asp:TextBox>
                                </td>
                     <td>State</td>
                     <td>
                                    <asp:TextBox ID="txtCarrState" runat="server" ></asp:TextBox>
                                </td>
                     <td>Zip</td>
                     <td>
                                    <asp:TextBox ID="txtCarrZip" runat="server" ></asp:TextBox>
                                </td>
             </tr>
                 <tr>
                     <td style="Width:10%;"">&nbsp;</td>
                     <td>
                                    &nbsp;</td>
                     <td>&nbsp;</td>
                     <td>
                                    &nbsp;</td>
                     <td>&nbsp;</td>
                     <td>
                                    &nbsp;</td>
             </tr>
            
            </table>
        <table style="width:100%; font-weight: 700; text-align:center">
            <tr><td></td></tr>
            <tr><td style="width:100%; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;">Affirmation of Non-Payment</td></tr>
        </table>
        <table style="border: thin groove #000000; width:100%; text-align:center;font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;" >
             <tr>
                <td style="font-style: normal;text-align:left" colspan="2"><u>PHYSICIANS COMPLETE THE FOLLOWING:</u></td>
            </tr>
            
            <tr>
                <td style="text-align:left" colspan="2">I state that I am a physician, authorized by law to practice in the State of New York, am not a party to this proceeding, am the physician not remunerated for the above award(s) or decision(s), have read and know the contents thereof; that the same is true to my knowledge, except as to the matters stated to be on information and belief, and as to those matters I believe it to be true. Affirmed  as true under the penalty of perjury.</td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td style="text-align:left">Written Signature(Facsimile not Accepted) <asp:TextBox ID="txtWSigntext"  runat="server" Width="416px" ></asp:TextBox>
                     &nbsp; Date <asp:TextBox ID="txtPhyCompDate" placeholder="MM/DD/YYYY" runat="server" ></asp:TextBox>
                     <ajaxcontrol:CalendarExtender ID="calextPhyCompDate" runat="server"  Format="MM/dd/yyyy" 
                                           Enabled="True" TargetControlID="txtPhyCompDate">
                       </ajaxcontrol:CalendarExtender>
                </td>
                <td style="text-align:left">&nbsp;</td>
            </tr>
             <tr>
                 <td colspan="2" style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
             </tr>
         </table>
        <table>
            <tr><td></td></tr>
        </table>
        <table style="border: thin groove #000000; width:100%;  text-align:center; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;" >
             <tr>
                <td style="font-style: normal;text-align:left" colspan="2"><u>ALL OTHERS COMPLETE THE FOLLOWING:</u></td>
            </tr>
            
            <tr>
                <td style="text-align:left" colspan="2">IMPORTANT:  BY LAW THOSE COMPLETING THIS SECTION MUST BE SWORN TO BEFOREA NOTARY PUBLIC.</td>
                
            </tr>
             <tr><td style="text-align:left" colspan="2">I state that I am a chiropractor, authorized hospital representative, physical or occupational therapist, podiatrist or psychologist, authorized by law to practice in the State of New York and/or authorized to represent a hospital, am not a party to this proceeding, am the provider or representative of a hospital not remunerated for the above award(s) or decision(s), have read and know the
                    contents thereof; that the same is true to my knowledge, except as to the matters stated to be on information and belief, and as to
                        those matters I believe it to be true. Affirmed as true under the penalty of perjury.

                 </td></tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td style="text-align:left" colspan="2">Written Signature(Facsimile not Accepted)
                    <asp:TextBox ID="txtWSigntext2" runat="server"  Width="416px"></asp:TextBox>
                    &nbsp; Date <asp:TextBox ID="txtAllOthCompDate" placeholder="MM/DD/YYYY" runat="server"></asp:TextBox>
                     <ajaxcontrol:CalendarExtender ID="calextAllOthCompDate" runat="server"  Format="MM/dd/yyyy"
                                           Enabled="True" TargetControlID="txtAllOthCompDate">
                       </ajaxcontrol:CalendarExtender>
                </td>
            </tr>
             <tr>
                 <td colspan="2" class="auto-style1"></td>
             </tr>
             <tr>
                 <td style="text-align:left" colspan="2">State of New York
                     <asp:TextBox ID="txtStateText" runat="server"  Width="298px"></asp:TextBox>
                     ) ss:
                     <asp:TextBox ID="txtSSText" runat="server"  Width="463px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align:left" colspan="2">County of
                     <asp:TextBox ID="txtCountryOf" runat="server"  Width="298px"></asp:TextBox>
                     &nbsp;)
                     <asp:TextBox ID="txtBeing" runat="server"  Width="298px"></asp:TextBox>
                     &nbsp;being duly sworn, deposes and says:</td>
             </tr>
             <tr>
                 <td style="text-align:left" colspan="2">
                     That (s)he is the
                     <asp:TextBox ID="txtHeisthe" runat="server"  Width="298px"></asp:TextBox>
                     , duly licensed in the State of New York and/or authorized to represent a hospital, who has not been remunerated for the above award(s) or decision(s), and that(s) he has read the same and knows the contents thereof; that the same is true to the knowledgeof the deponent, except as to the matters stated to be on information and belief, and as to those matters(s) he believes it to be true. 

                 </td>
             </tr>
              <tr>
                  <td style="text-align:left" colspan="2">Subscribed and sworn before me this</td>
              </tr>
              <tr>
                 <td colspan="2" style="text-align:left">
                     <asp:TextBox ID="txtDay" runat="server"  Width="238px"></asp:TextBox>
                     &nbsp;day of
                     <asp:TextBox ID="txtDayOf" runat="server"  Width="304px"></asp:TextBox>
                     , ______________________</td>
             </tr>
             <tr>
                 <td style="text-align:center; width:50%">&nbsp;</td>
                 <td style="text-align:center">(Signature of  Notary Public) </td>
             </tr>
         </table>
        <table style="width:100%; text-align:center;font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: small;">
            <tr> <td>Mail Completed form to : Workers' Compensation Board </td></tr>
            <tr> <td>Office of Health Provider Administartion </td></tr>
            <tr> <td>100 Broadway - Menands </td></tr>
            <tr> <td>Albany,NY 12241 </td></tr>
            <tr> <td> <asp:Button ID="btnBottomSave"  CssClass="pure-button pure-button-primary" runat="server" Text="Save" Width="100px" OnClick="btnBottomSave_Click" /> 
                 <asp:Button ID="btnBottomCancle" runat="server"  CssClass="pure-button pure-button-primary" Text="Cancel"  Width="100px" OnClick="btnBottomCancle_Click"/> 
                <asp:Button ID="btnBottomPrint"  CssClass="pure-button pure-button-primary"  runat="server" Text="Print" Width="100px" OnClick="btnBottomPrint_Click" />
                </td></tr>
            <tr>
               <td>
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <contenttemplate>
                         <UserMessage:MessageControl runat="server" ID="MessageControl1" />
                          
                               
                             
                                    <%--<div id="Div2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                        runat="Server">
                                        <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        Loading...
                                    </div>--%>
                               
                           
                     </contenttemplate>
                  </asp:UpdatePanel>
               </td>
            </tr>
        </table>
     </ContentTemplate>
    </asp:updatepanel>
    </div>
    </form>
</body>
</html>

