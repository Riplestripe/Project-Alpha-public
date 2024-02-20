using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;

    private PlayerMovement movement;
    private PlayerLook look;

    private InventoryManager inventoryManager;
    Animator animator;
    public bool walkPressed;
    public bool runPressed;
    public bool jumpPressed;
    public bool ShootPressed;
    bool isJumping;
    public bool crouchPressed;

    private void Awake()
    {
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        
        playerInput = new PlayerInput();
        player = playerInput.Player;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        player.Jump.performed += ctx => movement.Jump();
        player.Crouch.performed += ctx => movement.Crouch();
        player.Sprint.performed += ctx => movement.Sprint();
        player.Sprint.performed += ctx => runPressed = ctx.ReadValueAsButton();
        player.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        player.Shoot.performed += ctx => ShootPressed = ctx.ReadValueAsButton();
    }

    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>()); 
    }
    void FixedUpdate()
    {
        bool isRunneing = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool isJumping = animator.GetBool("isJumping");
        bool inFreeFall = animator.GetBool("inFreeFall");
        bool isGrounded = movement.isGrounded;

        if (!jumpPressed)
        {
            animator.SetBool("isJumping", false);

        }
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("inFreeFall", false);
            animator.SetBool("isJumping", false);


        }
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("inFreeFall", true);
        }
        
        if (movement.movementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        if(!movement.movementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
        if((movement.movementPressed && isWalking && !isRunneing) && runPressed)
        {
            animator.SetBool("isRunning", true);
        }

        if((movement.movementPressed && isWalking && isRunneing) && !runPressed)
        {
            animator.SetBool("isRunning", false);
        }

        if(!movement.movementPressed && (isWalking ||  isRunneing) && !runPressed)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);

        }
        if(jumpPressed && !isJumping && isGrounded)
        {
            
            animator.SetBool("isJumping", true);
        }

        if(isGrounded && isJumping)
        {
            animator.SetBool("isJumping", false);
        }
        

        //передача действия из PlayerMovement используя значения из карты
        movement.ProcessMove(player.Movement.ReadValue<Vector2>());
      
    }
    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }
}
