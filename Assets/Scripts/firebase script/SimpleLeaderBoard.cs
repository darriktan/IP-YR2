using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleLeaderBoard 
{
    public string userName;
    public int highScore;
    public long updatedOn;

    public SimpleLeaderBoard()
    {

    }

    public SimpleLeaderBoard(string userName, int highScore)
    {
        this.userName = userName;
        this.highScore = highScore;
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
