using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_PlayerController : MonoBehaviour
{
    public CFT_InputHandler _inputHandler;
    public static int NumberOfPlayers = 0;
    public int ID;
    public int controllNumber;
    private bool isProduction;
    private bool _canDrop;
    public int score;

    public void Init(int ID)
    {
        this.ID = ID;
        NumberOfPlayers++;
        controllNumber = NumberOfPlayers;
        _inputHandler.Init(controllNumber);
        isProduction = false;
        _canDrop = true;

    }

    public void InitProduction(int ID)
    {
        this.ID = ID;
        NumberOfPlayers++;
        controllNumber = NumberOfPlayers;
        _inputHandler.InitProduction();
        isProduction = true;
        _canDrop = true;
    }

    private void Update()
    {
        if (isProduction)
        {
            PlayerProInput();
        }
        else
        {
            PlayerInput();
        }
  
    }

    private void PlayerProInput()
    {
        if (_inputHandler.IsButtonAPressedPro(ID) && _canDrop)
        {
            Drop();
        }
    }

    private void PlayerInput()
    {
        if (_inputHandler.IsButtonAPressed() && _canDrop)
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
