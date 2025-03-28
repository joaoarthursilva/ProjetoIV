using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ProjetoIV.Util
{
    public class UIComponentButtonComplex : Selectable
    {
        protected enum ButtonPhase
        {
            NORMAL,
            PRESSED,
            HOVER,
            DISABLE,
            SELECTED,
            EXIT_HOVER
        }

        [System.Serializable]
        class ButtonPhaseClass
        {
            public ButtonPhase phase;
            public bool useUnscaledTime = false;

            [System.Serializable]
            class TextElement
            {
                [SerializeField] TextMeshProUGUI m_text;
                public bool shouldColorize;

                [ShowIf("shouldColorize")] [AllowNesting] [SerializeField]
                Color m_color;

                [ShowIf("shouldColorize")] [AllowNesting] [SerializeField]
                AnimationCurve m_colorAnimCurve;

                public bool shouldScale;

                [ShowIf("shouldScale")] [AllowNesting] [SerializeField]
                Vector3 m_scale;

                [ShowIf("shouldScale")] [AllowNesting] [SerializeField]
                AnimationCurve m_scaleAnimCurve;

                Color m_initalColor;
                Vector3 m_initialScale;

                public void SetInitialValues()
                {
                    m_initalColor = m_text.color;
                    m_initialScale = m_text.transform.localScale;
                }

                public void ChangeText(float p_lerp)
                {
                    if (shouldColorize)
                        m_text.color = Color.LerpUnclamped(m_initalColor, m_color, m_colorAnimCurve.Evaluate(p_lerp));
                    if (shouldScale)
                        m_text.transform.localScale =
                            Vector3.LerpUnclamped(m_initialScale, m_scale, m_scaleAnimCurve.Evaluate(p_lerp));
                }
            }

            [System.Serializable]
            class ImageElement
            {
                [SerializeField] Image m_image;
                public bool shouldColorize;

                [ShowIf("shouldColorize")] [AllowNesting] [SerializeField]
                Color m_color;

                [ShowIf("shouldColorize")] [AllowNesting] [SerializeField]
                AnimationCurve m_colorAnimCurve;

                public bool shouldChangeSprite;

                [ShowIf("shouldChangeSprite")] [AllowNesting] [SerializeField]
                Sprite m_sprite;

                public bool shouldScale;

                [ShowIf("shouldScale")] [AllowNesting] [SerializeField]
                Vector3 m_scale;

                [ShowIf("shouldScale")] [AllowNesting] [SerializeField]
                AnimationCurve m_scaleAnimCurve;

                public bool shouldFillAmount;

                [ShowIf("shouldFillAmount")] [AllowNesting] [SerializeField]
                float m_fillAmount;

                [ShowIf("shouldFillAmount")] [AllowNesting] [SerializeField]
                AnimationCurve m_fillAmountAnimCurve;

                float m_initialFillAmount;
                Color m_initalColor;
                Vector3 m_initialScale;

                public void SetInitialValues()
                {
                    m_initalColor = m_image.color;
                    m_initialScale = m_image.transform.localScale;
                    m_initialFillAmount = m_image.fillAmount;
                }

                public void ChangeImage(float p_lerp)
                {
                    if (shouldFillAmount)
                        m_image.fillAmount = Mathf.LerpUnclamped(m_initialFillAmount, m_fillAmount,
                            m_fillAmountAnimCurve.Evaluate(p_lerp));
                    if (shouldScale)
                        m_image.transform.localScale =
                            Vector3.LerpUnclamped(m_initialScale, m_scale, m_scaleAnimCurve.Evaluate(p_lerp));
                    if (shouldColorize)
                        m_image.color = Color.LerpUnclamped(m_initalColor, m_color, m_colorAnimCurve.Evaluate(p_lerp));
                    if (shouldChangeSprite
                        && m_image.sprite != m_sprite) m_image.sprite = m_sprite;
                }
            }

            [SerializeField] TextElement[] m_textElements;
            [SerializeField] ImageElement[] m_imageElements;

            [Header("Anim")]
            public float animDuration;

            [Header("Actions")]
            public UnityEvent<bool> OnEntryPhase;

            public UnityEvent<bool> OnExitPhase;
            public UnityEvent<bool> OnFinishPhase;

            public void SetValuesInstantly()
            {
                for (int i = 0; i < m_textElements.Length; i++) m_textElements[i].ChangeText(1);
                for (int i = 0; i < m_imageElements.Length; i++) m_imageElements[i].ChangeImage(1);

                OnEntryPhase?.Invoke(true);
            }

            public IEnumerator ChangeElements()
            {
                OnEntryPhase?.Invoke(true);

                for (int i = 0; i < m_imageElements.Length; i++) m_imageElements[i].SetInitialValues();
                for (int i = 0; i < m_textElements.Length; i++) m_textElements[i].SetInitialValues();

                for (float l_time = 0;
                     l_time < animDuration;
                     l_time += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime)
                {
                    for (int i = 0; i < m_textElements.Length; i++) m_textElements[i].ChangeText(l_time / animDuration);
                    for (int i = 0; i < m_imageElements.Length; i++)
                        m_imageElements[i].ChangeImage(l_time / animDuration);

                    yield return null;
                }

                for (int i = 0; i < m_textElements.Length; i++) m_textElements[i].ChangeText(1f);
                for (int i = 0; i < m_imageElements.Length; i++) m_imageElements[i].ChangeImage(1f);

                OnFinishPhase?.Invoke(true);
            }
        }

        [SerializeField] private bool m_playGeneralClickSFX = false;
        // [SerializeField] bool m_notifyInputManager = false;

        [Header("Click Down")]
        public Button.ButtonClickedEvent onClickDown;

        [Header("Click Up")]
        public Button.ButtonClickedEvent onClick;

        [SerializeField, ReadOnly] ButtonPhase m_currentPhase, m_hovePhase;
        [SerializeField] bool m_awaysChangeInstantly = false;
        [SerializeField] ButtonPhaseClass[] m_phaseCustom;

        [SerializeField] bool m_justEnter = false, m_justExit = false;
        [SerializeField] bool m_useRaycastToEnter;
        [SerializeField] float m_checkExitFrequency = 0.3f;

        [Header("SFX")]
        //[SerializeField] StudioEventEmitter m_hoverSFX;
        //[SerializeField] StudioEventEmitter m_pressedSFX;
        Coroutine m_checkExit;

        WaitForSecondsRealtime m_checkExitFrequencyWait;

        bool m_entered = false;

        Coroutine m_animCoroutine;

        protected override void Start()
        {
            m_checkExitFrequencyWait = new WaitForSecondsRealtime(m_checkExitFrequency);

            if (m_currentPhase != ButtonPhase.NORMAL
                && interactable == true) m_currentPhase = ButtonPhase.NORMAL;
            if (interactable == false
                && m_currentPhase != ButtonPhase.DISABLE) m_currentPhase = ButtonPhase.DISABLE;

            ChangePhase(m_currentPhase, true);
        }

        protected override void OnEnable()
        {
            if (m_currentPhase != ButtonPhase.NORMAL
                && interactable == true) m_currentPhase = ButtonPhase.NORMAL;
            if (interactable == false
                && m_currentPhase != ButtonPhase.DISABLE) m_currentPhase = ButtonPhase.DISABLE;

            ChangePhase(m_currentPhase, true);
        }

        protected override void OnDisable()
        {
            if (m_checkExit != null)
            {
                m_entered = false;
                StopCoroutine(m_checkExit);
            }

            if (m_animCoroutine != null) StopCoroutine(m_animCoroutine);
            ChangePhase(ButtonPhase.NORMAL, true);
            m_currentPhase = ButtonPhase.NORMAL;
        }

        List<RaycastResult> l_rayResults = new List<RaycastResult>();

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (!gameObject.activeInHierarchy) return;
            if (interactable == false) return;

            if (!m_justExit)
            {
                if (!m_useRaycastToEnter)
                {
                    if (m_currentPhase == ButtonPhase.NORMAL)
                    {
                        ChangePhase(ButtonPhase.HOVER, false, eventData);
                        m_hovePhase = ButtonPhase.HOVER;
                    }
                }
                else
                {
                    if (CheckRaycast(eventData))
                    {
                        if (m_currentPhase == ButtonPhase.NORMAL)
                        {
                            ChangePhase(ButtonPhase.HOVER, false, eventData);
                            m_hovePhase = ButtonPhase.HOVER;
                        }
                    }
                }
            }

            if (!m_justEnter)
            {
                m_entered = true;

                if (m_checkExit != null) StopCoroutine(m_checkExit);
                m_checkExit = StartCoroutine(CheckExit(eventData));
            }
        }

        public void StartCheckExit(PointerEventData eventData)
        {
            if (m_checkExit != null) StopCoroutine(m_checkExit);
            m_checkExit = StartCoroutine(CheckExit(eventData));
        }

        public IEnumerator CheckExit(PointerEventData eventData)
        {
            bool l_exit = false;
            while (!l_exit)
            {
                yield return m_checkExitFrequencyWait;

                if (!CheckRaycast(eventData))
                {
                    m_entered = false;

                    if (m_currentPhase == ButtonPhase.NORMAL)
                    {
                        ChangePhase(ButtonPhase.EXIT_HOVER);
                        m_hovePhase = ButtonPhase.EXIT_HOVER;
                    }

                    l_exit = true;
                }
            }

            m_checkExit = null;
        }

        bool CheckRaycast(PointerEventData p_eventData)
        {
            bool l_hited = false;

            l_rayResults.Clear();
            EventSystem.current.RaycastAll(p_eventData, l_rayResults);

            for (int i = 0; i < l_rayResults.Count; i++)
            {
                if (l_rayResults[i].gameObject == gameObject)
                {
                    l_hited = true;
                    break;
                }
            }

            return l_hited;
        }

        public void SetPhase(int p_phase)
        {
            if ((ButtonPhase) p_phase == ButtonPhase.NORMAL)
            {
                if (!interactable) return;

                if (m_currentPhase != ButtonPhase.NORMAL) ChangePhase(ButtonPhase.NORMAL);
                m_currentPhase = ButtonPhase.NORMAL;
            }
            else if ((ButtonPhase) p_phase == ButtonPhase.DISABLE)
            {
                if (interactable) return;

                ChangePhase(ButtonPhase.EXIT_HOVER);
                m_hovePhase = ButtonPhase.EXIT_HOVER;

                if (m_currentPhase != ButtonPhase.DISABLE) ChangePhase(ButtonPhase.DISABLE);
                m_currentPhase = ButtonPhase.DISABLE;
            }
            else
            {
                Debug.Log($"Difiniu a phase como {p_phase} sem passar eventdata");
                ChangePhase((ButtonPhase) p_phase);

                if ((ButtonPhase) p_phase != ButtonPhase.EXIT_HOVER && (ButtonPhase) p_phase != ButtonPhase.HOVER)
                    m_currentPhase = (ButtonPhase) p_phase;
                else m_hovePhase = (ButtonPhase) p_phase;
            }
        }

        public void ChangePhaseToCurrent()
        {
            if (m_animCoroutine != null) StopCoroutine(m_animCoroutine);

            if (m_currentPhase == ButtonPhase.NORMAL
                && m_hovePhase == ButtonPhase.HOVER) m_animCoroutine = StartCoroutine(RunAnim(ButtonPhase.HOVER));
            else m_animCoroutine = StartCoroutine(RunAnim(m_currentPhase));
        }

        protected void ChangePhase(ButtonPhase p_phase, bool p_instantly = false, PointerEventData p_eventData = null)
        {
            if (p_phase != ButtonPhase.EXIT_HOVER && p_phase != ButtonPhase.HOVER)
            {
                for (int i = 0; i < m_phaseCustom.Length; i++)
                {
                    if (m_phaseCustom[i].phase == m_currentPhase)
                    {
                        m_phaseCustom[i].OnExitPhase?.Invoke(false);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_phaseCustom.Length; i++)
                {
                    if (m_phaseCustom[i].phase == m_hovePhase)
                    {
                        m_phaseCustom[i].OnExitPhase?.Invoke(false);
                        break;
                    }
                }
            }

            //PlaySound(p_phase);

            for (int i = 0; i < m_phaseCustom.Length; i++)
            {
                if (m_phaseCustom[i].phase == p_phase)
                {
                    if (gameObject.activeInHierarchy)
                    {
                        if (p_instantly
                            || m_awaysChangeInstantly)
                        {
                            m_phaseCustom[GetPhaseID(p_phase)].SetValuesInstantly();
                        }
                        else
                        {
                            if (m_animCoroutine != null) StopCoroutine(m_animCoroutine);
                            m_animCoroutine = StartCoroutine(RunAnim(p_phase));
                        }
                    }

                    break;
                }
            }
        }

        /*
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!gameObject.activeInHierarchy) return;

            if (m_currentPhase != ButtonPhase.PRESSED) ChangePhase(ButtonPhase.PRESSED);
            m_currentPhase = ButtonPhase.PRESSED;
        }*/

        public override void Select()
        {
            base.Select();

            if (!gameObject.activeInHierarchy) return;

            if (m_currentPhase != ButtonPhase.SELECTED) ChangePhase(ButtonPhase.SELECTED);
            m_currentPhase = ButtonPhase.SELECTED;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (!gameObject.activeInHierarchy) return;

            if (interactable)
            {
                if (m_currentPhase != ButtonPhase.NORMAL) ChangePhase(ButtonPhase.NORMAL);
                m_currentPhase = ButtonPhase.NORMAL;
            }
            else
            {
                if (m_currentPhase != ButtonPhase.DISABLE) ChangePhase(ButtonPhase.DISABLE);
                m_currentPhase = ButtonPhase.DISABLE;
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (interactable
                && state == SelectionState.Pressed)
            {
                if (m_currentPhase != ButtonPhase.PRESSED) ChangePhase(ButtonPhase.PRESSED);
                m_currentPhase = ButtonPhase.PRESSED;
            }
            else if (interactable
                     && m_currentPhase != ButtonPhase.NORMAL)
            {
                if (HasPhase(ButtonPhase.PRESSED))
                {
                    if (m_currentPhase != ButtonPhase.NORMAL && m_currentPhase != ButtonPhase.PRESSED)
                        ChangePhase(ButtonPhase.NORMAL);
                }
                else
                {
                    if (m_currentPhase != ButtonPhase.NORMAL) ChangePhase(ButtonPhase.NORMAL);
                }

                m_currentPhase = ButtonPhase.NORMAL;
            }
            else if (!interactable
                     && m_currentPhase != ButtonPhase.DISABLE)
            {
                //ChangePhase(ButtonPhase.EXIT_HOVER);
                m_hovePhase = ButtonPhase.EXIT_HOVER;

                if (HasPhase(ButtonPhase.PRESSED))
                {
                    if (m_currentPhase != ButtonPhase.DISABLE && m_currentPhase != ButtonPhase.PRESSED)
                        ChangePhase(ButtonPhase.DISABLE);
                }
                else
                {
                    if (m_currentPhase != ButtonPhase.DISABLE) ChangePhase(ButtonPhase.DISABLE);
                }

                m_currentPhase = ButtonPhase.DISABLE;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //InputManager.Instance.SetClickedObject(gameObject);

            if (eventData.button is not PointerEventData.InputButton.Left) return;
            base.OnPointerDown(eventData);
            if (interactable == false) return;

            onClickDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (interactable == false) return;

            if (!eventData.dragging && eventData.button == 0)
            {
                onClick?.Invoke();
            }
        }

        int GetPhaseID(ButtonPhase state)
        {
            int l_id = 0;
            for (int i = 0; i < m_phaseCustom.Length; i++)
            {
                if (m_phaseCustom[i].phase == state)
                {
                    l_id = i;
                    break;
                }
            }

            return l_id;
        }

        bool HasPhase(ButtonPhase p_phase)
        {
            for (int i = 0; i < m_phaseCustom.Length; i++)
            {
                if (m_phaseCustom[i].phase == p_phase) return true;
            }

            return false;
        }


        IEnumerator RunAnim(ButtonPhase p_phase)
        {
            yield return m_phaseCustom[GetPhaseID(p_phase)].ChangeElements();
        }

        //void PlaySound(ButtonPhase p_phase)
        //{
        //    switch (p_phase)
        //    {
        //        case ButtonPhase.HOVER:
        //            if (m_hoverSFX != null) m_hoverSFX.Play();
        //            else if (GeneralSFX.Instance != null) GeneralSFX.Instance.PlayButtonHover();
        //            break;
        //        case ButtonPhase.PRESSED:
        //            if (m_pressedSFX != null) m_pressedSFX.Play();
        //            if (m_playGeneralClickSFX) GeneralSFX.Instance.PlayButtonSuccess();
        //            if (m_notifyInputManager) InputManager.Instance.SetClickedObject(gameObject);
        //            break;
        //    }
        //}
    }
}