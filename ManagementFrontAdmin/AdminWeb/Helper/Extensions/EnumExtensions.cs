using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Reflection;

namespace AdminWeb.Helper.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? enumValue.ToString();
        }
    }
}
