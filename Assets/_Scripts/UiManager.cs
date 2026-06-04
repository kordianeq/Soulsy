using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    public GameObject checkpointPanel;
    public Slider healthBar;
    public Slider staminaBar;

    public TextMeshProUGUI interactText;
    [Header("Texts")]
    public List<TextMeshProUGUI> playerLocationNameText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterUi(this);
        }
        else
        {
            Debug.LogError("Nie mogłem znaleźć GameManager.Instance!");
        }
    }
    private void Update()
    {
       
    }

    public void UpdatePlayerLocationName(string locationName)
    {
        if (playerLocationNameText == null || playerLocationNameText.Count == 0)
            return;

        foreach (var textElement in playerLocationNameText)
        {
            if (textElement != null)
                textElement.text = locationName;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    
}
