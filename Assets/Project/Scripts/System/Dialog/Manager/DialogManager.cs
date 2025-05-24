using System;
using System.Collections.Generic;
using Fungus;
using NaughtyAttributes;
using ProjetoIV.Util;
using UnityEngine;

namespace RatSpeak
{
    public class DialogManager : Singleton<DialogManager>
    {
        [Header("Dialog")] public Flowchart flowchart;

        // [SerializeField] private SayDialog m_sayDialog;

        // [SerializeField] private List<CharacterReference> m_charReferences;
        [SerializeField] private Fungus.Character m_fungusCharacter;
        private Block m_block;
        private const string BLOCK_NAME = "Fala";

        protected override void OnAwake()
        {
            base.OnAwake();
            m_block = flowchart.FindBlock(BLOCK_NAME);
        }

        private Say l_tempSay;

        SayDialog m_currentSayDialog;

        public void ShowDialog(Dialog p_dialog, Action p_onComplete = null)
        {
            l_tempSay = (Say)m_block.CommandList[0];

            m_fungusCharacter.NameText = p_dialog.character.characterName;
            l_tempSay.Character = m_fungusCharacter;
            // l_tempSay.SetStandardText(p_dialog.dialogText);

            l_tempSay.Execute();

            m_currentSayDialog = SayDialog.ActiveSayDialog;

            // if (p_onComplete != null)
            // {
                // m_currentSayDialog.AddAction(p_onComplete);
            // }
        }

        #region Debug

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private string m_debugShowDialogText;
        [SerializeField, ShowIf("m_debug")] private Dialog m_debugShowDialog;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog()
        {
            ShowDialog(m_debugShowDialog, () => Debug.Log("BUMDA"));
        }

        #endregion
    }

    [Serializable]
    public struct CharacterReference
    {
        public CharacterID characterID;
        public Character characterConfig;
        public Fungus.Character character;
    }

    public enum CharacterID
    {
        NONE,
        PLAYER,
        INA,
        LEO,
        ETC
    }
}