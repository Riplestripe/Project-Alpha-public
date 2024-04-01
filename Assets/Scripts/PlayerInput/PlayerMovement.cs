using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
    public InputManager InputManager;
    public TextMeshProUGUI speed;
    [Header("Movment")]
    private float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float groundDrag;
    public float climbSpeed;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public float fallmultiplayer = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Crouch")]
    public bool cantCrouching;
    private float crouchTimer = 1f;
    public bool lerpCrouching = false;
    public bool crouching;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    [Header("Keybinds")]
    public KeyCode jump = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode crouch = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whayIsGround;
    public bool grounded;
    public Transform orientation;

    [Header("SlipCheck")]
    public Vector2 wallCheck;
    public float slipSpeed = 10f;
    float horizontalInput, verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private CapsuleCollider capsulcollider;
    public MovementState state; 
    public enum MovementState
    {
        walking,
        sprinting,
        climbing,
        air,
        crouch
    }
    public bool climbing;
    private void Start()
    {
        InputManager = GetComponent<InputManager>();

        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        capsulcollider = GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        if (!grounded)
        {
            SlipChecker();
        }
        if (Physics.Raycast(transform.position, Vector3.up, out var hit2, playerHeight, ~LayerMask.GetMask("Player")))
        {
            if (hit2.collider != null)
            {
                cantCrouching = true;
            }

        }
        if (cantCrouching == true && crouching == true && hit2.collider == null)
        {
            cantCrouching = false;
        }
        if (lerpCrouching)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                capsulcollider.height = Mathf.Lerp(capsulcollider.height, 1, p);
            if (!crouching)
                capsulcollider.height = Mathf.Lerp(capsulcollider.height, 2, p * 2);

            if (p > 1)
            {
                lerpCrouching = false;
                crouchTimer = 0f;
            }
        }
       // speed.text = rb.velocity.magnitude.ToString();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whayIsGround);
        SpeedControl();
        StateHandler();
        MyInput();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;

        if(rb.velocity.y < 0)
        {
            rb.velocity += (fallmultiplayer - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
        } else if(rb.velocity.y > 0 && !Input.GetKey(jump))
        {
            rb.velocity += (lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;

        }

    }
    private void MyInput()
    {
        

        if (InputManager.player.Jump.triggered && readyToJump && grounded && !crouching)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (InputManager.player.Crouch.triggered)
        {
            Crouch();
        }
    }
    private void StateHandler()
    {
        if(climbing)
        {
            state = MovementState.climbing;
            movementSpeed = climbSpeed;
        }
        if(grounded && crouching)
        {
            state = MovementState.crouch;
            movementSpeed = crouchSpeed;
        }
        if (grounded && !crouching)
        {
            state = MovementState.walking;
            movementSpeed = walkSpeed;
        }
        if (grounded && Input.GetKey(sprint) && !crouching)
        {
            state = MovementState.sprinting;
            movementSpeed = sprintSpeed;
        }
        if (!grounded && !crouching)
        {
            state = MovementState.air;
        }
    }
    public void MovePlayer()
    {
        horizontalInput = InputManager.moveDirection.x;
        verticalInput = InputManager.moveDirection.y;
        moveDirection =  orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (OnSlope())
        {
            rb.AddForce(20f * movementSpeed * GetSlopeMoveDirection(), ForceMode.Force);
        }
        
        if (grounded) rb.AddForce(10F * movementSpeed * moveDirection.normalized, ForceMode.Force);
        else if(!grounded) rb.AddForce(10F * airMultiplier * movementSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    public void Crouch()
    {
        //crouching = !crouching;
        if (crouching && !cantCrouching)
        {
            crouching = false;
        }
        else crouching = true;
        crouchTimer = 0f;
        lerpCrouching = true;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0f;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    private bool SlipChecker()
    {
        RaycastHit hit;
        Vector3 spawnPoint = transform.position + Vector3.up * wallCheck.y;

        Vector3 forward = transform.forward * wallCheck.x;
        Vector3 back = -transform.forward * wallCheck.x;
        Vector3 right = transform.right * wallCheck.x;
        Vector3 left = -transform.right * wallCheck.x;

        Ray front_ray = new Ray(spawnPoint, forward);
        Ray back_ray = new Ray(spawnPoint, back);
        Ray left_ray = new Ray(spawnPoint, left);
        Ray right_ray = new Ray(spawnPoint, right);

        float dis = wallCheck.y;
        
        if(Physics.Raycast(front_ray, out hit, dis, whayIsGround))
        {
            HitForSlip(transform.forward);
            return true;
        }
        if(Physics.Raycast(back_ray, out hit, dis, whayIsGround) ||
            Physics.Raycast(right_ray, out hit, dis, whayIsGround) ||
            Physics.Raycast(left_ray, out hit, dis, whayIsGround))
        {
            HitForSlip(hit.normal);
            return true;
        }
        return false;
    }

    void HitForSlip(Vector3 slipDir)
    {
        if (state == MovementState.air)
        {
            rb.velocity = Vector3.zero;
            state = MovementState.walking;
        }

        rb.AddForce((slipDir * slipSpeed) + Vector3.down);
    }

   

    public void Teleport(Vector3 pos, Quaternion quaternion)
    {
        transform.position = pos;
        rb.velocity = Vector3.zero;
        Physics.SyncTransforms();
    }













}

