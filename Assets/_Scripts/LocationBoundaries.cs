using UnityEngine;

public class LocationBoundaries : MonoBehaviour
{
    public PlayerLocation location; // lokalizacja przypisana do tej granicy
    private void OnTriggerEnter(Collider other)
    {
        if (other == null || GameManager.Instance == null)
            return;

        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.playerLocation != location)
            {
                GameManager.Instance.playerLocation = location;
                GameManager.Instance.UpdatePlayerLocation();
            }
        }
    }
}
