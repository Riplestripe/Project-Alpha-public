using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteract : Interactble
{
    private bool doorOpen;
    private Animator animator;
    public bool isDoorClosed = false;
    private InventoryManager inventoryManager;
    private GameObject inventoryCanvas;

    private void Awake()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory");
        inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
        animator = GetComponent<Animator>();

    }

   
    protected override void Interact()
    {
        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            if (inventoryManager.itemSlot[i].itemName == "Rock")
            {
                isDoorClosed = false;

            }

        }

        if (isDoorClosed == false)
        {
            doorOpen = !doorOpen;

            if (!doorOpen)
            {

                animator.SetBool("doorOpen", false);
            }
            if (doorOpen)
            {
                animator.SetBool("doorOpen", true);

            }

        }

    }

}
