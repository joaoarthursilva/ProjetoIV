using System;
using ProjetoIV.Util;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    public Action<Ingredient> OnChangeInventory;
    [SerializeField] private Ingredient m_currentIngredient;
    public Ingredient CurrentIngredient => m_currentIngredient;
    [SerializeField] private OnHandIngredient[] m_onHandIngredients;
    [System.Serializable]
    private struct OnHandIngredient
    {
        public Ingredient ingredient;
        public GameObject gameObject;
    }

    Ingredient l_inventory;
    void Update()
    {
        if (m_currentIngredient != l_inventory)
        {
            OnChangeInventory?.Invoke(m_currentIngredient);
            l_inventory = m_currentIngredient;
        }
    }

    public void SetCurrentInventory(Ingredient p_ingredient)
    {
        m_currentIngredient = p_ingredient;

        for (int i = 0; i < m_onHandIngredients.Length; i++)
            m_onHandIngredients[i].gameObject.SetActive(m_onHandIngredients[i].ingredient == p_ingredient);

        OnChangeInventory?.Invoke(m_currentIngredient);
    }

    public void SetInventoryObject(GameObject p_gameObject, Ingredient p_ingredient)
    {
        for (int i = 0; i < m_onHandIngredients.Length; i++)
        {
            if(m_onHandIngredients[i].ingredient == p_ingredient)
            {
                p_gameObject.transform.SetParent(m_onHandIngredients[i].gameObject.transform.parent);
                p_gameObject.transform.SetLocalPositionAndRotation(m_onHandIngredients[i].gameObject.transform.localPosition,
                                                                    m_onHandIngredients[i].gameObject.transform.localRotation);
                DestroyImmediate(m_onHandIngredients[i].gameObject);
                m_onHandIngredients[i].gameObject = p_gameObject;
            }
        }

    }
}
