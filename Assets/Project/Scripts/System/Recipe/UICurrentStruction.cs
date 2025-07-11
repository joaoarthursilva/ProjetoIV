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
        Debug.Log("bbbb");
            text.text = string.Empty;
            return;
        }

        Debug.Log("ccc");
        text.text = p_minigame.Instruction;
    }

    public void UpdateInstructionToBook()
    {
        Debug.Log("aasd");
        text.text = selectRecipeText;
    }
}
