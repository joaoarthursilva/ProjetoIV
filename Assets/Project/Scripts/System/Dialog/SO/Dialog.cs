using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace RatSpeak
{
    [CreateAssetMenu(fileName = "CFG_Dialog_St00_Ch00_Gr00_Di00", menuName = "Scriptable Objects/Dialogs/Dialog")]
    public class Dialog : ScriptableObject
    {
        public DialogType dialogType = DialogType.NPC;
        public Character character;

        [ShowIf("dialogType", DialogType.PLAYER)]
        public string dialogChoicePreviewText;

        public string dialogText;
        public List<UnityEvent> dialogEvents;
    }

    public enum DialogType
    {
        NONE,
        PLAYER,
        NPC,
    }
}