using ProjetoIV.RatInput;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class OvenStationSauce : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private Map m_map;
    public Map Map => m_map;
    [SerializeField] private InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    [SerializeField] private List<RaycastableMinigame> m_raycastableMinigame;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastableMinigame;

    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera Camera => m_camera;

    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusCamera { get { return m_onFocusCamera; } set { m_onFocusCamera = value; } }
    [SerializeField] Minigame[] m_minigames;
    private Minigame m_minigame;
    private SauceMinigame sauceMinigame;

    [SerializeField] MinigameStep[] m_minigameInteractions;
    private Action m_onEndAction;
    public bool EmbraceMinigame(Ingredient p_minigame, out Minigame o_minigame)
    {
        o_minigame = null;
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i].InitialIngredient() == p_minigame)
            {
                o_minigame = m_minigames[i];
                return true;
            }
        }

        return false;
    }

    int m_currentStep = 0;
    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        MinigamesManager.SetCursorVisible(true);
        m_minigame = p_minigame;

        if (m_minigame is not SauceMinigame) return;
        sauceMinigame = m_minigame as SauceMinigame;

        m_onEndAction = p_actionOnEnd;
        m_currentStep = 0;
        PlayNextInteraction();
    }

    void PlayNextInteraction()
    {
        if (m_currentStep >= sauceMinigame.Ingredients.Count)
        {
            IOnEndInteraction();
            return;
        }
        for (int i = 0; i < m_minigameInteractions.Length; i++)
        {
            if (m_minigameInteractions[i].IngredientsAccepted().Contains(sauceMinigame.Ingredients[m_currentStep]))
            {
                m_minigameInteractions[i].StartInteraction(sauceMinigame.Ingredients[m_currentStep], () =>
                {
                    m_currentStep++;

                    PlayNextInteraction();
                });
            }
        }
    }

    public void IOnEndInteraction()
    {
        MinigamesManager.SetCursorVisible(false);

        m_onEndAction?.Invoke();
    }

    public void ICheckEndInteraction()
    {
        throw new NotImplementedException();
    }

    #region inUtil
    public void IOnCut()
    {
        throw new NotImplementedException();
    }
    public void IOnEndedCut()
    {
        throw new NotImplementedException();
    }
    public void IOnLook(Vector2 p_vector)
    {
        throw new NotImplementedException();
    }
    public void IOnMove(Vector2 p_vector)
    {
        throw new NotImplementedException();
    }

    public void IOnPressExit()
    {
        throw new NotImplementedException();
    }
    #endregion

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



}
