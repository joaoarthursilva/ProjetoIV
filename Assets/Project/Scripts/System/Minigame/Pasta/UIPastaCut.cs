using System;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public class InteractionPointClass
{
    public Ingredient Recipe;
    public Transform[] CutPoints;
    public CinemachineCamera FocuseCamera;
}

public class UIPastaCut : MonoBehaviour
{
    [SerializeField] private InteractionPointClass[] cutObjects;
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
    private InteractionPointClass m_currentCutClass;
    private void SetCurrentCutClass(InteractionPointClass p_cutClass)
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
