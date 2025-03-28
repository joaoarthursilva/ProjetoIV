using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ProjetoIV.Util
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected List<T> pooledObjects;
        [SerializeField] protected GameObject pooledObjectPrefab;

        protected T GetObjectSetPositionAndRotation(Vector3 p_position, Quaternion p_rotation)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].gameObject.activeInHierarchy)
                {
                    pooledObjects[i].gameObject.SetActive(true);
                    pooledObjects[i].transform.position = p_position;
                    pooledObjects[i].transform.rotation = p_rotation;
                    return pooledObjects[i];
                }
            }

            pooledObjects.Add(Instantiate(pooledObjectPrefab, p_position, p_rotation, transform)
                .GetComponent<T>());
            return pooledObjects[^1];
        }

        protected T GetObjectSetPosition(Vector3 p_position)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].gameObject.activeInHierarchy)
                {
                    pooledObjects[i].gameObject.SetActive(true);
                    pooledObjects[i].transform.position = p_position;
                    return pooledObjects[i];
                }
            }

            pooledObjects.Add(Instantiate(pooledObjectPrefab, p_position, Quaternion.identity, transform)
                .GetComponent<T>());
            return pooledObjects[^1];
        }

        protected T GetObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].gameObject.activeInHierarchy) return pooledObjects[i];
            }

            return (Instantiate(pooledObjectPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<T>());
        }

        [SerializeField, HorizontalLine, Header("Test")]
        private int m_generatePoolAmount;

        [Button]
        private void GenerateInstances()
        {
            for (int i = 0; i < m_generatePoolAmount; i++)
            {
                pooledObjects.Add(Instantiate(pooledObjectPrefab, Vector3.zero, Quaternion.identity, transform)
                    .GetComponent<T>());
                pooledObjects[^1].gameObject.SetActive(false);
            }
        }

        [Button]
        private void ClearPool()
        {
            var a = gameObject.GetComponentsInChildren(typeof(Transform), true);
            for (int i = 0; i < a.Length; i++)
            {
                DestroyImmediate(a[i].gameObject);
            }

            pooledObjects.Clear();
        }
    }
}