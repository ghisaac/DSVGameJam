using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_InputHandler : MonoBehaviour
{
    [SerializeField]
    private string _aButtonProduction;


    public void InitProduction()
    {
        _aButtonProduction = "A";
    }

    public bool IsButtonAPressedPro(int ID)
    {
        return Player.GetPlayerByRewindID(ID).Input.GetButtonDown(_aButtonProduction);
    }
}
