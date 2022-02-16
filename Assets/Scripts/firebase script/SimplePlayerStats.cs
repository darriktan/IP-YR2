using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats
{
    public string userName;
    public int totalTimeSpent;
    public int highScore;
    public int xp;
    public long updatedOn;
    public long createdOn;

    //simple constructor
    public SimplePlayerStats()
    {

    }

    public SimplePlayerStats(string userName, int highScore, int xp = 0,
        int totalTimeSpent = 0)
    {
        this.userName = userName;
        this.s = highScore;
        this.xp = xp;
        this.totalTimeSpent = totalTimeSpent;

        var timestamp = this.GetTimeUnix();
        this.updatedOn = timestamp;
        this.createdOn = timestamp;
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public string SimplePlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}