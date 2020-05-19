using System;

public static class DateUtils
{
    public static string AddDay(string date, int days)
    {
        var parsedDate = DateTime.Parse(date);
        var newDate = parsedDate.AddDays(days);

        return newDate.ToString();
    }

    public static string DateToText(string date)
    {
        var parsedDate = DateTime.Parse(date);

        var month = parsedDate.Month > 9 ? parsedDate.Month.ToString() : String.Format("0{0}", parsedDate.Month.ToString());
        var day = parsedDate.Day > 9 ? parsedDate.Day.ToString() : String.Format("0{0}", parsedDate.Day.ToString());

        return String.Format("{0}-{1}-{2}", parsedDate.Year, month, day);
    }

    public static int GetMonth(string date)
    {
        var parsedDate = DateTime.Parse(date);
        return parsedDate.Month;
    }

    public static int GetDay(string date)
    {
        var parsedDate = DateTime.Parse(date);
        return parsedDate.Day;
    }

    public static int GetYear(string date)
    {
        var parsedDate = DateTime.Parse(date);
        return parsedDate.Year;
    }

    public static string DetermineSeason(string date)
    {
        var parsedDate = DateTime.Parse(date);
        var month = parsedDate.Month;

        if (month == 12 || month == 1 || month == 2)
        {
            return "Winter";
        }
        else if (month >= 3 && month <= 5)
        {
            return "Spring";
        }
        else if (month >= 6 && month <= 8)
        {
            return "Summer";
        }
        else
        {
            return "Fall";
        }
    }
}
