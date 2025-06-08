using UnityEngine;

public class PutIngredientBehavior : MonoBehaviour
{
    [Header("Knife")]
    public Vector3 knife0Position;
    public Vector3 knife1Position;
    public Transform knifeTransform;
    [Range(0, 1)] public float followLerpValue;

    public Transform ingredientsParent;
    public void StartInteraction(Ingredient p_ingredient)
    {
        Instantiate(p_ingredient.prefab, ingredientsParent);
    }

    public Vector3 targetPosition;
    public void UpdateKnifePosition(float p_position)
    {
        targetPosition = Vector3.Lerp(knife0Position, knife1Position, p_position);
    }

    private void FixedUpdate()
    {
        knifeTransform.localPosition = Vector3.Lerp(knifeTransform.localPosition, targetPosition, followLerpValue);
        
    }
}
