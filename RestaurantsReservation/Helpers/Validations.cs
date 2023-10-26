using RestaurantsReservation.Models;
using System.Text.RegularExpressions;

namespace RestaurantsReservation.Helpers;

internal static class Validations
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
    // Method to validate email address using regular expression
    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(emailPattern);
        return regex.IsMatch(email);
    }

    public static bool IsOldDate(ReservationSchedule reservationSchedule)
    {
        DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        TimeOnly todayTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        var currentReservationDate = DateOnly.Parse(reservationSchedule.ReservationDate);
        var currentStartAt = TimeOnly.Parse(reservationSchedule.StartAt);
        if (currentReservationDate < todayDate) return true;

        else if (currentReservationDate == todayDate && currentStartAt < todayTime) return true;

        return false;
    }

    public static bool IsValidReservation(ReservationSchedule reservationSchedule, string newReservationDate, string newStartAt, string newEndAt)
    {

        var currentReservationDate = DateOnly.Parse(reservationSchedule.ReservationDate);
        var currentStartAt = TimeOnly.Parse(reservationSchedule.StartAt);
        var currentEndAt = TimeOnly.Parse(reservationSchedule.EndAt);

        var newReseDate = DateOnly.Parse(newReservationDate);
        var newResStartAt = TimeOnly.Parse(newStartAt);
        var newResEndAt = TimeOnly.Parse(newEndAt);

        if (currentReservationDate == newReseDate)
        {
            if ((newResStartAt < currentStartAt && newResEndAt <= currentStartAt) || (newResStartAt >= currentEndAt && newResEndAt > currentEndAt)) return true;
        }
        return true;

    }

    public static bool CanCancel(ReservationSchedule reservation)
    {
        DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        TimeOnly todayTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        var reservationDate = DateOnly.Parse(reservation.ReservationDate);
        var startAt = TimeOnly.Parse(reservation.StartAt);
        if (todayDate == reservationDate && startAt < todayTime.AddHours(2))
            return false;
        return true;
    }

}
