using ProjetoIV.Util;
using UnityEngine;

public class SeasoningBehavior : MonoBehaviour
{
    public Ingredient ingredient;
    [SerializeField] protected ObjectAnimationBehaviour m_anim;
    [SerializeField] protected ParticleSystem particles;

    public virtual Coroutine PlayAnim()
    {
        gameObject.SetActive(true);
        particles.Clear();
        if(particles.isPlaying) particles.Stop();
        if(!particles.isPlaying) particles.Play();

        return m_anim.PlayAnimations(UIAnimationType.ENTRY);
    }
}
