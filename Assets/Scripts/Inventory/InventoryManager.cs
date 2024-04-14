using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public bool isMenuActive;
    private InputManager inputManager;
    private GameObject Player;
    private GameObject itemHolder;
    public ItemSlot[] itemSlot;
    public bool isCursorActive = false;
    public PlayerLook playerLook;
    public GameObject crossheir;
    public GameObject textpromt;
    public bool isfull;
    public int hotbarCount = 0;
    public  GameObject itemsInHand = null;
    public GameObject itemsOutHand = null;
    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
        inputManager = Player.GetComponent<InputManager>();
        itemHolder = GameObject.FindGameObjectWithTag("PlayerUI");
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) hotbarCount = 1;
        if (!isMenuActive)
        {
           

            if (Input.mouseScrollDelta.y > 0)
            {
                for (int i = 0; i < itemHolder.transform.childCount; i++)
                {
                    itemHolder.transform.GetChild(i).gameObject.SetActive(false);
                }
                DeSelectSlots();    
                
                hotbarCount++;
                if (hotbarCount >= 10) hotbarCount = 1;
                for (int i = 0; i < itemSlot.Length; i++)
                {


                    if (itemSlot[i].hotbarIndex == hotbarCount)
                    {
                        itemSlot[i].isThisItemSelected = true;
                        if (itemSlot[i].isThisItemSelected == true)
                        {

                            itemsInHand = itemSlot[i].objRef;
                            itemsInHand.transform.localPosition = Vector3.zero;
                            itemsInHand.transform.localScale = Vector3.one;
                            itemsInHand.SetActive(true);
                            
                        }
                    }
                    if (itemSlot[i].hotbarIndex != hotbarCount && itemSlot[i].isThisItemSelected == true) {

                        itemSlot[i].isThisItemSelected = false;
                    }

                }

            }
            
        }
        
        if(isMenuActive)
        {
            crossheir.SetActive(false);
            textpromt.SetActive(false);
        }
        else
        {
            crossheir.SetActive(true);
            textpromt.SetActive(true);
           
        }
        InventoryMenu.SetActive(isMenuActive);

        if (inputManager.player.Inventory.triggered)
        {
            isMenuActive = !isMenuActive;
            if (isMenuActive)
            {
                playerLook.isLocked = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                for (int i = 0; i < itemSlot.Length; i++)
                {
                    if (itemSlot[i].isFull == true && itemSlot[i].hotbar)
                    {
                        itemSlot[i].isThisItemSelected = true;
                        DeSelectSlots();
                    }
                }
            }
            else
            {
                playerLook.isLocked = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }


        }
        }

    public void AddItem(string itemName, Vector3 scale, string message, int quantity, Sprite itemSprite, string itemDescription, Item.Type type, GameObject objRef)
    {
        for (int i = 0; i < itemSlot.Length; i++) 
        {
            if ((itemSlot[i].isFull == false && itemSlot[i].hotbar) || itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, scale, message, quantity, itemSprite, itemDescription, type , objRef);
                return;


            }
        }
    }

    public void DeSelectSlots()
    {
        for(int i = 0;i < itemSlot.Length; i++)
       {
            itemSlot[i].selectShader.SetActive(false);
            itemSlot[i].isThisItemSelected = false;
           
        }
   }
}
