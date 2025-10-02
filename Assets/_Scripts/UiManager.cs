using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    public GameObject checkpointPanel;

    [Header("Texts")]
    public List<TextMeshProUGUI> playerLocationNameText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
       
    }

    public void UpdatePlayerLocationName(string locationName)
    {
        foreach (var playerLocationNameText in playerLocationNameText)
        {
            playerLocationNameText.text = locationName;
        }
            
    }
    public void PauseGame()
    {

    }
    public void ResumeGame()
    {

    }
}
