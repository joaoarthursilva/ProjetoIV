using System.Collections.Generic;
using UnityEngine;

public class MinigameStep : MonoBehaviour
{
    [SerializeField] protected List<Ingredient> m_ingredients;
    public virtual List<Ingredient> IngredientsAccepted() { return m_ingredients; }
    public virtual void StartInteraction(Ingredient p_ingredient, System.Action p_onEnd) { }
    protected System.Action OnEnd;
}
