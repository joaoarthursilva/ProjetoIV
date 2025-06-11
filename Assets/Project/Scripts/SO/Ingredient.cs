using UnityEngine;

[CreateAssetMenu(fileName = "CFG_Ingredient_", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public GameObject prefab;
    public string title;
    public Sprite sprite;
}
