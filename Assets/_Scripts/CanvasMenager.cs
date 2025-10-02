using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenager : MonoBehaviour
{
    public Text timerText;
    public TextMeshProUGUI speed;

    GameObject player;
    PlayerMovement playerMovement; 
    bool isGamePaused;
    [SerializeField] GameObject PausePanel;

    [Header("Keybinds")]
    public KeyCode pauseButton = KeyCode.P;

    // Start is called before the first frame update
    private void Start()
    {
        PausePanel.SetActive(false);
        isGamePaused = false;

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    public void Update()
    {

        speed.text = "Speed:" + playerMovement.speed.ToString();

        if (Input.GetKeyUp(pauseButton) && isGamePaused == false)
        {
            PausePanel.SetActive(true);
            OnClickPause(true);
            isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        if (Input.GetKeyUp(pauseButton) && isGamePaused == true)
        {
            PausePanel.SetActive(false);
            OnClickPause(false);
            isGamePaused = false;
            MouseReset();
        }
    }
   

    public void OnClickPause(bool enablePause)
    {
        if (enablePause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void MouseReset()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
