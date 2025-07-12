using System;
using UnityEngine;

public class CursorBehavior
{
    public static Action<bool> OnSetCursorBehaviorVisible;
    public static void Set(bool p_visible, CursorLockMode p_mode)
    {
        Cursor.visible = p_visible;
        Cursor.lockState = p_mode;
        OnSetCursorBehaviorVisible?.Invoke(p_visible);
    }
}