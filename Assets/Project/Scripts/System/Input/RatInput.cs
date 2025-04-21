using System.Collections.Generic;
using ProjetoIV.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjetoIV.RatInput
{
    public class RatInput : Singleton<RatInput>
    {
        public Map CurrentMap;
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private InputActionAsset m_actionAsset;
        [SerializeField] private List<RatInputAction> m_inputs;

        private void Start()
        {
            SetMap("Kitchen");
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        public Input GetInput(InputID p_inputID)
        {
            for (int i = 0; i < m_inputs.Count; i++)
            {
                RatInputAction input = m_inputs[i];
                if (input.InputID == p_inputID)
                {
                    return input.Input;
                }
            }

            Debug.LogError($"Input with ID {p_inputID} not found.");
            return null;
        }

        [field: SerializeField] public Vector2 Movement { get; private set; }
        [field: SerializeField] public Vector2 LookInput { get; private set; }

        public void OnKitchenLook(InputAction.CallbackContext p_context)
        {
            LookInput = p_context.ReadValue<Vector2>();
        }

        public void OnKitchenMovement(InputAction.CallbackContext p_context)
        {
            Movement = p_context.ReadValue<Vector2>();
        }

        public void SetMap(string p_map)
        {
            switch (p_map)
            {
                case "Kitchen":
                    CurrentMap = Map.KITCHEN;
                    break;
                case "Minigame":
                    CurrentMap = Map.MINIGAME;
                    break;
                case "Dialog":
                    CurrentMap = Map.DIALOG;
                    break;
                default:
                    CurrentMap = Map.KITCHEN;
                    break;
            }

            for (int i = 0; i < m_actionAsset.actionMaps.Count; i++)
            {
                if (m_actionAsset.actionMaps[i].name.Equals(p_map)) m_actionAsset.actionMaps[i].Enable();
                else m_actionAsset.actionMaps[i].Disable();
            }
        }

        public static System.Action<InputID> ShowInputUIElement;

        public void ShowUIElement(InputID p_inputID)
        {
            ShowInputUIElement?.Invoke(p_inputID);
        }
    }

    public enum Map
    {
        NONE,
        MENU,
        KITCHEN,
        BOOK,
        MINIGAME,
        DIALOG,
    }

    public enum InputID
    {
        NONE,
        KITCHEN_MOVE,
        KITCHEN_LOOK,
        KITCHEN_INTERACT,
        MINIGAME_CUT,
        MINIGAME_ENDINTERACTION
    }
}