using UnityEngine;

public abstract class BarMinigame : Minigame
{

    public RectTransform safeZone;
    public RectTransform perfectZone;

    public RectTransform cursor;

    public Transform startPoint;
    public Transform endPoint;

    public Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();

        targetPosition = endPoint.position;
        cursor.position = startPoint.position;
    }
}
