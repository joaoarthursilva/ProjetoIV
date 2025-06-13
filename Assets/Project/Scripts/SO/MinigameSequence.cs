using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameSequence", menuName = "Scriptable Objects/MinigameSequence")]
public class MinigameSequence : ScriptableObject
{
    public List<Minigame> sequence;
    public Ingredient FinalIngredient;
}
