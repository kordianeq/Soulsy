using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particleSystem;

    public Color ParticleColor1;
    public Color ParticleColor2;

    private bool used = false;
    private bool ePressed = false;

    private void Start()
    {
        if (animator == null)
        {
            GameObject playerCharacter = GameObject.FindWithTag("Player");
            if (playerCharacter != null)
                animator = playerCharacter.GetComponent<Animator>();
        }

        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        ePressed = Input.GetKeyDown(KeyCode.E);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null || animator == null)
            return;

        if (other.CompareTag("Player") && ePressed && !used)
        {
            animator.SetTrigger("Upgrade");
            if (particleSystem != null)
                particleSystem.Pause();
            used = true;
        }
    }
    
}
