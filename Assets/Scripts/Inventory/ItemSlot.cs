 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Vector3 offset;
    //=======ITEM DATA========//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public enum Type { Default, Consumable, Weapon, Note }
    public Type type = Type.Default;
    public string message;
    public Vector3 scale;
    public GameObject objRef;
    //=======ITEM SLOT========//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Sprite emptySlot;
    //=======ITEM DESCRIPTION=======//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;


    public GameObject selectShader;
    public bool isThisItemSelected;

    private InventoryManager inventoryManager;
    private InputManager inputManager;
    public GameObject grabpoint;
    Vector3 oldPos;
    public bool hoverd = false;
    public Transform parent;
    private Transform rootParent;
    public bool hotbar;
    public int hotbarIndex = -1;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }
    private void Update()
    {
        if (isThisItemSelected && inputManager.player.Trow.triggered) DropItem();
        if (isThisItemSelected)
        {
            selectShader.SetActive(true);
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
        }
    }
    public void AddItem(string itemName, Vector3 scale, string message, int quantity, Sprite itemSprite, string itemDescription, Type type, GameObject objRef)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.type = type;
        this.objRef = objRef;
        this.message = message;
        this.scale = scale;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

   
    private void OnRightClick()
    {
        DropItem();

    }

    public void DropItem()
    {
       objRef.SetActive(true);
        objRef.GetComponent<PickUpGun>().Drop();
        ClearSlot();
        isFull = false;


    }

    void ClearSlot()
    {
        quantityText.enabled = false;
        this.itemName = null;
        this.quantity = 0;
        this.itemSprite = null;
        this.itemDescription = null;
        this.type = default;
        this.objRef = null;
        this.message = null;
        itemDescriptionNameText.text = null;
        itemDescriptionText.text = null;
        itemDescriptionImage.sprite = emptySlot;
        isFull = false;
        itemImage.sprite = emptySlot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPos = this.transform.position;
        gameObject.GetComponent<Image>().raycastTarget = false;
        transform.SetAsFirstSibling();
        if (hotbar)
        {
            rootParent = transform.parent;
            transform.SetParent(parent);
            transform.SetAsFirstSibling();

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        GameObject draggeble = eventData.pointerDrag.gameObject;
        ItemSlot itemSlot = draggeble.GetComponent<ItemSlot>();
        if (itemSlot.isFull == true)
        {

            this.hoverd = false;
            this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = true;
        this.transform.position = oldPos;
        if (hotbar)
        {
            transform.SetParent(rootParent);

        }

    }

    public void OnDrop(PointerEventData eventData)
    {

        GameObject itemDropped = eventData.pointerDrag;
        ItemSlot itemData = itemDropped.GetComponent<ItemSlot>();
        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            if (inventoryManager.itemSlot[i].hoverd == true)
            {
                if (itemData.isFull && !inventoryManager.itemSlot[i].isFull)
                {
                    inventoryManager.itemSlot[i].AddItem(itemData.itemName, itemData.scale, itemData.message, itemData.quantity, itemData.itemSprite, itemData.itemDescription, itemData.type, itemData.objRef);
                    itemData.ClearSlot();
                }
                else return;
            }
        }


    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        hoverd = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverd = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();

        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        inventoryManager.DeSelectSlots();
        isThisItemSelected = true;


    }

   
}

    
