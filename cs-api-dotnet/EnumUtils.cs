using System;
using System.ComponentModel;
using System.Reflection;

namespace cs_api_dotnet
{
    /// <summary>
    /// Convert between Enum and String
    /// Adapted From http://waynehartman.com/posts/c-enums-and-string-values.html
    /// </summary>
    public class EnumUtils
    {
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static object enumValueOf(string value, System.Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }
            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
    }
}
