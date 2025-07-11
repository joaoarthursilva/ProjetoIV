using System;
using ProjetoIV.Audio;
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
        if (Mathf.Abs(knifeTransform.localPosition.x - knife1Position.x) < 0.001f  
            && Mathf.Abs(knifeTransform.localPosition.y - knife1Position.y) < 0.001f)
        {
            OnEnd?.Invoke();
            OnEnd = null;
        }
    }
    public Action OnEnd;

}
