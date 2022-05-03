using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeepBo.Nani.Custom
{
    public class CustomControlPanelTitleButton : ScriptableButton
    {
        [ManagedText("DefaultUI")]
        protected static string ConfirmationMessage = "에피소드에서 나가시겠습니까?";

        private IStateManager gameState;
        private IUIManager uiManager;
        private IConfirmationUI confirmationUI;

        protected override void Awake()
        {
            base.Awake();

            gameState = Engine.GetService<IStateManager>();
            uiManager = Engine.GetService<IUIManager>();
        }

        protected override void Start()
        {
            base.Start();

            confirmationUI = uiManager.GetUI<IConfirmationUI>();
        }

        protected override void OnButtonClick()
        {
            uiManager.GetUI<IPauseUI>()?.Hide();

            ExitToTitleAsync();
        }

        private async void ExitToTitleAsync()
        {
            if (!await confirmationUI.ConfirmAsync(ConfirmationMessage)) return;
            await gameState.ResetStateAsync();
            uiManager.GetUI<ITitleUI>()?.Show();

            SceneManager.LoadScene("MainScene");
        }
    }
}
