using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cursors", menuName = "Scriptable Objects/Cursors")]
public class Cursors : ScriptableObject
{
    public enum CursorType
    {
        Default,
        InspectGhost,
        LookAt,
        Book,
        Map,

    }

    [System.Serializable]
    public struct CursorStruct
    {
        public CursorType type;
        public Texture2D texture;
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
