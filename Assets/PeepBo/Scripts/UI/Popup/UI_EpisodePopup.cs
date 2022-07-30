using UnityEngine;
using PeepBo.Utils;
using UnityEngine.EventSystems;
using PeepBo.Managers;
using UnityEngine.SceneManagement;
using Naninovel;
using System.Threading.Tasks;

namespace PeepBo.UI.Popup
{
    public class UI_EpisodePopup : UI_Popup
    {
        enum GameObjects
        {
            CloseButton,
            Dummy0,
            Dummy1,
            Dummy2,
            Dummy3,
            Dummy4,
            Dummy5,
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

            GameObject dummy0 = GetObject((int)GameObjects.Dummy0);
            AddUIEvent(dummy0, (a) =>
            {
                GameManager.DummyEpisode = "000";
                SceneManager.LoadScene("InGameScene");
            }, Define.UIEvent.Click);
            AddButtonAnim(dummy0);

            GameObject dummy1 = GetObject((int)GameObjects.Dummy1);
            AddUIEvent(dummy1, (a) => 
            { 
                GameManager.DummyEpisode = "101"; 
                SceneManager.LoadScene("InGameScene"); }, Define.UIEvent.Click);
            AddButtonAnim(dummy1);

            GameObject dummy2 = GetObject((int)GameObjects.Dummy2);
            AddUIEvent(dummy2, (a) =>
            {
                GameManager.DummyEpisode = "102";
                SceneManager.LoadScene("InGameScene");
            }, Define.UIEvent.Click);
            AddButtonAnim(dummy2);

            GameObject dummy3 = GetObject((int)GameObjects.Dummy3);
            AddUIEvent(dummy3, (a) =>
            {
                GameManager.DummyEpisode = "108";
                SceneManager.LoadScene("InGameScene");
            }, Define.UIEvent.Click);
            AddButtonAnim(dummy3);

            GameObject dummy4 = GetObject((int)GameObjects.Dummy4);
            AddUIEvent(dummy4, (a) =>
            {
                GameManager.DummyEpisode = "110";
                SceneManager.LoadScene("InGameScene");
            }, Define.UIEvent.Click);
            AddButtonAnim(dummy4);

            GameObject dummy5 = GetObject((int)GameObjects.Dummy5);
            AddUIEvent(dummy5, (a) =>
            {
                GameManager.DummyEpisode = "206";
                SceneManager.LoadScene("InGameScene");
            }, Define.UIEvent.Click);
            AddButtonAnim(dummy5);
            //SceneManager.LoadScene("InGameScene");
        }

        private void OnClickCloseButton(PointerEventData evt)
        {
            ClosePopupUI();
        }
    }
}
