using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PastaMinigame", menuName = "Scriptable Objects/Minigame/Pasta")]
public class PastaMinigame : Minigame
{
    [SerializeField] private List<Ingredient> m_recipes;
    public override bool IsPartOfTheRecipe(Ingredient p_recipe)
    {
        return m_recipes.Contains(p_recipe);
    }
}
