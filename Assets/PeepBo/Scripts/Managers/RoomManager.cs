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
        // TODO : Serialization, json 구조화
        public Dictionary<string, Dictionary<string, (string, bool)>> roomInteractionDict = new Dictionary<string, Dictionary<string, (string, bool)>>();

        public RoomManager()
        {
            roomInteractionDict.Add(
                "1-1", new Dictionary<string, (string, bool)> 
                { 
                    {"동백꽃", ("Dongbaek", false) },
                    {"담벼락", ("Wall", true) },
                    {"야생화", ("Flower", false) },
                    {"집", ("House", false) },
                    {"지붕", ("Roof", true) }
                }
            );
        }

        public bool IsScriptPlaying { get; set; } = true;
        public bool isEnd = false;


        public void Test()
        {
            //var switchCommand = new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = GameManager.Switch.Label };
            //switchCommand.ExecuteAsync().Forget();
        }

        public void StartRoomMode(StringParameter scriptName, StringParameter label, AsyncToken asyncToken = default)
        {
            // TODO : scriptName과 label로 어떤 탐색내용인지 매칭



            // 매칭했다고 가정
            //ShowToastUI("도움이 될 만한 것들을 찾아보자.", asyncToken);
            HideUI(new List<string> { "RightTopUI" }, asyncToken);
            ShowUI(new List<string> { "RoomModeUI" }, asyncToken);
            //GameManager.UI.ShowPopupUI<UI_RoomModePopup>();

            IsScriptPlaying = false;
        }

        private void ExitRoomMode()
        {
            // TODO : StateLess하게 유지
            IsScriptPlaying = true;
            isEnd = false;

            //ShowUI(new List<string>)

            GameManager.UI.ClosePopupUI();
            ShowUI(new List<string> { "RightTopUI" });
            HideUI(new List<string> { "RoomModeUI" });

        }

        public void RoomInteractionOccured(string roomName, string interactionName)
        {
            if (IsScriptPlaying)
                return;

            switch (interactionName)
            {
                case "야생화":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Flower" }.ExecuteAsync().Forget();
                    break;
                case "지붕":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Roof" }.ExecuteAsync().Forget();
                    break;
                case "집":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "House" }.ExecuteAsync().Forget();
                    break;
                case "동백꽃":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "DongBaek" }.ExecuteAsync().Forget();
                    break;
                case "담벼락":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Wall" }.ExecuteAsync().Forget();
                    break;
            }
        }

        public void RoomExitButtonCliked()
        {
            if(!isEnd)
            {
                isEnd = true;
                // 아직 탐색이 남았다고 화면 표시
            }
            else
            {
                ExitRoomMode();
                new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = GameManager.Switch.RoomBackLabel }.ExecuteAsync().Forget();
            }
        }

        public void ScriptStopped()
        {
            IsScriptPlaying = false;
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

        private void ShowUI(List<string> uiList, AsyncToken asyncToken = default)
        {
            if (uiList == null || uiList.Count == 0) return;

            var showUI = new ShowUI { UINames = uiList };
            showUI.ExecuteAsync(asyncToken).Forget();
        }
    }
}
