using System;
using System.Collections.Generic;
using ProjetoIV.Util;
using UnityEngine;

public class RaycastManager : Singleton<RaycastManager>
{
    RaycastableObject[] m_raycastableObjects;
    Dictionary<string, RaycastableObject> m_raycastableObjectsDictionary;

    public void Start()
    {
        m_raycastableObjects = GameObject.FindObjectsByType<RaycastableObject>(FindObjectsSortMode.None);
        for (int i = 0; i < m_raycastableObjects.Length; i++)
        {
            m_raycastableObjectsDictionary.Add(m_raycastableObjects[i].tag, m_raycastableObjects[i]);
        }
    }

    public RaycastableObject GetRaycastableObject(string tag)
    {
        if (!m_raycastableObjectsDictionary.ContainsKey(tag)) return null;

        return m_raycastableObjectsDictionary[tag];
    }
}