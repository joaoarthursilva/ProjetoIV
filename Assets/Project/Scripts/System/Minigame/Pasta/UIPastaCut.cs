using ProjetoIV.Util;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public struct LineRendererGroup
{
    public LineRenderer[] lines;
    public Transform[] endPoint;
    public Material material;

    public void SetMaterial()
    {
        for (int i = 0; i < lines.Length; i++)
            if (material != null) lines[i].material = material;
    }

    public void SetLine(float p_lerp)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].positionCount = 2;
            lines[i].SetPosition(0, lines[i].transform.position);
            lines[i].SetPosition(1, Vector3.Lerp(lines[i].GetPosition(0), endPoint[i].position, p_lerp));
        }
    }
}

[Serializable]
public struct InteractionPointClass
{
    public Ingredient Recipe;
    public GameObject pastaPrefab;
    public Transform parent;
    public Transform[] CutPoints;
    public LineRendererGroup[] lines;
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
        lines = p_base.lines;
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

        for (int i = 0; i < m_currentCutClass.lines.Length; i++)
            m_currentCutClass.lines[i].SetMaterial();

        m_holdButtonGroup.StartGroup(m_currentCutClass.CutPoints, (id, lerp) => UpdateLine(id, lerp));
        m_holdButtonGroup.CallNextStep = CallNextStep;
    }

    void UpdateLine(int p_lineID, float p_lerp)
    {
        m_currentCutClass.lines[p_lineID].SetLine(p_lerp);
    }

    public Action OnCallNextStep;
    void CallNextStep()
    {
        OnCallNextStep?.Invoke();
    }

    public void ResetAllLines()
    {
        for (int i = 0; i < m_currentCutClass.lines.Length; i++)
        {
            m_currentCutClass.lines[i].SetLine(0);
        }

    }
}
