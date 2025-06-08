using System.Collections.Generic;
using UnityEngine;

public class GetSetChildrenPosition : MonoBehaviour
{
    [System.Serializable]
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

        public List<TransformData> children = new();
    [NaughtyAttributes.Button]
    public void GetPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(new()
            {
                position = transform.GetChild(i).position,
                rotation = transform.GetChild(i).rotation
            });
        }
    }

    [NaughtyAttributes.Button]
    public void SetPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.SetPositionAndRotation(children[i].position, children[i].rotation);
        }
    }
}
