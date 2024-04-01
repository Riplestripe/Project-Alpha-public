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
    private PlayerLook look;
    private InventoryManager inventoryManager;
    PlayerMovement movement;
    public Vector2 moveDirection;
    public bool walkPressed;
    public bool runPressed;
    public bool jumpPressed;
    public bool ShootPressed;
    bool isJumping;
    public bool crouchPressed;

    private void Awake()
    {        
        playerInput = new PlayerInput();
        player = playerInput.Player;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        player.Shoot.performed += ctx => ShootPressed = ctx.ReadValueAsButton();
        player.Movement.performed += ctx => this.moveDirection = ctx.ReadValue<Vector2>();
        player.Movement.canceled += ctx => this.moveDirection = Vector2.zero;
        
    }

    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>()); 
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
