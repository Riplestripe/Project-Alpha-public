using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public bool isSlope;
    private CharacterController characterController;
    public Vector3 playerVelocity;
    public bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpheight = 3f;
    public bool crouching = false;
    public bool sprinting = false;
    private bool lerpCrouching = false;
    private float crouchTimer = 1f;
    public bool movementPressed;
    private Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    // Update is called once per frame
    void Update()
    {

        
        
        isGrounded = characterController.isGrounded;
        if (lerpCrouching)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            if(!crouching)
                characterController.height = Mathf.Lerp(characterController.height, 2, p  * 2);

            if(p > 1)
            {
                lerpCrouching = false;
                crouchTimer = 0f;
            }
        }

        if (isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hit, characterController.height * 0.5f + 0.3f))
            {
                var angle = Vector3.Angle(Vector3.up, hit.normal);
                Debug.DrawLine(hit.point, hit.point + hit.normal, Color.black, 3f);
                if (angle > characterController.slopeLimit)
                {
                    isSlope = true;

                    var normal = hit.normal;
                    var yIneverse = 1f - normal.y;
                    playerVelocity.x += yIneverse * normal.x;
                    playerVelocity.z += yIneverse * normal.z;
                }
                else if (angle <= characterController.slopeLimit)
                {
                    isSlope = false;
                    playerVelocity.x = 0;
                    playerVelocity.z = 0;

                }
            }

        }

    }

    

    // Применение ввода из скрипта InputManager.cs на персонажа
    public void ProcessMove(Vector2 input)
    {
        moveDir.x = input.x;
        moveDir.z = input.y;
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        { 
            playerVelocity.y = -2f; 
        }
        
        characterController.Move(playerVelocity * Time.deltaTime);
        
        if(moveDir.x != 0 ||  moveDir.z != 0)
        {
            movementPressed = true;
        }
        else movementPressed = false;

    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpheight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouching = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8f;
        else
            speed = 5;
        
    }
  
        
    
}
