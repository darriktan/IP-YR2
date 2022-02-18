using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bandaging : MonoBehaviour
{
    public Questing questingScript;

    public List<GameObject> bandageList = new List<GameObject>();
    public List<GameObject> bcolliderList = new List<GameObject>();

    public GameObject theCutBody;
    public GameObject gauzeSocket;
    public GameObject bandageColliders;

    public int colliderNum = 0;
    public int bandageCount = 0;
    public bool bandageComplete = false;

    public TextMeshProUGUI cutQuestStatus;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bFirstCollider")
        {
            if (colliderNum == 0)
            {
                other.gameObject.SetActive(false);
                colliderNum += 1;
                Debug.Log(colliderNum);
            }
        }
        else if (other.tag == "bSecondCollider")
        {
            if (colliderNum == 1)
            {
                other.gameObject.SetActive(false);
                colliderNum += 1;
                Debug.Log(colliderNum);
            }
        }
        else if (other.tag == "bThirdCollider")
        {
            if (colliderNum == 2)
            {
                other.gameObject.SetActive(false);
                colliderNum += 1;
                Debug.Log(colliderNum);
            }
        }
        else if (other.tag == "bFourthCollider")
        {
            if (colliderNum == 3)
            {
                other.gameObject.SetActive(false);
                colliderNum = 0;
                bandageCount += 1;
                Debug.Log(colliderNum);
                Debug.Log(bandageCount);
                ShowBandage();
            }
        }
    }

    public void ShowBandage()
    {
        if(bandageCount <= 4)
        {
            bandageList[bandageCount-1].gameObject.SetActive(true);
            if (bandageCount < 4)
            {
                for (int i = 0; i < bcolliderList.Count; ++i)
                {
                    bcolliderList[i].gameObject.SetActive(true);
                }
            }
            else if (bandageCount == 4)
            {
                theCutBody.GetComponent<Bandaging>().bandageComplete = true;
                Debug.Log("bdone");
                questingScript.PatientQuestStatus();
                cutQuestStatus.text = "Complete";
            }
        }
    }

    public void AddedDisinfectant()
    {
        gauzeSocket.gameObject.SetActive(true);
        Debug.Log("hi");
    }

    public void AddedGauze()
    {
        bandageColliders.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        gauzeSocket.gameObject.SetActive(false);
        bandageColliders.gameObject.SetActive(false);
    }
}
