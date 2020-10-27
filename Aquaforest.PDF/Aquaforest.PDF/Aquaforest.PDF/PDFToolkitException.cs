using System;

namespace Aquaforest.PDF
{
	public class PDFToolkitException : Exception
	{
		public PDFToolkitException()
		{
		}

		public PDFToolkitException(string message) : base(message)
		{
		}

		public PDFToolkitException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}