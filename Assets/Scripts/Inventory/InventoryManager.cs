using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip soundOpen = null;
    public AudioClip soundClose = null;
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
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isCursorActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }

        if (inputManager.player.Inventory.triggered)
        { 
            isMenuActive = !isMenuActive;            
            isCursorActive = !isCursorActive;
            if(isMenuActive)
            {
                audioSource.PlayOneShot(soundClose);

            }
            else audioSource.PlayOneShot(soundOpen);

        }
        if (isMenuActive)
        {
            Player.GetComponent<PlayerLook>().isLocked = true;
            InventoryMenu.SetActive(true);
        }
        else
        {
            Player.GetComponent<PlayerLook>().isLocked = false;
            InventoryMenu.SetActive(false);
        };
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
