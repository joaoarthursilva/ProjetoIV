using UnityEngine;
using ProjetoIV.RatInput;
using System.Collections.Generic;
using Unity.Cinemachine;

public class MinigamesManager : MonoBehaviour, IMinigameInputs
{
    public static System.Action<CinemachineCamera> OnSetMinigamecamera;
    public ProjetoIV.RatInput.Input cut;

    public List<IMinigameInteraction> minigamesInteraction;
    private IMinigameInteraction m_currentMinigame;

    //public Dictionary<RaycastableObject, >

    private void Start()
    {
        cut = RatInput.Instance.GetInput(InputID.MINIGAME_CUT);
        cut.OnInputStarted += IOnCut;
        cut.OnInputCanceled += IOnEndedCut;

        var components = GetComponentsInChildren<IMinigameInteraction>();
        minigamesInteraction = new List<IMinigameInteraction>();

        for (int i = 0; i < components.Length; i++)
            if (components[i] is IMinigameInteraction minigame) minigamesInteraction.Add(minigame);
    }

    private void OnDestroy()
    {
        cut.OnInputStarted -= IOnCut;
        cut.OnInputCanceled -= IOnEndedCut;
    }

    public void OnInteractWithRaycastableObject(RaycastableMinigame p_object)
    {
        for (int i = 0; i < minigamesInteraction.Count; i++)
        {
            if (minigamesInteraction[i].RaycastableMinigame.Contains(p_object))
            {
                m_currentMinigame = minigamesInteraction[i];
                OnSetMinigamecamera?.Invoke(m_currentMinigame.MinigameCamera);
                break;
            }
        }
    }

    public void IOnLook(Vector2 p_vector)
    {
        if (m_currentMinigame == null) return;

    }

    public void IOnMouseClick()
    {
        if (m_currentMinigame == null) return;

    }

    public void IOnMouseDown()
    {
        if (m_currentMinigame == null) return;

    }

    public void IOnMouseUp()
    {
        if (m_currentMinigame == null) return;

    }

    public void IOnMove(Vector2 p_vector)
    {
        if (m_currentMinigame == null) return;

    }

    public void IOnCut()
    {
        if (m_currentMinigame == null) return;

        m_currentMinigame.IOnCut();
    }

    public void IOnEndedCut()
    {
        if (m_currentMinigame == null) return;
        
        m_currentMinigame.IOnEndedCut();
    }
}
