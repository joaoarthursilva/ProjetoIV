using System;
using ProjetoIV.Util;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    public Action<Ingredient> OnChangeInventory;
    public Ingredient currentIngredient;

    Ingredient l_inventory;
    void Update()
    {
        if (currentIngredient != l_inventory)
        {
            OnChangeInventory?.Invoke(currentIngredient);
            l_inventory = currentIngredient;
        }
    }

    public void SetCurrentInventory(Ingredient p_ingredient)
    {
        currentIngredient = p_ingredient;
        OnChangeInventory?.Invoke(currentIngredient);
    }

}
