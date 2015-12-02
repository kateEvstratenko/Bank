using System;

namespace BLL.Services
{
    public static class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static string GetRandomString(int length)
        {
            var str = String.Empty;
            for (var i = 0; i < length; i++)
            {
                str += Random.Next(0, 10);
            }
            return str;
        }
    }
}