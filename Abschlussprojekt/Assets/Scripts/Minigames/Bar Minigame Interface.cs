using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BarMinigame : MonoBehaviour
{

    public RectTransform safeZone;
    public RectTransform perfectZone;

    public RectTransform cursor;

    public Transform startPoint;
    public Transform endPoint;

    public InputAction interactAction;
    public Vector3 targetPosition;
    public DifficultyName currentDifficulty;

    public enum DifficultyName
    {
        Easy,
        Normal,
        Hard,
    };

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        targetPosition = endPoint.position;
        cursor.position = startPoint.position;
    }

    void Update()
    {
        UpdateGame();
    }

    public abstract void SetupGame(DifficultyName difficulty);
    public abstract void UpdateGame();
    public abstract void endGame();
}
