using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public enum Audio
{
    NONE,
    MSC_MAIN_THEME,
    SFX_JUMP,
}

public struct AudioData
{
    public Audio AudioID;
    public StudioEventEmitter Emitter;
}

public class AudioManager : MonoBehaviour
{
    [Header("SFX"), SerializeField] private List<AudioData> m_sfx;

    public void PlayAudio(Audio p_audioID)
    {
        for (int i = 0; i < m_sfx.Count; i++)
        {
            AudioData audioData = m_sfx[i];
            if (audioData.AudioID != p_audioID) continue;
            audioData.Emitter.Play();
            return;
        }
    }

    public void StopAudio(Audio p_audioID)
    {
        for (int i = 0; i < m_sfx.Count; i++)
        {
            AudioData audioData = m_sfx[i];
            if (audioData.AudioID != p_audioID) continue;
            audioData.Emitter.Stop();
            return;
        }
    }
}