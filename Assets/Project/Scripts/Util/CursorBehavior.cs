using UnityEngine;

public class CursorBehavior
{
    public static void Set(bool p_visible, CursorLockMode p_mode)
    {
        Cursor.visible = p_visible;
        Cursor.lockState = p_mode;
    }
}