using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;

public class HubManager : MonoBehaviour
{

    public GameObject[] levels;
    public int current;

    void Start()
    {
        current = 0;
    }

    void Update()
    {
        //ReInput.players.GetPlayer(Player.AllPlayers[0].RewierdId).GetButton("A");

        foreach(Rewired.Player p in ReInput.players.AllPlayers)
        {
            if(p.GetButtonDown("Start"))
            {
                if (!Player.CheckIfPlayerExists(p.id))
                {
                    new Player(p.id, "Maestro " +  p.id);
                }
            }
            else if (p.GetButtonDown("Back"))
            {
                if (Player.CheckIfPlayerExists(p.id))
                {
                    Player.RemovePlayer(p.id);
                }
            }

        }

    }

    private void HandleJoins()
    {
        //ReInput.players

            //När start trycks lägg till den kontrollern som en spelare
            //när back trycks ta bort den kontrollern från spelare

        //ReInput.configuration.
    }
}
