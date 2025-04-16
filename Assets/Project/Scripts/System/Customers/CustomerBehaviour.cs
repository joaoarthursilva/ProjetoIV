using System;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Action<bool> OnOrderDelivered;
    public Customer Customer;

    public void CheckOrder(Ingredient p_ingredient)
    {
        if (p_ingredient == Customer.ingredient)
        {
            OnOrderDelivered?.Invoke(true);
        }
        else
        {
            OnOrderDelivered?.Invoke(false);
        }
    }
}