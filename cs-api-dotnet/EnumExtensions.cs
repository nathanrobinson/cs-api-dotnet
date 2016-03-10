using System;
using System.ComponentModel;
using System.Reflection;

namespace cs_api_dotnet
{
    /// <summary>
    /// Convert between Enum and String
    /// Adapted From http://waynehartman.com/posts/c-enums-and-string-values.html
    /// </summary>
    public static class EnumExtensions
    {
        public static string ToFriendlyName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
