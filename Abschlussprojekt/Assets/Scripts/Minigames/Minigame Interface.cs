using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public enum DifficultyName
    {
        Easy,
        Normal,
        Hard,
    };

    public abstract void SetupGame(DifficultyName difficulty);
    public abstract void endGame();
}
