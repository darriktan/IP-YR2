using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModel : MonoBehaviour
{
    public GameObject resetSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = new Vector3(resetSpawn.transform.position.x,
            resetSpawn.transform.position.y, resetSpawn.transform.position.x);
        Debug.Log("pls reset");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
