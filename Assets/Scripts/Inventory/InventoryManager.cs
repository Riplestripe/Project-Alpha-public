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
    private GameObject cam;
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
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) hotbarCount = 1;
        if (!isMenuActive)
        {
           

            if (Input.mouseScrollDelta.y > 0)
            {
                for (int i = 0; i < cam.transform.childCount; i++)
                {
                    cam.transform.GetChild(i).gameObject.SetActive(false);
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
                            itemsInHand.SetActive(true);
                            
                        }
                    }
                    if (itemSlot[i].hotbarIndex != hotbarCount && itemSlot[i].isThisItemSelected == true) {

                        itemSlot[i].isThisItemSelected = false;
                            };

                }

            }
            
        }
        
        if(isMenuActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossheir.SetActive(false);
            textpromt.SetActive(false);
        }
        else
        {
            playerLook.isLocked = false;
            Cursor.lockState = CursorLockMode.Locked;
            crossheir.SetActive(true);
            textpromt.SetActive(true);
           
        }
        InventoryMenu.SetActive(isMenuActive);

        if (inputManager.player.Inventory.triggered)
        {
            isMenuActive = !isMenuActive;
            playerLook.isLocked = !playerLook.isLocked;
            if (isMenuActive)
            {
                for (int i = 0; i < itemSlot.Length; i++)
                {
                    if (itemSlot[i].isFull == true && itemSlot[i].hotbar)
                    {
                        itemSlot[i].isThisItemSelected = true;
                        DeSelectSlots();
                    }
                }
            }
            
        }
    }

    public void AddItem(string itemName, Vector3 scale, string message, int quantity, Sprite itemSprite, string itemDescription, ItemSlot.Type type, GameObject objRef)
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
