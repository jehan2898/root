using java.io;
using org.apache.pdfbox.multipdf;
using org.apache.pdfbox.pdmodel;
using System;
using System.Collections.Generic;
using System.IO;
using Console = System.Console;
namespace Aquaforest.PDF
{
	public class PDFMerger
	{
		private PDFMergerUtility mergeUtility;

		private string outputFolder = "";

		public PDFMerger()
		{
			this.mergeUtility = new PDFMergerUtility();
		}

		private bool CreateDirectory(string path)
		{
			bool flag = true;
			if (!Directory.Exists(path))
			{
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception exception)
				{
					Console.WriteLine("Could not create directory: '{0}'.{1}{2}", path, Environment.NewLine, exception.Message);
					flag = false;
				}
			}
			return flag;
		}

		private bool DirectoryExists(string path)
		{
			bool flag;
			if (!Directory.Exists(path))
			{
				Console.WriteLine("The specified source folder does not exist");
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private void GetSubFolders(DirectoryInfo sourceDirectoryInfo)
		{
			DirectoryInfo[] directories = sourceDirectoryInfo.GetDirectories();
			for (int i = 0; i < (int)directories.Length; i++)
			{
				this.GetSubFolders(directories[i]);
			}
			this.mergeUtility = new PDFMergerUtility();
			FileInfo[] files = sourceDirectoryInfo.GetFiles("*.pdf");
			for (int j = 0; j < (int)files.Length; j++)
			{
				FileInfo fileInfo = files[j];
				this.mergeUtility.addSource(fileInfo.FullName);
			}
			if (!PDFHelper.AddStamp)
			{
				this.mergeUtility.setDestinationFileName(Path.Combine(this.outputFolder, string.Concat(sourceDirectoryInfo.Name, ".pdf")));
				this.mergeUtility.mergeDocuments();
			}
			else
			{
				string str = Path.Combine(Path.GetTempPath(), string.Concat("aquaforest\\pdftoolkit\\", Path.GetRandomFileName(), ".pdf"));
				this.mergeUtility.setDestinationFileName(str);
				this.mergeUtility.mergeDocuments();
				PDDocument pDDocument = PDDocument.load(new java.io.File(str));
				pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				pDDocument.save(Path.Combine(this.outputFolder, string.Concat(sourceDirectoryInfo.Name, ".pdf")));
				if (System.IO.File.Exists(str))
				{
					System.IO.File.Delete(str);
				}
			}
		}

		public void MergeEachFolderInFolderTree(string sourceFolder, string outputFolder)
		{
			if (this.DirectoryExists(sourceFolder))
			{
				if (this.CreateDirectory(outputFolder))
				{
					this.outputFolder = outputFolder;
					DirectoryInfo directoryInfo = new DirectoryInfo(sourceFolder);
					PDFHelper.DisplayTrialPopupIfNecessary();
					this.GetSubFolders(directoryInfo);
				}
			}
		}

		public void MergeFolderOfPDFs(string sourceFolder, string outputFile, bool processSubFolders)
		{
			FileInfo[] fileInfoArray;
			PDFHelper.DisplayTrialPopupIfNecessary();
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceFolder);
			fileInfoArray = (!processSubFolders ? directoryInfo.GetFiles("*.pdf") : directoryInfo.GetFiles("*.pdf", SearchOption.AllDirectories));
			FileInfo[] fileInfoArray1 = fileInfoArray;
			for (int i = 0; i < (int)fileInfoArray1.Length; i++)
			{
				FileInfo fileInfo = fileInfoArray1[i];
				this.mergeUtility.addSource(fileInfo.FullName);
			}
			if (!PDFHelper.AddStamp)
			{
				this.mergeUtility.setDestinationFileName(outputFile);
				this.mergeUtility.mergeDocuments();
			}
			else
			{
				string str = Path.Combine(Path.GetTempPath(), string.Concat("aquaforest\\pdftoolkit\\", Path.GetRandomFileName(), ".pdf"));
				this.mergeUtility.setDestinationFileName(str);
				this.mergeUtility.mergeDocuments();
				PDDocument pDDocument = PDDocument.load(new java.io.File(str));
				pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				pDDocument.save(outputFile);
				if (pDDocument != null)
				{
					pDDocument.close();
				}
				if (System.IO.File.Exists(str))
				{
					System.IO.File.Delete(str);
				}
			}
		}

		public void MergePDFs(List<string> sourcePDFs, string outputFile)
		{
			PDFHelper.DisplayTrialPopupIfNecessary();
			foreach (string sourcePDF in sourcePDFs)
			{
				this.mergeUtility.addSource(sourcePDF);
			}
			if (!PDFHelper.AddStamp)
			{
				this.mergeUtility.setDestinationFileName(outputFile);
				this.mergeUtility.mergeDocuments();
			}
			else
			{
				string str = Path.Combine(Path.GetTempPath(), string.Concat(Path.GetRandomFileName(), ".pdf"));
				this.mergeUtility.setDestinationFileName(str);
				this.mergeUtility.mergeDocuments();
				PDDocument pDDocument = PDDocument.load(new java.io.File(str));
				pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				pDDocument.save(outputFile);
				if (pDDocument != null)
				{
					pDDocument.close();
				}
				if (System.IO.File.Exists(str))
				{
					System.IO.File.Delete(str);
				}
			}
		}
	}
}