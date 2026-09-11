using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    //private S_SceneManager sceneManager_;

    [SerializeField]
    private GameObject pauseMenu_Prefab;

    private GameObject pauseMenu_;
    private bool isPaused = false;
    Minigame[] pausedMinigames_;

    InputAction pauseAction;

    public void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        StartCoroutine(OnSceneLoaded());
    }

    private IEnumerator OnSceneLoaded()
    {
        yield return null;
        //sceneManager_ = Resources.Load<S_SceneManager>("Scriptable Objects/S_SceneSaver");
    }

    public void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            TogglePause(true);
        }
    }

    public void TogglePause(bool loadPauseMenu = false)
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame(loadPauseMenu);
        }
    }

    private void PauseGame(bool loadPauseMenu = true)
    {
        Time.timeScale = 0f; // Freeze time
        isPaused = true;

        pausedMinigames_ = FindObjectsByType<Minigame>();
        foreach (Minigame minigame in pausedMinigames_)
        {
            Debug.LogWarning(minigame.name + " is paused");
            minigame.GameObject().SetActive(false);
        }

        if (loadPauseMenu)
        {
            if (pauseMenu_ == null)
            {
                pauseMenu_ = Instantiate(pauseMenu_Prefab);
            }
        }
    }

    private void ResumeGame()
    {
        Debug.Log("Resuming game");
        Time.timeScale = 1f; // Resume time
        isPaused = false;

        foreach (Minigame minigame in pausedMinigames_)
        {
            Debug.LogWarning(minigame.name + " is resumed");
            minigame.GameObject().SetActive(true);
        }

        pausedMinigames_ = null;

        GameObject.Destroy(pauseMenu_);
    }
}
