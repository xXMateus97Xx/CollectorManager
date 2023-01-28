namespace CollectorManager.Helpers;

public static class DateTimeHelpers
{
    public static string FormatDate(this DateTime date) => date.ToString("dd/MM/yyyy");
}
