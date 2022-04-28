using Naninovel;
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
        }

        GameObject startButton;

        private void Start()
            => Init();

        public override void Init()
        {
            base.Init();
            Bind<GameObject>(typeof(GameObjects));
            BindObjects();
        }

        private void BindObjects()
        {
            startButton = GetObject((int)GameObjects.StartButton);
            AddUIEvent(startButton, OnClickStartButton, Define.UIEvent.Click);
            AddButtonAnim(startButton);
        }

        private void OnClickStartButton(PointerEventData evt)
        {
            SceneManager.LoadScene("InGameScene");
        }
    }
}