using FMODUnity;
using ProjetoIV.Audio;
using ProjetoIV.Util;
using UnityEngine;
using UnityEngine.UI;

public class BookBehavior : MonoBehaviour
{
    [SerializeField] private ObjectAnimationBehaviour m_behavior;
    [SerializeField] private GameObject[] m_recipeGameObject;
    [SerializeField] private GameObject[] m_flipButtonGameObject;
    [SerializeField] private TMPro.TextMeshProUGUI[] m_texts;
    [SerializeField] private Ingredient[] ingredients;
    public System.Action<Ingredient> OnSelectRecipe;
    [SerializeField] private EventReference pageSFX;
    [SerializeField] private Button m_selectButton;
    [SerializeField] private UIAnimationBehaviour m_selectButtonAnimBehavior;
    public void OpenBook()
    {
        if (ArrowIndicator.Instance != null) ArrowIndicator.Instance.HideArrow();
        m_behavior.PlayEnteryAnimations();
        AudioManager.Instance.Play(pageSFX);
        for (int i = 0; i < m_flipButtonGameObject.Length; i++) m_flipButtonGameObject[i].SetActive(true);
        m_selectButtonAnimBehavior.PlayEntryAnimations();
    }

    public void CloseBook()
    {
        AudioManager.Instance.Play(pageSFX);
        for (int i = 0; i < m_flipButtonGameObject.Length; i++) m_flipButtonGameObject[i].SetActive(false);
        m_behavior.PlayLeaveAnimations();
        m_selectButtonAnimBehavior.PlayLeaveAnimations();
    }

    public void SelectRecipe(int p_recipe)
    {
        CloseBook();
        OnSelectRecipe.Invoke(ingredients[p_recipe]);
    }

    private int l_currentRecipe = 0;
    public void ButtonBack()
    {
        if (l_currentRecipe - 1 >= 0)
        {
            //m_recipeGameObject[l_currentRecipe].SetActive(false);
            //m_recipeGameObject[l_currentRecipe - 1].SetActive(true);
            l_currentRecipe--;
        }
    }

    public void ButtonForward()
    {
        if (l_currentRecipe + 1 < m_recipeGameObject.Length)
        {
            //m_recipeGameObject[l_currentRecipe].SetActive(false);
            //m_recipeGameObject[l_currentRecipe + 1].SetActive(true);
            l_currentRecipe++;
        }
    }

    public void SelectCurrentRecipe()
    {
        OnSelectRecipe.Invoke(ingredients[l_currentRecipe]);
        CloseBook();
    }
}
