using ProjetoIV.RatInput;
using UnityEngine;

public class UIInputElement : MonoBehaviour
{
    public InputID inputId;
    public GameObject go;

    public void SetElementOn(bool p_set)
    {
        go.SetActive(p_set);
    }
}
