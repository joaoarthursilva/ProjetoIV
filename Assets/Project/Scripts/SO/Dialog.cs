using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace RatSpeak
{
    [CreateAssetMenu(fileName = "CFG_Dialog_", menuName = "Scriptable Objects/Dialogs/Dialog")]
    public class Dialog : ScriptableObject
    {
        // public DialogType dialogType = DialogType.NPC;
        public DialogID dialogID;
        public Character character;

        public string dialogText;
        // public List<UnityEvent> dialogEvents;
    }

    public enum DialogType
    {
        NONE,
        PLAYER,
        NPC,
    }

    public enum DialogID
    {
        NONE,
        PEDIDO,
        ESPERA,
        ENTREGA,
        RESULTADO_RUIM,
        RESULTADO_BOM,
    }
}