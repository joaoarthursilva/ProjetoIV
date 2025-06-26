using ProjetoIV.Util;
using System.Collections;
using UnityEngine;

public class KnifeMinigameBehavior : MonoBehaviour
{
    public ObjectAnimationBehaviour cutAnimBehavior;
    public float moveAnimTime;
    public AnimationCurve moveAnimCurve;

    public Coroutine Cut()
    {
        return cutAnimBehavior.PlayAnimations(UIAnimationType.ENTRY);
    }

    public Coroutine EndCut()
    {
        return cutAnimBehavior.PlayAnimations(UIAnimationType.LEAVE);
    }

    private Vector3 l_lastPos;
    private Vector3 l_nextPos;
    public Coroutine MoveKnife(Vector3 p_addPos)
    {
        l_lastPos = transform.position;
        l_nextPos = p_addPos;
        l_nextPos.y = transform.position.y;
        return StartCoroutine(IMoveKnife());
    }

    IEnumerator IMoveKnife()
    {
        float l_time = 0f;
        while (l_time < moveAnimTime)
        {
            transform.position = Vector3.Lerp(l_lastPos, l_nextPos, moveAnimCurve.Evaluate(l_time / moveAnimTime));

            yield return null;
            l_time += Time.deltaTime;
        }
        transform.position = Vector3.Lerp(l_lastPos, l_nextPos, 1f);

    }
}
