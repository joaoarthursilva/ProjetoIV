using UnityEngine;

public class UICurrentStruction : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public string selectRecipeText;
    private void Start()
    {
        RecipeManager.Instance.SetNextMinigame += UpdateInstruction;
        RecipeManager.Instance.UseRecipeBook += UpdateInstructionToBook;
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

    public void UpdateInstructionToBook()
    {
        text.text = selectRecipeText;
    }
}
