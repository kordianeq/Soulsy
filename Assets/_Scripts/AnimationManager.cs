using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //References
    Animator animator;
    GameObject player;
    PlayerMovement playerMovement;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("SwordEquip", false);

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();


    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetBool("isRunning", playerMovement.isRunning);

        animator.SetFloat("Speed", playerMovement.speed);

        //Debug.Log("Speed: " + speed.ToString("F2"));


        //if(Input.GetKeyUp(KeyCode.Alpha1))
        //{

        //}
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void EquipSword()
    {
        animator.SetBool("SwordEquip", true);
    }
    public void UnequipSword()
    {
        animator.SetBool("SwordEquip", false);
    }
    public void CastSpell()
    {


    }
    public void Death()
    {
        animator.SetTrigger("Death");
    }
    public void SitOnCheckpoint()
    {
        animator.SetBool("isSitting", true);
    }

    public void StandUpFromCheckpoint()
    {
        animator.SetBool("isSitting", false);
    }

}
