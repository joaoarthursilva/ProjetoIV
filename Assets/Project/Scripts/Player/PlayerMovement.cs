using System.Collections;
using ProjetoIV.RatInput;
using UnityEngine;
using UnityEngine.InputSystem;

// using UnityEngine.InputSystem;
// using Input = ProjetoIV.RatInput.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_horizontalLookSensitivity = .5f;
    [SerializeField] private float m_verticalLookSensitivity = .5f;
    [SerializeField] private Rigidbody m_rb;

    private void OnEnable()
    {
        // m_moveInput = RatInput.Instance.GetInput(InputID.KITCHEN_MOVE);
        // SubscribeToActions(true);
        StartCoroutine(UpdateCoroutine());
        StartCoroutine(FixedUpdateCoroutine());
        SetCursor(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        SetCursor(true);
    }

    private void SetCursor(bool p_state)
    {
        if (p_state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private WaitForFixedUpdate m_waitForFixedUpdate = new();

    Vector3 l_tempMovement = new();

    private IEnumerator FixedUpdateCoroutine()
    {
        while (true)
        {
            HandleMovement();
            yield return m_waitForFixedUpdate;
        }
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            HandleLook();
            yield return null;
        }
    }

    private void HandleMovement()
    {
        l_tempMovement.Set(RatInput.Instance.Movement.x, 0, RatInput.Instance.Movement.y);
        l_tempMovement.Normalize();
        l_tempMovement *= m_moveSpeed * 50 * Time.fixedDeltaTime;
        l_tempMovement = transform.TransformDirection(l_tempMovement);
        m_rb.linearVelocity = l_tempMovement;
    }

    Vector3 l_look;
    float l_verticalRotation = 0f;
    [SerializeField] private Transform m_firstPersonCamera;
    [SerializeField] private float m_angleLimitUp = 90f;
    [SerializeField] private float m_angleLimitDown = -90f;

    private void HandleLook()
    {
        transform.Rotate(0, RatInput.Instance.LookInput.x * m_horizontalLookSensitivity * Time.deltaTime, 0);
        l_verticalRotation -= RatInput.Instance.LookInput.y * m_verticalLookSensitivity * Time.deltaTime;
        l_verticalRotation = Mathf.Clamp(l_verticalRotation, m_angleLimitDown, m_angleLimitUp);
        m_firstPersonCamera.localRotation = Quaternion.Euler(l_verticalRotation, 0f, 0f);
    }
}