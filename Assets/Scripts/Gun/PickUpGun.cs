using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpGun : MonoBehaviour
{
    public Gun gunScript = null;
    Rigidbody rb;
    public Collider colliders;
    public Transform player, weaponeHolder, playerUI;
    private InputManager inputManager;
    public float dropForwardForce, dropUpwardForce;
    public InventoryManager inventoryManager;
    public bool equiped;
    public static bool slotFull;
    private Item scale;
    private void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponeHolder = GameObject.FindGameObjectWithTag("WeaponHolder").transform;
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<Collider>();
        scale = GetComponent<Item>();
        
        if (!equiped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
        }
        if(equiped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
        }
        
    }  
      
    

    public void PickUp()
    {
        transform.position = weaponeHolder.position;
        transform.parent = weaponeHolder;
        transform.rotation = playerUI.transform.rotation;
        transform.localScale = Vector3.one;
        transform.SetParent(playerUI);
        equiped = true;
        slotFull = true;
        rb.isKinematic = true;
        colliders.isTrigger = true;
        gunScript.enabled = true;
        
    }
    public void Drop()
    {
        equiped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.AddForce(playerUI.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(playerUI.forward * dropUpwardForce, ForceMode.Impulse);
        transform.localScale = scale.scale;
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random));
        rb.isKinematic = false;
        colliders.isTrigger = false;
        gunScript.enabled = false;
    }

}
