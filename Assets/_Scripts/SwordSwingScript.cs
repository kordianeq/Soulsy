using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingScript : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    [SerializeField] private float animationDuration = 0.66f;
    private Animator swordAnimator;

    private void Start()
    {
        if (Sword != null)
            swordAnimator = Sword.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && swordAnimator != null)
        {
            StartCoroutine(SwordAnim());
        }
    }

    private IEnumerator SwordAnim()
    {
        swordAnimator.Play("SwordAnim");
        yield return new WaitForSeconds(animationDuration);
        swordAnimator.Play("Default");
    }
}