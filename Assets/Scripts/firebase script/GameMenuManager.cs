using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public FirebaseAuth auth;

    public SimpleAuthManager authMgr;
    public DatabaseReference dbClanReference;
    public GameObject logoutBtn;

    //ui
    public TextMeshProUGUI blueCounter;
    public TextMeshProUGUI redCounter;
    public TextMeshProUGUI displayName;

    public void Awake()
    {
        InitializeFirebase();
        SetClanMembersCount();

        displayName.text = "Smasher: " + auth.CurrentUser.DisplayName;
    }

    public void InitializeFirebase()
    {
        dbClanReference = FirebaseDatabase.DefaultInstance.GetReference("clans");
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SetClanMembersCount()
    {
        long blueClanMembers = 0;
        long redClanMembers = 0;

        dbClanReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error in retrieving clan information, ERROR: " + task.Exception);
                return;
            }
            else if(task.IsCompleted)
            {
                DataSnapshot clanSnapshot = task.Result;
                if (clanSnapshot.Exists)
                {
                    blueClanMembers = clanSnapshot.Child("blue").ChildrenCount;
                    redClanMembers = clanSnapshot.Child("red").ChildrenCount;

                    blueCounter.text = blueClanMembers.ToString();
                    redCounter.text = redClanMembers.ToString();

                    Debug.LogFormat("Number of red {0} blue {1} members", redClanMembers, blueClanMembers);
                }
            }
        });   
    }

    public void LogOut()
    {
        authMgr.LogoutUser();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToProfile()
    {
        SceneManager.LoadScene(4);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
