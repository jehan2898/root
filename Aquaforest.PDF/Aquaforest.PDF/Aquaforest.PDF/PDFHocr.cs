using java.io;
using java.text;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.font;
using org.apache.pdfbox.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Aquaforest.PDF
{
	internal class PDFHocr : PDFTextStripper
	{
		public StringBuilder tWord = new StringBuilder();

		private List<WordData> wordList = new List<WordData>();

		private List<HocrLineModel> lineList = new List<HocrLineModel>();

		private List<HocrPageModel> pageList;

		public bool is1stChar = true;

		public bool lineMatch;

		public int pageNo = 0;

		private int wordID = 1;

		private double initialXcoord = 0;

		private double initialWidth = 0;

		public double lastYVal;

		private double xcord = 0;

		private double ycord = 0;

		private double lineYcord = 0;

		private double wordHeight = 0;

		private double wordwidth = 0;

		private bool getHOCRByWords = false;

		private int lineID1;

		public PDFHocr()
		{
			this.pageList = new List<HocrPageModel>();
		}

		private void AddToPageList()
		{
			if ((this.lineList == null ? false : this.lineList.Count > 0))
			{
				HocrPageModel hocrPageModel = new HocrPageModel();
				hocrPageModel.Lines.AddRange(this.SortLineList(this.lineList));
				this.pageList.Add(hocrPageModel);
				this.lineList.Clear();
			}
		}

		private void appendChar(string tChar, TextPosition text)
		{
			this.tWord.Append(tChar);
			this.is1stChar = false;
			this.wordwidth = this.wordwidth + (double)text.getWidth();
		}

		internal static bool CreateHocrFileFromPDF(PDDocument document, string outputfile, bool useWords)
		{
			bool flag;
			try
			{
				PDFHocr pDFHocr = new PDFHocr()
				{
					getHOCRByWords = useWords
				};
				pDFHocr.setSortByPosition(true);
				pDFHocr.setStartPage(0);
				pDFHocr.setEndPage(document.getNumberOfPages());
				PDFHelper.DisplayTrialPopupIfNecessary();
				if (PDFHelper.AddStamp)
				{
					pDFHocr.setEndPage(3);
				}
				pDFHocr.writeText(document, new OutputStreamWriter(new ByteArrayOutputStream()));
				if ((pDFHocr.lineList == null ? false : pDFHocr.lineList.Count > 0))
				{
					HocrPageModel hocrPageModel = new HocrPageModel();
					hocrPageModel.Lines.AddRange(pDFHocr.SortLineList(pDFHocr.lineList));
					pDFHocr.pageList.Add(hocrPageModel);
					pDFHocr.lineList.Clear();
				}
				pDFHocr.GetHocrFromPageList(pDFHocr.pageList, outputfile);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private bool CreateHocrPage(HocrPageModel page, string fileName, int pageNumber)
		{
			bool flag = true;
			try
			{
				List<string> strs = new List<string>();
				foreach (HocrLineModel line in page.Lines)
				{
					strs.Add(this.GetLineWithWords(line));
				}
				using (StreamWriter streamWriter = new StreamWriter(fileName))
				{
					streamWriter.WriteLine("<!DOCTYPE html  PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"  \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" > ");
					streamWriter.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
					streamWriter.WriteLine("\t<head>");
					streamWriter.WriteLine("\t\t <title>OCR Output</title>");
					streamWriter.WriteLine("\t</head>");
					streamWriter.WriteLine("\t<body>");
					streamWriter.WriteLine(string.Format("\t\t <div class=\"ocr_page\" title=\"bbox 0 0 {0} {1}; ppageno {2}\">", page.Lines.Max<HocrLineModel>((HocrLineModel l) => l.Words.Max<WordData>((WordData w) => w.XCord1)), page.Lines.Max<HocrLineModel>((HocrLineModel l) => l.Words.Max<WordData>((WordData w) => w.YCord1)), pageNumber));
					foreach (string str in strs)
					{
						streamWriter.WriteLine(str);
					}
					streamWriter.WriteLine("\t\t</div>");
					streamWriter.WriteLine("\t</body>");
					streamWriter.WriteLine("</html>");
				}
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void endLine()
		{
			HocrLineModel hocrLineModel = new HocrLineModel();
			if (this.wordList.Count > 0)
			{
				string str = string.Join(" ", (
					from w in this.wordList
					select w.Word).ToArray<string>());
				hocrLineModel.Text = (new Regex("\\s+(\\p{P})")).Replace(str, "$1");
				hocrLineModel.FontNameFirstWord = this.wordList[0].FontName;
				hocrLineModel.FontSizeFirstWord = this.wordList[0].FontSize;
				hocrLineModel.XCord = this.wordList[0].XCord;
				hocrLineModel.XCord1 = this.wordList[this.wordList.Count - 1].XCord1;
				hocrLineModel.YCord = this.wordList.Min<WordData>((WordData y) => y.YCord);
				hocrLineModel.YCord1 = this.wordList.Max<WordData>((WordData y) => y.YCord1);
				hocrLineModel.Words.AddRange(this.wordList);
				this.lineList.Add(hocrLineModel);
			}
			this.initialXcoord = 0;
			this.wordList.Clear();
		}

		private void endWord(TextPosition text)
		{
			if (this.ycord == 162)
			{
			}
			if (Math.Abs(this.lineYcord - this.ycord) > 5)
			{
				if (this.wordList.Count > 0)
				{
					this.endLine();
				}
			}
			string str = this.tWord.ToString().Replace("[^\\x00-\\x7F]", "");
			if (!string.IsNullOrEmpty(str.Substring(str.LastIndexOf(' ') + 1)))
			{
				this.wordList.Add(new WordData()
				{
					Word = str,
					WordHeight = this.wordHeight,
					WordLength = this.wordwidth,
					FontName = text.getFont().getName(),
					XCord = this.xcord,
					YCord = this.ycord,
					XCord1 = this.xcord + this.wordwidth,
					YCord1 = this.ycord + this.wordHeight,
					FontSize = (double)text.getFontSize(),
					PageNumber = this.pageNo,
					SpaceWidth = this.initialWidth
				});
			}
			this.tWord.Remove(0, this.tWord.Length);
			this.is1stChar = true;
			this.wordwidth = 0;
			this.wordHeight = 0;
			this.lineYcord = this.ycord;
		}

		private string GetHocrFromPageList(List<HocrPageModel> pages, string outputfileprefix)
		{
			string empty = string.Empty;
			int num = 1;
			foreach (HocrPageModel page in pages)
			{
				if (this.CreateHocrPage(page, string.Format("{0}{1}.{2}", outputfileprefix, num, "html"), num))
				{
					num++;
				}
			}
			return string.Empty;
		}

		private string GetLineWithWords(HocrLineModel line)
		{
			string empty;
			if (line.Words.Count <= 0)
			{
				empty = string.Empty;
			}
			else
			{
				string str = string.Empty;
				if (this.getHOCRByWords)
				{
					str = string.Format("\t\t\t<span class='ocrx_line' id='line_{0}' title=\"bbox {1} {2} {3} {4}\">\n", new object[] { line.LineID, line.XCord, line.YCord, line.XCord1, line.YCord1 });
					foreach (WordData word in line.Words)
					{
						str = string.Concat(str, this.GetOCRWords(word), "\n");
					}
				}
				else
				{
					str = ((string.IsNullOrEmpty(line.FontNameFirstWord) ? true : line.FontSizeFirstWord <= 0) ? string.Format("\t\t\t<span class='ocrx_line' id='line_{0}' title=\"bbox {1} {2} {3} {4}\">\n", new object[] { line.LineID, line.XCord, line.YCord, line.XCord1, line.YCord1, line.Text }) : string.Format("\t\t\t<span class='ocrx_line' id='line_{0}' title=\"bbox {1} {2} {3} {4}; x_font {6}; x_fsize {7}\">\n", new object[] { line.LineID, line.XCord, line.YCord, line.XCord1, line.YCord1, line.Text, line.FontNameFirstWord, line.FontSizeFirstWord }));
					str = string.Concat(str, "\t\t\t\t", string.Join(" ", (
						from w in line.Words
						select w.Word).ToArray<string>()), "\n");
				}
				str = string.Concat(str, "\t\t\t</span>\n");
				empty = str;
			}
			return empty;
		}

		private string GetOCRWords(WordData line)
		{
			string empty = string.Empty;
			empty = ((string.IsNullOrEmpty(line.FontName) ? true : line.FontSize <= 0) ? string.Format("\t\t\t\t<span class='ocrx_word' id='word_{0}' title=\"bbox {1} {2} {3} {4}\">{5}</span>", new object[] { this.wordID, line.XCord, line.YCord, line.XCord1, line.YCord1, line.Word }) : string.Format("\t\t\t\t<span class='ocrx_word' id='word_{0}' title=\"bbox {1} {2} {3} {4}; x_font {6}; x_fsize {7}\">{5}</span>", new object[] { this.wordID, line.XCord, line.YCord, line.XCord1, line.YCord1, line.Word, line.FontName, line.FontSize }));
			this.wordID = this.wordID + 1;
			return empty;
		}

		internal static List<HocrPageModel> GetPageWordDetails(PDDocument document)
		{
			List<HocrPageModel> hocrPageModels;
			try
			{
				PDFHocr pDFHocr = new PDFHocr();
				pDFHocr.setSortByPosition(true);
				pDFHocr.setStartPage(0);
				pDFHocr.setEndPage(document.getNumberOfPages());
				Writer outputStreamWriter = new OutputStreamWriter(new ByteArrayOutputStream());
				PDFHelper.DisplayTrialPopupIfNecessary();
				if (PDFHelper.AddStamp)
				{
					pDFHocr.setEndPage(3);
				}
				pDFHocr.writeText(document, outputStreamWriter);
				if ((pDFHocr.lineList == null ? false : pDFHocr.lineList.Count > 0))
				{
					HocrPageModel hocrPageModel = new HocrPageModel();
					hocrPageModel.Lines.AddRange(pDFHocr.SortLineList(pDFHocr.lineList));
					pDFHocr.pageList.Add(hocrPageModel);
					pDFHocr.lineList.Clear();
				}
				hocrPageModels = pDFHocr.pageList;
			}
			catch (Exception exception)
			{
				hocrPageModels = null;
			}
			return hocrPageModels;
		}

		private bool matchCharLine(TextPosition text)
		{
			bool flag;
			double num = this.roundVal(text.getYDirAdj());
			if (num != this.lastYVal)
			{
				this.lastYVal = num;
				this.endWord(text);
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public override void processPage(PDPage page)
		{
			this.AddToPageList();
			this.pageNo = this.pageNo + 1;
			base.processPage(page);
		}

		protected override void processTextPosition(TextPosition text)
		{
			string unicode = text.getUnicode();
			string str = "";
			char charArray = unicode.ToCharArray()[0];
			this.lineMatch = this.matchCharLine(text);
			float widthOfSpace = text.getWidthOfSpace();
			if ((this.initialXcoord == 0 ? false : (double)text.getXDirAdj() - this.initialXcoord >= (double)widthOfSpace))
			{
				this.endWord(text);
				this.setWordCoord(text, unicode);
			}
			else if ((str.Contains(unicode) ? true : charArray == ' '))
			{
				this.endWord(text);
			}
			else
			{
				if ((this.is1stChar ? false : this.lineMatch))
				{
					this.initialWidth = (double)text.getWidth();
					this.appendChar(unicode, text);
				}
				else if (this.is1stChar)
				{
					this.setWordCoord(text, unicode);
				}
				if ((double)text.getHeight() > this.wordHeight)
				{
					this.wordHeight = (double)text.getHeight();
				}
			}
			this.initialXcoord = (double)(text.getXDirAdj() + text.getWidth());
		}

		private double roundVal(float yVal)
		{
			DecimalFormat decimalFormat = new DecimalFormat("0.0'0'");
			return Convert.ToDouble(decimalFormat.format((double)yVal));
		}

		private void setWordCoord(TextPosition text, string tChar)
		{
			this.tWord.Append(tChar);
			this.xcord = (double)text.getXDirAdj();
			this.ycord = (double)text.getYDirAdj();
			this.wordwidth = this.wordwidth + (double)text.getWidth();
			this.initialWidth = (double)text.getWidthDirAdj();
			this.is1stChar = false;
		}

		private List<HocrLineModel> SortLineList(List<HocrLineModel> lines)
		{
			List<HocrLineModel> hocrLineModels = new List<HocrLineModel>();
			IEnumerable<IGrouping<double, HocrLineModel>> groupings = 
				from l in lines
				group l by l.YCord;
			foreach (IGrouping<double, HocrLineModel> nums in groupings)
			{
				HocrLineModel hocrLineModel = new HocrLineModel()
				{
					FontNameFirstWord = nums.ElementAt<HocrLineModel>(0).FontNameFirstWord,
					FontSizeFirstWord = nums.ElementAt<HocrLineModel>(0).FontSizeFirstWord
				};
				hocrLineModel.Words.AddRange(nums.SelectMany<HocrLineModel, WordData>((HocrLineModel g) => g.Words).ToList<WordData>());
				hocrLineModel.XCord = (
					from w in hocrLineModel.Words
					select w.XCord).Min();
				hocrLineModel.XCord1 = (
					from w in hocrLineModel.Words
					select w.XCord1).Max();
				hocrLineModel.YCord = (
					from w in hocrLineModel.Words
					select w.YCord).Min();
				hocrLineModel.YCord1 = (
					from w in hocrLineModel.Words
					select w.YCord1).Max();
				hocrLineModels.Add(hocrLineModel);
			}
			hocrLineModels = (
				from l in hocrLineModels
				orderby l.YCord
				select l).ToList<HocrLineModel>();
			for (int i = 0; i < hocrLineModels.Count; i++)
			{
				HocrLineModel item = hocrLineModels[i];
				int num = this.lineID1;
				this.lineID1 = num + 1;
				item.LineID = num;
			}
			return hocrLineModels;
		}
	}
}