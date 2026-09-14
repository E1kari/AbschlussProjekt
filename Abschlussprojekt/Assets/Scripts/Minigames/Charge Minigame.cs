using System;
using UnityEngine;
using static S_AudioData;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ChargeMinigame : BarMinigame
{
    [Serializable]
    public struct ChargeDifficultyParams
    {
        public DifficultyName difficulty;

        public float safeZoneWidth_min;
        public float safeZoneWidth_max;

        public float perfectZoneWidth_min;
        public float perfectZoneWidth_max;
        [Range(0f, 1f)] public float perfectToMidRatio_max;

        public float cursorMoveSpeed;
    }

    public ChargeDifficultyParams[] difficultyParams_;


    public ChargeDifficultyParams GetDifficultyParams(DifficultyName difficultyName)
    {
        foreach (ChargeDifficultyParams param in difficultyParams_)
        {
            if (param.difficulty == difficultyName)
            {
                return param;
            }
        }

        Debug.LogError("Difficulty " + difficultyName + " not found");
        return difficultyParams_[0];
    }

    public override void SetupGame(DifficultyName difficultyName)
    {
        currentDifficulty = difficultyName;
        ChargeDifficultyParams diffParams = GetDifficultyParams(difficultyName);


        //-----------------safe zone-----------------\\
        float safeZone_Width = Random.Range(diffParams.safeZoneWidth_min, diffParams.safeZoneWidth_max); //ToDo: auf base width beschränken
        safeZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, safeZone_Width);

        float barWidth_base = ((RectTransform)safeZone.parent).rect.width;
        float maxOffset_safe = barWidth_base - safeZone_Width;
        safeZone.anchoredPosition = new Vector2(Random.Range(0, maxOffset_safe), safeZone.anchoredPosition.y);


        //-----------------perfect zone-----------------\\

        float actualWidthMax = Mathf.Min(safeZone_Width * diffParams.perfectToMidRatio_max, diffParams.perfectZoneWidth_max);
        float perfectZone_Width = Random.Range(diffParams.perfectZoneWidth_min, actualWidthMax);
        perfectZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, perfectZone_Width);

        float barWidth_safe = ((RectTransform)perfectZone.parent).rect.width;
        float maxOffset_perfect = barWidth_safe - perfectZone_Width;
        perfectZone.anchoredPosition = new Vector2(Random.Range(0, maxOffset_perfect), perfectZone.anchoredPosition.y);


        //-----------------cursor-----------------\\
        cursor.position = startPoint.position;
    }


    void CheckSuccess()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();


        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(perfectZone, cursor.position, null))
        {
            audioManager.PlayAudio(AudioIndex.MINIGAME_perfect);
            Debug.Log("Perfect!");
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, cursor.position, null))
        {
            audioManager.PlayAudio(AudioIndex.MINIGAME_success);
            Debug.Log("Success!");
        }
        else
        {
            audioManager.PlayAudio(AudioIndex.MINIGAME_fail);
            Debug.Log("Fail!");
        }

        endGame();
    }

    public override void endGame()
    {
        //SetupGame(currentDifficulty);
        Object.Destroy(this.gameObject);
    }

    public override void UpdateGame()
    {
        ChargeDifficultyParams diffParams = GetDifficultyParams(currentDifficulty);

        if (interactAction.IsPressed() && !(Vector3.Distance(cursor.position, endPoint.position) < 0.1f))
        {
            // Move the pointer towards the target position
            cursor.position = Vector3.MoveTowards(cursor.position, targetPosition, diffParams.cursorMoveSpeed * Time.deltaTime);
        }

        // Check for input
        if (interactAction.WasReleasedThisFrame())
        {
            CheckSuccess();
        }
    }
}
