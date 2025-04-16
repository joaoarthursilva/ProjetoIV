using System.Collections.Generic;
using UnityEngine;

public interface IMinigameInteraction : IMinigameInputs
{
    public List<RaycastableMinigame> RaycastableMinigame { get; }
    public Unity.Cinemachine.CinemachineCamera MinigameCamera { get; }

    public bool EmbraceMinigame(Ingredient p_minigame, out Minigame o_minigame);
    public void IOnStartInteraction(Minigame p_minigame, System.Action p_actionOnEnd);
    public void ICheckEndInteraction();
    public void IOnEndInteraction();
}
