using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aquaforest.PDF
{
	public class HocrLineModel
	{
		internal string Bbox
		{
			get;
			set;
		}

		public string FontNameFirstWord
		{
			get;
			set;
		}

		public double FontSizeFirstWord
		{
			get;
			set;
		}

		public int LineID
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public List<WordData> Words
		{
			get;
			set;
		}

		public double XCord
		{
			get;
			set;
		}

		public double XCord1
		{
			get;
			set;
		}

		public double YCord
		{
			get;
			set;
		}

		public double YCord1
		{
			get;
			set;
		}

		internal HocrLineModel()
		{
			this.Words = new List<WordData>();
		}
	}
}