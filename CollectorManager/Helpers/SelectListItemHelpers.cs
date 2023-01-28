using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollectorManager.Helpers;

public static class SelectListItemHelpers
{
    public static IEnumerable<SelectListItem> PrependDefault(this IEnumerable<SelectListItem> list) => list.Prepend(new SelectListItem("", ""));
}
