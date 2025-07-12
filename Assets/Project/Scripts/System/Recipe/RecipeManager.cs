using ProjetoIV.Util;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : Singleton<RecipeManager>
{
    public Ingredient currentRecipe;
    public List<MinigameSequence> possibleSequences;
    public List<bool> currentSteps;
    public MinigameSequence currentSequence;
    public System.Action<Minigame> SetNextMinigame;
    public System.Action<Customer> UseRecipeBook;
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
                for (int j = 0; j < possibleSequences[i].sequence.Count; j++) currentSteps.Add(false);

                currentSequence = possibleSequences[i];
                SetNextMinigame?.Invoke(currentSequence.sequence[0]);
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

    public Minigame Nextminigame()
    {
        for (int i = 0; i < currentSteps.Count; i++)
        {
            if (currentSteps[i]) continue;

            return currentSequence.sequence[i];
        }

        return null;
    }

    public void EnteredMinigame(Minigame p_minigame)
    {
        SetNextMinigame?.Invoke(null);
    }

    public void EndMinigame(Minigame p_minigame, ref bool p_forceCall)
    {
        for (int i = 0; i < currentSteps.Count; i++)
        {
            if (currentSteps[i]) continue;

            if (currentSequence.sequence[i] == p_minigame)
            {
                currentSteps[i] = true;

                if (p_minigame.callServePlate) p_forceCall = true;
                if (i + 1 == currentSteps.Count) EndedSequence();
                else SetNextMinigame?.Invoke(currentSequence.sequence[i + 1]);

                return;
            }
        }
    }

    public void EndedSequence()
    {
        SetNextMinigame?.Invoke(currentSequence.sequence[^1]);
        Debug.Log("baita jogador");
    }

    [SerializeField] private Minigame bookminigame;
    public void SetInstructionToBook(Customer p_customer)
    {
        UseRecipeBook?.Invoke(p_customer);
        SetNextMinigame?.Invoke(bookminigame);
    }
}
