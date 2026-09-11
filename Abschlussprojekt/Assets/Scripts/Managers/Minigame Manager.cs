using UnityEngine;
using static Minigame;
using static S_Minigames;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] S_Minigames minigames;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startMinigame(MinigameNames.Bar, DifficultyName.Easy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startMinigame(MinigameNames minigameName, DifficultyName difficultyName)
    {
        MinigameStruct minigame = minigames.GetMinigame(minigameName);
        GameObject instance = Instantiate(minigame.minigamePrefab);

        if (instance.TryGetComponent(out Minigame game))
            game.SetupGame(difficultyName);
    }
}
