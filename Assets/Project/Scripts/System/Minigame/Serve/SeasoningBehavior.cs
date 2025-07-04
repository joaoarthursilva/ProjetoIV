using ProjetoIV.Util;
using UnityEngine;

public class SeasoningBehavior : MonoBehaviour
{
    public Ingredient ingredient;
    [SerializeField] protected ObjectAnimationBehaviour m_anim;
    [SerializeField] protected Transform particleParent;
    [SerializeField] protected GameObject particlesPrefab;
    [SerializeField, NaughtyAttributes.ReadOnly] protected ParticleSystem particles;

    public virtual Coroutine PlayAnim()
    {
        particles = Instantiate(particlesPrefab, particleParent).GetComponent<ParticleSystem>();
        gameObject.SetActive(true);
        particles.Clear();
        if(particles.isPlaying) particles.Stop();
        if(!particles.isPlaying) particles.Play();

        return m_anim.PlayAnimations(UIAnimationType.ENTRY);
    }

    public void SetParticlesParent(Transform p_transform)
    {
        particles.transform.parent = p_transform;
        particles.Stop();
        Invoke(nameof(StopParticles), 0.2f);
        //particles.set = ParticleSystemSimulationSpace.Local;
    }

    void StopParticles()
    { 
        particles.Pause();
    }
}
