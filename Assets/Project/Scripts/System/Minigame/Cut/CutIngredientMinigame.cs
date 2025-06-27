using ProjetoIV.RatInput;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutIngredientMinigame : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private Map m_inputMap;
    public Map Map => m_inputMap;

    [SerializeField] private InputID[] m_inputsToShow;
    public InputID[] InputsToShow => m_inputsToShow;

    [SerializeField] private Minigame[] m_minigames;
    public Minigame CurrentMinigame;

    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera Camera => m_camera;

    [SerializeField] private List<RaycastableMinigame> m_raycastable;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastable;

    private Action<CinemachineCamera, Action> m_onFocusCamera;
    public Action<CinemachineCamera, Action> OnFocusCamera { get { return m_onFocusCamera; } set { m_onFocusCamera = value; } }

    [Space]
    private Ingredient m_initialIngredient;
    [SerializeField] private Transform m_ingredientParent;
    [SerializeField] private IngredientBehavior m_currentIngredient;
    [SerializeField] private KnifeMinigameBehavior m_currentKnife;
    [SerializeField] private ProccessMinigame m_currentMinigame;
    [SerializeField] private int m_interactionsCounter;
    private System.Action m_onEndAction;
    private KnifeOnSceneTag m_onSceneKnife;
    [SerializeField] private KnifeMinigameBehavior knifeprefab;
    private void Start()
    {
        m_onSceneKnife = FindFirstObjectByType<KnifeOnSceneTag>();
    }

    public bool EmbraceMinigame(Minigame p_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
            if (m_minigames[i] == p_minigame) return true;

        return false;
    }

    public void IOnStartInteraction(Minigame p_minigame, System.Action p_actionOnEnd)
    {
        m_currentMinigame = p_minigame as ProccessMinigame;
        m_initialIngredient = p_minigame.InitialIngredient();
        m_onEndAction = p_actionOnEnd;
        RatInput.Instance.ShowUIElement(InputID.MINIGAME_CUT);

        SpawnIngredient();
    }

    void SpawnIngredient()
    {
        if (Instantiate(m_initialIngredient.prefab, m_ingredientParent).TryGetComponent(out m_currentIngredient))
        {
            m_currentIngredient.SetProcessed(false);
            if (Instantiate(knifeprefab, m_ingredientParent).TryGetComponent(out m_currentKnife))
            {
                m_canInteract = true;
                m_onSceneKnife.gameObject.SetActive(false);
            }
        }


    }

    public void IOnEndInteraction()
    {
        FadeController.Instance.CallFadeAnimation(false);
        RatInput.Instance.ShowUIElement(InputID.NONE);

        Destroy(m_currentKnife.gameObject);
        DestroyIngredient();
        m_onEndAction?.Invoke();
        m_onSceneKnife.gameObject.SetActive(true);

    }
    void DestroyIngredient()
    {
        if (m_currentIngredient == null) return;

        Destroy(m_currentIngredient.gameObject);
    }

    public void ICheckEndInteraction()
    {
        if (m_currentMinigame.quantityOfInteractions <= m_interactionsCounter)
        {
            IOnEndInteraction();
        }
    }
    public void IOnMove(Vector2 p_vector) { }

    public void IOnLook(Vector2 p_vector) { }

    public void IOnMouseClick() { }

    public void IOnMouseDown() { }

    public void IOnMouseUp() { }

    bool m_canInteract = false;
    public void IOnCut()
    {
        if (!m_canInteract) return;

        StartCoroutine(CutAnim());
    }

    IEnumerator CutAnim()
    {
        m_canInteract = false;
        yield return m_currentKnife.Cut();
        m_canInteract = true;
        if (waitingUp) IOnEndedCut();
    }


    bool waitingUp = false;
    public void IOnEndedCut()
    {
        if (!m_canInteract)
        {
            waitingUp = true;
            return;
        }
        else waitingUp = false;
        //m_currentIngredient.AnimCut(m_interactionsCounter);
        m_interactionsCounter++;
        //ICheckEndInteraction();
        StartCoroutine(EndCutAnim());
    }
    IEnumerator EndCutAnim()
    {
        if (m_currentMinigame.quantityOfInteractions <= m_interactionsCounter)
            FadeController.Instance.CallFadeAnimation(true, null, .75f);
        m_canInteract = false;
        yield return m_currentKnife.EndCut();
        yield return m_currentKnife.MoveKnife(m_currentIngredient.GetKnifePosition((float)m_interactionsCounter / (float)m_currentMinigame.quantityOfInteractions));
        ICheckEndInteraction();
        m_canInteract = true;
    }
    public void IOnPressExit()
    {

    }
}
