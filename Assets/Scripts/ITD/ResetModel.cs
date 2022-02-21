using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModel : MonoBehaviour
{
    public GameObject resetSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ResetColliders")
        {
            collision.gameObject.transform.position = new Vector3(resetSpawn.gameObject.transform.position.x,
            resetSpawn.gameObject.transform.position.y, resetSpawn.gameObject.transform.position.x);
            Debug.Log("pls reset");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
