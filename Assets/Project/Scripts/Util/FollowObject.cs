using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public class FollowObject : MonoBehaviour
{
    public Transform transformToFollow;
    [SerializeField] private Vector3 m_offset;
    public bool shouldSmooth;
    public bool canFollow = true;

    [ShowIf("shouldSmooth")]
    [SerializeField] private float m_smoothTime = 0.3f;

    private Vector3 m_velocity = Vector3.zero;
    private IEnumerator m_followObjectCoroutine;

    private void OnEnable()
    {
        m_followObjectCoroutine = FollowObjectCoroutine();
        StartCoroutine(m_followObjectCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(m_followObjectCoroutine);
        m_followObjectCoroutine = null;
    }

    Vector3 l_targetPosition;

    private IEnumerator FollowObjectCoroutine()
    {
        while (canFollow)
        {
            l_targetPosition = transformToFollow.position + m_offset;
            transform.position = shouldSmooth
                ? Vector3.SmoothDamp(transform.position, l_targetPosition, ref m_velocity, m_smoothTime)
                : l_targetPosition;

            yield return new WaitForEndOfFrame();
        }
    }

#if UNITY_EDITOR
    [Button] private void SetPositionToFollowedObject() => transform.position = transformToFollow.position + m_offset;
#endif
}