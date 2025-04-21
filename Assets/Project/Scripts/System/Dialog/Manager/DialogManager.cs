using System;
using System.Collections.Generic;
using Fungus;
using NaughtyAttributes;
using ProjetoIV.RatInput;
using ProjetoIV.Util;
using Unity.Cinemachine;
using UnityEngine;

namespace RatSpeak
{
    public class DialogManager : Singleton<DialogManager>
    {
        [Header("Dialog")] public Flowchart flowchart;
        [SerializeField] private CinemachineCamera m_camera;
        [SerializeField] private List<DialogReference> m_dialogReferences;
        public bool IsInDialog { get; private set; }

        [SerializeField] private DialogID m_currentDialogID = DialogID.PEDIDO;
        public CutIngredientMinigame cutIngredientMinigame;

        protected override void OnAwake()
        {
            base.OnAwake();
            cutIngredientMinigame.OnEndCutMinigame += () => SetCurrentDialog(DialogID.ENTREGA);
        }

        public void EnterDialog()
        {
            if (IsInDialog) return;
            CameraManager.Instance.SetCameraToCurrent(m_camera);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            RatInput.Instance.SetMap("Menu");
            IsInDialog = true;
            ShowDialog(m_currentDialogID);
        }

        public void ExitDialog()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            IsInDialog = false;
            CameraManager.Instance.SetCameraToCurrent(null);
            RatInput.Instance.SetMap("Kitchen");
        }

        public void SetCurrentDialog(DialogID p_dialogID)
        {
            m_currentDialogID = p_dialogID;
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
        ESPERA,
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