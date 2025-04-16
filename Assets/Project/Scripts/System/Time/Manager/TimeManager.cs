using System;
using UnityEngine;
using System.Collections.Generic;
using ProjetoIV.Util;

public class TimeManager : Singleton<TimeManager>
{
    public Action<Day> OnStartDay;
    public Action<int> OnEndDay;
    public Action<float> OnPassTime;

    [SerializeField] private List<Day> m_days;
    private int m_today = 0;
    private float m_now = 0f;

    public void Start()
    {
        StartNextDay();
    }

    public void StartNextDay()
    {
        m_now = m_days[m_today].start;
        OnStartDay?.Invoke(m_days[m_today]);
        OnPassTime?.Invoke(m_now);
    }

    public void PassTime(float p_timePassed)
    {
        m_now += p_timePassed;
        OnPassTime?.Invoke(m_now);
        CheckEndDay();
    }

    public void CheckEndDay()
    {
        if (m_now >= m_days[m_today].end /* && receita entregue */)
        {
            OnEndDay?.Invoke(m_today);
            m_today++;
        }
    }

    //action receita entregue
        //if (m_now >= m_days[m_today].end)
        //{
            //OnEndDay?.Invoke(m_today);
            //m_today++;
        //}
    
}