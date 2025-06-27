using System.Collections.Generic;
using Assets.Plugins.RatLocalization.Scripts;
using ProjetoIV.RatInput;
using ProjetoIV.Util;
using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_gameMain;
    [SerializeField] private GameObject m_gameCredits;
    [SerializeField] private GameObject m_gameOptions;

    [SerializeField] private GameObject m_gamePause;

    [SerializeField] private TMP_Dropdown m_languageDropdown;
    [SerializeField] private List<string> m_availableLanguages = new();

    [Space]
    public ProjetoIV.RatInput.Input pause;
    public ProjetoIV.RatInput.Input unpause;

    private void Start()
    {
        pause = RatInput.Instance.GetInput(InputID.KITCHEN_PAUSE);
        unpause = RatInput.Instance.GetInput(InputID.MENU_UNPAUSE);
        pause.OnInputCanceled += ButtonPause;
        unpause.OnInputCanceled += ButtonBack;
    }

    private void OnDestroy()
    {
        pause.OnInputCanceled -= ButtonPause;
        unpause.OnInputCanceled -= ButtonBack;
    }

    private float m_volume;
    private float m_soundEffects;

    public void ButtonPause()
    {
        RatInput.Instance.SetMap(Map.MENU);
        Time.timeScale = 0f;
        m_gamePause.SetActive(true);
        m_languageDropdown.value = IdentifyLenguage();
    }

    public void ButtonBack()
    {
        RatInput.Instance.SetMap(Map.KITCHEN);
        Time.timeScale = 1f;
        m_gamePause.SetActive(false);
    }

    public void ButtonCredits()
    {
        m_gameMain.SetActive(false);
        m_gameCredits.SetActive(true);
    }

    public void ButtonOptions()
    {
        m_gameMain.SetActive(false);
        m_gameOptions.SetActive(true);
    }

    public void ButtonMain()
    {
        m_gameCredits.SetActive(false);
        m_gameOptions.SetActive(false);
        m_gameMain.SetActive(true);
    }

    public void ButtonMenu()
    {
        SceneLoader.Instance.Load(SceneLoader.Scene.SCN_Menu);
    }

    public void ButtonPlay()
    {
        SceneLoader.Instance.Load(SceneLoader.Scene.SCN_Game);
    }

    public void ButtonQuit()
    {
        Application.Quit();
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
