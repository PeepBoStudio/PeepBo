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
        public StringParameter ClickerName { get; set; } = null;

        public void SwitchToNovel(StringParameter scriptName, StringParameter label)
        {
            new SwitchToNovelMode { ScriptName = scriptName, Label = label }.ExecuteAsync().Forget();
        }
    }


    [CommandAlias("clicker")]
    public class SwitchToClickerMode : Command
    {
        [ParameterAlias("scriptname")] public StringParameter ScriptName; // 클리커를 실행 할 나니스크립트
        [ParameterAlias("clickerName")] public StringParameter ClickerName; // 실행 시킬 클리커UI 이름

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var showUI = new ShowUI { UINames = new List<string> { ClickerName } };
            showUI.ExecuteAsync(asyncToken).Forget();

            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            var hidePrinter = new HidePrinter();
            hidePrinter.ExecuteAsync(asyncToken).Forget();

            GameManager.Command.ScriptName = ScriptName;
            GameManager.Command.ClickerName = ClickerName;
        }
    }

    [CommandAlias("vibrate")]
    public class Vibrate : Command
    {
        [ParameterAlias("time")] public IntegerParameter Time; // float가 없어서 int로 대체, 0.1초면 1에 해당
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            VibrateManager.Vibrate(Time*100);
        }
    }

    [CommandAlias("room")]
    public class SwitchToRoomMode : Command
    {
        [ParameterAlias("scriptname")] public StringParameter ScriptName; // 탐색을 완료하고 실행 할 나니스크립트
        [ParameterAlias("label")] public StringParameter Label; // 나니스크립트 내의 라벨

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            GameManager.Room.EnterRoomMode(ScriptName, Label, asyncToken);
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
            var hidePrinter = new HidePrinter();
            //hidePrinter.ExecuteAsync(asyncToken).Forget();
            await hidePrinter.ExecuteAsync(asyncToken);

            // 1. Disable Naninovel input.
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            // 2. Stop script player.
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();
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