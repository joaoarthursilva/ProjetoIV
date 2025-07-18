using ProjetoIV.Util;
using System.Collections;
using ProjetoIV.Audio;
using UnityEngine;

public class GrinderSeasoningBehavior : SeasoningBehavior
{
    [SerializeField] private float waitToEnter;
    public override Coroutine PlayAnim()
    {
        particles = Instantiate(particlesPrefab, particleParent).GetComponent<ParticleSystem>();
        gameObject.SetActive(true);

        AddParticlesCoroutine = StartCoroutine(AddParticles());
        return StartCoroutine(IPlayAnim());
    }

    IEnumerator IPlayAnim()
    {
        bool l_wait = true;
        m_anim.PlayAnimations(UIAnimationType.ENTRY, delegate { l_wait = false; });

        yield return new WaitForSeconds(waitToEnter);

        AudioManager.Instance.Play(AudioID.GRIND_PEPPER, transform.position);
        
        particles.Clear();
        particles.Play();

        while (l_wait) yield return null;

        yield return m_anim.PlayAnimations(UIAnimationType.LEAVE);
    }
}
