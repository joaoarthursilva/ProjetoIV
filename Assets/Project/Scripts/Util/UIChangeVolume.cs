using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

public class UIChangeVolume : MonoBehaviour
{
    public const string SAVE_MASTERVOLUME_KEY = "MasterVolumeKey";
    public const string VCA_MASTERVOLUME_KEY = "vca:/Master";

    private VCA m_MasterVCA;
    [SerializeField] TextMeshProUGUI m_voluemText;
    [SerializeField] Slider m_volumeSlider;

    void Awake()
    {
        m_MasterVCA = RuntimeManager.GetVCA(VCA_MASTERVOLUME_KEY);
    }

    private void Start()
    {
        float l_volume = 0;
        m_MasterVCA.getVolume(out l_volume);
        m_voluemText.text = string.Format("{0:00}", (l_volume * 100f));
        m_volumeSlider.SetValueWithoutNotify(l_volume);
    }

    public void SetMasterVolume(float p_volume)
    {
        m_voluemText.text = string.Format("{0:00}", (p_volume * 100f));
        m_MasterVCA.setVolume(p_volume);
        PlayerPrefs.SetFloat(SAVE_MASTERVOLUME_KEY, p_volume);
    }
}
