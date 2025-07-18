using ProjetoIV.RatInput;
using System;
using UnityEngine;

public class PlayerCameraBehavior : MonoBehaviour
{
    public float maxDistance = 5f;
    public RaycastableObject currentRaycastableObject;
    private RaycastableObject l_tempRaycastable;

    [Space]
    public ProjetoIV.RatInput.Input interact;

    private void Start()
    {
        interact = ProjetoIV.RatInput.RatInput.Instance.GetInput(ProjetoIV.RatInput.InputID.KITCHEN_INTERACT);
        interact.OnInputCanceled += Interact;
    }

    private void OnDestroy()
    {
        interact.OnInputCanceled -= Interact;
    }

    RaycastHit[] l_hits;
    public void Update()
    {
        if (RatInput.Instance.CurrentMap is not Map.KITCHEN) return;

        l_hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);

        l_tempRaycastable = null;
        for (int i = 0; i < l_hits.Length; i++)
        {
            l_tempRaycastable = RaycastManager.Instance.GetRaycastableObject(l_hits[i].collider.GetInstanceID());
            if (l_tempRaycastable != null) break;
        }

        currentRaycastableObject = l_tempRaycastable;
        RaycastManager.Instance.SetCurrentRaycastableObjectHover(currentRaycastableObject);
    }

    public void Interact()
    {
        if (currentRaycastableObject == null) return;

        currentRaycastableObject.Interact();
    }
}