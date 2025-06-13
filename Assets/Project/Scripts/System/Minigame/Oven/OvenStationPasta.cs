using UnityEngine;
using ProjetoIV.RatInput;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using ProjetoIV.Util;

public class OvenStationPasta : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private Map m_map;
    public Map Map => m_map;
    [SerializeField] private InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    [SerializeField] private List<RaycastableMinigame> m_raycastableMinigame;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastableMinigame;
    public CinemachineCamera Camera => m_currentPasta.camera;

    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusCamera { get { return m_onFocusCamera; } set { m_onFocusCamera = value; } }
    [SerializeField] Minigame[] m_minigames;
    private Minigame m_minigame;
    private Action m_onEndAction;

    [Serializable]
    private class PastaServingClass
    {
        public Ingredient pasta;
        //public ObjectAnimationBehaviour animBehavior;
        public BoilBehavior prefab;
        public CinemachineCamera camera;
        public Transform instantiateParent;
    }

    [SerializeField] private PastaServingClass[] pastaPrefabs;
    [SerializeField] private Vector3 instantiatePos;
    public bool EmbraceMinigame(Ingredient p_minigame, out Minigame o_minigame)
    {
        o_minigame = null;
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i].InitialIngredient() == p_minigame)
            {
                for (int j = 0; j < pastaPrefabs.Length; j++)
                {
                    if (pastaPrefabs[j].pasta == m_minigames[i].InitialIngredient())
                    {
                        m_currentPasta = pastaPrefabs[j];
                        break;
                    }
                }
                o_minigame = m_minigames[i];
                return true;
            }
        }

        return false;
    }

    private BoilBehavior l_prefabAnimBehavior;
    private PastaServingClass m_currentPasta;
    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        m_minigame = p_minigame;
        l_prefabAnimBehavior = Instantiate(m_currentPasta.prefab, m_currentPasta.instantiateParent);
        l_prefabAnimBehavior.SetFutureParent(m_currentPasta.instantiateParent);
        m_onEndAction = p_actionOnEnd;
        Invoke(nameof(IOnEndInteraction), .8f);
    }


    public void IOnEndInteraction()
    {
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
