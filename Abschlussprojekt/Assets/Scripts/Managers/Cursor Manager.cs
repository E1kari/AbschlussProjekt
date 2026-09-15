using UnityEngine;
using UnityEngine.InputSystem;
using static S_Cursors;

public class CursorManager : MonoBehaviour
{
    [SerializeField] S_Cursors cursors;

    CursorType currentCursor = CursorType.Default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCursor(CursorType.Default);
    }

    protected void OnInteract(InputValue value)
    {
        SetCursor(currentCursor, value.isPressed);
    }

    public void SetCursor(CursorType type, bool clicked = false)
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
