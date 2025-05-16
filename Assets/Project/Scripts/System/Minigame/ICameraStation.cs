using UnityEngine;

public interface ICameraStation
{
    public Unity.Cinemachine.CinemachineCamera Camera { get; }
    public System.Action<Unity.Cinemachine.CinemachineCamera, System.Action> OnFocusCamera { get; set; }
}
