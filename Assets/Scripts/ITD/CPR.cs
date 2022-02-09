using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPR : MonoBehaviour
{
    //public GameObject collider1;
    //public GameObject collider2;

    public int collisionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oh man");
        if (other.gameObject.tag == "FirstCollider")
        {
            collisionCount += 1;
            Debug.Log("First Collider" + other.gameObject.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
