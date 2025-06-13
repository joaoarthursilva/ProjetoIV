using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PastaMinigame", menuName = "Scriptable Objects/Minigame/Pasta")]
public class PastaMinigame : Minigame
{
    [SerializeField] private List<Ingredient> m_recipes;
    [SerializeField] private Ingredient m_finalIngredient;
    public override bool IsPartOfTheRecipe(Ingredient p_recipe)
    {
        return m_recipes.Contains(p_recipe);
    }

    public override Ingredient FinalIngredient() => m_finalIngredient;
}
