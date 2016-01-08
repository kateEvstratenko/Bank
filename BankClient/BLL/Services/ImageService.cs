using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BLL.Interfaces;
using Core.Enums;

namespace BLL.Services
{
    public class ImageService: IImageService
    {
        private const string MilitaryPath = "Military";
        private const string IncomeCertificatesPath = "IncomeCertificates";

        public string SaveImageFromByteArray(byte[] array, string baseUrl, int userId, ImageType imageType)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = Image.FromStream(ms);
                var path = imageType == ImageType.MilitaryId
                    ? GetMilitaryIdPath(baseUrl)
                    : GetIncomeCertificatePath(baseUrl);
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                var fullPath = string.Format(@"{0}{1}_{2}.jpg", path, userId, Guid.NewGuid());
                image.Save(fullPath, ImageFormat.Jpeg);
                return fullPath;
            }           
        }

        private string GetMilitaryIdPath(string baseUrl)
        {
            return String.Format(@"{0}Content\{1}\", baseUrl, MilitaryPath);
        }

        private string GetIncomeCertificatePath(string baseUrl)
        {
            return String.Format(@"{0}Content\{1}\", baseUrl, IncomeCertificatesPath);
        }
    }
}