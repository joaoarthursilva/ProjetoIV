using ProjetoIV.Util;
using System.Collections;
using UnityEngine;

public class GrinderSeasoningBehavior : SeasoningBehavior
{
    [SerializeField] private float waitToEnter;
    public override Coroutine PlayAnim()
    {
        particles = Instantiate(particlesPrefab, particleParent).GetComponent<ParticleSystem>();
        var main = particles.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Custom;
        main.customSimulationSpace = m_tranformToAttachParticles;

        gameObject.SetActive(true);
        return StartCoroutine(IPlayAnim());
    }

    IEnumerator IPlayAnim()
    {
        bool l_wait = true;
        m_anim.PlayAnimations(UIAnimationType.ENTRY, delegate { l_wait = false; });

        yield return new WaitForSeconds(waitToEnter);

        particles.Clear();
        particles.Play();

        while(l_wait) yield return null;

        yield return m_anim.PlayAnimations(UIAnimationType.LEAVE);
    }
}
