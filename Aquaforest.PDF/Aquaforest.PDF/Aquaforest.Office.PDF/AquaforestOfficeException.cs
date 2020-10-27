using System;

namespace Aquaforest.Office.PDF
{
	public class AquaforestOfficeException : Exception
	{
		public AquaforestOfficeException()
		{
		}

		public AquaforestOfficeException(string message) : base(message)
		{
		}

		public AquaforestOfficeException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}