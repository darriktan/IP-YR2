using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats
{
    public string userName;
    public float gameTimer = 0f;
    public string performanceGrade;
    public long updatedOn;
    public long createdOn;

    //simple constructor
    public SimplePlayerStats()
    {

    }

    public SimplePlayerStats(string userName, string performanceGrade,
        float gameTimer = 0f)
    {
        this.userName = userName;
        this.performanceGrade = performanceGrade;
        this.gameTimer = gameTimer;

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