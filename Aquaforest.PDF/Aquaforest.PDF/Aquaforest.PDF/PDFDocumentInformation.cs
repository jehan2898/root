using java.text;
using java.util;
using org.apache.pdfbox.pdmodel;
using System;
using System.Collections.Generic;

namespace Aquaforest.PDF
{
	public class PDFDocumentInformation
	{
		private PDDocumentInformation documentInformation;

		public string Author
		{
			get
			{
				return this.documentInformation.getAuthor();
			}
			set
			{
				this.documentInformation.setAuthor(value);
			}
		}

		public DateTime? CreationDate
		{
			get
			{
				return this.ConvertJavaDateToCSharp(this.documentInformation.getCreationDate());
			}
			set
			{
				if (value.HasValue)
				{
					Calendar instance = Calendar.getInstance();
					DateTime dateTime = value.Value;
					int year = dateTime.Year;
					dateTime = value.Value;
					int month = dateTime.Month - 1;
					dateTime = value.Value;
					int day = dateTime.Day;
					dateTime = value.Value;
					int hour = dateTime.Hour;
					dateTime = value.Value;
					int minute = dateTime.Minute;
					dateTime = value.Value;
					instance.@set(year, month, day, hour, minute, dateTime.Second);
					this.documentInformation.setCreationDate(instance);
				}
			}
		}

		public string Creator
		{
			get
			{
				return this.documentInformation.getCreator();
			}
			set
			{
				this.documentInformation.setCreator(value);
			}
		}

		public string Keywords
		{
			get
			{
				return this.documentInformation.getKeywords();
			}
			set
			{
				this.documentInformation.setKeywords(value);
			}
		}

		public DateTime? ModifiedDate
		{
			get
			{
				return this.ConvertJavaDateToCSharp(this.documentInformation.getModificationDate());
			}
			set
			{
				if (value.HasValue)
				{
					Calendar instance = Calendar.getInstance();
					DateTime dateTime = value.Value;
					int year = dateTime.Year;
					dateTime = value.Value;
					int month = dateTime.Month - 1;
					dateTime = value.Value;
					int day = dateTime.Day;
					dateTime = value.Value;
					int hour = dateTime.Hour;
					dateTime = value.Value;
					int minute = dateTime.Minute;
					dateTime = value.Value;
					instance.@set(year, month, day, hour, minute, dateTime.Second);
					this.documentInformation.setModificationDate(instance);
				}
			}
		}

		internal PDDocumentInformation PDFBoxDocumentInformation
		{
			get
			{
				return this.documentInformation;
			}
		}

		public string Producer
		{
			get
			{
				return this.documentInformation.getProducer();
			}
			set
			{
				this.documentInformation.setProducer(value);
			}
		}

		public string Subject
		{
			get
			{
				return this.documentInformation.getSubject();
			}
			set
			{
				this.documentInformation.setSubject(value);
			}
		}

		public string Title
		{
			get
			{
				return this.documentInformation.getTitle();
			}
			set
			{
				this.documentInformation.setTitle(value);
			}
		}

		public string Trapped
		{
			get
			{
				return this.documentInformation.getTrapped();
			}
			set
			{
				this.documentInformation.setTrapped(value);
			}
		}

		public PDFDocumentInformation()
		{
			this.documentInformation = new PDDocumentInformation();
		}

		private DateTime? ConvertJavaDateToCSharp(Calendar cal)
		{
			DateTime? nullable;
			SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss Z");
			if (cal != null)
			{
				string str = simpleDateFormat.format(cal.getTime());
				if (str != null)
				{
					nullable = new DateTime?(DateTime.Parse(str));
					return nullable;
				}
			}
			nullable = null;
			return nullable;
		}

		public string GetCustomMetadataValue(string fieldName)
		{
			return this.documentInformation.getCustomMetadataValue(fieldName);
		}

		internal PDFDocumentInformation GetDocumentInformation(PDDocument pdfDocument)
		{
			PDDocumentInformation documentInformation = pdfDocument.getDocumentInformation();
			PDFDocumentInformation pDFDocumentInformation = new PDFDocumentInformation()
			{
				Author = documentInformation.getAuthor()
			};
			documentInformation.getCreationDate();
			pDFDocumentInformation.CreationDate = this.ConvertJavaDateToCSharp(documentInformation.getCreationDate());
			pDFDocumentInformation.Creator = documentInformation.getCreator();
			pDFDocumentInformation.Keywords = documentInformation.getKeywords();
			pDFDocumentInformation.ModifiedDate = this.ConvertJavaDateToCSharp(documentInformation.getModificationDate());
			pDFDocumentInformation.Producer = documentInformation.getProducer();
			pDFDocumentInformation.Subject = documentInformation.getSubject();
			pDFDocumentInformation.Title = documentInformation.getTitle();
			pDFDocumentInformation.Trapped = documentInformation.getTrapped();
			return pDFDocumentInformation;
		}

		public List<string> GetMetadataKeys()
		{
			object[] objArray = this.documentInformation.getMetadataKeys().toArray();
			List<string> strs = new List<string>();
			for (int i = 0; i < (int)objArray.Length; i++)
			{
				strs.Add(objArray[i].ToString());
			}
			return strs;
		}

		public void SetCustomMetadataValue(string fieldName, string fieldValue)
		{
			this.documentInformation.setCustomMetadataValue(fieldName, fieldValue);
		}
	}
}