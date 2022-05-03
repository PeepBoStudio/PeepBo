using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeepBo.Managers
{
    public class RoomManager
    {
        public void Test()
        {
            var switchCommand = new SwitchToNovelMode { ScriptName = GameManager.Switch.ScriptName, Label = GameManager.Switch.Label };
            switchCommand.ExecuteAsync().Forget();
        }
    }
}
