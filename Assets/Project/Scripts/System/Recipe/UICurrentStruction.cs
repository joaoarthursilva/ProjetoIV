using Assets.Plugins.RatLocalization.Scripts;
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
        if (p_minigame == null)
        {
            Debug.Log("bbbb");
            text.text = string.Empty;
            return;
        }

        Debug.Log("ccc");
        if (!string.IsNullOrEmpty(m_feelingKey))
        {
            text.text = LocalizationManager.Localize(m_feelingKey) + " " + p_minigame.Instruction;
            m_feelingKey = string.Empty;
            return;
        }

        text.text = p_minigame.Instruction;
    }

    string m_feelingKey;
    public void UpdateInstructionToBook(Customer p_custumer)
    {
        m_feelingKey = p_custumer.feelingKey;
    }
}
