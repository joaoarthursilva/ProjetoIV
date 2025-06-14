using UnityEngine;

public class LayoutOnWorld : MonoBehaviour
{
    public Transform[] transfoms;
    public Vector3 positionZero;
    public Vector3 positionAdd;

    [NaughtyAttributes.Button]
    public void SetPositions()
    {
        Vector3 l_tempPos = positionZero;
        for (int i = 0; i < transfoms.Length; i++)
        {
            transfoms[i].localPosition = l_tempPos;
            l_tempPos += positionAdd;
        }
    }
}
