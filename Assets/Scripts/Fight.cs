using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Fight : MonoBehaviour
{
    Animator animator;
    public GameObject animatorRef;
    bool isSwordEquiped;
    // Start is called before the first frame update
    void Start()
    {
        animator = animatorRef.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isSwordEquiped = animator.GetBool("SwordEquip");
        //Debug.Log(isSwordEquiped);
        
        
        if (isSwordEquiped == true && Input.GetKeyDown(KeyCode.Mouse0) )
        {
            animator.SetTrigger("Punch");
            Debug.Log("Punch");
        }
        
        if(isSwordEquiped == true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("CastSpell");
        }
    }
}
