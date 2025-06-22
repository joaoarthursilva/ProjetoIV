using ProjetoIV.Util;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    [SerializeField] private ObjectAnimationBehaviour m_behavior;
    [SerializeField] private UnityEngine.UI.Image[] m_buttonsImages;
    [SerializeField] private TMPro.TextMeshProUGUI[] m_texts;
    [SerializeField] private Ingredient[] ingredients;
    public System.Action<Ingredient> OnSelectRecipe;

    public void OpenBook()
    {
        m_behavior.PlayEnteryAnimations();
        for (int i = 0; i < m_buttonsImages.Length; i++) m_buttonsImages[i].raycastTarget = true;
    }

    public void CloseBook()
    {
        for (int i = 0; i < m_buttonsImages.Length; i++) m_buttonsImages[i].raycastTarget = false; 
        m_behavior.PlayLeaveAnimations();
    }

    public void SelectRecipe(int p_recipe)
    {
        CloseBook();
        OnSelectRecipe.Invoke(ingredients[p_recipe]);
    }
}
