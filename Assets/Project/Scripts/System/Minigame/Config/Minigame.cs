using UnityEngine;

public class Minigame : ScriptableObject
{
    public virtual Ingredient InitialIngredient() { return null; }
    public virtual Ingredient FinalIngredient() { return null; }
    public virtual bool IsPartOfTheRecipe(Ingredient p_recipe) { return false; }
    //public Audio startAudio;
    //public Audio endAudio;
}
