using Unity.Cinemachine;

public interface IFocusedMinigameStep
{
    public CinemachineCamera Camera { get; }
    public System.Action<CinemachineCamera, System.Action> OnFocusOnCamera { get; set; }
}
