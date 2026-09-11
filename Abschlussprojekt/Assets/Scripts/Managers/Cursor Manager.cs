using UnityEngine;
using UnityEngine.InputSystem;
using static S_Cursors;

public class CursorManager : MonoBehaviour
{
    [SerializeField] S_Cursors cursors;

    InputAction interactAction;
    CursorType currentCursor = CursorType.Default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setCursor(CursorType.Default);
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            setCursor(currentCursor, true);
        }
        else if (interactAction.WasReleasedThisFrame())
        {
            setCursor(currentCursor, false);
        }
    }

    // Update is called once per frame
    void setCursor(CursorType type, bool clicked = false)
    {
        CursorStruct cursor = cursors.GetCursor(type);
        if (clicked)
        {
            Cursor.SetCursor(cursor.clickedTexture, cursor.hotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
        }
        currentCursor = type;
    }
}
