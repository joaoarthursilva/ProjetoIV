using UnityEngine;
using UnityEngine.UI;

public class UICurrentInventory : MonoBehaviour
{
    [SerializeField] private Image m_dayPassGameObject;

    void Awake()
    {
        PlayerInventory.Instance.OnChangeInventory += OnChangeInventory;
    }

    private void OnChangeInventory(Ingredient ingredient)
    {
        if (ingredient == null)
        {
            m_dayPassGameObject.color = new Color(1, 1, 1, 0);
        }
        else
        {
            m_dayPassGameObject.color = new Color(1, 1, 1, 1);
            m_dayPassGameObject.sprite = ingredient.sprite;
        }
    }
}
