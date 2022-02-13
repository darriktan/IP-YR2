using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandaging : MonoBehaviour
{
    public List<GameObject> bandageList = new List<GameObject>();
    public List<GameObject> bcolliderList = new List<GameObject>();

    public int colliderNum = 0;
    public int bandageCount = 0;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
