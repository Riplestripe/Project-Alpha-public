using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : Interactble
{
    public GameObject playerRef;
    float playerHeight;
    public bool isDraging;
    private Rigidbody rb;
    public GameObject grabpoint;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("MainCamera");
        grabpoint = GameObject.FindGameObjectWithTag("DragPoint");
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        playerHeight = playerRef.transform.position.y;
        if (isDraging)
        {
            rb.useGravity = false;
            rb.freezeRotation = true;
            this.rb.MovePosition(grabpoint.transform.position);
        }
        else
        {   rb.useGravity = true;
            rb.freezeRotation = false;
        }

    }
    protected override void Interact()
    {
        isDraging = !isDraging;
        
       
    }
}
