using UnityEngine;

public class PutIngredientController : MonoBehaviour
{
    public float currentSliderValue;
    
    public PutIngredientBehavior behaviour;
    public void UpdateSlider(float p_float)
    {
        float l_deltaFloat = p_float - currentSliderValue;
        currentSliderValue = p_float;

        if(l_deltaFloat < 0) l_deltaFloat = 0;
        UpdatePut(l_deltaFloat);
    }

    public void UpdatePut(float delta)
    {
        behaviour.UpdateKnifePosition(currentSliderValue);
    }
}
