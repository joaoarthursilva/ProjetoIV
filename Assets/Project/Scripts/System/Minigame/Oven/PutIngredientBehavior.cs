using UnityEngine;

public class PutIngredientBehavior : MonoBehaviour
{
    [Header("Knife")]
    public Vector3 knife0Position;
    public Vector3 knife1Position;
    public Transform knifeTransform;

    public Transform ingredientsParent;

    public void StartInteraction()
    {

    }

    public void UpdateKnifePosition(float p_position)
    {
        Vector3 nextPos = Vector3.Lerp(knife0Position, knife1Position, p_position);
        knifeTransform.localPosition = Vector3.Lerp(knifeTransform.localPosition, nextPos, .66f);
    }
}
