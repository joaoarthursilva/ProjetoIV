using System;
using System.Collections.Generic;
using FMODUnity;
using ProjetoIV.Util;
using UnityEngine;

namespace ProjetoIV.Audio
{
    public enum AudioID
    {
        NONE,
        MSC_MAIN_THEME,
        SFX_DIALOG,
        PASTA_CUT,
        PASTA_FOLD_RIGHT,
        PASTA_FOLD_WRONG,
        PASTA_FOLD_SOUND,
        MOVE_PASTA,
        CUSTOMER_ARRIVED,
        OIL,
        DROP_IN_WATER,
        GRATE_CHEESE,
        GRIND_PEPPER,
    }

    [Serializable]
    public struct AudioEventReference
    {
        public AudioID id;
        public EventReference eventReference;
    }

    [Serializable]
    public struct AudioReference
    {
        public AudioID id;
        public StudioEventEmitter emitter;
    }

    public class AudioManager : Singleton<AudioManager>
    {
        [Header("SFX"), SerializeField] private List<AudioReference> m_soundReferences;
        [SerializeField] private List<AudioEventReference> m_audioEventReferences;

        public void Play(EventReference p_eventReference)
        {
            if (p_eventReference.IsNull) return;
            RuntimeManager.PlayOneShot(p_eventReference);
        }

        public void Play(EventReference p_eventReference, Vector3 p_pos)
        {
            if (p_eventReference.IsNull) return;
            RuntimeManager.PlayOneShot(p_eventReference, p_pos);
        }

        public void Play(EventReference p_eventReference, GameObject p_go)
        {
            if (p_eventReference.IsNull) return;
            RuntimeManager.PlayOneShotAttached(p_eventReference, p_go);
        }

        public void Play(AudioID p_audioID, Vector3 p_pos)
        {
            EventReference aaa = GetEventReference(p_audioID);
            if (aaa.IsNull) return;
            RuntimeManager.PlayOneShot(aaa, p_pos);
        }
        public void Play(AudioID p_audioID)
        {
            EventReference aaa = GetEventReference(p_audioID);
            if (aaa.IsNull) return;
            RuntimeManager.PlayOneShot(aaa);
        }

        public void PlayAudio(AudioID p_audioID)
        {
            GetEmitter(p_audioID)?.Play();
        }

        public void StopAudio(AudioID p_audioID)
        {
            GetEmitter(p_audioID)?.Stop();
        }

        private StudioEventEmitter GetEmitter(AudioID p_id)
        {
            for (int i = 0; i < m_soundReferences.Count; i++)
            {
                if (m_soundReferences[i].id == p_id)
                {
                    return m_soundReferences[i].emitter;
                }
            }

            return null;
        }

        private EventReference GetEventReference(AudioID p_id)
        {
            for (int i = 0; i < m_audioEventReferences.Count; i++)
            {
                if (m_audioEventReferences[i].id == p_id)
                {
                    return m_audioEventReferences[i].eventReference;
                }
            }

            return new EventReference();
        }

        private void StopAllSounds()
        {
            for (int i = 0; i < m_soundReferences.Count; i++)
            {
                m_soundReferences[i].emitter.Stop();
            }
        }

        [Header("Debug"), SerializeField] private bool m_debug;
    }
}