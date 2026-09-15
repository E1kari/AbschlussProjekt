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

    public void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    protected void OnPause()
    {
        TogglePause(true);
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

    protected void PauseGame(bool loadPauseMenu = true)
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

    protected void ResumeGame()
    {
        Debug.Log("Resuming game");
        Time.timeScale = 1f; // Resume time
        isPaused = false;

        foreach (BarMinigame minigame in pausedMinigames_)
        {
            Debug.LogWarning(minigame.name + " is resumed");
            minigame.GameObject().SetActive(true);
        }

        pausedMinigames_ = null;

        GameObject.Destroy(pauseMenu_);
    }
}
