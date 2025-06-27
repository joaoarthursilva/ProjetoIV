using ProjetoIV.Util;
using UnityEngine;
using UnityEngine.UI;

public class BookBehavior : MonoBehaviour
{
    [SerializeField] private ObjectAnimationBehaviour m_behavior;
    [SerializeField] private GameObject[] m_recipeGameObject;
    [SerializeField] private GameObject[] m_flipButtonGameObject;
    [SerializeField] private Image[] m_buttonsImages;
    [SerializeField] private TMPro.TextMeshProUGUI[] m_texts;
    [SerializeField] private Ingredient[] ingredients;
    public System.Action<Ingredient> OnSelectRecipe;

    public void OpenBook()
    {
        if (ArrowIndicator.Instance != null) ArrowIndicator.Instance.HideArrow();
        m_behavior.PlayEnteryAnimations();
        for (int i = 0; i < m_buttonsImages.Length; i++) m_buttonsImages[i].raycastTarget = true;
        for (int i = 0; i < m_flipButtonGameObject.Length; i++) m_flipButtonGameObject[i].SetActive(true);
    }

    public void CloseBook()
    {
        for (int i = 0; i < m_buttonsImages.Length; i++) m_buttonsImages[i].raycastTarget = false;
        for (int i = 0; i < m_flipButtonGameObject.Length; i++) m_flipButtonGameObject[i].SetActive(false);
        m_behavior.PlayLeaveAnimations();
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
            m_recipeGameObject[l_currentRecipe].SetActive(false);
            m_recipeGameObject[l_currentRecipe - 1].SetActive(true);
            l_currentRecipe--;
        }
    }

    public void ButtonForward()
    {
        if (l_currentRecipe + 1 < m_recipeGameObject.Length)
        {
            m_recipeGameObject[l_currentRecipe].SetActive(false);
            m_recipeGameObject[l_currentRecipe + 1].SetActive(true);
            l_currentRecipe++;
        }
    }
}
