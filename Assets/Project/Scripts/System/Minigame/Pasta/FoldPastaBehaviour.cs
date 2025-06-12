using ProjetoIV.Util;
using UnityEngine;

public class FoldPastaBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_instancedAnimator;
    [SerializeField] private ObjectAnimationBehaviour m_instancedAnimationBehaviour;

    System.Action m_onEndStep0;
    System.Action m_onEndFold;
    public void StartFoldAnimation(System.Action p_onEnd, System.Action p_onEndStep0)
    {
        m_onEndFold = p_onEnd;
        m_onEndStep0 = p_onEndStep0;
    }

    public void StartStep1()
    {
        m_instancedAnimator.SetInteger("Fold", 1);
    }

    public void EndFoldStep0()
    {
        m_onEndStep0?.Invoke();
    }

    public void EndFoldAnimation()
    {
        m_onEndFold?.Invoke();
    }

    public void PlayEntryAnimation(System.Action p_delegate)
    {
        m_instancedAnimationBehaviour.PlayAnimations(UIAnimationType.ENTRY, p_delegate);
    }
    public void PlayLeaveAnimation(System.Action p_delegate)
    {
        p_delegate += () => Destroy(gameObject);
        m_instancedAnimator.SetInteger("Fold", 1);
        m_instancedAnimationBehaviour.PlayAnimations(UIAnimationType.LEAVE, p_delegate);
    }
}
