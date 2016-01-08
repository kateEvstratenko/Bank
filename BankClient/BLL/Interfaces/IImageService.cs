using Core.Enums;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        string SaveImageFromByteArray(byte[] array, string baseUrl, int userId, ImageType imageType, string baseLocalhostUrl);
    }
}