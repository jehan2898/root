using java.io;
using java.util;
using org.apache.pdfbox.io;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.common.filespecification;
using org.apache.pdfbox.pdmodel.interactive.annotation;
using System;
using System.Collections;
using Console = System.Console;
namespace Aquaforest.PDF
{
	internal class PDFExtractEmbeddedFiles
	{
		public PDFExtractEmbeddedFiles()
		{
		}

		internal static void ExtractAttachment(PDFDocument pdfDoc, string OutputPath)
		{
			try
			{
				string filePath = pdfDoc.FilePath;
				PDDocument pDFBoxDocument = pdfDoc.PDFBoxDocument;
				PDEmbeddedFilesNameTreeNode embeddedFiles = (new PDDocumentNameDictionary(pDFBoxDocument.getDocumentCatalog())).getEmbeddedFiles();
				if (embeddedFiles != null)
				{
					Map names = embeddedFiles.getNames();
					if (names == null)
					{
						object[] objArray = embeddedFiles.getKids().toArray();
						for (int i = 0; i < (int)objArray.Length; i++)
						{
							names = ((PDNameTreeNode)objArray[i]).getNames();
							PDFExtractEmbeddedFiles.extractFiles(names, OutputPath);
						}
					}
					else
					{
						PDFExtractEmbeddedFiles.extractFiles(names, OutputPath);
					}
				}
				foreach (PDPage page in pDFBoxDocument.getPages())
				{
					object[] objArray1 = page.getAnnotations().toArray();
					for (int j = 0; j < (int)objArray1.Length; j++)
					{
						PDAnnotation pDAnnotation = (PDAnnotation)objArray1[j];
						if (pDAnnotation is PDAnnotationFileAttachment)
						{
							PDComplexFileSpecification file = (PDComplexFileSpecification)((PDAnnotationFileAttachment)pDAnnotation).getFile();
							PDEmbeddedFile embeddedFile = PDFExtractEmbeddedFiles.getEmbeddedFile(file);
							PDFExtractEmbeddedFiles.extractFile(filePath, file.getFilename(), embeddedFile);
						}
					}
				}
			}
			finally
			{
			}
		}

		private static void extractFile(string filePath, string filename, PDEmbeddedFile embeddedFile)
		{
			string str = string.Concat(filePath, filename);
			File file = new File(string.Concat(filePath, filename));
			Console.WriteLine(string.Concat("Writing ", str));
			FileOutputStream fileOutputStream = null;
			try
			{
				fileOutputStream = new FileOutputStream(file);
				fileOutputStream.write(embeddedFile.toByteArray());
			}
			finally
			{
				IOUtils.closeQuietly(fileOutputStream);
			}
		}

		private static void extractFiles(Map names, string filePath)
		{
			object[] objArray = names.entrySet().toArray();
			for (int i = 0; i < (int)objArray.Length; i++)
			{
				PDComplexFileSpecification value = (PDComplexFileSpecification)((Map.Entry)objArray[i]).getValue();
				string filename = value.getFilename();
				PDFExtractEmbeddedFiles.extractFile(filePath, filename, PDFExtractEmbeddedFiles.getEmbeddedFile(value));
			}
		}

		private static PDEmbeddedFile getEmbeddedFile(PDComplexFileSpecification fileSpec)
		{
			PDEmbeddedFile embeddedFileUnicode = null;
			if (fileSpec != null)
			{
				embeddedFileUnicode = fileSpec.getEmbeddedFileUnicode() ?? fileSpec.getEmbeddedFileDos() ?? fileSpec.getEmbeddedFileMac() ?? fileSpec.getEmbeddedFileUnix() ?? fileSpec.getEmbeddedFile();
			}
			return embeddedFileUnicode;
		}
	}
}