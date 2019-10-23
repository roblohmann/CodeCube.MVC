using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CodeCube.Mvc.Functions;

namespace CodeCube.Mvc.Extensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Resizes the provided image and returns an bitmap
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The desired width</param>
        /// <param name="height">The desired height</param>
        /// <returns>An bitmap created in the desired size, based on the original image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var frameCount = image.GetFrameCount(new FrameDimension(image.FrameDimensionsList[0]));
            var newDimensions = ImageFunctions.GenerateImageDimensions(image.Width, image.Height, width, height);

            var resizedImage = new Bitmap(newDimensions.Width, newDimensions.Height);
            if (frameCount > 1)
            {
                //we have a animated GIF
                //resizedImage = ResizeAnimatedGifImage(image, width, height);
                throw new NotSupportedException("The function to resize animated gifs is implemented yet.");
            }
            
            //we have a normal image
            using (var gfx = Graphics.FromImage(resizedImage))
            {
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var targRectangle = new Rectangle(0, 0, newDimensions.Width, newDimensions.Height);
                var srcRectangle = new Rectangle(0, 0, image.Width, image.Height);

                gfx.DrawImage(image, targRectangle, srcRectangle, GraphicsUnit.Pixel);
            }

            return resizedImage;
        }
    }
}