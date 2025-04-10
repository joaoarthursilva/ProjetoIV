using UnityEngine;

[CreateAssetMenu(fileName = "CFG_Minigame_Proccess_", menuName = "Scriptable Objects/Minigame/Proccess/default")]
public class ProccessMinigame : Minigame
{

    [SerializeField] private Ingredient m_finalIngredient;
    public Ingredient FinalIngredient => m_finalIngredient;

    public int quantityOfInteractions;
}
