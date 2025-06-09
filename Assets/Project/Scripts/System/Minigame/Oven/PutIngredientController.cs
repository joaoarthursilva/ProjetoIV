using ProjetoIV.Util;
using System.Collections.Generic;
using UnityEngine;

public class PutIngredientController : MinigameStep
{
    [Space]
    [NaughtyAttributes.ReadOnly]private float m_currentSliderValue;
    [Space]
    public PutIngredientBehavior behaviour;
    public GameObject knifeAndTablePrefab;
    public Transform knifeAndTableParent;
    public ObjectAnimationBehaviour objectAnimBehavior;
    [Space]
    public GameObject sliderGO;
    [Space]
    public Ingredient debugIngreciente;
    [NaughtyAttributes.Button] public void DebugStart() => StartInteraction(debugIngreciente, () => Debug.Log("diva"));

    public override void StartInteraction(Ingredient p_ingredient, System.Action p_OnEnd)
    {
        OnEnd = p_OnEnd;

        sliderGO.SetActive(true);
        behaviour = Instantiate(knifeAndTablePrefab, knifeAndTableParent).GetComponent<PutIngredientBehavior>();
        behaviour.StartInteraction(p_ingredient);
        objectAnimBehavior = behaviour.GetComponent<ObjectAnimationBehaviour>();
    }

    public void UpdateSlider(float p_float)
    {
        m_currentSliderValue = p_float;

        UpdatePut();

        if (Mathf.Approximately(m_currentSliderValue, 1f))
        {
            sliderGO.SetActive(false);
            Invoke(nameof(WaitAndEnd), 0.3f);
        }
    }

    void WaitAndEnd()
    {
        objectAnimBehavior.PlayLeaveAnimations();
        Invoke(nameof(End), 0.5f);
    }

    void End()
    {
        Destroy(behaviour.gameObject);
        OnEnd.Invoke();
    }

    public void UpdatePut()
    {
        behaviour.UpdateKnifePosition(m_currentSliderValue);
    }
}
