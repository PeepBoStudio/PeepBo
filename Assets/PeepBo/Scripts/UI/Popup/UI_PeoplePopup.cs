using UnityEngine;
using PeepBo.Utils;
using UnityEngine.EventSystems;
using PeepBo.Managers;
using UnityEngine.SceneManagement;
using Naninovel;
using System.Threading.Tasks;

namespace PeepBo.UI.Popup
{
    public class UI_PeoplePopup : UI_Popup
    {
        enum GameObjects
        {
            CloseButton,
            list,
            before,
            after,
        }
        private void Start()
                    => Init();

        public override void Init()
        {
            base.Init();
            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            BindObjects();
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject closeButton = GetObject((int)GameObjects.CloseButton);
            AddUIEvent(closeButton, OnClickCloseButton, Define.UIEvent.Click);
            AddButtonAnim(closeButton);

            GameObject tolist = GetObject((int)GameObjects.list);
            AddUIEvent(tolist, OnClickListButton, Define.UIEvent.Click);
            AddButtonAnim(tolist);

            GameObject tobefore = GetObject((int)GameObjects.before);
            AddUIEvent(tobefore, OnClickListButton, Define.UIEvent.Click);
            AddButtonAnim(tobefore);

            GameObject toafter = GetObject((int)GameObjects.after);
            AddUIEvent(toafter, OnClickListButton, Define.UIEvent.Click);
            AddButtonAnim(toafter);
        }

        private void OnClickListButton(PointerEventData evt)
        {
            ClosePopupUI();
        }
        private void OnClickCloseButton(PointerEventData evt)
        {
            CloseAllPopupUI();
        }
    }
}