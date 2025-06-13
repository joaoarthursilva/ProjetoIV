using ProjetoIV.Util;
using UnityEngine;

public class FoldPastaBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_instancedAnimator;
    [SerializeField] private ObjectAnimationBehaviour m_instancedAnimationBehaviour;
    [SerializeField] private ObjectAnimationBehaviour m_rotateAnimationBehaviour;

    System.Action m_onEndRotate;
    public void SetFoldAnimationEvents(System.Action p_onEndRotate)
    {
        m_onEndRotate = p_onEndRotate;
    }

    public void StartStep1()
    {
        m_instancedAnimator.SetInteger("Fold", 1);
    }

    public void EndFoldInteractionFirst()
    {
        m_instancedAnimator.SetInteger("Fold", 0);
    }

    public void OnEndFold0()
    {
        m_rotateAnimationBehaviour.PlayAnimations(UIAnimationType.ENTRY, () =>
        {
            m_onEndRotate?.Invoke();
        });
    }

    public void OnEndFold1() { }

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
