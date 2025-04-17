using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class BookStation : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private Minigame m_minigame;
    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera Camera => m_camera;

    public List<RaycastableMinigame> RaycastableMinigame => throw new NotImplementedException();

    System.Action m_onEndAction;
    public bool EmbraceMinigame(Ingredient p_ingredient, out Minigame o_minigame)
    {
        if (p_ingredient == null)
        {
            o_minigame = m_minigame;
            return true;
        }

        o_minigame = null;
        return false;
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
        m_onEndAction?.Invoke();
    }

    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        m_onEndAction = p_actionOnEnd;
    }
}
