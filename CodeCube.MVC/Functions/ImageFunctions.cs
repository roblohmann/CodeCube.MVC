using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace CodeCube.Mvc.Functions
{
    public static class ImageFunctions
    {
        public static Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            // some sanity
            if (destW <= 0 || destH <= 0) return new Size(destW, destH);
            if (currW <= 0 || currH <= 0) return new Size(currW, currH);

            //double to hold the final multiplier to use when scaling the image
            double multiplier;
            double normalizedImageWidth = currW;
            double normalizedImageHeight = currH;

            if (currW >= destW || currH >= destH)
            {
                // either current width or height are greater than destination
                if (currW > currH)
                {
                    // current width dominates the scaling
                    multiplier = (double)currH / currW;
                    normalizedImageWidth = destW;
                    normalizedImageHeight = Math.Min(destH, currH) * multiplier;
                }
                else
                {
                    // current height dominates the scaling
                    multiplier = (double)currW / currH;
                    normalizedImageWidth = Math.Min(destW, currW) * multiplier;
                    normalizedImageHeight = destH;
                } 
            }
            else
            {
                // both sides are less, scale out
                if (currW > currH)
                {
                    // destination width dominates the scaling
                    multiplier = (double)destW / currW;
                }
                else
                {
                    // destination height dominates the scaling
                    multiplier = (double)destH / currH;
                } 
                normalizedImageWidth *= multiplier;
                normalizedImageHeight *= multiplier;
            } 

            return new Size((int)normalizedImageWidth, (int)normalizedImageHeight);
        } 

        /// <summary>
        /// Gets the image codec info for the provided mime type
        /// </summary>
        /// <param name="mimeType">The mimetype to get the codec info for. Eg. image/jpeg</param>
        /// <returns>An codecinco object.</returns>
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            var codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        /// <summary>
        /// Crops an image to the provided size.
        /// </summary>
        /// <param name="img">The image to crop</param>
        /// <param name="cropArea">The area of the image to crop</param>
        /// <returns>The croped image</returns>
        public static Image CropImage(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            
            return bmpCrop;
        }
    }
}