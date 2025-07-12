using System.Collections;
using ProjetoIV.RatInput;
using ProjetoIV.Util;
using RatSpeak;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_horizontalLookSensitivity = .5f;
    [SerializeField] private float m_verticalLookSensitivity = .5f;
    [SerializeField] private Rigidbody m_rb;

    private void OnEnable()
    {
        // m_moveInput = RatInput.Instance.GetInput(InputID.KITCHEN_MOVE);
        // SubscribeToActions(true);
        CursorBehavior.Set(false, CursorLockMode.Locked);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        CursorBehavior.Set(true, CursorLockMode.None);
    }

    private WaitForFixedUpdate m_waitForFixedUpdate = new();

    Vector3 l_tempMovement = new();

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleLook();
    }
    private void HandleMovement()
    {
        if (DialogManager.Instance != null && DialogManager.Instance.InDialog) return;
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
        if (DialogManager.Instance != null && DialogManager.Instance.InDialog) return;
        transform.Rotate(0, (RatInput.Instance.LookInput.x + currentInaEffect) * m_horizontalLookSensitivity * Time.deltaTime, 0);
        l_verticalRotation -= (RatInput.Instance.LookInput.y + currentInaEffect) * m_verticalLookSensitivity * Time.deltaTime;
        l_verticalRotation = Mathf.Clamp(l_verticalRotation, m_angleLimitDown, m_angleLimitUp);
        m_firstPersonCamera.localRotation = Quaternion.Euler(l_verticalRotation, 0f, 0f);
    }

    [Header("Inas effect")]
    [SerializeField] private float maxInaEffect;
    [SerializeField] private AnimationCurve inaEffectAnimCurve;
    [SerializeField] private float inaTimeEffect;
    [SerializeField, NaughtyAttributes.ReadOnly] private float currentInaEffect;

    private IEnumerator InaEffect()
    {
        float l_time = 0f;

        while (l_time < inaTimeEffect)
        {
            currentInaEffect = Random.Range(0f, 1f);
            currentInaEffect *= inaEffectAnimCurve.Evaluate(l_time / inaTimeEffect);
            l_time += Time.deltaTime;
            yield return null;
        }
    }
}