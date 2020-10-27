using java.io;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.encryption;
using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using Console = System.Console;
namespace Aquaforest.PDF
{
	public class PDFValidator : IDisposable
	{
		public string ErrorMessage
		{
			get;
			set;
		}

		public bool IsPasswordProtected
		{
			get;
			set;
		}

		public bool IsValid
		{
			get;
			set;
		}

		public int NumberOfPages
		{
			get;
			set;
		}

		public int NumberOfPagesDict
		{
			get;
			set;
		}

		public PDFValidator(string fileName)
		{
			try
			{
				this.IsValid = true;
				if (!System.IO.File.Exists(fileName))
				{
					Console.WriteLine("The PDF file does not Exist.");
				}
				else
				{
					PDDocument pDDocument = PDDocument.load(new java.io.File(fileName));
					this.CheckAllPages(pDDocument);
					if (pDDocument != null)
					{
						pDDocument.close();
					}
				}
			}
			catch (InvalidPasswordException invalidPasswordException)
			{
				this.IsPasswordProtected = true;
				this.IsValid = false;
			}
			catch (Exception exception)
			{
				this.ErrorMessage = string.Format("PDF analysis failed With exception {0}", exception.Message);
				this.IsValid = false;
			}
		}

		public PDFValidator(string fileName, string password)
		{
			try
			{
				this.IsValid = true;
				if (!System.IO.File.Exists(fileName))
				{
					Console.WriteLine("The PDF file does not Exist.");
				}
				else
				{
					PDDocument pDDocument = null;
					pDDocument = (!string.IsNullOrEmpty(password) ? PDDocument.load(new java.io.File(fileName), password) : PDDocument.load(new java.io.File(fileName)));
					if (pDDocument.isEncrypted())
					{
						this.IsPasswordProtected = true;
					}
					this.CheckAllPages(pDDocument);
					if (pDDocument != null)
					{
						pDDocument.close();
					}
				}
			}
			catch (InvalidPasswordException invalidPasswordException)
			{
				this.IsPasswordProtected = true;
				this.IsValid = false;
			}
			catch (Exception exception)
			{
				this.ErrorMessage = string.Format("PDF analysis failed With exception {0}", exception.Message);
				this.IsValid = false;
			}
		}

		private void CheckAllPages(PDDocument doc)
		{
			int num = 0;
			try
			{
				this.NumberOfPagesDict = doc.getNumberOfPages();
				foreach (PDPage page in doc.getPages())
				{
					if (page.getMediaBox() == null)
					{
						this.ErrorMessage = string.Format("Page number {0} has no media box", num);
						this.IsValid = false;
					}
					if (page.getResources() == null)
					{
						this.ErrorMessage = string.Format("Page number {0}, has no page resources", num);
						this.IsValid = false;
					}
					num++;
				}
				if (this.NumberOfPagesDict != num)
				{
					this.ErrorMessage = string.Format("Page Number Mismatch between dictionary and actual document", new object[0]);
					this.IsValid = false;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.ErrorMessage = string.Format("PDF analysis failed on page number {0},\nWith exception {1}", num, exception.Message);
				this.IsValid = false;
			}
		}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}