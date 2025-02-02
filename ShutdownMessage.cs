namespace J.A.R.V.I.S;

public class ShutdownMessage
{
    //todo: store lat and long to config.
    private readonly double latitude = 43.3785; // Example latitude
    private readonly double longitude = -92.919576; // Example longitude
    private readonly DateTime today = DateTime.Now; // Today's date

    public string GetMessage()
    {
        var currentHour = today.Hour;
        var sunsetHour = GetSunset();
        var sunRiseHour = GetSunrise();

        var result = "Good Bye";

        if (currentHour >= sunsetHour)
        {
            result = "Have a Good Night, Sir.";
        }
        else if (currentHour >= 00 && currentHour < sunRiseHour)
        {
            result = "It's late sir you should get some sleep. ";
        }
        else if (currentHour >= sunRiseHour)
        {
            result = "Have a Good Morning, Sir. ";
        }
        else if (currentHour >= 12 && currentHour <= sunsetHour)
        {
            result = "Have a Good Afternoon, Sir. ";
        }

        return result;
    }

    private SolarTimes GetSolarTime()
    {
        return new(today, latitude, longitude);
    }

    private int GetSunset()
    {
        var solarTimes = GetSolarTime();
        return Convert.ToInt32(solarTimes.Sunset.ToString("HH"));
    }

    private int GetSunrise()
    {
        var solarTimes = GetSolarTime();
        return Convert.ToInt32(solarTimes.Sunrise.ToString("HH"));
    }
}
