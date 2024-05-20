using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;
    private PlayerMovement movement;
    private PlayerLook look;
    private Animator animator;
    public Vector2 moveDirection;
    public bool walkPressed;
    public bool runPressed;
    public bool jumpPressed;
    public bool ShootPressed;
    bool isJumping;
    public bool crouchPressed;
    public PlayerSounds playerSounds;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerInput = new PlayerInput();
        player = playerInput.Player;
        look = GetComponent<PlayerLook>();
        animator = GetComponent<Animator>();
        player.Shoot.performed += ctx => ShootPressed = ctx.ReadValueAsButton();
        player.Movement.performed += ctx => this.moveDirection = ctx.ReadValue<Vector2>();
        player.Movement.performed += ctx => walkPressed = true;
        player.Movement.canceled += ctx => walkPressed = false;
        player.Movement.canceled += ctx => this.moveDirection = Vector2.zero;
        player.Sprint.performed += ctx => ctx.ReadValueAsButton();
        player.Jump.performed += ctx => isJumping = true;
    }

    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>()); 
    }
    private void Update()
    {
        //if (walkPressed)
        //{
        //    animator.SetBool("isWalking", true);
        //}
        //if(player.Sprint.IsPressed())
        //{
        //    animator.SetBool("isRunning", true);
        //}
        //if(!walkPressed)
        //{
        //    animator.SetBool("isWalking", false);
        //}
        //if(!player.Sprint.IsPressed())
        //{
        //    animator.SetBool("isRunning", false);

        //}
        //if (player.Jump.triggered || player.Jump.IsPressed())
        //{
        //    animator.SetBool("isGrounded", false);

        //    animator.SetBool("isJumping", true);

        //}
        //if (movement.grounded)
        //{
        //    animator.SetBool("isGrounded", true);
        //    animator.SetBool("isJumping", false);

        //}
        if(moveDirection != Vector2.zero)
        {
            playerSounds.PlayFootsteps();
        }
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    private void OnFootstep()
    {
        playerSounds.PlayFootsteps();
    }

    private void OnLand()
    {
        playerSounds.PlayFootsteps();
    }
   
  
}
