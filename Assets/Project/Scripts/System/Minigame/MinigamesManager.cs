using UnityEngine;
using ProjetoIV.RatInput;
using System.Collections.Generic;
using Unity.Cinemachine;

public class MinigamesManager : MonoBehaviour, IMinigameInputs
{
    public static System.Action<CinemachineCamera> OnSetMinigamecamera;
    public ProjetoIV.RatInput.Input cut;
    public ProjetoIV.RatInput.Input exitInteraction;

    public List<IMinigameInteraction> minigamesInteraction;
    private IMinigameInteraction m_currentMinigame;

    //public Dictionary<RaycastableObject, >
    [SerializeField] private PlayerInventory m_playerInventory;

    private void Start()
    {
        SubscribeToInput();

        var components = GetComponentsInChildren<IMinigameInteraction>();
        minigamesInteraction = new List<IMinigameInteraction>();

        for (int i = 0; i < components.Length; i++)
            if (components[i] is IMinigameInteraction minigame) minigamesInteraction.Add(minigame);
    }

    void SubscribeToInput()
    {
        cut = RatInput.Instance.GetInput(InputID.MINIGAME_CUT);
        cut.OnInputStarted += IOnCut;
        cut.OnInputCanceled += IOnEndedCut;

        exitInteraction = RatInput.Instance.GetInput(InputID.MINIGAME_ENDINTERACTION);
        exitInteraction.OnInputCanceled += IOnPressExit;
    }

    void UnsubscribeToInput()
    {
        if (cut != null)
        {
            cut.OnInputStarted -= IOnCut;
            cut.OnInputCanceled -= IOnEndedCut;
        }

        if (exitInteraction != null) exitInteraction.OnInputCanceled -= IOnPressExit;
        
    }

    private void OnDestroy()
    {
        UnsubscribeToInput();
    }

    public void OnInteractWithRaycastableObject(RaycastableMinigame p_object)
    {
        for (int i = 0; i < minigamesInteraction.Count; i++)
        {
            if (minigamesInteraction[i].RaycastableMinigame.Contains(p_object)
                && minigamesInteraction[i].EmbraceMinigame(m_playerInventory.currentIngredient, out Minigame l_minigame))
            {
                m_currentMinigame = minigamesInteraction[i];

                m_currentMinigame.IOnStartInteraction(l_minigame, () => OnEndMinigame(l_minigame.FinalIngredient()));

                OnSetMinigamecamera?.Invoke(m_currentMinigame.Camera);
                break;
            }
        }
    }

    private void OnEndMinigame(Ingredient p_finalIngredient)
    {
        m_playerInventory.currentIngredient = p_finalIngredient;
        OnSetMinigamecamera?.Invoke(null);
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

    public void IOnPressExit()
    {
        throw new System.NotImplementedException();
    }
}
