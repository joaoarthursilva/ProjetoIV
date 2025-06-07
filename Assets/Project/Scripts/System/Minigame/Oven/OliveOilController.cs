using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OliveOilController : MonoBehaviour
{
    [Header("World")]
    public Transform parent;
    public GameObject oilDispenserPrefab;
    public Transform dispenserTransform;
    [Space]
    public Vector3 initialOliveOilPosition;
    public Vector3 finalOliveOilPosition;
    public AnimationCurve postionCurve;
    [Space]
    public Vector3 finalOliveOilRotation;
    public AnimationCurve rotationCurve;
    public Vector3 initialOliveOilRotation;

    [Header("UI")]
    public GameObject uiParent;
    public Slider slider;

    [NaughtyAttributes.Button]
    public void DebugStartInteraction()
    {
        StartInteraction(null);
    }
    Action onEnd;
    public void StartInteraction(Action p_onEndInteraction)
    {
        uiParent.SetActive(true);

        dispenserTransform = Instantiate(oilDispenserPrefab, initialOliveOilPosition, Quaternion.Euler(initialOliveOilRotation), parent).GetComponent<Transform>();
        dispenserTransform.localPosition = initialOliveOilPosition;
        StartCoroutine(SliderGravity());
        onEnd = p_onEndInteraction;
    }

    float m_sliderValue = 0;
    public void OnChangeSliderValue(float p_float)
    {
        dispenserTransform.localPosition = Vector3.Lerp(initialOliveOilPosition, finalOliveOilPosition, postionCurve.Evaluate(p_float));
        float xRot = Mathf.LerpAngle(initialOliveOilRotation.x, finalOliveOilRotation.x, rotationCurve.Evaluate(p_float));
        Vector3 rot = new(xRot, 0f, 0f);
        dispenserTransform.rotation = Quaternion.Euler(xRot, 0, 0);
    }

    public float pouringThreshold;
    public float maxOil;
    public float oilCount = 0f;
    IEnumerator SliderGravity()
    {
        oilCount = 0f;
        while (true)
        {
            if (slider.value > pouringThreshold) oilCount += Time.deltaTime;

            if (oilCount >= maxOil) break;

            yield return null;
        }

        Debug.Log("bahh");
    }
}
