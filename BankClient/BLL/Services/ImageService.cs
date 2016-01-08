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

        public string SaveImageFromByteArray(byte[] array, string baseUrl, int userId, ImageType imageType, string baseLocalhostUrl)
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

                var guid = Guid.NewGuid();
                var fullPath = string.Format(@"{0}{1}_{2}.png", path, userId, guid);
                image.Save(fullPath, ImageFormat.Png);

                var returnPath = imageType == ImageType.MilitaryId
                    ? String.Format(@"{0}/Content/{1}/{2}_{3}.png", baseLocalhostUrl, MilitaryPath, userId, guid)
                    : String.Format(@"{0}/Content/{1}/{2}_{3}.png", baseLocalhostUrl, IncomeCertificatesPath, userId, guid); 

                return returnPath;
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