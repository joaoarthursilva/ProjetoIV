using ProjetoIV.Util;
using System;
using System.Collections;
using UnityEngine;

public class SaltPepperStep : MinigameStep
{
    public Transform parent;
    [NaughtyAttributes.ReadOnly] public ObjectAnimationBehaviour objectAnimBehavior;
    [NaughtyAttributes.ReadOnly] public GameObject animatedGameObject;
    public override void StartInteraction(Ingredient p_ingredient, Action p_onEnd)
    {
        animatedGameObject = Instantiate(p_ingredient.prefab, parent);
        objectAnimBehavior = animatedGameObject.GetComponent<ObjectAnimationBehaviour>();

        OnEnd = p_onEnd;

        StartCoroutine(SaltAnimation());
    }

    private IEnumerator SaltAnimation()
    {
        yield return objectAnimBehavior.PlayAnimations(UIAnimationType.ENTRY);
        yield return objectAnimBehavior.PlayAnimations(UIAnimationType.LEAVE);

        OnEnd.Invoke();
        Destroy(objectAnimBehavior.gameObject);
    }
}
