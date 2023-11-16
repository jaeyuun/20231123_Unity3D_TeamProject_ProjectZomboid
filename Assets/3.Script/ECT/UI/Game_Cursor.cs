using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Cursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorHover;
    [SerializeField] Texture2D cursorNormal;
    void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void OnMouseOver()
    {
        Cursor.SetCursor(cursorHover, new Vector2(cursorHover.width / 4, 0), CursorMode.ForceSoftware); print("mouse over");
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.ForceSoftware);
    }
}
