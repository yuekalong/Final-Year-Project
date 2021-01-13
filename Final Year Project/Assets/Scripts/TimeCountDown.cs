using UnityEngine;
using System.Collections;
using System;

public static class TimeCountDown
{
    static DateTime startTime;
    static TimeSpan total;

    public static void StartCountDown(TimeSpan input)
    {
        startTime = DateTime.UtcNow;
        total = input;
    }

    public static TimeSpan TimeLeft
    {
        get
        {
            var result = total - (DateTime.UtcNow - startTime);
            if (result.TotalSeconds <= 0)
                return TimeSpan.Zero;
            return result;
        }
    }
}
