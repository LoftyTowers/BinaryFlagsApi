using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
        if (member != null)
        {
            var attr = member.GetCustomAttribute<DisplayAttribute>();
            if (attr != null)
                return attr.Name ?? enumValue.ToString();
        }
        return enumValue.ToString();
    }
}
