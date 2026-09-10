using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static S_AudioData;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonBehavior : MonoBehaviour
{

    private GameObject[] panels_;
    private S_SceneManager sceneManager_;

    public void Start()
    {
        sceneManager_ = Resources.Load<S_SceneManager>("Scriptable Objects/S_SceneManager");

        //panels_ = GameObject.FindGameObjectsWithTag("Panel");
        //foreach (GameObject panel in panels_)
        //{
        //    if (panel.name != "Sound Panel")
        //    {
        //        panel.SetActive(false);
        //    }
        //}
    }

    public void LoadSceneByName(string sceneNameToLoad)
    {
        if (string.IsNullOrEmpty(sceneNameToLoad))
        {
            Debug.LogError("Scene name is empty");
            return;
        }

        if (sceneNameToLoad.ToLower().Contains("menu") && !sceneNameToLoad.ToLower().Contains("main"))
        {
            SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);
        }
        else if (sceneNameToLoad.ToLower().Contains("level") || sceneNameToLoad.ToLower().Contains("room") || sceneNameToLoad.ToLower().Contains("main"))
        {
            SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Invalid scene name: " + sceneNameToLoad);
        }

    }


    // Wrapper method to be called by the button in the Unity Inspector
    public void LoadSceneWrapper(SceneRef sceneRef)
    {
        // AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        // audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        string sceneName = sceneRef.sceneName;

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty");
            return;
        }

        Debug.Log("Loading scene: " + sceneName);
        LoadSceneByName(sceneName);

    }

    public void activatePanel(GameObject pa_panel)
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        if (pa_panel == null)
        {
            Debug.LogError("Panel is not set");
            return;
        }

        foreach (GameObject panel in panels_)
        {
            panel.SetActive(false);
        }
        pa_panel.gameObject.SetActive(true);
    }

    public void startLevel()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        //GameObject.Find("Preview Manager").GetComponent<PreviewManager>().reactivatePauseManager();
        SceneManager.UnloadSceneAsync("menu_preview");
        resumeLevel();
    }

    public void nextLevel()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string levelName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(nextLevelIndex));
        SceneManager.LoadScene(levelName);
    }

    public void resetLevel()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        resumeLevel();
        SceneManager.LoadScene(sceneManager_.GetCurrentLevelSceneName());
        //S_Timer timer = Resources.Load<S_Timer>("Scriptable Objects/Timer");
        //timer.ResetTimer();

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void resumeLevel()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        GameObject.Find("Pause Manager").GetComponent<PauseManager>().TogglePause();
    }

    public void back()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        SceneManager.UnloadSceneAsync("menu_options");

        if (SceneManager.GetSceneByName("menu_pause").isLoaded)
        {
            S_SceneManager.determineSelectedButton(SceneManager.GetSceneByName("menu_pause"));
        }
        else if (SceneManager.GetSceneByName("menu_main").isLoaded)
        {
            S_SceneManager.determineSelectedButton(SceneManager.GetSceneByName("menu_main"));
        }
    }

    public void exitGame()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);

        Debug.Log("closing game...");
        Application.Quit();
    }
}