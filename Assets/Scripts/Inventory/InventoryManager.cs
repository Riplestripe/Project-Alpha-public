using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public bool isMenuActive;
    private InputManager inputManager;
    private GameObject Player;
    public ItemSlot[] itemSlot;
    private bool isCursorActive = false;
    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
        inputManager = Player.GetComponent<InputManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isCursorActive)
        {
            Cursor.visible = true;
        }
        else Cursor.visible = false;

        if(inputManager.player.Inventory.triggered)
        { 
            isMenuActive = !isMenuActive;            
            isCursorActive = !isCursorActive;
        }
        if(isMenuActive)
        {
            InventoryMenu.SetActive(true);
        }
        else InventoryMenu.SetActive(false);
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {

        for (int i = 0; i < itemSlot.Length; i++) 
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
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
