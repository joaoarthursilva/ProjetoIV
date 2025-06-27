using System.Collections;
using Assets.Plugins.RatLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDayPassManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_dayPassText;
    [SerializeField] private GameObject m_dayPassGameObject;
    [SerializeField] private AnimationCurve m_animationCurve;
    [SerializeField] private float m_timeToFade;

    void Awake()
    {
        TimeManager.Instance.OnStartDay += OnStartDay;
    }

    private void OnStartDay(Day day)
    {
        m_dayPassGameObject.SetActive(true);
        m_dayPassText.text = LocalizationManager.Localize(day.day);
        m_dayPassGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        m_dayPassText.color = new Color(1, 1, 1, 1);
        StartCoroutine(OnStartingNextDay());
    }

    IEnumerator OnStartingNextDay()
    {
        float l_currentTime = 0;
        float l_alpha;

        while (l_currentTime <= m_timeToFade)
        {
            l_alpha = m_animationCurve.Evaluate(l_currentTime / m_timeToFade);
            m_dayPassText.color = new Color(1, 1, 1, l_alpha);
            m_dayPassGameObject.GetComponent<Image>().color = new Color(0, 0, 0, l_alpha);
            l_currentTime += Time.deltaTime;
            yield return null;
        }

        m_dayPassGameObject.SetActive(false);
    }
}
