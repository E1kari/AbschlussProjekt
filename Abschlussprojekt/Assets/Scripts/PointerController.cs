using UnityEngine;
using UnityEngine.InputSystem;

public class PointerController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public RectTransform safeZone;
    public RectTransform perfectZone;

    public float moveSpeed = 1500f; // Speed of the pointer movement

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");


        pointerTransform = GetComponent<RectTransform>();
        targetPosition = endPoint.position;
        pointerTransform.position = startPoint.position;
    }

    void Update()
    {
        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Change direction if the pointer reaches one of the points
        if (Vector3.Distance(pointerTransform.position, startPoint.position) < 0.1f)
        {
            targetPosition = endPoint.position;
        }
        else if (Vector3.Distance(pointerTransform.position, endPoint.position) < 0.1f)
        {
            targetPosition = startPoint.position;
        }

        // Check for input
        if (interactAction.WasPressedThisFrame())
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(perfectZone, pointerTransform.position, null))
        {
            Debug.Log("Perfect!");
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }
}