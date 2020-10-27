using java.io;
using java.nio.file;
using java.util;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.common;
using org.apache.pdfbox.pdmodel.common.filespecification;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aquaforest.PDF
{
	public class PDFAttachmentItem
	{
		private string filePath = string.Empty;

		public PDFAttachmentItem(string filePath)
		{
			if (!System.IO.File.Exists(filePath))
			{
				throw new Exception("File Not Found");
			}
			this.filePath = filePath;
		}

		internal static bool EmbedPDFAttachment(PDFAttachmentItem attachment, PDDocument doc)
		{
			bool flag;
			try
			{
				PDEmbeddedFilesNameTreeNode pDEmbeddedFilesNameTreeNode = new PDEmbeddedFilesNameTreeNode();
				List arrayList = new ArrayList();
				PDComplexFileSpecification pDComplexFileSpecification = new PDComplexFileSpecification();
				pDComplexFileSpecification.setFile(System.IO.Path.GetFileName(attachment.filePath));
				java.io.File file = new java.io.File(attachment.filePath);
				byte[] numArray = Files.readAllBytes(file.toPath());
				PDEmbeddedFile pDEmbeddedFile = new PDEmbeddedFile(doc, new ByteArrayInputStream(numArray));
				pDEmbeddedFile.setSize((int)numArray.Length);
				pDEmbeddedFile.setCreationDate(new GregorianCalendar());
				pDComplexFileSpecification.setEmbeddedFile(pDEmbeddedFile);
				PDEmbeddedFilesNameTreeNode pDEmbeddedFilesNameTreeNode1 = new PDEmbeddedFilesNameTreeNode();
				pDEmbeddedFilesNameTreeNode1.setNames(Collections.singletonMap("My first attachment", pDComplexFileSpecification));
				arrayList.@add(pDEmbeddedFilesNameTreeNode1);
				pDEmbeddedFilesNameTreeNode.setKids(arrayList);
				PDDocumentNameDictionary pDDocumentNameDictionary = new PDDocumentNameDictionary(doc.getDocumentCatalog());
				pDDocumentNameDictionary.setEmbeddedFiles(pDEmbeddedFilesNameTreeNode);
				doc.getDocumentCatalog().setNames(pDDocumentNameDictionary);
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return flag;
		}

		internal static bool EmbedPDFAttachment(List<PDFAttachmentItem> attachments, PDDocument doc)
		{
			bool flag;
			try
			{
				PDEmbeddedFilesNameTreeNode pDEmbeddedFilesNameTreeNode = new PDEmbeddedFilesNameTreeNode();
				List arrayList = new ArrayList();
				foreach (PDFAttachmentItem attachment in attachments)
				{
					PDComplexFileSpecification pDComplexFileSpecification = new PDComplexFileSpecification();
					pDComplexFileSpecification.setFile(System.IO.Path.GetFileName(attachment.filePath));
					java.io.File file = new java.io.File(attachment.filePath);
					byte[] numArray = Files.readAllBytes(file.toPath());
					PDEmbeddedFile pDEmbeddedFile = new PDEmbeddedFile(doc, new ByteArrayInputStream(numArray));
					pDEmbeddedFile.setSize((int)numArray.Length);
					pDEmbeddedFile.setCreationDate(new GregorianCalendar());
					pDComplexFileSpecification.setEmbeddedFile(pDEmbeddedFile);
					PDEmbeddedFilesNameTreeNode pDEmbeddedFilesNameTreeNode1 = new PDEmbeddedFilesNameTreeNode();
					pDEmbeddedFilesNameTreeNode1.setNames(Collections.singletonMap(attachment.filePath, pDComplexFileSpecification));
					arrayList.@add(pDEmbeddedFilesNameTreeNode1);
				}
				pDEmbeddedFilesNameTreeNode.setKids(arrayList);
				PDDocumentNameDictionary pDDocumentNameDictionary = new PDDocumentNameDictionary(doc.getDocumentCatalog());
				pDDocumentNameDictionary.setEmbeddedFiles(pDEmbeddedFilesNameTreeNode);
				doc.getDocumentCatalog().setNames(pDDocumentNameDictionary);
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new PDFToolkitException(exception.Message, exception);
			}
			return flag;
		}
	}
}