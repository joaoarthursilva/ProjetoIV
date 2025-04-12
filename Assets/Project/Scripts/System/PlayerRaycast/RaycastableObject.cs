using System;
using UnityEngine;

public class RaycastableObject : MonoBehaviour
{
    private Collider m_collider;
    public Collider Collider { get; }

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }
}
