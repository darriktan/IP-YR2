using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Auth;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SimplePlayerStatsManager : MonoBehaviour
{
    public FirebaseAuth auth;

    public TextMeshProUGUI playerXP;
    public TextMeshProUGUI playerTimeSpent;
    public TextMeshProUGUI playerHighScore;
    public TextMeshProUGUI playerLastPlayed;
    public TextMeshProUGUI playerName;

    public SimpleFirebaseManager fbMgr;
    public SimpleAuthManager authMgr;

    // Start is called before the first frame update
    void Start()
    {
        //empty UI in the start
        ResetStatusUI();

        //retrieve current logged in user uuid
        //update UI
        UpdatePlayerStats(auth.CurrentUser.UserId);
    }

    void Awake()
    {
        Initalizefirebase();
    }

    void Initalizefirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public async void UpdatePlayerStats(string uuid)
    {
        SimplePlayerStats playerStats = await fbMgr.GetPlayerStats(uuid);
        if (playerStats != null)
        {
            Debug.Log("playerstats.... :" + playerStats.SimplePlayerStatsToJson());
            playerXP.text = playerStats.xp + "xp";
            playerTimeSpent.text = playerStats.totalTimeSpent + " secs";
            playerHighScore.text = playerStats.highScore.ToString();
            playerLastPlayed.text = UnixToDateTime(playerStats.updatedOn);
        }
        else
        {
            //reset our UI to 0 and NA
            ResetStatusUI();
        }
        playerName.text = authMgr.GetCurrentUserDisplayName();
    }

    public void ResetStatusUI()
    {
        playerXP.text = "0 XP";
        playerTimeSpent.text = "0";
        playerHighScore.text = "0";
        playerLastPlayed.text = "0";
    }

    public void DeletePlayerStatus()
    {
        fbMgr.DeletePlayerStats(auth.CurrentUser.UserId);

        //refresh my player stats on screen
        UpdatePlayerStats(auth.CurrentUser.UserId);
    }

    public string UnixToDateTime(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);//number of secs from 1/1/1970
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");

    }

    public void BackToMain()
    {
        SceneManager.LoadScene(1);
    }

}
