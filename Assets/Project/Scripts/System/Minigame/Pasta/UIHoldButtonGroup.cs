using UnityEngine;

public class UIHoldButtonGroup : MonoBehaviour
{
    [SerializeField] private UIHoldButton[] m_holdButtons;
    [SerializeField] private RectTransform m_canvasRect;


    [Header("debug")] public Transform[] tempTrnaforms;
    [NaughtyAttributes.Button]
    public void DebugPositon()
    {
        StartGroup(tempTrnaforms);
    }

    public void StartGroup(Transform[] p_cutPoints)
    {
        for (int i = 0; i < p_cutPoints.Length; i++)
        {
            m_holdButtons[i].SetPosition(p_cutPoints[i], m_canvasRect);
        }
    }
    public void OnCompleteGroup()
    {

    }
}
