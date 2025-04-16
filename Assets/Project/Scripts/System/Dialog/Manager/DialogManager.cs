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
        [Header("Dialog")]
        public Flowchart flowchart;

        // [SerializeField] private SayDialog m_sayDialog;
        [SerializeField] private List<DialogReference> m_dialogReferences;
        // [SerializeField] private Fungus.Character m_testCharacter;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        public void ShowDialog(DialogID p_dialogID)
        {
            for (int i = 0; i < m_dialogReferences.Count; i++)
            {
                if (m_dialogReferences[i].dialogID == p_dialogID)
                {
                    flowchart.ExecuteIfHasBlock(m_dialogReferences[i].blockName);
                    return;
                }
            }

            Debug.LogError($"Dialog ID {p_dialogID} not found.");
        }

        #region Debug

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private string m_debugShowDialogText;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog()
        {
            flowchart.ExecuteIfHasBlock(m_debugShowDialogText);
        }

        #endregion
    }

    public enum DialogID
    {
        NONE,
        PEDIDO,
        ENTREGA,
        RESULTADO_RUIM,
        RESULTADO_BOM,
    }

    [Serializable]
    public struct DialogReference
    {
        public DialogID dialogID;
        public string blockName;
    }
}