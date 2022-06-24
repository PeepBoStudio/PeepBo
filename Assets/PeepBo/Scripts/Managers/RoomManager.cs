using Naninovel;
using Naninovel.Commands;
using PeepBo.UI.Popup;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PeepBo.Managers
{
    public class RoomManager
    {
        public Dictionary<string, Dictionary<string, bool>> roomInteractionDict = new Dictionary<string, Dictionary<string, bool>>();

        public StringParameter CurrentScriptName { get; set; } = null;

        public RoomManager()
        {
            // TODO : Serialization, json ����ȭ
            roomInteractionDict.Add(
                "Script101", new Dictionary<string, bool> 
                { 
                    {"�����", false },
                    {"�㺭��", true },
                    {"�߻�ȭ", false},
                    {"��",false },
                    {"����",true }
                }
            );
            roomInteractionDict.Add(
                "Script206", new Dictionary<string, bool>
                {
                    {"��",false },
                    {"�⵿���",false },
                    {"��������", false },
                    {"���డ��", false },
                    {"����",false },
                    {"�ڽ�",true },
                    {"����",true },
                    {"�⵿���2",false },
                    {"����",true },
                    {"��",false },
                    {"��",false },
                    {"��",true },
                }
            );
        }

        private RoomModeUI roomModeUI;

        public void InitRoomModeUI(RoomModeUI target) => roomModeUI = target;


        public void OnFind(string name)
        {
            roomModeUI.OnFinishInteraction(name);
        }

        public void StartRoomMode(StringParameter scriptName, StringParameter label, AsyncToken asyncToken = default)
        {
            CurrentScriptName = scriptName;

            HideUI(new List<string> { "RightTopUI" }, asyncToken);
            ShowUI(new List<string> { "RoomModeUI" }, asyncToken);
        }

        public void ExitRoomMode()
        {
            ShowUI(new List<string> { "RightTopUI" });
            HideUI(new List<string> { "RoomModeUI" });

            new SwitchToNovelMode { ScriptName = GameManager.Command.ScriptName, Label = GameManager.Command.RoomBackLabel }.ExecuteAsync().Forget();
        }

        public void RoomInteractionOccured(string roomName, string interactionName)
        {
            new SwitchToNovelMode { ScriptName = GameManager.Command.ScriptName, Label = interactionName }.ExecuteAsync().Forget();
        }

        public List<string> GetFindList()
        {
            List<string> ret = new List<string>();
            roomInteractionDict.TryGetValue(CurrentScriptName, out var list);
            list.ToList().ForEach((data) => { if (data.Value) ret.Add(data.Key); }); // ���ͷ��� �ʼ���� ����
            return ret;
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
