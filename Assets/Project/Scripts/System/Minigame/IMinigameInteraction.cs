using System.Collections.Generic;
using UnityEngine;

public interface IMinigameInteraction : IMinigameInputs, ICameraStation
{
    public List<RaycastableMinigame> RaycastableMinigame { get; }

    public bool EmbraceMinigame(Ingredient p_minigame, out Minigame o_minigame);
    public void IOnStartInteraction(Minigame p_minigame, System.Action p_actionOnEnd);
    public void ICheckEndInteraction();
    public void IOnEndInteraction();
}
