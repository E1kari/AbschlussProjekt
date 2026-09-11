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

    public void Start()
    {

    }

    private void LoadSceneByName(string sceneNameToLoad)
    {
        if (string.IsNullOrEmpty(sceneNameToLoad))
        {
            Debug.LogError("Scene name is empty");
            return;
        }

        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
    }


    // Wrapper method to be called by the button in the Unity Inspector
    public void LoadSceneWrapper(SceneRef sceneRef)
    {
        playButtonClick();

        string sceneName = sceneRef.sceneName;

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene ref is empty");
            return;
        }

        Debug.Log("Loading scene: " + sceneName);
        LoadSceneByName(sceneName);

    }

    public void resumeGame()
    {
        playButtonClick();

        GameObject.FindAnyObjectByType<PauseManager>()?.TogglePause();
    }

    // public void back()
    // {
    //    playButtonClick();

    //     SceneManager.UnloadSceneAsync("menu_options");

    //     if (SceneManager.GetSceneByName("menu_pause").isLoaded)
    //     {
    //         S_SceneManager.determineSelectedButton(SceneManager.GetSceneByName("menu_pause"));
    //     }
    //     else if (SceneManager.GetSceneByName("menu_main").isLoaded)
    //     {
    //         S_SceneManager.determineSelectedButton(SceneManager.GetSceneByName("menu_main"));
    //     }
    // }

    public void exitGame()
    {
        playButtonClick();

        Debug.Log("closing game...");
        Application.Quit();
    }

    private void playButtonClick()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.PlayAudio(AudioIndex.UI_buttonClick);
    }
}