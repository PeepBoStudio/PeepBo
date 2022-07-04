using UnityEngine;
using PeepBo.Utils;
using UnityEngine.EventSystems;
using PeepBo.Managers;
using System.Collections.Generic;
using UnityEngine.UI;
using Naninovel;

namespace PeepBo.UI.Popup
{
    public class UI_TutorialPopup : UI_Popup
    {
        enum GameObjects
        {
            TutorialWrapper,
            ExitButton,
        }

        private void Start()
            => Init();

        public override void Init()
        {
            base.Init();

            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = true;

            BindObjects();
        }

        public override void ClosePopupUI()
        {
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            base.ClosePopupUI();
        }

        public void OnClick(int index)
        {
            if (index == phaseList.Count - 1)
            {
                ClosePopupUI();
                return;
            }
            phaseList[curIdx.Value].SetActive(false);
            phaseList[index+1].SetActive(true);
            curIdx = index;
        }

        int? curIdx = null;
        List<GameObject> phaseList = new List<GameObject>();

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject exitButton = GetObject((int)GameObjects.ExitButton);
            AddUIEvent(exitButton, OnClickExitButton, Define.UIEvent.Click);

            GameObject tutorialWrapper = GetObject((int)GameObjects.TutorialWrapper);

            for (int i = 0; i < tutorialWrapper.transform.childCount; i++)
            {
                int index = i;
                phaseList.Add(tutorialWrapper.transform.GetChild(i).gameObject);
                var phase = phaseList[i];
                AddUIEvent(phase, (e) => OnClick(index), Define.UIEvent.Click);
            }

            curIdx = 0;


            //GameObject exitButton = GetObject((int)GameObjects.ExitButton);
            //AddUIEvent(exitButton, OnClickExitButton, Define.UIEvent.Click);
            //AddButtonAnim(exitButton);
        }

        private void OnClickExitButton(PointerEventData evt)
        {
            ClosePopupUI();
        }
    }
}
