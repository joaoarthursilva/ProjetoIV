using UnityEngine;

public class UICurrentStruction : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        RecipeManager.Instance.SetNextMinigame += UpdateInstruction;
    }

    private void UpdateInstruction(Minigame p_minigame)
    {
        if(p_minigame == null)
        {
            text.text = string.Empty;
            return;
        }

        text.text = p_minigame.Instruction;
    }
}
