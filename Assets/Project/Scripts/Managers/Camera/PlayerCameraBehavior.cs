using System;
using UnityEngine;

public class PlayerCameraBehavior : MonoBehaviour
{
    public float maxDistance = 5f;
    public RaycastableObject currentRaycastableObject;
    private RaycastableObject l_tempRaycastable;
    public void Update()
    {
        var rayCastInfo = Physics.RaycastAll(transform.position, Vector3.forward, maxDistance);

        for (int i = 0; i < rayCastInfo.Length; i++)
        {
            l_tempRaycastable = RaycastManager.Instance.GetRaycastableObject(rayCastInfo[i].transform.tag);
        }
        
        if(l_tempRaycastable != null) currentRaycastableObject = l_tempRaycastable;
    }
}