using UnityEngine;

public interface IMinigameInteraction : IMinigameInputs
{
    public Unity.Cinemachine.CinemachineCamera MinigameCamera { get; }

    public void IOnStartInteraction();
    public void ICheckEndInteraction();
    public void IOnEndInteraction();
}
