using System.Collections;
using UnityEngine;

public class BoilBehavior : MonoBehaviour
{
    [SerializeField] private GameObject[] m_pastaObject;
    private Transform m_futureParent;
    public void SetFutureParent(Transform p_parent)
    {
        m_futureParent = p_parent;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < m_pastaObject.Length; i++)
        {
            m_pastaObject[i].transform.SetParent(m_futureParent);
        }

        Destroy(gameObject);

    }
}
