using UnityEngine;

public class Upgrade : MonoBehaviour
{
    Animator animator;
    GameObject PlayerCharacter;
   
    ParticleSystem particlesystem;

    public Color ParticleColor1;
    public Color ParticleColor2;

    bool used;
    bool Epressed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter = GameObject.FindWithTag("PlayerCharacter");
        animator = PlayerCharacter.GetComponent<Animator>();
        particlesystem = GetComponent<ParticleSystem>();
        Epressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Epressed = true;
        }
        else Epressed = false;
    }

   
    public void OnTriggerStay(Collider obiect)
    {
        if (obiect.gameObject.CompareTag("Player"))
        {
            if (Epressed == true && used == false)
            {
                Debug.Log("LevelUp");
                animator.SetTrigger("Upgrade");

                particlesystem.Pause();
                used = true;
            }
            else if(Epressed == true && used == true)
            {
                Debug.Log("Already Used");
               
            }
        }
        
    }
    
}
