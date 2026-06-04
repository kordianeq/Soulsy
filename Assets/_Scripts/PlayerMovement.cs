using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    [SerializeField] private float gravity = -9.8f;
    public float groundDrag;
    [SerializeField] private float deceleration = 0.15f;  // How quickly to lose momentum (0-1)

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [SerializeField] bool shouldFaceMoveDirection = false;

    [HideInInspector] public float walkSpeed;
    public float sprintSpeed;

    [Header("Dodge Mechanics (Backstep/Roll)")]
    public float dodgeCooldown = 0.5f;
    public float dodgeDuration = 1.2f;
    public float rollDistance = 5f;
    public float backstepDistance = 3f;
    public float backstepDuration = 0.3f;

    public float iFrameProcentage = 0.5f; // Procent animacji rolla, podczas którego gracz ma I-frames

    private float lastDodgeTime = -1f;
    public bool isDodging = false;
    private Vector3 currentMoveDirection = Vector3.zero;  // Stored movement direction with inertia

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public bool isRunning;

    [Header("References")]
    public Transform cameraTransform;
    [SerializeField] private Animator animator;

    float horizontalInput;
    float verticalInput;

    private CharacterController characterController;
    AnimationManager animationManager;
    Stats stats;

    private Vector3 velocity;
    EquipmentSystem equipment;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        equipment = GetComponent<EquipmentSystem>();
        stats = GetComponent<Stats>();
        walkSpeed = speed;
        animationManager = GetComponentInChildren<AnimationManager>();
        
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

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

        HandleJumpInput();
        HandleDodgeInput();
        HandleWeaponInput();
    }

    private void MyInput()
    {
        // Block input during dodge to prevent steering
        if (isDodging)
        {
            horizontalInput = 0f;
            verticalInput = 0f;
        }
        else
        {
            // Czytaj move input z InputManager
            Vector2 moveInput = InputManager.Instance.moveInput;
            horizontalInput = moveInput.x;
            verticalInput = moveInput.y;

            // Sprint hold detection
            if (InputManager.Instance.isSprintHeld && grounded && moveInput.magnitude > 0.1f)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        }

        if(InputManager.Instance.emotePressed)
        {
            if (animationManager != null)
                animationManager.Emote(true);
            
        }

        if(characterController.velocity.magnitude > 0.1f)
        {
            if (animationManager != null)
                animationManager.Emote(false);
        }
    }
   
    private void HandleJumpInput()
    {
        if (InputManager.Instance.jumpPressed && readyToJump && grounded)
        {
            Jump();
        }
    }

    private void HandleDodgeInput()
    {
        if (InputManager.Instance.dodgePressed && !isDodging && CanDodge())
        {
            InputManager.Instance.dodgePressed = false; // Konsumuj input

            if (characterController.velocity.magnitude > 0.1f)
            {
                lastDodgeTime = Time.time;
                StartCoroutine(RollCoroutine());
            }
            else
            {
                lastDodgeTime = Time.time;
                StartCoroutine(BackstepCoroutine());
            }
        }

        
    }

    private void HandleWeaponInput()
    {
        if (InputManager.Instance.drawWeaponPressed)
        {
            InputManager.Instance.drawWeaponPressed = false; // Konsumuj input
            HandleWeaponToggle();
        }
    }

    private void HandleWeaponToggle()
    {
        if (animator == null)
            return;

        if (animator.GetBool("SwordEquip") == false)
        {
            animationManager.EquipSword();
            Debug.Log("Dobywasz miecz");
        }
        else
        {
            animationManager.UnequipSword();
            Debug.Log("Chowasz miecz");
        }
    }

    private bool CanDodge()
    {
        return Time.time >= (lastDodgeTime + dodgeCooldown);
    }

    private IEnumerator BackstepCoroutine()
    {
        isDodging = true;

        // Trigger animation
        if (animator != null)
            animator.Play("Backstep");

        if (animationManager != null)
            animationManager.BackstepStart();

        // Backstep direction = Camera backward (opposite of forward) - FIXED at start
        Vector3 backstepDirection = -characterController.transform.forward;
        backstepDirection.y = 0;
        backstepDirection.Normalize();

        float elapsedTime = 0f;
        while (elapsedTime < backstepDuration)
        {
            elapsedTime += Time.deltaTime;

            // Movement during backstep (no I-frames)
            Vector3 backstepMove = backstepDirection * (backstepDistance / backstepDuration) * Time.deltaTime;
            characterController.Move(backstepMove);

            yield return null;
        }

        if (animationManager != null)
            animationManager.BackstepEnd();

        isDodging = false;
    }

    private IEnumerator RollCoroutine()
    {
        isDodging = true;

        // Trigger animation
        if (animator != null)
            animator.Play("Roll");

        if (animationManager != null)
            animationManager.RollStart();
        if (stats != null)
            stats.StartIFrames(dodgeDuration * iFrameProcentage); // I-frames for part of the roll duration
        
        Vector3 rollDirection = characterController.transform.forward;
        rollDirection.y = 0;
        rollDirection.Normalize();

        float elapsedTime = 0f;
        while (elapsedTime < dodgeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Movement during backstep (no I-frames)
            Vector3 rollMove = rollDirection * (rollDistance / dodgeDuration) * Time.deltaTime;
            characterController.Move(rollMove);

            yield return null;
        }

        if (animationManager != null)
            animationManager.RollEnd();

        isDodging = false;
    }


    
    private void MovePlayer()
    {
        if (isDodging)
            return; // Don't move normally while dodging


        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // reset gravity when grounded
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isRunning && grounded)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        // Calculate desired movement direction
        Vector3 desiredMoveDirection = forward * verticalInput + right * horizontalInput;

        // Apply inertia/deceleration - smoothly transition to new direction
        currentMoveDirection = Vector3.Lerp(currentMoveDirection, desiredMoveDirection, deceleration);

        // obrót w stronę ruchu
        if (shouldFaceMoveDirection && currentMoveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(currentMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // grawitacja i skok
        velocity.y += gravity * Time.deltaTime;

        // Jedno wywołanie Move dla całego wektora ruchu - używamy currentMoveDirection z inercją
        Vector3 finalVelocity = (currentMoveDirection * speed) + velocity;
        characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        readyToJump = false;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        animationManager.Jump();
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}