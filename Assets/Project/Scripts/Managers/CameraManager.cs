using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const int PRIORITY_CURRENT = 10;
    const int PRIORITY_DEFAULT = 1;

    public List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public void SetCameraToCurrent(CinemachineCamera p_camera)
    {
        if (!cameras.Contains(p_camera)) cameras.Add(p_camera);

        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] == p_camera) continue;

            cameras[i].Priority = PRIORITY_DEFAULT;
        }

        p_camera.Priority = PRIORITY_CURRENT;

    }

}
