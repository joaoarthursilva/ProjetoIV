using System.Collections;
using ProjetoIV.Audio;
using UnityEngine;

public class BoilBehavior : MonoBehaviour
{
    [SerializeField] private bool debug;
    [SerializeField] private GameObject[] m_pastaObject;
    private Transform m_futureParent;
    public void SetFutureParent(Transform p_parent)
    {
        m_futureParent = p_parent;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        AudioManager.Instance.Play(AudioID.DROP_IN_WATER, transform.position);
        yield return new WaitForSeconds(0.6f);
        if(debug) yield break;
        for (int i = 0; i < m_pastaObject.Length; i++)
        {
            m_pastaObject[i].transform.SetParent(m_futureParent);
        }

        Destroy(gameObject);

    }
}
