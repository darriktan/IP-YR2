using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleGamePlayer
{
    //properties of our SimpleGameP layer
    public string userName;
    public string displayName;
    public string email;
    public bool active;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;

    //empty constructor
    public SimpleGamePlayer()
    {

    }

    public SimpleGamePlayer(string userName, string displayName, string email,
        bool active = true)
    {
        this.userName = userName;
        this.displayName = displayName;
        this.email = email;
        this.active = active;

        //timestamp properties
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        this.lastLoggedIn = timestamp;
        this.createdOn = timestamp;
        this.updatedOn = timestamp;
    }

    //helper functions to convert object to JSON
    public string SimpleGamePlayerToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //print player details
    public string PrintPlayer()
    {
        return String.Format("Player details: {0} \n User Name: {1}\n Email: {2}\n Active: {3}",
            this.displayName, this.userName, this.email, this.active);
    }
}
