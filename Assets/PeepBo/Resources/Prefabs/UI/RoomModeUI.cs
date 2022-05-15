using Naninovel;
using Naninovel.Commands;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomModeUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject findListObject;
    [SerializeField] private GameObject roomModeIcon;
    [SerializeField] private Sprite findSprite;
    [SerializeField] private Text panelText;

    private bool isEnd = false;
    private int findCount = 0;
    private List<string> findList = new List<string>();
    private Dictionary<string, GameObject> findObjectDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        GameManager.Room.InitRoomModeUI(this);

        exitButton.onClick.AddListener(OnClickExitButton);
        findList = GameManager.Room.GetFindList();
        for(int i=0; i<findList.Count; i++)
        {
            var findObject = Instantiate(roomModeIcon, findListObject.transform);
            findObject.name = findList[i];
            findObjectDict.Add(findObject.name, findObject);
        }
    }

    public void OnFinishInteraction(string interactionName)
    {
        findObjectDict[interactionName].GetComponent<Image>().sprite = findSprite;
        findCount++;
        if(findCount == findList.Count)
        {
            panelText.text = "이만하면 된 것 같아";
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
