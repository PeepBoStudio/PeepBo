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
    [SerializeField] private Image fillImage;

    int currentCount = 0;

    private void Start()
    {
        fillImage.fillAmount = (float)currentCount / targetCount;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentCount++;
            fillImage.fillAmount = (float)currentCount / targetCount;
            if (currentCount == 8)
            {
                fillImage.color=new Color(255/255f, 171/255f, 111/255f);
            }
            else if (currentCount == 13)
            {
                fillImage.color = new Color(255/255f, 111/255f, 111/255f);
            }
            else if (currentCount == targetCount)
            {
                var hideUI = new HideUI { UINames = new List<string> { transform.name } };
                hideUI.ExecuteAsync().Forget();

                new SwitchToNovelMode { ScriptName = GameManager.Command.ScriptName, Label = GameManager.Command.ClickerName }.ExecuteAsync().Forget();
            }
        };
    }
}
