using UnityEngine;
using System.Collections;
using System;


public class TimeCountDown : MonoBehaviour
{
    public DateTime startTime;
    public TimeSpan total;

    public void StartCountDown(TimeSpan input)
    {
        startTime = DateTime.UtcNow;
        total = input;
    }

    public TimeSpan TimeLeft
    {
        get
        {
            var result = total - (DateTime.UtcNow - startTime);
            if (result.TotalSeconds <= 0)
                return TimeSpan.Zero;
            return result;
        }
    }
    public TimeSpan TimeUsed
    {
        get
        {
            var result = DateTime.UtcNow - startTime;
            if (result.TotalSeconds <= 0)
                return TimeSpan.Zero;
            return result;
        }
    }
}
