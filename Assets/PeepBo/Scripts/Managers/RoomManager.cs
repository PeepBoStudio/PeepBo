using Naninovel;
using Naninovel.Commands;
using PeepBo.UI.Popup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeepBo.Managers
{
    public class RoomManager
    {
        public void Test()
        {
            //var switchCommand = new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = GameManager.Switch.Label };
            //switchCommand.ExecuteAsync().Forget();
        }

        public void StartRoomMode(StringParameter scriptName, StringParameter label, AsyncToken asyncToken = default)
        {
            // TODO : scriptName�� label�� � Ž���������� ��Ī



            // ��Ī�ߴٰ� ����
            ShowToastUI("������ �� ���� �͵��� ã�ƺ���.", asyncToken);
            HideUI(new List<string> { "RightTopUI" }, asyncToken);

            GameManager.UI.ShowPopupUI<UI_RoomModePopup>();
        }

        private void ShowToastUI(string toastText, AsyncToken asyncToken = default)
        {
            if (toastText == null) return;

            var testToast = new ShowToastUI { Text = toastText, Duration = 1 };
            testToast.ExecuteAsync(asyncToken).Forget();
        }

        private void HideUI(List<string> uiList, AsyncToken asyncToken = default)
        {
            if(uiList == null || uiList.Count == 0) return;

            var hideUI = new HideUI { UINames = uiList };
            hideUI.ExecuteAsync(asyncToken).Forget();
        }
    }
}
