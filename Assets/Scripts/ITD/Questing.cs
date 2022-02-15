using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questing : MonoBehaviour
{
    public GameObject bandageBody;
    public GameObject cprBody;

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

    public void PatientQuestStatus()
    {
        burnTreated = burnScript.burnComplete;
        cutTreated = bandagingScript.bandageComplete;
        cprTreated = cprScript.cprComplete;

        if(burnTreated && cutTreated && cprTreated)
        {
            paitentQuestComplete = true;
            Debug.Log("paitientComp" + gameTimer);

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

        }
    }

    public void StartGameTimer()
    {
        if (!paitentQuestComplete || !packingQuestComplete)
        {
            gameTimer += Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bandageBody.gameObject.SetActive(false);
        cprBody.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartGameTimer();
    }
}
