using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering;
public class Item : Interactble
{
    
    [SerializeField]
    public ItemSlot.Type type;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    public Mesh itemMesh;
     
    [SerializeField]
    public Material itemMaterial;

    [SerializeField]
    public string message;

    [SerializeField]
    public Vector3 scale;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    [Header("Item position")]
    private InventoryManager inventoryManager;
    InputManager inputManager;
    public GameObject grabpoint;
    Rigidbody rb;
    public float time;
    bool inHands;
    PlayerLook PlayerLook;
    private new GameObject camera;

    [Header("Rotate object")]
    public float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    [Header("Gun")]
    public PickUpGun PickUpGun;

    public GameObject objRef;
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        grabpoint = GameObject.FindGameObjectWithTag("DragPoint");
        rb = GetComponent<Rigidbody>();
        PlayerLook = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        message = promtMessage;
        scale = this.transform.localScale;
        objRef = this.gameObject;
    }
    private void Update()
    {
        
        if (inHands)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (_isRotating)
            {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // apply rotation
                _rotation.z = -_mouseOffset.x * _sensitivity;
                _rotation.x = _mouseOffset.y * _sensitivity;


                // rotate
                transform.Rotate(_rotation);

                // store mouse
                _mouseReference = Input.mousePosition;
            }
                time -= Time.deltaTime;
                float lerpSpeed = 10f;
                rb.useGravity = false;
                rb.freezeRotation = true;
                camera.transform.LookAt(this.transform.position);
                Vector3 newPos = Vector3.Lerp(transform.position, grabpoint.transform.position, Time.deltaTime * lerpSpeed);
                rb.MovePosition(newPos);
            
            if (time <= 0)
            {
                time = 0;

                if (inputManager.player.Interact.triggered)
                {
                    inventoryManager.AddItem(itemName,scale, message, quantity, itemSprite, itemDescription, type, objRef);
                    Destroy(gameObject);
                    time = 5f;
                    inHands = false;
                }
                
            }
            

        }
        if (!inHands)
        {
            time = 5f;
            rb.useGravity = true;
            rb.freezeRotation = false;
        }
    }

    protected override void Interact()
    {
       
        if (type != ItemSlot.Type.Note)
        {
            inventoryManager.AddItem(itemName,scale, message, quantity, itemSprite, itemDescription, type, objRef);
            inventoryManager.itemsInHand = objRef;
            inventoryManager.itemsInHand.GetComponent<PickUpGun>().PickUp();
            gameObject.SetActive(false);
         }

        if (type == ItemSlot.Type.Note)
        {
            if (time > 0)
            {
                inHands = !inHands;
            }
            transform.rotation = quaternion.Euler(-70, 0, 0);

        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

}
