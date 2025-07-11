using System.Collections;
using ProjetoIV.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuickTimeButton : MonoBehaviour, IPointerClickHandler
{
    public const float PUNISHMENT = 0.2f;
    [SerializeField] private RectTransform m_rectTransform;
    [Space]
    [SerializeField] private RectTransform m_variantCircleRect;
    [SerializeField] private Vector2 m_variantCircleMaxSize;
    [SerializeField] private Vector2 m_variantCircleMinSize;
    [SerializeField] private float m_variantCircleTimeCycle;
    [SerializeField] private Vector2 m_timeRangeToSuccess;
    [SerializeField] private Image m_variantCircleImage;
    public bool Done;

    Coroutine m_coroutine;
    public void OnPointerClick(PointerEventData eventData)
    {
        l_waitingClick = false;
    }

    public void Click()
    {
        l_waitingClick = false;
    }

    public void ResetClick()
    {
        Done = false;
        l_waitingClick = true;
        if (m_coroutine != null) StopCoroutine(m_coroutine);
    }

    public void StartClick()
    {
        gameObject.SetActive(true);

        m_coroutine = StartCoroutine(CircleCicle());
    }

    bool l_waitingClick;
    [SerializeField, NaughtyAttributes.ReadOnly] float m_time;
    IEnumerator CircleCicle()
    {
        while (!Done)
        {
            l_waitingClick = true;
            m_time = m_variantCircleTimeCycle;
            while (l_waitingClick)
            {
                m_time -= Time.deltaTime;
                if (m_time / m_variantCircleTimeCycle > m_timeRangeToSuccess.x
                    && m_time / m_variantCircleTimeCycle < m_timeRangeToSuccess.y) m_variantCircleImage.color = Color.green;
                else m_variantCircleImage.color = Color.white;

                m_variantCircleRect.sizeDelta = Vector2.Lerp(m_variantCircleMinSize, m_variantCircleMaxSize, m_time / m_variantCircleTimeCycle);

                yield return null;

                if (m_time <= 0f) m_time = m_variantCircleTimeCycle;
            }

            if (m_time / m_variantCircleTimeCycle > m_timeRangeToSuccess.x
                && m_time / m_variantCircleTimeCycle < m_timeRangeToSuccess.y)
            {
                Done = true;
                AudioManager.Instance.Play(AudioID.PASTA_FOLD_RIGHT);
            }
            else
            {
                AudioManager.Instance.Play(AudioID.PASTA_FOLD_WRONG);
                m_variantCircleImage.color = Color.red;
                yield return new WaitForSeconds(PUNISHMENT);
                m_variantCircleImage.color = Color.white;
            }
        }
    }

    RectTransform m_canvasRect;
    Vector3 l_tempPos;
    public void SetPosition(Transform p_transform, RectTransform p_canvasRect)
    {
        Done = false;
        m_canvasRect = p_canvasRect;
        l_tempPos = Camera.main.WorldToViewportPoint(p_transform.position);
        l_tempPos.x *= m_canvasRect.sizeDelta.x * m_canvasRect.localScale.x;
        l_tempPos.y *= m_canvasRect.sizeDelta.y * m_canvasRect.localScale.y;
        l_tempPos.z = 0;

        m_rectTransform.anchoredPosition = l_tempPos;
    }
}