using UnityEngine;
using PeepBo.Managers;
using PeepBo.UI.Scene;

namespace PeepBo.Scene
{ 
    public class MainScene : BaseScene
    {
        private void Start()
                => Init();

        protected override void Init()
        {
            base.Init();
            GameManager.UI.ShowSceneUI<UI_MainScene>();
        }
    }
}
