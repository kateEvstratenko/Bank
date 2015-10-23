using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Enums
{
    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}