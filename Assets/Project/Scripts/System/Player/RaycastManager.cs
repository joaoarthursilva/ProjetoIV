using System;
using System.Collections.Generic;
using ProjetoIV.Util;
using UnityEngine;

public class RaycastManager : Singleton<RaycastManager>
{
    RaycastableObject[] m_raycastableObjects;
    Dictionary<int, RaycastableObject> m_raycastableObjectsDictionary;

    public void Start()
    {
        m_raycastableObjects = FindObjectsByType<RaycastableObject>(FindObjectsSortMode.None);

        m_raycastableObjectsDictionary = new();
        for (int i = 0; i < m_raycastableObjects.Length; i++)
            m_raycastableObjectsDictionary.Add(m_raycastableObjects[i].Collider.GetInstanceID(), m_raycastableObjects[i]);

    }

    public RaycastableObject GetRaycastableObject(int p_instanceID)
    {
        if (!m_raycastableObjectsDictionary.ContainsKey(p_instanceID)) return null;

        return m_raycastableObjectsDictionary[p_instanceID];
    }

    public void SetCurrentRaycastableObjectHover(RaycastableObject p_raycastObj)
    {
        for (int i = 0; i < m_raycastableObjects.Length; i++)
        {
            m_raycastableObjects[i].SetHoverBehavior(p_raycastObj == m_raycastableObjects[i]);
        }
    }
}