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
        // TODO : Serialization, json ����ȭ
        public Dictionary<string, Dictionary<string, (string, bool)>> roomInteractionDict = new Dictionary<string, Dictionary<string, (string, bool)>>();

        public RoomManager()
        {
            roomInteractionDict.Add(
                "1-1", new Dictionary<string, (string, bool)> 
                { 
                    {"�����", ("Dongbaek", false) },
                    {"�㺭��", ("Wall", true) },
                    {"�߻�ȭ", ("Flower", false) },
                    {"��", ("House", false) },
                    {"����", ("Roof", true) }
                }
            );
        }

        public bool IsScriptPlaying { get; set; } = true;

        private RoomModeUI roomModeUI;

        public void InitRoomModeUI(RoomModeUI target) => roomModeUI = target;


        public void OnFind(string name)
        {
            roomModeUI.OnFinishInteraction(name);
        }

        public void StartRoomMode(StringParameter scriptName, StringParameter label, AsyncToken asyncToken = default)
        {
            // TODO : scriptName�� label�� � Ž���������� ��Ī

            // ��Ī�ߴٰ� ����
            HideUI(new List<string> { "RightTopUI" }, asyncToken);
            ShowUI(new List<string> { "RoomModeUI" }, asyncToken);

            IsScriptPlaying = false;
        }

        public void ExitRoomMode()
        {
            // TODO : StateLess�ϰ� ����
            IsScriptPlaying = true;

            ShowUI(new List<string> { "RightTopUI" });
            HideUI(new List<string> { "RoomModeUI" });

            new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = GameManager.Switch.RoomBackLabel }.ExecuteAsync().Forget();
        }

        public void RoomInteractionOccured(string roomName, string interactionName)
        {
            if (IsScriptPlaying)
                return;

            switch (interactionName)
            {
                case "�߻�ȭ":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Flower" }.ExecuteAsync().Forget();
                    break;
                case "����":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Roof" }.ExecuteAsync().Forget();
                    break;
                case "��":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "House" }.ExecuteAsync().Forget();
                    break;
                case "�����":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "DongBaek" }.ExecuteAsync().Forget();
                    break;
                case "�㺭��":
                    new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = "Wall" }.ExecuteAsync().Forget();
                    break;
            }
        }

        public void ScriptStopped()
        {
            IsScriptPlaying = false;
        }

        public List<string> GetFindList()
        {
            // TODO : ����Ʈ ����ȭ
            return new List<string> { "Roof", "Wall" };
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

    [CommandAlias("stopscript")]
    public class StopScript : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            // 1. Disable Naninovel input.
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            // 2. Stop script player.
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            var hidePrinter = new HidePrinter();
            hidePrinter.ExecuteAsync(asyncToken).Forget();

            GameManager.Room.IsScriptPlaying = false;
        }
    }

    [CommandAlias("find")]
    public class FindSomething : Command
    {
        [ParameterAlias("name")] public StringParameter Name;
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameManager.Room.OnFind(Name);
        }
    }
}
