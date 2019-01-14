using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;

public class HubManager : MonoBehaviour
{

    private GameObject[] levelPins;
    private GameObject currentSelection;
    private int currentIndex;

    void Start()
    {
        levelPins = GameObject.FindGameObjectsWithTag("LevelPins");
        currentIndex = 0;
        currentSelection = levelPins[currentIndex];
    }

    void Update()
    {
        HandleJoins();
        HandleLevelSelection();
    }

    private void HandleJoins()
    {
        foreach (Rewired.Player p in ReInput.players.AllPlayers)
        {
            if (p.GetButtonDown("Start"))
            {
                if (!Player.CheckIfPlayerExists(p.id))
                {
                    new Player(p.id, "Maestro " + p.id);
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

    private void HandleLevelSelection()
    {
        if (ReInput.players.AllPlayers[0].GetAxisRaw("Horizontal") > 0)
        {
            currentIndex += 1;

            if (currentIndex > levelPins.Length)
            {
                currentIndex = 0;
            }

            currentSelection = levelPins[currentIndex];
        }

        else if (ReInput.players.AllPlayers[0].GetAxisRaw("Horizontal") < 0)
        {
            currentIndex -= 1;

            if (currentIndex < 0)
            {
                currentIndex = 0;
            }

            currentSelection = levelPins[currentIndex];
        }
    }
}
