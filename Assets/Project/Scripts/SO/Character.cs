using UnityEngine;

namespace RatSpeak
{
    [CreateAssetMenu(fileName = "CFG_Character_T_Ch00_", menuName = "Scriptable Objects/Dialogs/Character")]
    public class Character : ScriptableObject
    {
        public string characterName;
        public CharacterType characterType;
        public CharacterId characterId;
        public Color dialogColor = Color.black;
        public float textSpeed = 10f;
    }

    public enum CharacterType
    {
        NONE,
        MAIN,
        SECONDARY,
        TERTIARY,
        GENERIC,
    }

    public enum CharacterId
    {
        NONE,
        CARLO,
        CRAZY,
        DOROTEA,
        INA,
        LEO
    }
}