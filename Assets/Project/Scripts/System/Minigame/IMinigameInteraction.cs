using System.Collections.Generic;
using UnityEngine;

public interface IMinigameInteraction : IMinigameInputs
{
    public List<RaycastableMinigame> RaycastableMinigame { get; }
    public Unity.Cinemachine.CinemachineCamera MinigameCamera { get; }

    public void IOnStartInteraction();
    public void ICheckEndInteraction();
    public void IOnEndInteraction();
}
