/***********************************************************
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Registration.aspx.cs
/*Purpose              :       To enter check number and interest for each cost
/*Author               :       Sandeep.D
/*Date of creation     :       12 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using System.Diagnostics;
using EmailHelper;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Text;

public partial class Bill_Sys_Registration : PageBase
{

    private SaveOperation _saveOperation;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnRegister.Attributes.Add("onclick", "return formValidator('frmBill_Sys_Registration','txtUserName,txtNewPassword,txtConfirmPassword,txtCompanyName,txtContFirstName,txtContLastName,txtContAdminEmailID');");
           // extddlScheme.Attributes.Add("onChange", "MakeChange();");
            
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        _saveOperation = new SaveOperation();
        try
        {
            Page.Validate();
            if (Page.IsValid == true)
            {
                txtNewGUID.Text = GenerateGUID();
                txtPassword.Text = txtNewPassword.Text;
                txtUserId.Text = txtCompanyName.Text.Substring(1, 4) + GeneratePassword().Substring(1, 4);
                txtEncryptedPassword.Text = EncryptPassword(txtPassword.Text);
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "BillingCompanyRegistartion.xml";
                txtUserName.Text = txtUserName.Text.Trim().Replace(" ", "");
               // txtPassword.Text = txtNewPassword.Text;
                _saveOperation.SaveMethod();
                lblError.Visible = true;
                lblError.Text = "Thank you registering.<br/>Click <a href='../Bill_Sys_Login.aspx'> here </a> to go back to the login page to start using the website.";

             //   if (SendEmail(GenerateLINK(txtNewGUID.Text.ToString())) == true)
            //    {
            //        Response.Redirect("Bill_Sys_Confirmation.aspx", false);
            //    }
            }
}
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            lblError.Text = "Error : " + strError + "<br/> Stack Trace : " + ex.StackTrace.ToString();
            lblError.Visible = true;
            
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtCompanyCity.Text = "";
            txtCompanyEmailID.Text = "";
            txtCompanyName.Text = "";
            txtCompanyPhoneNo.Text = "";
            txtCompanyState.Text = "";
            txtCompanyStreet.Text = "";
            txtCompanyZip.Text = "";
            txtContAdminEmailID.Text = "";
            txtContEmail.Text = "";
            txtContFirstName.Text = "";
            txtContLastName.Text = "";
            txtContOfficeExt.Text = "";
            txtContOfficePhone.Text = "";
            extddlScheme.SelectedValue = "NA";
            Session["BillingCompanyID"] = "";
            lblError.Visible = false;
          
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    
    private string GenerateGUID()
    {
        try
        {
            string _newGUID="";
            _newGUID=Guid.NewGuid().ToString();
            return _newGUID;
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            return null;
        }
    }

    private string GenerateLINK(string _guID)
    {
        try
        {
            string _newLink = "";
            _newLink = ConfigurationSettings.AppSettings["URL"].ToString()+ "?name=" +_guID.ToString();            
            return _newLink;
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            lblError.Text = "Error : " + strError + "<br/> Stack Trace : " + ex.StackTrace.ToString();
            lblError.Visible = true;
            return "jkhjkh";
        }
    }

    private bool SendEmail(string newLink)
    {
        try
        {
                EmailEntity emailEntity =new EmailEntity();
                EmailOperation emailOperation = new EmailOperation();

                emailEntity.SmtpServer = ConfigurationSettings.AppSettings["SMTPServer"].ToString();
                emailEntity.From = ConfigurationSettings.AppSettings["FromMailID"].ToString();
                emailEntity.ToEmail = txtContAdminEmailID.Text.ToString();
                emailEntity.Password = ConfigurationSettings.AppSettings["Password"].ToString();
                emailEntity.Subject = "Your account details for - live.lawallies.com";
                emailEntity.Body = "Dear user, <br/> Please use the link below to login to the websitel live.lawllies.com . <br/> " + newLink.ToString() + " <br/> User Id=" + txtUserName.Text.ToString().Trim() + " <br/> Password=" + txtNewPassword.Text.ToString();
                emailOperation.SenEmail(emailEntity);
                return true;
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            lblError.Text = "Error : " + strError + "<br/> Stack Trace : " + ex.StackTrace.ToString();
            lblError.Visible = true;
            return false;
        }
    }

    private string EncryptPassword(string Password)
    {
        string strPassPhrase = "Pas5pr@se";        // can be any string
        string strSaltValue = "s@1tValue";			// can be any string
        string strHashAlgorithm = "SHA1";			// can be "MD5"
        int intPasswordIterations = 2;             // can be any number
        string strInitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        int intKeySize = 256;
        return Bill_Sys_EncryDecry.Encrypt(Password, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
    }

    private string GeneratePassword()
    {
        try
        {
           
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            return null;
        }
    }

    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    //protected void extddlScheme_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (extddlScheme.Text == "PS00001")
    //        {
    //            lblnoOfUser.Visible = true;
    //            txtnoOfUser.Visible = true;
    //            lblDescription.Visible = true;
    //            lblDescription.Text = "Charges is as per no of users";
    //        }
    //        else if (extddlScheme.Text == "PS00002")
    //        {
    //            lblnoOfUser.Visible = false;
    //            txtnoOfUser.Visible = false;
    //            lblDescription.Visible = true;
    //            txtnoOfUser.Text = "50";
    //            lblDescription.Text = "Charges is as per billing company";
    //        }
    //        else
    //        {
    //            lblnoOfUser.Visible = false;
    //            txtnoOfUser.Visible = false;
    //            lblDescription.Visible = false;
    //            txtnoOfUser.Text = "";
    //            lblDescription.Text = "";
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}

    protected void lnkbtnRegBillComp_Click(object sender, EventArgs e)
    {
        try
        {
            lblHeading.Text = "Billing Company Details";
            extddlScheme.Visible = true;
            lblScheme.Visible = true;
            txtReferringFacility.Text = "0";
            txtLawFirm.Text = "0";
            imgRegisterBillCompany.Style.Add("border-style", "solid");
            imgReferringFacility.Style.Add("border-style", "none");
            imgLawFirm.Style.Add("border-style", "none");
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

    protected void lnkbtnRegReferringFacility_Click(object sender, EventArgs e)
    {
        try
        {
            lblHeading.Text = "Referring Facility Details";
            extddlScheme.Visible = true;
            lblScheme.Visible = true;
            txtReferringFacility.Text = "1";
            txtLawFirm.Text = "0";
            imgRegisterBillCompany.Style.Add("border-style", "none");
            imgReferringFacility.Style.Add("border-style", "solid");
            imgLawFirm.Style.Add("border-style", "none");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkbtnLawFirm_Click(object sender, EventArgs e)
    {
        try
        {
            lblHeading.Text = "Law Firm Details";
            extddlScheme.Visible = false;
            lblScheme.Visible = false;
            //lblnoOfUser.Visible = false;
            //txtnoOfUser.Visible = false;
            //lblDescription.Visible = false;
            txtLawFirm.Text = "1";
            txtReferringFacility.Text = "0";
            imgRegisterBillCompany.Style.Add("border-style", "none");
            imgReferringFacility.Style.Add("border-style", "none");
            imgLawFirm.Style.Add("border-style", "solid");
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

}
