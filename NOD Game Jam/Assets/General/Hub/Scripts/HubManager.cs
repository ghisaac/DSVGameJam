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
        ReInput.players.GetPlayer(Player.AllPlayers[0].RewierdId).GetButton("A");

        if (Input.GetButtonDown("Start"))
        {

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
