using ProjetoIV.Util;
using System.Collections.Generic;
using ProjetoIV.Audio;
using UnityEngine;
using UnityEngine.UI;

public class PutIngredientController : MinigameStep
{
    [Space] [NaughtyAttributes.ReadOnly] private float m_currentSliderValue;
    [Space] public PutIngredientBehavior behaviour;
    public GameObject knifeAndTablePrefab;
    public Transform knifeAndTableParent;
    public ObjectAnimationBehaviour objectAnimBehavior;
    [Space] public GameObject sliderGO;
    public Slider slider;
    [Space] public Ingredient debugIngreciente;

    [NaughtyAttributes.Button]
    public void DebugStart() => StartInteraction(debugIngreciente, () => Debug.Log("diva"));

    public override void StartInteraction(Ingredient p_ingredient, System.Action p_OnEnd)
    {
        OnEnd = p_OnEnd;

        sliderGO.SetActive(true);
        slider.value = 0f;
        behaviour = Instantiate(knifeAndTablePrefab, knifeAndTableParent).GetComponent<PutIngredientBehavior>();
        behaviour.OnEnd += END;
        behaviour.StartInteraction(p_ingredient);
        objectAnimBehavior = behaviour.GetComponent<ObjectAnimationBehaviour>();
    }

    public void UpdateSlider(float p_float)
    {
        m_currentSliderValue = p_float;

        UpdatePut();
    }

    void END()
    {
        sliderGO.SetActive(false);
        Invoke(nameof(WaitAndEnd), 0.3f);
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

    private bool can = true;

    public void UpdatePut()
    {
        if (can && Mathf.Approximately(m_currentSliderValue, 1f))
        {Debug.Log("bumda 25");
            can = false;
            AudioManager.Instance.Play(AudioID.DROP_IN_WATER, behaviour.transform.position);
        }

        behaviour.UpdateKnifePosition(m_currentSliderValue);
    }
}