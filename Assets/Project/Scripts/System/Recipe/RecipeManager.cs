using ProjetoIV.Util;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : Singleton<RecipeManager>
{
    public Ingredient currentRecipe;
    public List<MinigameSequence> possibleSequences;
    public List<bool> currentSteps;
    public MinigameSequence currentSequence;

#if UNITY_EDITOR
    private void Start()
    {
        SetRecipe(currentRecipe);
    }
#endif

    public void SetRecipe(Ingredient p_finalIngredient)
    {
        Debug.Log("set recipe");
        for (int i = 0; i < possibleSequences.Count; i++)
        {
            if (p_finalIngredient == possibleSequences[i].FinalIngredient)
            {
            Debug.Log("final ingre " + possibleSequences[i].FinalIngredient);
                currentSteps = new();
                for (int j = 0; j < possibleSequences.Count; j++) currentSteps.Add(false);

                currentSequence = possibleSequences[i];
                return;
            }
        }
    }

    public bool CanOpenMinigame(Minigame p_minigame)
    {
        for (int i = 0; i < currentSteps.Count; i++)
        {
            if (currentSteps[i]) continue;

            return currentSequence.sequence[i] == p_minigame;
        }

        return false;
    }

    public void EndMinigame(Minigame p_minigame)
    {
        for (int i = 0; i < currentSteps.Count; i++)
        {
            if (currentSteps[i]) continue;

            if (currentSequence.sequence[i] == p_minigame)
            {
                currentSteps[i] = true;

                if (i + 1 == currentSteps.Count) EndedSequence();
                return;
            }
        }
    }

    public void EndedSequence()
    {
        Debug.Log("baita jogador");
    }
}
