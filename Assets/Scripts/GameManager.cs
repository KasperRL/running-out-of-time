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
        } else {
            Cursor.visible = true;
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
        AudioListener.pause = false;
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        isGameOver = true;
        Cursor.visible = true;
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
        yield return new WaitForSeconds(11f);
        MainMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        Destroy(GameObject.Find("Quest Manager"));

        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        AudioListener.pause = false;
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;
        Cursor.visible = true;
    }
}
