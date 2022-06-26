using Naninovel;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteraction : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Room room;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        room = GetComponentInParent<Room>();
    }

    public void OnMouseDown()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        var inputManager = Engine.GetService<IInputManager>();
        if (scriptPlayer.Playing) // printer ������ ������
            return;

        if (inputManager.ProcessInput) // ���� input�� �ް� ������
            return;

        scriptPlayer.Stop();
        
        room.OnMouseDown();
    }

    public void OnMouseDrag()
    {
        /*
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if (scriptPlayer.Playing)
            return;

        var inputManager = Engine.GetService<IInputManager>();

        if (inputManager.ProcessInput)
            return;
        */
        room.OnMouseDrag();
    }

    public void OnMouseUpAsButton()
    {
        GameManager.Room.RoomInteractionOccured(room.gameObject.name, gameObject.name);
    }

    private void OnMouseExit()
    {
        //isClicked = false;
    }
}
