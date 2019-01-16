using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_InputHandler : MonoBehaviour
{
    [SerializeField]
    private string _aButton;

    [SerializeField]
    private string _aButtonProduction;

    public void Init(int controllNumber)
    {
        _aButton = "Player" + controllNumber + "_ButtonA";
    }

    public void InitProduction()
    {
        _aButtonProduction = "A";

    }

    public bool IsButtonAPressed()
    {
        return Input.GetButtonDown(_aButton);
    }

    public bool IsButtonAPressedPro(int ID)
    {
        return Player.AllPlayers[ID].Input.GetButtonDown(_aButtonProduction);
    }
}
