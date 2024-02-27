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
    public GameObject player;
    private InputManager inputManager;
    public float trowStr = 5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager =player.GetComponent<InputManager>();
        playerRef = GameObject.FindGameObjectWithTag("MainCamera");
        grabpoint = GameObject.FindGameObjectWithTag("DragPoint");
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        playerHeight = playerRef.transform.position.y;
        if (isDraging)
        {
            float lerpSpeed = 10f;
            rb.useGravity = false;
            rb.freezeRotation = true;
           Vector3 newPos = Vector3.Lerp(transform.position, grabpoint.transform.position,Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPos);
        }
        else
        {   rb.useGravity = true;
            rb.freezeRotation = false;
        }

        if(isDraging && inputManager.player.Trow.triggered)
        {
            isDraging = false;
            rb.useGravity = true;
            rb.freezeRotation = false;
            rb.AddForce(grabpoint.transform.forward * trowStr, ForceMode.Impulse);

        }

    }
    protected override void Interact()
    {
        isDraging = !isDraging;
        
       
    }
}
