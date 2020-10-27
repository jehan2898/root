
/***********************************************************/
/*Project Name         :       Medical Billing System
/*Description          :       Add DocumentType page in Ajax Pages Folder.
/*Author               :       Sandeep Y
/*Date of creation     :       15 Dec 2008
/*Modified By (Last)   :       Ananda Naphade
/*Modified By (S-Last) :       
/*Modified Date        :       17 May 2010
/************************************************************/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using OboutInc.SlideMenu;
using System.IO;


public class OptionMenu
{
    private DAO_User p_objUser = null;
    private Boolean menuflag = false;
    public OptionMenu(DAO_User p_objUser)
    {
        this.p_objUser = p_objUser;
    }

    // Change  : new parameter added p_objHelpObjects
    // Purpose : this parameter of type arraylist , which contain information , which will useful while
    //           displaying left hand side menu on page.

    public void Initialize(OboutInc.SlideMenu.SlideMenu p_objSlideMenu, Bill_Sys_BillingCompanyObject billObj, Bill_Sys_UserObject billUser, Bill_Sys_SystemObject p_objSysSettings, DAO_User p_objDAOUser, SpecialityPDFDAO p_objSpecialityDAO, ArrayList p_objHelpObjects)
    {
        string FlagSYN = "0";
        // retrive of url from Helper objects
        String _url = p_objHelpObjects[0].ToString();

        p_objSlideMenu.MenuItems.Clear();
        OptionMenuDataAccess objMenuAccess = new OptionMenuDataAccess();
        ArrayList l_objArrayMenu = objMenuAccess.getOptionsMenu(this.p_objUser);
        for (int i = 0; i < l_objArrayMenu.Count; i++)
        {
            //if (((DAO_OptionMenu)l_objArrayMenu[i]).MenuName == "View Bills")
            //{
            //    Boolean check = true;
            //    for (int i1 = 0; i1 < l_objArrayMenu.Count; i1++)
            //    {
            //        if (((DAO_OptionMenu)l_objArrayMenu[i1]).MenuName == "New Bill" && p_objSysSettings.SZ_NEW_BILL == "True")
            //        {
            //            check = false;
            //         //   p_objSlideMenu.MenuItems.Remove(i);

            //        }
            //    }
            //    if (check == true)
            //    {
            //        OboutInc.SlideMenu.Child objChild2 = new Child();
            //        OboutInc.SlideMenu.SlideMenuItem o2 = objChild2;
            //        objChild2.ID = "200";
            //        objChild2.InnerHtml = "View Bills";
            //        //To Remove Ajax Pages From Path Of View Bills:-TUSHAR
            //        if (_url.Contains("AJAX Pages"))
            //        {
            //            objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_BillSearch.aspx?fromCase=true';";
            //        }
            //        else
            //        {
            //            objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_BillSearch.aspx?fromCase=true';";
            //        }
            //        //End Of Code
            //        p_objSlideMenu.MenuItems.Add(o2);
            //        continue;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
            DAO_OptionMenu objMenu = (DAO_OptionMenu)l_objArrayMenu[i];
            //  if ((objMenu.MenuName != "In-Schedule" && objMenu.MenuName != "Schedule" && objMenu.MenuName != "Referring Facility") || billObj.BT_REFERRING_FACILITY == false)
            //  {

            if (objMenu.isParent == true)
            {
                p_objSlideMenu.AddParent(objMenu.MenuID, objMenu.MenuName);
            }
            else
            {
                //p_objSlideMenu.AddChild(objMenu.MenuID, objMenu.MenuName, objMenu.URL);

                OboutInc.SlideMenu.Child objChild = new Child();
                OboutInc.SlideMenu.SlideMenuItem o = objChild;
                objChild.ID = objMenu.MenuID;

                if (objMenu.MenuName == "Add Patient")
                {
                    //   objChild.UrlTitle = objMenu.MenuName;
                    if (_url.ToLower().Contains("ajax pages"))
                        objChild.OnClientClick = "javascript:document.location.href='../Bill_Sys_Patient.aspx';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Patient.aspx';";
                    // objChild.InnerHtml = objMenu.MenuName;
                    // objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Patient.aspx';";
                    objChild.InnerHtml = objMenu.MenuName;
                    p_objSlideMenu.MenuItems.Add(o);
                }

                else if (objMenu.MenuName == "In-Schedule")
                {
                    if (billObj.BT_REFERRING_FACILITY == false)
                    {
                        //Remove AjaxPages From Path in "In-Schedule" :- TUSHAR
                        if (_url.Contains("AJAX Pages"))
                        {
                            objChild.InnerHtml = objMenu.MenuName;
                            objChild.OnClientClick = "javascript:document.location.href='../Bill_Sys_ScheduleEvent.aspx?Flag=True';";
                            p_objSlideMenu.MenuItems.Add(o);
                        }
                        else
                        {
                            objChild.InnerHtml = objMenu.MenuName;
                            objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ScheduleEvent.aspx?Flag=True';";
                            p_objSlideMenu.MenuItems.Add(o);
                        }
                        //End Of Code
                    }
                    OboutInc.SlideMenu.Child objChild2 = new Child();
                    OboutInc.SlideMenu.SlideMenuItem o2 = objChild2;
                    objChild2.ID = "400";

                    if (billObj.BT_REFERRING_FACILITY == false)
                    {
                        objChild2.InnerHtml = "Out-Schedule";
                    }
                    else
                    {
                        objChild2.InnerHtml = "Schedule";
                    }


                    if (_url.Contains("AJAX Pages"))
                        objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_AppointPatientEntry.aspx?Flag=True';";
                    else
                        objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_AppointPatientEntry.aspx?Flag=True';";



                    ///add for ajax change atul
                    if (_url.Contains("AJAX Pages"))
                        objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_Test_Unbilled_Procedures.aspx?Flag=True';";
                    else
                        objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Test_Unbilled_Procedures.aspx?Flag=True';";

                    if (_url.Contains("AJAX Pages"))
                        objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_Test_ThirtyDaysUnbilledVisits.aspx?Flag=True';";
                    else
                        objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Test_ThirtyDaysUnbilledVisits.aspx?Flag=True';";
                    ////end
                    if (_url.Contains("AJAX Pages"))
                        objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_AppointPatientEntry.aspx?Flag=True';";
                    else
                        objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_AppointPatientEntry.aspx?Flag=True';";


                    p_objSlideMenu.MenuItems.Add(o2);
                }
                else
                {
                    // if (objMenu.MenuName == "User Master" && billUser.SZ_USER_ROLE_NAME != "USR00001")
                    if (objMenu.MenuName == "User Master" && !billUser.SZ_USER_ROLE_NAME.Contains("ADMIN"))
                    {
                        objChild.InnerHtml = "Change Password";
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ChangePasswordMaster.aspx';";
                    }
                    else
                    {
                        if (objMenu.MenuName == "Doctor Master" && billObj.BT_REFERRING_FACILITY == true)
                        {
                            objChild.InnerHtml = "Referring Doctor";
                            if (_url.Contains("AJAX Pages"))
                                objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ReferringDoctor.aspx';";

                            else
                                objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ReferringDoctor.aspx';";

                            // objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ReferringDoctor.aspx';";
                        }
                        else
                        {
                            //  objChild.UrlTitle = objMenu.MenuName;
                            objChild.InnerHtml = objMenu.MenuName;
                            if (objMenu.URL == "javascript:window.open('Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');" || objMenu.URL == "javascript:window.open('TemplateManager/templates.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30');")
                            {
                                //code To check Weathe rAjax Pages Is In Url of Document Manager Or Not:-TUSHAR
                                if (objMenu.URL == "javascript:window.open('Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                    {
                                        objMenu.URL = "javascript:window.open('../Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');";
                                        objChild.OnClientClick = objMenu.URL;
                                    }
                                    else
                                    {
                                        objMenu.URL = "javascript:window.open('Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');";
                                        objChild.OnClientClick = objMenu.URL;
                                    }

                                }
                                //END OF CODE
                                else
                                {
                                    objChild.OnClientClick = objMenu.URL;
                                }
                            }

                            else
                            {
                                objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";

                                if (objMenu.MenuName == "Synaptic Notes")
                                {
                                    string Speciality;
                                    Speciality = GetProcGroupCode(billUser.SZ_USER_ID);
                                    if (Speciality != "SYN" && Speciality != "")
                                    {
                                        FlagSYN = "1";
                                    }
                                }
                                if (objMenu.MenuName == "Accupuncture Notes")
                                {
                                    string Speciality;
                                    Speciality = GetProcGroupCode(billUser.SZ_USER_ID);
                                    if (Speciality != "AC" && Speciality != "")
                                    {
                                        FlagSYN = "1";
                                    }
                                }
                                //To Show PT Notes On Work Area Of Doctor Screen
                                if (objMenu.MenuName == "PT Notes")
                                {
                                    string Speciality;
                                    Speciality = GetProcGroupCode(billUser.SZ_USER_ID);
                                    if (Speciality != "PT" && Speciality != "")
                                    {
                                        FlagSYN = "1";
                                    }
                                }
                                //End

                                //To Show Chiro Notes On Work Area Of Doctor Screen
                                if (objMenu.MenuName == "Chiro Notes")
                                {
                                    string Speciality;
                                    Speciality = GetProcGroupCode(billUser.SZ_USER_ID);
                                    if (Speciality != "CH" && Speciality != "")
                                    {
                                        FlagSYN = "1";
                                    }
                                }
                                //End

                                //To Show LMT On Work Area Of Doctor Screen
                                if (objMenu.MenuName == "LMT Notes")
                                {
                                    string Speciality;
                                    Speciality = GetProcGroupCode(billUser.SZ_USER_ID);
                                    if (Speciality != "LMT" && Speciality != "")
                                    {
                                        FlagSYN = "1";
                                    }
                                }
                                //End

                                if (objMenu.MenuName == "Bill Report" || objMenu.MenuName == "Show Report" || objMenu.MenuName == "Received Report" || objMenu.MenuName == "Transport Report" || objMenu.MenuName == "Payment Repor 1 t By Speciality" || objMenu.MenuName == "Acupuncture Followup" || objMenu.MenuName == "Acupuncture Re Eval" || objMenu.MenuName == "Check out" || objMenu.MenuName == "Completed PT notes" || objMenu.MenuName == "Doctor Master" || objMenu.MenuName == "Provider Master" || objMenu.MenuName == "Referring Facility" || objMenu.MenuName == "Accupuncture Notes" || objMenu.MenuName == "Bill Report For Billing" || objMenu.MenuName == "Synaptic Notes" || objMenu.MenuName == "Only Visit Report" || objMenu.MenuName == "Check out" || objMenu.MenuName == "PT Notes" || objMenu.MenuName == "Chiro Notes" || objMenu.MenuName == "LMT Notes" || objMenu.MenuName == "IMCheck out")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Payment Report by Specialty" || objMenu.MenuName == "Past Medical History" || objMenu.MenuName == "Print POM" || objMenu.MenuName == "Missing Procedure Report" || objMenu.MenuName == "Bill Report By Provider")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Bill Report By Provider" || objMenu.MenuName == "Un Sent NF2 Report" || objMenu.MenuName == "Bill Report Specialty Details" || objMenu.MenuName == "Bill Report By Specialty" || objMenu.MenuName == "40 Days Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Bill Report Details" || objMenu.MenuName == "Bill Report By Doctor" || objMenu.MenuName == "Missing Specialty Report" || objMenu.MenuName == "Follow Up Report" || objMenu.MenuName == "Patient Summary Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "30 Days Un-billed Visit Report" || objMenu.MenuName == "Un-billed Visit Report" || objMenu.MenuName == "Procedure Report" || objMenu.MenuName == "Schedule visits Report" || objMenu.MenuName == "Litigation By Month Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Payment By Month Report" || objMenu.MenuName == "Patient Procedures Report" || objMenu.MenuName == "Unbilled Procedures" || objMenu.MenuName == "Attorney Report" || objMenu.MenuName == "Denial Report" || objMenu.MenuName == "Write Off Desk" || objMenu.MenuName == "Arbitration Desk" || objMenu.MenuName == "Billing Desk")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                //Code To Navigate Patient Visit Summary Report from Ajax Folder
                                if (objMenu.MenuName == "PT Billing Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                //End Of Code

                                //Code To Navigate Patient Visit Summary Report from Ajax Folder
                                if (objMenu.MenuName == "Patient Visit Summary Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Insurance Company Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code


                                //Code To Navigate Patient Entry from Ajax Folder
                                if (objMenu.MenuName == "Patients Entry")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Forms")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code

                                if (objMenu.MenuName == "Doctor Notes")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code

                                //Code To Navigate Schedule Report from Ajax Folder
                                if (objMenu.MenuName == "Schedule Report")
                                {

                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                //End Of Code
                                //Code To Navigate MiscPayment Report from Ajax Folder
                                if (objMenu.MenuName == "Delete Visit")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Misc Payment Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate View Misc Report from Ajax Folder
                                if (objMenu.MenuName == "View Misc Payment")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Invoice from Ajax Folder


                                if (objMenu.MenuName == "Search Invoice")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "?fromCase=False';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "?fromCase=False';";
                                }
                                if (objMenu.MenuName == "Bill Summary")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "?fromCase=False';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "?fromCase=False';";
                                }
                                if (objMenu.MenuName == "Missing Lit-Info Desk")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "?fromCase=False';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "?fromCase=False';";
                                }
                                //End Of Code
                                //Code To Navigate Invoice from Ajax Folder
                                if (objMenu.MenuName == "New Invoice")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                if (objMenu.MenuName == "JFK Billing Company")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "DownloadPackets")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "No Denial Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Unfinalized Visit Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Unfinalized Visit Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "IntakeSheet Provider")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Add Intake Document")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Visit")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Inital-ReEval Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Missing Speciality")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Visit By Speciality")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Todays Visit")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "No Denial Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Visit Reports")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Change Visit Status")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                //Code To Navigate Invoice from Ajax Folder
                                if (objMenu.MenuName == "View Invoice")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Bill Packet Report from Ajax Folder

                                if (objMenu.MenuName == "Email Notification")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "LitigationDesk")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Bill Configuration")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Upload Doc Settings")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Provider Master")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Provider Master")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Associate Diagnosis Codes")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Remove Ajax Pages From Work Area Options:-TUSHAR
                                if (objMenu.MenuName == "Notes" || objMenu.MenuName == "Verification Request" || objMenu.MenuName == "Patient Desk" || objMenu.MenuName == "Report Status" || objMenu.MenuName == "Required Documents" || objMenu.MenuName == "Intake" || objMenu.MenuName == "Lien" || objMenu.MenuName == "Patient-Intake" || objMenu.MenuName == "HIPPA" || objMenu.MenuName == "MRI-Questionaire" || objMenu.MenuName == "AOB" || objMenu.MenuName == "Document Manager" || objMenu.MenuName == "Schedule" || objMenu.MenuName == "New Bill" || objMenu.MenuName == "View Bills" || objMenu.MenuName == "Misc Payment" || objMenu.MenuName == "Assign Procedure" || objMenu.MenuName == "Assign Procedure" || objMenu.MenuName == "In-Schedule" || objMenu.MenuName == "Out-Schedule")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Remove Ajax Pages From Data Entry Options:-TUSHAR
                                if (objMenu.MenuName == "Case Status Master" || objMenu.MenuName == "Case Type Master" || objMenu.MenuName == "Bill Status Master" || objMenu.MenuName == "Specialty" || objMenu.MenuName == "Billing Office Master" || objMenu.MenuName == "Attorney Master" || objMenu.MenuName == "Referring Doctor" || objMenu.MenuName == "Reading Doctor" || objMenu.MenuName == "Billing Doctor" || objMenu.MenuName == "Insurance Company Master" || objMenu.MenuName == "Adjuster" || objMenu.MenuName == "Diagnosis Code Type" || objMenu.MenuName == "Diagnosis Code" || objMenu.MenuName == "User Role" || objMenu.MenuName == "User Master" || objMenu.MenuName == "Assign Menu" || objMenu.MenuName == "Room" || objMenu.MenuName == "Configure Colors" || objMenu.MenuName == "Configure Operation" || objMenu.MenuName == "Transportation Office" || objMenu.MenuName == "Mandatory Fields Settings" || objMenu.MenuName == "Workers Compensation Board" || objMenu.MenuName == "Location" || objMenu.MenuName == "Convert Procedure" || objMenu.MenuName == "Supplies Master" || objMenu.MenuName == "Employer Company Master")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Bill Packet Report from Ajax Folder
                                if (objMenu.MenuName == "Bill Packet Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Bill Packet Report from Ajax Folder
                                if (objMenu.MenuName == "Intake Sheet")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Bill Packet Report from Ajax Folder
                                if (objMenu.MenuName == "Payment Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Attorney Visit Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Assign Users")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //End Of Code
                                //Code To Navigate Download desk from Ajax Folder
                                if (objMenu.MenuName == "Download Desk")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Day View" || objMenu.MenuName == "In-Schedule Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "POM Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Uprocessed Bills Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Doctor Payment Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Associate Payment Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Payment Type Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Billing Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Verification Request")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Insurance Groups")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Unbilled Visits Report" || objMenu.MenuName == "30 Day Unbilled Visits Report" || objMenu.MenuName == "40 Day Unbilled Visits Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Day View" || objMenu.MenuName == "In-Schedule Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Bill Report" || objMenu.MenuName == "Show Report" || objMenu.MenuName == "Received Report" || objMenu.MenuName == "Transport Report" || objMenu.MenuName == "Payment Repor 1 t By Speciality")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Payment Report By Specialty Report" || objMenu.MenuName == "Past Medical History" || objMenu.MenuName == "Print POM Report" || objMenu.MenuName == "Missing Procedure Report" || objMenu.MenuName == "Bill Report By Provider")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Bill Report By Provider" || objMenu.MenuName == "Un Sent NF2 Report" || objMenu.MenuName == "Bill Report Specialty Details" || objMenu.MenuName == "Bill Report By Specialty" || objMenu.MenuName == "40 Days Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Bill Report Details" || objMenu.MenuName == "Bill Report By Doctor" || objMenu.MenuName == "Missing Specialty Report" || objMenu.MenuName == "Follow Up Report" || objMenu.MenuName == "Patient Summary Report")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "30 Days Un-billed Visit Report" || objMenu.MenuName == "Un-billed Visit Report" || objMenu.MenuName == "Procedure Report" || objMenu.MenuName == "Schedule visits" || objMenu.MenuName == "Litigation By Month")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Payment By Month" || objMenu.MenuName == "Patient Procedures" || objMenu.MenuName == "Unbilled Procedures Report" || objMenu.MenuName == "Procedure Group")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "MG2")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Add Manual Visit")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                if (objMenu.MenuName == "Add Manual Zwanger Visit")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //

                                if (objMenu.MenuName == "Bill Search")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }

                                if (objMenu.MenuName == "Forms")
                                {
                                    if (_url.Contains("AJAX Pages"))
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                                }
                                //if (objMenu.MenuName == "Dash Board" || objMenu.MenuName == "Search Case" || objMenu.MenuName == "Bill Search" || objMenu.MenuName == "Contact Search" || objMenu.MenuName == "Hard Delete" || objMenu.MenuName == "Add Patient" || objMenu.MenuName == "Add Visits")
                                //{
                                //    if (_url.Contains("AJAX Pages"))
                                //        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";
                                //    else
                                //        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                //}
                            }
                        }
                        if (objMenu.MenuName == "Day View" && billObj.BT_REFERRING_FACILITY == false)
                        {
                            objChild.InnerHtml = "Out-Schedule";
                        }

                        if (objMenu.MenuName == "Out-Schedule Report" && billObj.BT_REFERRING_FACILITY == true)
                        {
                            objChild.InnerHtml = "Schedule Report";
                        }

                    }
                    if (objMenu.MenuName == "Room" || objMenu.MenuName == "Reading Doctor" || objMenu.MenuName == "Billing Doctor" || objMenu.MenuName == "Billing Office Master")
                    {
                        if (billObj.BT_REFERRING_FACILITY == true)
                        {
                            p_objSlideMenu.MenuItems.Add(o);
                        }
                    }
                    else
                    {
                        if ((objMenu.MenuName == "Referring Facility" || objMenu.MenuName == "Procedure Group" || objMenu.MenuName == "In-Schedule Report") && billObj.BT_REFERRING_FACILITY == true)
                        {

                        }
                        else
                        {
                            if (objMenu.MenuName == "Visit Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            if (objMenu.MenuName == "Dash Board")
                            {
                                if (_url.ToLower().Contains("ajax pages"))
                                {
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                }

                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            //code 1: Add DocumentType page in the ajax pages folder  - anand 17 may 2010
                            //code 1: Add DocumentType page in the ajax pages folder  - anand 17 may 2010
                            if (objMenu.MenuName == "Document Type")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Procedure Master")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Packet Document")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Transfer Document")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Missing Doc Settings")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Template Manager")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Configure Template")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }

                            else if (objMenu.MenuName == "Billing Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Complaint Master")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "90 Days Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Verification Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }

                            else if (objMenu.MenuName == "Menu Reports")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Denial Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Test Schedule")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Verification Print POM Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Patient Absentee Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Change Visits")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "Search Case")
                            {
                                if (_url.ToLower().Contains("ajax pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }

                            else if (objMenu.MenuName == "SYN Billing Report")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }
                            else if (objMenu.MenuName == "System Settings")
                            {
                                if (_url.Contains("AJAX Pages"))
                                    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                else
                                {
                                    string url = _url.Substring(_url.LastIndexOf("/"));
                                    url = _url.Replace(url, "/");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                                }
                            }

                            else
                            {
                                if (_url.Contains("Bill_Sys_DocumentType.aspx"))
                                {
                                    string url = _url.Replace("AJAX Pages/Bill_Sys_DocumentType.aspx", "");
                                    objChild.OnClientClick = "javascript:document.location.href='" + url + objMenu.URL + "';";
                                }
                            }

                            //code 1 : End
                            //code 1 : End
                            if (objMenu.MenuName == "New Bill" || objMenu.MenuName == "MG2")
                            {
                                if (p_objSysSettings.SZ_NEW_BILL == "True")
                                    p_objSlideMenu.MenuItems.Add(o);
                            }

                            //#region "PDF Menu"           
                            else if ((objMenu.MenuName == "Accupenture Re Eval") || (objMenu.MenuName == "Accupenture Followup") || (objMenu.MenuName == "Start Examination") || (objMenu.MenuName == "Examination Section") || (objMenu.MenuName == "Test Result") || (objMenu.MenuName == "Plain") || (objMenu.MenuName == "Doctor opinion") || (objMenu.MenuName == "Return to work") || (objMenu.MenuName == "Patient Iillness") || (objMenu.MenuName == "Patient Current Complaints") || (objMenu.MenuName == "Medical History") || (objMenu.MenuName == "Physical Status Part 1") || (objMenu.MenuName == "Physical Status Part 2") || (objMenu.MenuName == "Physical Status Part 3") || (objMenu.MenuName == "NeuroLogical Examination") || (objMenu.MenuName == "Diagnostic Impresstion") || (objMenu.MenuName == "Plan Of Care") || (objMenu.MenuName == "Work Status") || (objMenu.MenuName == "ROM") /*|| (objMenu.MenuName == "Chiro Notes") || (objMenu.MenuName == "PT Notes")*/ || (objMenu.MenuName == "Past Medical History") || (objMenu.MenuName == "TCM Diagnosis") || (objMenu.MenuName == "Initial Impression"))
                            {
                                if (p_objSpecialityDAO != null)
                                {
                                    if (p_objSpecialityDAO.ProcedureGroup == "AC" || p_objSpecialityDAO.ProcedureGroup == "ac")
                                    {
                                        if (p_objSpecialityDAO.VisitType == "IE")
                                        {
                                            if ((objMenu.MenuName == "Accupenture Re Eval") || (objMenu.MenuName == "Past Medical History") || (objMenu.MenuName == "TCM Diagnosis") || (objMenu.MenuName == "Initial Impression"))
                                            {
                                                p_objSlideMenu.MenuItems.Add(o);
                                            }
                                        }
                                        if (p_objSpecialityDAO.VisitType == "FU")
                                        {
                                            if ((objMenu.MenuName == "Accupenture Re Eval") || (objMenu.MenuName == "Accupenture Followup"))
                                            {
                                                p_objSlideMenu.MenuItems.Add(o);
                                            }
                                        }
                                    }
                                    else if (p_objSpecialityDAO.ProcedureGroup == "IM" || p_objSpecialityDAO.ProcedureGroup == "im")
                                    {
                                        if (p_objSpecialityDAO.VisitType == "IE")
                                        {
                                            if ((objMenu.MenuName == "Patient Iillness") || (objMenu.MenuName == "Patient Current Complaints") || (objMenu.MenuName == "Medical History") || (objMenu.MenuName == "Physical Status Part 1") || (objMenu.MenuName == "Physical Status Part 2") || (objMenu.MenuName == "Physical Status Part 3") || (objMenu.MenuName == "NeuroLogical Examination") || (objMenu.MenuName == "Diagnostic Impresstion") || (objMenu.MenuName == "Plan Of Care") || (objMenu.MenuName == "Work Status"))
                                            {
                                                p_objSlideMenu.MenuItems.Add(o);
                                            }
                                        }
                                        else if (p_objSpecialityDAO.VisitType == "FU")
                                        {
                                            if ((objMenu.MenuName == "Start Examination") || (objMenu.MenuName == "Examination Section") || (objMenu.MenuName == "Test Result") || (objMenu.MenuName == "Plain") || (objMenu.MenuName == "Doctor opinion") || (objMenu.MenuName == "Return to work"))
                                            {
                                                p_objSlideMenu.MenuItems.Add(o);
                                            }
                                        }
                                    }
                                    else if ((p_objSpecialityDAO.ProcedureGroup == "ROM") || (p_objSpecialityDAO.ProcedureGroup == "rom"))
                                    {
                                        if ((objMenu.MenuName == "ROM"))
                                        {
                                            p_objSlideMenu.MenuItems.Add(o);
                                        }
                                    }
                                    else if ((p_objSpecialityDAO.ProcedureGroup == "CH") || (p_objSpecialityDAO.ProcedureGroup == "ch"))
                                    {
                                        if (p_objSpecialityDAO.VisitType == "IE")
                                        {
                                            if ((objMenu.MenuName == "Chiro Initial"))
                                            {
                                                p_objSlideMenu.MenuItems.Add(o);
                                            }
                                        }
                                    }
                                    else if ((p_objSpecialityDAO.ProcedureGroup == "PT") || (p_objSpecialityDAO.ProcedureGroup == "pt"))
                                    {
                                        if ((objMenu.MenuName == "PT Notes"))
                                        {
                                            p_objSlideMenu.MenuItems.Add(o);
                                        }
                                    }
                                }
                            }
                            //#endregion
                            else
                            {
                                if (objMenu.MenuName == "Contact Search" || objMenu.MenuName == "Hard Delete" || objMenu.MenuName == "Collection Desk" || objMenu.MenuName == "Patient Visit Status ")
                                {
                                    if (_url.ToLower().Contains("ajax pages"))
                                        objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";

                                    else
                                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";

                                }
                                if (objMenu.URL == "Bill_Sys_MultipleVisits.aspx")
                                {

                                    Bill_Sys_SystemObject strVal = (Bill_Sys_SystemObject)(System.Web.HttpContext.Current.Session["SYSTEM_OBJECT"]);
                                    string flag = strVal.IS_EMPLOYER.ToString();

                                    if (flag == "1")
                                    {
                                        objMenu.URL = "MultipleVisits.aspx";
                                    }
                                }
                                if (objMenu.MenuName == "Add Visits")
                                {
                                    if (true)
                                    {
                                        if (_url.ToLower().Contains("ajax pages"))
                                            objChild.OnClientClick = "javascript:document.location.href= '" + "../" + objMenu.URL + "';";

                                        else
                                            objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                                    }
                                }
                                if (FlagSYN == "0")
                                {
                                    p_objSlideMenu.MenuItems.Add(o);

                                }
                                else
                                {
                                    FlagSYN = "0";
                                }
                            }
                        }

                    }
                }

                if (objMenu.MenuName == "New Bill")
                {
                    OboutInc.SlideMenu.Child objChild2 = new Child();
                    OboutInc.SlideMenu.SlideMenuItem o2 = objChild2;
                    objChild2.ID = "200";

                    OboutInc.SlideMenu.Child objChild3 = new Child();
                    OboutInc.SlideMenu.SlideMenuItem o3 = objChild3;
                    objChild3.ID = "250";

                    //Tushar:- Code For Read Text And Trasnsfer User to New Diagnosys Page
                    if (objMenu.URL == "Bill_Sys_BillTransaction.aspx" || objMenu.URL == "Bill_Sys_ReferralBillTransaction.aspx")
                    {
                        string flag = GetValue(billUser.SZ_USER_ID);
                        if (flag == "True")
                        {
                            objMenu.URL = "Bill_Sys_BillTransaction_NoDiagnosys.aspx";
                        }
                    }

                    //End

                    if (p_objSysSettings.SZ_NEW_BILL == "True")
                    {
                        if (_url.Contains("AJAX Pages"))
                            objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                        else
                        {
                            string url = _url.Substring(_url.LastIndexOf("/"));
                            url = _url.Replace(url, "/");
                            objChild.OnClientClick = "javascript:document.location.href='" + url + "AJAX Pages/" + objMenu.URL + "';";
                        }
                    }
                    //Added "New Bill" Page in Ajax Pages folder..... Ananda Naphade


                    //

                    //  objChild2.UrlTitle = "View Bills";

                    if (p_objSysSettings.SZ_VIEW_BILL == "True")
                    {
                        objChild2.InnerHtml = "View Bills";
                        //To Remove Ajax Pages From Path Of View Bills:-TUSHAR


                        if (_url.Contains("AJAX Pages"))
                        {
                            objChild2.OnClientClick = "javascript:document.location.href='Bill_Sys_BillSearch.aspx?fromCase=true';";
                        }
                        else
                        {
                            objChild2.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_BillSearch.aspx?fromCase=true';";
                        }
                        //End Of Code
                        p_objSlideMenu.MenuItems.Add(o2);

                        //To Add Ajax Pages In Menu Of View Payments:-TUSHAR
                        objChild3.InnerHtml = "View Payments";
                        if (_url.Contains("AJAX Pages"))
                            objChild3.OnClientClick = "javascript:document.location.href='Bill_Sys_NewPaymentReport.aspx?fromCase=true';";
                        else
                        {
                            objChild3.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_NewPaymentReport.aspx?fromCase=true';";
                        }
                        //End Of Code



                        p_objSlideMenu.MenuItems.Add(o3);

                    }
                }

                if (objMenu.MenuName == "Provider Master" && billObj.BT_REFERRING_FACILITY == true)
                {
                    objChild.InnerHtml = "Office Master";
                    //Code To Remove Ajax Folder From Url:-TUSHAR
                    //if (_url.Contains("AJAX Pages"))
                    //{
                    //    objChild.OnClientClick = "javascript:document.location.href='" + "../" + objMenu.URL + "';";
                    //}
                    //else
                    //{
                    //    objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    //}

                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                    }
                    //End Of Code
                }
                if (objMenu.MenuName == "Configure IP")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ConfigureIP.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ConfigureIP.aspx';";
                    }
                }
                if ((objMenu.MenuName == "Pending Reports") && (objMenu.URL == "Bill_Sys_Paid_bills.aspx"))
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='bill_sys_paid_bills.aspx?Flag=report&Type=P';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/bill_sys_paid_bills.aspx?Flag=report&Type=P';";
                    }
                }
                if ((objMenu.MenuName == "Received Report") && (objMenu.URL == "Bill_Sys_ReffPaidBills.aspx"))
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ReffPaidBills.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ReffPaidBills.aspx';";
                    }
                }

                if (objMenu.MenuName == "Denial Reasons Master")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_DenialReasons.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_DenialReasons.aspx';";
                    }
                }



                if (objMenu.MenuName == "Treatment Master")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Treatment_Objective_Master.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Treatment_Objective_Master.aspx';";
                    }
                }
                if (objMenu.MenuName == "Update Reminder")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Reminders.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Reminders.aspx';";
                    }
                }
                if (objMenu.MenuName == "Bill Config")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_BillConfig.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_BillConfig.aspx';";
                    }
                }


                if (objMenu.MenuName == "Associate Documents")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Associate_Documents.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Associate_Documents.aspx';";
                    }
                }

                if (objMenu.MenuName == "Import Visits")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ProcessReport.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ProcessReport.aspx';";
                    }
                }
                if (objMenu.MenuName == "Employer Procedure Master")
                {

                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ProcessReport.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ProcessReport.aspx';";
                    }
                }

                if (objMenu.MenuName == "Verification Master")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='VerificationReason.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/VerificationReason.aspx';";
                    }
                }

                if (objMenu.MenuName == "Transportation Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Transportation.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Transportation.aspx';";
                    }
                }
                if (objMenu.MenuName == "Assign Attorney")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Assign_Attorny.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Assign_Attorny.aspx';";
                    }
                }
                if (objMenu.MenuName == "NF3 Configuration")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_NF3Configuration.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_NF3Configuration.aspx';";
                    }
                }
                if (objMenu.MenuName == "Insurance Adjuster Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Insurance_Adjuster_Report.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Insurance_Adjuster_Report.aspx';";
                    }
                }

                if (objMenu.MenuName == "Software Invoice Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_billinvoice.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_billinvoice.aspx';";
                    }
                }


                if (objMenu.MenuName == "Generate Invoice Report")
                {
                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                }

                if (objMenu.MenuName == "Employer Procedure Master")
                {
                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";
                }
                if (objMenu.MenuName == "Invoice Report")
                {

                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";

                }
                if (objMenu.MenuName == "Employer Invoice")
                {

                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";

                }
                if (objMenu.MenuName == "Invoice Search")
                {

                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";

                }

                if (objMenu.MenuName == "Download Packets Report")
                {

                    if (_url.Contains("AJAX Pages"))
                        objChild.OnClientClick = "javascript:document.location.href='" + objMenu.URL + "';";
                    else
                        objChild.OnClientClick = "javascript:document.location.href='" + "AJAX Pages/" + objMenu.URL + "';";

                }


                if (objMenu.MenuName == "Software Invoice Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Billinvoice_report.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Billinvoice_report.aspx';";
                    }
                }
                if (objMenu.MenuName == "Software Invoice Transaction")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Billinvoice_transaction.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Billinvoice_transaction.aspx';";
                    }
                }

                if (objMenu.MenuName == "Storage Invoice")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Billinvoice_Storage.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Billinvoice_Storage.aspx';";
                    }
                }

                if (objMenu.MenuName == "Storage Invoice Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Billinvoice_Storagereport.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Billinvoice_Storagereport.aspx';";
                    }
                }
                if (objMenu.MenuName == "Bill Count Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_BillCount.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_BillCount.aspx';";
                    }
                }
                if (objMenu.MenuName == "Envelope Configuration")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Envelope_config.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Envelope_config.aspx';";
                    }
                }

                if (objMenu.MenuName == "Add Reminder Type")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Reminder_Type.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Reminder_Type.aspx';";
                    }
                }

                if (objMenu.MenuName == "Notes Info Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_NotesInfo.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_NotesInfo.aspx';";
                    }
                }

                if (objMenu.MenuName == "Add Reminder")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Add_Reminder.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Add_Reminder.aspx';";
                    }
                }
                if (objMenu.MenuName == "Insurance Priority Billing Desk")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_InsurancePriorityBilling.aspx?Flag=report&Type=P';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_InsurancePriorityBilling.aspx?Flag=report&Type=P';";
                    }
                }
                if (objMenu.MenuName == "Reconciliation Desk")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='ReconcilationDesk.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/ReconcilationDesk.aspx';";
                    }
                }
                if (objMenu.MenuName == "Paper Authorization desk")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_PaperAuthorizationDesk.aspx?Flag=report&Type=P';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_PaperAuthorizationDesk.aspx?Flag=report&Type=P';";
                    }
                }

                if (objMenu.MenuName == "Bills Summary")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Billing_Summary.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Billing_Summary.aspx';";
                    }
                }

                if (objMenu.MenuName == "Exclude Billing")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Excluding_Billing.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Excluding_Billing.aspx';";
                    }
                }

                if (objMenu.MenuName == "Add PreAuthorisation")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Add_PreAuth.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Add_PreAuth.aspx';";
                    }
                }


                if (objMenu.MenuName == "Transfer Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Transfer_Report.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Transfer_Report.aspx';";
                    }
                }

                if (objMenu.MenuName == "Attach Reffering Provider")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Reffering_Provider_Visit.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Reffering_Provider_Visit.aspx';";
                    }
                }

                if (objMenu.MenuName == "Notes Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Notes_Report.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Notes_Report.aspx';";
                    }
                }

                if (objMenu.MenuName == "Balance Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Balance_Report.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Balance_Report.aspx';";
                    }
                }

                if (objMenu.MenuName == "Reffering Office")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_RefferingOffice.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_RefferingOffice.aspx';";
                    }
                }

                if (objMenu.MenuName == "Reffering Doctor")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_RefferingDOCBilling.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_RefferingDOCBilling.aspx';";
                    }
                }

                if (objMenu.MenuName == "Modifier Master")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Modifier.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Modifier.aspx';";
                    }
                }

                if (objMenu.MenuName == "Initial Followup Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Initial_followupReport.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Initial_followupReport.aspx';";
                    }
                }

                if (objMenu.MenuName == "Mandatory Doctor Setting")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Mandatory_Doctor_Setting.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Mandatory_Doctor_Setting.aspx';";
                    }
                }

                if (objMenu.MenuName == "NF2 Config Setting")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Nf2_Config_Setting.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Nf2_Config_Setting.aspx';";
                    }
                }

                if (objMenu.MenuName == "Case Type Pdf")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Casetype_BillConfig.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Casetype_BillConfig.aspx';";
                    }
                }

                if (objMenu.MenuName == "Document Manager Grid")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:window.open('../AJAX Pages/Bill_Sys_Document_Manager_Popup.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:window.open('../AJAX Pages/Bill_Sys_Document_Manager_Popup.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');";
                    }
                }

                if (objMenu.MenuName == "Configure Outschedule Documents")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='OutScheduleDocumentConfiguration.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/OutScheduleDocumentConfiguration.aspx';";
                    }
                }

                if (objMenu.MenuName == "In-Scheduling")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Calendar.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Calendar.aspx';";
                    }
                }
                if (objMenu.MenuName == "Configure Outschedule Documents")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='OutScheduleDocumentConfiguration.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/OutScheduleDocumentConfiguration.aspx';";
                    }
                }

                ///ADDED FOR Print Delivery Reciept--ATUL
                ///
                if (objMenu.MenuName == "Print Delivery Reciept")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Print_Delivery_Reciept.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Print_Delivery_Reciept.aspx';";
                    }
                }

                if (objMenu.MenuName == "Psy Information")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Psy_Information.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Psy_Information.aspx';";
                    }
                }

                if (objMenu.MenuName == "Collection Report")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_CollectionsReport.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_CollectionsReport.aspx';";
                    }
                }

                if ((objMenu.MenuName == "Doctor Reports") && (objMenu.URL == "Bill_Sys_Doctor_Reports.aspx"))
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Doctor_Reports.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Doctor_Reports.aspx';";
                    }
                }

                if (objMenu.MenuName == "Add Leave")
                {
                    if (_url.Contains("AJAX Pages"))
                    {
                        objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_Doctor_Leave.aspx';";
                    }
                    else
                    {
                        objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_Doctor_Leave.aspx';";
                    }
                }

                if (objMenu.MenuName == "Patient Information")
                {
                    if (billObj.BT_REFERRING_FACILITY == true)
                    {
                        if (_url.Contains("AJAX Pages"))
                        {
                            objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_ReCaseDetails.aspx';";


                        }
                        else
                        {
                            objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_ReCaseDetails.aspx';";
                        }
                    }
                    else
                    {

                        if (_url.Contains("AJAX Pages"))
                        {
                            objChild.OnClientClick = "javascript:document.location.href='Bill_Sys_CaseDetails.aspx';";


                        }
                        else
                        {
                            objChild.OnClientClick = "javascript:document.location.href='AJAX Pages/Bill_Sys_CaseDetails.aspx';";
                        }
                    }
                }






            }
            //   }

        }
        menuflag = false;
    }

    protected string GetProcGroupCode(string ProceGroupID)
    {
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        ds = new DataSet();
        string ProcGroupCode = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_GET_PROCEDURE_GROUP_CODE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", ProceGroupID);
            comm.Parameters.AddWithValue("@FLAG", "USERID");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables.Count >= 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ProcGroupCode = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return ProcGroupCode;

    }

    public string GetValue(string p_szUserID)
    {
        DataTable dt = new DataTable();
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_USER_DIAGNOSYS_PAGE_FLAG";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_szUserID);
            comm.Parameters.AddWithValue("@FLAG", "SELECT");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(dt);
            conn.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dt.Rows[0][0].ToString();
    }
}

public class OptionMenuDataAccess
{
    private string szConnectionString = "";

    public OptionMenuDataAccess()
    {
        szConnectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public ArrayList getOptionsMenu(DAO_User p_objUser)
    {
        SqlConnection.ClearAllPools();
        SqlConnection conn = new SqlConnection(szConnectionString);
        SqlCommand comm;
        SqlDataReader dr = null;

        conn.Open();
        comm = new SqlCommand();
        comm.CommandText = "SP_GET_OPTIONS_MENU";
        comm.CommandType = CommandType.StoredProcedure;
        comm.Connection = conn;
        comm.Parameters.AddWithValue("@I_PARENT_ID", p_objUser.UserID);
        comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_objUser.UserRoleID);
        dr = comm.ExecuteReader();

        ArrayList objArray = new ArrayList();
        DAO_OptionMenu objMenu = null;


        while (dr.Read())
        {
            objMenu = new DAO_OptionMenu();
            objMenu.MenuID = dr["i_menu_id"].ToString();
            objMenu.MenuName = dr["sz_menu_name"].ToString();
            objMenu.URL = dr["sz_menu_link"].ToString();
            if (dr["i_parent_id"].Equals(System.DBNull.Value))
            {
                objMenu.isParent = true;
            }
            else
            {
                objMenu.isParent = false;
            }
            objArray.Add(objMenu);
        }


        conn.Close();
        return objArray;
    }


    public DataTable GetAssignedMenu(string p_szUserRoleID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection.ClearAllPools();
            SqlConnection conn = new SqlConnection(szConnectionString);
            SqlCommand comm;
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_USER_ACCESS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_szUserRoleID);
            comm.Parameters.AddWithValue("@FLAG", "GETASSIGNEDMENU");

            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);


            conn.Close();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dt;
    }

}