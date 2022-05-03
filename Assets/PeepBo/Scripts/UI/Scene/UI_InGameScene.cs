using Naninovel;
using PeepBo.Managers;
using PeepBo.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PeepBo.UI.Scene
{
    public class UI_InGameScene : UI_Scene
    {
        enum GameObjects
        {
            //Test,
        }
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
            //GameObject startButton = GetObject((int)GameObjects.Test);
            //AddUIEvent(startButton, OnClickStartButton, Define.UIEvent.Click);
            //AddButtonAnim(startButton);
        }
        
        private void OnClickStartButton(PointerEventData evt)
        {
            var switchCommand = new SwitchToNovelMode { ScriptName = "Script001", Label = "myLabel" };
            switchCommand.ExecuteAsync().Forget();
        }
    }
}