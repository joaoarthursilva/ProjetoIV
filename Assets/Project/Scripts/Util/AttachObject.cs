using UnityEngine;

public class AttachObject : MonoBehaviour
{
    [NaughtyAttributes.ReadOnly, SerializeField] private Transform m_objectTransform;
    [NaughtyAttributes.ReadOnly, SerializeField] private Vector3 m_positionOffset; 

    public void StartFollow(Transform p_transform)
    {
        m_positionOffset = transform.position - p_transform.position;
        m_objectTransform = p_transform;
    }

    private void Update()
    {
        if(m_objectTransform != null)
        {
            transform.position = m_objectTransform.position + m_positionOffset;
        }
    }
}
