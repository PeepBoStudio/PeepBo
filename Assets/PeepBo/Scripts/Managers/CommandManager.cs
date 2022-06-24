using Naninovel;
using Naninovel.Commands;
using Naninovel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PeepBo.Managers
{
    public class CommandManager
    {
        public StringParameter ScriptName { get; set; } = null;
        public StringParameter RoomBackLabel { get; set; } = null;
        public StringParameter ClickerBackLabel { get; set; } = null;
    }


    [CommandAlias("clicker")]
    public class SwitchToClickerMode : Command
    {
        [ParameterAlias("scriptname")] public StringParameter ScriptName; // 클리커를 실행 할 나니스크립트
        [ParameterAlias("label")] public StringParameter Label; // 나니스크립트 내의 라벨

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var showUI = new ShowUI { UINames = new List<string> { "Clicker" } };
            showUI.ExecuteAsync(asyncToken).Forget();

            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            var hidePrinter = new HidePrinter();
            hidePrinter.ExecuteAsync(asyncToken).Forget();

            GameManager.Command.ScriptName = ScriptName;
            GameManager.Command.ClickerBackLabel = Label;
        }
    }

    [CommandAlias("vibrate")]
    public class Vibrate : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
#if UNITY_ANDROID || UNITY_IOS
    Handheld.Vibrate();
#endif
        }
    }

    [CommandAlias("room")]
    public class SwitchToRoomMode : Command
    {
        [ParameterAlias("scriptname")] public StringParameter ScriptName; // 탐색을 완료하고 실행 할 나니스크립트
        [ParameterAlias("label")] public StringParameter Label; // 나니스크립트 내의 라벨

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

            GameManager.Command.ScriptName = ScriptName;
            GameManager.Command.RoomBackLabel = Label;

            GameManager.Room.StartRoomMode(ScriptName, Label, asyncToken);


            // 3. Reset state.
            //var stateManager = Engine.GetService<IStateManager>();
            //await stateManager.ResetStateAsync();

            // 4. Switch cameras.
            //var advCamera = GameObject.Find("AdventureModeCamera").GetComponent<Camera>();
            //advCamera.enabled = true;
            //var naniCamera = Engine.GetService<ICameraManager>().Camera;
            //naniCamera.enabled = true;

            // 5. Enable character control.
            //var controller = Object.FindObjectOfType<CharacterController3D>();
            //controller.IsInputBlocked = false;
        }
    }

    [CommandAlias("novel")]
    public class SwitchToNovelMode : Command
    {
        public StringParameter ScriptName;
        public StringParameter Label;

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            // 1. Disable character control.
            //var controller = Object.FindObjectOfType<CharacterController3D>();
            //controller.IsInputBlocked = true;

            // 2. Switch cameras.
            //var advCamera = GameObject.Find("AdventureModeCamera").GetComponent<Camera>();
            //advCamera.enabled = false;
            //var naniCamera = Engine.GetService<ICameraManager>().Camera;
            //naniCamera.enabled = true;
            // 3. Load and play specified script (if assigned).
            if (Assigned(ScriptName))
            {
                var scriptPlayer = Engine.GetService<IScriptPlayer>();
                await scriptPlayer.PreloadAndPlayAsync(ScriptName, label: Label);
            }
            // 4. Enable Naninovel input.
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = true;
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