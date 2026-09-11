using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cursors", menuName = "Scriptable Objects/Cursors")]
public class S_Cursors : ScriptableObject
{
    public enum CursorType
    {
        Default,
        InspectGhost,
        LookAt,
        Book,
        Map,

    }

    [Serializable]
    public struct CursorStruct
    {
        public CursorType type;
        public Texture2D texture;
        public Texture2D clickedTexture;
        public Vector2 hotspot;
    }

    [SerializeField] CursorStruct[] cursors;

    public CursorStruct GetCursor(CursorType type)
    {
        foreach (CursorStruct cursor in cursors)
        {
            if (cursor.type == type)
            {
                return cursor;
            }
        }

        return cursors[0];
    }
}
