using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Action<bool> OnOrderDelivered;

    [SerializeField] private List<GameObject> m_models;
    [SerializeField] private Vector3 m_spawnPosition;
    private Customer m_customer;

    public void SpawnCustomer(Customer p_customer)
    {
        m_customer = p_customer;

        transform.position = m_spawnPosition;

        if (m_customer.character != null && m_customer.character.characterId != RatSpeak.CharacterId.NONE)
            m_models[(int)m_customer.character.characterId - 1].SetActive(true);
    }

    public void OnInteractWithRaycastableObject()
    {
        Debug.Log("interact with customer");
    }

    public void CheckOrder(Ingredient p_ingredient)
    {
        if (p_ingredient == m_customer.ingredient)
        {
            OnOrderDelivered?.Invoke(true);
        }
        else
        {
            OnOrderDelivered?.Invoke(false);
        }
    }
}