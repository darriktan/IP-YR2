using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Burns : MonoBehaviour
{
    public Questing questingScript;

    public GameObject ointmentSocket;
    public GameObject burnGauzeSocket;

    public bool burnComplete = false;

    public TextMeshProUGUI burnQuestStatus;

    public void AddedWater()
    {
        //waterSocket.gameObject.SetActive(false);
        ointmentSocket.gameObject.SetActive(true);
        Debug.Log("hi");
    }

    public void AddedOintment()
    {
        //ointmentSocket.gameObject.SetActive(false);
        burnGauzeSocket.gameObject.SetActive(true);
    }

    public void AddedBurnGauze()
    {
        //burnGauzeSocket.gameObject.SetActive(false);
        burnComplete = true;
        questingScript.PatientQuestStatus();
        burnQuestStatus.text = "Complete";
    }

    // Start is called before the first frame update
    void Start()
    {
        ointmentSocket.gameObject.SetActive(false);
        burnGauzeSocket.gameObject.SetActive(false);
    }
}
