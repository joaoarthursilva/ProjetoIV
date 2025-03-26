using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UIAnimation
{
    [Header("General")]
    public RectTransform uiElement;

    [Header("Fade")]
    public bool shouldFade;
    [ShowIf("shouldFade")][AllowNesting] public bool alwaysBlockRaycast = false;
    [ShowIf("shouldFade")][AllowNesting][Range(0, 1)] public float startAlpha;
    [ShowIf("shouldFade")][AllowNesting][Range(0, 1)] public float endAlpha;
    [ShowIf("shouldFade")][AllowNesting] public float fadeAnimationTime;
    [ShowIf("shouldFade")][AllowNesting] public AnimationCurve fadeAnimationCurve;
    [ShowIf("shouldFade")][AllowNesting] public float fadeDelayTime;

    [Header("Move")]
    public bool shouldMove;
    public bool useLocalPosition;
    public bool setInitialPosition = false;
    [ShowIf("shouldMove")][EnableIf("useLocalPosition")][AllowNesting] public RectTransform parent;
    [ShowIf("shouldMove")][EnableIf("useLocalPosition")][AllowNesting] public Vector3 startLocalPosition;
    [ShowIf("shouldMove")][EnableIf("useLocalPosition")][AllowNesting] public Vector3 finalLocalPosition;
    [ShowIf("shouldMove")][DisableIf("useLocalPosition")][AllowNesting] public Vector3 startPosition;
    [ShowIf("shouldMove")][DisableIf("useLocalPosition")][AllowNesting] public Vector3 finalPosition;
    [ShowIf("shouldMove")][AllowNesting] public float moveAnimationTime;
    [ShowIf("shouldMove")][AllowNesting] public AnimationCurve moveAnimationCurve;
    [ShowIf("shouldMove")][AllowNesting] public float moveDelayTime;

    [Header("Scale")]
    [ShowIf("isEnabled")] public bool shouldScale;
    [ShowIf("shouldScale")][AllowNesting] public Vector3 startScale;
    [ShowIf("shouldScale")][AllowNesting] public Vector3 finalScale;
    [ShowIf("shouldScale")][AllowNesting] public float scaleAnimationTime;
    [ShowIf("shouldScale")][AllowNesting] public AnimationCurve scaleAnimationCurve;
    [ShowIf("shouldScale")][AllowNesting] public float scaleDelayTime;

    public Coroutine animCoroutine;

    bool l_usesCurve = false;
    float l_timeCounter = 0;
    CanvasGroup l_canvasGroup;
    Vector3 l_initialPosition;
    public IEnumerator AnimateUIElement(UIAnimation p_uiAnimation, UIAnimationType p_uiAnimationType)
    {
        if (p_uiAnimation.uiElement == null) yield break;

        bool l_isFadeIn;
        float l_fadeTimeCounter = 0, l_moveTimeCounter = 0, l_scaleTimeCounter = 0;
        bool l_isFadeComplete = !p_uiAnimation.shouldFade, l_isMoveComplete = !p_uiAnimation.shouldMove, l_isScaleComplete = !p_uiAnimation.shouldScale;

        l_timeCounter = 0;

        if (l_canvasGroup == null) l_canvasGroup = p_uiAnimation.uiElement.GetComponent<CanvasGroup>();
        if (l_canvasGroup != null)
        {
            l_canvasGroup.alpha = p_uiAnimation.startAlpha;
            if (!alwaysBlockRaycast) l_canvasGroup.blocksRaycasts = true;
        }

        if (p_uiAnimation.shouldMove)
        {
            if (setInitialPosition)
            {
                if (p_uiAnimation.useLocalPosition) l_initialPosition = p_uiAnimation.uiElement.anchoredPosition;
                else l_initialPosition = p_uiAnimation.uiElement.transform.position;
            }
            else
            {
                if (p_uiAnimation.useLocalPosition) l_initialPosition = p_uiAnimation.startLocalPosition;
                else l_initialPosition = p_uiAnimation.startPosition;
            }
        }

        while (!l_isFadeComplete || !l_isMoveComplete || !l_isScaleComplete)
        {
            if (!l_isFadeComplete)
            {
                if (l_timeCounter >= p_uiAnimation.fadeDelayTime)
                {
                    l_isFadeIn = p_uiAnimation.startAlpha < p_uiAnimation.endAlpha;
                    l_fadeTimeCounter = l_timeCounter - p_uiAnimation.fadeDelayTime;
                    l_usesCurve = p_uiAnimation.fadeAnimationCurve.length > 0;

                    if (l_usesCurve)
                    {
                        l_canvasGroup.alpha = p_uiAnimation.fadeAnimationCurve.Evaluate(Mathf.LerpUnclamped(p_uiAnimation.startAlpha, l_isFadeIn ? 1 : 0, l_fadeTimeCounter / p_uiAnimation.fadeAnimationTime));
                    }
                    else
                    {
                        l_canvasGroup.alpha = Mathf.LerpUnclamped(p_uiAnimation.startAlpha, l_isFadeIn ? 1 : 0, l_fadeTimeCounter / p_uiAnimation.fadeAnimationTime);
                    }

                    if (l_fadeTimeCounter / p_uiAnimation.fadeAnimationTime >= 1)
                    {
                        l_canvasGroup.alpha = l_isFadeIn ? 1 : 0;

                        if (l_isFadeIn)
                        {
                            l_canvasGroup.interactable = true;
                            if (!alwaysBlockRaycast) l_canvasGroup.blocksRaycasts = true;
                        }
                        else
                        {
                            l_canvasGroup.interactable = false;
                            l_canvasGroup.blocksRaycasts = false;
                        }

                        l_isFadeComplete = true;
                    }
                }
            }

            if (!l_isMoveComplete)
            {
                if (l_timeCounter >= p_uiAnimation.moveDelayTime)
                {
                    l_moveTimeCounter = l_timeCounter - p_uiAnimation.moveDelayTime;
                    l_usesCurve = p_uiAnimation.moveAnimationCurve.length > 0;

                    if (l_usesCurve)
                    {
                        if (p_uiAnimation.useLocalPosition) p_uiAnimation.uiElement.anchoredPosition = Vector3.LerpUnclamped(l_initialPosition, p_uiAnimation.finalLocalPosition, p_uiAnimation.moveAnimationCurve.Evaluate(l_moveTimeCounter / p_uiAnimation.moveAnimationTime));
                        else p_uiAnimation.uiElement.transform.position = Camera.main.ViewportToScreenPoint(Vector3.LerpUnclamped(l_initialPosition, p_uiAnimation.finalPosition, p_uiAnimation.moveAnimationCurve.Evaluate(l_moveTimeCounter / p_uiAnimation.moveAnimationTime)));
                    }
                    else
                    {
                        if (p_uiAnimation.useLocalPosition) p_uiAnimation.uiElement.anchoredPosition = Vector3.LerpUnclamped(l_initialPosition, p_uiAnimation.finalLocalPosition, l_moveTimeCounter / p_uiAnimation.moveAnimationTime);
                        else p_uiAnimation.uiElement.transform.position = Camera.main.ViewportToScreenPoint(Vector3.LerpUnclamped(l_initialPosition, p_uiAnimation.finalPosition, l_moveTimeCounter / p_uiAnimation.moveAnimationTime));
                    }

                    if (l_moveTimeCounter / p_uiAnimation.moveAnimationTime >= 1) l_isMoveComplete = true;
                }
            }

            if (!l_isScaleComplete)
            {
                if (l_timeCounter >= p_uiAnimation.scaleDelayTime)
                {
                    l_scaleTimeCounter = l_timeCounter - p_uiAnimation.scaleDelayTime;
                    l_usesCurve = p_uiAnimation.scaleAnimationCurve.length > 0;

                    if (l_usesCurve)
                    {
                        p_uiAnimation.uiElement.localScale = Vector3.LerpUnclamped(p_uiAnimation.startScale, p_uiAnimation.finalScale, p_uiAnimation.scaleAnimationCurve.Evaluate(l_scaleTimeCounter / p_uiAnimation.scaleAnimationTime));
                    }
                    else
                    {
                        p_uiAnimation.uiElement.localScale = Vector3.LerpUnclamped(p_uiAnimation.startScale, p_uiAnimation.finalScale, l_scaleTimeCounter / p_uiAnimation.scaleAnimationTime);
                    }

                    if (l_scaleTimeCounter / p_uiAnimation.scaleAnimationTime >= 1) l_isScaleComplete = true;
                }
            }

            l_timeCounter += Time.deltaTime;
            yield return null;
        }

        animCoroutine = null;
    }
}

public enum UIAnimationType { ENTRY, LEAVE }

public class UIAnimationBehaviour : MonoBehaviour
{
    public string animationsName;

    [SerializeField] UIAnimationType m_currentState;

    public bool enableAnimationsOnStart;
    public List<UIAnimation> entryUiAnimationsList;
    public List<UIAnimation> leaveUiAnimationsList;

    [Header("Actions")]
    public UnityEvent<bool> OnEntryAnimationsStarted;
    public UnityEvent<bool> OnEntryAnimationsFinished;
    public UnityEvent<bool> OnLeaveAnimationsStarted;
    public UnityEvent<bool> OnLeaveAnimationsFinished;

    [Header("Debug")]
    public RectTransform m_debugRectTransform;

    private void Start()
    {
        if (enableAnimationsOnStart) PlayAnimations(UIAnimationType.ENTRY);
    }

    public void PlayLeaveAnimations()
    {
        PlayAnimations(UIAnimationType.LEAVE);
    }

    public void PlayEnteryAnimations()
    {
        PlayAnimations(UIAnimationType.ENTRY);
    }

    public void PlayChangeAnimations()
    {
        if (m_currentState == UIAnimationType.LEAVE) PlayAnimations(UIAnimationType.ENTRY);
        else PlayAnimations(UIAnimationType.LEAVE);
    }

    Coroutine l_allAnimCoroutine;
    public Coroutine PlayAnimations(UIAnimationType p_uiAnimationType, Action p_action = null)
    {
        if (l_allAnimCoroutine != null) StopCoroutine(l_allAnimCoroutine);

        if (p_uiAnimationType.Equals(UIAnimationType.ENTRY))
        {
            m_currentState = UIAnimationType.ENTRY;

            if (gameObject.activeInHierarchy)
            {
                for (int i = 0; i < entryUiAnimationsList.Count; i++)
                {
                    if (entryUiAnimationsList[i].animCoroutine != null) StopCoroutine(entryUiAnimationsList[i].animCoroutine);
                    entryUiAnimationsList[i].animCoroutine = StartCoroutine(entryUiAnimationsList[i].AnimateUIElement(entryUiAnimationsList[i], UIAnimationType.ENTRY));
                }
            }
        }
        else
        {
            m_currentState = UIAnimationType.LEAVE;

            if (gameObject.activeInHierarchy)
            {
                for (int i = 0; i < leaveUiAnimationsList.Count; i++)
                {
                    if (leaveUiAnimationsList[i].animCoroutine != null) StopCoroutine(leaveUiAnimationsList[i].animCoroutine);
                    leaveUiAnimationsList[i].animCoroutine = StartCoroutine(leaveUiAnimationsList[i].AnimateUIElement(leaveUiAnimationsList[i], UIAnimationType.LEAVE));
                }
            }
        }

        if (gameObject.activeInHierarchy) return l_allAnimCoroutine = StartCoroutine(WaitAnims(p_uiAnimationType, p_action));

        return null;
    }

    IEnumerator WaitAnims(UIAnimationType p_uiAnimationType, Action p_action)
    {
        if (p_uiAnimationType == UIAnimationType.ENTRY) OnEntryAnimationsStarted?.Invoke(true);
        else if (p_uiAnimationType == UIAnimationType.LEAVE) OnLeaveAnimationsStarted?.Invoke(true);

        yield return null;

        bool l_allFinished = true;

        do
        {
            l_allFinished = true;

            if (p_uiAnimationType.Equals(UIAnimationType.ENTRY))
            {
                for (int i = 0; i < entryUiAnimationsList.Count; i++)
                {
                    if (entryUiAnimationsList[i].animCoroutine != null)
                    {
                        l_allFinished = false;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < leaveUiAnimationsList.Count; i++)
                {
                    if (leaveUiAnimationsList[i].animCoroutine != null)
                    {
                        l_allFinished = false;
                        break;
                    }
                }
            }

            yield return null;
        }
        while (!l_allFinished);

        if (p_uiAnimationType == UIAnimationType.ENTRY) OnEntryAnimationsFinished?.Invoke(true);
        else if (p_uiAnimationType == UIAnimationType.LEAVE) OnLeaveAnimationsFinished?.Invoke(true);

        if (p_action != null) p_action?.Invoke();
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(UIAnimationBehaviour))]
public class UIAnimationsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIAnimationBehaviour script = (UIAnimationBehaviour)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Debug position"))
        {
            Debug.Log($"Element position " + Camera.main.ScreenToViewportPoint(script.m_debugRectTransform.transform.position));
            Debug.Log($"Element localposition " + script.m_debugRectTransform.anchoredPosition);
        }

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Debug entry animation"))
            {
                script.PlayAnimations(UIAnimationType.ENTRY);
            }
            else if (GUILayout.Button("Debug leave animation"))
            {
                script.PlayAnimations(UIAnimationType.LEAVE);
            }
        }
    }
}
#endif