using ProjetoIV.Util;
using UnityEngine;

public class SeasoningBehavior : MonoBehaviour
{
    public Ingredient ingredient;
    [SerializeField] protected ObjectAnimationBehaviour m_anim;
    [SerializeField] protected Transform particleParent;
    [SerializeField] protected GameObject particlesPrefab;
    [SerializeField, NaughtyAttributes.ReadOnly] protected ParticleSystem particles;

    public void SetTransformSimulationSpace(Transform p_transform)
    {
        m_tranformToAttachParticles = p_transform;
    }

    public virtual Coroutine PlayAnim()
    {
        particles = Instantiate(particlesPrefab, particleParent).GetComponent<ParticleSystem>();
        var main = particles.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Custom;
        main.customSimulationSpace = m_tranformToAttachParticles;
        gameObject.SetActive(true);
        particles.Clear();
        if (particles.isPlaying) particles.Stop();
        if (!particles.isPlaying) particles.Play();

        return m_anim.PlayAnimations(UIAnimationType.ENTRY);
    }

   protected Transform m_tranformToAttachParticles;
    public void SetParticlesParent(Transform p_transform)
    {
        m_tranformToAttachParticles = p_transform;
        float particlesScaleFactor = particles.transform.lossyScale.x / particles.transform.localScale.x;
        particles.transform.SetParent(null);
        particles.transform.localScale /= particlesScaleFactor;
        Invoke(nameof(StopParticles), 0.5f);
    }

    void StopParticles()
    {
        var main = particles.main;
        main.gravityModifier = 0f;
        main.emitterVelocity = Vector3.zero;
        particles.Stop();
        particles.gameObject.GetComponent<AttachObject>().StartFollow(m_tranformToAttachParticles);
    }
}
