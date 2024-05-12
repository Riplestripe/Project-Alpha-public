using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;

public class LockInteract : Interactble
{
    public GameObject player;
    public Camera playerCam;
    public Transform lookPoint;
    public bool lockActive;
    public PlayerLook playerLook;
    public InventoryManager inventoryManager;
    public GameObject KeyUI;
    PlayerMovement playerMovement;
    private void Start()
    {

        playerMovement = player.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (lockActive)
        {
            playerCam.transform.localRotation = Quaternion.Euler(90, 0, 0);
            playerLook.isLocked = true;
            
        }
     
        //if(freeSlot != null) EnableKey();

    }
    protected override void Interact()
    {
        playerCam.gameObject.GetComponent<HeadbobSystem>().enabled = false;
        playerCam.transform.parent = lookPoint.transform.parent;
        playerLook.enabled = !playerLook.enabled;
        playerCam.transform.localRotation = Quaternion.Euler(90, 0, 0);
        playerCam.transform.position = lookPoint.position;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        lockActive = !lockActive;
        playerMovement.enabled = false;
        KeyUI.SetActive(true);
 
        if (!lockActive)
        {
            lockActive = false;
            DisableKey();
        }

    }

    
    public void DisableKey()
    {   
        
            playerMovement.enabled = true;
            playerCam.transform.SetParent(player.transform);
            Cursor.visible = false;
            playerCam.gameObject.GetComponent<HeadbobSystem>().enabled = true;
            playerLook.isLocked = false;
        
    }

 
}

