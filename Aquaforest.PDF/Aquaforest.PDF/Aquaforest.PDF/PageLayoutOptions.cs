using System;

namespace Aquaforest.PDF
{
	public class PageLayoutOptions
	{
		public static string ONE_COLUMN
		{
			get
			{
				return "OneColumn";
			}
		}

		public static string SINGLE_PAGE
		{
			get
			{
				return "SinglePage";
			}
		}

		public static string TWO_COLUMN_LEFT
		{
			get
			{
				return "TwoColumnLeft";
			}
		}

		public static string TWO_COLUMN_RIGHT
		{
			get
			{
				return "TwoColumnRight";
			}
		}

		public static string TWO_PAGE_LEFT
		{
			get
			{
				return "TwoPageLeft";
			}
		}

		public static string TWO_PAGE_RIGHT
		{
			get
			{
				return "TwoPageRight";
			}
		}

		public PageLayoutOptions()
		{
		}
	}
}