using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastObjectEvent : UnityEvent<RaycastableObject> { }

public class RaycastableObject : MonoBehaviour
{
    private Collider m_collider;
    public Collider Collider { get => m_collider; }
    public UnityEvent<RaycastableObject> OnInteract;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    public void SetHoverBehavior(bool p_hoverOn)
    {

    }

    public virtual void Interact()
    {
        OnInteract?.Invoke(this);

    }
}
