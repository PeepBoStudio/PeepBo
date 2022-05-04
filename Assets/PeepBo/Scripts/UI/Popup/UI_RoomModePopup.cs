using UnityEngine;
using PeepBo.Utils;
using UnityEngine.EventSystems;

namespace PeepBo.UI.Popup
{
    public class UI_RoomModePopup : UI_Popup
    {
        enum GameObjects
        {
            ExitButton,
            LeftButton,
            RightButton,
        }

        private void Start()
            => Init();

        public override void Init()
        {
            base.Init();

            BindObjects();
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject exitButton = GetObject((int)GameObjects.ExitButton);
            AddUIEvent(exitButton, OnClickExitButton, Define.UIEvent.Click);
            //AddButtonAnim(exitButton);
        }

        private void OnClickExitButton(PointerEventData evt)
        {
            Debug.Log("Click");
            // TODO : 탐색 종료 조건 확인하기
        }
    }
}
