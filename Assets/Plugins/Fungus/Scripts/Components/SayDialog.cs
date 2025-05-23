// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Fungus.Lua;
using Fungus.DentedPixel;
using UnityEngine.Serialization;

namespace Fungus
{
    /// <summary>
    /// Display story text in a visual novel style dialog box.
    /// </summary>
    public class SayDialog : MonoBehaviour
    {
        [Tooltip("Duration to fade dialogue in/out")] [SerializeField]
        private float fadeDuration = 0.25f;

        [Tooltip("The continue button UI object")] [SerializeField]
        private Button continueButton;

        [Tooltip("The canvas UI object")] [SerializeField]
        private Canvas dialogCanvas;

        [Tooltip("TextAdapter will search for appropriate output on this GameObject if nameText is null")]
        [SerializeField]
        private GameObject nameTextGO;

        protected TextAdapter nameTextAdapter = new TextAdapter();

        public virtual string NameText
        {
            get { return nameTextAdapter.Text; }
            set { nameTextAdapter.Text = value; }
        }

        [Tooltip("TextAdapter will search for appropriate output on this GameObject if storyText is null")]
        [SerializeField]
        private GameObject storyTextGO;

        protected TextAdapter storyTextAdapter = new TextAdapter();

        public virtual string StoryText
        {
            get { return storyTextAdapter.Text; }
            set { storyTextAdapter.Text = value; }
        }

        public virtual RectTransform StoryTextRectTrans
        {
            get { return storyTextGO.transform as RectTransform; }
        }

        [Tooltip("The character UI object")] [SerializeField]
        private Image characterImage;

        public virtual Image CharacterImage
        {
            get { return characterImage; }
        }

        [Tooltip("Adjust width of story text when Character Image is displayed (to avoid overlapping)")]
        [SerializeField]
        private bool fitTextWithImage = true;

        [Tooltip("Close any other open Say Dialogs when this one is active")] [SerializeField]
        private bool closeOtherDialogs;

        private float startStoryTextWidth;
        private float startStoryTextInset;

        [SerializeField] private WriterAudio m_writerAudio;
        [SerializeField] private Writer m_writer;
        [SerializeField] private CanvasGroup m_canvasGroup;

        private bool fadeWhenDone = true;
        private float targetAlpha = 0f;
        private float fadeCoolDownTimer = 0f;

        private Sprite currentCharacterImage;

        // Most recent speaking character
        private static Character speakingCharacter;

        private StringSubstituter stringSubstituter = new StringSubstituter();

        // Cache active Say Dialogs to avoid expensive scene search
        private static List<SayDialog> activeSayDialogs = new List<SayDialog>();

        protected virtual void Awake()
        {
            if (!activeSayDialogs.Contains(this))
            {
                activeSayDialogs.Add(this);
            }

            nameTextAdapter.InitFromGameObject(nameTextGO);
            storyTextAdapter.InitFromGameObject(storyTextGO);
        }

        private Action OnDisableAction;

        public void AddAction(Action p_action)
        {
            if (OnDisableAction == null) OnDisableAction = p_action;
        }

        private void OnDisable()
        {
            OnDisableAction?.Invoke();
            OnDisableAction = null;
        }

        protected virtual void OnDestroy()
        {
            activeSayDialogs.Remove(this);
        }

        protected virtual void Start()
        {
            // Dialog always starts invisible, will be faded in when writing starts
            m_canvasGroup.alpha = 0f;

            // Add a raycaster if none already exists so we can handle dialog input
            GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
            if (raycaster == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }

            // It's possible that SetCharacterImage() has already been called from the
            // Start method of another component, so check that no image has been set yet.
            // Same for nameText.

            if (NameText == "")
            {
                SetCharacterName("", Color.white);
            }

            if (currentCharacterImage == null)
            {
                // Character image is hidden by default.
                SetCharacterImage(null);
            }
        }

        protected virtual void LateUpdate()
        {
            UpdateAlpha();

            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(m_writer.IsWaitingForInput);
            }
        }

        protected virtual void UpdateAlpha()
        {
            if (m_writer.IsWriting)
            {
                targetAlpha = 1f;
                fadeCoolDownTimer = 0.1f;
            }
            else if (fadeWhenDone && Mathf.Approximately(fadeCoolDownTimer, 0f))
            {
                targetAlpha = 0f;
            }
            else
            {
                // Add a short delay before we start fading in case there's another Say command in the next frame or two.
                // This avoids a noticeable flicker between consecutive Say commands.
                fadeCoolDownTimer = Mathf.Max(0f, fadeCoolDownTimer - Time.deltaTime);
            }

            if (fadeDuration <= 0f)
            {
                m_canvasGroup.alpha = targetAlpha;
            }
            else
            {
                float delta = (1f / fadeDuration) * Time.deltaTime;
                float alpha = Mathf.MoveTowards(m_canvasGroup.alpha, targetAlpha, delta);
                m_canvasGroup.alpha = alpha;

                if (alpha <= 0f)
                {
                    // Deactivate dialog object once invisible
                    gameObject.SetActive(false);
                }
            }
        }

        protected virtual void ClearStoryText()
        {
            StoryText = "";
        }

        #region Public members

        public Character SpeakingCharacter
        {
            get { return speakingCharacter; }
        }

        /// <summary>
        /// Currently active Say Dialog used to display Say text
        /// </summary>
        public static SayDialog ActiveSayDialog { get; set; }

        /// <summary>
        /// Returns a SayDialog by searching for one in the scene or creating one if none exists.
        /// </summary>
        public static SayDialog GetSayDialog()
        {
            if (ActiveSayDialog == null)
            {
                SayDialog sd = null;

                // Use first active Say Dialog in the scene (if any)
                if (activeSayDialogs.Count > 0)
                {
                    sd = activeSayDialogs[0];
                }

                if (sd != null)
                {
                    ActiveSayDialog = sd;
                }

                if (ActiveSayDialog == null)
                {
                    // Auto spawn a say dialog object from the prefab
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/SayDialog");
                    if (prefab != null)
                    {
                        GameObject go = Instantiate(prefab) as GameObject;
                        go.SetActive(false);
                        go.name = "SayDialog";
                        ActiveSayDialog = go.GetComponent<SayDialog>();
                    }
                }
            }

            return ActiveSayDialog;
        }

        /// <summary>
        /// Stops all active portrait tweens.
        /// </summary>
        public static void StopPortraitTweens()
        {
            // Stop all tweening portraits
            var activeCharacters = Character.ActiveCharacters;
            for (int i = 0; i < activeCharacters.Count; i++)
            {
                var c = activeCharacters[i];
                if (c.State.portraitImage != null)
                {
                    if (LeanTween.isTweening(c.State.portraitImage.gameObject))
                    {
                        LeanTween.cancel(c.State.portraitImage.gameObject, true);
                        PortraitController.SetRectTransform(c.State.portraitImage.rectTransform, c.State.position);
                        if (c.State.dimmed == true)
                        {
                            c.State.portraitImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        }
                        else
                        {
                            c.State.portraitImage.color = Color.white;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the active state of the Say Dialog gameobject.
        /// </summary>
        public virtual void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        /// <summary>
        /// Sets the active speaking character.
        /// </summary>
        /// <param name="character">The active speaking character.</param>
        public virtual void SetCharacter(Character character)
        {
            if (character == null)
            {
                if (characterImage != null)
                {
                    characterImage.gameObject.SetActive(false);
                }

                if (NameText != null)
                {
                    NameText = "";
                }

                speakingCharacter = null;
            }
            else
            {
                var prevSpeakingCharacter = speakingCharacter;
                speakingCharacter = character;

                // Dim portraits of non-speaking characters
                var activeStages = Stage.ActiveStages;
                for (int i = 0; i < activeStages.Count; i++)
                {
                    var stage = activeStages[i];
                    if (stage.DimPortraits)
                    {
                        var charactersOnStage = stage.CharactersOnStage;
                        for (int j = 0; j < charactersOnStage.Count; j++)
                        {
                            var c = charactersOnStage[j];
                            if (prevSpeakingCharacter != speakingCharacter)
                            {
                                if (c != null && !c.Equals(speakingCharacter))
                                {
                                    stage.SetDimmed(c, true);
                                }
                                else
                                {
                                    stage.SetDimmed(c, false);
                                }
                            }
                        }
                    }
                }

                string characterName = character.NameText;

                if (characterName == "")
                {
                    // Use game object name as default
                    characterName = character.GetObjectName();
                }

                SetCharacterName(characterName, character.NameColor);
            }
        }

        /// <summary>
        /// Sets the character image to display on the Say Dialog.
        /// </summary>
        public virtual void SetCharacterImage(Sprite image)
        {
            if (characterImage == null)
            {
                return;
            }

            if (image != null)
            {
                characterImage.overrideSprite = image;
                characterImage.gameObject.SetActive(true);
                currentCharacterImage = image;
            }
            else
            {
                characterImage.gameObject.SetActive(false);

                if (startStoryTextWidth != 0)
                {
                    StoryTextRectTrans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,
                        startStoryTextInset,
                        startStoryTextWidth);
                }
            }

            // Adjust story text box to not overlap image rect
            if (fitTextWithImage &&
                StoryText != null &&
                characterImage.gameObject.activeSelf)
            {
                if (Mathf.Approximately(startStoryTextWidth, 0f))
                {
                    startStoryTextWidth = StoryTextRectTrans.rect.width;
                    startStoryTextInset = StoryTextRectTrans.offsetMin.x;
                }

                // Clamp story text to left or right depending on relative position of the character image
                if (StoryTextRectTrans.position.x < characterImage.rectTransform.position.x)
                {
                    StoryTextRectTrans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,
                        startStoryTextInset,
                        startStoryTextWidth - characterImage.rectTransform.rect.width);
                }
                else
                {
                    StoryTextRectTrans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right,
                        startStoryTextInset,
                        startStoryTextWidth - characterImage.rectTransform.rect.width);
                }
            }
        }

        /// <summary>
        /// Sets the character name to display on the Say Dialog.
        /// Supports variable substitution e.g. John {$surname}
        /// </summary>
        public virtual void SetCharacterName(string name, Color color)
        {
            if (NameText != null)
            {
                var subbedName = stringSubstituter.SubstituteStrings(name);
                NameText = subbedName;
                nameTextAdapter.SetTextColor(color);
            }
        }

        /// <summary>
        /// Write a line of story text to the Say Dialog. Starts coroutine automatically.
        /// </summary>
        /// <param name="p_text">The text to display.</param>
        /// <param name="p_clearPrevious">Clear any previous text in the Say Dialog.</param>
        /// <param name="p_waitForInput">Wait for player input before continuing once text is written.</param>
        /// <param name="p_shouldFadeWhenDone">Fade out the Say Dialog when writing and player input has finished.</param>
        /// <param name="p_stopVoiceover">Stop any existing voiceover audio before writing starts.</param>
        /// <param name="p_voiceOverClip">Voice over audio clip to play.</param>
        /// <param name="p_onComplete">Callback to execute when writing and player input have finished.</param>
        public virtual void Say(string p_text, bool p_clearPrevious, bool p_waitForInput, bool p_shouldFadeWhenDone,
            bool p_stopVoiceover, bool p_waitForVo, AudioClip p_voiceOverClip, Action p_onComplete)
        {
            gameObject.SetActive(true);
            StartCoroutine(DoSay(p_text, p_clearPrevious, p_waitForInput, p_shouldFadeWhenDone, p_stopVoiceover,
                p_waitForVo,
                p_voiceOverClip, p_onComplete));
        }

        /// <summary>
        /// Write a line of story text to the Say Dialog. Must be started as a coroutine.
        /// </summary>
        /// <param name="p_text">The text to display.</param>
        /// <param name="p_clearPrevious">Clear any previous text in the Say Dialog.</param>
        /// <param name="p_waitForInput">Wait for player input before continuing once text is written.</param>
        /// <param name="p_fadeWhenDone">Fade out the Say Dialog when writing and player input has finished.</param>
        /// <param name="p_stopVoiceover">Stop any existing voiceover audio before writing starts.</param>
        /// <param name="p_waitForVo">Wait for voiceover audio.</param>
        /// <param name="p_voiceOverClip">Voice over audio clip to play.</param>
        /// <param name="p_onComplete">Callback to execute when writing and player input have finished.</param>
        public virtual IEnumerator DoSay(string p_text, bool p_clearPrevious, bool p_waitForInput, bool p_fadeWhenDone,
            bool p_stopVoiceover, bool p_waitForVo, AudioClip p_voiceOverClip, Action p_onComplete)
        {
            if (m_writer.IsWriting || m_writer.IsWaitingForInput)
            {
                m_writer.Stop();
                while (m_writer.IsWriting || m_writer.IsWaitingForInput)
                {
                    yield return null;
                }
            }

            if (closeOtherDialogs)
            {
                for (int i = 0; i < activeSayDialogs.Count; i++)
                {
                    var sd = activeSayDialogs[i];
                    if (sd.gameObject != gameObject)
                    {
                        sd.SetActive(false);
                    }
                }
            }


            this.fadeWhenDone = p_fadeWhenDone;

            // Voice over clip takes precedence over a character sound effect if provided

            AudioClip soundEffectClip = null;
            if (p_voiceOverClip != null)
            {
                m_writerAudio.OnVoiceover(p_voiceOverClip);
            }
            else if (speakingCharacter != null)
            {
                soundEffectClip = speakingCharacter.SoundEffect;
            }

            m_writer.AttachedWriterAudio = m_writerAudio;

            yield return StartCoroutine(m_writer.Write(p_text, p_clearPrevious, p_waitForInput, p_stopVoiceover,
                p_waitForVo,
                soundEffectClip, p_onComplete));
        }

        /// <summary>
        /// Tell the Say Dialog to fade out once writing and player input have finished.
        /// </summary>
        public virtual bool FadeWhenDone
        {
            get { return fadeWhenDone; }
            set { fadeWhenDone = value; }
        }

        /// <summary>
        /// Stop the Say Dialog while its writing text.
        /// </summary>
        public virtual void Stop()
        {
            fadeWhenDone = true;
            m_writer.Stop();
        }

        /// <summary>
        /// Stops writing text and clears the Say Dialog.
        /// </summary>
        public virtual void Clear()
        {
            ClearStoryText();

            // Kill any active write coroutine
            StopAllCoroutines();
        }

        #endregion
    }
}