using ProjetoIV.RatInput;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigameInteraction : IMinigameInputs, ICameraStation
{
    public Map Map { get; }
    public InputID[] InputsToShow { get; }
    public List<RaycastableMinigame> RaycastableMinigame { get; }

    public bool EmbraceMinigame(Minigame o_minigame);
    public void IOnStartInteraction(Minigame p_minigame, System.Action p_actionOnEnd);
    public void ICheckEndInteraction();
    public void IOnEndInteraction();
}
