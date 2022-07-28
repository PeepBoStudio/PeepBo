using Naninovel;
using Naninovel.UI;
using PeepBo.Managers;
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
            uiManager.GetUI<ITitleUI>()?.Hide();
            ExitToTitleAsync();
        }

        private async void ExitToTitleAsync()
        {
            if (!await confirmationUI.ConfirmAsync(ConfirmationMessage)) return;
            gameState.OnResetFinished += GameState_OnResetFinished;

            await gameState.ResetStateAsync();
        }

        private void GameState_OnResetFinished()
        {
            GameManager.LoadMainSceneAndEpisodePopup();
        }
    }
}
