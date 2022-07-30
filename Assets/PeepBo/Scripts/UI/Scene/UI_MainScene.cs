using Naninovel;
using PeepBo.Managers;
using PeepBo.UI.Popup;
using PeepBo.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PeepBo.UI.Scene
{
    public class UI_MainScene : UI_Scene
    {
        enum GameObjects
        {
            StartButton,
            Diary,
        }

        GameObject startButton;
        GameObject diaryButton;

        private void Start()
            => Init();

        public override void Init()
        {
            base.Init();
            Bind<GameObject>(typeof(GameObjects));
            BindObjects();

            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
        }

        private void BindObjects()
        {
            startButton = GetObject((int)GameObjects.StartButton);
            AddUIEvent(startButton, OnClickStartButton, Define.UIEvent.Click);
            AddButtonAnim(startButton);

            diaryButton = GetObject((int)GameObjects.Diary);
            AddUIEvent(diaryButton, OnClickDiaryButton, Define.UIEvent.Click);
            AddButtonAnim(diaryButton);
        }

        private void OnClickStartButton(PointerEventData evt)
        {
            GameManager.UI.ShowPopupUI<UI_EpisodePopup>();
        }

        private void OnClickDiaryButton(PointerEventData evt)
        {
            GameManager.UI.ShowPopupUI<UI_DiaryListPopup>();
        }
    }
}