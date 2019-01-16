using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_PlayerManager : MonoBehaviour
{
    public GameObject playerPreFab;
    public List<CFT_PlayerController> players;
    public Transform playerContainer;
    
   

    public void Init(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject p = Instantiate(playerPreFab, playerContainer);
            CFT_PlayerController pc = p.GetComponent<CFT_PlayerController>();
            if(pc != null)
            {
                pc.Init(i);
                players.Add(pc);
            }
        }
    }

    public void InitProduction()
    {
        for (int i = 0; i < Player.AllPlayers.Count; i++)
        {
            GameObject p = Instantiate(playerPreFab, playerContainer);
            CFT_PlayerController pc = p.GetComponent<CFT_PlayerController>();
            if (pc != null)
            {
                pc.Init(Player.AllPlayers[i].RewierdId);
                players.Add(pc);
            }
        }
    }

    public void CanDrop(bool yesOrNo)
    {
        foreach(CFT_PlayerController p in players)
        {
            p.CanDrop(yesOrNo);
        }
    }

}
