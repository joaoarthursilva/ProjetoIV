using System;
using UnityEngine;

public class UIPastaCut : MonoBehaviour
{
    [System.Serializable]
    private class CutClass
    {
        public Ingredient Recipe;
        public Transform[] CutPoints;
    }

    [SerializeField] private CutClass[] cutObjects;
    [Space]
    [SerializeField] private UIHoldButtonGroup m_holdButtonGroup;
    public void StartPastaCut(Ingredient p_recipe)
    {
        for (int i = 0; i < cutObjects.Length; i++)
        {
            if (cutObjects[i].Recipe == p_recipe)
            {
                SetCurrentCutClass(cutObjects[i]);
                break;
            }
        }
    }

    [SerializeField, NaughtyAttributes.ReadOnly]
    private CutClass m_currentCutClass;
    private void SetCurrentCutClass(CutClass p_cutClass)
    {
        m_currentCutClass = p_cutClass;

        m_holdButtonGroup.StartGroup(m_currentCutClass.CutPoints);
        m_holdButtonGroup.CallNextStep = CallNextStep;
    }

    public Action OnCallNextStep;
    void CallNextStep()
    {
        OnCallNextStep?.Invoke();
    }
}
