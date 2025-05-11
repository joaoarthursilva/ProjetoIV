using System.Collections.Generic;
using Assets.Plugins.RatLocalization.Scripts;
using ProjetoIV.Util;
using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_gamePause;

    [SerializeField] private TMP_Dropdown m_languageDropdown;
    [SerializeField] private List<string> m_availableLanguages = new();

    private float m_volume;
    private float m_soundEffects;

    public void ButtonPause()
    {
        Time.timeScale = 0f;
        m_gamePause.SetActive(true);
        m_languageDropdown.value = IdentifyLenguage();
    }

    public void ButtonBack()
    {
        Time.timeScale = 1f;
        m_gamePause.SetActive(false);
    }

    public void ButtonMenu()
    {
        SceneLoader.Instance.Load(SceneLoader.Scene.SCN_Menu);
    }

    public void SetVolume(System.Single p_volume)
    {
        m_volume = p_volume;
    }

    public void SetSoundEffects(System.Single p_soundEffects)
    {
        m_soundEffects = p_soundEffects;
    }

    public void SetLanguage(System.Int32 p_value)
    {
        GameLocalization.Instance.SetLocalization(m_availableLanguages[p_value]);
    }

    int IdentifyLenguage()
    {
        string l_atualLanguage = LocalizationManager.Language;
        for (int i = 0; i < m_availableLanguages.Count; i++)
        {
            if (m_availableLanguages[i].Equals(l_atualLanguage))
            {
                return i;
            }
        }
        return 0;
    }
}
