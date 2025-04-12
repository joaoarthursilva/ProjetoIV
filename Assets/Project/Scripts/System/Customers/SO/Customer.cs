using UnityEngine;
using RatSpeak;

[CreateAssetMenu(fileName = "CFG_Customer_0", menuName = "Scriptable Objects/Customer")]
public class Customer : ScriptableObject
{
    public Character character;
    public Ingredient ingredient;

}
