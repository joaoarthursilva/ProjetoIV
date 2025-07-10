using System.Collections.Generic;
using UnityEngine;

public class PlateBehavior : MonoBehaviour
{
    [System.Serializable]
    public struct SeasonObjects
    {
        public Ingredient ingredient;
        public GameObject[] GOs;
    }

    public SeasonObjects[] seasonObjects;
    List<int> ingedients;

    private void Start()
    {
        for (int i = 0; i < seasonObjects.Length; i++)
        {
            for (int j = 0; j < seasonObjects[i].GOs.Length; j++)
            {
                seasonObjects[i].GOs[j].SetActive(false);
            }
        }
    }

    public void ActiveObject(Ingredient p_ingredient)
    {
        if (ingedients == null)
        {
            ingedients = new();
            for (int i = 0; i < seasonObjects.Length; i++)
            {
                ingedients.Add(0);
            }
        }

        int id = -1;
        for (int i = 0; i < seasonObjects.Length; i++)
        {
            if (seasonObjects[i].ingredient == p_ingredient)
            {
                id = i;
                break;
            }
        }

        if (id == -1) return;
        if (ingedients[id] == seasonObjects[id].GOs.Length) return;

        seasonObjects[id].GOs[ingedients[id]].SetActive(true);
        ingedients[id]++;
    }
}
