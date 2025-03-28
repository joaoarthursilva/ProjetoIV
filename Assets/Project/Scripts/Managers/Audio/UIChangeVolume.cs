using UnityEngine;
using TMPro;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

namespace ProjetoIV.Audio
{
    public class UIChangeVolume : MonoBehaviour
    {
        public const string SAVE_MASTERVOLUME_KEY = "MasterVolumeKey";
        public const string VCA_MASTERVOLUME_KEY = "vca:/Master";

        private VCA m_MasterVCA;
        [SerializeField] private TextMeshProUGUI m_volumeText;
        [SerializeField] private Slider m_volumeSlider;

        private void Awake()
        {
            m_MasterVCA = RuntimeManager.GetVCA(VCA_MASTERVOLUME_KEY);
        }

        private void Start()
        {
            m_MasterVCA.getVolume(out float l_volume);
            m_volumeText.text = string.Format("{0:00}", (l_volume * 100f));
            m_volumeSlider.SetValueWithoutNotify(l_volume);
        }

        public void SetMasterVolume(float p_volume)
        {
            m_volumeText.text = string.Format("{0:00}", (p_volume * 100f));
            m_MasterVCA.setVolume(p_volume);
            PlayerPrefs.SetFloat(SAVE_MASTERVOLUME_KEY, p_volume);
        }
    }
}