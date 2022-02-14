using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPR : MonoBehaviour
{
    public Questing questingScript;

    public GameObject cprColliders;

    public int collisionCount = 0;
    public int validCprAttempt = 0;
    public bool cprComplete = false;

    public float tooFast = 0.5f;
    public float tooSlow = 0.6f;
    public int cprGoal = 3;

    public bool useTimer;
    public float cprTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oh man");
        if (other.gameObject.tag == "FirstCollider")
        {
            if (collisionCount == 0) //if you touch collider1
            {
                collisionCount += 1;
                Debug.Log("First Collider" + other.gameObject.name);
                cprTime = 0; //when touch collider set timer to 0, 
                useTimer = true; //start the timer
            }
            else if (collisionCount == 1) //if you touch collider1 again without touching collider2
            {
                Debug.Log("First Collider" + other.gameObject.name);
                cprTime = 0; //when touch collider set timer to 0, 
                useTimer = true; //start the timer
            }
            else if (collisionCount == 2) //if you touch collider1 after collider2
            {
                collisionCount = 0;
                Debug.Log("First Collider" + other.gameObject.name); 
                useTimer = false; //when touch the second collider stop the time
                Debug.Log(cprTime);
                CheckValidCPR();
            }
        }
        else if (other.gameObject.tag == "SecondCollider")
        {
            if (collisionCount == 1) //if you touch collider2 after collider1
            {
                collisionCount += 1;
                Debug.Log("Second Collider" + other.gameObject.name);
            }
        }
    }

    public void StartTimer()
    {
        if(useTimer)
        {
            cprTime += 1 * Time.deltaTime;
        }
    }

    public void CheckValidCPR()
    {
        if(cprTime >= tooFast && cprTime <= tooSlow)
        {
            validCprAttempt += 1;
            if(validCprAttempt == cprGoal)
            {
                cprComplete = true;
                cprColliders.gameObject.SetActive(!cprComplete);
                questingScript.PatientQuestStatus();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }
}
