using UnityEngine;

public class PlayerAimBehavior : MonoBehaviour
{
    [SerializeField] private GameObject m_knobGO;

    private void Start()
    {
        CursorBehavior.OnSetCursorBehaviorVisible += SetKnobGO;
    }
    void SetKnobGO(bool p_cursorVisible) => m_knobGO.SetActive(!p_cursorVisible);
}
