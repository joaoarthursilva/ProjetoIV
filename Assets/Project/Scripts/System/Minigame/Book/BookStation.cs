using System;
using System.Collections.Generic;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using ProjetoIV.RatInput;

public class BookStation : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private bool fadeBefore = false;
    public bool FadeBefore { get; }

    [SerializeField] private Map m_inputMap;
    public Map Map => m_inputMap;

    [SerializeField] private InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    public Minigame m_minigame;
    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera Camera => m_camera;

    [SerializeField] private List<RaycastableMinigame> m_raycastable;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastable;

    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusCamera { get => m_onFocusCamera; set => m_onFocusCamera = value; }

    public BookBehavior bookBehavior;

    System.Action m_onEndAction;
    public bool EmbraceMinigame(Minigame p_minigame)
    {
        return true;
    }

    public void ICheckEndInteraction()
    {
       
    }

    public void IOnCut()
    {
        throw new NotImplementedException();
    }

    public void IOnEndedCut()
    {
        throw new NotImplementedException();
    }

    public void IOnEndInteraction()
    {
        throw new NotImplementedException();
    }

    public void IOnLook(Vector2 p_vector)
    {
        throw new NotImplementedException();
    }

    public void IOnMouseClick()
    {
        throw new NotImplementedException();
    }

    public void IOnMouseDown()
    {
        throw new NotImplementedException();
    }

    public void IOnMouseUp()
    {
        throw new NotImplementedException();
    }

    public void IOnMove(Vector2 p_vector)
    {
        throw new NotImplementedException();
    }

    public void IOnPressExit()
    {
        RatInput.ShowInputUIElement(InputID.MINIGAME_ENDINTERACTION);
        m_onEndAction?.Invoke();
    }

    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        MinigamesManager.SetCursorVisible(true);
        RatInput.ShowInputUIElement(InputID.NONE);

        m_onEndAction = p_actionOnEnd;

        bookBehavior.OpenBook();
        bookBehavior.OnSelectRecipe = SelectRecipe;
    }

    public void SelectRecipe(Ingredient p_ingredient)
    {
        MinigamesManager.SetCursorVisible(false);
        RecipeManager.Instance.SetRecipe(p_ingredient);
        m_onEndAction?.Invoke();
    }
}
