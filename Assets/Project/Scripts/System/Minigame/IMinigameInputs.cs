using UnityEngine;

public interface IMinigameInputs
{
    public void IOnLook(Vector2 p_vector);
    public void IOnMove(Vector2 p_vector);
    public void IOnMouseDown();
    public void IOnMouseUp();
    public void IOnMouseClick();
    public void IOnCut();
    public void IOnEndedCut();
}
