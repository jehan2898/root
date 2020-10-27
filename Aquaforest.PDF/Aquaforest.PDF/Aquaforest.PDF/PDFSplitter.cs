using java.io;
using java.util;
using org.apache.commons.collections4;
using org.apache.pdfbox.multipdf;
using org.apache.pdfbox.pdfwriter;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.interactive.documentnavigation.outline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Console = System.Console;
namespace Aquaforest.PDF
{
	public class PDFSplitter
	{
		private PDFDocument pdfDocument;

		private int splitCounter = 0;

		public string OutputFileName
		{
			get;
			set;
		}

		public string OutputFilePath
		{
			get;
			set;
		}

		public PDFSplitter(PDFDocument sourcePDF)
		{
			this.pdfDocument = sourcePDF;
		}

		private bool CheckOutput()
		{
			bool flag;
			if (string.IsNullOrEmpty(this.OutputFileName))
			{
				Console.WriteLine("OutputFileName has not been set.");
				flag = false;
			}
			else if (!string.IsNullOrEmpty(this.OutputFilePath))
			{
				if (!Directory.Exists(this.OutputFilePath))
				{
					try
					{
						Directory.CreateDirectory(this.OutputFilePath);
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						Console.WriteLine("Could not create directory: '{0}'.{1}{2}", this.OutputFilePath, Environment.NewLine, exception.Message);
						flag = false;
						return flag;
					}
				}
				flag = true;
			}
			else
			{
				Console.WriteLine("OutputFilePath has not been set.");
				flag = false;
			}
			return flag;
		}

		private bool ConfigureRange(string range, int pageCount, out int start, out int end, out int counterStart, out int stepCount)
		{
			bool flag;
			bool flag1;
			start = 0;
			end = 0;
			bool flag2 = false;
			bool flag3 = false;
			counterStart = 0;
			stepCount = 1;
			if (!range.Contains<char>('-'))
			{
				if (!int.TryParse(range, out start))
				{
					flag = false;
					return flag;
				}
				end = start;
				start = start - 1;
			}
			else
			{
				string[] strArrays = range.Split(new char[] { '-' });
				if (int.TryParse(strArrays[0], out start))
				{
					if (strArrays[1].ToLower() != "lastpage")
					{
						string lower = strArrays[1].ToLower();
						if (lower.Contains("odd"))
						{
							lower = lower.Replace("odd", "");
							flag3 = true;
							stepCount = 2;
						}
						else if (lower.Contains("even"))
						{
							lower = lower.Replace("even", "");
							flag2 = true;
							stepCount = 2;
						}
						if (!int.TryParse(lower, out end))
						{
							flag = false;
							return flag;
						}
					}
					else
					{
						end = pageCount;
					}
					if (!flag2 || this.IsEven(start))
					{
						flag1 = (!flag3 ? false : this.IsEven(start));
					}
					else
					{
						flag1 = true;
					}
					if (flag1)
					{
						counterStart = 1;
					}
					start = start - 1;
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			flag = ((start >= 0 ? true : end <= pageCount) ? true : false);
			return flag;
		}

		private bool IsEven(int number)
		{
			return number % 2 == 0;
		}

		private void Split(int start, int end, int repeatEvery)
		{
			if (this.CheckOutput())
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				try
				{
					Splitter splitter = new Splitter();
					splitter.setStartPage(start);
					splitter.setEndPage(end);
					splitter.setSplitAtPage(repeatEvery);
					List list = splitter.split(this.pdfDocument.PDFBoxDocument);
					for (int i = 0; i < list.size(); i++)
					{
						PDDocument pDDocument = PDFHelper.AddTrialStampIfNecessary((PDDocument)list.@get(i));
						string str = string.Format("{0} [{1}].pdf", this.OutputFileName, i);
						pDDocument.save(Path.Combine(this.OutputFilePath, str));
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception.InnerException);
				}
			}
		}

		public void SplitByPageRanges(string pageRanges)
		{
			int num;
			int num1;
			int num2;
			int num3;
			if (this.CheckOutput())
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				try
				{
					string[] strArrays = pageRanges.Split(new char[] { ',' });
					List list = IteratorUtils.toList(this.pdfDocument.PDFBoxDocument.getPages().iterator());
					int num4 = list.size();
					string[] strArrays1 = strArrays;
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						string str = strArrays1[i];
						if (this.ConfigureRange(str, num4, out num, out num1, out num2, out num3))
						{
							List list1 = list.subList(num, num1);
							PDDocument pDDocument = new PDDocument();
							for (int j = num2; j < list1.size(); j = j + num3)
							{
								PDPage pDPage = (PDPage)list1.@get(j);
								pDPage.getResources();
								pDDocument.addPage(pDPage);
							}
							pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
							string str1 = string.Format("{0} [{1}].pdf", this.OutputFileName, str);
							pDDocument.save(Path.Combine(this.OutputFilePath, str1));
						}
						else
						{
							Console.WriteLine("Invalid range: {0}", str);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception.InnerException);
				}
			}
		}

		public void SplitByRepeatingNumber(int repeatEvery)
		{
			this.Split(1, this.pdfDocument.NumberOfPages, repeatEvery);
		}

		public void SplitByRepeatingNumber(int startPage, int endPage, int repeatEvery)
		{
			this.Split(startPage, endPage, repeatEvery);
		}

		public void SplitByRepeatingPageRanges(string pageRange, int repeatEvery)
		{
			if (this.CheckOutput())
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				try
				{
					if (pageRange.Contains<char>('-'))
					{
						string str = "";
						if (pageRange.ToLower().Contains("odd"))
						{
							str = "odd";
						}
						else if (pageRange.ToLower().Contains("even"))
						{
							str = "even";
						}
						PDPageTree pages = this.pdfDocument.PDFBoxDocument.getPages();
						int count = pages.getCount();
						this.splitCounter = 0;
						this.SplitByRepeatingPageRanges(pageRange, repeatEvery, pages, count, str);
					}
					else
					{
						Console.WriteLine("Invalid page range.");
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception);
				}
			}
		}

		private void SplitByRepeatingPageRanges(string pageRange, int repeatEvery, PDPageTree allPages, int pageCount, string suffix)
		{
			int num;
			int num1;
			int num2;
			int num3;
			if (this.ConfigureRange(pageRange, pageCount, out num, out num1, out num2, out num3))
			{
				List list = IteratorUtils.toList(allPages.iterator());
				List list1 = list.subList(num, num1);
				PDDocument pDDocument = new PDDocument();
				for (int i = num2; i < list1.size(); i = i + num3)
				{
					pDDocument.addPage((PDPage)list1.@get(i));
				}
				pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
				string str = string.Format("{0} [{1}].pdf", this.OutputFileName, this.splitCounter);
				pDDocument.save(Path.Combine(this.OutputFilePath, str));
				if (repeatEvery > 0)
				{
					num = num + repeatEvery + 1;
					num1 = num1 + repeatEvery;
					if (num <= pageCount)
					{
						if (num1 > pageCount)
						{
							num1 = pageCount;
						}
						this.splitCounter = this.splitCounter + 1;
						string str1 = string.Format("{0}-{1}{2}", num, num1, suffix);
						this.SplitByRepeatingPageRanges(str1, repeatEvery, allPages, pageCount, suffix);
					}
				}
			}
			else
			{
				Console.WriteLine("Invalid page range.");
			}
		}

		public void SplitByTopLevelBookmarks()
		{
			if (this.CheckOutput())
			{
				PDFHelper.DisplayTrialPopupIfNecessary();
				try
				{
					PDDocumentCatalog documentCatalog = this.pdfDocument.PDFBoxDocument.getDocumentCatalog();
					PDDocumentOutline documentOutline = documentCatalog.getDocumentOutline();
					if (documentOutline != null)
					{
						PDOutlineItem firstChild = documentOutline.getFirstChild();
						PDPageTree pages = documentCatalog.getPages();
						List<int> nums = new List<int>();
						while (firstChild != null)
						{
							PDPage pDPage = firstChild.findDestinationPage(this.pdfDocument.PDFBoxDocument);
							nums.Add(pages.indexOf(pDPage));
							firstChild = firstChild.getNextSibling();
						}
						nums.Add(pages.getCount());
						for (int i = 0; i < nums.Count - 1; i++)
						{
							int item = nums[i];
							int num = nums[i + 1];
							PDDocument pDDocument = new PDDocument();
							for (int j = item; j < num; j++)
							{
								pDDocument.addPage(this.pdfDocument.PDFBoxDocument.getPage(j));
							}
							pDDocument = PDFHelper.AddTrialStampIfNecessary(pDDocument);
							string str = string.Format("{0} [{1}].pdf", this.OutputFileName, i);
							pDDocument.save(Path.Combine(this.OutputFilePath, str));
							pDDocument.close();
						}
					}
					else
					{
						Console.WriteLine("This document does not contain any bookmarks.");
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new PDFToolkitException(exception.Message, exception.InnerException);
				}
			}
		}

		public void SplitIntoSinglePages()
		{
			this.Split(1, this.pdfDocument.NumberOfPages, 1);
		}

		public void SplitIntoSinglePages(int startPage, int endPage)
		{
			this.Split(startPage, endPage, 1);
		}

		private void WriteDocument(PDDocument doc, string fileName)
		{
			FileOutputStream fileOutputStream = null;
			COSWriter cOSWriter = null;
			try
			{
				fileOutputStream = new FileOutputStream(fileName);
				cOSWriter = new COSWriter(fileOutputStream);
				cOSWriter.write(doc);
			}
			finally
			{
				if (fileOutputStream != null)
				{
					fileOutputStream.close();
				}
				if (cOSWriter != null)
				{
					cOSWriter.close();
				}
			}
		}
	}
}