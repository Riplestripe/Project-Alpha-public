using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHandler : MonoBehaviour
{
    public GameObject[] objectsToDestroy = null;
    
    private void Start()
    {
        if(objectsToDestroy != null)
        {
            objectsToDestroy = null;
        }
    }
    private void Update()
    {
        int totalObj = gameObject.transform.childCount;
        objectsToDestroy = new GameObject[totalObj];
        for (int i = 0; i < totalObj; i++)
        {
            objectsToDestroy[i] = gameObject.transform.GetChild(i).gameObject;

        }

        DestroyObj();
    }
    public void DestroyObj()
    {
        if(objectsToDestroy.Length> 0f)
        {
            foreach(GameObject destroy in objectsToDestroy)
            {
                Destroy(destroy);
            }
        }
    }
}
