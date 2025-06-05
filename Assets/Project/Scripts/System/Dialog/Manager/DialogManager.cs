using System;
using Fungus;
using NaughtyAttributes;
using ProjetoIV.Util;
using UnityEngine;

namespace RatSpeak
{
    public class DialogManager : Singleton<DialogManager>
    {
        public bool InDialog { get; private set; }
        [Header("Dialog")] public Flowchart flowchart;
        [SerializeField] private Fungus.Character m_fungusCharacter;
        private Block m_block;
        private const string BLOCK_NAME = "Fala";

        protected override void OnAwake()
        {
            base.OnAwake();
            m_block = flowchart.FindBlock(BLOCK_NAME);
        }

        private Say l_tempSay;

        public void ShowDialog(Dialog p_dialog, Action p_onComplete = null)
        {
            l_tempSay = (Say)m_block.CommandList[0];

            m_fungusCharacter.NameText = p_dialog.character.characterName;
            l_tempSay.Character = m_fungusCharacter;
            // l_tempSay.SetStandardText(p_dialog.dialogText);

            l_tempSay.Execute();

            // if (p_onComplete != null)
            // {
            // m_currentSayDialog.AddAction(p_onComplete);
            // }
        }

        public void ShowDialog(string p_blockName, Action p_onComplete = null)
        {
            flowchart.ExecuteIfHasBlock(p_blockName);
        }

        public Action onStartDialog;
        public Action onEndDialog;

        public void EnterDialog()
        {
            InDialog = true;
            CursorBehavior.Set(true, CursorLockMode.None);
            onStartDialog?.Invoke();
        }

        public void ExitDialog()
        {
            InDialog = false;
            CursorBehavior.Set(false, CursorLockMode.Locked);
            onEndDialog?.Invoke();
        }


        #region Debug

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private string m_debugShowDialogText;
        [SerializeField, ShowIf("m_debug")] private Dialog m_debugShowDialog;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog()
        {
            ShowDialog(m_debugShowDialogText);
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