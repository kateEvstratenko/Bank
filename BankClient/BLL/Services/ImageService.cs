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

        public string SaveImageFromByteArray(byte[] array, string baseUrl, string userId, ImageType imageType)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = Image.FromStream(ms);
                var path = imageType == ImageType.MilitaryId
                    ? GetMilitaryIdPath(baseUrl, userId)
                    : GetIncomeCertificatePath(baseUrl, userId);
                image.Save(path, ImageFormat.Jpeg);
                return path;
            }           
        }

        private string GetMilitaryIdPath(string baseUrl, string userId)
        {
            return String.Format("{0}/{1}/{2}_{3}", baseUrl, MilitaryPath, userId, Guid.NewGuid());
        }

        private string GetIncomeCertificatePath(string baseUrl, string userId)
        {
            return String.Format("{0}/{1}/{2}_{3}", baseUrl, IncomeCertificatesPath, userId, Guid.NewGuid());
        }
    }
}