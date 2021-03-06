﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hanterar ett events i detta fall körse ett event när spelarna trycker på A - Micke
public class CFT_EventManager : MonoBehaviour
{
    public delegate void ClickAction_Player1();
    public static event ClickAction_Player1 OnClicked_Player1;

    public delegate void ClickAction_Player2();
    public static event ClickAction_Player2 OnClicked_Player2;

    public delegate void ClickAction_Player3();
    public static event ClickAction_Player3 OnClicked_Player3;

    public delegate void ClickAction_Player4();
    public static event ClickAction_Player4 OnClicked_Player4;

    public static void OnClicked(int ID)
    {
        switch (ID)
        {
            case 0:
                if (OnClicked_Player1 != null)
                    OnClicked_Player1();
                break;
            case 1:
                if (OnClicked_Player2 != null)
                    OnClicked_Player2();
                break;
            case 2:
                if (OnClicked_Player3 != null)
                    OnClicked_Player3();
                break;
            case 3:
                if (OnClicked_Player4 != null)
                    OnClicked_Player4();
                break;
        }    
    }
}
