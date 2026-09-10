using UnityEngine;
using static Minigame;
using static Minigames;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] Minigames minigames;

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
        Minigame instance = Instantiate(minigame.minigamePrefab);

        instance.SetupGame(difficultyName);
    }
}
