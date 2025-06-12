using System;
using UnityEngine;
using System.Collections.Generic;
using ProjetoIV.Util;

public class RoutineManager : Singleton<RoutineManager>
{
    private Day m_today;
    private List<Customer> m_customers;
    private int m_currentCustomer = 0;

    public void Awake()
    {
        TimeManager.Instance.OnStartDay += OnStartDay;
        TimeManager.Instance.OnNextCustomer += OnNextCustomer;
    }

    private void OnNextCustomer()
    {
        Debug.Log("OnNextCustomer");
        m_currentCustomer++;
        if (m_currentCustomer >= m_customers.Count) TimeManager.Instance.StartNextDay();
        else CustomerManager.Instance.SpawnCustomer(m_customers[m_currentCustomer]);
    }

    public void OnStartDay(Day p_day)
    {
        Debug.Log("OnStartDay");
        m_today = p_day;
        m_currentCustomer = 0;
        m_customers = m_today.customers;
        CustomerManager.Instance.SpawnCustomer(m_customers[m_currentCustomer]);
    }

}