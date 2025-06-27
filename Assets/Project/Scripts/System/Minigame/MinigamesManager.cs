using UnityEngine;
using ProjetoIV.RatInput;
using System.Collections.Generic;
using Unity.Cinemachine;

public class MinigamesManager : MonoBehaviour, IMinigameInputs
{
    public static System.Action<CinemachineCamera, System.Action> OnSetMinigamecamera;
    public static System.Action<CinemachineCamera, System.Action> OnSetMinigamecameraOverride;
    public ProjetoIV.RatInput.Input cut;
    public ProjetoIV.RatInput.Input exitInteraction;

    public List<IMinigameInteraction> minigamesInteraction;
    private IMinigameInteraction m_currentMinigameInteraction;
    [SerializeField] private ServingStation m_servingStation;
    private Minigame m_currentMinigame;
    //public Dictionary<RaycastableObject, >
    [SerializeField] private PlayerInventory m_playerInventory;

    private void Start()
    {
        SubscribeToInput();

        var components = GetComponentsInChildren<IMinigameInteraction>();
        minigamesInteraction = new List<IMinigameInteraction>();

        for (int i = 0; i < components.Length; i++)
            if (components[i] is IMinigameInteraction minigame) minigamesInteraction.Add(minigame);
    }

    void SubscribeToInput()
    {
        cut = RatInput.Instance.GetInput(InputID.MINIGAME_CUT);
        cut.OnInputStarted += IOnCut;
        cut.OnInputCanceled += IOnEndedCut;

        exitInteraction = RatInput.Instance.GetInput(InputID.MINIGAME_ENDINTERACTION);
        exitInteraction.OnInputCanceled += IOnPressExit;
    }

    void UnsubscribeToInput()
    {
        if (cut != null)
        {
            cut.OnInputStarted -= IOnCut;
            cut.OnInputCanceled -= IOnEndedCut;
        }

        if (exitInteraction != null) exitInteraction.OnInputCanceled -= IOnPressExit;

    }

    private void OnDestroy()
    {
        UnsubscribeToInput();
    }

    public void OnInteractWithRaycastableObject(RaycastableMinigame p_object)
    {
        for (int i = 0; i < minigamesInteraction.Count; i++)
        {
            if (minigamesInteraction[i].RaycastableMinigame.Contains(p_object)
                && (minigamesInteraction[i].EmbraceMinigame(RecipeManager.Instance.Nextminigame())
                    || minigamesInteraction[i] is BookStation))
            {
                m_currentMinigameInteraction = minigamesInteraction[i];
                m_currentMinigame = minigamesInteraction[i] is BookStation bookStation ? bookStation.m_minigame : RecipeManager.Instance.Nextminigame();

                RecipeManager.Instance.EnteredMinigame(m_currentMinigame);
                if (m_currentMinigameInteraction.FadeBefore)
                {
                    FadeController.Instance.CallFadeAnimation(true);
                    Debug.Log("fafe");
                }
                OnSetMinigamecamera?.Invoke(m_currentMinigameInteraction.Camera,
                                            () => m_currentMinigameInteraction.IOnStartInteraction(m_currentMinigame,
                                                                                        () => OnEndMinigame(m_currentMinigame.FinalIngredient())));
                m_currentMinigameInteraction.OnFocusCamera = OnFocusCamera;

                RatInput.Instance.SetMap(m_currentMinigameInteraction.Map);
                break;
            }
        }
    }

    private void OnEndMinigame(Ingredient p_finalIngredient)
    {
        m_playerInventory.SetCurrentInventory(p_finalIngredient);

        bool l_forceCallPlate = false;
        RecipeManager.Instance.EndMinigame(m_currentMinigame, ref l_forceCallPlate);

        if (l_forceCallPlate)
        {
            CallServePlate();
            return;
        }

        m_currentMinigameInteraction = null;
        OnSetMinigamecamera?.Invoke(null, null);
        RatInput.Instance.SetMap(Map.KITCHEN);

    }

    public void CallServePlate()
    {
        if (m_servingStation.EmbraceMinigame(RecipeManager.Instance.Nextminigame()))
        {
            m_currentMinigameInteraction = m_servingStation;
            m_currentMinigame = RecipeManager.Instance.Nextminigame();

            float l_fadeTime = 1f;
            FadeController.Instance.CallFadeAnimation(true, null, l_fadeTime);
            Invoke(nameof(SetServeCamera), l_fadeTime -.5f);
        }
    }

    void SetServeCamera()
    {
        RecipeManager.Instance.EnteredMinigame(m_currentMinigame);
        OnSetMinigamecamera?.Invoke(m_currentMinigameInteraction.Camera,
                                    () =>
                                    {
                                        FadeController.Instance.CallFadeAnimation(false, null, .3f);
                                        m_currentMinigameInteraction.IOnStartInteraction(m_currentMinigame,
                                                                               () => OnEndMinigame(m_currentMinigame.FinalIngredient()));
                                    });
        m_currentMinigameInteraction.OnFocusCamera = OnFocusCamera;

        RatInput.Instance.SetMap(m_currentMinigameInteraction.Map);
    }

    private void OnFocusCamera(CinemachineCamera p_camera, System.Action p_action)
    {
        OnSetMinigamecameraOverride?.Invoke(p_camera, p_action);
    }

    public void IOnLook(Vector2 p_vector)
    {
        if (m_currentMinigameInteraction == null) return;

    }

    public void IOnMouseClick()
    {
        if (m_currentMinigameInteraction == null) return;

    }

    public void IOnMouseDown()
    {
        if (m_currentMinigameInteraction == null) return;

    }

    public void IOnMouseUp()
    {
        if (m_currentMinigameInteraction == null) return;

    }

    public void IOnMove(Vector2 p_vector)
    {
        if (m_currentMinigameInteraction == null) return;

    }

    public void IOnCut()
    {
        if (m_currentMinigameInteraction == null) return;

        m_currentMinigameInteraction.IOnCut();
    }

    public void IOnEndedCut()
    {
        if (m_currentMinigameInteraction == null) return;

        m_currentMinigameInteraction.IOnEndedCut();
    }

    public void IOnPressExit()
    {
        if (m_currentMinigameInteraction == null) return;

        m_currentMinigameInteraction.IOnPressExit();
    }

    public static void SetCursorVisible(bool p_state)
    {
        if (p_state)
        {
            CursorBehavior.Set(true, CursorLockMode.None);
        }
        else
        {
            CursorBehavior.Set(false, CursorLockMode.Locked);
        }
    }

}
