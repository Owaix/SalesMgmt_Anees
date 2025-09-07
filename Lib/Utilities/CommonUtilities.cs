using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Lib.Utilities
{
    /// <summary>
    /// Enumerator that set values of transaction for Insert, Select, Update and Delete
    /// </summary>
    public enum SqlOperation
    {
        Insert,
        Select,
        Update,
        Delete
    }

    public static class Printer
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(String Printer);
        public static void setDef(String Name)
        {
            Printer.SetDefaultPrinter(Name);
        }
    }
    public static class ExceptionExtensions
    {
        public static string ToMessageAndCompleteStacktrace(this Exception exception)
        {
            Exception e = exception;
            StringBuilder s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine("Exception type: " + e.GetType().FullName);
                s.AppendLine("Message       : " + e.Message);
                s.AppendLine("Stacktrace:");
                s.AppendLine(e.StackTrace);
                s.AppendLine();
                e = e.InnerException;
            }
            return s.ToString();
        }
    }
    public static class Utillityfunctions
    {
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            if (!File.Exists(path))
            {
                img.Save(path, jpegCodec, encoderParams);
            }
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

        public static string ToBase64(string Path, string SavingPath)
        {
            try
            {

                using (Image image = Image.FromFile(Path))
                {
                    SaveJpeg(SavingPath, image, 50);
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static Image LoadImage(string BaseImg)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(BaseImg);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                return image;
            }
            catch (Exception)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                var paths = new Uri(path + "\\Img\\124444444.png").LocalPath;
                return Image.FromFile(paths);
            }
        }
    }
}
