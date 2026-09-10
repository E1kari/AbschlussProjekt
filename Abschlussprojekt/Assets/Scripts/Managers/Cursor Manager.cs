using UnityEngine;
using static Cursors;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Cursors cursors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setCursor(CursorType.Default);
    }

    // Update is called once per frame
    void setCursor(CursorType type)
    {
        CursorStruct cursor = cursors.GetCursor(type);
        Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
    }
}
