using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteract : Interactble
{
    private bool doorOpen;
    private Animator animator;
    public bool isDoorClosed = false;
    public GameObject Lock;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        if (Lock.activeInHierarchy)
        {
            isDoorClosed = true;
        }
        else isDoorClosed = false;
    }

    protected override void Interact()
    {
        

        if (isDoorClosed == false)
        {
            doorOpen = !doorOpen;

            if (!doorOpen)
            {

                animator.SetBool("doorOpen", false);
            }
            if (doorOpen)
            {
                animator.SetBool("doorOpen", true);

            }

        }

    }

}
