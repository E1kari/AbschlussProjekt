using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    //private S_SceneManager sceneManager_;

    [SerializeField]
    private GameObject pauseMenu_Prefab;

    private GameObject pauseMenu_;
    private bool isPaused = false;
    GameObject player;

    InputAction pauseAction = InputSystem.actions.FindAction("Pause");

    public void Start()
    {
        StartCoroutine(OnSceneLoaded());
    }

    private IEnumerator OnSceneLoaded()
    {
        yield return null;
        //sceneManager_ = Resources.Load<S_SceneManager>("Scriptable Objects/S_SceneSaver");
    }

    public void Update()
    {
        if (pauseAction.IsPressed())
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

    private void PauseGame(bool loadPauseMenu)
    {
        Time.timeScale = 0f; // Freeze time
        player = GameObject.FindWithTag("Player");
        player?.SetActive(false);
        if (loadPauseMenu)
        {
            if (pauseMenu_ == null)
            {
                pauseMenu_ = Instantiate(pauseMenu_Prefab);
            }
            pauseMenu_.SetActive(true);

            //SceneManager.LoadScene("menu_pause", LoadSceneMode.Additive); // Load menu without unloading game
        }
        isPaused = true;
    }

    private void ResumeGame()
    {
        // if (SceneManager.GetSceneByName("menu_pause").isLoaded)
        // {
        //     SceneManager.UnloadSceneAsync("menu_pause"); // Unload menu
        // }
        isPaused = false;
        player.SetActive(true);
        Time.timeScale = 1f; // Resume time

        pauseMenu_.SetActive(false);
    }
}
