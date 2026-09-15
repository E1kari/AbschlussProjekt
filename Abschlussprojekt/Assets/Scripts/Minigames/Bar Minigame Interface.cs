using UnityEngine;

public abstract class BarMinigame : Minigame
{

    [SerializeField] protected RectTransform safeZone;
    [SerializeField] protected RectTransform perfectZone;

    [SerializeField] protected RectTransform cursor;

    [SerializeField] protected Transform startPoint;
    [SerializeField] protected Transform endPoint;

    protected Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();

        targetPosition = endPoint.position;
        cursor.position = startPoint.position;
    }
}
