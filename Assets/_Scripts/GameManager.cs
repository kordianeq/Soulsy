using UnityEngine;
public enum GameState
{
    Normal,
    Paused, Checkpoint, Dialogue, Cutscene,
    Dead,
}
public enum  PlayerLocation
{
    Dungeon1,
    Dungeon2,
    Hub,
    BossRoom,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton — tylko jeden GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // zachowaj przy zmianie sceny
        }
        else
        {
            Destroy(gameObject); // usuń duplikat
        }
    }
    public GameState currentState = GameState.Normal; // aktualny stan gry
    public PlayerLocation playerLocation = PlayerLocation.Hub; // lokalizacja gracza

    public AnimationManager animationManager; // referencja do AnimationManager
    public UiManager uiManager; // referencja do UiManager
    private void Start()
    {
        animationManager = GameObject.Find("Player").GetComponentInChildren<AnimationManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    public void UpdatePlayerLocation()
    {
        uiManager.UpdatePlayerLocationName(playerLocation.ToString());
    }
    public void CheckpointReached()
    {
        Debug.Log("Checkpoint reached!");
        uiManager.checkpointPanel.SetActive(true);
        currentState = GameState.Checkpoint;
        animationManager.SitOnCheckpoint();
    }
    public void CheckpointExit()
    {
        Debug.Log("Checkpoint exit!");
        uiManager.checkpointPanel.SetActive(false);
        currentState = GameState.Normal;
        animationManager.StandUpFromCheckpoint();
    }

    public void Pause()
    {
        currentState = GameState.Paused;
        uiManager.PauseGame();
        Time.timeScale = 0f; // zatrzymaj czas gry
    }
    public void Resume()
    {
        currentState = GameState.Normal;
        uiManager.ResumeGame();
        Time.timeScale = 1f; // wznow czas gry
    }


}