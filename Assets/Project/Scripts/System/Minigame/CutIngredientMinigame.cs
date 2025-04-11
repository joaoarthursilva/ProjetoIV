using Unity.Cinemachine;
using UnityEngine;

public class CutIngredientMinigame : MonoBehaviour, IMinigameInteraction
{
    [SerializeField, SerializeReference] private Minigame[] m_minigames;

    [SerializeField] private CinemachineCamera m_camera;
    public CinemachineCamera MinigameCamera => m_camera;

    public bool EmbraceMinigame(Minigame p_minigame)
    {
        for (int i = 0; i < m_minigames.Length; i++)
        {
            if (m_minigames[i] == p_minigame) return true;
        }

        return false;
    }

    public void IOnStartInteraction()
    {

    }

    public void IOnEndInteraction()
    {

    }
    public void ICheckEndInteraction()
    {

    }
    public void IOnMove(Vector2 p_vector) { }

    public void IOnLook(Vector2 p_vector) { }

    public void IOnMouseClick() { }

    public void IOnMouseDown() { }

    public void IOnMouseUp() { }

    public void IOnCut()
    {
        Debug.Log("cut");
    }

    public void IOnEndedCut()
    {
        Debug.Log("ended cut");
    }
}
