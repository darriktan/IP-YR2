using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Questing : MonoBehaviour
{
    public GameManager theGameManager;

    public Burns burnScript;
    public Bandaging bandagingScript;
    public CPR cprScript;
    public PackingQuest packingQuestScript;

    public bool burnTreated;
    public bool cutTreated;
    public bool cprTreated;

    public bool burnBoxReady;
    public bool sprainBoxReady;
    public bool cutBoxReady;
    public bool brokeBoxReady;

    public float gameTimer = 0f;

    public bool paitentQuestComplete = false;
    public bool packingQuestComplete = false;

    public TextMeshProUGUI packingQuestStatus;
    public double minuteTimer;
    public float secTimer;
    public TextMeshProUGUI timeUiMin;
    public TextMeshProUGUI timeUiSec;

    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI timetakenText;

    public string performanceGrade;
    public GameObject endGameUI;

    public void PatientQuestStatus()
    {
        burnTreated = burnScript.burnComplete;
        cutTreated = bandagingScript.bandageComplete;
        cprTreated = cprScript.cprComplete;

        if(burnTreated && cutTreated && cprTreated)
        {
            paitentQuestComplete = true;
            Debug.Log("paitientComp" + gameTimer);
            GameStatus();
        }
    }

    public void PackingQuestStatus()
    {
        burnBoxReady = packingQuestScript.burnPacked;
        sprainBoxReady = packingQuestScript.sprainPacked;
        cutBoxReady = packingQuestScript.cutPacked;
        brokeBoxReady = packingQuestScript.brokenPacked;

        if (burnBoxReady && sprainBoxReady && cutBoxReady && brokeBoxReady)
        {
            packingQuestComplete = true;
            Debug.Log("packingComp" + gameTimer);
            packingQuestStatus.text = "Complete";
            GameStatus();
        }
    }

    public void StartGameTimer()
    {
        if (!paitentQuestComplete || !packingQuestComplete)
        {
            gameTimer += Time.deltaTime;
        }
        minuteTimer = Math.Floor(gameTimer / 60);
        secTimer = gameTimer % 60;
        timeUiMin.text = minuteTimer.ToString("00");
        timeUiSec.text = "  :" + secTimer.ToString("00");
        if (secTimer >= 55 && minuteTimer >= 5)
        {
            timeUiMin.color = Color.red;
            timeUiSec.color = Color.red;
        }
        else if(secTimer >= 55)
        {
            timeUiMin.color = Color.yellow;
            timeUiSec.color = Color.yellow;
        }
        else
        {
            timeUiMin.color = Color.white;
            timeUiSec.color = Color.white;
        }
    }

    public void GameStatus()
    {
        if (paitentQuestComplete && packingQuestComplete)
        {
            if (gameTimer <= 360)
            {
                performanceGrade = "A";
            }
            else if (gameTimer <= 420)
            {
                performanceGrade = "B";
            }
            else if (gameTimer <= 480)
            {
                performanceGrade = "C";
            }
            else if (gameTimer <= 570)
            {
                performanceGrade = "D";
            }
            else
            {
                performanceGrade = "F";
            }
            //display grade and time on UI
            gradeText.text = performanceGrade;
            timetakenText.text = minuteTimer.ToString("00") + "minute" + secTimer.ToString("00") + "seconds";
            endGameUI.gameObject.SetActive(true);
            theGameManager.GameOver();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartGameTimer();
    }
}
