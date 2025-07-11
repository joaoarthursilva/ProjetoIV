using System.Collections;
using ProjetoIV.Audio;
using UnityEngine;

public class PourStream : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public Transform origin;
    public Vector3 target;
    public float streamLenght;

    [Header("Anim")]
    public float startEndTime;
    public bool isPouring;
    private void Update()
    {
        if (!isPouring) return;

        lineRenderer.SetPosition(0, origin.transform.position);
        target = origin.transform.position - Vector3.up * streamLenght;
        lineRenderer.SetPosition(1, target);
    }

    public void StartPour()
    {
        Debug.Log("start pour");
        StartCoroutine(StartPourAnim());
    }

    public IEnumerator StartPourAnim()
    {
        AudioManager.Instance.PlayAudio(AudioID.OIL);
        lineRenderer.positionCount = 2;
        float l_streamLenght = 0f;
        float l_time = 0f;
        while (l_time < startEndTime)
        {
            l_streamLenght = Mathf.Lerp(0, streamLenght, l_time / startEndTime);

            lineRenderer.SetPosition(0, origin.transform.position);
            target = origin.transform.position - Vector3.up * l_streamLenght;
            lineRenderer.SetPosition(1, target);

            l_time += Time.deltaTime;
            yield return null;
        }

        isPouring = true;
    }

    public void EndPour()
    {
        Debug.Log("end pour");
        StartCoroutine(EndPourAnim());
    }
    public IEnumerator EndPourAnim()
    {
        Vector3 endOriginPoint = origin.transform.position + (Vector3.down * streamLenght);
        isPouring = false;
        float l_streamLenght = streamLenght;
        float l_time = 0f;
        while (l_time < startEndTime)
        {
            l_streamLenght = Mathf.Lerp(streamLenght, 0, l_time / startEndTime);

            lineRenderer.SetPosition(0, endOriginPoint + (l_streamLenght * Vector3.up));
            target = endOriginPoint;
            lineRenderer.SetPosition(1, target);

            l_time += Time.deltaTime;
            yield return null;
        }
        AudioManager.Instance.StopAudio(AudioID.OIL);

        lineRenderer.positionCount = 0;
    }
}
