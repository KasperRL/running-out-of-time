using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
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
                StartCoroutine(LoadScene(sceneIndex)); // Load the scene when E is pressed
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
    IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); // Clamp01 ensures progress is between 0 and 1

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
