using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SIGPLUSLib;

/// <summary>
/// Summary description for DigitalSign
/// </summary>
public class DigitalSign
{
    System.Drawing.Image img;
	public DigitalSign()
	{
		//
		// TODO: Add constructor logic here
        
		//
	}
    public bool SignSave(string imagePath, string PatientPath)
   {
        try
        {
            if (imagePath == null)
            {
                return false;
            }
            SIGPLUSLib.SigPlus sigObj_Patient = new SIGPLUSLib.SigPlus();
            sigObj_Patient.InitSigPlus();
            sigObj_Patient.AutoKeyStart();
            sigObj_Patient.AutoKeyFinish();
            sigObj_Patient.SigCompressionMode = 1;
            sigObj_Patient.EncryptionMode = 2;
            sigObj_Patient.SigString = imagePath;
            if (sigObj_Patient.NumberOfTabletPoints() > 0)
            {
                sigObj_Patient.ImageFileFormat = 0;
                sigObj_Patient.ImageXSize = 150;
                sigObj_Patient.ImageYSize = 75;
                sigObj_Patient.ImagePenWidth = 8;
                sigObj_Patient.SetAntiAliasParameters(1, 600, 700);
                sigObj_Patient.JustifyX = 5;
                sigObj_Patient.JustifyY = 5;
                sigObj_Patient.JustifyMode = 5;
                long size;
                byte[] byteValue;
                sigObj_Patient.BitMapBufferWrite();
                size = sigObj_Patient.BitMapBufferSize();
                byteValue = new byte[size];
                byteValue = (byte[])sigObj_Patient.GetBitmapBufferBytes();
                sigObj_Patient.BitMapBufferClose();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(byteValue);
                if (ms.Length < 1024)
                {
                    return false;
                }
                img = System.Drawing.Image.FromStream(ms);
                img.Save(PatientPath, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return false;
        }
   }
    

}
