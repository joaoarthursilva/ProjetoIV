using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetoIV.Util;
public class OliveOilController : MinigameStep
{
    [Serializable]
    private struct PourData
    {
        public Ingredient ingredient;
        [Space]
        public Vector3 initialOliveOilPosition;
        public Vector3 finalOliveOilPosition;
        public AnimationCurve postionCurve;
        [Space]
        public Vector3 initialOliveOilRotation;
        public Vector3 finalOliveOilRotation;
        public AnimationCurve rotationCurve;
        [Space]
        [Range(0, 1)] public float pouringThreshold;
        public float maxOil;
    }

    [Header("World")]
    public Transform parent;
    public Transform dispenserTransform;
    public PourStream streamController;
    public ObjectAnimationBehaviour objectAnimationBehaviour;

    [SerializeField] PourData[] pourData;
    [SerializeField] private PourData currentPourData;

    [Header("UI")]
    public GameObject uiParent;
    public Slider slider;

    public Ingredient debugIngredient;
    [NaughtyAttributes.Button]
    public void DebugStartInteraction()
    {
        StartInteraction(debugIngredient, null);
    }

    public override void StartInteraction(Ingredient p_ingredient, Action p_onEndInteraction)
    {
        for (int i = 0; i < pourData.Length; i++)
        {
            if (pourData[i].ingredient == p_ingredient)
            {
                currentPourData = pourData[i];
            }
        }

        dispenserTransform = Instantiate(currentPourData.ingredient.prefab,
                                        currentPourData.initialOliveOilPosition,
                                        Quaternion.Euler(currentPourData.initialOliveOilRotation), parent).GetComponent<Transform>();
        dispenserTransform.localPosition = currentPourData.initialOliveOilPosition;
        streamController = dispenserTransform.GetComponentInChildren<PourStream>();
        objectAnimationBehaviour = dispenserTransform.GetComponent<ObjectAnimationBehaviour>();
        OnEnd = p_onEndInteraction;
        Invoke(nameof(SetSlider), 0.5f);
    }

    void SetSlider()
    {
        uiParent.SetActive(true);
        StartCoroutine(SliderCounter());
    }

    float m_sliderValue = 0;
    public void OnChangeSliderValue(float p_float)
    {
        if (pourCount >= currentPourData.maxOil) return;
        if (Mathf.Approximately(p_float, 1f)) uiParent.SetActive(false);

        dispenserTransform.localPosition = Vector3.Lerp(currentPourData.initialOliveOilPosition, currentPourData.finalOliveOilPosition, currentPourData.postionCurve.Evaluate(p_float));
        float xRot = Mathf.LerpAngle(currentPourData.initialOliveOilRotation.x, currentPourData.finalOliveOilRotation.x, currentPourData.rotationCurve.Evaluate(p_float));
        Vector3 rot = new(xRot, 0f, 0f);
        dispenserTransform.rotation = Quaternion.Euler(xRot, 0, 0);
    }

    public float pourCount = 0f;
    IEnumerator SliderCounter()
    {
        pourCount = 0f;
        bool l_isPouring = false;
        while (true)
        {
            if (slider.value > currentPourData.pouringThreshold)
            {
                if (!l_isPouring && streamController != null) streamController.StartPour();
                l_isPouring = true;
                pourCount += Time.deltaTime;
            }
            else if (l_isPouring && streamController != null)
            {
                streamController.EndPour();
                l_isPouring = false;
            }

            if (pourCount >= currentPourData.maxOil) break;

            yield return null;
        }

        if (l_isPouring && streamController != null) streamController.EndPour();

        float l_tValue = 1f;
        float l_timeToEnd = 0.5f;
        float l_time = 0;
        while (l_time < l_timeToEnd)
        {
            l_time += Time.deltaTime;
            l_tValue = Mathf.Lerp(1, 0, l_time / l_timeToEnd);

            dispenserTransform.localPosition = Vector3.Lerp(currentPourData.initialOliveOilPosition, currentPourData.finalOliveOilPosition, currentPourData.postionCurve.Evaluate(l_tValue));
            float xRot = Mathf.LerpAngle(currentPourData.initialOliveOilRotation.x, currentPourData.finalOliveOilRotation.x, currentPourData.rotationCurve.Evaluate(l_tValue));
            Vector3 rot = new(xRot, 0f, 0f);
            dispenserTransform.rotation = Quaternion.Euler(xRot, 0, 0);
            yield return null;
        }

        yield return objectAnimationBehaviour.PlayAnimations(UIAnimationType.LEAVE);
        Destroy(dispenserTransform.gameObject);
        dispenserTransform = null;
        objectAnimationBehaviour = null;
        streamController = null;

        OnEnd?.Invoke();
    }
}
