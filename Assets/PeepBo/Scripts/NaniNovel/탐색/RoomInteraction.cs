using Naninovel;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomInteraction : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Room room;
    Vector2 lastTouchPos;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        room = GetComponentInParent<Room>();
    }

    bool CanProcessInput()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        var inputManager = Engine.GetService<IInputManager>();
        if (scriptPlayer.Playing) // printer ������ ������
            return false;

        if (inputManager.ProcessInput) // ���� input�� �ް� ������
            return false;

        return true;
    }

    bool IsMoved(Vector2 currentTouchPos) // �巡�� �� Ŭ���� �ƴ� ���� Ŭ������ �����ϱ� ����
    {
        if ((lastTouchPos - currentTouchPos).magnitude == 0) return false;
        return true;
    }

    public void OnMouseDown()
    {
        if (!CanProcessInput()) return;

        lastTouchPos = Input.mousePosition;

        room.OnMouseDown();
    }

    public void OnMouseDrag()
    {
        if (!CanProcessInput()) return;

        room.OnMouseDrag();
    }

    public void OnMouseUpAsButton()
    {
        //if (!IsMoved(Input.mousePosition))
        if (!CanProcessInput()) return;

        GameManager.Room.RoomInteractionOccured(room.gameObject.name, gameObject.name);
    }

    /*
    private void OnMouseOver()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        var inputManager = Engine.GetService<IInputManager>();
        if (scriptPlayer.Playing)
            return;

        if (inputManager.ProcessInput)
            return;
        Debug.Log(transform.name);
    }
    */
}
