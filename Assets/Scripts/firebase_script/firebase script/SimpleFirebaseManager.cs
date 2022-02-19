using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;

public class SimpleFirebaseManager : MonoBehaviour
{
    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbLeaderboardsReference;

    public void Awake()
    {
        InitializeFirebase();
    }

    public void InitializeFirebase()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        dbLeaderboardsReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
    }

    /// <summary>
    /// Create a new entry ONLY if its the first time playing
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="grade"></param>
    /// <param name="time"></param>
    /// <param name="displayName"></param>
    public void UpdatePlayerStats(string uuid, string grade, float time, string displayName)
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        //READ the data first and check whether there has been an entry based on my uuid
        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                //check if there is an entry created
                if (playerStats.Exists)
                {
                    //Update player stats
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());

                    //check if theres a new high score
                    //update leaderboard if theres a new highscore
                    if (time > sp.gameTimer)
                    {
                        sp.gameTimer = time;
                        //UpdatePlayerLeaderBoardEntry(uuid, time);
                    }

                    //update with entire temp object
                    //playerStats/$uuid
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    //CREATE player stats
                    //if there no existing data, it's our first time player
                    SimplePlayerStats sp = new SimplePlayerStats(displayName, grade, time);

                    //SimpleLeaderBoard lb = new SimpleLeaderBoard(displayName, grade);

                    //create new entires into firebase
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    //dbLeaderboardsReference.Child(uuid).SetRawJsonValueAsync(lb.SimpleLeaderBoardToJson());
                }
            }
        });
    }

    public void UpdatePlayerLeaderBoardEntry(string uuid, string performanceGrade, float gameTimer, long updatedOn)
    {
        //leaderboards/$uuid/highscore
        //leaderboards/$uuid/updatedOn
        dbLeaderboardsReference.Child(uuid).Child("performanceGrade").SetValueAsync(performanceGrade);
        dbLeaderboardsReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);
        dbLeaderboardsReference.Child(uuid).Child("gameTimer").SetValueAsync(gameTimer);
    }
    
    public async Task<List<SimpleLeaderBoard>> GetLeaderboard(int limit = 5)
    {
        Query q = dbLeaderboardsReference.OrderByChild("score").LimitToLast(limit);

        List<SimpleLeaderBoard> leaderBoardList = new List<SimpleLeaderBoard>();

        await q.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error getting leaderboard entries, : ERROR" + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Exists)
                {
                    foreach(DataSnapshot d in ds.Children)
                    {
                        //create temp objects based on the results
                        SimpleLeaderBoard lb = JsonUtility.FromJson<SimpleLeaderBoard>(d.GetRawJsonValue());

                        //Add item to list
                        leaderBoardList.Add(lb);

                        //Debug.LogFormat("Leaderboard: Grade {0} Playername {1} FastestTime{2}",
                           // rankCounter, lb.userName, lb.highScore);
                    }

                    //change to descending order
                    //leaderBoardList.Reverse();

                    //for each simpleleaderboard object inside our leaderboardlist
                    foreach(SimpleLeaderBoard lb in leaderBoardList)
                    {
                        Debug.LogFormat("Leaderboard: Grade {0} Playername {1} FastestTime{2}",
                            lb.performanceGrade, lb.userName, lb.gameTimer);
                    }
                }
            }
        });

        return leaderBoardList;
    }

    public async Task<SimplePlayerStats> GetPlayerStats(string uuid)
    {
        Query q = dbPlayerStatsReference.Child(uuid);
        SimplePlayerStats playerStats = null;

        await dbPlayerStatsReference.GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error retrieving player stats: ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Child(uuid).Exists)
                {
                    //datasnapshot/playerstats/$uuid/<we want this values>
                    playerStats = JsonUtility.FromJson<SimplePlayerStats>(ds.Child(uuid).GetRawJsonValue());

                    Debug.Log("ds... :" + ds.GetRawJsonValue());
                    Debug.Log("player stats values.. " + playerStats.SimplePlayerStatsToJson());
                }
            }
        });

        return playerStats;
    }

    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
        dbLeaderboardsReference.Child(uuid).RemoveValueAsync();
    }

}
