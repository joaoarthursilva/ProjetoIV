using System;
using UnityEngine;
using System.Collections.Generic;
using ProjetoIV.Util;
using System.Collections;

public class TimeManager : Singleton<TimeManager>
{
    public Action<Day> OnStartDay;
    public Action<int> OnEndDay;
    public Action<float> OnPassTime;
    public Action OnNextCustomer;

    [SerializeField] private List<Day> m_days;
    [SerializeField] private int m_today = 0;
    [SerializeField] private float m_now = 0f;

    public void Start()
    {
        StartNextDay();
    }

    public void StartNextDay()
    {
        Debug.Log("StartNextDay");
        StartCoroutine(OnStartNextDay());
    }

    public void PassTime(float p_timePassed, bool p_orderDelivered)
    {
        m_now += p_timePassed;
        OnPassTime?.Invoke(m_now);
        CheckEndDay(p_orderDelivered);
    }

    public void CheckEndDay(bool p_orderDelivered)
    {
        if (m_now >= m_days[m_today].end && p_orderDelivered)
        {
            OnEndDay?.Invoke(m_today);
            m_today++;
            StartNextDay();
        }
        else
        {
            if (p_orderDelivered) OnNextCustomer?.Invoke();
        }
    }

    IEnumerator OnStartNextDay()
    {
        yield return new WaitForSeconds(m_today == 0 ? 0 : m_days[m_today - 1].delayBeforeNextDay);
        m_now = m_days[m_today].start;
        OnStartDay?.Invoke(m_days[m_today]);
        OnPassTime?.Invoke(m_now);
    }

    //action receita entregue
    //if (m_now >= m_days[m_today].end)
    //{
    //OnEndDay?.Invoke(m_today);
    //m_today++;
    //}

}