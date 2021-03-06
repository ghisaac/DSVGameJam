﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// En klass som samlar poäng och delar ut poäng för spelet, Micke Patric
public class CFT_WinnerData
{
    public List<CFT_RoundData> rounds;
    public List<CFT_PlayerData> player;

    public CFT_WinnerData()
    {
        rounds = new List<CFT_RoundData>();
        player = new List<CFT_PlayerData>();
    }

    // Hämtar en vinnare
    public int GetWinner()
    {
        player.Sort(new SortByScoreDecending());
        return player[0].Id;
    }

    // Sätter Ger spelarna sin plasering i spelet.
    public void SetPlayerAllOverPlacement()
    {
        List<int> placement = new List<int>();
        foreach(CFT_PlayerData p in player)
        {
            if(placement.Contains(p.Score) == false)
                placement.Add(p.Score);
        }

        placement.Sort();
        placement.Reverse();

        for (int i = 0; i < placement.Count; i++)
        {
            for (int j = 0; j < player.Count; j++)
            {
                if(placement[i] == player[j].Score)
                {
                    player[j].TotalPlacement = i + 1;
                }
            }
        }
    }

    //Ger poäng till spelarna beroende på runda
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
    //Adderar poäng
    private void AddPoints(int[] ids,int point)
    {
        foreach(int id in ids)
        {
           
            CFT_PlayerData p = new CFT_PlayerData(id);
            if (player.Contains(p))
            {
                foreach(CFT_PlayerData pl in player)
                {
                    if(pl.Id == p.Id)
                    {
                        pl.Score += point;
                    }
                }
            }
            else
            {
                p.Score += point;
                player.Add(p);
            }
        }
    }

    // Egen sotering Klass Som soterar med spelar data på poäng.
    private class SortByScoreDecending : IComparer<CFT_PlayerData>
    {
        public int Compare(CFT_PlayerData x, CFT_PlayerData y)
        {
            if (x.Score < y.Score)
                return 1;
            if (x.Score > y.Score)
                return -1;
            else
                return 0;
        }
    }

}




