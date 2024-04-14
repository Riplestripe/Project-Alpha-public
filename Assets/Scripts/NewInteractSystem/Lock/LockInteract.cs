using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteract : Interactble
{
    public GameObject player;
    public Camera playerCam;
    public Transform lookPoint;
    public bool lockActive;
    public PlayerLook playerLook;
    public GameObject[] KeySlots;
    public InventoryManager inventoryManager;
    public GameObject itemHolder;
    [SerializeField] Transform freeSlot;
    public GameObject KeyUI;
    private void Update()
    {
        if (lockActive) playerLook.isLocked = true;
        FindFreeSlot();
        EnableKey();
    }
    protected override void Interact()
    {
        playerCam.gameObject.GetComponent<HeadbobSystem>().enabled = false;
        playerCam.transform.parent = lookPoint.transform.parent;
        playerLook.enabled = false;
        playerCam.transform.localRotation = Quaternion.Euler(90, 0, 0);
        playerCam.transform.position = lookPoint.position;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        lockActive = true;
        KeyUI.SetActive(true);
        KeyFinder();




    }

    private void EnableKey()
    {
        for (int i = 0; i < KeySlots.Length; i++)
        {
            if (KeySlots[i].GetComponent<isFull>().Full)
            { 
                KeySlots[i].transform.GetChild(i).localScale = Vector3.one;
                KeySlots[i].transform.GetChild(i).gameObject.SetActive(true);
                KeySlots[i].transform.GetChild(i).transform.localPosition = Vector3.zero;
                KeySlots[i].transform.GetChild(i).gameObject.GetComponent<Key>().enabled = true;
            }
        }
    }

    private void KeyFinder()
    {
        for (int i = 0; i < itemHolder.transform.childCount; i++)
        {
            itemHolder.transform.GetChild(i).gameObject.transform.SetParent(freeSlot);

        }
        
    }

    private void FindFreeSlot()
    {
        for (int i = 0; i < KeySlots.Length; i++)
        {
            if (!KeySlots[i].gameObject.GetComponent<isFull>().Full) freeSlot = KeySlots[i].transform;
            return;
        }
    }
}

