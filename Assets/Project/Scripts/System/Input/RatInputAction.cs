using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjetoIV.RatInput
{
    public class RatInputAction : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_actionRef;
        private InputAction m_action;

        [field: SerializeField] public InputID InputID { get; private set; }
        [field: SerializeField, ReadOnly] public Input Input { get; private set; }

        private void Awake()
        {
            Input = new Input();
            if (m_actionRef == null)
            {
                Debug.LogError("InputActionReference is not assigned.");
                return;
            }

            m_action = m_actionRef.ToInputAction();
        }

        private void OnEnable()
        {
            StartCoroutine(IEUpdate());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator IEUpdate()
        {
            while (true)
            {
                Input.PressedThisFrame = m_action.WasPressedThisFrame();
                Input.ReleasedThisFrame = m_action.WasReleasedThisFrame();
                yield return null;
            }
        }

        public void OnInput(InputAction.CallbackContext p_context)
        {
            if (p_context.started)
            {
                Input.OnInputStarted?.Invoke();
            }

            if (p_context.performed)
            {
                Input.Pressed = true;

                Input.OnInputPerformed?.Invoke();
            }

            if (p_context.canceled)
            {
                Input.Pressed = false;
                Input.OnInputCanceled?.Invoke();
            }
        }
    }

    [Serializable]
    public class Input
    {
        [field: SerializeField, ReadOnly] public bool Pressed;
        [field: SerializeField, ReadOnly] public bool PressedThisFrame;
        [field: SerializeField, ReadOnly] public bool ReleasedThisFrame;
        public Action OnInputStarted;
        public Action OnInputPerformed;
        public Action OnInputCanceled;
    }
}