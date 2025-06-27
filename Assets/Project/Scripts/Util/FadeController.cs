using ProjetoIV.Util;
using UnityEngine;

public class FadeController : Singleton<FadeController>
{
    public UIAnimationBehaviour fadeAnimations;

    public void CallFadeAnimation(bool p_fadeState, System.Action p_afterAction = null, float p_time = .3f)
    {
        if (p_fadeState)
        {
            fadeAnimations.entryUiAnimationsList[0].fadeAnimationTime = p_time;
            fadeAnimations.PlayAnimations(UIAnimationType.ENTRY, p_afterAction);
        }
        else
        {
            fadeAnimations.leaveUiAnimationsList[0].fadeAnimationTime = p_time;
            fadeAnimations.PlayAnimations(UIAnimationType.LEAVE, p_afterAction);
        }
    }

}
