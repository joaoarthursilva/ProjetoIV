using System.Collections.Generic;
using Fungus;
using NaughtyAttributes;
using ProjetoIV.Util;
using UnityEngine;

namespace RatSpeak
{
    public class DialogManager : Singleton<DialogManager>
    {
        [Header("Dialog")]
        public Flowchart flowchart;

        [SerializeField] private SayDialog m_sayDialog;
        [SerializeField] private Fungus.Character m_testCharacter;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        #region Debug

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private string m_debugShowDialogText;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog()
        {
            m_sayDialog.SetActive(true);
            m_sayDialog.SetCharacter(m_testCharacter);
            m_sayDialog.Say(m_debugShowDialogText, true, true, false, false, false, null, null);
        }

        // [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        // private void DebugShowOptions() => ShowOptions(m_debugDialogOptions);

        #endregion
    }
}