using UnityEngine;


public class Stats : MonoBehaviour
{
    [SerializeField] private AnimationManager animationManager;
    private UiManager uiManager;
    public int health = 100;
    public int maxHealth = 100;
    public int level = 1;

    [Header("I-Frames (Invulnerability)")]
    private bool hasIFrames = false;
    private float iFramesEndTime = 0f;

    private void Start()
    {
        uiManager.healthBar.value = (float)maxHealth;
        if (animationManager == null)
            animationManager = GetComponentInChildren<AnimationManager>();

        uiManager = GameManager.Instance.uiManager;
    }

    void Awake()
    {
        uiManager = GameManager.Instance.uiManager;
    }

    private void Update()
    {
        if(health != uiManager.healthBar.value * maxHealth)
            uiManager.healthBar.value = (float)health/ maxHealth;
        
        // Check if I-frames have expired
        if (hasIFrames && Time.time >= iFramesEndTime)
        {
            hasIFrames = false;
        }
    }

    /// <summary>
    /// Activate invulnerability frames (I-frames) for specified duration
    /// </summary>
    public void StartIFrames(float duration)
    {
        iFramesEndTime = Time.time + duration;
        hasIFrames = true;
    }
    public void EndIFrames()
    {
        hasIFrames = false;
    }

    /// <summary>
    /// Check if player is currently invulnerable
    /// </summary>
    public bool IsInvulnerable()
    {
        return hasIFrames;
    }

    /// <summary>
    /// Take damage with I-frames protection
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        if (IsInvulnerable())
        {
            Debug.Log($"Player blocked {damageAmount} damage due to I-frames!");
            return;
        }

        health -= damageAmount;

        Debug.Log($"Player took {damageAmount} damage! Health: {health}/{maxHealth}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        health = 0;
        if (animationManager != null)
            animationManager.Death();
        Debug.Log("Player died!");
    }

    public void Checkpoint()
    {
        if (animationManager != null)
            animationManager.SitOnCheckpoint();

        health = maxHealth;
        Debug.Log("Player reached a checkpoint! Health restored.");
    }

    /// <summary>
    /// Heal the player
    /// </summary>
    public void Heal(int healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        Debug.Log($"Player healed for {healAmount}! Health: {health}/{maxHealth}");
    }

    /// <summary>
    /// Restore health to full
    /// </summary>
    public void FullRestore()
    {
        health = maxHealth;
        Debug.Log("Player fully restored!");
    }
}
