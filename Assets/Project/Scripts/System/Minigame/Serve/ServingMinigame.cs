using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CFG_Minigame_Serve_", menuName = "Scriptable Objects/Minigame/Serving")]

public class ServingMinigame : Minigame
{
    public GameObject initialPlatePrefab;

    public Ingredient initialIngredient;
    public Ingredient finalIngredient;
    public List<Ingredient> ingredientsToServe;
    public override Ingredient InitialIngredient()
    {
        return initialIngredient;
    }

    public override Ingredient FinalIngredient()
    {
        return finalIngredient;
    }

}
