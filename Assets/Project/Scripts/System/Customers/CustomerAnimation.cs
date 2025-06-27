using UnityEngine;

public class CustomerAnimation : MonoBehaviour
{
    [SerializeField] private CustomerBehaviour m_customerBehaviour;

    public void OnEndEntryAnim()
    {
        Debug.Log("OnEndEntryAnim");
        m_customerBehaviour.Collider.enabled = true;
    }

    public void OnStartExitAnim()
    {
        Debug.Log("OnStartExitAnim");
        m_customerBehaviour.Collider.enabled = false;
    }


    public void OnEndExitAnim()
    {
        TimeManager.Instance.PassTime(1f, true);
    }

}
