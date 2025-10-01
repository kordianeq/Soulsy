using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //[SerializeField] GameObject UI;
 
    public GameObject interactionUIPrefab; // Reference to the keybind icon UI prefab
    private GameObject interactionUI; // Reference to the instantiated UI element

    bool interactable = false;

    void Start()
    {
       
    }

    void Update()
    {
        // Check if the player is within the interaction area
        // You might want to customize the condition based on your collider and player setup
        if (Input.GetKeyDown(KeyCode.E) && interactable == true) // Replace with your desired key
        {
            // Perform the interaction logic here
            Debug.Log("Player interacted with NPC!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player in collision");
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            interactable = true;
            Debug.Log("Player in collision");
        }
    }

  
   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = false;
        }
    }

}

