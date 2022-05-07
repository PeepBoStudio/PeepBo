using Naninovel;
using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteraction : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Room room;

    bool isClicked = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        room = GetComponentInParent<Room>();
    }

    private void OnMouseDown()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if (scriptPlayer.Playing)
            return;

        var inputManager = Engine.GetService<IInputManager>();

        if (inputManager.ProcessInput)
            return;

        scriptPlayer.Stop();
        room.OnMouseDown();
        isClicked = true;
    }

    private void OnMouseDrag()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if (scriptPlayer.Playing)
            return;

        var inputManager = Engine.GetService<IInputManager>();

        if (inputManager.ProcessInput)
            return;
        room.OnMouseDrag();
    }

    private void OnMouseUp()
    {
        if (isClicked) // 이벤트 발생
        {
            GameManager.Room.RoomInteractionOccured(room.gameObject.name, gameObject.name);
        }
    }

    private void OnMouseExit()
    {
        isClicked = false;
    }
}
