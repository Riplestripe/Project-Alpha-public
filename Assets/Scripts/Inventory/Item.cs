using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactble
{
    [SerializeField]
    public AudioClip sound;
     AudioSource audioSource;

    [SerializeField]
    private string itemname;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

    }

    protected override void Interact()
    {
        inventoryManager.AddItem(itemname, quantity, sprite, itemDescription);
        audioSource.PlayOneShot(sound);
        Destroy(gameObject);
    }

}
