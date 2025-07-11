using ProjetoIV.Audio;
using UnityEngine;

public class CustomerAnimation : MonoBehaviour
{
    [SerializeField] private CustomerBehaviour m_customerBehaviour;
    [SerializeField] private GameObject m_doroteaIdle;
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    Vector3 odeioOpedro;
    public void OnEndEntryAnim()
    {
        Debug.Log("OnEndEntryAnim");
        m_customerBehaviour.Collider.enabled = true;
        AudioManager.Instance.Play(AudioID.CUSTOMER_ARRIVED, m_customerBehaviour.gameObject.transform.position);
        if (m_doroteaIdle == null)
        {
            odeioOpedro = transform.localPosition;
            transform.localPosition = new Vector3(0.68f, odeioOpedro.y, odeioOpedro.z);
            m_animator.Play("Idle");
        }
        else
        {
            m_doroteaIdle.SetActive(true);
            odeioOpedro = transform.localPosition;
            transform.localPosition = new Vector3(odeioOpedro.x, odeioOpedro.y + 50f, odeioOpedro.z);
        }
    }

    public void OnStartExitAnim()
    {
        Debug.Log("OnStartExitAnim");
        transform.localPosition = odeioOpedro;
        m_customerBehaviour.Collider.enabled = false;

        if (m_doroteaIdle != null)
        {
            m_doroteaIdle.SetActive(false);
        }
    }


    public void OnEndExitAnim()
    {
        TimeManager.Instance.PassTime(1f, true);
    }

}
