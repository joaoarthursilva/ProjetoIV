using UnityEngine;

public class SingleBoilBehaviour : MonoBehaviour
{
    public Transform finalPos;

    private void OnEnable()
    {
        Destroy(gameObject, 2f);    
    }

    
}
