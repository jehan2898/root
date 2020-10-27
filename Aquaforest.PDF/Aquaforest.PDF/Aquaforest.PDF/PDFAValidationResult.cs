using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class PDFAValidationResult
	{
		public bool IsValid
		{
			get;
			internal set;
		}

		public List<string> ValidationResult
		{
			get;
			set;
		}

		public PDFAValidationResult()
		{
		}
	}
}