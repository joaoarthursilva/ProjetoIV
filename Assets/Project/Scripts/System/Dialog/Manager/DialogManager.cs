using System.Collections.Generic;
using NaughtyAttributes;
using ProjetoIV.Util;
using UnityEngine;

namespace RatSpeak
{
    public class DialogManager : Singleton<DialogManager>
    {
        [Header("Dialog")]
        public UIPanelDialog uiPanelDialog;

        public void Show(Dialog p_dialog)
        {
            uiPanelDialog.Show(p_dialog);
        }

        public void ShowOptions(List<Dialog> p_dialog)
        {
            uiPanelDialog.ShowOptions(p_dialog);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        public void Hide()
        {
            uiPanelDialog.Hide();
        }

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private List<Dialog> m_debugDialogOptions;
        [SerializeField, ShowIf("m_debug")] private Dialog m_debugShowDialog;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog() => Show(m_debugShowDialog);

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowOptions() => ShowOptions(m_debugDialogOptions);
    }
}