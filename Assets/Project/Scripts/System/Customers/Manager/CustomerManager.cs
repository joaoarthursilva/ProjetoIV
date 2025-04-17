using System;
using UnityEngine;
using System.Collections.Generic;
using ProjetoIV.Util;

public class CustomerManager : Singleton<CustomerManager>
{
    private Customer m_currentCustomer;
    [SerializeField] private GameObject m_customerGameObject;

    private GameObject m_currentCustomerGameObject;

    public void SpawnCustomer(Customer p_customer)
    {
        Debug.Log("SpawnCustomer");
        m_currentCustomer = p_customer;
        m_currentCustomerGameObject = Instantiate(m_customerGameObject);

        m_currentCustomerGameObject.GetComponent<CustomerBehaviour>().Customer = m_currentCustomer;
    }
}