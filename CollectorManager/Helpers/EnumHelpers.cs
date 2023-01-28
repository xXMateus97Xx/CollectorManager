using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CollectorManager.Helpers;

public static class EnumHelpers
{
    public static string GetName<T>(this T @enum) where T : Enum
    {
        var str = @enum.ToString();

        var type = @enum.GetType();

        var display = type.GetField(str)
            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault();

        if (display == null || display is not DisplayAttribute)
            return str;

        return ((DisplayAttribute)display).Name ?? str;
    }

    public static string GetNumber<T>(this T @enum) where T : Enum => Enum.Format(@enum.GetType(), @enum, "d");

    public static SelectListItem ToSelectListItem<T>(this T @enum) where T : Enum => new SelectListItem(@enum.GetName(), @enum.GetNumber());
}
