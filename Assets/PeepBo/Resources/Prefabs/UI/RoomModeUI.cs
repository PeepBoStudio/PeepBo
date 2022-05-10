using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomModeUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    private void Awake()
    {
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickExitButton()
    {
        GameManager.Room.RoomExitButtonCliked();
        // TODO : Ž�� ���� ���� Ȯ���ϱ�
    }
}
