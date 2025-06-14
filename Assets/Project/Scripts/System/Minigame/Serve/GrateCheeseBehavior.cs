using ProjetoIV.Util;
using System.Collections;
using UnityEngine;

public class GrateCheeseBehavior : MonoBehaviour
{
    public ObjectAnimationBehaviour entryLeaveAnim;
    public ObjectAnimationBehaviour grateAnim;
    public ParticleSystem particles;

    [NaughtyAttributes.Button("Grate")]
    public Coroutine PlayGrate()
    {
        return StartCoroutine(Grate());
    }

    IEnumerator Grate()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return GrateCheese();
        }
    }

    IEnumerator GrateCheese()
    {
        bool l_grating = true;
        grateAnim.PlayAnimations(UIAnimationType.ENTRY, () => { l_grating = false; });

        if (!particles.isPlaying) particles.Play();
        while (l_grating) yield return null;
        if (particles.isPlaying) particles.Stop();

        yield return grateAnim.PlayAnimations(UIAnimationType.LEAVE);
    }

}
