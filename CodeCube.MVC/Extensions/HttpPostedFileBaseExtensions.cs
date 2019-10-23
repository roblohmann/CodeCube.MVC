using System.Drawing;
using System.IO;
using System.Web;

namespace CodeCube.Mvc.Extensions
{
    public static class HttpPostedFileBaseExtensions
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }

        public static Size GetImageDimensions(this Stream str)
        {
            using (Image myImage = Image.FromStream(str))
            {
                return new Size(myImage.Width, myImage.Height);
            }
        }
    }
}
