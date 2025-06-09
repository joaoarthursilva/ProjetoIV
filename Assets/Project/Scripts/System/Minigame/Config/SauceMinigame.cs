using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SauceMinigame", menuName = "Scriptable Objects/Minigame/Sauce")]
public class SauceMinigame : Minigame
{
    public List<Ingredient> Ingredients;
    [SerializeField] private Ingredient firstIngredient;
    [SerializeField] private Ingredient result;
    public override Ingredient InitialIngredient()
    {
        return firstIngredient;
    }

    public override Ingredient FinalIngredient() { return result; }

    public override bool IsPartOfTheRecipe(Ingredient p_recipe)
    {
        return Ingredients.Contains(p_recipe);
    }
}
