using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Text;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FPSDisplay : MonoBehaviour
{
    public float updateInterval = 0.5f;

    public bool showMedian = false;
    public float medianLearnrate = 0.05f;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    private float currentFPS = 0;

    private float median = 0;
    private float average = 0;

    StringBuilder m_textStringBuilder = new StringBuilder();

    public float CurrentFPS
    {
        get { return currentFPS; }
    }

    public float FPSMedian
    {
        get { return median; }
    }

    public float FPSAverage
    {
        get { return average; }
    }

    TextMeshProUGUI uguiText;

    void Start()
    {
        uguiText = GetComponent<TextMeshProUGUI>();
        timeleft = updateInterval;
    }

    void Update()
    {
        // Timing inside the editor is not accurate. Only use in actual build.

        //#if !UNITY_EDITOR

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            currentFPS = accum / frames;

            average += (Mathf.Abs(currentFPS) - average) * 0.1f;
            median += Mathf.Sign(currentFPS - median) * Mathf.Min(average * medianLearnrate, Mathf.Abs(currentFPS - median));

            // display two fractional digits (f2 format)
            float fps = showMedian ? median : currentFPS;

            m_textStringBuilder.Clear();
            m_textStringBuilder.AppendFormat("{0:F2} FPS ({1:F1} ms)", fps, 1000.0f / fps);

            uguiText.text = m_textStringBuilder.ToString();

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
        //#endif
    }

    public void ResetMedianAndAverage()
    {
        median = 0;
        average = 0;
    }
}