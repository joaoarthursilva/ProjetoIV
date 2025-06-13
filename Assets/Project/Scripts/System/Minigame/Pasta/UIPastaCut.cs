using ProjetoIV.Util;
using System;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public struct InteractionPointClass
{
    public Ingredient Recipe;
    public GameObject pastaPrefab;
    public Transform parent;
    public Transform[] CutPoints;
    public Transform[] CutPoints2;
    public CinemachineCamera FocuseCamera;

    public InteractionPointClass(InteractionPointClass p_base)
    {
        Recipe = p_base.Recipe;
        pastaPrefab = p_base.pastaPrefab;
        parent = p_base.parent;
        CutPoints = p_base.CutPoints;
        CutPoints2 = p_base.CutPoints2;
        FocuseCamera = p_base.FocuseCamera;
    }
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
