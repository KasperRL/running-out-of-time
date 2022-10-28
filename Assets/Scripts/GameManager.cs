using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public static bool isPaused = false;
    public static bool isGameOver = false;
    
    void Awake()
    {
        // Make sure there is only one GameManager
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        // Hide the cursor when not in the main menu
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu" && !isGameOver)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        
        Unfreeze();

        // Hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        
        Freeze();
        
        // Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        isPaused = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        
        Freeze();

        // Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        isGameOver = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameCompleted()
    {
        StartCoroutine(ShowEndScene());
    }

    IEnumerator ShowEndScene()
    {
        SceneManager.LoadScene(3);
        yield return new WaitForSeconds(11f); // Wait for the end scene to finish
        MainMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        Destroy(GameObject.Find("Quest Manager"));

        // Hide UI menus
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        Unfreeze();

        // Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        isPaused = false;
        isGameOver = false;
    }

    void Freeze()
    {
        // Freeze game audio and time
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    void Unfreeze()
    {
        // Unfreeze game audio and time
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }
}
