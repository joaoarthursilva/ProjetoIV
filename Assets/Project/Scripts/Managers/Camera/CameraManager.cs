using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const int PRIORITY_CURRENT = 10;
    const int PRIORITY_OVERRIDE = 15;
    const int PRIORITY_DEFAULT = 0;

    public List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public CinemachineBrainEvents m_events;
    public CinemachineCamera playerCamera;
    public CinemachineCamera overrideCamera;

    private void Start()
    {
        var cams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        for (int i = 0; i < cams.Length; i++) cameras.Add(cams[i]);

        MinigamesManager.OnSetMinigamecamera += SetCameraToCurrent;
        MinigamesManager.OnSetMinigamecameraOverride += SetOverrideCamera;
    }

    public void SetCameraToCurrent(CinemachineCamera p_camera, System.Action p_onEndTransition)
    {
        if (p_camera == null) p_camera = playerCamera;

        if (!cameras.Contains(p_camera)) cameras.Add(p_camera);

        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] == p_camera) continue;

            cameras[i].Priority = PRIORITY_DEFAULT;
        }

        p_camera.Priority = PRIORITY_CURRENT;
        m_onEndBlend = p_onEndTransition;
    }

    public void SetOverrideCamera(CinemachineCamera p_camera, System.Action p_onEndTransition)
    {
        if (overrideCamera == p_camera)
        {
            p_onEndTransition?.Invoke();
            return;
        }

        if (p_camera == null)
        {
            overrideCamera.Priority = PRIORITY_DEFAULT;
            m_onEndBlend = p_onEndTransition;
            overrideCamera = null;
            return;
        }

        overrideCamera = p_camera;
        m_onEndBlend = p_onEndTransition;
        overrideCamera.Priority = PRIORITY_OVERRIDE;
    }

    System.Action m_onEndBlend;
    public void EndBlend(ICinemachineMixer p_mixer, ICinemachineCamera p_camera)
    {
        m_onEndBlend?.Invoke();
        m_onEndBlend = null;
    }
}
