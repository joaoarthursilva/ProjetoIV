using System;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Action<bool> OnOrderDelivered;
    [SerializeField] private Customer m_customer;

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