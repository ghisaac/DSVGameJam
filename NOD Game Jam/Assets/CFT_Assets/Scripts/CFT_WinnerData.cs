using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_WinnerData
{
    public List<CFT_RoundData> rounds;
    public List<CFT_PlayerData> player;

    public CFT_WinnerData()
    {
        rounds = new List<CFT_RoundData>();
        player = new List<CFT_PlayerData>();
    }

    public int GetWinner()
    {
        player.Sort();
        return player[(player.Count - 1)].Id;

    }

    public void SetRoundScore(int round)
    {
        foreach (KeyValuePair<int, int[]> _playerAndScore in rounds[round].placements)
        {
            switch (_playerAndScore.Key)
            {
                case 1:
                    AddPoints(_playerAndScore.Value,100);
                    break;
                case 2:
                    AddPoints(_playerAndScore.Value, 75);
                    break;
                case 3:
                    AddPoints(_playerAndScore.Value, 50);
                    break;
                case 4:
                default:
                    AddPoints(_playerAndScore.Value, 0);
                    break;
            }
        }
    }

    private void AddPoints(int[] ids,int point)
    {
        foreach(int id in ids)
        {
            CFT_PlayerData p = new CFT_PlayerData(id);
            if (player.Contains(p))
            {
                player[id].Score += point;
            }
            else
            {
                p.Score += point;
                player.Add(p);
            }
        }
    }
}
