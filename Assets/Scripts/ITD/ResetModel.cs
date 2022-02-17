using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModel : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public List<Transform> originalTransformList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            originalTransformList.Add(itemList[i].transform);
            Debug.Log(itemList[i].transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int j = 0; j < itemList.Count; j++)
        {
            if (collision.gameObject == itemList[j])
            {
                itemList[j].transform.position = new Vector3(originalTransformList[j].transform.position.x,
                    originalTransformList[j].transform.position.y, originalTransformList[j].transform.position.x);
                Debug.Log("pls reset");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
