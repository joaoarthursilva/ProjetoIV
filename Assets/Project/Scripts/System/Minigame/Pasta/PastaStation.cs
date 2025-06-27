using ProjetoIV.RatInput;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PastaStation : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private bool fadeBefore = true;
    public bool FadeBefore => fadeBefore;
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

    [SerializeField] private UIPastaCut m_uiPastaCut;
    [SerializeField] private UIPastaFold m_uiPastaFold;
    [SerializeField] private CutFoldObject[] cutFoldObjects;
    [System.Serializable]
    public struct CutFoldObject
    {
        public Ingredient ingredient;
        public GameObject cutPlane;
        public GameObject foldPlane;
    }

    public bool EmbraceMinigame(Minigame o_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
            if (m_minigames[i] == o_minigame) return true;

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
        SetCutPlane(m_currentMinigame.FinalIngredient(), false);
        m_uiPastaCut.ResetAllLines();
        m_onEndAction?.Invoke();
        MinigamesManager.SetCursorVisible(false);
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

    private Action m_onEndAction;
    Minigame m_currentMinigame;
    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        m_currentMinigame = p_minigame;
        m_foldCount = 0;
        SetCutPlane(p_minigame.FinalIngredient(), true);
        SetFoldPlane(p_minigame.FinalIngredient(), false);
        RatInput.Instance.ShowUIElement(InputID.NONE);
        m_onEndAction = p_actionOnEnd;
        MinigamesManager.SetCursorVisible(true);
        m_uiPastaCut.StartPastaCut(p_minigame.FinalIngredient());
        m_uiPastaCut.OnCallNextStep = EndCut;
        FadeController.Instance.CallFadeAnimation(false);
    }

    public void SetCutPlane(Ingredient p_ingredient, bool p_set)
    {
        for (int i = 0; i < cutFoldObjects.Length; i++)
        {
            if (cutFoldObjects[i].ingredient == p_ingredient) cutFoldObjects[i].cutPlane.SetActive(p_set);
        }
    }
    public void SetFoldPlane(Ingredient p_ingredient, bool p_set)
    {
        for (int i = 0; i < cutFoldObjects.Length; i++)
        {
            if (cutFoldObjects[i].ingredient == p_ingredient && cutFoldObjects[i].foldPlane != null)
                cutFoldObjects[i].foldPlane.SetActive(p_set);
        }
    }
    private void EndCut()
    {
        //chamar animação das massas sumindo

        StartCoroutine(CutToFoldTransition());
    }

    IEnumerator CutToFoldTransition()
    {
        yield return new WaitForSeconds(0.25f);

        m_uiPastaFold.OnFocusOnCamera += OnFocusCamera;
        m_uiPastaFold.OnCallNextStep += NextFold;
        m_uiPastaFold.StartPastaFold(m_currentMinigame.FinalIngredient());
    }

    int m_foldCount = 0;
    int m_maxFold
    {
        get
        {
#if UNITY_EDITOR
            return 2;
#endif
            return 3;
        }
    }
    void NextFold()
    {
        m_foldCount++;
        if (m_foldCount == m_maxFold)
        {
            m_uiPastaFold.OnFocusOnCamera?.Invoke(null, null);
            IOnEndInteraction();
            return;
        }

        m_uiPastaFold.StartPastaFold(m_currentMinigame.FinalIngredient(), m_foldCount + 1 == m_maxFold);
    }
}
