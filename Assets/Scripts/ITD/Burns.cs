using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burns : MonoBehaviour
{
    public GameObject theBurnBody;
    public GameObject theBandageBody;

    public GameObject waterSocket;
    public GameObject ointmentSocket;
    public GameObject burnGauzeSocket;

    public bool burnComplete = false;

    public void AddedWater()
    {
        waterSocket.gameObject.SetActive(false);
        ointmentSocket.gameObject.SetActive(true);
        Debug.Log("hi");
    }

    public void AddedOintment()
    {
        ointmentSocket.gameObject.SetActive(false);
        burnGauzeSocket.gameObject.SetActive(true);
    }

    public void AddedBurnGauze()
    {
        burnGauzeSocket.gameObject.SetActive(false);
        burnComplete = true;
        theBurnBody.gameObject.SetActive(!burnComplete);
        theBandageBody.gameObject.SetActive(burnComplete);
    }

    // Start is called before the first frame update
    void Start()
    {
        ointmentSocket.gameObject.SetActive(false);
        burnGauzeSocket.gameObject.SetActive(false);
    }
}
