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

    private bool inRange = false;

    private string sceneName;

    private void Awake()
    {
        // Unity GetSceneByBuildIndex() is bugged, so I had to use this workaround
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex); // Get the path of the scene
        sceneName = scenePath.Substring(0, scenePath.Length - 6).Substring(scenePath.LastIndexOf('/') + 1); // Get the name of the scene from the path
    }

    private void Update()
    {
        // Check for input when in range
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LoadSceneAsync(sceneIndex)); // Load the scene when E is pressed
            }
        }
    }

    private void OnTriggerEnter()
    {
        inRange = true;
        
        instructionsText.text = "Press \"E\" to travel to \"" + sceneName + "\"";
        instructions.SetActive(true);
    }

    private void OnTriggerExit()
    {
        inRange = false;
        
        instructions.SetActive(false);
    }

    // Load scene asynchronously
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
