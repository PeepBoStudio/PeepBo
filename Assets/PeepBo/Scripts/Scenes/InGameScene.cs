using PeepBo.Managers;
using PeepBo.UI.Scene;
using Naninovel;

namespace PeepBo.Scene
{ 
    public class InGameScene : BaseScene
    {
        private void Start()
                => Init();

        protected override void Init()
        {
            base.Init();
            InitNaniNovel();
            GameManager.UI.ShowSceneUI<UI_InGameScene>();

            if (Engine.Initialized) Start1_1();
            else Engine.OnInitializationFinished += Start1_1;
        }

        private async void Start1_1()
        {
            var player = Engine.GetService<IScriptPlayer>();
            await player.PreloadAndPlayAsync("Script206");
        }

        private async void InitNaniNovel()
        {
            await RuntimeInitializer.InitializeAsync();
        }
    }
}
