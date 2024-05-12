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
    float childs;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        childs = transform.parent.childCount;
    }
    private void Update()
    {
        if (childs == transform.parent.childCount)
        {
            isDoorClosed = true;
        }
        else
        {
            isDoorClosed = false;
         
        }
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
