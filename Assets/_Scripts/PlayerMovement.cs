using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    [SerializeField] private float gravity = -9.8f;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [SerializeField] bool shouldFaceMoveDirection = false;




    [HideInInspector] public float walkSpeed;
    public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode drawWeapon = KeyCode.F;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public bool isRunning;


    public Transform cameraTransform;

    float horizontalInput;
    float verticalInput;


    private CharacterController controller;

    AnimationManager animationManager;

    Animator animator;

    private Vector3 velocity;
    EquipmentSystem equipment;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        equipment = GetComponent<EquipmentSystem>();
        walkSpeed = speed;
        animationManager = GetComponentInChildren<AnimationManager>();
        animator = GameObject.Find("Baba").GetComponent<Animator>();
        readyToJump = true;


    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        if (GameManager.Instance.currentState == GameState.Normal)
        {
            MovePlayer();
        }
        
    }



    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            animationManager.Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // handle sprinting
        if (Input.GetKey(sprintKey))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // weapon draw / sheathe
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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        if (isRunning && grounded)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        // ruch względem kamery
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;
        controller.Move(moveDirection * speed * Time.deltaTime);

        // obrót w stronę ruchu
        if (shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // grawitacja i skok
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    //private void SpeedControl()
    //{
    //    Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    //    // limit velocity if needed
    //    if(flatVel.magnitude > moveSpeed)
    //    {
    //        Vector3 limitedVel = flatVel.normalized * moveSpeed;
    //        rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
    //    }
    //}

    private void Jump()
    {
        // reset y velocity
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}