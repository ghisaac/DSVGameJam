using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;

public class HubManager : MonoBehaviour
{

    public GameObject[] levels;
    public int current;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ReInput.players.GetPlayer(Player.AllPlayers[0].RewierdId).GetButton("A");

        foreach(Rewired.Player p in ReInput.players.AllPlayers)
        {
            if(p.GetButtonDown("Start"))
            {
                Debug.Log("  START" + p.id);
                if (!Player.CheckIfPlayerExists(p.id))
                {
                    new Player(p.id, "Maestro " +  p.id);
                    Debug.Log("  NEW " + p.id);
                }
            }

        }
        foreach (Rewired.Player p in ReInput.players.AllPlayers)
        {
            if (p.GetButtonDown("Back"))
            {
                Debug.Log("  Backubuttono Pressu " + p.id);
                if (Player.CheckIfPlayerExists(p.id))
                {
                    Player.RemovePlayer(p.id);
                    Debug.Log("  Playeru Ideruu remuvuu " + p.id);
                }
            }
        }
        Debug.Log("  All makt åt tengil");
        Debug.Log(" " + ReInput.players.AllPlayers[0].GetButtonDown("Back"));
        Debug.Log(" " + ReInput.players.AllPlayers[1].GetButtonDown("Back"));
        Debug.Log(" " + ReInput.players.AllPlayers[2].GetButtonDown("Back"));
        Debug.Log(" " + ReInput.players.AllPlayers[3].GetButtonDown("Back"));

    }

    private void HandleJoins()
    {
        //ReInput.players

            //När start trycks lägg till den kontrollern som en spelare
            //när back trycks ta bort den kontrollern från spelare

        //ReInput.configuration.
    }
}
