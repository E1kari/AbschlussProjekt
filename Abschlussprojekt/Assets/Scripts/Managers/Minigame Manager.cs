using UnityEngine;
using UnityEngine.InputSystem;
using static Minigame;
using static S_Minigames;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] S_Minigames minigames;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    protected void OnDEBUG_StartReactionGame()
    {
        Debug.Log("Starting reaction minigame");
        StartMinigame(MinigameNames.Reaction, DifficultyName.Easy);
    }

    protected void OnDEBUG_StartChargeGame()
    {
        Debug.Log("Starting charge minigame");
        StartMinigame(MinigameNames.Charge, DifficultyName.Easy);
    }

    protected void OnDEBUG_StartHoldGame()
    {
        Debug.Log("Starting hold minigame");
        StartMinigame(MinigameNames.Hold, DifficultyName.Easy);
    }


    public void StartMinigame(MinigameNames minigameName, DifficultyName difficultyName)
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
