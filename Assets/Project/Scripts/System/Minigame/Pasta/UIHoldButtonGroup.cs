using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHoldButtonGroup : MonoBehaviour
{
    [SerializeField] private UIHoldButton[] m_holdButtons;
    [SerializeField] private RectTransform m_canvasRect;
    public System.Action OnEndCutGroup;

    [Header("debug")] public Transform[] tempTrnaforms;
    [NaughtyAttributes.Button]
    public void DebugPositon()
    {
        StartGroup(tempTrnaforms);
    }


    List<bool> checks;
    public void StartGroup(Transform[] p_cutPoints)
    {
        if (checks == null) checks = new();
        else checks.Clear();

        for (int i = 0; i < p_cutPoints.Length; i++)
        {
            checks.Add(false);

            m_holdButtons[i].gameObject.SetActive(true);
            m_holdButtons[i].SetPosition(p_cutPoints[i], m_canvasRect);
        }

        int l_emptyIndex = checks.Count;
        while(l_emptyIndex < m_holdButtons.Length)
        {
            m_holdButtons[l_emptyIndex].gameObject.SetActive(false);
            l_emptyIndex++;
        }
    }

    public void OnComplete(UIHoldButton p_button)
    {
        for (int i = 0; i < m_holdButtons.Length; i++)
        {
            if (m_holdButtons[i] == p_button) checks[i] = true;
        }

        bool l_solved = true;
        for (int i = 0; i < checks.Count; i++)
        {
            if (!checks[i]) l_solved = false;
        }

        if (l_solved) OnCompleteGroup();
    }
    public Action CallNextStep;
    public void OnCompleteGroup()
    {
        TurnOffButtons();
        CallNextStep.Invoke();
    }

    void TurnOffButtons()
    {
        for (int i = 0; i < m_holdButtons.Length; i++)
        {
            m_holdButtons[i].gameObject.SetActive(false);
        }
    }
}
