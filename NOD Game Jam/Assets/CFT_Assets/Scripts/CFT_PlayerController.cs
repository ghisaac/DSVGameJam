using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_PlayerController : MonoBehaviour
{
    public CFT_InputHandler _inputHandler;
    public int ID;
    private bool _canDrop;
    public int score;

    public void InitProduction(int ID)
    {
        this.ID = ID;
        _inputHandler.InitProduction();
        _canDrop = true;
    }

    private void Update()
    {
        PlayerProInput();
    }

    private void PlayerProInput()
    {
        if (_inputHandler.IsButtonAPressedPro(ID) && _canDrop)
        {
            Drop();
        }
    }


    private void Drop()
    {
        CFT_EventManager.OnClicked(ID);
    }

    public void CanDrop(bool yesOrNo)
    {
        _canDrop = yesOrNo;
    }
}
