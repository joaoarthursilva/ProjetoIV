using ProjetoIV.RatInput;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PastaStation : MonoBehaviour, IMinigameInteraction
{
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

    [SerializeField] private GameObject cutPlane;
    [SerializeField] private GameObject foldPlane;

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

    public void IOnStartInteraction(Minigame p_minigame, Action p_actionOnEnd)
    {
        m_foldCount = 0;
        cutPlane.SetActive(true);
        foldPlane.SetActive(false);
        RatInput.Instance.ShowUIElement(InputID.NONE);
        m_onEndAction = p_actionOnEnd;
        MinigamesManager.SetCursorVisible(true);
        m_uiPastaCut.StartPastaCut(RecipeManager.Instance.currentRecipe);
        m_uiPastaCut.OnCallNextStep = EndCut;
    }
    private void EndCut()
    {
        //chamar animação das massas sumindo

        StartCoroutine(CutToFoldTransition());
    }

    IEnumerator CutToFoldTransition()
    {
        cutPlane.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        foldPlane.SetActive(true);

        m_uiPastaFold.OnFocusOnCamera += OnFocusCamera;
        m_uiPastaFold.OnCallNextStep += NextFold;
        m_uiPastaFold.StartPastaFold(RecipeManager.Instance.currentRecipe);
    }

    int m_foldCount = 0;
    int m_maxFold = 5;
    void NextFold()
    {
        m_foldCount++;
        if (m_foldCount == m_maxFold)
        {
            m_uiPastaFold.OnFocusOnCamera?.Invoke(null, null);
            IOnEndInteraction();
            return;
        }

        m_uiPastaFold.StartPastaFold(RecipeManager.Instance.currentRecipe);
    }
}
