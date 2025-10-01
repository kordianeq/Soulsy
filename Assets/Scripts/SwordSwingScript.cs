using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingScript : MonoBehaviour
{

    public GameObject Sword;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordAnim());
        }
    }

    IEnumerator SwordAnim()
    {
        Sword.GetComponent<Animator>().Play("SwordAnim");
        yield return new WaitForSeconds(0.66f);
        Sword.GetComponent<Animator>().Play("Default");
    }
}