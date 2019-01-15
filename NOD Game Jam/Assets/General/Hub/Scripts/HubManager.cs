using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField]
    private GameObject confirmationPanel;
    private GameObject[] levelPins;
    private GameObject currentSelection;
    private int currentIndex;
    private bool selectOnCooldown;

    void Start()
    {
        levelPins = GameObject.FindGameObjectsWithTag("LevelPin");
        currentIndex = 0;
        currentSelection = levelPins[currentIndex];
        selectOnCooldown = false;
    }

    void Update()
    {
        HandleJoins();

        if (!selectOnCooldown)
        {
            HandleLevelSelection();
        }
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
        if (ReInput.players.AllPlayers[1].GetAxisRaw("Horizontal") > 0)
        {
            currentIndex += 1;

            if (currentIndex >= levelPins.Length)
            {
                currentIndex = 0;
            }

            StartCoroutine(SelectionCooldown());
            currentSelection.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            currentSelection = levelPins[currentIndex];
        }
        
        else if (ReInput.players.AllPlayers[1].GetAxisRaw("Horizontal") < 0)
        {
            if (currentIndex == 0)
            {
                currentIndex = levelPins.Length;
            }

            currentIndex -= 1;

            StartCoroutine(SelectionCooldown());
            currentSelection.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            currentSelection = levelPins[currentIndex];
        }

        else if (ReInput.players.AllPlayers[1].GetButtonDown("A"))
        {
            int currentSelectionSceneIndex = currentSelection.GetComponent<LevelPinSceneReference>().GetSceneIndex();
            SceneManager.LoadScene(currentSelectionSceneIndex);
        }

        currentSelection.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
    }

    private IEnumerator SelectionCooldown()
    {
        selectOnCooldown = true;
        yield return new WaitForSeconds(0.32f);
        selectOnCooldown = false;
    }
}
