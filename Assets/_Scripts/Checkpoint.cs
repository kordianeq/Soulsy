using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool playerInRange = false;


    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentState != GameState.Checkpoint)
            {
                GameManager.Instance.CheckpointReached();
            }
            else
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentState == GameState.Checkpoint)
            {
                GameManager.Instance.CheckpointExit();
            }
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
        }
    }
}

