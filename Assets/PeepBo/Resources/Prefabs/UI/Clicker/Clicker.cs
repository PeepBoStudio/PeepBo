using Naninovel;
using Naninovel.Commands;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private int targetCount;
    [SerializeField] private Button button;
    [SerializeField] private Image fillImage;

    int currentCount = 0;

    private void Start()
    {
        button.onClick.AddListener(()=> 
        { 
            currentCount++;
            fillImage.fillAmount = (float)currentCount / targetCount;
            if(currentCount == targetCount)
            {
                var hideUI = new HideUI { UINames = new List<string> { "Clicker" } };
                hideUI.ExecuteAsync().Forget();

                new SwitchToNovelMode { ScriptName = GameManager.Command.ScriptName, Label = GameManager.Command.ClickerBackLabel }.ExecuteAsync().Forget();
            }
        });
    }
}
