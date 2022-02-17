using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleLeaderBoard 
{
    public string userName;
    public string gradeScore;
    public float gameTimer;
    public long updatedOn;

    public SimpleLeaderBoard()
    {

    }

    public SimpleLeaderBoard(string userName, string gradeScore, float gameTimer)
    {
        this.gradeScore = gradeScore;
        this.userName = userName;
        this.gameTimer = gameTimer;
        this.updatedOn = GetTimeUnix();
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public string SimpleLeaderBoardToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
