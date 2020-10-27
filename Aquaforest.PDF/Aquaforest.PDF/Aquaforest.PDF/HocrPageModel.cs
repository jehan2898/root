using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class HocrPageModel
	{
		public List<HocrLineModel> Lines
		{
			get;
			set;
		}

		internal HocrPageModel()
		{
			this.Lines = new List<HocrLineModel>();
		}
	}
}