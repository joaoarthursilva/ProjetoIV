using UnityEngine;

public class RaycastableContainer : RaycastableObject
{
    public Ingredient ingredientToSet;

    public override void Interact()
    {
        base.Interact();

        PlayerInventory.Instance.SetCurrentInventory(ingredientToSet);
    }
}
