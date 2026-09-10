using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigames", menuName = "Scriptable Objects/Minigames")]
public class Minigames : ScriptableObject
{
    public enum MinigameNames
    {
        Bar,

    }

    [Serializable]
    public struct MinigameStruct
    {
        public MinigameNames minigameName;
        public Minigame minigamePrefab;
    }

    public MinigameStruct[] minigames;

    public MinigameStruct GetMinigame(MinigameNames minigameName)
    {
        foreach (MinigameStruct minigame in minigames)
        {
            if (minigame.minigameName == minigameName)
            {
                return minigame;
            }
        }

        Debug.LogError("Minigame " + minigameName + " not found");
        return minigames[0];
    }
}
