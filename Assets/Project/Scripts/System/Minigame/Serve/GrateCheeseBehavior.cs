using ProjetoIV.Util;
using System.Collections;
using UnityEngine;

public class GrateCheeseBehavior : SeasoningBehavior
{
    public ObjectAnimationBehaviour entryLeaveAnim;
    public ObjectAnimationBehaviour grateAnim;

    [NaughtyAttributes.Button("Grate")]
    public override Coroutine PlayAnim()
    {
        particles = Instantiate(particlesPrefab, particleParent).GetComponent<ParticleSystem>();
        gameObject.SetActive(true);

        AddParticlesCoroutine = StartCoroutine(AddParticles());
        return StartCoroutine(Grate());
    }

    IEnumerator Grate()
    {
        yield return entryLeaveAnim.PlayAnimations(UIAnimationType.ENTRY);

        particles.Clear();
        for (int i = 0; i < 3; i++)
        {
            yield return GrateCheese();
        }

        yield return entryLeaveAnim.PlayAnimations(UIAnimationType.LEAVE);
    }

    IEnumerator GrateCheese()
    {
        bool l_grating = true;
        grateAnim.PlayAnimations(UIAnimationType.ENTRY, () => { l_grating = false; });

        if (!particles.isPlaying) particles.Play();
        while (l_grating) yield return null;
        //if (particles.isPlaying) particles.pau();

        yield return grateAnim.PlayAnimations(UIAnimationType.LEAVE);
    }

}
