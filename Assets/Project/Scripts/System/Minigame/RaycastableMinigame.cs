using UnityEngine;
using UnityEngine.Events;

public class RaycastableMinigame : RaycastableObject
{
    public UnityEvent<RaycastableMinigame> OnInteractMinigame;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("aaaaaaaa");
        OnInteractMinigame?.Invoke(this);
    }
}
