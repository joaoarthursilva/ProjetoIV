using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDayPassManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_dayPassText;
    [SerializeField] private GameObject m_dayPassGameObject;

    void Awake()
    {
        TimeManager.Instance.OnStartDay += OnStartDay;
    }

    private void OnStartDay(Day day)
    {
        m_dayPassGameObject.SetActive(true);
        m_dayPassText.text = "Starting day " + day.day;
        m_dayPassGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        StartCoroutine(OnStartingNextDay());
    }

    IEnumerator OnStartingNextDay()
    {
        float l_alpha = m_dayPassGameObject.GetComponent<Image>().color.a;

        for (float t = 0.0f; t < 1.5f; t += Time.deltaTime / 1.5f)
        {
            m_dayPassGameObject.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Lerp(l_alpha, 0f, t));
            yield return null;
        }

        m_dayPassGameObject.SetActive(false);
    }
}
