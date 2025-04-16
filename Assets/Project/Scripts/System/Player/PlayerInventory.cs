using ProjetoIV.Util;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    public Ingredient currentIngredient;

    public void SetCurrentInventory(Ingredient p_ingredient)
    {
        currentIngredient = p_ingredient;
    }

}
