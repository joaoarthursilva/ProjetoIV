using ProjetoIV.Util;
using UnityEngine;

public class IngredientBehavior : MonoBehaviour
{
    [SerializeField] private GameObject m_ingredient;
    [SerializeField] private Material m_material;
    [SerializeField] private GameObject m_processedIngredient;
    [SerializeField] private Material m_processedMaterial;
    [SerializeField] private ObjectAnimationBehaviour m_objectAnim;
    [Space]
    [SerializeField] private Transform[] cuts;
    public Transform cut0;
    public Transform cut1;

    public void SetProcessed(bool p_set)
    {
       
    }

    public void AnimCut(int p_index)
    {
        SetCutPosition(p_index);
    }

    public void SetKnifeBehavior()
    {

    }

    public Vector3 GetKnifePosition(float p_lerp)
    {
        return Vector3.Lerp(cut0.position, cut1.position, p_lerp);
    }

    void SetCutPosition(int p_index)
    {
        //Vector3 pos = cuts[p_index].localPosition * 2;

        cuts[p_index].localPosition *= 2;
    }
    void ResetCutPosition(int p_index)
    {
        Vector3 pos = new(0f, 0f, 0f);

        cuts[p_index].localPosition = pos;
    }
}
