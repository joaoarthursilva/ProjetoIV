using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIResizeOtherRect : MonoBehaviour
{
    [SerializeField] RectTransform m_tex;
    public RectTransform Rect { get { return m_tex; } }
    [SerializeField] List<RectToSize> m_rectList = new List<RectToSize>();

    [SerializeField] bool m_changeX, m_changeY;

    [SerializeField] UnityAction<bool> OnRectChange;
    [SerializeField] UnityAction<bool> OnExecuteAfterOneFrame;
    [SerializeField] UnityAction<bool> OnEndResizeAnim;

    private void OnEnable()
    {
        SetIntialPosition();
    }

    [NaughtyAttributes.Button]
    public void SetIntialPosition()
    {
        for (int i = 0; i < m_rectList.Count; i++)
        {
            m_rectList[i].SetInitialAnchoredPosition();
        }
    }

    Vector2 l_sizeDelta;
    private void OnRectTransformDimensionsChange()
    {
        OnRectChange?.Invoke(true);

        for (int i = 0; i < m_rectList.Count; i++)
        {
            if (m_changeX)
            {
                l_sizeDelta.x = (m_tex.sizeDelta.x * (m_rectList[i].useLocalScale ? m_tex.localScale.x : 1f)) + m_rectList[i].offset.x;
                if (m_rectList[i].cantBeNegativeX
                    && l_sizeDelta.x < 0) l_sizeDelta.x = 0;
            }
            else l_sizeDelta.x = m_rectList[i].rect.sizeDelta.x;

            if (m_changeY)
            {
                l_sizeDelta.y = (m_tex.sizeDelta.y * (m_rectList[i].useLocalScale ? m_tex.localScale.y : 1f)) + m_rectList[i].offset.y;

                if (m_rectList[i].cantBeNegativeY
                    && l_sizeDelta.y < 0) l_sizeDelta.y = 0;
            }
            else l_sizeDelta.y = m_rectList[i].rect.sizeDelta.y;

            if (!m_rectList[i].useAnim) m_rectList[i].rect.sizeDelta = l_sizeDelta;
            else
            {
                if (Application.isPlaying)
                {
                    if (gameObject.activeInHierarchy)
                    {
                        if (m_rectList[i].animCoroutine != null) StopCoroutine(m_rectList[i].animCoroutine);
                        m_rectList[i].animCoroutine = StartCoroutine(AnimResize(i, l_sizeDelta));
                    }
                }
                else m_rectList[i].rect.sizeDelta = l_sizeDelta;
            }

            for (int j = 0; j < m_rectList[i].imagesToChange.Length; j++)
            {
                if (m_rectList[i].rect.sizeDelta.y > m_rectList[i].imagesToChange[j].minToChange.y
                    || m_rectList[i].rect.sizeDelta.x > m_rectList[i].imagesToChange[j].minToChange.x)
                {
                    m_rectList[i].image.pixelsPerUnitMultiplier = m_rectList[i].imagesToChange[j].pixelPerUnit;
                    break;
                }
            }
        }

        if (gameObject.activeInHierarchy) StartCoroutine(WaitOneFrame());
    }

    IEnumerator WaitOneFrame()
    {
        yield return null;
        OnExecuteAfterOneFrame?.Invoke(true);

        for (int i = 0; i < m_rectList.Count; i++)
        {
            if (m_rectList[i].maintainAnchoredPosition) m_rectList[i].rect.anchoredPosition = m_rectList[i].InitialAnchoredPosition;
        }
    }

    IEnumerator AnimResize(int p_resizeIndex, Vector2 p_newSize)
    {
        Vector2 l_initialSize = m_rectList[p_resizeIndex].rect.sizeDelta;

        if (m_rectList[p_resizeIndex].setObjectOffIfHeightIs0 && l_initialSize.y - m_rectList[p_resizeIndex].offset.y == 0)
            m_rectList[p_resizeIndex].rect.gameObject.SetActive(p_newSize.y - m_rectList[p_resizeIndex].offset.y > 0);

        for (float l_time = 0; l_time < m_rectList[p_resizeIndex].animDuration; l_time += Time.unscaledDeltaTime)
        {
            m_rectList[p_resizeIndex].rect.sizeDelta = Vector2.Lerp(l_initialSize, p_newSize, m_rectList[p_resizeIndex].animCurve.Evaluate(l_time / m_rectList[p_resizeIndex].animDuration));

            yield return null;
        }

        m_rectList[p_resizeIndex].rect.sizeDelta = p_newSize;
        m_rectList[p_resizeIndex].animCoroutine = null;

        if (m_rectList[p_resizeIndex].setObjectOffIfHeightIs0)
            m_rectList[p_resizeIndex].rect.gameObject.SetActive(p_newSize.y - m_rectList[p_resizeIndex].offset.y > 0);

        OnEndResizeAnim?.Invoke(true);
    }

    [System.Serializable]
    class RectToSize
    {
        public RectTransform rect;
        public Vector2 offset;
        public RectTransform anotherElementToFitIn;
        public bool cantBeNegativeX, cantBeNegativeY, useLocalScale, maintainLocalPosition, maintainAnchoredPosition;
        public Vector2 InitialAnchoredPosition { get; private set; }
        public void SetInitialAnchoredPosition()
        {
            InitialAnchoredPosition = rect.anchoredPosition;
        }

        public Image image;
        public SetImage[] imagesToChange;
        public bool setObjectOffIfHeightIs0;
        [Header("Anim")]
        public bool useAnim = false;
        public float animDuration = 0.3f;
        public AnimationCurve animCurve;
        public Coroutine animCoroutine;

        [System.Serializable]
        public class SetImage
        {
            public Vector2 minToChange;
            public float pixelPerUnit = 1f;
        }

    }
}
