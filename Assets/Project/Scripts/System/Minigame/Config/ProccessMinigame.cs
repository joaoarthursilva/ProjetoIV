using UnityEngine;

[CreateAssetMenu(fileName = "CFG_Minigame_Proccess_", menuName = "Scriptable Objects/Minigame/Proccess/default")]
public class ProccessMinigame : Minigame
{
    [SerializeField] private Ingredient m_initialIngredient;
    public override Ingredient InitialIngredient() => m_initialIngredient;


    [SerializeField] private Ingredient m_finalIngredient;
    public override Ingredient FinalIngredient() => m_finalIngredient;

    public int quantityOfInteractions;
}
