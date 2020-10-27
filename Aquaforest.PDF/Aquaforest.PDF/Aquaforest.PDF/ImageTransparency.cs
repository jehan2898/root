using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Aquaforest.PDF
{
	internal class ImageTransparency
	{
		public ImageTransparency()
		{
		}

		public static void ChangeOpacity(string imagePath, float opacityvalue)
		{
			Bitmap bitmap = null;
			using (Image image = Image.FromFile(imagePath))
			{
				bitmap = new Bitmap(image.Width, image.Height);
				Graphics graphic = Graphics.FromImage(bitmap);
				ColorMatrix colorMatrix = new ColorMatrix()
				{
					Matrix33 = opacityvalue / 100f
				};
				ImageAttributes imageAttribute = new ImageAttributes();
				imageAttribute.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
				graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttribute);
				graphic.Dispose();
			}
			bitmap.Save(imagePath);
		}
	}
}