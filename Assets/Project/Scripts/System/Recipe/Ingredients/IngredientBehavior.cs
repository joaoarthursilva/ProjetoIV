using UnityEngine;

public class IngredientBehavior : MonoBehaviour
{
    [SerializeField] private GameObject m_ingredient;
    [SerializeField] private Material m_material;
    [SerializeField] private GameObject m_processedIngredient;
    [SerializeField] private Material m_processedMaterial;

    [SerializeField] private Transform[] cuts;

    public void SetProcessed(bool p_set)
    {
        //    m_ingredient.SetActive(!p_set);
        //    m_processedIngredient.SetActive(p_set);
        if (!p_set) return;

        for (int i = 0; i < cuts.Length; i++)
        {
            SetCutPosition(i);
        }
    }

    public void AnimCut(int p_index)
    {
        SetCutPosition(p_index);
    }

    void SetCutPosition(int p_index)
    {
        Vector3 pos = new(Random.Range(-0.035f, 0.035f), 0f, Random.Range(0.0006f, 0.014f));

        cuts[p_index].localPosition = pos;
    }
    void ResetCutPosition(int p_index)
    {
        Vector3 pos = new(0f, 0f, 0f);

        cuts[p_index].localPosition = pos;
    }
}
