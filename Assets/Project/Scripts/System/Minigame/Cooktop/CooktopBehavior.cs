using UnityEngine;

public class CooktopBehavior : MonoBehaviour
{
    [SerializeField] bool[] stove;
    [SerializeField] GameObject[] fires;
    public void TurnOnStovePit(int p_stoveID)
    {
        stove[p_stoveID] = !stove[p_stoveID];

        fires[p_stoveID].SetActive(stove[p_stoveID]);
    }


}
