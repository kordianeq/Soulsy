using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public bool swordEquipped = false;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("AnimationManager: Animator not found!", gameObject);
            return;
        }

        animator.SetBool("SwordEquip", false);

        if (playerMovement == null)
            playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void Awake()
    {
        GameManager.Instance.animationManager = this;
    }
    private void Update()
    {
        if (animator == null || playerMovement == null)
            return;

        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetBool("isRunning", playerMovement.isRunning);
        animator.SetFloat("Speed", playerMovement.speed);
    }

    public void Jump()
    {
        if (animator != null)
            animator.SetTrigger("Jump");
    }

    public void EquipSword()
    {
        if (animator != null)
            animator.SetBool("SwordEquip", true);
            swordEquipped = true;
    }

    public void UnequipSword()
    {
        if (animator != null)
            animator.SetBool("SwordEquip", false);
            swordEquipped = false;
    }

    public void CastSpell()
    {
        if (animator != null)
            animator.SetTrigger("CastSpell");
    }


    public void Emote(bool value)
    {
        if (animator != null)
            animator.SetBool("Emote", value);
    }
    

    
    public void Death()
    {
        if (animator != null)
            animator.Play("Death");
    }

    public void SitOnCheckpoint()
    {
        if (animator != null)
            animator.SetBool("isSitting", true);
    }

    public void StandUpFromCheckpoint()
    {
        if (animator != null)
            animator.SetBool("isSitting", false);
    }

    public void RollStart()
    {
        // Hook for additional logic when roll begins (e.g., disable colliders, visual effects)
        if (animator != null)
        {
            animator.SetBool("isRolling", true);
        }
    }
    public void RollEnd()
    {
        // Hook for additional logic when roll ends (e.g., re-enable colliders, end visual effects)
        if (animator != null)
        {
            animator.SetBool("isRolling", false);
        }
    }



    public void BackstepStart()
    {
        // Hook for additional logic when backstep begins
        if (animator != null)
        {
            animator.SetBool("isBackstepping", true);
        }
    }

    public void BackstepEnd()
    {
        // Hook for additional logic when backstep ends
        if (animator != null)
        {
            animator.SetBool("isBackstepping", false);
        }
    }
}
