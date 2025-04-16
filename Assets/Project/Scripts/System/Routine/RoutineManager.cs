using System;
using UnityEngine;
using System.Collections.Generic;
using ProjetoIV.Util;

public class RoutineManager : Singleton<RoutineManager>
{
    public TimeManager TimeManager;
    public CustomerManager CustomerManager;
    private Day m_today;
    private List<Customer> m_customers;
    private int m_currentCustomer = 0;

    public void Start()
    {
        TimeManager.OnStartDay += OnStartDay;
    }

    public void OnStartDay(Day p_day)
    {
        m_today = p_day;
        m_customers = m_today.customers;
        CustomerManager.SpawnCustomer(m_customers[m_currentCustomer]);
    }

}