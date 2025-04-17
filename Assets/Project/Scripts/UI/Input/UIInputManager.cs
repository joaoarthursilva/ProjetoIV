using ProjetoIV.RatInput;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] private UIInputElement[] inputElements;

    private void Start()
    {
        SetInputElementOn(InputID.NONE);
        RatInput.ShowInputUIElement += SetInputElementOn;
    }

    private void OnDestroy()
    {
        RatInput.ShowInputUIElement -= SetInputElementOn;
    }

    public void SetInputElementOn(InputID p_inputID)
    {
        for (int i = 0; i < inputElements.Length; i++)
        {
            inputElements[i].SetElementOn(p_inputID == inputElements[i].inputId);
        }
    }
}
