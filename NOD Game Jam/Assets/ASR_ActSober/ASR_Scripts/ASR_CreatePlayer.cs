using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_CreatePlayer : MonoBehaviour
{
    public ASR_CharacterController PlayerController;

    public void CreatePlayerController(Player player)
    {
        PlayerController.Player = player;
    }
}
