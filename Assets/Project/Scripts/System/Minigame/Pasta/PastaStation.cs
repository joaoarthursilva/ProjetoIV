using ProjetoIV.RatInput;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PastaStation : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    [SerializeField] private List<RaycastableMinigame> m_raycastableMinigames;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastableMinigames;

    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera Camera => m_camera;
    [SerializeField] Minigame[] m_minigames;
    public bool EmbraceMinigame(Ingredient p_ingredient, out Minigame o_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i].IsPartOfTheRecipe(RecipeManager.Instance.currentRecipe))
            {
                o_minigame = m_minigames[i];
                return true;
            }
        }

        o_minigame = null;
        return false;
    }

    public void ICheckEndInteraction()
    {
        
    }

    public void IOnCut()
    {
        
    }

    public void IOnEndedCut()
    {
        
    }

    public void IOnEndInteraction()
    {
        
    }

    public void IOnLook(Vector2 p_vector)
    {
        
    }

    public void IOnMouseClick()
    {
        
    }

    public void IOnMouseDown()
    {
        
    }

    public void IOnMouseUp()
    {
      
    }

    public void IOnMove(Vector2 p_vector)
    {
        
    }

    public void IOnPressExit()
    {
        
    }

    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        
    }

  
}
