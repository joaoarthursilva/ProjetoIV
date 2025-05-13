using UnityEngine;
using ProjetoIV.Util;

public class CustomerManager : Singleton<CustomerManager>
{
    private Customer m_currentCustomer;
    private CustomerBehaviour m_customerBehaviour;

    void Awake()
    {
        m_customerBehaviour = FindAnyObjectByType<CustomerBehaviour>();
    }

    public void SpawnCustomer(Customer p_customer)
    {
        Debug.Log("SpawnCustomer");

        m_currentCustomer = p_customer;
        m_customerBehaviour.SpawnCustomer(m_currentCustomer);
    }
}