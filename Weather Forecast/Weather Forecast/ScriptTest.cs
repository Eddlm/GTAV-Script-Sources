using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.IO;
using System.Windows.Forms;

enum WeatherType
{
    EXTRA_SUNNY = -1750463879,
    CLEAR = 916995460,
    NEUTRAL = -1530260698,
    SMOG = 282916021,
    FOGGY = -1368164796,
    OVERCAST = -1148613331,
    CLOUDS = 821931868,
    CLEARING = 1840358669,
    RAIN = 1420204096,
    THUNDER = -1233681761,
    SNOW = -273223690,
    BLIZZARD = 669657108,
    LIGHT_SNOW = 603685163,
    X_MAS = -1429616491
};

public class WeatherForecast : Script
{

    WeatherType expectedWeather = WeatherType.NEUTRAL;
    public WeatherForecast()
    {
        Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, "news_weazelnews", true);

        Tick += OnTick;
        Interval = 5000;
    }

    void OnTick(object sender, EventArgs e)
    {
        if(expectedWeather == WeatherType.NEUTRAL)
        {
            expectedWeather = (WeatherType)Function.Call<int>(GTA.Native.Hash._GET_CURRENT_WEATHER_TYPE);
        }
        if (expectedWeather != (WeatherType)Function.Call<int>(GTA.Native.Hash._GET_NEXT_WEATHER_TYPE))
        {
            expectedWeather = (WeatherType)Function.Call<int>(GTA.Native.Hash._GET_NEXT_WEATHER_TYPE);
            WarnPlayer("Weazel News", "Forecast", "Here's the weather forecast: "+ GetForecastText(expectedWeather));
        }
    }


    void WarnPlayer(string script_name, string title, string message)
    {
        Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
        Function.Call(Hash._SET_NOTIFICATION_MESSAGE, "news_weazelnews", "news_weazelnews", true, 0, title, "~b~" + script_name);
    }

    bool CanWeUse(Entity entity)
    {
        return entity != null && entity.Exists();
    }

    void DisplayHelpTextThisFrame(string text)
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, false, true, -1);
    }

    string GetForecastText(WeatherType weather)
    {
        switch (weather)
        {
            case WeatherType.BLIZZARD:
                {
                    return "A big blizzard is expected.";
                }
            case WeatherType.CLEARING:
                {
                    return "Skies will clear in the next hours.";
                }
            case WeatherType.CLEAR:
                {
                    return "The skies will be clear for the next couple of hours.";
                }
            case WeatherType.FOGGY:
                {
                    return "Its going to be foggy for a while.";
                }
            case WeatherType.SMOG:
                {
                    return "Clear skies accompanied by little fog are expected.";
                }
            case WeatherType.RAIN:
                {
                    return "Rain is expected to feature the following hours.";
                }
            case WeatherType.NEUTRAL:
                {
                    return "Strange shit happening on the skies soon. Beware.";
                }
            case WeatherType.THUNDER:
                {
                    return "Heavy rain accompanied by thunder is expected.";
                }
            case WeatherType.LIGHT_SNOW:
                {
                    return "Some snow is expected.";
                }
            case WeatherType.SNOW:
                {
                    return "Copious ammounts of snow for the next hours.";
                }
            case WeatherType.X_MAS:
                {
                    return "Christmas itself is expected. Ho ho ho!";
                }
            case WeatherType.EXTRA_SUNNY:
                {
                    return "Skies will be completely clear for a few hours.";
                }
            case WeatherType.OVERCAST:
                {
                    return "Its going to be very cloudy for some time.";
                }
            case WeatherType.CLOUDS:
                {
                    return "Clouds will cover the sky for some hours.";
                }
        }
        return "No idea.";

    }
}
