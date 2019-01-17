using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spelar Kontrollern - Micke
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

    // hanterar spelar input
    private void PlayerProInput()
    {
        if (_inputHandler.IsButtonAPressedPro(ID) && _canDrop)
        {
            Drop();
        }
    }

    // kör ett event när spelaren trycker på A
    private void Drop()
    {
        CFT_EventManager.OnClicked(ID);
    }

    public void CanDrop(bool yesOrNo)
    {
        _canDrop = yesOrNo;
    }
}
