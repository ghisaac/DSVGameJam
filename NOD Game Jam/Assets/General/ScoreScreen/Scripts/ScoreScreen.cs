using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{

    [SerializeField] private ScoreScreenPlayerScore[] playerScoreObjects;
    public bool testing = false;


    void Start()
    {
        if(testing)
            Player.SpawnTestPlayers(4);
        ActivateScores();
    }


    void Update()
    {
        
    }

    private void ActivateScores()
    {
        for(int i = 0; i < playerScoreObjects.Length; i++)
        {
            playerScoreObjects[i].SetValues(Player.GetPlayerAtPlacement(i));
        }
    }
}
