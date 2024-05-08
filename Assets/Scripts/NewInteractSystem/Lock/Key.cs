using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour
{
    public bool hovered;
    private RaycastHit hit;
    public KeyToOpen keyToOpen;
    public GameObject Lock;
    public InputManager InputManager;
    public GameObject objRef;
    public InventoryManager inventoryManager;
    public DestroyHandler destroyHandler;
    public GameObject DH;
    public GameObject itemHolder;
    private void Start()
    {
        objRef = this.gameObject;
    }
    private void CompareKeys()
    {
        if (objRef == keyToOpen.key.gameObject && Lock.GetComponent<LockInteract>().lockActive)
        {   for(int i = 0; i < inventoryManager.itemSlot.Length; i++)
            {
                if (inventoryManager.itemSlot[i].itemName == objRef.name)
                {
                    inventoryManager.itemSlot[i].GetComponent<ItemSlot>().ClearSlot();
                }
            }
            Lock.GetComponent<LockInteract>().lockActive = false;
            Lock.GetComponent<LockInteract>().DisableKey();
            Lock.transform.SetParent(DH.transform);
            transform.SetParent(DH.transform);
           

        }
    }

    private void Update()
    {
        if(InputManager.player.Shoot.triggered && hovered)
        {
            CompareKeys();
        }
        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.5f);
        Vector3 oldPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(EventSystem.current.IsPointerOverGameObject() || Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("key") && hit.collider.gameObject == objRef)
            {
                objRef.gameObject.GetComponent<Key>().hovered = true;
            }
            else hovered = false;

        }

        if (hovered)
        {
            objRef.transform.localPosition = newPos;
        }
        else objRef.transform.localPosition = oldPos;
    }

    
}
