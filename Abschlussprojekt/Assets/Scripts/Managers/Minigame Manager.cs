using UnityEngine;
using UnityEngine.InputSystem;
using static Minigame;
using static S_Minigames;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] S_Minigames minigames;

    public InputAction startReactionGame;
    public InputAction startChargeGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startReactionGame = InputSystem.actions.FindAction("DEBUG_StartReactionGame");
        startChargeGame = InputSystem.actions.FindAction("DEBUG_StartChargeGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (startReactionGame.WasPressedThisFrame())
        {
            Debug.Log("Starting reaction minigame");
            startMinigame(MinigameNames.Reaction, DifficultyName.Easy);
        }
        if (startChargeGame.WasPressedThisFrame())
        {
            Debug.Log("Starting charge minigame");
            startMinigame(MinigameNames.Charge, DifficultyName.Easy);
        }
    }

    public void startMinigame(MinigameNames minigameName, DifficultyName difficultyName)
    {
        MinigameStruct minigame = minigames.GetMinigame(minigameName);
        GameObject instance = Instantiate(minigame.minigamePrefab);

        if (instance.TryGetComponent(out Minigame game))
        {
            Debug.Log("setting up " + minigameName);
            game.SetupGame(difficultyName);
        }
    }
}
