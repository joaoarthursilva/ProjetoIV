using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuickTimeGroup : MonoBehaviour
{
    [SerializeField] private UIQuickTimeButton[] m_buttons;
    [SerializeField] private RectTransform m_canvasRect;
    public System.Action OnEndSequence;

    private int m_numerOfQuickTimeEvents;
    private int m_currentQuickTimeEvent;
    public void StartGroup(Transform[] p_foldPoints)
    {
        for (int i = 0; i < p_foldPoints.Length; i++)
        {
            m_buttons[i].ResetClick();
            m_buttons[i].SetPosition(p_foldPoints[i], m_canvasRect);
        }

        m_currentQuickTimeEvent = 0;
        m_numerOfQuickTimeEvents = p_foldPoints.Length;

        StartCoroutine(PlayEvents());
    }

    IEnumerator PlayEvents()
    {
        Debug.Log("a");
        while (m_currentQuickTimeEvent < m_numerOfQuickTimeEvents)
        {
            Debug.Log("b");
            m_buttons[m_currentQuickTimeEvent].StartClick();

            while (!m_buttons[m_currentQuickTimeEvent].Done) yield return null;

            Debug.Log("c");
            m_currentQuickTimeEvent++;
        }

        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i].gameObject.SetActive(false);
        }
        OnEndSequence?.Invoke();
    }
}
