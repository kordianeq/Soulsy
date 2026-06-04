using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private Soulsy_Controls playerControls;

    // ===== INPUT STATE - PUBLIC (czytane przez inne skrypty) =====
    [HideInInspector] public Vector2 moveInput = Vector2.zero;
    [HideInInspector] public bool jumpPressed = false;
    [HideInInspector] public bool dodgePressed = false;
    [HideInInspector] public bool drawWeaponPressed = false;
    [HideInInspector] public bool attackPressed = false;
    [HideInInspector] public bool interactPressed = false;

    [HideInInspector] public bool emotePressed = false;

    // Sprint hold detection
    [HideInInspector] public bool isSprintHeld = false;
    private float dodgePressStartTime = 0f;
    private bool dodgeButtonDown = false;
    public float dodgeInputThreshold = 0.2f;
    public float sprintHoldDuration = 0.5f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicjalizuj Input System
        playerControls = new Soulsy_Controls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        // Subscribe do Input Actions
        playerControls.Player.Move.performed += OnMovePerformed;
        playerControls.Player.Move.canceled += OnMoveCanceled;

        playerControls.Player.Jump.performed += OnJumpPerformed;
        playerControls.Player.Jump.canceled += OnJumpCanceled;

        playerControls.Player.Dodge.performed += OnDodgePressed;
        playerControls.Player.Dodge.canceled += OnDodgeReleased;

        playerControls.Player.DrawWeapon.performed += OnDrawWeaponPerformed;

        // Walka
        playerControls.Player.Attack.performed += OnAttackPerformed;
        
        playerControls.Player.Interact.performed += OnInteractPerformed;
        playerControls.Player.Emote.performed += OnEmotePerformed;
        playerControls.Player.Emote.canceled += OnEmoteCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe
        playerControls.Player.Move.performed -= OnMovePerformed;
        playerControls.Player.Move.canceled -= OnMoveCanceled;

        playerControls.Player.Jump.performed -= OnJumpPerformed;
        playerControls.Player.Jump.canceled -= OnJumpCanceled;

        playerControls.Player.Dodge.performed -= OnDodgePressed;
        playerControls.Player.Dodge.canceled -= OnDodgeReleased;

        playerControls.Player.DrawWeapon.performed -= OnDrawWeaponPerformed;

        // Walka
        playerControls.Player.Attack.performed -= OnAttackPerformed;
        playerControls.Player.Interact.performed -= OnInteractPerformed;
        playerControls.Player.Emote.performed -= OnEmotePerformed;
        playerControls.Player.Emote.canceled -= OnEmoteCanceled;
        playerControls.Disable();
    }

    private void Update()
    {
        // Sprint hold detection logic
        if (dodgeButtonDown)
        {
            float pressDuration = Time.time - dodgePressStartTime;
            if (pressDuration >= sprintHoldDuration)
            {
                isSprintHeld = true;
            }
        }
    }

    // ===== INPUT CALLBACKS =====

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpPressed = false;
    }

    private void OnDodgePressed(InputAction.CallbackContext context)
    {
        dodgePressStartTime = Time.time;
        dodgeButtonDown = true;
        
        isSprintHeld = false;
    }

    private void OnDodgeReleased(InputAction.CallbackContext context)
    {
        if (dodgeButtonDown)
        {
            float pressDuration = Time.time - dodgePressStartTime;

            // Tylko jeśli to było szybkie naciśnięcie (tap), a nie hold
            if (pressDuration < dodgeInputThreshold)
            {
                dodgePressed = true; // Sygnalizuj dodge
            }
            else
            {
                dodgePressed = false; // Nie był szybki tap
            }
        }

        dodgeButtonDown = false;
        isSprintHeld = false;
    }

    private void OnDrawWeaponPerformed(InputAction.CallbackContext context)
    {
        drawWeaponPressed = true;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        attackPressed = true;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        interactPressed = true;
    }

    private void OnEmotePerformed(InputAction.CallbackContext context)
    {
        emotePressed = true;
    }

    private void OnEmoteCanceled(InputAction.CallbackContext context)
    {
        emotePressed = false;
    }
    // ===== PUBLIC HELPERS =====

    /// <summary>
    /// Pobiera czy gracz się porusza (ma input)
    /// </summary>
    public bool IsMoving()
    {
        return moveInput.sqrMagnitude > 0.1f;
    }

    /// <summary>
    /// Reset input flags (np. po obsłużeniu)
    /// </summary>
    public void ResetInputFlags()
    {
        jumpPressed = false;
        dodgePressed = false;
        drawWeaponPressed = false;
        attackPressed = false;
        interactPressed = false;
        emotePressed = false;
    }
}
