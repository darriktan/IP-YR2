using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;

public class GameManager : MonoBehaviour
{
    public FirebaseAuth auth;
    public SimpleAuthManager authMgr;

    public Questing theQuestingScript;

    //float currentTime = 0f;
    //float startTime = 60f;

    //public List<GameObject> targets;
    //private float spawnRate = 1f;

    //public TextMeshProUGUI scoreText;
    //private int score;
    //public TextMeshProUGUI gameOverText;
    //public TextMeshProUGUI countDownText;
    //public GameObject restartButton;
    //public GameObject backToMainButton;
    //public bool isGameActive;

    public SimpleFirebaseManager firebaseMgr;
    public bool isPlayerStatUpdated;
    //public int xpPerGame = 5;

    public void Awake()
    {
        InitializeFirebase();
    }

    public void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    //IEnumerator SpawnTarget()
    //{
    //    while (isGameActive)
    //    {
    //        yield return new WaitForSeconds(spawnRate);
    //        int index = Random.Range(0, targets.Count);
    //        Instantiate(targets[index]);
    //    }
    //}

    //IEnumerator GameComplete()
    //{
    //    yield return new WaitForSeconds(startTime);
    //    GameOver();
    //}

    //public void UpdateScore(int scoreToAdd)
    //{
    //    score += scoreToAdd;
    //    scoreText.text = "Score: " + score;
    //}

    public void GameOver()
    {
        //isGameActive = false;
        if (!isPlayerStatUpdated)
        {
            //UpdatePlayerStat(this.theQuestingScript.gameTimer, xpPerGame, 30);
        }
        isPlayerStatUpdated = true;
    }

    public void UpdatePlayerStat(int score, int xp, int time)
    {
        Debug.Log("hi " + auth.CurrentUser.DisplayName);
        //firebaseMgr.UpdatePlayerStats(auth.CurrentUser.UserId, score, xp, time, authMgr.GetCurrentUserDisplayName());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlayerStatUpdated = false;
    }
}