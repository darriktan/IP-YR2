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

    public bool burnTreated;
    public bool cutTreated;
    public bool cprTreated;

    public float gameTimer = 0f;

    public bool paitentQuestComplete = false;

    public void PatientQuestStatus()
    {
        burnTreated = burnScript.burnComplete;
        cutTreated = bandagingScript.bandageComplete;
        cprTreated = cprScript.cprComplete;

        if(burnTreated && cutTreated && cprTreated)
        {
            paitentQuestComplete = true;
            Debug.Log("questComp"+ gameTimer);

        }
    }

    public void StartGameTimer()
    {
        if (!paitentQuestComplete)
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
