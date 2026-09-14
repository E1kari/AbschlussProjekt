using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Minigame : MonoBehaviour
{
    public enum DifficultyName
    {
        Easy,
        Normal,
        Hard,
    };

    public InputAction interactAction;
    public DifficultyName currentDifficulty;

    protected virtual void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    protected virtual void Update()
    {
        UpdateGame();
    }

    public abstract void SetupGame(DifficultyName difficulty);
    public abstract void UpdateGame();
    public abstract void endGame();
}
