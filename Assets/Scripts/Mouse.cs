using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D cursorArrow;
    void Start()
    {

        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }


}
