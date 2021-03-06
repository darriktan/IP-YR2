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
    public GameObject logoutBtn;

    //ui
    public TextMeshProUGUI displayName;

    public void Awake()
    {
        InitializeFirebase();
        displayName.text = "Player: " + authMgr.GetCurrentUserDisplayName();
    }

    public void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }


    public void LogOut()
    {
        authMgr.LogoutUser();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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
