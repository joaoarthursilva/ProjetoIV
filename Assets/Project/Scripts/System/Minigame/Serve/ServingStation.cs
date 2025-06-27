using ProjetoIV.RatInput;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ServingStation : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private bool fadeBefore = false;
    public bool FadeBefore { get; }

    [SerializeField] private Map m_inputMap;
    public Map Map => m_inputMap;
    [SerializeField] InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    [SerializeField] private List<RaycastableMinigame> m_raycastableMinigames;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastableMinigames;

    [SerializeField] private CinemachineCamera m_camera;
    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusCamera { get { return m_onFocusCamera; } set { m_onFocusCamera = value; } }
    public CinemachineCamera Camera => m_camera;
    [SerializeField] Minigame[] m_minigames;
    [SerializeField] SeasoningBehavior[] m_servingInteractions;
    [SerializeField] private Transform m_plateParent;
    ServingMinigame m_minigame;

    [Space] public float waitAfterDone;
    public bool EmbraceMinigame(Minigame p_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i] == p_minigame)
            {
                m_minigame = m_minigames[i] as ServingMinigame;
                return true;
            }
        }

        m_minigame = null;
        return false;
    }

    public void ICheckEndInteraction()
    {
        throw new NotImplementedException();
    }

    Action m_onEnd;
    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        m_onEnd = p_actionOnEnd;
        Instantiate(m_minigame.initialPlatePrefab, m_plateParent).transform.localPosition = Vector3.zero;

        StartCoroutine(Nextstep());
    }

    IEnumerator Nextstep()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < m_minigame.ingredientsToServe.Count; i++)
        {
            for (int j = 0; j < m_servingInteractions.Length; j++)
            {
                if (m_minigame.ingredientsToServe[i] == m_servingInteractions[j].ingredient)
                {
                    Debug.Log("play anim " + m_servingInteractions[j].gameObject.name);
                    yield return m_servingInteractions[j].PlayAnim();
                    m_servingInteractions[j].gameObject.SetActive(false);
                }
            }
        }

        yield return new WaitForSeconds(waitAfterDone);

        IOnEndInteraction();
    }


    public void IOnEndInteraction()
    {
        m_onEnd?.Invoke();
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
        throw new NotImplementedException();
    }
    #endregion
}
