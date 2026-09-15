using System;
using UnityEngine;
using static S_AudioData;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class HoldMinigame : BarMinigame
{
    [Serializable]
    public struct HoldDifficultyParams
    {
        public DifficultyName difficulty;

        [Space(10)]
        public float safeZoneWidth_min;
        public float safeZoneWidth_max;
        [Range(0f, 100f)] public float safeZone_percentagePerSec;

        [Space(10)]
        public float zoneMovementSpeed_min;
        public float zoneMovementSpeed_max;

        [Space(10)]
        public float perfectZoneWidth_min;
        public float perfectZoneWidth_max;
        [Range(0f, 100f)] public float perfectZone_percentagePerSec;
        [Range(0f, 1f)] public float perfectToMidRatio_max;

        [Space(10)]
        [Range(0f, 100f)] public float baseZone_falloffPercentagePerSec;

        [Space(10)]
        public float cursorMoveSpeed;
        public float cursorFalloffSpeed;

    }
    protected float holdPercentage;
    protected float currentZoneSpeed = 500f;

    public HoldDifficultyParams[] difficultyParams_;


    public HoldDifficultyParams GetDifficultyParams(DifficultyName difficultyName)
    {
        foreach (HoldDifficultyParams param in difficultyParams_)
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
        HoldDifficultyParams diffParams = GetDifficultyParams(difficultyName);


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

        GenerateTarget();
    }


    void CheckSuccess()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        HoldDifficultyParams diffParams = GetDifficultyParams(currentDifficulty);

        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(perfectZone, cursor.position, null))
        {
            holdPercentage += Time.deltaTime * diffParams.perfectZone_percentagePerSec;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, cursor.position, null))
        {
            holdPercentage += Time.deltaTime * diffParams.safeZone_percentagePerSec;
        }
        else
        {
            if (holdPercentage > 0f)
            {
                holdPercentage -= Time.deltaTime * diffParams.baseZone_falloffPercentagePerSec;
            }
        }

        if (holdPercentage >= 100f)
        {
            audioManager.PlayAudio(AudioIndex.MINIGAME_perfect);
            endGame();
        }

    }

    public override void endGame()
    {
        //SetupGame(currentDifficulty);
        Object.Destroy(this.gameObject);
    }

    public override void UpdateGame()
    {
        HoldDifficultyParams diffParams = GetDifficultyParams(currentDifficulty);

        if (interactAction.IsPressed())
        {
            cursor.position = Vector3.MoveTowards(cursor.position, endPoint.position, diffParams.cursorMoveSpeed * Time.deltaTime);
        }
        else
        {
            cursor.position = Vector3.MoveTowards(cursor.position, startPoint.position, diffParams.cursorFalloffSpeed * Time.deltaTime);
        }

        safeZone.anchoredPosition = Vector3.MoveTowards(safeZone.anchoredPosition, targetPosition, currentZoneSpeed * Time.deltaTime);

        if (Vector3.Distance(safeZone.anchoredPosition, targetPosition) < 0.1f)
        {
            GenerateTarget();
        }

        CheckSuccess();
    }

    public void GenerateTarget()
    {
        HoldDifficultyParams diffParams = GetDifficultyParams(currentDifficulty);

        float barWidth_base = ((RectTransform)safeZone.parent).rect.width;
        float maxOffset_safe = barWidth_base - safeZone.rect.width;
        targetPosition = new Vector2(Random.Range(0, maxOffset_safe), 0);

        currentZoneSpeed = Random.Range(diffParams.zoneMovementSpeed_min, diffParams.zoneMovementSpeed_max);
    }

}
