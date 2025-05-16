using System;
using Unity.Cinemachine;
using UnityEngine;

public class UIPastaFold : MonoBehaviour, IFocusedMinigameStep
{
    [SerializeField] private InteractionPointClass[] foldObjects;
    [Space]
    [SerializeField] private UIQuickTimeGroup m_holdButtonGroup;

    public CinemachineCamera Camera { get { return m_currentCutClass.FocuseCamera; } }
    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusOnCamera
    {
        get { return m_onFocusCamera; }
        set { m_onFocusCamera = value; }
    }
    public Action OnCallNextStep;

    public void StartPastaFold(Ingredient p_recipe)
    {
        for (int i = 0; i < foldObjects.Length; i++)
        {
            if (foldObjects[i].Recipe == p_recipe)
            {
                SetCurrentFoldClass(foldObjects[i]);
                break;
            }
        }
    }

    [SerializeField, NaughtyAttributes.ReadOnly]
    private InteractionPointClass m_currentCutClass;
    private void SetCurrentFoldClass(InteractionPointClass p_cutClass)
    {
        m_currentCutClass = p_cutClass;

        OnFocusOnCamera?.Invoke(Camera, () =>
        {
            m_holdButtonGroup.StartGroup(m_currentCutClass.CutPoints);
            m_holdButtonGroup.OnEndSequence = CallNextStep;
        });
    }

    void CallNextStep()
    {
        OnCallNextStep?.Invoke();
    }
}
