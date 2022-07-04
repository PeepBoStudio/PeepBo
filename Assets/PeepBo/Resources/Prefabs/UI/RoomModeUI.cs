using Naninovel;
using Naninovel.Commands;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomModeUI : MonoBehaviour
{
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Sprite findSprite;
    [SerializeField] private Sprite exitSprite;
    [SerializeField] private List<GameObject> findObjectList;

    private bool isEnd = false;
    private int findCount = 0;
    private List<string> findList = new List<string>();
    private Dictionary<string, GameObject> findObjectDict = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> alreadyFindDict = new Dictionary<string, bool>();

    bool isShow = false;

    public void OnShow() // 에디터 내에서 Serialize됨
    {
        if (isShow) return;

        isShow = true;

        GameManager.Room.InitRoomModeUI(this);

        TryTutorialSetUp();
        Init();
    }

    private void Init()
    {
        exitButton.onClick.AddListener(OnClickExitButton);
        findList = GameManager.Room.GetFindList();
        for (int i = 0; i < findList.Count; i++)
        {
            var obj = findObjectList[i];
            obj.SetActive(true); // 활성화
            obj.transform.name = findList[i];

            findObjectDict.Add(findList[i], obj);
            alreadyFindDict.Add(findList[i], false);
        }
    }

    private void TryTutorialSetUp()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if(scriptPlayer.PlayedScript.name == "Script101")
            tutorialButton.gameObject.SetActive(true);
    }

    public void OnHide() // 에디터 내에서 Serialize됨
    {
        isShow = false;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if (scriptPlayer?.PlayedScript?.name == "Script101")
            tutorialButton.gameObject.SetActive(false);
    }

    public void OnFinishInteraction(string interactionName)
    {
        findObjectDict.TryGetValue(interactionName, out GameObject obj);
        alreadyFindDict.TryGetValue(interactionName, out bool alreadyFind);
        if (alreadyFind) return;

        obj.GetComponentInChildren<Image>().sprite = findSprite;
        obj.GetComponentInChildren<Text>().text = obj.name;

        alreadyFindDict[interactionName] = true;
        findCount++;

        if(findCount == findList.Count)
        {
            var image = exitButton.GetComponent<Image>();
            image.sprite = exitSprite;
            //panelText.text = "이만하면 된 것 같아";
            isEnd = true;
        }
    }

    public void OnClickExitButton()
    {
        if(isEnd)
            GameManager.Room.ExitRoomMode();
        else
            NoExit();
    }

    private async void NoExit()
    {
        var name = new List<string> { "NoExitRoomModeUI" };
        var showUI = new ShowUI { UINames = name, Duration = (DecimalParameter)0.5 };
        //showUI.ExecuteAsync().Forget();
        await showUI.ExecuteAsync();

        var hideUI = new HideUI { UINames = name, Duration = (DecimalParameter)0.5 };
        hideUI.ExecuteAsync().Forget();
    }
}
