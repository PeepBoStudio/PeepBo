using Naninovel;
using Naninovel.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PeepBo.Managers
{
    public class HogamManager
    {
        public Dictionary<string, (string, int)> hogamDict = new Dictionary<string, (string, int)>(); // key : text, value : (ĳ���� �̸�, ����)

        Hogam hogam;

        public HogamManager() 
        {
            // TODO : ���� ������ ���̺� �����
            hogamDict.Add("�׿��� ���� ��︮�׿䡦", ("���", 5)); // 1-1
            hogamDict.Add("�˷��ش�", ("����", 5)); // 1-2
            hogamDict.Add("�ǿܷ� ���� ��ȸ������. ���� �ٴѴ�", ("����", 5)); // 1-2
            hogamDict.Add("�̸��� �� ���ڳ׿�", ("����", 5));
            hogamDict.Add("������ �װ� �ϴ� ��� �������д�", ("����", 5));
            hogamDict.Add("������", ("����", 5));
            hogamDict.Add("����", ("����", 5));
            hogamDict.Add("��", ("���", 5));
            hogamDict.Add("����", ("����", 5));
            hogamDict.Add("� �Ӹ��� â���� ����", ("����", 5));
        }

        private void Custom_OnVariableUpdated(CustomVariableUpdatedArgs obj)
        {
            if(obj.Name == "choiceText")
            {
                var custom = Engine.GetService<ICustomVariableManager>();

                string text = obj.Value;
                hogamDict.TryGetValue(text, out var result);

                if (result == default)
                {
                    custom.SetVariableValue("isHogam", "false");
                }
                else
                {
                    custom.SetVariableValue("isHogam", "true");
                    hogam.InitHogam(result.Item1, result.Item2);
                }
            }
            else if(obj.Name == "choiceButtonInfo")
            {
                var array = obj.Value.Split(' '); // x, y, width, height
                OnClickHogamChoiceButton(
                    new Vector2(float.Parse(array[0]), float.Parse(array[1])),
                    new Vector2(float.Parse(array[2]), float.Parse(array[3])));
            }
        }

        public void SetHogamScript(Hogam target)
        {
            hogam = target;

            var custom = Engine.GetService<ICustomVariableManager>();
            custom.OnVariableUpdated += Custom_OnVariableUpdated;
        }

        private async void OnClickHogamChoiceButton(Vector2 choiceScreenPos, Vector2 choiceScreenSize)
        {
            //UIChoiceHandler.HandleChoice���� �̹� wait ����
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            var rectTransform = hogam.GetComponent<RectTransform>();

            var cameraManager = Engine.GetService<ICameraManager>();
            Vector2 uiScreenPosition = new Vector2(
                choiceScreenPos.x + choiceScreenSize.x / 2 - rectTransform.sizeDelta.x / 2 - Screen.width / 2, 
                choiceScreenPos.y + choiceScreenSize.y / 2 + rectTransform.sizeDelta.y / 2 - Screen.height / 2);
            //Vector2 uiWorldPosition = cameraManager.UICamera.ScreenToWorldPoint(uiScreenPosition);



            rectTransform.anchoredPosition = uiScreenPosition; 
            //rectTransform.position = new Vector3(uiWorldPosition.x, uiWorldPosition.y, 0); // posz 20000
            //rectTransform.position = uiWorldPosition; // posz 20000
            //rectTransform.anchoredPosition = uiScreenPosition;
            //rectTransform.anchoredPosition = choiceScreenPos;

            var showUI = new ShowUI { UINames = new List<string> { "HogamUI" } };
            showUI.Duration = 0.5f;
            await showUI.ExecuteAsync();

            var hideUI = new HideUI { UINames = new List<string> { "HogamUI" } };
            hideUI.Duration = 0.5f;
            await hideUI.ExecuteAsync();
        }

        /*
        public async void OnClickChoiceButton(string text, Vector2 choiceScreenPos, Vector2 choiceScreenSize)
        {
            hogamDict.TryGetValue(text, out var result);

            if (result == default) return;

            hogam.InitHogam(result.Item1, result.Item2);

            var showUI = new ShowUI { UINames = new List<string> { "HogamUI" } };
            showUI.Duration = 0.5f;
            await showUI.ExecuteAsync();

            //UIChoiceHandler.HandleChoice���� wait ����

            var hideUI = new HideUI { UINames = new List<string> { "HogamUI" } };
            hideUI.Duration = 0.5f;
            await hideUI.ExecuteAsync();

            Debug.Log(text);
            Debug.Log(choiceScreenPos);
            Debug.Log(choiceScreenSize);
        }
        */
    }
}
