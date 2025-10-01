using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float speed;
    Vector3 oldPosition;
    

    [HideInInspector] public float walkSpeed;
    public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode drawWeapon = KeyCode.F;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded, isRunning;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    AnimationMenager animationManager;

    Animator animator;

    EquipmentSystem equipment;

    private void Start()
    {
        equipment = GetComponent<EquipmentSystem>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animationManager = GetComponentInChildren<AnimationMenager>();
        animator = GameObject.Find("Baba").GetComponent<Animator>();
        readyToJump = true;

        oldPosition = new Vector3(0,0,0);
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        speed = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;

        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            animationManager.Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKey(sprintKey))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if (Input.GetKeyUp(drawWeapon) && animator.GetBool("SwordEquip") == false)
        {
            animationManager.EquipSword();
           
            Debug.Log("Dobywasz miecz");

        }
        else
        if (Input.GetKeyUp(drawWeapon) && animator.GetBool("SwordEquip") == true)
        {
            animationManager.UnequipSword();
          
            Debug.Log("Chowasz miecz");

        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
        {
            

            if (isRunning)
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force );

                animator.SetBool("isRunning", true);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

                animator.SetBool("isRunning", false);
            }
        }
        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}