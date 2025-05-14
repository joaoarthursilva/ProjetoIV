using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoldButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform rectTransform;

    [Header("Animation")]
    [SerializeField] private Vector2 m_initialSize;
    [SerializeField] private Vector2 m_finalSize;
    [SerializeField] private AnimationCurve m_circleCurve;
    [SerializeField] private RectTransform m_innerCircleRect;

    [Header("Interaction")]
    [SerializeField, NaughtyAttributes.ReadOnly] private bool m_holding;
    [SerializeField, NaughtyAttributes.ReadOnly] private float m_holdFloat;
    [SerializeField] private float m_holdTime;

    Coroutine m_holdingCorutine;
    public void OnPointerDown(PointerEventData eventData)
    {
        m_holding = true;
        if (m_holdingCorutine != null) StopCoroutine(m_holdingCorutine);

        m_holdingCorutine = StartCoroutine(HandleHolding());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        m_holding = false;
    }

    IEnumerator HandleHolding()
    {
        while (m_holding && m_holdFloat < m_holdTime)
        {
            m_holdFloat += Time.deltaTime;
            UpdateInnerCircle();
            yield return null;
        }

        while (!m_holding && m_holdFloat < m_holdTime && m_holdFloat >= 0)
        {
            m_holdFloat -= Time.deltaTime;
            UpdateInnerCircle();
            yield return null;
        }

        if (m_holding && m_holdFloat >= m_holdTime)
        {
            Debug.Log("foi!");
        }

        m_holdingCorutine = null;
    }

    private void UpdateInnerCircle()
    {
        m_innerCircleRect.sizeDelta = Vector2.Lerp(m_initialSize, m_finalSize, m_circleCurve.Evaluate(m_holdFloat / m_holdTime));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    RectTransform m_canvasRect;
    [SerializeField] Camera m_camera;
    public void SetPosition(Transform p_transform, RectTransform p_canvasRect)
    {
        m_canvasRect = p_canvasRect;
        Vector3 l_tempPos = m_camera.WorldToViewportPoint(p_transform.position);
        l_tempPos.x *= m_canvasRect.sizeDelta.x * m_canvasRect.localScale.x;
        l_tempPos.y *= m_canvasRect.sizeDelta.y * m_canvasRect.localScale.y;
        l_tempPos.z = 0;

        rectTransform.anchoredPosition = l_tempPos;
    }
}
