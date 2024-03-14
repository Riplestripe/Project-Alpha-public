 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
    public Mesh itemMesh;
    public Material itemMaterial;
    public string message;
    public Vector3 scale;
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
    public GameObject grabpoint;
    Vector3 oldPos;
    Transform parentDrag;
    public bool hoverd = false;
    public Transform parent;
    public Transform canvasParent;
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
   
    public void AddItem(string itemName, Mesh itemMesh, Material itemMaterial, Vector3 scale, string message, int quantity, Sprite itemSprite, string itemDescription, Type type)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.type = type;
        this.itemMesh = itemMesh;
        this.itemMaterial = itemMaterial;
        this.message = message;
        this.scale = scale;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
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

    private void OnRightClick()
    {
        DropIten();

    }

    private void DropIten()
    {
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        itemToDrop.AddComponent<Rigidbody>();
        MeshRenderer material = itemToDrop.AddComponent<MeshRenderer>();
        MeshFilter mesh = itemToDrop.AddComponent<MeshFilter>();
        itemToDrop.transform.localScale = scale;
        itemToDrop.transform.position = grabpoint.transform.position;
        itemToDrop.AddComponent<BoxCollider>();
        itemToDrop.layer = LayerMask.NameToLayer("Usable");
        newItem.itemName = itemName;
        newItem.quantity = quantity;
        newItem.itemSprite = itemSprite;
        newItem.itemDescription = itemDescription;
        newItem.type = type;
        newItem.itemMesh = itemMesh;
        newItem.itemMaterial = itemMaterial;
        newItem.promtMessage = message;
        material.material = itemMaterial;
        mesh.mesh = itemMesh;

        isFull = false;

        ClearSlot();

    }

    private void OnLeftClick()
    {
        inventoryManager.DeSelectSlots();
        selectShader.SetActive(true);
        isThisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if(!isFull) itemDescriptionImage.sprite = emptySlot;


    }


    void ClearSlot()
    {
        quantityText.enabled = false;
        this.itemName = null;
        this.quantity = 0;
        this.itemSprite = null;
        this.itemDescription = null;
        this.type = default;
        this.itemMesh = null;
        this.itemMaterial = null;
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
                    inventoryManager.itemSlot[i].AddItem(itemData.itemName, itemData.itemMesh, itemData.itemMaterial, itemData.scale,
                       itemData.message, itemData.quantity, itemData.itemSprite, itemData.itemDescription, itemData.type);

                }
                else return;
            }
            }
           itemData.ClearSlot();
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        hoverd = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverd = false;
    }
}

    
