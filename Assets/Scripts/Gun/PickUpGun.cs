using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpGun : Interactble
{
    public Gun gunScript;
    Rigidbody rb;
    public new BoxCollider collider;
    public Transform player, weaponeHolder, cam;
    private InputManager inputManager;
    public float dropForwardForce, dropUpwardForce;
    public InventoryManager inventoryManager;
    public bool equiped;
    public static bool slotFull;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponeHolder = GameObject.FindGameObjectWithTag("WeaponHolder").transform;
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        if (!equiped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            collider.isTrigger = false;
        }
        if(equiped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            collider.isTrigger = true;
        }
        
    }
    private void Update()
    {
        if (equiped && inputManager.player.Trow.triggered) Drop();
      
      
    }
    protected override void Interact()
    {
        if(!equiped && !slotFull)
        {
            PickUp();

        }
    }

    private void PickUp()
    {
        transform.position = weaponeHolder.position;
        transform.parent = weaponeHolder;
        transform.rotation = cam.transform.rotation;
        transform.localScale = Vector3.one;
        transform.SetParent(cam);
        equiped = true;
        slotFull = true;
        rb.isKinematic = true;
        collider.isTrigger = true;
        gunScript.enabled = true;
    }
    private void Drop()
    {
        equiped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.forward * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random));

        rb.isKinematic = false;
        collider.isTrigger = false;

        gunScript.enabled = false;
    }

}
