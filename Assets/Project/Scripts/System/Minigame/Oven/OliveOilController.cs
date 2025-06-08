using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OliveOilController : MonoBehaviour
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
    Action onEnd;
    public void StartInteraction(Ingredient p_ingredient, Action p_onEndInteraction)
    {
        uiParent.SetActive(true);
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

        StartCoroutine(SliderCounter());
        onEnd = p_onEndInteraction;
    }

    float m_sliderValue = 0;
    public void OnChangeSliderValue(float p_float)
    {
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

        Debug.Log("bahh");
    }
}
