using UnityEngine;

public class PutIngredientController : MonoBehaviour
{
    public float currentSliderValue;
    
    public PutIngredientBehavior behaviour;
    public GameObject knifeAndTablePrefab;
    public Transform knifeAndTableParent;
    public System.Action OnEnd;

    [Space]
    public GameObject sliderGO;
    [Space]
    public Ingredient debugIngreciente;
    [NaughtyAttributes.Button] public void DebugStart() => StartInteraction(debugIngreciente, () => Debug.Log("diva"));
    public void StartInteraction(Ingredient p_ingredient, System.Action p_OnEnd)
    {
        OnEnd = p_OnEnd;

        sliderGO.SetActive(true);
        behaviour = Instantiate(knifeAndTablePrefab, knifeAndTableParent).GetComponent<PutIngredientBehavior>();
        behaviour.StartInteraction(p_ingredient);
    }

    public void UpdateSlider(float p_float)
    {
        currentSliderValue = p_float;

        UpdatePut();
        
        if(Mathf.Approximately(currentSliderValue, 1f))
        {
            OnEnd?.Invoke();
        }
    }

    public void UpdatePut()
    {
        behaviour.UpdateKnifePosition(currentSliderValue);
    }
}
