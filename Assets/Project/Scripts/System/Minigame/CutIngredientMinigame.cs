using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutIngredientMinigame : MonoBehaviour, IMinigameInteraction
{
    [SerializeField] private Minigame[] m_minigames;
    public Minigame CurrentMinigame;

    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera MinigameCamera => m_camera;

    [SerializeField] private List<RaycastableMinigame> m_raycastable;
    public List<RaycastableMinigame> RaycastableMinigame => m_raycastable;

    [Space]
    private Ingredient m_initialIngredient;
    [SerializeField] private Transform m_ingredientParent;
    [SerializeField] private IngredientBehavior m_currentIngredient;
    [SerializeField] private ProccessMinigame m_currentMinigame;
    [SerializeField] private int m_interactionsCounter;
    private System.Action m_onEndAction;
    public bool EmbraceMinigame(Ingredient p_minigame, out Minigame o_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i].InitialIngredient() == p_minigame)
            {
                o_minigame = m_minigames[i];
                return true;
            }
        }

        o_minigame = null;
        return false;
    }

    public void IOnStartInteraction(Minigame p_minigame, System.Action p_actionOnEnd)
    {
        m_currentMinigame = p_minigame as ProccessMinigame;
        m_initialIngredient = p_minigame.InitialIngredient();
        m_onEndAction = p_actionOnEnd;
        SpawnIngredient();
    }

    void SpawnIngredient()
    {
        m_currentIngredient = Instantiate(m_initialIngredient.prefab, m_ingredientParent).GetComponent<IngredientBehavior>();
        m_currentIngredient.SetProcessed(false);
    }

    public void IOnEndInteraction()
    {
        Invoke(nameof(DestroyIngredient), 1f);
        m_onEndAction?.Invoke();
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

    public void IOnCut()
    {

    }

    public void IOnEndedCut()
    {
        m_currentIngredient.AnimCut(m_interactionsCounter);
        m_interactionsCounter++;
        ICheckEndInteraction();
    }
}
