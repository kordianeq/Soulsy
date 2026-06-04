using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool playerInRange = false;


    void Update()
    {
        if (playerInRange)
        {
            if (InputManager.Instance.interactPressed && GameManager.Instance.currentState != GameState.Checkpoint)
            {
                InputManager.Instance.interactPressed = false; 
                
                GameManager.Instance.CheckpointReached();
            }
            else
            if (InputManager.Instance.interactPressed && GameManager.Instance.currentState == GameState.Checkpoint)
            {
                GameManager.Instance.CheckpointExit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            var ineractText = GameManager.Instance.uiManager.interactText;
            ineractText.gameObject.SetActive(true);
            ineractText.text ="Press E to interact";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInRange = false;
             GameManager.Instance.uiManager.interactText.gameObject.SetActive(false);
        }
    }
}

