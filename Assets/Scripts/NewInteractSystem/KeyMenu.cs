using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyMenu : MonoBehaviour
{
    public GameObject[] keys;
    public GameObject[] KeySlots;
    public GameObject itemHolder;
    public LockInteract Lock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Lock.gameObject.GetComponent<LockInteract>().lockActive) 
        {
            KeyFinder();
            SetKeyInSlots();
            isFullhander();
            EnableKey();
        }
        if (!Lock.gameObject.GetComponent<LockInteract>().lockActive)
        {
            keys = null;
            this.gameObject.SetActive(false);
            for (int i = 0; i < KeySlots.Length; i++)
            {
                if (KeySlots[i].transform.childCount != 0)
                {
                    KeySlots[i].transform.GetChild(i).transform.SetParent(itemHolder.transform);
                }
            }
        }
    }
    private void EnableKey()
    {
        for (int i = 0; i < KeySlots.Length; i++)
        {
            keys[i].transform.localScale = Vector3.one;
            keys[i].transform.localPosition = Vector3.zero;
            keys[i].gameObject.GetComponent<Key>().enabled = true;
            keys[i].SetActive(true);
        }

    }
    private void KeyFinder()
    {
        int totalKeys = itemHolder.transform.childCount;
        keys = new GameObject[totalKeys];
        for (int i = 0; i < totalKeys; i++)
        {
            keys[i] = itemHolder.transform.GetChild(i).gameObject;

        }

    }
    private void SetKeyInSlots()
    {
        if (keys.Length != 0)
        {

            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].transform.SetParent(KeySlots[i].transform);
            }


        }
    }
    private void isFullhander()
    {
        if (Lock.gameObject.GetComponent<LockInteract>().lockActive)
        {
            for (int i = 0; i < KeySlots.Length; i++)
            {
                if (KeySlots[i].transform.childCount != 0)
                {
                    KeySlots[i].gameObject.GetComponent<isFull>().Full = true;
                }
                else return;
            }
        }
    }
}
