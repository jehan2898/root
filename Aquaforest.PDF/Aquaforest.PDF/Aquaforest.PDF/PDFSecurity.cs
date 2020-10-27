using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.encryption;
using System;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFSecurity
	{
		public string OwnerPassword
		{
			get;
			set;
		}

		public PDFPermission Permission
		{
			get;
			set;
		}

		public string UserPassword
		{
			get;
			set;
		}

		public PDFSecurity()
		{
			this.Permission = new PDFPermission();
		}

		public PDFPermission ReadPDFSecurity(PDFDocument sourcePDF)
		{
			PDFPermission pDFPermission;
			PDFHelper.DisplayTrialPopupIfNecessary();
			try
			{
				PDFPermission pDFPermission1 = null;
				AccessPermission currentAccessPermission = sourcePDF.PDFBoxDocument.getCurrentAccessPermission();
				if (currentAccessPermission != null)
				{
					pDFPermission1 = new PDFPermission()
					{
						AllowAssembly = currentAccessPermission.canAssembleDocument(),
						AllowDegradedPrinting = currentAccessPermission.canPrintDegraded(),
						AllowExtractContents = currentAccessPermission.canExtractContent(),
						AllowExtractForAccessibility = currentAccessPermission.canExtractForAccessibility(),
						AllowFillInForm = currentAccessPermission.canFillInForm(),
						AllowModifyAnnotations = currentAccessPermission.canModifyAnnotations(),
						AllowModifyContents = currentAccessPermission.canModifyAnnotations(),
						AllowPrinting = currentAccessPermission.canPrint()
					};
				}
				pDFPermission = pDFPermission1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return pDFPermission;
		}

		public void SecurePDF(PDFDocument sourcePDF, string outputFile)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			try
			{
				AccessPermission accessPermission = new AccessPermission();
				accessPermission.setCanAssembleDocument(this.Permission.AllowAssembly);
				accessPermission.setCanExtractContent(this.Permission.AllowExtractContents);
				accessPermission.setCanExtractForAccessibility(this.Permission.AllowExtractForAccessibility);
				accessPermission.setCanFillInForm(this.Permission.AllowFillInForm);
				accessPermission.setCanModify(this.Permission.AllowModifyContents);
				accessPermission.setCanModifyAnnotations(this.Permission.AllowModifyAnnotations);
				accessPermission.setCanPrint(this.Permission.AllowPrinting);
				accessPermission.setCanPrintDegraded(this.Permission.AllowDegradedPrinting);
				StandardProtectionPolicy standardProtectionPolicy = new StandardProtectionPolicy(this.OwnerPassword, this.UserPassword, accessPermission);
				if (!PDFHelper.AddStamp)
				{
					sourcePDF.PDFBoxDocument.protect(standardProtectionPolicy);
					sourcePDF.PDFBoxDocument.save(outputFile);
					sourcePDF.PDFBoxDocument.close();
				}
				else
				{
					PDDocument pDFBoxDocument = sourcePDF.PDFBoxDocument;
					pDFBoxDocument = PDFHelper.AddTrialStampIfNecessary(pDFBoxDocument);
					pDFBoxDocument.protect(standardProtectionPolicy);
					pDFBoxDocument.save(outputFile);
					pDFBoxDocument.close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
		}
	}
}