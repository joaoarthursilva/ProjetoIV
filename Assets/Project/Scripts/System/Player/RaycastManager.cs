using System;
using System.Collections;
using System.Collections.Generic;
using ProjetoIV.RatInput;
using ProjetoIV.Util;
using UnityEngine;

public class RaycastManager : Singleton<RaycastManager>
{
    RaycastableObject[] m_raycastableObjects;
    Dictionary<int, RaycastableObject> m_raycastableObjectsDictionary;

    public IEnumerator Start()
    {
        yield return null;

        StartCoroutine(iStart());
       

    }

    IEnumerator iStart() 
    {
        yield return new WaitForSeconds(.5f);
        m_raycastableObjects = FindObjectsByType<RaycastableObject>(FindObjectsSortMode.None);

        m_raycastableObjectsDictionary = new();
        for (int i = 0; i < m_raycastableObjects.Length; i++)
        {
            m_raycastableObjectsDictionary.Add(m_raycastableObjects[i].Collider.GetInstanceID(), m_raycastableObjects[i]);
        }
    }

    public RaycastableObject GetRaycastableObject(int p_instanceID)
    {
        if (m_raycastableObjectsDictionary == null) return null;
        if (!m_raycastableObjectsDictionary.ContainsKey(p_instanceID)) return null;

        return m_raycastableObjectsDictionary[p_instanceID];
    }

    public void SetCurrentRaycastableObjectHover(RaycastableObject p_raycastObj)
    {
        if (m_raycastableObjects == null || RatInput.Instance.CurrentMap == Map.DIALOG) return;

        for (int i = 0; i < m_raycastableObjects.Length; i++)
        {
            m_raycastableObjects[i].SetHoverBehavior(p_raycastObj == m_raycastableObjects[i]);
        }

        if (p_raycastObj == null) RatInput.Instance.ShowUIElement(InputID.NONE);
        else RatInput.Instance.ShowUIElement(InputID.KITCHEN_INTERACT);
    }


}