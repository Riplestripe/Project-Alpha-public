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
    public ItemSlot[] hotBar;
    public bool isCursorActive = false;
    public PlayerLook playerLook;
    public GameObject crossheir;
    public GameObject textpromt;
    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
        inputManager = Player.GetComponent<InputManager>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
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
                
                audioSource.PlayOneShot(soundClose);
                
            }
            else audioSource.PlayOneShot(soundOpen);
            


        }

    }

    public void AddItem(string itemName, Mesh itemMesh, Material itemMaterial, Vector3 scale, string message, int quantity, Sprite itemSprite, string itemDescription, ItemSlot.Type type)
    {

        for (int i = 0; i < itemSlot.Length; i++) 
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemMesh, itemMaterial, scale, message, quantity, itemSprite, itemDescription, type);
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
