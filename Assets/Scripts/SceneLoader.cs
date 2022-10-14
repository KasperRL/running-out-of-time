using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public GameObject instructions = null;
    public TextMeshProUGUI instructionsText = null;

    public int sceneIndex;

    private bool triggerActive = false;

    private string sceneName;

    private void Awake()
    {
        // Unity GetSceneByBuildIndex() is bugged, so I had to use this workaround
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex); // Get the path of the scene
        sceneName = scenePath.Substring(0, scenePath.Length - 6).Substring(scenePath.LastIndexOf('/') + 1); // Get the name of the scene from the path
    }

    private void Update()
    {
        // Check for input when the trigger is active
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LoadSceneAsync(sceneIndex)); // Load the scene when E is pressed
            }
        }
    }

    private void OnTriggerStay()
    {
        instructionsText.text = "Press \"E\" to travel to \"" + sceneName + "\"";
        instructions.SetActive(true);
        triggerActive = true;
    }

    private void OnTriggerExit()
    {
        instructions.SetActive(false);
        triggerActive = false;
    }

    // Load scene asynchronously and display progress on loading screen
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadScene(int sceneIndex = 0)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
