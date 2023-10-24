namespace RestaurantsReservation.Helpers;

internal static class ValidationDateTime
{
    public static void IsValidDate(string dateString, out bool isValid, out DateOnly date)
    {
        DateOnly dateTime = DateOnly.FromDateTime(DateTime.UtcNow);
        bool isValidDate = DateOnly.TryParse(dateString, out dateTime);

        date = dateTime;
        isValid = isValidDate;

    }
    public static void IsValidTime(string timeString, out bool isValid, out TimeOnly time)
    {
        TimeOnly returnedTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        bool isValidTime = TimeOnly.TryParse(timeString, out returnedTime);

        time = returnedTime;
        isValid = isValidTime;
    }
}
