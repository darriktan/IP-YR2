using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class SimpleLeaderBoardManager : MonoBehaviour
{
    public SimpleFirebaseManager fbManager;
    public GameObject rowPrefab;
    public Transform tableContent;

    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        UpdateLeaderboardUI();
    }

    /// <summary>
    /// Get and Update Leaderboard UI
    /// </summary>
    public async void UpdateLeaderboardUI()
    {
        var leaderBoardList = await fbManager.GetLeaderboard(5);
        int rankCounter = 1;

        //clear all leaderboard entries in UI
        foreach(Transform item in tableContent)
        {
            Destroy(item.gameObject);
        }

        //create prefabs of our rows
        //assign each value from list to the prefab text content
        foreach(SimpleLeaderBoard lb in leaderBoardList)
        {
            Debug.LogFormat("Leader Mgr: Rank {0} Playername {1} Highscore {2}",
                rankCounter, lb.userName, lb.highScore);

            //create prefab in the position of tableContent
            GameObject entry = Instantiate(rowPrefab, tableContent);
            TextMeshProUGUI[] leaderBoardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();
            leaderBoardDetails[0].text = rankCounter.ToString();
            leaderBoardDetails[1].text = lb.userName;
            leaderBoardDetails[2].text = lb.highScore.ToString();

            rankCounter++;
        }
    }

    public void GoToGameMenu()
    {
        SceneManager.LoadScene(1);
    }
}
