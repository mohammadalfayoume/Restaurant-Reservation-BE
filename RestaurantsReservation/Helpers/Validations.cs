namespace RestaurantsReservation.Helpers;
/// <summary>
/// Custom Validation.
/// </summary>
internal static class Validations
{

    /// <summary>
    /// Check if the given string date is valid format.
    /// </summary>
    /// <param name="dateString"></param>
    /// <param name="isValid"></param>
    /// <param name="date"></param>
    /// <returns>bool(isValid) + DateOnly(date)</returns>
    public static void IsValidDate(string dateString, out bool isValid, out DateOnly date)
    {
        DateOnly dateTime = DateOnly.FromDateTime(DateTime.UtcNow);
        bool isValidDate = DateOnly.TryParse(dateString, out dateTime);

        date = dateTime;
        isValid = isValidDate;

    }
    /// <summary>
    /// Check if the given string time is valid format.
    /// </summary>
    /// <param name="timeString"></param>
    /// <param name="isValid"></param>
    /// <param name="time"></param>
    /// <returns>bool(isValid) + TimeOnly(time)</returns>
    public static void IsValidTime(string timeString, out bool isValid, out TimeOnly time)
    {
        TimeOnly returnedTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        bool isValidTime = TimeOnly.TryParse(timeString, out returnedTime);

        time = returnedTime;
        isValid = isValidTime;
    }


}
