using System;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFPermission
	{
		public bool AllowAssembly
		{
			get;
			set;
		}

		public bool AllowDegradedPrinting
		{
			get;
			set;
		}

		public bool AllowExtractContents
		{
			get;
			set;
		}

		public bool AllowExtractForAccessibility
		{
			get;
			set;
		}

		public bool AllowFillInForm
		{
			get;
			set;
		}

		public bool AllowModifyAnnotations
		{
			get;
			set;
		}

		public bool AllowModifyContents
		{
			get;
			set;
		}

		public bool AllowPrinting
		{
			get;
			set;
		}

		public PDFPermission()
		{
			this.AllowAssembly = true;
			this.AllowDegradedPrinting = true;
			this.AllowExtractContents = true;
			this.AllowExtractForAccessibility = true;
			this.AllowFillInForm = true;
			this.AllowModifyAnnotations = true;
			this.AllowModifyContents = true;
			this.AllowPrinting = true;
		}
	}
}