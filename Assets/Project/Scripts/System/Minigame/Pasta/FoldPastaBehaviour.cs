using ProjetoIV.Audio;
using ProjetoIV.Util;
using UnityEngine;

public class FoldPastaBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_instancedAnimator;
    [SerializeField] private ObjectAnimationBehaviour m_instancedAnimationBehaviour;
    [SerializeField] private ObjectAnimationBehaviour m_rotateAnimationBehaviour;
    public GameObject filoi;
    public bool last;
    System.Action m_onEndRotate;
    public void SetFoldAnimationEvents(System.Action p_onEndRotate)
    {
        m_onEndRotate = p_onEndRotate;
    }

    public void StartStep1()
    {
        m_instancedAnimator.SetInteger("Fold", 1);
        AudioManager.Instance.Play(AudioID.PASTA_FOLD_SOUND);
    }

    public void EndFoldInteractionFirst()
    {
        m_instancedAnimator.SetInteger("Fold", 0);
        AudioManager.Instance.Play(AudioID.PASTA_FOLD_SOUND);
    }

    public void OnEndFold0()
    {
        AudioManager.Instance.Play(AudioID.MOVE_PASTA);
        filoi.SetActive(false);
        m_rotateAnimationBehaviour.PlayAnimations(UIAnimationType.ENTRY, () =>
        {
            m_onEndRotate?.Invoke();
        });
    }

    public void OnEndFold1()
    {
        if (last)
            FadeController.Instance.CallFadeAnimation(true, () =>
            {
                FadeController.Instance.CallFadeAnimation(false, null, .3f);
            }, .75f);
    }

    public void PlayEntryAnimation(System.Action p_delegate)
    {
        m_instancedAnimationBehaviour.PlayAnimations(UIAnimationType.ENTRY, p_delegate);
    }
    public void PlayLeaveAnimation(System.Action p_delegate)
    {
        p_delegate += () => Destroy(gameObject);
        m_instancedAnimator.SetInteger("Fold", 1);
        Invoke(nameof(AAAAA), 2.25f);
        m_instancedAnimationBehaviour.PlayAnimations(UIAnimationType.LEAVE, p_delegate);
    }
    
    public void AAAAA() => AudioManager.Instance.Play(AudioID.MOVE_PASTA);
}
