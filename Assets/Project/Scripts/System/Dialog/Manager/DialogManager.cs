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

        // private Fungus.Character GetFungusCharacter(Character p_character)
        // {
        //     for (int i = 0; i < m_charReferences.Count; i++)
        //     {
        //         if (m_charReferences[i].characterConfig == p_character)
        //         {
        //             return m_charReferences[i].character;
        //         }
        //     }
        //
        //     return null;
        // }

        public void ShowDialog(Dialog p_dialog)
        {
            l_tempSay = (Say)m_block.CommandList[0];
            // Fungus.Character character = GetFungusCharacter(p_dialog.character);

            m_fungusCharacter.NameText = p_dialog.character.characterName;
            l_tempSay.Character = m_fungusCharacter;
            l_tempSay.SetStandardText(p_dialog.dialogText);

            l_tempSay.Execute();
        }

        #region Debug

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private string m_debugShowDialogText;
        [SerializeField, ShowIf("m_debug")] private Dialog m_debugShowDialog;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog()
        {
            ShowDialog(m_debugShowDialog);
        }

        // [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        // private void DebugHideDialog()
        // {
        // m_sayDialog.SetActive(false);
        // }

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