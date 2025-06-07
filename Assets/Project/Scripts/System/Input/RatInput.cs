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
            SetMap(m_playerInput.defaultActionMap);
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

        [HideInInspector] public Vector2 Movement;
        [HideInInspector] public Vector2 LookInput;

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
                case "Book":
                    CurrentMap = Map.BOOK;
                    break;
                case "Menu":
                    CurrentMap = Map.MENU;
                    break;
                default:
                    CurrentMap = Map.KITCHEN;
                    break;
            }

            m_playerInput.SwitchCurrentActionMap(p_map);

            // for (int i = 0; i < m_actionAsset.actionMaps.Count; i++)
            // {
            //     if (m_actionAsset.actionMaps[i].name.Equals(p_map)) m_actionAsset.actionMaps[i].Enable();
            //     else m_actionAsset.actionMaps[i].Disable();
            // }
        }

        public void SetMap(Map p_map)
        {
            CurrentMap = p_map;
            SetMap(GetMap(p_map));
            // for (int i = 0; i < m_actionAsset.actionMaps.Count; i++)
            // {
            //     if (m_actionAsset.actionMaps[i].name.ToLower().Equals(p_map.ToString().ToLower()))
            //         m_actionAsset.actionMaps[i].Enable();
            //     else m_actionAsset.actionMaps[i].Disable();
            // }
        }

        private string GetMap(Map p_map)
        {
            switch (p_map)
            {
                case Map.KITCHEN:
                    return "Kitchen";
                case Map.MINIGAME:
                    return "Minigame";
                case Map.BOOK:
                    return "Book";
                case Map.MENU:
                    return "Menu";
            }

            return "";
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