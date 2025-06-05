using System;
using System.Collections.Generic;
using RatSpeak;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Action<bool> OnOrderDelivered;

    [SerializeField] private List<GameObject> m_models;
    [SerializeField] private Vector3 m_spawnPosition;
    [SerializeField] private Customer m_customer;

    private bool m_isNewCustomer;

    public void SpawnCustomer(Customer p_customer)
    {
        m_isNewCustomer = true;

        m_customer = p_customer;

        transform.position = m_spawnPosition;

        if (m_customer.character != null && m_customer.character.characterId != RatSpeak.CharacterId.NONE)
        {
            foreach (var model in m_models) model.SetActive(false);
            m_models[(int)m_customer.character.characterId - 1].SetActive(true);
        }
    }

    public void OnInteractWithRaycastableObject()
    {
        if (m_isNewCustomer)
        {
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.PEDIDO).key);
            m_isNewCustomer = false;
        }
        else if (PlayerInventory.Instance.currentIngredient != null)
        {
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.ENTREGA).key);
        }
        else
        {
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.ESPERA).key);
        }
    }

    public void CheckOrder(Ingredient p_ingredient)
    {
        if (p_ingredient == m_customer.ingredient)
        {
            PlayerInventory.Instance.currentIngredient = null;
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.RESULTADO_BOM).key);
            // OnOrderDelivered(true);
        }
        else
        {
            PlayerInventory.Instance.currentIngredient = null;
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.RESULTADO_RUIM).key);
            // OnOrderDelivered(false);
        }
    }
}