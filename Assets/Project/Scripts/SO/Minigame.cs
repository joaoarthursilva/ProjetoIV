using UnityEngine;

public class Minigame : ScriptableObject
{
   [TextArea(2,5)] public string Instruction;
    public bool callInstructionAuto = true;

    public virtual Ingredient InitialIngredient() { return null; }
    public virtual Ingredient FinalIngredient() { return null; }
    public virtual bool IsPartOfTheRecipe(Ingredient p_recipe) { return false; }
    public bool callServePlate;

    //public Audio startAudio;
    //public Audio endAudio;
}
