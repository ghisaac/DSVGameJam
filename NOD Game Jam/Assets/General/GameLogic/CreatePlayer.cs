using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour
{ 
    [Header("0 = RED PLAYER, 1 = BLUE PLAYER, 2 = GREEN PLAYER, 3 = YELLOW PLAYER")]
    [Tooltip("DONT GET IT TWISTED!")]
    public GameObject[] AllPlayerPrefabs;
    [Header("This bool will spawn some fake players into allplayers before the spawing of the playercontollers.")]
    [Tooltip("OLY USED FOR DEBBUGING, DUMMY!")]
    public bool DEBUGGING = false;

    void Start()
    {
        if(DEBUGGING)
            Player.SpawnTestPlayers(4);

        for(int i=0; i< AllPlayerPrefabs.Length;i++)
        {
            GameObject obj = Instantiate(AllPlayerPrefabs[Player.AllPlayers[i].RewierdId]);
            obj.SendMessage("CreatePlayerController", Player.AllPlayers[i]);
        }
    }
}
