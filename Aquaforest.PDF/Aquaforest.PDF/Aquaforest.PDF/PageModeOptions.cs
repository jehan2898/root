using System;

namespace Aquaforest.PDF
{
	public class PageModeOptions
	{
		public static string FULL_SCREEN
		{
			get
			{
				return "FullScreen";
			}
		}

		public static string USE_ATTACHMENTS
		{
			get
			{
				return "Use_Attachments";
			}
		}

		public static string USE_NONE
		{
			get
			{
				return "UseNone";
			}
		}

		public static string USE_OPTIONAL_CONTENT
		{
			get
			{
				return "UseOptionalContent";
			}
		}

		public static string USE_OUTLINES
		{
			get
			{
				return "Use_Outlines";
			}
		}

		public static string USE_THUMBS
		{
			get
			{
				return "UseThumbs";
			}
		}

		public PageModeOptions()
		{
		}
	}
}