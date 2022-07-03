// Copyright 2017-2021 Elringus (Artyom Sovetnikov). All rights reserved.

using Naninovel;
using PeepBo.Managers;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PeepBo.Nani.Custom
{
    [RequireComponent(typeof(Button))]
    public class ChoiceHandlerButton : ScriptableButton
    {
        [Serializable]
        private class SummaryTextChangedEvent : UnityEvent<string> { }

        /// <summary>
        /// Invoked when the choice summary text is changed.
        /// </summary>
        public event Action<string> OnSummaryTextChanged;

        public ChoiceState ChoiceState { get; private set; }

        [Tooltip("Invoked when the choice summary text is changed.")]
        [SerializeField] private SummaryTextChangedEvent onSummaryTextChanged = default;

        public virtual void Initialize (ChoiceState choiceState)
        {
            ChoiceState = choiceState;

            OnSummaryTextChanged?.Invoke(choiceState.Summary);
            onSummaryTextChanged?.Invoke(choiceState.Summary);
        }

        public void OnPress()
        {
            Debug.Log("PRESS");
            GetComponentInChildren<Text>().color = Color.black;
        }

        public void DePress()
        {
            Debug.Log("DEPRESS");
            GetComponentInChildren<Text>().color = Color.white;
        }

        public void OnClick()
        {
            Debug.Log("Click");
            //OnButtonClick();
        }

        protected override void OnButtonClick() // 호감도용, hogam
        {
            /*
            Debug.Log("Clicked2");
            base.OnButtonClick();

            string text = GetComponentInChildren<Text>().text;
            var cameraManager = Engine.GetService<ICameraManager>();
            Vector3 screenPos = cameraManager.Camera.WorldToScreenPoint(transform.position);

            var rectTransform = GetComponent<RectTransform>();
            Vector2 size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);

            GameManager.Hogam.OnClickChoiceButton(text, screenPos, size);
            */
        }
    }
}
