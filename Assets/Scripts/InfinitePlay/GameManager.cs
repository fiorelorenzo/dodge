using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public static GameManager Instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public TextMeshProUGUI pointsText;
    public float countDown;
    public TextMeshProUGUI countDownText;
    public Text pauseButtonText;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject joystick;
    public TextMeshProUGUI finalPointsText;

    private long gamePoints = 0;
    private float timeToStart;
    private bool isGamePaused;
    private bool inputEnabled;
    private bool exitingPause;
    private bool isGameOver;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        //If instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        countDownText.gameObject.SetActive(true);
        pauseButtonText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        isGamePaused = true;
        exitingPause = true;
        inputEnabled = false;
        isGameOver = false;
        Time.timeScale = 0;

        timeToStart = countDown;
        pointsText.SetText(gamePoints.ToString());
    }

    private void Update()
    {
        if (exitingPause)
        {
            if (timeToStart <= 0.9f)
            {
                countDownText.gameObject.SetActive(false);
                inputEnabled = true;
                exitingPause = false;
                isGamePaused = false;
                Time.timeScale = 1;
                pauseButtonText.gameObject.SetActive(true);
            }
            else
            {
                timeToStart -= Time.unscaledDeltaTime;
                countDownText.gameObject.SetActive(true);
                countDownText.SetText(timeToStart.ToString("0"));
            }
        }
    }

    public bool IsPlayerAlive()
    {
        return (player != null);
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public bool IsInputEnabled()
    {
        return inputEnabled;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void AddPoints(int points)
    {
        gamePoints += points;
        pointsText.SetText(gamePoints.ToString());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        inputEnabled = false;
        isGamePaused = true;
        exitingPause = false;
        pauseButtonText.text = "Play";
        pauseMenuPanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        timeToStart = countDown;
        exitingPause = true;
        countDownText.gameObject.SetActive(true);
        pauseButtonText.text = "Pause";
        pauseMenuPanel.SetActive(false);
        //enable the scripts again
    }

    public void GameOver()
    {
        // TODO: save score

        isGameOver = true;
        pointsText.gameObject.SetActive(false);
        joystick.SetActive(false);
        pauseButtonText.gameObject.SetActive(false);
        finalPointsText.SetText("{0} points!", gamePoints);
        gameOverPanel.SetActive(true);
    }

    public void TogglePuase()
    {
        if (isGamePaused)
            ContinueGame();
        else
            PauseGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}