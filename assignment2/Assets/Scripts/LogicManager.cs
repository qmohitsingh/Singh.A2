using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LogicManager : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    private float countdown; // Changed to float to represent time in seconds
    public Text timerText;

    public TextMeshProUGUI gameStatusText;
    public TextMeshProUGUI messageText;

    public GameObject gameOverScreen;
    public GameObject mainMenu;

    public GameObject backgroundImage;

    public GameObject panel; 
    public Sprite winSprite;
    public Sprite loseSprite;

    private bool isGameOver = false;

    public PlayerController playerController;

    // Start the countdown when the game starts
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();

        countdown = 30f; // Set the countdown to 30 seconds initially
    }

    IEnumerator StartCountdown()
    {
        while (countdown > 0 && !isGameOver)
        {
            yield return null; // Wait for the next frame
            countdown -= Time.deltaTime; // Decrease countdown by time passed since the last frame
            UpdateTimerText();
        }

        //Debug.Log("StartCountdown: "+countdown);

        // Game over condition
        if (!isGameOver)
        {
            GameOver();
        }
    }

    void UpdateTimerText()
    {
        // Ensure countdown never goes below zero
        countdown = Mathf.Max(countdown, 0f);

        // Format the timer text to display minutes and seconds
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);

        // Clamp values to never go below zero
        minutes = Mathf.Clamp(minutes, 0, int.MaxValue);
        seconds = Mathf.Clamp(seconds, 0, int.MaxValue);

        timerText.text = "Timer:    "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [ContextMenu("Increase Score")]
    public void AddScore()
    {
        playerScore = playerScore + 1;
        scoreText.text = playerScore.ToString();
    }

    public void GameOver()
    {
        playerController.SetIsPlayerAlive(false);

        if (playerScore < 10) {
            gameStatusText.text = "You Lose";
            gameStatusText.color = Color.red; // Set the color to red for losing message
            messageText.text = "You couldn't collect all 10 coins in 30 seconds.";
            messageText.color = Color.red; // Set the color to red for losing message
            panel.GetComponent<Image>().sprite = loseSprite;
        }
        else {
            gameStatusText.text = "You Win";
            gameStatusText.color = Color.green; // Set the color to green for winning message
            messageText.text = "You collected all 10 coins in 30 seconds.";
            messageText.color = Color.green; // Set the color to green for winning message
            panel.GetComponent<Image>().sprite = winSprite; // Set panel image to win sprite
        }

        isGameOver = true;
        panel.SetActive(true);
        // scoreText.gameObject.SetActive(false);
        // timerText.gameObject.SetActive(false);
        backgroundImage.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.sceneLoaded += OnSceneLoadedForRestart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoadedForRestart(Scene scene, LoadSceneMode mode)
    {
        // Ensure that the loaded scene is the one we want to play
        if (scene.name == SceneManager.GetActiveScene().name)
        {
            // Unsubscribe from the sceneLoaded event to avoid multiple calls
            SceneManager.sceneLoaded -= OnSceneLoadedForRestart;

            // Play the game after the scene has finished loading
        }
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerController.SetIsPlayerAlive(true);
        initializeGameState();
    }

    public void initializeGameState() {
        mainMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        StartCoroutine(StartCountdown());
        backgroundImage.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
